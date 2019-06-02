using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CapaDTO
{
    public class StatusResponce
    {
        private String mensaje;
        private String estado;

        public StatusResponce()
        {
        }

        public string Estado { get => estado; set => estado = value; }
        public string Mensaje { get => mensaje; set => mensaje = value; }
    }
}
