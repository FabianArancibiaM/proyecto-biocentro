using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CapaDTO
{
    public class HoraAtencion
    {
        private int idHora;
        private DateTime fecha;
        private Bloque idBloque;
        private Empleado terapeuta;
        private Paciente paciente;
        private Sala sala;
        private EstadoReserva estadoReserva;
        private Venta venta;
        private EspecialidadClinica especialidadClinica;

        public int IdHora { get => idHora; set => idHora = value; }
        public DateTime Fecha { get => fecha; set => fecha = value; }
        public Bloque IdBloque { get => idBloque; set => idBloque = value; }
        public Paciente Paciente { get => paciente; set => paciente = value; }
        public EstadoReserva EstadoReserva { get => estadoReserva; set => estadoReserva = value; }
        public EspecialidadClinica EspecialidadClinica { get => especialidadClinica; set => especialidadClinica = value; }
        public Empleado Terapeuta { get => terapeuta; set => terapeuta = value; }
        public Sala Sala { get => sala; set => sala = value; }
        public Venta Venta { get => venta; set => venta = value; }
    }
}
