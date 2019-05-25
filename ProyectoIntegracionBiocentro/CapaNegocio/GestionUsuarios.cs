using CapaDTO;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CapaNegocio
{
    public class GestionUsuarios
    {

        private UtilMethods utilMethods;

        public GestionUsuarios()
        {
            this.utilMethods = new UtilMethods();
        }

        public Boolean guardarCliente(Persona persona)
        {
            try
            {
                int id = 0;
                String query = "";
                query = "INSERT INTO BIOCENTRO_DB.dbo.PERSONA (RUT,NOMBRE,APELLIDO_PATERNO,APELLIDO_MATERNO) OUTPUT INSERTED.ID_PERSONA VALUES " +
                " ('" + persona.Rut + "','" + persona.Nombre + "','" + persona.ApellidoPaterno + "','" + persona.ApellidoMaterno + "');";
                int idPersona = this.utilMethods.guardarObjeto(query, true);
                query = "INSERT INTO BIOCENTRO_DB.dbo.USUARIO (ESTADO,ID_ROL,ID_PERSONA) OUTPUT INSERTED.ID_USUARIO VALUES(1, 4, "+idPersona+"); ";
                id = this.utilMethods.guardarObjeto(query, true);
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine("Se produjo un error en el metodo buscarTerapeutas ", ex);
                return false;
            }
        }


        public void listar()
        {

            String query = "SELECT usu.ID_USUARIO,"
                +" per.ID_PERSONA,per.RUT,per.NOMBRE,per.APELLIDO_PATERNO,per.APELLIDO_MATERNO,"
                +" esp.NOMBRE as nombreEspecialidad,hAtencion.ID_HORA,bloq.HORA_INICIO,bloq.HORA_FIN"
                +" FROM BIOCENTRO_DB.dbo.USUARIO as usu JOIN BIOCENTRO_DB.dbo.PERSONA as per on usu.ID_PERSONA = per.ID_PERSONA"
                +" JOIN BIOCENTRO_DB.dbo.TERAPEUTA as tera ON tera.ID_USUARIO = usu.ID_USUARIO"
                +" JOIN BIOCENTRO_DB.dbo.ESPECIALIDAD as esp ON esp.ID_ESPECIALIDAD = tera.ID_ESPECIALIDAD"
                +" JOIN BIOCENTRO_DB.dbo.HORA_ATENCION as hAtencion ON hAtencion.ID_TERAPEUTA = tera.ID_TERAPEUTA"
                +" JOIN BIOCENTRO_DB.dbo.BLOQUE as bloq ON bloq.ID_BLOQUE = hAtencion.ID_BLOQUE; ";
            DataSet dataTable = this.utilMethods.listarObjectMultiTabla(query);
            foreach (DataRow row in dataTable.Tables[0].Rows)
            {
                Console.WriteLine(Convert.ToString(row["ID_USUARIO"]) + " - " + Convert.ToString(row["ID_PERSONA"]) + " - " +
                    Convert.ToString(row["RUT"]) + " - " + Convert.ToString(row["NOMBRE"]) + " - " +
                    Convert.ToString(row["APELLIDO_PATERNO"]) + " - " + Convert.ToString(row["APELLIDO_MATERNO"]) + " - "
                    + Convert.ToString(row["nombreEspecialidad"]) + " - " + Convert.ToString(row["ID_HORA"]) + " - "
                    + Convert.ToString(row["HORA_INICIO"]) + " - " + Convert.ToString(row["HORA_FIN"]));
            }
            //retornarListaRolUsuario(this.conexion.DbDataSet);
        }

        public List<HoraAtencion> buscarHorarioAtencion(int id, DateTime fecha)
        {
            try
            {
                String query = "";
                if(fecha!=null)
                {
                    query = "SELECT * FROM BIOCENTRO_DB.dbo.HORA_ATENCION WHERE FECHA BETWEEN '10-05-2019 15:25:06.000' and '21-06-2019 15:25:06.000'";
                }
                else
                {
                    query = "SELECT * FROM BIOCENTRO_DB.dbo.HORA_ATENCION";
                }
                DataSet dataBd = this.utilMethods.listarObjectConTablaEspecifica(query, "HORA_ATENCION");
                return generarListaHorarioAtencion(id, dataBd);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Se produjo un error en el metodo buscarTerapeutas ", ex);
            }
            return null;
        }

        private List<HoraAtencion> generarListaHorarioAtencion(int id, DataSet dataHoraAtencion)
        {
            if (dataHoraAtencion==null)
            {
                return null;
            }
            List<Reserva> listaReservas = generarListaReserva();
            List<Bloque> listaBloque = generarListaBloque();
            List<HoraAtencion> list = new List<HoraAtencion>();
            foreach (DataRow row in dataHoraAtencion.Tables[0].Rows)
            {
                if (atencionDisponible(listaReservas, row))
                {
                    list.Add(generarObjectHoraAtencion(row, id, listaBloque));
                }
            }
            return list;
        }

        private bool atencionDisponible(List<Reserva> listaReservas, DataRow rowHoraAtencion)
        {
            int id = Convert.ToInt32(rowHoraAtencion["ID_HORA"]);
            List<Reserva> listMismoId = listaReservas.FindAll(r => r.IdHora.IdHora == id);
            if (listMismoId==null)
            {
                return true;
            }
            int maxId = listMismoId.Max(r => r.IdReserva);
            int idEstado = listMismoId.SingleOrDefault(r => r.IdReserva == maxId).IdEstado.IdEstado;
            return idEstado==3;
        }

        private List<Reserva> generarListaReserva()
        {
            try
            {
                List<EstadoReserva> listaEstados = generarListaEstadosReserva();
                String query = "SELECT * FROM BIOCENTRO_DB.dbo.RESERVA";
                DataSet dataBd = this.utilMethods.listarObjectConTablaEspecifica(query, "RESERVA");
                List<Reserva> list = new List<Reserva>();
                foreach (DataRow row in dataBd.Tables[0].Rows)
                {
                    list.Add(generarObjectReserva(row, listaEstados));
                }
                return list;
            }
            catch (Exception ex)
            {
                Console.WriteLine("Se produjo un error en el metodo buscarTerapeutas ", ex);
            }
            return null;
        }

        private List<EstadoReserva> generarListaEstadosReserva()
        {
            try
            {
                String query = "SELECT * FROM BIOCENTRO_DB.dbo.ESTADO_RESERVA";
                DataSet dataBd = this.utilMethods.listarObjectConTablaEspecifica(query, "ESTADO_RESERVA");
                List<EstadoReserva> list = new List<EstadoReserva>();
                foreach (DataRow row in dataBd.Tables[0].Rows)
                {
                    list.Add(generarObjectEstadoReserva(row));
                }
                return list;
            }
            catch (Exception ex)
            {
                Console.WriteLine("Se produjo un error en el metodo buscarTerapeutas ", ex);
            }
            return null;
        }

        private EstadoReserva generarObjectEstadoReserva(DataRow row)
        {
            EstadoReserva estado = new EstadoReserva();
            estado.IdEstado = Convert.ToInt32(row["ID_ESTADO"]);
            estado.Descripcion = Convert.ToString(row["DESCRIPCION"]);
            return estado;
        }

        private Reserva generarObjectReserva(DataRow row,List<EstadoReserva> listaEstadoReserva)
        {
            Reserva reserva = new Reserva();
            reserva.IdReserva = Convert.ToInt32(row["ID_RESERVA"]);
            reserva.IdPaciente = generarObjetUsuarioPorId(Convert.ToInt32(row["ID_PACIENTE"]));
            HoraAtencion horaAtencion = new HoraAtencion();
            horaAtencion.IdHora = Convert.ToInt32(row["ID_HORA"]);
            reserva.IdHora = horaAtencion;
            reserva.IdEstado = buscarEstadoReserva(listaEstadoReserva, Convert.ToInt32(row["ID_ESTADO"]));
            return reserva;
        }

        private EstadoReserva buscarEstadoReserva(List<EstadoReserva> listaEstadoReserva, int id)
        {
            return listaEstadoReserva.SingleOrDefault(e => e.IdEstado == id);
        }

        private List<Bloque> generarListaBloque()
        {
            try
            {
                String query = "SELECT * FROM BIOCENTRO_DB.dbo.BLOQUE";
                DataSet dataBd = this.utilMethods.listarObjectConTablaEspecifica(query, "BLOQUE");
                List<Bloque> list = new List<Bloque>();
                foreach (DataRow row in dataBd.Tables[0].Rows)
                {
                    list.Add(generarObjectBloque(row));
                }
                return list;
            }
            catch (Exception ex)
            {
                Console.WriteLine("Se produjo un error en el metodo buscarTerapeutas ", ex);
            }
            return null;
        }

        private Bloque generarObjectBloque(DataRow row)
        {
            Bloque bloque = new Bloque();
            bloque.IdBloque = Convert.ToInt32(row["ID_BLOQUE"]);
            bloque.HoraInicio = Convert.ToInt32(row["HORA_INICIO"]);
            bloque.HoraFin = Convert.ToInt32(row["HORA_FIN"]);
            return bloque;
        }

        private HoraAtencion generarObjectHoraAtencion(DataRow row, int id, List<Bloque> listaBloque)
        {
            List<Terapeuta> listaTerapeutas = buscarTerapeutas(Convert.ToInt32(row["ID_TERAPEUTA"]),new DateTime());
            if (listaTerapeutas==null || listaTerapeutas.Count<=0)
            {
                return null;
            }
            HoraAtencion horaAtencion = new HoraAtencion();
            horaAtencion.IdHora = Convert.ToInt32(row["ID_HORA"]);
            horaAtencion.IdBloque = buscarEnListaBloque(Convert.ToInt32(row["ID_BLOQUE"]),listaBloque);
            //horaAtencion.ListTerapeuta = listaTerapeutas;
            horaAtencion.Fecha = Convert.ToDateTime(row["FECHA"]);
            return horaAtencion;
        }

        private Bloque buscarEnListaBloque(int id, List<Bloque> listaBloque)
        {
            return listaBloque.SingleOrDefault(b => b.IdBloque == id);
        }

        public List<Terapeuta> buscarTerapeutas(int id,DateTime fecha)
        {
            try
            {
                String queryTerapeuta = "SELECT ID_TERAPEUTA,ID_USUARIO,ID_ESPECIALIDAD FROM BIOCENTRO_DB.dbo.TERAPEUTA WHERE ID_ESPECIALIDAD = "+id;
                DataSet dataBdTerapeuta = this.utilMethods.listarObjectConTablaEspecifica(queryTerapeuta, "TERAPEUTA");
                return generarListTerapeuta(dataBdTerapeuta);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Se produjo un error en el metodo buscarTerapeutas ", ex);
            }
            return null;
        }

        public List<Especialidad> generarListaEspecialidad()
        {
            try
            {
                String queryTerapeuta = "SELECT ID_ESPECIALIDAD,NOMBRE FROM BIOCENTRO_DB.dbo.ESPECIALIDAD";
                DataSet dataBdTerapeuta = this.utilMethods.listarObjectConTablaEspecifica(queryTerapeuta, "ESPECIALIDAD");
                List<Especialidad> list = new List<Especialidad>();
                foreach (DataRow row in dataBdTerapeuta.Tables[0].Rows)
                {
                    list.Add(generarObjetEspecialidad(row));
                }
                return list;
            }
            catch (Exception ex)
            {
                Console.WriteLine("Se produjo un error en el metodo buscarTerapeutas ", ex);
            }
            return null;
        }
        public List<RolUsuario> generarListaRoles()
        {
            try
            {
                String query = "SELECT * FROM BIOCENTRO_DB.dbo.ROL_USUARIO";
                DataSet data = this.utilMethods.listarObjectConTablaEspecifica(query, "ROL_USUARIO");
                List<RolUsuario> list = new List<RolUsuario>();
                foreach (DataRow row in data.Tables[0].Rows)
                {
                    list.Add(generarObjectRolUsuario(row));
                }
                return list;
            }
            catch (Exception ex)
            {
                Console.WriteLine("Se produjo un error en el metodo generarListaRoles ", ex);
            }
            return null;
        }

        private List<Terapeuta> generarListTerapeuta(DataSet dataBdTerapeuta)
        {
            List<Terapeuta> list = new List<Terapeuta>();
            foreach (DataRow row in dataBdTerapeuta.Tables[0].Rows)
            {
                list.Add(generarObjectTerapeuta(row));
            }
            return list;
        }

        private Terapeuta generarObjectTerapeuta(DataRow row)
        {
            Terapeuta terapeuta = new Terapeuta();
            terapeuta.IdUsuario = generarObjetUsuarioPorId(Convert.ToInt32(row["ID_USUARIO"]));
            terapeuta.IdEspecialidad = generarObjetEspecialidadPorId(Convert.ToInt32(row["ID_ESPECIALIDAD"]));
            return terapeuta;
        }

        private Usuario generarObjetUsuarioPorId(int v)
        {
            try
            {
                String queryUsuario = "SELECT ID_USUARIO,ID_ROL,ID_PERSONA,ESTADO FROM BIOCENTRO_DB.dbo.USUARIO WHERE ID_USUARIO = "+v;
                DataSet dataBdUsuario = this.utilMethods.listarObjectConTablaEspecifica(queryUsuario, "USUARIO");
                if (dataBdUsuario != null)
                {
                    DataRow row = dataBdUsuario.Tables[0].Rows[0];
                    Usuario usuario = new Usuario();
                    usuario.IdUsuario = Convert.ToInt32(row["ID_USUARIO"]);
                    usuario.IdRol = null;
                    usuario.IdPersona = generarObjetPersona(Convert.ToInt32(row["ID_PERSONA"]));
                    usuario.Estado = Convert.ToInt32(row["ESTADO"]);
                    return usuario;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Se produjo un error en el metodo generarObjetUsuario ", ex);
            }
            return null;
        }

        private Persona generarObjetPersona(int v)
        {
            try
            {
                String queryUsuario = "SELECT ID_PERSONA,RUT,NOMBRE,APELLIDO_PATERNO,APELLIDO_MATERNO FROM BIOCENTRO_DB.dbo.PERSONA WHERE ID_PERSONA = " + v;
                DataSet dataBdUsuario = this.utilMethods.listarObjectConTablaEspecifica(queryUsuario, "PERSONA");
                if (dataBdUsuario != null)
                {
                    DataRow row = dataBdUsuario.Tables[0].Rows[0];
                    Persona persona = new Persona();
                    persona.IdPersona = Convert.ToInt32(row["ID_PERSONA"]);
                    persona.Rut = Convert.ToString(row["RUT"]);
                    persona.Nombre = Convert.ToString(row["NOMBRE"]);
                    persona.ApellidoPaterno = Convert.ToString(row["APELLIDO_PATERNO"]);
                    persona.ApellidoMaterno = Convert.ToString(row["APELLIDO_MATERNO"]);
                    return persona;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Se produjo un error en el metodo generarObjetPersona ", ex);
            }
            return null;
        }

        private Especialidad generarObjetEspecialidadPorId(int v)
        {
            try
            {
                String queryEspecialidad = "SELECT ID_ESPECIALIDAD,NOMBRE FROM BIOCENTRO_DB.dbo.ESPECIALIDAD WHERE ID_ESPECIALIDAD = " + v;
                DataSet dataBdEspecialidad = this.utilMethods.listarObjectConTablaEspecifica(queryEspecialidad, "ESPECIALIDAD");
                if (dataBdEspecialidad != null)
                {
                    return generarObjetEspecialidad(dataBdEspecialidad.Tables[0].Rows[0]);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Se produjo un error en el metodo generarObjetEspecialidad ", ex);
            }
            return null;
        }

        private Especialidad generarObjetEspecialidad(DataRow row)
        {
            Especialidad especialidad = new Especialidad();
            especialidad.IdEspecialidad = Convert.ToInt32(row["ID_ESPECIALIDAD"]);
            especialidad.Nombre = Convert.ToString(row["NOMBRE"]);
            return especialidad;
        }
        
        private RolUsuario generarObjectRolUsuario(DataRow row)
        {
            RolUsuario rolUsuario = new RolUsuario();
            rolUsuario.IdRol = Convert.ToInt32(row["ID_ROL"]);
            rolUsuario.Nombre = Convert.ToString(row["NOMBRE"]);
            return rolUsuario;
        }
    }
}
