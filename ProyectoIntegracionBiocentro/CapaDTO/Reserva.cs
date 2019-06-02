using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CapaDTO
{
    public class Reserva
    {
        private int idReserva;
        private HoraAtencion idHora;
        private Persona idPaciente;
        private EstadoReserva idEstado;

        public int IdReserva
        {
            get
            {
                return idReserva;
            }

            set
            {
                idReserva = value;
            }
        }

        public HoraAtencion IdHora
        {
            get
            {
                return idHora;
            }

            set
            {
                idHora = value;
            }
        }

        public Persona IdPaciente
        {
            get
            {
                return idPaciente;
            }

            set
            {
                idPaciente = value;
            }
        }

        public EstadoReserva IdEstado
        {
            get
            {
                return idEstado;
            }

            set
            {
                idEstado = value;
            }
        }
    }
}
