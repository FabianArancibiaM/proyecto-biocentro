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
        private Terapeuta idTerapeuta;

        public int IdHora
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

        public DateTime Fecha
        {
            get
            {
                return fecha;
            }

            set
            {
                fecha = value;
            }
        }

        public Bloque IdBloque
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

        public Terapeuta IdTerapeuta
        {
            get
            {
                return idTerapeuta;
            }

            set
            {
                idTerapeuta = value;
            }
        }
    }
}
