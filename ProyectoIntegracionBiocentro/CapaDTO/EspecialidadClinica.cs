using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CapaDTO
{
    public class EspecialidadClinica
    {
        private int idEspecialidadClinica;
        private string nombre;
        private int precio;

        

        public string Nombre
        {
            get
            {
                return nombre;
            }

            set
            {
                nombre = value;
            }
        }

        public int Precio { get => precio; set => precio = value; }
        public int IdEspecialidadClinica { get => idEspecialidadClinica; set => idEspecialidadClinica = value; }
    }
}
