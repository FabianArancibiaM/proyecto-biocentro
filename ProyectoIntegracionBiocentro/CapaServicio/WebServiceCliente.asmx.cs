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
    // [System.Web.Script.Services.ScriptService]
    public class WebServiceCliente : System.Web.Services.WebService
    {

        [WebMethod]
        public string HelloWorld()
        {
            return "Hola a todos";
        }

        [WebMethod]
        public void registrarPacienteService(Persona persona, int idHoraAtencion)
        {
            try
            {
                NegocioService negocio = new NegocioService();
                negocio.registrarPaciente(persona, idHoraAtencion);
            }
            catch (Exception ex)
            {
                throw new Exception("Ocurrio un error al realizar la operacion ", ex);
            }
        }
        [WebMethod]
        public List<HoraAtencion> buscarHorasDisponiblesService(Especialidad especialidad, DateTime? fecha, Persona persona)
        {
            try
            {
                NegocioService negocio = new NegocioService();
                return negocio.buscarHorasDisponibles(especialidad, fecha, persona);
            }
            catch (Exception ex)
            {
                throw new Exception("Ocurrio un error al realizar la operacion ", ex);
            }
        }
        [WebMethod]
        public List<Especialidad> generarListaEspecialidadService()
        {
            try
            {
                NegocioService negocio = new NegocioService();
                return negocio.generarListaEspecialidad();
            }
            catch (Exception ex)
            {
                throw new Exception("Ocurrio un error al realizar la operacion ", ex);
            }
        }
        [WebMethod]
        public List<Terapeuta> generarListaEspecialistaService()
        {
            try
            {
                NegocioService negocio = new NegocioService();
                return negocio.generarListaEspecialista();
            }
            catch (Exception ex)
            {
                throw new Exception("Ocurrio un error al realizar la operacion ", ex);
            }
        }
        [WebMethod]
        public Usuario buscarPacienteService(String rut)
        {
            try
            {
                NegocioService negocio = new NegocioService();
                return negocio.buscarPaciente(rut);
            }
            catch (Exception ex)
            {
                throw new Exception("Ocurrio un error al realizar la operacion ", ex);
            }
        }
        [WebMethod]
        public List<Reserva> listaReservasPorRutService(String rut)
        {
            try
            {
                NegocioService negocio = new NegocioService();
                return negocio.listaReservasPorRut(rut);
            }
            catch (Exception ex)
            {
                throw new Exception("Ocurrio un error al realizar la operacion ", ex);
            }
        }
        [WebMethod]
        public void rechazarConfirmarReservaService(Char cambioEstado, int idReserva)
        {
            try
            {
                NegocioService negocio = new NegocioService();
                negocio.rechazarConfirmarReserva(cambioEstado, idReserva);
            }
            catch (Exception ex)
            {
                throw new Exception("Ocurrio un error al realizar la operacion ", ex);
            }
        }
    }
}
