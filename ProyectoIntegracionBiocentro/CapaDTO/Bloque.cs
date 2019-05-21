using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CapaDTO
{
    public class Bloque
    {
        private int idBloque;
        private int horaInicio;
        private int horaFin;

        public int IdBloque
        {
            get
            {
                return idBloque;
            }

            set
            {
                idBloque = value;
            }
        }

        public int HoraInicio
        {
            get
            {
                return horaInicio;
            }

            set
            {
                horaInicio = value;
            }
        }

        public int HoraFin
        {
            get
            {
                return horaFin;
            }

            set
            {
                horaFin = value;
            }
        }
    }
}
