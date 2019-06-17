using CapaDTO;
using CapaNegocio;
using System;
using System.Collections.Generic;
using System.Web.Services;

namespace CapaServicio
{
    /// <summary>
    /// Descripción breve de WebServiceCliente
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    // Para permitir que se llame a este servicio web desde un script, usando ASP.NET AJAX, quite la marca de comentario de la línea siguiente. 
    //[System.Web.Script.Services.ScriptService]
    public class WebServiceCliente : System.Web.Services.WebService
    {

        [WebMethod]
        public StatusResponce registrarPacienteService(Paciente persona, int idHoraAtencion)
        {
            NegocioService negocio = new NegocioService();
            return negocio.registrarPaciente(persona, idHoraAtencion);
        }
        [WebMethod]
        public List<HoraAtencion> buscarHorasDisponiblesService(EspecialidadClinica especialidad, DateTime? fecha, Empleado persona)
        {
            NegocioService negocio = new NegocioService();
            return negocio.buscarHorasDisponibles(especialidad, fecha, persona);
        }
        [WebMethod]
        public HoraAtencion buscarDetalleHoraService(int idHoraAtencion)
        {
            NegocioService negocio = new NegocioService();
            return negocio.buscarDetalleHora(idHoraAtencion);
        }
        [WebMethod]
        public List<EspecialidadClinica> generarListaEspecialidadService()
        {
            NegocioService negocio = new NegocioService();
            return negocio.generarListaEspecialidad();
        }
        [WebMethod]
        public List<EspecialidadTerapeuta> generarListaEspecialistaService()
        {
            NegocioService negocio = new NegocioService();
            return negocio.generarListaEspecialista();
        }
        [WebMethod]
        public Paciente buscarPacienteService(String rut)
        {
            NegocioService negocio = new NegocioService();
            return negocio.buscarPaciente(rut);
        }
        [WebMethod]
        public List<HoraAtencion> listaReservasPorRutAndCorreoService(String rut,String correo)
        {
            NegocioService negocio = new NegocioService();
            return negocio.listaReservasPorRut(rut,correo);
        }
        [WebMethod]
        public StatusResponce rechazarConfirmarReservaService(Char cambioEstado, int idReserva)
        {
            NegocioService negocio = new NegocioService();
            return negocio.rechazarConfirmarReserva(cambioEstado, idReserva);
        }
        [WebMethod]
        public List<MedioPago> generarListaMedioPagoService()
        {
            NegocioService negocio = new NegocioService();
            return negocio.generarListaMedioPago();
        }
        [WebMethod]
        public StatusResponce guardarVentaRealizadaService(Venta venta,List<int> idHora)
        {
            NegocioService negocio = new NegocioService();
            return negocio.guardarVentaRealizada(venta, idHora);
        }
        [WebMethod]
        public List<HoraAtencion> horasPorRutPacienteMasVentaService(String rut)
        {
            NegocioService negocio = new NegocioService();
            return negocio.horasPorRutPacienteMasVenta(rut);
        }
        [WebMethod]
        public StatusResponce agregarEmpleadoService(Empleado empleado)
        {
            NegocioService negocio = new NegocioService();
            return negocio.agregarEmpleado(empleado);
        }
        [WebMethod]
        public Empleado loginService(string usuario, string password)
        {
            NegocioService negocio = new NegocioService();
            return negocio.login(usuario, password);
        }
    }
}
