using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CapaDTO
{
    public class Terapeuta
    {
        private int idTerapeuta;
        private Especialidad idEspecialidad;
        private Usuario idUsuario;

        public int IdTerapeuta
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

        public Especialidad IdEspecialidad
        {
            get
            {
                return idEspecialidad;
            }

            set
            {
                idEspecialidad = value;
            }
        }

        public Usuario IdUsuario
        {
            get
            {
                return idUsuario;
            }

            set
            {
                idUsuario = value;
            }
        }
    }
}
