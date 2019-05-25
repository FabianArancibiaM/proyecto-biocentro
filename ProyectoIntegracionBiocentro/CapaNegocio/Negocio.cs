using CapaDTO;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CapaNegocio
{
    public class Negocio
    {
        private UtilMethods utilMethods;

        public Negocio()
        {
            this.utilMethods = new UtilMethods();
        }

        public List<HoraAtencion> buscarHorasDisponibles(Especialidad especialidad, DateTime? fecha, Persona persona)
        {
            try
            {
                List<Reserva> listReserva = buscarReservaAndEstado();
                if (listReserva == null)
                {
                    return null;
                }
                List<HoraAtencion> listHoraAtencion = buscarInfoHorasEspecialistas();
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
                Console.Write("Se produjo un error en clase Negocio Method buscarHorasDisponibles() ", ex.Message);
                return null;
            }
        }
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
        public List<Especialidad> generarListaEspecialidad()
        {
            try
            {
                String queryTerapeuta = "SELECT ID_ESPECIALIDAD,NOMBRE as nombreEspecialidad FROM BIOCENTRO_DB.dbo.ESPECIALIDAD";
                DataSet dataBdTerapeuta = this.utilMethods.listarObjectConTablaEspecifica(queryTerapeuta, "ESPECIALIDAD");
                if (dataBdTerapeuta == null)
                {
                    return null;
                }
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
        public List<Terapeuta> generarListaEspecialista()
        {
            try
            {
                String query = "SELECT usu.ID_USUARIO,"
                + " per.ID_PERSONA,per.RUT,per.NOMBRE,per.APELLIDO_PATERNO,per.APELLIDO_MATERNO,"
                + " esp.NOMBRE as nombreEspecialidad, esp.ID_ESPECIALIDAD"
                + " FROM BIOCENTRO_DB.dbo.USUARIO as usu JOIN BIOCENTRO_DB.dbo.PERSONA as per on usu.ID_PERSONA = per.ID_PERSONA"
                + " JOIN BIOCENTRO_DB.dbo.TERAPEUTA as tera ON tera.ID_USUARIO = usu.ID_USUARIO"
                + " JOIN BIOCENTRO_DB.dbo.ESPECIALIDAD as esp ON esp.ID_ESPECIALIDAD = tera.ID_ESPECIALIDAD; ";
                DataSet dataTable = this.utilMethods.listarObjectMultiTabla(query);
                if (dataTable == null)
                {
                    return null;
                }
                List<Terapeuta> list = new List<Terapeuta>();
                foreach (DataRow row in dataTable.Tables[0].Rows)
                {
                    Terapeuta terapeuta = generarObjectTerapeuta(row);
                    String nombreCompleto = terapeuta.IdUsuario.IdPersona.Nombre+" "+ terapeuta.IdUsuario.IdPersona.ApellidoPaterno +" "+ terapeuta.IdUsuario.IdPersona.ApellidoMaterno;
                    terapeuta.IdUsuario.IdPersona.Nombre = nombreCompleto;
                    list.Add(terapeuta);
                }
                return list;
            }
            catch (Exception ex)
            {
                Console.WriteLine("Se produjo un error en el metodo buscarTerapeutas ", ex);
            }
            return null;
        }
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
        private List<Reserva> buscarReservaAndEstado()
        {
            try
            {
                String query = "SELECT res.ID_RESERVA,res.ID_HORA,est.ID_ESTADO,est.DESCRIPCION"
                + " FROM BIOCENTRO_DB.dbo.RESERVA as res JOIN BIOCENTRO_DB.dbo.ESTADO_RESERVA as est ON res.ID_ESTADO = est.ID_ESTADO;";
                DataSet dataTable = this.utilMethods.listarObjectMultiTabla(query);
                if (dataTable == null)
                {
                    return null;
                }
                List<Reserva> listaReserva = new List<Reserva>();
                foreach (DataRow row in dataTable.Tables[0].Rows)
                {
                    listaReserva.Add(generarObjectReserva(row));
                }
                return listaReserva;
            }
            catch (Exception ex)
            {
                Console.WriteLine("Se produjo un error en clase Negocio Method buscarReservaAndEstado() ", ex);
                return null;
            }
        }
        private Reserva generarObjectReserva(DataRow row)
        {
            Reserva reserva = new Reserva();
            reserva.IdReserva = Convert.ToInt32(row["ID_RESERVA"]);
            HoraAtencion horaAtencion = new HoraAtencion();
            horaAtencion.IdHora = Convert.ToInt32(row["ID_HORA"]);
            reserva.IdHora = horaAtencion;
            reserva.IdEstado = generarObjectEstadoReserva(row);
            return reserva;
        }
        private EstadoReserva generarObjectEstadoReserva(DataRow row)
        {
            EstadoReserva estado = new EstadoReserva();
            estado.IdEstado = Convert.ToInt32(row["ID_ESTADO"]);
            estado.Descripcion = Convert.ToString(row["DESCRIPCION"]);
            return estado;
        }
        private List<HoraAtencion> buscarInfoHorasEspecialistas()
        {
            try
            {
                String query = "SELECT usu.ID_USUARIO,"
                + " per.ID_PERSONA,per.RUT,per.NOMBRE,per.APELLIDO_PATERNO,per.APELLIDO_MATERNO,"
                + " esp.NOMBRE as nombreEspecialidad, esp.ID_ESPECIALIDAD,hAtencion.ID_HORA,hAtencion.FECHA,bloq.HORA_INICIO,bloq.HORA_FIN"
                + " FROM BIOCENTRO_DB.dbo.USUARIO as usu JOIN BIOCENTRO_DB.dbo.PERSONA as per on usu.ID_PERSONA = per.ID_PERSONA"
                + " JOIN BIOCENTRO_DB.dbo.TERAPEUTA as tera ON tera.ID_USUARIO = usu.ID_USUARIO"
                + " JOIN BIOCENTRO_DB.dbo.ESPECIALIDAD as esp ON esp.ID_ESPECIALIDAD = tera.ID_ESPECIALIDAD"
                + " JOIN BIOCENTRO_DB.dbo.HORA_ATENCION as hAtencion ON hAtencion.ID_TERAPEUTA = tera.ID_TERAPEUTA"
                + " JOIN BIOCENTRO_DB.dbo.BLOQUE as bloq ON bloq.ID_BLOQUE = hAtencion.ID_BLOQUE; ";
                DataSet dataTable = this.utilMethods.listarObjectMultiTabla(query);
                if (dataTable == null)
                {
                    return null;
                }
                List<HoraAtencion> listaHoras = new List<HoraAtencion>();
                foreach (DataRow row in dataTable.Tables[0].Rows)
                {
                    listaHoras.Add(generarObjectHoraAtencion(row));
                }
                return listaHoras;
            }
            catch (Exception ex)
            {
                Console.WriteLine("Se produjo un error en clase Negocio Method buscarInfoHorasEspecialistas() ",ex);
                return null;
            }
        }
        private HoraAtencion generarObjectHoraAtencion(DataRow row)
        {
            HoraAtencion horaAtencion = new HoraAtencion();
            horaAtencion.IdHora = Convert.ToInt32(row["ID_HORA"]);
            horaAtencion.IdBloque = generarObjectBloque(row);
            horaAtencion.IdTerapeuta = generarObjectTerapeuta(row);
            horaAtencion.Fecha = Convert.ToDateTime(row["FECHA"]);
            return horaAtencion;
        }
        private Bloque generarObjectBloque(DataRow row)
        {
            Bloque bloque = new Bloque();
            bloque.HoraInicio = Convert.ToInt32(row["HORA_INICIO"]);
            bloque.HoraFin = Convert.ToInt32(row["HORA_FIN"]);
            return bloque;
        }
        private Terapeuta generarObjectTerapeuta(DataRow row)
        {
            Terapeuta terapeuta = new Terapeuta();
            terapeuta.IdUsuario = generarObjectUsuario(row);
            terapeuta.IdEspecialidad = generarObjetEspecialidad(row);
            return terapeuta;
        }
        private Usuario generarObjectUsuario(DataRow row)
        {
            Usuario usuario = new Usuario();
            usuario.IdUsuario = Convert.ToInt32(row["ID_USUARIO"]);
            usuario.IdPersona = generarObjectPersona(row);
            return usuario;
        }
        private Persona generarObjectPersona(DataRow row)
        {
            Persona persona = new Persona();
            persona.IdPersona = Convert.ToInt32(row["ID_PERSONA"]);
            persona.Rut = Convert.ToString(row["RUT"]);
            persona.Nombre = Convert.ToString(row["NOMBRE"]);
            persona.ApellidoPaterno = Convert.ToString(row["APELLIDO_PATERNO"]);
            persona.ApellidoMaterno = Convert.ToString(row["APELLIDO_MATERNO"]);
            return persona;
        }
        private Especialidad generarObjetEspecialidad(DataRow row)
        {
            Especialidad especialidad = new Especialidad();
            especialidad.IdEspecialidad = Convert.ToInt32(row["ID_ESPECIALIDAD"]);
            especialidad.Nombre = Convert.ToString(row["nombreEspecialidad"]);
            return especialidad;
        }
    }
}
