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
            cargarEspecialidad();
            cargarEspecialista();
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
        private void cargarEspecialidad()
        {
            try
            {
                this.selectEspecialidad.Items.Add("Seleccionar");
                Negocio negocio = new Negocio();
                List<Especialidad> listaEspecialidad = negocio.generarListaEspecialidad();
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
        private void cargarEspecialista()
        {
            try
            {
                this.cmbTerapeuta.Items.Add("Seleccionar");
                Negocio negocio = new Negocio();
                List<Terapeuta> listaEspecialidad = negocio.generarListaEspecialista();
                if (listaEspecialidad == null)
                {
                    MessageBox.Show("No se encontraron datos");
                    return;
                }
                foreach (Terapeuta var in listaEspecialidad)
                {

                    this.cmbTerapeuta.Items.Add(var.IdUsuario.IdPersona);
                }
                this.cmbTerapeuta.DisplayMember = "nombre";
                this.cmbTerapeuta.SelectedIndex = 0;
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
                Persona persona = null;
                if (this.cmbTerapeuta.SelectedIndex>0)
                {
                    persona = (Persona)this.cmbTerapeuta.SelectedItem;
                }
                Especialidad especialidad=null;
                if (this.selectEspecialidad.SelectedIndex>0)
                {
                    especialidad = (Especialidad)this.selectEspecialidad.SelectedItem;
                }
                DateTime? fecha=null;
                if (this.dateTimePicker1.Value.CompareTo(DateTime.Today)<0)
                {
                    MessageBox.Show(" No puede seleccionar una fecha anterior a la actual ");
                    return;
                }
                Negocio negocio = new Negocio();
                List<HoraAtencion> listHoraAtencion = negocio.buscarHorasDisponibles(especialidad,fecha, persona);
                this.dataGridView1.Columns.Clear();
                this.dataGridView1.Columns.Add("idHora", "IdHora");
                this.dataGridView1.Columns.Add("especialidad", "Especialidad");
                this.dataGridView1.Columns.Add("rut", "rut");
                this.dataGridView1.Columns.Add("nombre", "Nombre Especialista");
                this.dataGridView1.Columns.Add("horaInicio", "Hora Inicio");
                this.dataGridView1.Columns.Add("horaFin", "Hora Fin");
                this.dataGridView1.Columns["idHora"].Visible = false;
                if (listHoraAtencion==null || listHoraAtencion.Count==0)
                {
                    this.dataGridView1.Rows.Clear();
                    return ;
                }
                foreach (HoraAtencion hora in listHoraAtencion)
                {
                    String nombreCompleto = hora.IdTerapeuta.IdUsuario.IdPersona.Nombre + " " + hora.IdTerapeuta.IdUsuario.IdPersona.ApellidoPaterno 
                        + " " + hora.IdTerapeuta.IdUsuario.IdPersona.ApellidoMaterno;
                    this.dataGridView1.Rows.Add(hora.IdHora, hora.IdTerapeuta.IdEspecialidad.Nombre, 
                        hora.IdTerapeuta.IdUsuario.IdPersona.Rut, nombreCompleto, hora.IdBloque.HoraInicio, hora.IdBloque.HoraFin);
                }
                
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                MessageBox.Show("Error al cargar la data: ", ex.Message);
            }
        }
        private void Button2_Click(object sender, EventArgs e)
        {
        }
    }
}
