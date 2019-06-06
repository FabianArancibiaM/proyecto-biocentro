using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CapaDTO
{
    public class Paciente
    {
        private int idPaciente;
        private string rut;
        private string nombre;
        private string apellidoPaterno;
        private string apellidoMaterno;
        private String telefono;
        private DateTime fechaNacimiento;
        private Char sexo;
        private String correo;


        public string Rut
        {
            get
            {
                return rut;
            }

            set
            {
                rut = value;
            }
        }

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

        public string ApellidoPaterno
        {
            get
            {
                return apellidoPaterno;
            }

            set
            {
                apellidoPaterno = value;
            }
        }

        public string ApellidoMaterno
        {
            get
            {
                return apellidoMaterno;
            }

            set
            {
                apellidoMaterno = value;
            }
        }

        public String Telefono { get => telefono; set => telefono = value; }
        public DateTime FechaNacimiento { get => fechaNacimiento; set => fechaNacimiento = value; }
        public char Sexo { get => sexo; set => sexo = value; }
        public string Correo { get => correo; set => correo = value; }
        public int IdPaciente { get => idPaciente; set => idPaciente = value; }
    }
}
