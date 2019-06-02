using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CapaDTO
{
    public class Usuario
    {
        private int idUsuario;
        private int estado;
        private RolUsuario idRol;
        private Persona idPersona;
        private String contraseña;
        private String nombreUsuario;
        public int IdUsuario
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

        public int Estado
        {
            get
            {
                return estado;
            }

            set
            {
                estado = value;
            }
        }

        public RolUsuario IdRol
        {
            get
            {
                return idRol;
            }

            set
            {
                idRol = value;
            }
        }

        public Persona IdPersona
        {
            get
            {
                return idPersona;
            }

            set
            {
                idPersona = value;
            }
        }

        public string Contraseña { get => contraseña; set => contraseña = value; }
        public string NombreUsuario { get => nombreUsuario; set => nombreUsuario = value; }
    }
}
