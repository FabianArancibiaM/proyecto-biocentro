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
        StatusResponce registrarPaciente(Paciente paciente, int idHoraAtencion);
        //[OperationContract]
        List<HoraAtencion> buscarHorasDisponibles(EspecialidadClinica especialidad, DateTime? fecha, Empleado empleado);
        //[OperationContract]
        List<EspecialidadClinica> generarListaEspecialidad();
        //[OperationContract]
        void rechazarConfirmarReserva(Char cambioEstado, int idReserva);
        //[OperationContract]
        List<EspecialidadTerapeuta> generarListaEspecialista();
        //[OperationContract]
        Paciente buscarPaciente(String rut);
        //[OperationContract]
        List<HoraAtencion> listaReservasPorRut(String rut);
    }
}
