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
        public StatusResponce registrarPaciente(Paciente paciente,int idHoraAtencion)
        {
            int? idPersona = null;
            Boolean esNuevo = false;
            if (validarSiRecervaFueTomada(idHoraAtencion))
            {
                return generarObjetoStatusResponce("error", "Esta reserva ya esta tomada, favor seleccionar otra");
            }
            try
            {
                String query = "";
                Paciente per = buscarPersonaPorRut(paciente.Rut);
                if (per == null)
                {
                    query = "INSERT INTO BIOCENTRO_DB.dbo.PACIENTE (RUT,NOMBRE,APELLIDO_PATERNO,APELLIDO_MATERNO,TELEFONO,FECHA_NACIMIENTO,SEXO,CORREO) OUTPUT INSERTED.ID_PACIENTE VALUES " +
                            " ('" + paciente.Rut + "','" + paciente.Nombre + "','" + paciente.ApellidoPaterno + "','" + paciente.ApellidoMaterno + "',"
                            + paciente.Telefono + ",'"+ paciente.FechaNacimiento.ToString("yyyy-MM-dd HH:mm:ss") +"','"+ paciente.Sexo+"','"+ paciente.Correo+"');";
                    idPersona =  this.utilMethods.guardarEliminarActualizarObjeto(query, true);
                    esNuevo = true;
                }
                else
                {
                    query = "UPDATE BIOCENTRO_DB.dbo.PACIENTE  SET RUT = '" + paciente.Rut + "' ,NOMBRE = '" + paciente.Nombre + "' ,APELLIDO_PATERNO = '" + paciente.ApellidoPaterno + "' " +
                        ",APELLIDO_MATERNO= '" + paciente.ApellidoMaterno + "',TELEFONO=" + paciente.Telefono + " ,FECHA_NACIMIENTO= '" + paciente.FechaNacimiento.ToString("yyyy-MM-dd HH:mm:ss") + "',SEXO='" + paciente.Sexo + "' ,CORREO='" + paciente.Correo + "'  WHERE ID_PACIENTE= " + per.IdPaciente+";";
                    this.utilMethods.guardarEliminarActualizarObjeto(query, false);
                    idPersona = per.IdPaciente;
                }
                query = "UPDATE BIOCENTRO_DB.dbo.HORA_ATENCION SET ID_ESTADO=1,ID_PACIENTE="+ idPersona+ " WHERE ID_HORA=" + idHoraAtencion + " ; ";
                this.utilMethods.guardarEliminarActualizarObjeto(query, false);
                
                return generarObjetoStatusResponce(String.Empty, "Su reserva se realizó correctamente");
            }
            catch (Exception ex)
            {
                if (esNuevo)
                {
                    eliminar("PACIENTE", "ID_PACIENTE", idPersona);
                }
                verErrorEnConsola(ex, "guardarPaciente");
                return generarObjetoStatusResponce("error", ex.Message);
            }
        }
        //Retorna el listado completo o filtrado de las Horas de atencion disponibles
        public List<HoraAtencion> buscarHorasDisponibles(EspecialidadClinica especialidad, DateTime? fecha, Empleado empleado)
        {
            try
            {
                List<HoraAtencion> listHoraAtencion = buscarInfoHorasEspecialistas(null);
                if (listHoraAtencion == null)
                {
                    return null;
                }
                List<HoraAtencion> listaFinal = listHoraAtencion.FindAll(h => filtrarEspecialidad(h,especialidad) && filtrarFecha(h, fecha) && filtrarTerapeuta(h, empleado));
                return listaFinal;
            }
            catch(Exception ex)
            {
                verErrorEnConsola(ex, "buscarHorasDisponibles");
                return null;
            }
        }
        //Retorna todas las especialidades registradas
        public List<EspecialidadClinica> generarListaEspecialidad()
        {
            try
            {
                String queryTerapeuta = "SELECT ID_ESPECIALIDAD,NOMBRE as nombreEspecialidad ,PRECIO FROM BIOCENTRO_DB.dbo.ESPECIALIDAD_CLINICA;";
                DataSet dataTable = this.utilMethods.listarObjetoConTablaEspecifica(queryTerapeuta, "ESPECIALIDAD_CLINICA");
                if (dataTable.Tables[0].Rows.Count == 0)
                {
                    return null;
                }
                List<EspecialidadClinica> list = new List<EspecialidadClinica>();
                foreach (DataRow row in dataTable.Tables[0].Rows)
                {

                    list.Add(generarObjetEspecialidadClinica(row));
                }
                return list;
            }
            catch (Exception ex)
            {
                verErrorEnConsola(ex, "generarListaEspecialidad");
                return null;
            }
        }
        //Retorna la data de todos los especialistas disponibles
        public List<EspecialidadTerapeuta> generarListaEspecialista()
        {
            try
            {
                //Query retorna los datos de especialistas, roles, persona, usuario
                String query = "SELECT espTera.ID,"
                + " emp.ID_EMPLEADO,emp.NOMBRE,emp.APELLIDO_PATERNO,emp.APELLIDO_MATERNO ,"
                + " espClin.ID_ESPECIALIDAD,espClin.NOMBRE as nombreEspecialidad,espClin.PRECIO"
                + " FROM BIOCENTRO_DB.dbo.ESPECIALIDAD_TERAPEUTA as espTera"
                + " JOIN BIOCENTRO_DB.dbo.EMPLEADO as emp on emp.ID_EMPLEADO = espTera.ID_EMPLEADO"
                + " JOIN BIOCENTRO_DB.dbo.ESPECIALIDAD_CLINICA as espClin on espTera.ID_ESPECIALIDAD=espClin.ID_ESPECIALIDAD ";
                DataSet dataTable = this.utilMethods.listarObjetoMultiTabla(query);
                if (dataTable.Tables[0].Rows.Count == 0)
                {
                    return null;
                }
                List<EspecialidadTerapeuta> list = new List<EspecialidadTerapeuta>();
                foreach (DataRow row in dataTable.Tables[0].Rows)
                {
                    list.Add(generarObjetoEspecialidadTerapeuta(row));
                }
                return list;
            }
            catch (Exception ex)
            {
                verErrorEnConsola(ex, "generarListaEspecialista");
                return null;
            }
        }
        //Retorna un unico Usuario
        public Paciente buscarPaciente(String rut)
        {
            try
            {
                Paciente persona = buscarPersonaPorRut(rut);
                if (persona == null)
                {
                    return null;
                }
                return persona;
            }
            catch (Exception ex)
            {
                verErrorEnConsola(ex, "buscarPaciente");
                return null;
            }
        }
        //Retorna la data de pacientes y reservas
        public List<HoraAtencion> listaReservasPorRut(String rut,String correo)
        {
            try
            {
                //Query entrega los datos del Paciente y la reserva realizada
                String query = "SELECT hora.ID_VENTA,paciente.ID_PACIENTE,paciente.RUT as pRu,paciente.NOMBRE as pNo ,paciente.APELLIDO_PATERNO as pPa, "
                    + " paciente.APELLIDO_MATERNO as pMa,paciente.TELEFONO as pTe ,paciente.FECHA_NACIMIENTO as pFe,paciente.SEXO as pSe,paciente.CORREO as pCo,"
                    + " hora.ID_HORA,hora.FECHA,"
                    + " blo.HORA_FIN,blo.HORA_INICIO,"
                    + "sala.ID_SALA,sala.NOMBRE as sNo,"
                    + "espCli.ID_ESPECIALIDAD,espCli.NOMBRE as nombreEspecialidad, espCli.PRECIO,"
                    + "estRes.ID_ESTADO,estRes.DESCRIPCION,"
                    + "emp.ID_EMPLEADO,emp.NOMBRE,emp.APELLIDO_PATERNO,emp.APELLIDO_MATERNO " +
                " FROM BIOCENTRO_DB.dbo.PACIENTE as paciente "
                    + "JOIN BIOCENTRO_DB.dbo.HORA_ATENCION as hora on hora.ID_PACIENTE=paciente.ID_PACIENTE "
                    + "JOIN BIOCENTRO_DB.dbo.BLOQUE as blo on blo.ID_BLOQUE = hora.ID_BLOQUE "
                    + "JOIN BIOCENTRO_DB.dbo.SALA as sala on sala.ID_SALA = hora.ID_SALA "
                    + "JOIN BIOCENTRO_DB.dbo.ESPECIALIDAD_CLINICA as espCli on espCli.ID_ESPECIALIDAD = hora.ID_ESPECIALIDAD "
                    + "JOIN BIOCENTRO_DB.dbo.ESTADO_RESERVA as estRes on estRes.ID_ESTADO=hora.ID_ESTADO "
                    + "JOIN EMPLEADO as emp on emp.ID_EMPLEADO = hora.ID_TERAPEUTA "
                    + " WHERE paciente.RUT = '" + rut +"' and estRes.ID_ESTADO <> 3 ";
                if (correo!=null)
                {
                    query = query + " and paciente.CORREO = '" + correo + "'";
                }
                query = query + " ;";
                DataSet dataTable = this.utilMethods.listarObjetoMultiTabla(query);
                if (dataTable.Tables[0].Rows.Count == 0)
                {
                    return null;
                }
                List<HoraAtencion> listaReserva = new List<HoraAtencion>();
                foreach (DataRow row in dataTable.Tables[0].Rows)
                {
                    HoraAtencion nuevo = generarObjetoHoraAtencion(row, true, true, true, true, true, false, true);
                    if (validarSiRowTieneCampo(row, "ID_VENTA"))
                    {
                        Venta venta = new Venta();
                        venta.IdVenta = Convert.ToInt32(row["ID_VENTA"]);
                        nuevo.Venta = venta;
                    }
                    listaReserva.Add(nuevo) ;
                }
                List<int> listId = new List<int>();
                List<HoraAtencion> listaReservasValidas = listaReserva.FindAll(r => r.Fecha.CompareTo(DateTime.Today) >= 0);
                return listaReservasValidas;
            }
            catch (Exception ex)
            {
                verErrorEnConsola(ex, "buscarReservasPorRut");
                return null;
            }
        }
        //cambioEstado de la reserva debe ser  1 = Confirmar(id = 2) y 0 = Rechazar(id = 3)
        public StatusResponce rechazarConfirmarReserva(Char cambioEstado,int idReserva)
        {
            try
            {
                if (!Char.IsDigit(cambioEstado) || cambioEstado.CompareTo('1') > 0 || idReserva.CompareTo(null)==0)
                {
                    throw new Exception(" Data ingresada incorrecta ",new Exception());
                }
                String estado = String.Empty;
                String codigo = String.Empty;
                if (cambioEstado.CompareTo('0')==0)
                {
                    codigo = "3";
                    estado = "Anulada";
                }
                else if (cambioEstado.CompareTo('1') == 0)
                {
                    codigo = "2";
                    estado = "Confirmada";
                }
                actualizar("HORA_ATENCION", "ID_ESTADO", codigo, "ID_HORA", Convert.ToString(idReserva));
                return generarObjetoStatusResponce(String.Empty, "La hora fue "+ estado + " con éxito");
            }
            catch (Exception ex)
            {
                verErrorEnConsola(ex, "rechazarConfirmarReserva");
                return generarObjetoStatusResponce("error", ex.Message);
            }
        }
        //Retorna los medios de pago disponibles
        public List<MedioPago> generarListaMedioPago()
        {
            try
            {
                String queryTerapeuta = "SELECT ID_MEDIO_PAGO,NOMBRE FROM BIOCENTRO_DB.dbo.MEDIO_PAGO ;";
                DataSet dataTable = this.utilMethods.listarObjetoConTablaEspecifica(queryTerapeuta, "ESPECIALIDAD_CLINICA");
                if (dataTable.Tables[0].Rows.Count == 0)
                {
                    return null;
                }
                List<MedioPago> list = new List<MedioPago>();
                foreach (DataRow row in dataTable.Tables[0].Rows)
                {

                    list.Add(generarObjetoMedioPago(row));
                }
                return list;
            }
            catch (Exception ex)
            {
                verErrorEnConsola(ex, "generarListaMedioPago");
                return null;
            }
        }
        //Generar Lista HorasPorRutPacienteMasVenta
        public List<HoraAtencion> horasPorRutPacienteMasVenta(String rut)
        {
            try
            {
                List<HoraAtencion> horas = listaReservasPorRut(rut,null);
                List<int> ids = new List<int>();
                horas.ForEach( h => {
                    if (h.Venta!=null)
                    {
                        ids.Add(h.Venta.IdVenta);
                    }
                });
                if (ids.Count==0)
                {
                    return horas;
                }
                String idsIn = ids.Aggregate(new StringBuilder(), (str, next) => str.Append(str.Length == 0 ? "" : ",").Append(next)).ToString();
                String query = "SELECT ven.ID_VENTA,ven.FECHA_PAGO,ven.MONTO, mpag.ID_MEDIO_PAGO,mpag.NOMBRE as mpagNombre," +
                    "estVenta.ID_ESTADO_VENTA,estVenta.DESCRIPCION as estVentaDescripcion " +
                    " FROM BIOCENTRO_DB.dbo.VENTA as ven " +
                    "JOIN BIOCENTRO_DB.dbo.MEDIO_PAGO as mpag on mpag.ID_MEDIO_PAGO = ven.ID_MEDIO_PAGO " +
                    "JOIN BIOCENTRO_DB.dbo.ESTADO_VENTA as estVenta on estVenta.ID_ESTADO_VENTA = ven.ID_ESTADO_VENTA " +
                    "WHERE ven.ID_VENTA in ("+ idsIn + "); ";
                DataSet dataTable = this.utilMethods.listarObjetoMultiTabla(query);
                if (dataTable.Tables[0].Rows.Count == 0)
                {
                    return null;
                }
                List<Venta> listaVenta = new List<Venta>();
                foreach (DataRow row in dataTable.Tables[0].Rows)
                {
                    listaVenta.Add(generarObjetoVenta(row));
                }

                horas.ForEach( h => {
                    h.Venta = h.Venta!=null?listaVenta.Find(v => v.IdVenta == h.Venta.IdVenta):null;
                });
                return horas;
            }
            catch (Exception ex)
            {
                verErrorEnConsola(ex, "HorasPorRutPacienteMasVenta");
                return null;
            }
        }
        //Guarda Venta
        public StatusResponce guardarVentaRealizada(Venta venta,List<int> idHora)
        {
            try
            {
                String ids = idHora.Aggregate(new StringBuilder(), (str, next) => str.Append(str.Length == 0 ? "" : ",").Append(next)).ToString();
                String queryTerapeuta = "SELECT  hora.ID_HORA, (SELECT estVenta.DESCRIPCION  " +
                    " from BIOCENTRO_DB.dbo.VENTA as ven " +
                    "JOIN BIOCENTRO_DB.dbo.ESTADO_VENTA as estVenta on estVenta.ID_ESTADO_VENTA = ven.ID_ESTADO_VENTA " +
                    "WHERE ven.ID_VENTA = hora.ID_VENTA ) as estado from BIOCENTRO_DB.dbo.HORA_ATENCION as hora " +
                    "WHERE hora.ID_HORA in (" + ids + "); ";
                DataSet dataTable = this.utilMethods.listarObjetoMultiTabla(queryTerapeuta);
                if (dataTable.Tables[0].Rows.Count == 0)
                {
                    return generarObjetoStatusResponce("error", "No se encontraron las horas seleccionadas");
                }
                foreach (DataRow row in dataTable.Tables[0].Rows)
                {
                    if (validarSiRowTieneCampo(row, "estado"))
                    {
                        return generarObjetoStatusResponce("error", "Debe seleccionar una venta que este pendiente de pago");
                    }
                }
                String fecha = venta.FechaPago.Year+"-"+ venta.FechaPago.Month+"-"+ venta.FechaPago.Day+" "+ venta.FechaPago.Hour+":"+ venta.FechaPago.Minute+":"+ venta.FechaPago.Second;
                String query = "INSERT INTO BIOCENTRO_DB.dbo.VENTA (ID_ESTADO_VENTA,ID_MEDIO_PAGO,FECHA_PAGO,MONTO) OUTPUT INSERTED.ID_VENTA "
                    + " VALUES("+ venta.EstadoVenta.IdEstadoVenta+"," + venta.MedioPago.IdMedioPago +
                     ",'" + fecha + "',"+ venta.Monto+")";
                int? idVenta = this.utilMethods.guardarEliminarActualizarObjeto(query, true);
                query = "UPDATE BIOCENTRO_DB.dbo.HORA_ATENCION SET ID_ESTADO=2,ID_VENTA=" + idVenta + " WHERE ID_HORA in (" + ids + ") ; ";
                this.utilMethods.guardarEliminarActualizarObjeto(query, false);
                return generarObjetoStatusResponce(String.Empty, "La venta se realizo correctamente");
            }
            catch (Exception ex)
            {
                verErrorEnConsola(ex, "guardarVentaRealizada");
                return generarObjetoStatusResponce("error", ex.Message);
            }
        }
        //Metodo retorna empleado cuando clave ingresada concuerda con la clave desencriptada
        public Empleado login(string usuario,string password)
        {
            try
            {
                String query = " SELECT CONTRASENA,ID_EMPLEADO,ID_CARGO,NOMBRE,TELEFONO,USUARIO,APELLIDO_MATERNO,APELLIDO_PATERNO,CORREO,FECHA_NACIMIENTO" +
                    " from BIOCENTRO_DB.dbo.EMPLEADO WHERE USUARIO = '" + usuario + "' ;";
                DataSet dataTable = this.utilMethods.listarObjetoMultiTabla(query);
                if (dataTable.Tables[0].Rows.Count == 0)
                {
                    return null;
                }
                Empleado empleado = null;
                foreach (DataRow row in dataTable.Tables[0].Rows)
                {
                    empleado = generarObjetoTerapeuta(row);
                    if (validarClave(empleado,password))
                    {
                        empleado.Contraseña = string.Empty;
                        return empleado;
                    }
                }
            }
            catch (Exception ex)
            {
                verErrorEnConsola(ex, "login");
            }
            return null;
        }
        //Metodo guarda empleado con clave encriptada
        public StatusResponce agregarEmpleado(Empleado empleado)
        {
            try
            {
                string segurity = encriptarClave(empleado);
                String query = "INSERT INTO BIOCENTRO_DB.dbo.EMPLEADO "+
                    "(USUARIO, CONTRASENA, NOMBRE, APELLIDO_PATERNO, APELLIDO_MATERNO, TELEFONO, CORREO, ID_CARGO, FECHA_NACIMIENTO)" +
                    " OUTPUT INSERTED.ID_EMPLEADO "
                    + " VALUES('" +empleado.Usuario + "','" + segurity +
                     "','" + empleado.Nombre + "','" + empleado.ApellidoPaterno + "','" + empleado.ApellidoMaterno + "',"+
                     empleado.Telefono + ",'" + empleado.Correo + "'," + empleado.Cargo.IdCargo + ",'" + empleado.FechaNacimiento + "');";
                this.utilMethods.guardarEliminarActualizarObjeto(query, true);
                return generarObjetoStatusResponce(string.Empty,"Se ha registrado correctamente"); 
            }
            catch(Exception ex)
            {
                verErrorEnConsola(ex, "login");
                return generarObjetoStatusResponce("error", "Se produjo un error al registrar el empleado");
            }
        }
        // Fin Metodos Publicos /////////////////////////////////////////////////////////////////////////////////////////////////////
        private string encriptarClave(Empleado em)
        {
            SeguritySystem segurity = new SeguritySystem();
            return segurity.seguridad(em.Contraseña, true);
        }
        private bool validarClave(Empleado em,string claveIngresada)
        {

            SeguritySystem segurity = new SeguritySystem();
            string claveDesen = segurity.seguridad(em.Contraseña, false);
            return claveDesen.Equals(claveIngresada);
        }
        private bool filtrarTerapeuta(HoraAtencion h, Empleado empleado)
        {
            if (empleado == null)
            {
                return true;
            }
            if (h.Terapeuta.IdEmpleado== empleado.IdEmpleado)
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
        private bool filtrarEspecialidad(HoraAtencion h, EspecialidadClinica especialidad)
        {
            if (especialidad==null)
            {
                return true;
            }
            if (especialidad.IdEspecialidadClinica==h.EspecialidadClinica.IdEspecialidadClinica)
            {
                return true;
            }
            return false;
        }
        private Paciente buscarPersonaPorRut(String rut)
        {
            try
            {
                String query = "SELECT ID_PACIENTE,RUT,NOMBRE,APELLIDO_PATERNO,APELLIDO_MATERNO,TELEFONO,FECHA_NACIMIENTO,SEXO,CORREO"
                    + " FROM BIOCENTRO_DB.dbo.PACIENTE "
                    + " WHERE RUT='" + rut + "';";
                DataSet dataTable = this.utilMethods.listarObjetoMultiTabla(query);
                if (dataTable.Tables[0].Rows.Count == 0)
                {
                    return null;
                }
                Paciente persona = null;
                foreach (DataRow row in dataTable.Tables[0].Rows)
                {
                    persona = generarObjetoPaciente(row);
                }
                return persona;
            }
            catch (Exception ex)
            {
                verErrorEnConsola(ex, "generarListaUsuarios");
                throw new Exception("Ocurrio un error al generar la lista de usuarios ", new Exception());
            }
        }
        //Valida si las Horas de atencion se encuentran Reservadas y con que estado
        private Boolean validarSiRecervaFueTomada(int idHora)
        {
            try
            {
                String query = "SELECT ID_HORA FROM BIOCENTRO_DB.dbo.HORA_ATENCION where ID_HORA = " + idHora + " AND (ID_ESTADO <> 3);";
                DataSet dataTable = this.utilMethods.listarObjetoMultiTabla(query);
                if (dataTable.Tables[0].Rows.Count == 0)
                {
                    return false;
                }
                return true;
            }
            catch (Exception ex)
            {
                verErrorEnConsola(ex, "validarSiRecervaFueTomada");
                throw new Exception("Ocurrio un error al validar la hora ", new Exception());
            }
        }

        //Retorna el listado de las horas, los bloques, los especialistas y persona, sin parametros
        // O Filtro por id de la HoraAtencion
        private List<HoraAtencion> buscarInfoHorasEspecialistas(List<int> listIdHoraAtencion)
        {
            try
            {
                //Query retorna el listado de las horas, los bloques, los especialistas y persona
                String query = "select emp.ID_EMPLEADO,emp.NOMBRE,emp.APELLIDO_PATERNO,emp.APELLIDO_MATERNO,"
                + " hora.ID_HORA,hora.FECHA,"
                + " blo.HORA_INICIO,blo.HORA_FIN,"
                + " sal.ID_SALA,sal.NOMBRE as sNo, "
                + " espClin.ID_ESPECIALIDAD,espClin.NOMBRE as nombreEspecialidad,espClin.PRECIO"
                + " FROM BIOCENTRO_DB.dbo.HORA_ATENCION as hora "
                + " JOIN BIOCENTRO_DB.dbo.BLOQUE as blo on hora.ID_BLOQUE=blo.ID_BLOQUE "
                + " JOIN BIOCENTRO_DB.dbo.ESPECIALIDAD_CLINICA as espClin on espClin.ID_ESPECIALIDAD=hora.ID_ESPECIALIDAD "
                + " JOIN BIOCENTRO_DB.dbo.SALA as sal on sal.ID_SALA = hora.ID_SALA "
                + "  JOIN BIOCENTRO_DB.dbo.EMPLEADO as emp on emp.ID_EMPLEADO = hora.ID_TERAPEUTA ";
                if (listIdHoraAtencion!= null && listIdHoraAtencion.Count!=0)
                {
                    String ids = listIdHoraAtencion.Aggregate(new StringBuilder(), (str, next) => str.Append(str.Length == 0 ? "" : ",").Append(next)).ToString();
                    query = query + " WHERE hora.ID_HORA IN (" + ids + ") ; ";
                }
                else
                {
                    query = query + " WHERE ( hora.ID_PACIENTE IS NULL OR hora.ID_ESTADO = 3) ";
                }
                DataSet dataTable = this.utilMethods.listarObjetoMultiTabla(query);
                if (dataTable.Tables[0].Rows.Count == 0)
                {
                    return null;
                }
                List<HoraAtencion> listaHoras = new List<HoraAtencion>();
                foreach (DataRow row in dataTable.Tables[0].Rows)
                {
                    listaHoras.Add(generarObjetoHoraAtencion(row,true,true,false,true,false,false,true));
                }
                return listaHoras;
            }
            catch (Exception ex)
            {
                verErrorEnConsola(ex, "buscarInfoHorasEspecialistas");
                throw new Exception("Ocurrio un error al buscar las horas de los especialistas ", new Exception());
            }
        }
        
        //Retorna el listado de las horas, los bloques, los especialistas y persona, sin parametros
        // O Filtro por id de la HoraAtencion
        public HoraAtencion buscarDetalleHora(int idHoraAtencion)
        {
            try
            {
                //Query retorna el listado de las horas, los bloques, los especialistas y persona
                String query = "select emp.ID_EMPLEADO,emp.NOMBRE,emp.APELLIDO_PATERNO,emp.APELLIDO_MATERNO,"
                + " hora.ID_HORA,hora.FECHA,"
                + " blo.HORA_INICIO,blo.HORA_FIN,"
                + " sal.ID_SALA,sal.NOMBRE as sNo, "
                + " espClin.ID_ESPECIALIDAD,espClin.NOMBRE as nombreEspecialidad,espClin.PRECIO"
                + " FROM BIOCENTRO_DB.dbo.HORA_ATENCION as hora "
                + " JOIN BIOCENTRO_DB.dbo.BLOQUE as blo on hora.ID_BLOQUE=blo.ID_BLOQUE "
                + " JOIN BIOCENTRO_DB.dbo.ESPECIALIDAD_CLINICA as espClin on espClin.ID_ESPECIALIDAD=hora.ID_ESPECIALIDAD "
                + " JOIN BIOCENTRO_DB.dbo.SALA as sal on sal.ID_SALA = hora.ID_SALA "
                + " JOIN BIOCENTRO_DB.dbo.EMPLEADO as emp on emp.ID_EMPLEADO = hora.ID_TERAPEUTA "
                + " WHERE hora.ID_HORA = " + idHoraAtencion  + " ; ";
               
                DataSet dataTable = this.utilMethods.listarObjetoMultiTabla(query);
                if (dataTable.Tables[0].Rows.Count == 0)
                {
                    return null;
                }
                HoraAtencion horaAtencion = new HoraAtencion();
                foreach (DataRow row in dataTable.Tables[0].Rows)
                {
                    horaAtencion = (generarObjetoHoraAtencion(row, true, true, false, true, false, false, true));
                }
                return horaAtencion;
            }
            catch (Exception ex)
            {
                verErrorEnConsola(ex, "buscarDetalleHora");
                throw new Exception("Ocurrio un error al buscar las horas de los especialistas ", new Exception());
            }
        }

        //Crea un objeto de tipo HoraAtencion
        private HoraAtencion generarObjetoHoraAtencion(DataRow row,Boolean bloque, Boolean terapeuta, Boolean paciente, Boolean sala, Boolean estadoRes,
            Boolean venta, Boolean espeClinica)
        {
            HoraAtencion horaAtencion = new HoraAtencion();
            if (validarSiRowTieneCampo(row, "ID_HORA"))
            {
                horaAtencion.IdHora = Convert.ToInt32(row["ID_HORA"]);
            }
            if (validarSiRowTieneCampo(row, "FECHA"))
            {
                horaAtencion.Fecha = Convert.ToDateTime(row["FECHA"]);
            }
            if (bloque)
            {
                horaAtencion.IdBloque = generarObjetoBloque(row);
            }
            if (terapeuta)
            {
                horaAtencion.Terapeuta = generarObjetoTerapeuta(row);
            }
            if (paciente)
            {
                horaAtencion.Paciente = generarObjetoPaciente(row);
            }
            if (sala)
            {
                horaAtencion.Sala = generarObjetoSala(row);
            }
            if (estadoRes)
            {
                horaAtencion.EstadoReserva = generarObjetoEstadoReserva(row);
            }
            if (venta)
            {
                horaAtencion.Venta = generarObjetoVenta(row);
            }
            if (espeClinica)
            {
                horaAtencion.EspecialidadClinica = generarObjetEspecialidadClinica(row);
            }          
            return horaAtencion;
        }
        //Crea un objeto de tipo Venta
        private Venta generarObjetoVenta(DataRow row)
        {
            Venta venta = new Venta();

            if (validarSiRowTieneCampo(row, "ID_VENTA"))
            {
                venta.IdVenta = Convert.ToInt32(row["ID_VENTA"]);
            }

            if (validarSiRowTieneCampo(row, "FECHA_PAGO"))
            {
                venta.FechaPago = Convert.ToDateTime(row["FECHA_PAGO"]);
            }

            if (validarSiRowTieneCampo(row, "MONTO"))
            {
                venta.Monto = Convert.ToInt32(row["MONTO"]);
            }
            venta.EstadoVenta = generarObjetoEstadoVenta(row);
            venta.MedioPago = generarObjetoMedioPago(row);
            return venta;
        }
        //Crea un objeto de tipo MedioPago
        private MedioPago generarObjetoMedioPago(DataRow row)
        {
            MedioPago medioPago = new MedioPago();
            if (validarSiRowTieneCampo(row, "ID_MEDIO_PAGO"))
            {
                medioPago.IdMedioPago = Convert.ToInt32(row["ID_MEDIO_PAGO"]);
            }
            if (validarSiRowTieneCampo(row, "NOMBRE", "mpagNombre"))
            {
                medioPago.Nombre = obtenerCampoDobleNombre(row, "NOMBRE", "mpagNombre");
            }
            return medioPago;
        }
        //Crea un objeto de tipo EstadoVenta
        private EstadoVenta generarObjetoEstadoVenta(DataRow row)
        {
            EstadoVenta estadoVenta = new EstadoVenta();
            if (validarSiRowTieneCampo(row, "ID_ESTADO_VENTA"))
            {
                estadoVenta.IdEstadoVenta = Convert.ToInt32(row["ID_ESTADO_VENTA"]);
            }
            if (validarSiRowTieneCampo(row, "DESCRIPCION", "estVentaDescripcion"))
            {
                estadoVenta.Descripcion = obtenerCampoDobleNombre(row, "DESCRIPCION", "estVentaDescripcion");
            }
            return estadoVenta;
        }
        //Crea un objeto de tipo Sala
        private Sala generarObjetoSala(DataRow row)
        {
            Sala sala = new Sala();
            if (validarSiRowTieneCampo(row, "ID_SALA"))
            {
                sala.IdSala = Convert.ToInt32(row["ID_SALA"]);
            }
            if (validarSiRowTieneCampo(row, "NOMBRE", "sNo"))
            {
                sala.Nombre = obtenerCampoDobleNombre(row, "NOMBRE", "sNo");
            }
            return sala;
        }
        //Crea un objeto de tipo Paciente
        private Paciente generarObjetoPaciente(DataRow row)
        {
            Paciente persona = new Paciente();
            if (validarSiRowTieneCampo(row, "ID_PACIENTE"))
            {
                persona.IdPaciente = Convert.ToInt32(row["ID_PACIENTE"]);
            }
            if (validarSiRowTieneCampo(row, "RUT", "pRu"))
            {
                persona.Rut = obtenerCampoDobleNombre(row, "RUT", "pRu");
            }
            if (validarSiRowTieneCampo(row, "NOMBRE", "pNo"))
            {
                persona.Nombre = obtenerCampoDobleNombre(row, "NOMBRE", "pNo");
            }
            if (validarSiRowTieneCampo(row, "APELLIDO_PATERNO", "pPa"))
            {
                persona.ApellidoPaterno = obtenerCampoDobleNombre(row, "APELLIDO_PATERNO", "pPa");
            }
            if (validarSiRowTieneCampo(row, "APELLIDO_MATERNO", "pMa"))
            {
                persona.ApellidoMaterno = obtenerCampoDobleNombre(row, "APELLIDO_MATERNO", "pMa");
            }
            if (validarSiRowTieneCampo(row, "TELEFONO", "pTe"))
            {
                persona.Telefono = obtenerCampoDobleNombre(row, "TELEFONO", "pTe");
            }
            if (validarSiRowTieneCampo(row, "FECHA_NACIMIENTO", "pFe"))
            {
                persona.FechaNacimiento = Convert.ToDateTime(obtenerCampoDobleNombre(row, "FECHA_NACIMIENTO", "pFe"));
            }
            if (validarSiRowTieneCampo(row, "SEXO", "pSe"))
            {
                persona.Sexo = Convert.ToChar(obtenerCampoDobleNombre(row, "SEXO", "pSe"));
            }
            if (validarSiRowTieneCampo(row, "CORREO", "pCo"))
            {
                persona.Correo = obtenerCampoDobleNombre(row, "CORREO", "pCo");
            }
            return persona;
        }
        //Crea un objeto de tipo Empleado
        private Empleado generarObjetoTerapeuta(DataRow row)
        {
            Empleado empleado = new Empleado();
            if (validarSiRowTieneCampo(row, "ID_EMPLEADO"))
            {
                empleado.IdEmpleado = Convert.ToInt32(row["ID_EMPLEADO"]);
            }
            if (validarSiRowTieneCampo(row, "USUARIO"))
            {
                empleado.Usuario = Convert.ToString(row["USUARIO"]);
            }
            if (validarSiRowTieneCampo(row, "CONTRASENA"))
            {
                empleado.Contraseña = Convert.ToString(row["CONTRASENA"]);
            }
            if (validarSiRowTieneCampo(row, "NOMBRE"))
            {
                empleado.Nombre = Convert.ToString(row["NOMBRE"]);
            }
            if (validarSiRowTieneCampo(row, "APELLIDO_PATERNO"))
            {
                empleado.ApellidoPaterno = Convert.ToString(row["APELLIDO_PATERNO"]);
            }
            if (validarSiRowTieneCampo(row, "APELLIDO_MATERNO"))
            {
                empleado.ApellidoMaterno = Convert.ToString(row["APELLIDO_MATERNO"]);
            }
            if (validarSiRowTieneCampo(row, "TELEFONO"))
            {
                empleado.Telefono = Convert.ToInt32(row["TELEFONO"]);
            }
            if (validarSiRowTieneCampo(row, "CORREO"))
            {
                empleado.Correo = Convert.ToString(row["CORREO"]);
            }
            empleado.Cargo = generarObjetoCargo(row);
            if (validarSiRowTieneCampo(row, "FECHA_NACIMIENTO"))
            {
                empleado.FechaNacimiento = Convert.ToString(row["FECHA_NACIMIENTO"]);
            }
            return empleado;
        }
        //Crea un objeto de tipo Cargo
        private Cargo generarObjetoCargo(DataRow row)
        {
            Cargo cargo = new Cargo();
            if (validarSiRowTieneCampo(row, "ID_CARGO"))
            {
                cargo.IdCargo = Convert.ToInt32(row["ID_CARGO"]);
            }
            if (validarSiRowTieneCampo(row, "NOMBRE"))
            {
                cargo.Nombre = Convert.ToString(row["NOMBRE"]);
            }
            return cargo;
        }

        //Crea un objeto de tipo EstadoReserva
        private EstadoReserva generarObjetoEstadoReserva(DataRow row)
        {
            EstadoReserva estado = new EstadoReserva();
            if (validarSiRowTieneCampo(row, "ID_ESTADO"))
            {
                estado.IdEstado = Convert.ToInt32(row["ID_ESTADO"]);
            }
            if (validarSiRowTieneCampo(row, "DESCRIPCION"))
            {
                estado.Descripcion = Convert.ToString(row["DESCRIPCION"]);
            }
            return estado;
        }
        //Crea un objeto de tipo EspecialidadSala
        private EspecialidadSala generarObjetoEspecialidadSala(DataRow row)
        {
            EspecialidadSala sala = new EspecialidadSala();
            if (validarSiRowTieneCampo(row, "ID_ESPECALIDAD_SALA"))
            {
                sala.IdEspecialidadSala = Convert.ToInt32(row["ID_ESPECALIDAD_SALA"]);
            }
            sala.EspecialidadClinica = generarObjetEspecialidadClinica(row);
            sala.Sala = generarObjetoSala(row);
            return sala;
        }
        //Crea un objeto de tipo EspecialidadTerapeuta
        private EspecialidadTerapeuta generarObjetoEspecialidadTerapeuta(DataRow row)
        {
            EspecialidadTerapeuta especialidadTerapeuta = new EspecialidadTerapeuta();
            if (validarSiRowTieneCampo(row, "ID"))
            {
                especialidadTerapeuta.IdEspecialidadTerapeuta = Convert.ToInt32(row["ID"]);
            }
            especialidadTerapeuta.EspecialidadClinica = generarObjetEspecialidadClinica(row);
            especialidadTerapeuta.Empleado = generarObjetoTerapeuta(row);
            return especialidadTerapeuta;
        }
        //Crea un objeto de tipo Bloque
        private Bloque generarObjetoBloque(DataRow row)
        {
            Bloque bloque = new Bloque();
            if (validarSiRowTieneCampo(row, "ID_BLOQUE"))
            {
                bloque.IdBloque = Convert.ToInt32(row["ID_BLOQUE"]);
            }
            bloque.HoraInicio = Convert.ToInt32(row["HORA_INICIO"]);
            bloque.HoraFin = Convert.ToInt32(row["HORA_FIN"]);
            return bloque;
        }
        //Crea un objeto de tipo EspecialidadClinica
        private EspecialidadClinica generarObjetEspecialidadClinica(DataRow row)
        {
            EspecialidadClinica especialidad = new EspecialidadClinica();
            if (validarSiRowTieneCampo(row, "ID_ESPECIALIDAD"))
            {
                especialidad.IdEspecialidadClinica = Convert.ToInt32(row["ID_ESPECIALIDAD"]);
            }
            if (validarSiRowTieneCampo(row, "nombreEspecialidad"))
            {
                especialidad.Nombre = Convert.ToString(row["nombreEspecialidad"]);
            }
            if (validarSiRowTieneCampo(row, "PRECIO"))
            {
                especialidad.Precio = Convert.ToInt32(row["PRECIO"]);
            }
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
                + " SET "+ columnaSet + " = " + valorSet;
            if (valorSet.Equals("3"))
            {
                query = query + " ,SET ID_VENTA=NULL";
            }
            query = query + " WHERE " + columnaWhere + " = " + valorWhere;
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
        private string obtenerCampoDobleNombre(DataRow row, string nombre1,string nombre2)
        {
            if (validarSiRowTieneCampo(row, nombre2))
            {
                return Convert.ToString(row[nombre2]);
            }
            else if (validarSiRowTieneCampo(row, nombre1))
            {
                return Convert.ToString(row[nombre1]);
            }
            return null;
        }
        private Boolean validarSiRowTieneCampo(DataRow row, String campo1, String campo2)
        {
            if (validarSiRowTieneCampo(row, campo2))
            {
                return true;
            }
            if (validarSiRowTieneCampo(row, campo1))
            {
                return true;
            }
            return false;
        }
        private Boolean validarSiRowTieneCampo(DataRow row,String campo)
        {
            try
            {
                String st = Convert.ToString(row[campo]);
                if (st.Length==0)
                {
                    return false;
                }
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
            
        }
        private StatusResponce generarObjetoStatusResponce(String estado,String msj)
        {
            StatusResponce statusResponce = new StatusResponce();
            statusResponce.Estado = estado;
            statusResponce.Mensaje = msj;
            return statusResponce;
        }
    }
}
