using CapaDTO;
using CapaNegocio;
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
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            cargarRol();
        }

        private void Button1_Click(object sender, EventArgs e)
        {
            try
            {
                Persona persona = new Persona();
                persona.Rut = this.txtRut.Text;
                persona.Nombre = this.txtNombre.Text;
                persona.ApellidoPaterno = this.txtPaterno.Text;
                persona.ApellidoMaterno = this.txtMaterno.Text;

                Class1 class1 = new Class1();
                class1.insertarUsuario(persona);
                MessageBox.Show("Guardado Correctamente");
            }
            catch(Exception ex)
            {
                MessageBox.Show("Error al guardar: ",ex.Message);
            }
        }

        private void cargarRol()
        {
            try
            {
                Class1 class1 = new Class1();
                List<RolUsuario> listaUsuarioRol = class1.listarRoles();
                foreach (RolUsuario rol in listaUsuarioRol)
                {
                    this.selectRol.Items.Add(rol);
                }
                this.selectRol.DisplayMember = "nombre";
            }
            catch(Exception ex)
            {
                MessageBox.Show("Error al cargar la data: ", ex.Message);
            }
        }

        private void Button2_Click(object sender, EventArgs e)
        {
            RolUsuario rolUsuario = new RolUsuario();
            rolUsuario = (RolUsuario)this.selectRol.SelectedItem;
            Console.WriteLine(rolUsuario.Nombre + " - "+ rolUsuario.IdRol);
        }
    }
}
