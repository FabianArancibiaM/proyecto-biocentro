using System;
using System.Collections.Generic;
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
            this.btnCancelar.Enabled = false;
            this.btnConfirmar.Enabled = false;
        }

        private void Button1_Click(object sender, EventArgs e)
        {
            try
            {
                if (this.dataGridView1.SelectedRows.Count==0)
                {
                    MessageBox.Show("Debe Seleccionar una hora");
                    return;
                }
                if (validarCampoEsVacio(this.txtRut.Text) 
                    || validarCampoEsVacio(this.txtNombre.Text) 
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
                char sexo = 'F';
                if (this.radioFemenino.Checked)
                {
                    sexo = 'F';
                }
                if (this.ButonMasculino.Checked)
                {
                    sexo = 'M';
                }
                if (this.radioIndefinido.Checked)
                {
                    sexo = 'x';
                }
                int idHoraSeleccionada = Convert.ToInt32(this.dataGridView1.SelectedRows[0].Cells[0].Value.ToString());
                ServiceCliente.WebServiceClienteSoapClient soapClient = new ServiceCliente.WebServiceClienteSoapClient();
                ServiceCliente.Persona persona = new ServiceCliente.Persona();
                persona.Rut = this.txtRut.Text;
                persona.Nombre = this.txtNombre.Text;
                persona.ApellidoPaterno = this.txtPaterno.Text;
                persona.ApellidoMaterno = this.txtMaterno.Text;
                persona.Correo = this.txtCorreo.Text;
                persona.Sexo = sexo;
                persona.FechaNacimiento = this.fechaNacimeinto.Value;
                persona.Telefono = this.txtTelefono.Text;
                ServiceCliente.StatusResponce statusResponce = soapClient.registrarPacienteService(persona, idHoraSeleccionada);
                if (statusResponce.Estado.Equals("error"))
                {
                    MessageBox.Show(statusResponce.Mensaje, "Error");
                    return;
                }
                this.txtRut.Text = String.Empty;
                this.txtNombre.Text = String.Empty;
                this.txtPaterno.Text = String.Empty;
                this.txtMaterno.Text = String.Empty;
                this.txtCorreo.Text = String.Empty;
                this.txtTelefono.Text = String.Empty;
                this.ButonMasculino.Checked=true;
                this.fechaNacimeinto.Value = DateTime.Now;
                this.dataGridView1.Columns.Clear();
                this.selectEspecialidad.SelectedIndex = 0;
                this.cmbTerapeuta.SelectedIndex = 0;
                this.dateTimePicker1.Value = DateTime.Now;
                MessageBox.Show(statusResponce.Mensaje);

            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message,"Error al guardar ");
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
                ServiceCliente.WebServiceClienteSoapClient soapClient = new ServiceCliente.WebServiceClienteSoapClient();
                List<ServiceCliente.Especialidad> listaEspecialidad = new List<ServiceCliente.Especialidad>();
                listaEspecialidad.AddRange(soapClient.generarListaEspecialidadService());
                if (listaEspecialidad == null)
                {
                    MessageBox.Show("No se encontraron datos");
                    return;
                }
                foreach (ServiceCliente.Especialidad var in listaEspecialidad)
                {
                    this.selectEspecialidad.Items.Add(var);
                }
                this.selectEspecialidad.DisplayMember = "nombre";
                this.selectEspecialidad.SelectedIndex =0;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message,"Error al guardar: ");
            }
        }
        private void cargarEspecialista()
        {
            try
            {
                this.cmbTerapeuta.Items.Add("Seleccionar");
                ServiceCliente.WebServiceClienteSoapClient soapClient = new ServiceCliente.WebServiceClienteSoapClient();
                List<ServiceCliente.Terapeuta> listaEspecialidad = new List<ServiceCliente.Terapeuta>();
                listaEspecialidad.AddRange(soapClient.generarListaEspecialistaService());
                if (listaEspecialidad == null)
                {
                    MessageBox.Show("No se encontraron datos");
                    return;
                }
                foreach (ServiceCliente.Terapeuta var in listaEspecialidad)
                {

                    this.cmbTerapeuta.Items.Add(var.IdUsuario.IdPersona);
                }
                this.cmbTerapeuta.DisplayMember = "nombre";
                this.cmbTerapeuta.SelectedIndex = 0;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message,"Error al guardar: ");
            }
        }

        private void BtnBuscar_Click(object sender, EventArgs e)
        {
            try
            {
                ServiceCliente.Persona persona = null;
                if (this.cmbTerapeuta.SelectedIndex>0)
                {
                    persona = (ServiceCliente.Persona)this.cmbTerapeuta.SelectedItem;
                }
                ServiceCliente.Especialidad especialidad=null;
                if (this.selectEspecialidad.SelectedIndex>0)
                {
                    especialidad = (ServiceCliente.Especialidad)this.selectEspecialidad.SelectedItem;
                }
                DateTime? fecha=null;
                if (this.dateTimePicker1.Value.CompareTo(DateTime.Today)<0)
                {
                    MessageBox.Show(" No puede seleccionar una fecha anterior a la actual ");
                    this.dateTimePicker1.Value = DateTime.Now;
                    return;
                }
                fecha = this.dateTimePicker1.Value;
                ServiceCliente.WebServiceClienteSoapClient soapClient = new ServiceCliente.WebServiceClienteSoapClient();
                List<ServiceCliente.HoraAtencion> listHoraAtencion = new List<ServiceCliente.HoraAtencion>();
                listHoraAtencion.AddRange(soapClient.buscarHorasDisponiblesService(especialidad, fecha, persona));
                this.dataGridView1.Columns.Clear();
                this.dataGridView1.Columns.Add("idHora", "IdHora");
                this.dataGridView1.Columns.Add("especialidad", "Especialidad");
                this.dataGridView1.Columns.Add("rut", "rut");
                this.dataGridView1.Columns.Add("nombre", "Nombre Especialista");
                this.dataGridView1.Columns.Add("horaInicio", "Hora Inicio");
                this.dataGridView1.Columns.Add("horaFin", "Hora Fin");
                //this.dataGridView1.Columns["idHora"].Visible = false;
                if (listHoraAtencion==null || listHoraAtencion.Count==0)
                {
                    this.dataGridView1.Rows.Clear();
                    MessageBox.Show(" No hay horas disponibles para esa fecha ");
                    this.dateTimePicker1.Value = DateTime.Now;
                    return ;
                }
                foreach (ServiceCliente.HoraAtencion hora in listHoraAtencion)
                {
                    String nombreCompleto = hora.IdTerapeuta.IdUsuario.IdPersona.Nombre + " " + hora.IdTerapeuta.IdUsuario.IdPersona.ApellidoPaterno 
                        + " " + hora.IdTerapeuta.IdUsuario.IdPersona.ApellidoMaterno;
                    this.dataGridView1.Rows.Add(hora.IdHora, hora.IdTerapeuta.IdEspecialidad.Nombre, 
                        hora.IdTerapeuta.IdUsuario.IdPersona.Rut, nombreCompleto, hora.IdBloque.HoraInicio, hora.IdBloque.HoraFin);
                }
                this.dataGridView1.ClearSelection();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message,"Error al guardar: ");
            }
        }
        private void Button2_Click(object sender, EventArgs e)
        {
            try
            {
                if (String.IsNullOrEmpty(this.rutMisReservas.Text))
                {
                    MessageBox.Show(" Debe ingresar un rut");
                    return;
                }
                ServiceCliente.WebServiceClienteSoapClient soapClient = new ServiceCliente.WebServiceClienteSoapClient();
                List<ServiceCliente.Reserva> listReserva = new List<ServiceCliente.Reserva>();
                ServiceCliente.Reserva[] listaSoap = soapClient.listaReservasPorRutService(this.rutMisReservas.Text);
                if (listaSoap!=null)
                {
                    for (int i = 0; i<listaSoap.Length;i++)
                    {
                        listReserva.Add(listaSoap[i]);
                    }
                }
                this.dataGridView1.Columns.Clear();
                this.dataGridView1.Columns.Add("idReserva", "IdReserva");
                this.dataGridView1.Columns.Add("especialidad", "Especialidad");
                this.dataGridView1.Columns.Add("rut", "rut");
                this.dataGridView1.Columns.Add("nombre", "Nombre Paciente");
                this.dataGridView1.Columns.Add("fecha", "Fecha");
                this.dataGridView1.Columns.Add("horaInicio", "Hora Inicio");
                this.dataGridView1.Columns.Add("horaFin", "Hora Fin");
                this.dataGridView1.Columns.Add("terapeuta", "Nombre Terapeuta");
                this.dataGridView1.Columns.Add("estadoReserva", "Estado Reserva");
                this.dataGridView1.Columns["idReserva"].Visible = false;
                if (listReserva == null || listReserva.Count == 0)
                {
                    this.dataGridView1.Rows.Clear();
                    MessageBox.Show(" No se encontraron reservas para ese rut");
                    return;
                }
                foreach (ServiceCliente.Reserva res in listReserva)
                {
                    String nombrePaciente = res.IdPaciente.Nombre + " " + res.IdPaciente.ApellidoPaterno
                        + " " + res.IdPaciente.ApellidoMaterno;
                    String nombreTerapeuta = res.IdHora.IdTerapeuta.IdUsuario.IdPersona.Nombre + " " + res.IdHora.IdTerapeuta.IdUsuario.IdPersona.ApellidoPaterno
                        + " " + res.IdHora.IdTerapeuta.IdUsuario.IdPersona.ApellidoMaterno;
                    String fecha = res.IdHora.Fecha.Day + "/" + res.IdHora.Fecha.Month + "/" + res.IdHora.Fecha.Year;
                    this.dataGridView1.Rows.Add(res.IdReserva, res.IdHora.IdTerapeuta.IdEspecialidad.Nombre,
                        res.IdPaciente.Rut, nombrePaciente, fecha, res.IdHora.IdBloque.HoraInicio, res.IdHora.IdBloque.HoraFin
                        , nombreTerapeuta,res.IdEstado.Descripcion);
                }
                this.dataGridView1.ClearSelection();
                this.btnCancelar.Enabled=true;
                this.btnConfirmar.Enabled = true;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error al guardar: ");
            }
        }

        private void Button3_Click(object sender, EventArgs e)
        {
            try
            {
                this.txtNombre.Text = String.Empty;
                this.txtPaterno.Text = String.Empty;
                this.txtMaterno.Text = String.Empty;
                this.txtCorreo.Text = String.Empty;
                this.txtTelefono.Text = String.Empty;
                this.ButonMasculino.Checked = true;
                this.fechaNacimeinto.Value = DateTime.Now;
                if (this.txtRut.Text.Length==0)
                {
                    MessageBox.Show(" Debe ingresar un rut ");
                    return;
                }
                ServiceCliente.WebServiceClienteSoapClient soapClient = new ServiceCliente.WebServiceClienteSoapClient();
                ServiceCliente.Persona persona = soapClient.buscarPacienteService(this.txtRut.Text);
                if (persona == null)
                {
                    this.txtRut.Text = String.Empty;
                    this.txtNombre.Text=String.Empty;
                    this.txtPaterno.Text = String.Empty;
                    this.txtMaterno.Text = String.Empty;
                    this.txtCorreo.Text = String.Empty;
                    this.txtTelefono.Text = String.Empty;
                    this.ButonMasculino.Checked = true;
                    this.fechaNacimeinto.Value = DateTime.Now;
                    MessageBox.Show(" No se encontro paciente ");
                    return;
                }
                this.txtNombre.Text = persona.Nombre;
                this.txtPaterno.Text = persona.ApellidoPaterno;
                this.txtMaterno.Text = persona.ApellidoMaterno;
                this.txtCorreo.Text = persona.Correo;
                this.txtTelefono.Text = persona.Telefono.ToString();
                if (persona.Sexo.Equals('F'))
                {
                    this.radioFemenino.Checked = true;
                }
                else if (persona.Sexo.Equals('M'))
                {
                    this.ButonMasculino.Checked = true;
                }
                else
                {
                    this.radioIndefinido.Checked = true;
                }
                this.fechaNacimeinto.Value = persona.FechaNacimiento;
            }
            catch(Exception ex)
            {
                this.txtRut.Text = String.Empty;
                this.txtNombre.Text = String.Empty;
                this.txtPaterno.Text = String.Empty;
                this.txtMaterno.Text = String.Empty;
                this.txtCorreo.Text = String.Empty;
                this.txtTelefono.Text = String.Empty;
                this.ButonMasculino.Checked = true;
                this.fechaNacimeinto.Value = DateTime.Now;
                MessageBox.Show(ex.Message," Se produjo un error al buscar el paciente ");
            }
        }

        private void BtnCancelar_Click(object sender, EventArgs e)
        {
            try
            {
                if (this.dataGridView1.SelectedRows.Count == 0)
                {
                    MessageBox.Show("Debe Seleccionar una Reserva");
                    return;
                }
                int idReservaSeleccionada = Convert.ToInt32(this.dataGridView1.SelectedRows[0].Cells[0].Value.ToString());
                ServiceCliente.WebServiceClienteSoapClient soapClient = new ServiceCliente.WebServiceClienteSoapClient();
                ServiceCliente.StatusResponce responce = soapClient.rechazarConfirmarReservaService('0', idReservaSeleccionada);
                if (responce.Estado.Equals("error"))
                {
                    MessageBox.Show(responce.Mensaje,"Error");
                    return;
                }
                MessageBox.Show(responce.Mensaje);
                Button2_Click(sender, e);
                return;
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message , " Error ");
            }
        }

        private void BtnConfirmar_Click(object sender, EventArgs e)
        {
            try
            {
                if (this.dataGridView1.SelectedRows.Count == 0)
                {
                    MessageBox.Show("Debe Seleccionar una Reserva");
                    return;
                }
                int idReservaSeleccionada = Convert.ToInt32(this.dataGridView1.SelectedRows[0].Cells[0].Value.ToString());
                ServiceCliente.WebServiceClienteSoapClient soapClient = new ServiceCliente.WebServiceClienteSoapClient();
                ServiceCliente.StatusResponce responce = soapClient.rechazarConfirmarReservaService('1', idReservaSeleccionada);
                if (responce.Estado.Equals("error"))
                {
                    MessageBox.Show(responce.Mensaje, "Error");
                    return;
                }
                MessageBox.Show(" Reserva Confirmada");
                Button2_Click(sender, e);
                return;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, " Error ");
            }
        }

        private void Label8_Click(object sender, EventArgs e)
        {

        }

        private void TextBox1_TextChanged(object sender, EventArgs e)
        {

        }
    }
}
