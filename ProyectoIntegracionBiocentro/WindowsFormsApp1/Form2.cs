using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowsFormsApp1
{
    public partial class Form2 : Form
    {
        public Form2()
        {
            InitializeComponent();
        }

        private void Label5_Click(object sender, EventArgs e)
        {

        }

        private void Button2_Click(object sender, EventArgs e)
        {
            if ( validarCampoEsVacio(this.txtNombre.Text)
                || validarCampoEsVacio(this.txtPaterno.Text)
                || validarCampoEsVacio(this.txtMaterno.Text)
                || validarCampoEsVacio(this.txtTelefono.Text)
                || validarCampoEsVacio(this.txtCorreo.Text))
            {
                MessageBox.Show("Debe llenar todos los campos");
                return;
            }
            if (!this.fechaNacimeinto.Checked)
            {
                MessageBox.Show("Debe ingresar su fecha de nacimiento");
                return;
            }
            ServiceCliente.WebServiceClienteSoapClient soapClient = new ServiceCliente.WebServiceClienteSoapClient();
            ServiceCliente.Empleado empleado = new ServiceCliente.Empleado();
            empleado.Usuario = this.txtUsuario.Text;
            empleado.Contraseña = this.txtContraseña.Text;
            empleado.Nombre = this.txtNombre.Text;
            empleado.ApellidoPaterno = this.txtPaterno.Text;
            empleado.ApellidoMaterno = this.txtMaterno.Text;
            empleado.Correo = this.txtCorreo.Text;
            ServiceCliente.Cargo cargo = new ServiceCliente.Cargo();
            cargo.IdCargo = 1;
            empleado.Cargo = cargo;
            empleado.FechaNacimiento = this.fechaNacimeinto.Value.Day.ToString()+"/"+ this.fechaNacimeinto.Value.Month.ToString()+"/"+ this.fechaNacimeinto.Value.Year.ToString();
            empleado.Telefono = Convert.ToInt32(this.txtTelefono.Text);
            ServiceCliente.StatusResponce statusResponce = soapClient.agregarEmpleadoService(empleado);
            if (statusResponce.Estado.Equals("error"))
            {
                MessageBox.Show(statusResponce.Mensaje, "Error");
                return;
            }
        }
        private Boolean validarCampoEsVacio(String text)
        {
            if (text == null || text.Length <= 0)
            {
                return true;
            }
            return false;
        }

        private void Button1_Click(object sender, EventArgs e)
        {
            try
            {
                ServiceCliente.WebServiceClienteSoapClient soapClient = new ServiceCliente.WebServiceClienteSoapClient();
                ServiceCliente.Empleado empleado = soapClient.loginService(this.txtUsuario.Text,this.txtContraseña.Text);
                if (empleado!=null)
                {
                    MessageBox.Show("Bienvenido "+empleado.Nombre, "Login");
                    return;
                }
                MessageBox.Show("Usuario o clave incorrectos", "Login Error");
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message, "Error");
                return;
            }
        }
    }
}
