using CapaDTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;

namespace CapaNegocio
{
    //[ServiceContract]
    public interface INegocioService
    {
        //[OperationContract]
        void registrarPaciente(Persona persona, int idHoraAtencion);
        //[OperationContract]
        List<HoraAtencion> buscarHorasDisponibles(EspecialidadClinica especialidad, DateTime? fecha, Persona persona);
        //[OperationContract]
        List<EspecialidadClinica> generarListaEspecialidad();
        //[OperationContract]
        List<Terapeuta> generarListaEspecialista();
        //[OperationContract]
        Usuario buscarPaciente(String rut);
        //[OperationContract]
        List<Reserva> listaReservasPorRut(String rut);
        //[OperationContract]
        void rechazarConfirmarReserva(Char cambioEstado, int idReserva);
    }
}
