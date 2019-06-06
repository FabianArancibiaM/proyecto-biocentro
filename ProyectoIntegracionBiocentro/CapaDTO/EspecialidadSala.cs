using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CapaDTO
{
    public class EspecialidadSala
    {
        private int idEspecialidadSala;
        private Sala sala;
        private EspecialidadClinica especialidadClinica;

        public int IdEspecialidadSala { get => idEspecialidadSala; set => idEspecialidadSala = value; }
        public EspecialidadClinica EspecialidadClinica { get => especialidadClinica; set => especialidadClinica = value; }
        public Sala Sala { get => sala; set => sala = value; }
    }
}
