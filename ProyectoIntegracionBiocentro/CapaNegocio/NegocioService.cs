using CapaDTO;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Net;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;

namespace CapaNegocio
{
    //[ServiceBehavior]
    public class NegocioService ///: INegocioService
    {
        private UtilMethods utilMethods;

        public NegocioService()
        {
            this.utilMethods = new UtilMethods();
        }
        //Guarda las reservas
        public void registrarPaciente(Persona persona,int idHoraAtencion)
        {
            int? idPersona = null;
            int? idUsuario = null;
            int? idReserva = null;
            try
            {
                String query = "";
                Usuario usuario = null;
                List<Usuario> listUsuario = generarListaUsuarios(persona.Rut);
                if (listUsuario== null)
                {
                    query = "INSERT INTO BIOCENTRO_DB.dbo.PERSONA (RUT,NOMBRE,APELLIDO_PATERNO,APELLIDO_MATERNO) OUTPUT INSERTED.ID_PERSONA VALUES " +
                            " ('" + persona.Rut + "','" + persona.Nombre + "','" + persona.ApellidoPaterno + "','" + persona.ApellidoMaterno + "');";
                    idPersona =  this.utilMethods.guardarEliminarActualizarObjeto(query, true);
                }
                else
                {
                    idPersona = listUsuario[0].IdPersona.IdPersona;
                    usuario = listUsuario.FirstOrDefault(u=>u.IdRol.IdRol==4);
                }
                if (listUsuario==null || usuario==null)
                {
                    query = "INSERT INTO BIOCENTRO_DB.dbo.USUARIO (ESTADO,ID_ROL,ID_PERSONA) OUTPUT INSERTED.ID_USUARIO VALUES(1, 4, " + idPersona + "); ";
                    idUsuario = this.utilMethods.guardarEliminarActualizarObjeto(query, true);
                }
                else
                {
                    idUsuario = usuario.IdUsuario;
                }
                query = "INSERT INTO BIOCENTRO_DB.dbo.RESERVA (ID_HORA,ID_PACIENTE,ID_ESTADO) OUTPUT INSERTED.ID_RESERVA VALUES (" + idHoraAtencion+ "," + idUsuario + ", 1); ";
                idReserva = this.utilMethods.guardarEliminarActualizarObjeto(query, true);
            }
            catch (Exception ex)
            {
                eliminar("PERSONA", "ID_PERSONA", idPersona);
                eliminar("USUARIO", "ID_USUARIO", idUsuario);
                eliminar("RESERVA", "ID_RESERVA", idReserva);
                verErrorEnConsola(ex, "guardarPaciente");
                throw new Exception("Ocurrio un error al realizar la operacion ", ex);
            }
        }
        //Retorna el listado completo o filtrado de las Horas de atencion disponibles
        public List<HoraAtencion> buscarHorasDisponibles(Especialidad especialidad, DateTime? fecha, Persona persona)
        {
            try
            {
                List<Reserva> listReserva = buscarReservaAndEstado();
                if (listReserva == null)
                {
                    return null;
                }
                List<HoraAtencion> listHoraAtencion = buscarInfoHorasEspecialistas(null);
                if (listHoraAtencion == null)
                {
                    return null;
                }
                List<HoraAtencion> listaFinal = listHoraAtencion.FindAll(h => 
                    validarReserva(h, listReserva) && filtrarEspecialidad(h,especialidad) && filtrarFecha(h, fecha) && filtrarTerapeuta(h, persona));
                return listaFinal;
            }
            catch(Exception ex)
            {
                verErrorEnConsola(ex, "buscarHorasDisponibles");
                throw new Exception("Ocurrio un error al realizar la operacion ", ex);
            }
        }
        //Retorna todas las especialidades registradas
        public List<Especialidad> generarListaEspecialidad()
        {
            try
            {
                String queryTerapeuta = "SELECT ID_ESPECIALIDAD,NOMBRE as nombreEspecialidad FROM BIOCENTRO_DB.dbo.ESPECIALIDAD";
                DataSet dataTable = this.utilMethods.listarObjetoConTablaEspecifica(queryTerapeuta, "ESPECIALIDAD");
                if (dataTable.Tables[0].Rows.Count == 0)
                {
                    return null;
                }
                List<Especialidad> list = new List<Especialidad>();
                foreach (DataRow row in dataTable.Tables[0].Rows)
                {

                    list.Add(generarObjetEspecialidad(row));
                }
                return list;
            }
            catch (Exception ex)
            {
                verErrorEnConsola(ex, "generarListaEspecialidad");
                throw new Exception("Ocurrio un error al realizar la operacion ", ex);
            }
        }
        //Retorna la data de todos los especialistas disponibles
        public List<Terapeuta> generarListaEspecialista()
        {
            try
            {
                //Query retorna los datos de especialistas, roles, persona, usuario
                String query = "SELECT usu.ID_USUARIO,usu.ID_ROL,"
                + " per.ID_PERSONA,per.RUT,per.NOMBRE,per.APELLIDO_PATERNO,per.APELLIDO_MATERNO,"
                + " esp.NOMBRE as nombreEspecialidad, esp.ID_ESPECIALIDAD"
                + " FROM BIOCENTRO_DB.dbo.USUARIO as usu JOIN BIOCENTRO_DB.dbo.PERSONA as per on usu.ID_PERSONA = per.ID_PERSONA"
                + " JOIN BIOCENTRO_DB.dbo.TERAPEUTA as tera ON tera.ID_USUARIO = usu.ID_USUARIO"
                + " JOIN BIOCENTRO_DB.dbo.ESPECIALIDAD as esp ON esp.ID_ESPECIALIDAD = tera.ID_ESPECIALIDAD WHERE usu.ESTADO = 1; ";
                DataSet dataTable = this.utilMethods.listarObjetoMultiTabla(query);
                if (dataTable.Tables[0].Rows.Count == 0)
                {
                    return null;
                }
                List<Terapeuta> list = new List<Terapeuta>();
                foreach (DataRow row in dataTable.Tables[0].Rows)
                {
                    Terapeuta terapeuta = generarObjetoTerapeuta(row);
                    String nombreCompleto = terapeuta.IdUsuario.IdPersona.Nombre + " " + terapeuta.IdUsuario.IdPersona.ApellidoPaterno + " " + terapeuta.IdUsuario.IdPersona.ApellidoMaterno;
                    terapeuta.IdUsuario.IdPersona.Nombre = nombreCompleto;
                    list.Add(terapeuta);
                }
                return list;
            }
            catch (Exception ex)
            {
                verErrorEnConsola(ex, "generarListaEspecialista");
                throw new Exception("Ocurrio un error al realizar la operacion ", ex);
            }
        }
        //Retorna un unico Usuario
        public Usuario buscarPaciente(String rut)
        {
            try
            {
                List<Usuario> list = generarListaUsuarios(rut);
                if (list == null)
                {
                    return null;
                }
                return list[0];
            }
            catch (Exception ex)
            {
                verErrorEnConsola(ex, "buscarPaciente");
                throw new Exception("Ocurrio un error al realizar la operacion ", ex);
            }
        }
        //Retorna la data de pacientes y reservas
        public List<Reserva> listaReservasPorRut(String rut)
        {
            try
            {
                //Query entrega los datos del Paciente y la reserva realizada
                String query = "SELECT estRes.DESCRIPCION,estRes.ID_ESTADO ,usu.ID_USUARIO,usu.ID_ROL, usuRol.NOMBRE as nombreRol,"
                    + " per.ID_PERSONA,per.RUT,per.NOMBRE,per.APELLIDO_PATERNO,per.APELLIDO_MATERNO,res.ID_RESERVA,res.ID_HORA,hAtencion.FECHA "
                    + "FROM BIOCENTRO_DB.dbo.PERSONA as per"
                    + " JOIN BIOCENTRO_DB.dbo.USUARIO as usu ON usu.ID_PERSONA = per.ID_PERSONA" 
                    + " JOIN BIOCENTRO_DB.dbo.RESERVA as res ON res.ID_PACIENTE = usu.ID_USUARIO" 
                    + " JOIN BIOCENTRO_DB.dbo.ESTADO_RESERVA as estRes ON estRes.ID_ESTADO = res.ID_ESTADO" 
                    + " JOIN BIOCENTRO_DB.dbo.ROL_USUARIO as usuRol ON usuRol.ID_ROL = usu.ID_ROL" 
                    + " JOIN BIOCENTRO_DB.dbo.HORA_ATENCION as hAtencion ON hAtencion.ID_HORA = res.ID_HORA"
                    + " WHERE per.RUT = '"+ rut + "' and estRes.ID_ESTADO = 1; ";
                DataSet dataTable = this.utilMethods.listarObjetoMultiTabla(query);
                if (dataTable.Tables[0].Rows.Count == 0)
                {
                    return null;
                }
                List<Reserva> listaReserva = new List<Reserva>();
                foreach (DataRow row in dataTable.Tables[0].Rows)
                {
                    listaReserva.Add(generarObjetoReserva(row,true));
                }
                List<int> listId = new List<int>();
                List<Reserva> listaReservasValidas = listaReserva.FindAll(r => r.IdHora.Fecha.CompareTo(DateTime.Today) >= 0);
                listaReservasValidas.ForEach(res => listId.Add(res.IdHora.IdHora));
                List<HoraAtencion> listHotaAtencion = buscarInfoHorasEspecialistas(listId);
                listaReservasValidas.ForEach(res => res.IdHora= listHotaAtencion.FirstOrDefault(h => h.IdHora == res.IdHora.IdHora));
                return listaReservasValidas;
            }
            catch (Exception ex)
            {
                verErrorEnConsola(ex, "buscarReservasPorRut");
                throw new Exception("Ocurrio un error al realizar la operacion ", ex);
            }
        }
        //cambioEstado de la reserva debe ser  1 = Confirmar(id = 2) y 0 = Rechazar(id = 3)
        public void rechazarConfirmarReserva(Char cambioEstado,int idReserva)
        {
            try
            {
                if (!Char.IsDigit(cambioEstado) || cambioEstado.CompareTo('1') > 0 || idReserva.CompareTo(null)==0)
                {
                    throw new Exception(" Data ingresada incorrecta ",new Exception());
                }
                String estado = String.Empty;
                if (cambioEstado.CompareTo('0')==0)
                {
                    estado = "3";
                }
                else if (cambioEstado.CompareTo('1') == 0)
                {
                    estado = "2";
                }
                actualizar("RESERVA", "ID_ESTADO", estado, "ID_RESERVA", Convert.ToString(idReserva));
            }
            catch (Exception ex)
            {
                verErrorEnConsola(ex, "rechazarConfirmarReserva");
                throw new Exception("Ocurrio un error al realizar la operacion ", ex);
            }
        }
        // Fin Metodos Publicos
        private bool filtrarTerapeuta(HoraAtencion h, Persona persona)
        {
            if (persona == null)
            {
                return true;
            }
            if (h.IdTerapeuta.IdUsuario.IdPersona.IdPersona==persona.IdPersona)
            {
                return true;
            }
            return false;
        }
        private bool filtrarFecha(HoraAtencion h, DateTime? fecha)
        {
            if (fecha==null)
            {
                return true;
            }
            if (h.Fecha.Day== fecha.Value.Day && h.Fecha.Month == fecha.Value.Month && h.Fecha.Year == fecha.Value.Year)
            {
                return true;
            }
            return false;
        }
        private bool filtrarEspecialidad(HoraAtencion h, Especialidad especialidad)
        {
            if (especialidad==null)
            {
                return true;
            }
            if (especialidad.IdEspecialidad==h.IdTerapeuta.IdEspecialidad.IdEspecialidad)
            {
                return true;
            }
            return false;
        }
        
