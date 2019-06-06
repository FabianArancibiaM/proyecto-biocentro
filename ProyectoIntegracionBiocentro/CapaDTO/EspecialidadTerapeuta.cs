using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CapaDTO
{
    public class EspecialidadTerapeuta
    {
        private int idEspecialidadTerapeuta;
        private Empleado empleado;
        private EspecialidadClinica especialidadClinica;

        public int IdEspecialidadTerapeuta { get => idEspecialidadTerapeuta; set => idEspecialidadTerapeuta = value; }
        public EspecialidadClinica EspecialidadClinica { get => especialidadClinica; set => especialidadClinica = value; }
        public Empleado Empleado { get => empleado; set => empleado = value; }
    }
}
