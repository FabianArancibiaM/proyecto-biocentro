using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CapaDTO
{
    public class Empleado
    {
        private int idEmpleado;
        private string usuario;
        private string contraseña;
        private string nombre;
        private string apellidoPaterno;
        private string apellidoMaterno;
        private int telefono;
        private string correo;
        private Cargo cargo;
        private string fechaNacimiento;

        public int IdEmpleado { get => idEmpleado; set => idEmpleado = value; }
        public string Usuario { get => usuario; set => usuario = value; }
        public string Contraseña { get => contraseña; set => contraseña = value; }
        public string Nombre { get => nombre; set => nombre = value; }
        public string ApellidoPaterno { get => apellidoPaterno; set => apellidoPaterno = value; }
        public string ApellidoMaterno { get => apellidoMaterno; set => apellidoMaterno = value; }
        public int Telefono { get => telefono; set => telefono = value; }
        public string Correo { get => correo; set => correo = value; }
        public string FechaNacimiento { get => fechaNacimiento; set => fechaNacimiento = value; }
        public Cargo Cargo { get => cargo; set => cargo = value; }
    }
}