        private List<Usuario> generarListaUsuarios(String rut)
        {
            try
            {
                String query = "SELECT usu.ID_USUARIO,usu.ID_ROL, per.ID_PERSONA,per.RUT,per.NOMBRE,per.APELLIDO_PATERNO,per.APELLIDO_MATERNO"
                    + " FROM BIOCENTRO_DB.dbo.PERSONA as per "
                    + " JOIN BIOCENTRO_DB.dbo.USUARIO as usu ON usu.ID_PERSONA = per.ID_PERSONA "
                    + " WHERE per.RUT='" + rut + "';";
                DataSet dataTable = this.utilMethods.listarObjetoMultiTabla(query);
                if (dataTable.Tables[0].Rows.Count == 0)
                {
                    return null;
                }
                List<Usuario> listUsuario = new List<Usuario>();
                foreach (DataRow row in dataTable.Tables[0].Rows)
                {
                    listUsuario.Add(generarObjetoUsuario(row));
                }
                return listUsuario;
            }
            catch (Exception ex)
            {
                verErrorEnConsola(ex, "generarListaUsuarios");
                throw new Exception("Ocurrio un error al generar la lista de usuarios ", new Exception());
            }
        }
        //Valida si las Horas de atencion se encuentran Reservadas y con que estado
        private bool validarReserva(HoraAtencion h, List<Reserva> listReserva)
        {
            List<Reserva> listMismoId = listReserva.FindAll(r => r.IdHora.IdHora == h.IdHora);
            if (listMismoId == null || listMismoId.Count==0)
            {
                return true;
            }
            int maxId = listMismoId.Max(r => r.IdReserva);
            int idEstado = listMismoId.SingleOrDefault(r => r.IdReserva == maxId).IdEstado.IdEstado;
            return idEstado == 3;
        }
        //Busca la data de las Reservas y Estados
        private List<Reserva> buscarReservaAndEstado()
        {
            try
            {
                String query = "SELECT res.ID_RESERVA,res.ID_HORA,est.ID_ESTADO,est.DESCRIPCION"
                + " FROM BIOCENTRO_DB.dbo.RESERVA as res JOIN BIOCENTRO_DB.dbo.ESTADO_RESERVA as est ON res.ID_ESTADO = est.ID_ESTADO;";
                DataSet dataTable = this.utilMethods.listarObjetoMultiTabla(query);
                if (dataTable.Tables[0].Rows.Count == 0)
                {
                    return null;
                }
                List<Reserva> listaReserva = new List<Reserva>();
                foreach (DataRow row in dataTable.Tables[0].Rows)
                {
                    listaReserva.Add(generarObjetoReserva(row,false));
                }
                return listaReserva;
            }
            catch (Exception ex)
            {
                verErrorEnConsola(ex, "buscarReservaAndEstado");
                throw new Exception("Ocurrio un error al buscar reservas y estado ", new Exception());
            }
        }
        //Retorna el listado de las horas, los bloques, los especialistas y persona, sin parametros
        // O Filtro por id de la HoraAtencion
        private List<HoraAtencion> buscarInfoHorasEspecialistas(List<int> listIdHoraAtencion)
        {
            try
            {
                //Query retorna el listado de las horas, los bloques, los especialistas y persona
                String query = "SELECT usu.ID_USUARIO,usu.ID_ROL,"
                + " per.ID_PERSONA,per.RUT,per.NOMBRE,per.APELLIDO_PATERNO,per.APELLIDO_MATERNO,"
                + " esp.NOMBRE as nombreEspecialidad, esp.ID_ESPECIALIDAD," 
                + " hAtencion.ID_HORA,hAtencion.FECHA,"
                + " bloq.HORA_INICIO,bloq.HORA_FIN"
                + " FROM BIOCENTRO_DB.dbo.USUARIO as usu " 
                + " JOIN BIOCENTRO_DB.dbo.PERSONA as per on usu.ID_PERSONA = per.ID_PERSONA"
                + " JOIN BIOCENTRO_DB.dbo.TERAPEUTA as tera ON tera.ID_USUARIO = usu.ID_USUARIO"
                + " JOIN BIOCENTRO_DB.dbo.ESPECIALIDAD as esp ON esp.ID_ESPECIALIDAD = tera.ID_ESPECIALIDAD"
                + " JOIN BIOCENTRO_DB.dbo.HORA_ATENCION as hAtencion ON hAtencion.ID_TERAPEUTA = tera.ID_TERAPEUTA"
                + " JOIN BIOCENTRO_DB.dbo.BLOQUE as bloq ON bloq.ID_BLOQUE = hAtencion.ID_BLOQUE  ";
                if (listIdHoraAtencion!= null && listIdHoraAtencion.Count!=0)
                {
                    String ids = listIdHoraAtencion.Aggregate(new StringBuilder(), (str, next) => str.Append(str.Length == 0 ? "" : ",").Append(next)).ToString();
                    query = query + " WHERE hAtencion.ID_HORA IN ("+ ids + ") AND usu.ESTADO = 1 ; ";
                }
                else
                {
                    query = query + " WHERE usu.ESTADO = 1";
                }
                DataSet dataTable = this.utilMethods.listarObjetoMultiTabla(query);
                if (dataTable.Tables[0].Rows.Count == 0)
                {
                    return null;
                }
                List<HoraAtencion> listaHoras = new List<HoraAtencion>();
                foreach (DataRow row in dataTable.Tables[0].Rows)
                {
                    listaHoras.Add(generarObjetoHoraAtencion(row));
                }
                return listaHoras;
            }
            catch (Exception ex)
            {
                verErrorEnConsola(ex, "buscarInfoHorasEspecialistas");
                throw new Exception("Ocurrio un error al buscar las horas de los especialistas ", new Exception());
            }
        }
        //Crea un objeto de tipo HoraAtencion
        private HoraAtencion generarObjetoHoraAtencion(DataRow row)
        {
            HoraAtencion horaAtencion = new HoraAtencion();
            horaAtencion.IdHora = Convert.ToInt32(row["ID_HORA"]);
            horaAtencion.IdBloque = generarObjetoBloque(row);
            horaAtencion.IdTerapeuta = generarObjetoTerapeuta(row);
            horaAtencion.Fecha = Convert.ToDateTime(row["FECHA"]);
            return horaAtencion;
        }
        //Crea un objeto de tipo Reserva
        private Reserva generarObjetoReserva(DataRow row,Boolean setUsuario)
        {
            Reserva reserva = new Reserva();
            reserva.IdReserva = Convert.ToInt32(row["ID_RESERVA"]);
            HoraAtencion horaAtencion = new HoraAtencion();
            horaAtencion.IdHora = Convert.ToInt32(row["ID_HORA"]);
            if (row.Table.Columns.Contains("FECHA"))
            {
                horaAtencion.Fecha = Convert.ToDateTime(row["FECHA"]);
            }            
            reserva.IdHora = horaAtencion;
            reserva.IdEstado = generarObjetoEstadoReserva(row);
            if (setUsuario)
            {
                reserva.IdPaciente = generarObjetoUsuario(row);
            }
            return reserva;
        }
        //Crea un objeto de tipo EstadoReserva
        private EstadoReserva generarObjetoEstadoReserva(DataRow row)
        {
            EstadoReserva estado = new EstadoReserva();
            estado.IdEstado = Convert.ToInt32(row["ID_ESTADO"]);
            estado.Descripcion = Convert.ToString(row["DESCRIPCION"]);
            return estado;
        }
        //Crea un objeto de tipo Bloque
        private Bloque generarObjetoBloque(DataRow row)
        {
            Bloque bloque = new Bloque();
            bloque.HoraInicio = Convert.ToInt32(row["HORA_INICIO"]);
            bloque.HoraFin = Convert.ToInt32(row["HORA_FIN"]);
            return bloque;
        }
        //Crea un objeto de tipo Terapeuta
        private Terapeuta generarObjetoTerapeuta(DataRow row)
        {
            Terapeuta terapeuta = new Terapeuta();
            terapeuta.IdUsuario = generarObjetoUsuario(row);
            terapeuta.IdEspecialidad = generarObjetEspecialidad(row);
            return terapeuta;
        }
        //Crea un objeto de tipo Usuario
        private Usuario generarObjetoUsuario(DataRow row)
        {
            Usuario usuario = new Usuario();
            usuario.IdUsuario = Convert.ToInt32(row["ID_USUARIO"]);
            usuario.IdPersona = generarObjetoPersona(row);
            usuario.IdRol = generarObjetoRolUsuario(row);
            return usuario;
        }
        //Crea un objeto de tipo RolUsuario
        private RolUsuario generarObjetoRolUsuario(DataRow row)
        {
            RolUsuario rolUsuario = new RolUsuario();
            rolUsuario.IdRol = Convert.ToInt32(row["ID_ROL"]);
            return rolUsuario;
        }
        //Crea un objeto de tipo Persona
        private Persona generarObjetoPersona(DataRow row)
        {
            Persona persona = new Persona();
            persona.IdPersona = Convert.ToInt32(row["ID_PERSONA"]);
            persona.Rut = Convert.ToString(row["RUT"]);
            persona.Nombre = Convert.ToString(row["NOMBRE"]);
            persona.ApellidoPaterno = Convert.ToString(row["APELLIDO_PATERNO"]);
            persona.ApellidoMaterno = Convert.ToString(row["APELLIDO_MATERNO"]);
            return persona;
        }
        //Crea un objeto de tipo Especialidad
        private Especialidad generarObjetEspecialidad(DataRow row)
        {
            Especialidad especialidad = new Especialidad();
            especialidad.IdEspecialidad = Convert.ToInt32(row["ID_ESPECIALIDAD"]);
            especialidad.Nombre = Convert.ToString(row["nombreEspecialidad"]);
            return especialidad;
        }
        //Borra un objeto de una tabla x
        private void eliminar(String tabla, String columnaid, int? id)
        {
            if (id != null)
            {
                String query = "DELETE FROM BIOCENTRO_DB.dbo." + tabla + " WHERE " + columnaid + " = " + id + ";";
                this.utilMethods.guardarEliminarActualizarObjeto(query, false);
            }
        }
        //Actualiza un objeto de una tabla x
        private void actualizar(String tabla, String columnaSet, String valorSet, String columnaWhere,  String valorWhere)
        {
            String query = "UPDATE BIOCENTRO_DB.dbo." + tabla
                + " SET "+ columnaSet + " = " + valorSet
                + " WHERE " + columnaWhere + " = " + valorWhere + ";";
            this.utilMethods.guardarEliminarActualizarObjeto(query, false);
        }
        //Retorna las exepciones por consola
        private void verErrorEnConsola(Exception exception,String metodo)
        {
            if (exception.InnerException != null && !String.IsNullOrEmpty(exception.InnerException.Message))
            {
                Console.WriteLine("***Se produjo un error en el metodo " + metodo + "=>" + exception.Message + exception.InnerException.Message);
            }
            else
            {
                Console.WriteLine("***Se produjo un error en el metodo " + metodo + "=>" + exception.Message);
            }
        }
    }
}
