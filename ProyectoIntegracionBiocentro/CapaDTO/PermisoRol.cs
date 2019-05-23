using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CapaDTO
{
    public class PermisoRol
    {
        private int idPermisoRol;
        private RolUsuario idRol;
        private Permiso idPermiso;

        public int IdPermisoRol
        {
            get
            {
                return idPermisoRol;
            }

            set
            {
                idPermisoRol = value;
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

        public Permiso IdPermiso
        {
            get
            {
                return idPermiso;
            }

            set
            {
                idPermiso = value;
            }
        }
    }
}
