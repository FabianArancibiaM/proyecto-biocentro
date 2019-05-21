using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CapaDTO
{
    public class Permiso
    {
        private int idPermiso;
        private string area;

        public int IdPermiso
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

        public string Area
        {
            get
            {
                return area;
            }

            set
            {
                area = value;
            }
        }
    }
}
