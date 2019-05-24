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
            this.selectEspecialidad.Enabled = false;
            this.btnBuscar.Enabled = false;
            cargarRol();
            cargarEspecialidad();
        }

        private void Button1_Click(object sender, EventArgs e)
        {
            try
            {
                if (validarCampoEsVacio(this.txtRut.Text) 
                    || validarCampoEsVacio(this.txtNombre.Text) 
                    || validarCampoEsVacio(this.txtPaterno.Text) 
                    || validarCampoEsVacio(this.txtMaterno.Text))
                {
                    MessageBox.Show("Debe llenar todos los campos");
                    return;
                }
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
        private Boolean validarCampoEsVacio(String text)
        {
            if (text==null || text.Length<=0)
            {
                return true;
            }
            return false;
        }
        private void cargarRol()
        {
            try
            {
                GestionUsuarios gestion = new GestionUsuarios();
                List<RolUsuario> listaUsuarioRol = gestion.generarListaRoles();
                if (listaUsuarioRol==null)
                {
                    MessageBox.Show("No se encontraron datos");
                    return;
                }
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
        private void cargarEspecialidad()
        {
            try
            {
                this.selectEspecialidad.Items.Add("Seleccionar");
                GestionUsuarios gestion = new GestionUsuarios();
                List<Especialidad> listaEspecialidad = gestion.generarListaEspecialidad();
                if (listaEspecialidad == null)
                {
                    MessageBox.Show("No se encontraron datos");
                    return;
                }
                foreach (Especialidad var in listaEspecialidad)
                {
                    this.selectEspecialidad.Items.Add(var);
                }
                this.selectEspecialidad.DisplayMember = "nombre";
                this.selectEspecialidad.SelectedIndex =0;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al cargar la data: ", ex.Message);
            }
        }

        private void BtnBuscar_Click(object sender, EventArgs e)
        {
            try
            {
                if (this.selectEspecialidad.SelectedIndex==0)
                {
                    MessageBox.Show("Debe seleccionar una especialidad");
                    return;
                }
                Especialidad especialidad = (Especialidad)this.selectEspecialidad.SelectedItem;
                GestionUsuarios gestion = new GestionUsuarios();
                List<Terapeuta>list = gestion.buscarTerapeutas(especialidad.IdEspecialidad);
                this.dataGridView1.Columns.Add("idTerapeuta","IdTerapeuta");
                this.dataGridView1.Columns.Add("especialidad","Especialidad");
                this.dataGridView1.Columns.Add("rut", "rut");
                this.dataGridView1.Columns.Add("nombre","Nombre Especialista");
                foreach (Terapeuta tera in list)
                {
                    String nombreCompleto = tera.IdUsuario.IdPersona.Nombre + " " + tera.IdUsuario.IdPersona.ApellidoPaterno + " " + tera.IdUsuario.IdPersona.ApellidoMaterno;
                    this.dataGridView1.Rows.Add(tera.IdTerapeuta, tera.IdEspecialidad.Nombre, tera.IdUsuario.IdPersona.Rut, nombreCompleto);
                }
                this.dataGridView1.Columns["idTerapeuta"].Visible = false;
                this.selectEspecialidad.Enabled = false;
                this.btnBuscar.Enabled = false;
            }
            catch (Exception ex)
            {
                this.selectEspecialidad.Enabled = false;
                this.btnBuscar.Enabled = false;
                Console.WriteLine(ex);
                MessageBox.Show("Error al cargar la data: ", ex.Message);
            }
        }
        private void buscarHorasSegunFecha(object sender, EventArgs e)
        {

            this.selectEspecialidad.Enabled = true;
            this.btnBuscar.Enabled = true;
        }
    }
}
