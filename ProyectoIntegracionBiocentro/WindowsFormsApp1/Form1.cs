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
            cargarMedioPago();
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
                ServiceCliente.Paciente persona = new ServiceCliente.Paciente();
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
                List<ServiceCliente.EspecialidadClinica> listaEspecialidad = new List<ServiceCliente.EspecialidadClinica>();
                listaEspecialidad.AddRange(soapClient.generarListaEspecialidadService());
                if (listaEspecialidad == null)
                {
                    MessageBox.Show("No se encontraron datos");
                    return;
                }
                foreach (ServiceCliente.EspecialidadClinica var in listaEspecialidad)
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
                List<ServiceCliente.EspecialidadTerapeuta> listaEspecialidad = new List<ServiceCliente.EspecialidadTerapeuta>();
                listaEspecialidad.AddRange(soapClient.generarListaEspecialistaService());
                if (listaEspecialidad == null)
                {
                    MessageBox.Show("No se encontraron datos");
                    return;
                }
                foreach (ServiceCliente.EspecialidadTerapeuta var in listaEspecialidad)
                {
                    this.cmbTerapeuta.Items.Add(var);
                }
                this.cmbTerapeuta.DisplayMember = "var.empleado.nombre";
                this.cmbTerapeuta.SelectedIndex = 0;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message,"Error al guardar: ");
            }
        }
        private void cargarMedioPago()
        {
            try
            {
                this.comboMedioPago.Items.Add("Seleccionar");
                ServiceCliente.WebServiceClienteSoapClient soapClient = new ServiceCliente.WebServiceClienteSoapClient();
                List<ServiceCliente.MedioPago> listaMedioPago = new List<ServiceCliente.MedioPago>();
                listaMedioPago.AddRange(soapClient.generarListaMedioPagoService());
                if (listaMedioPago == null)
                {
                    MessageBox.Show("No se encontraron datos");
                    return;
                }
                foreach (ServiceCliente.MedioPago var in listaMedioPago)
                {
                    this.comboMedioPago.Items.Add(var);
                }
                this.comboMedioPago.DisplayMember = "nombre";
                this.comboMedioPago.SelectedIndex = 0;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error al guardar: ");
            }
        }

        private void BtnBuscar_Click(object sender, EventArgs e)
        {
            try
            {
                ServiceCliente.Empleado persona = null;
                if (this.cmbTerapeuta.SelectedIndex>0)
                {
                    persona = (ServiceCliente.Empleado)this.cmbTerapeuta.SelectedItem;
                }
                ServiceCliente.EspecialidadClinica especialidad =null;
                if (this.selectEspecialidad.SelectedIndex>0)
                {
                    especialidad = (ServiceCliente.EspecialidadClinica)this.selectEspecialidad.SelectedItem;
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
                this.dataGridView1.Columns.Add("nombre", "Nombre Especialista");
                this.dataGridView1.Columns.Add("horaInicio", "Hora Inicio");
                this.dataGridView1.Columns.Add("horaFin", "Hora Fin");
                this.dataGridView1.Columns.Add("sala", "Sala");
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
                    String nombreCompleto = hora.Terapeuta.Nombre + " " + hora.Terapeuta.ApellidoPaterno 
                        + " " + hora.Terapeuta.ApellidoMaterno;
                    this.dataGridView1.Rows.Add(hora.IdHora, hora.EspecialidadClinica.Nombre, 
                         nombreCompleto, hora.IdBloque.HoraInicio, hora.IdBloque.HoraFin, hora.Sala.Nombre);
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
                if (String.IsNullOrEmpty(this.textBox2.Text))
                {
                    MessageBox.Show(" Debe ingresar un correo");
                    return;
                }
                ServiceCliente.WebServiceClienteSoapClient soapClient = new ServiceCliente.WebServiceClienteSoapClient();
                List<ServiceCliente.HoraAtencion> listReserva = new List<ServiceCliente.HoraAtencion>();
                ServiceCliente.HoraAtencion[] listaSoap = soapClient.listaReservasPorRutAndCorreoService(this.rutMisReservas.Text, this.textBox2.Text);
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
                this.dataGridView1.Columns.Add("monto", "Valor Consulta");
                this.dataGridView1.Columns["idReserva"].Visible = false;
                if (listReserva == null || listReserva.Count == 0)
                {
                    this.dataGridView1.Rows.Clear();
                    MessageBox.Show(" No se encontraron reservas del paciente consultado, vuelva a ingresar los datos");
                    return;
                }
                foreach (ServiceCliente.HoraAtencion res in listReserva)
                {
                    String nombrePaciente = res.Paciente.Nombre + " " + res.Paciente.ApellidoPaterno
                        + " " + res.Paciente.ApellidoMaterno;
                    String nombreTerapeuta = res.Terapeuta.Nombre + " " + res.Terapeuta.ApellidoPaterno
                        + " " + res.Terapeuta.ApellidoMaterno;
                    String fecha = res.Fecha.Day + "/" + res.Fecha.Month + "/" + res.Fecha.Year;
                    this.dataGridView1.Rows.Add(res.IdHora, res.EspecialidadClinica.Nombre,
                        res.Paciente.Rut, nombrePaciente, fecha, res.IdBloque.HoraInicio, res.IdBloque.HoraFin
                        , nombreTerapeuta,res.EstadoReserva.Descripcion, res.EspecialidadClinica.Precio);
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
                ServiceCliente.Paciente paciente = soapClient.buscarPacienteService(this.txtRut.Text);
                if (paciente == null)
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
                this.txtNombre.Text = paciente.Nombre;
                this.txtPaterno.Text = paciente.ApellidoPaterno;
                this.txtMaterno.Text = paciente.ApellidoMaterno;
                this.txtCorreo.Text = paciente.Correo;
                this.txtTelefono.Text = paciente.Telefono.ToString();
                if (paciente.Sexo.Equals('F'))
                {
                    this.radioFemenino.Checked = true;
                }
                else if (paciente.Sexo.Equals('M'))
                {
                    this.ButonMasculino.Checked = true;
                }
                else
                {
                    this.radioIndefinido.Checked = true;
                }
                this.fechaNacimeinto.Value = paciente.FechaNacimiento;
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

        private void SelectEspecialidad_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void cambiarPropiedadTabla(object sender, EventArgs e)
        {
            if (this.comboMedioPago.SelectedIndex>0)
            {
                this.dataGridView1.MultiSelect = true;
            }
            else
            {
                this.dataGridView1.MultiSelect = false;
            }
            this.textBox1.Text = Convert.ToString(0);
        }

        private void detectarSeleccion(object sender, DataGridViewCellMouseEventArgs e)
        {
            
        }

        private void DataGridView1_RowStateChanged(object sender, DataGridViewRowStateChangedEventArgs e)
        {
            if (this.comboMedioPago.SelectedIndex > 0)
            {
                int total = 0;
                for (int i = 0; i < this.dataGridView1.SelectedRows.Count; i++)
                {
                    total = total + Convert.ToInt32(this.dataGridView1.SelectedRows[i].Cells["monto"].Value.ToString());
                }
                this.textBox1.Text = Convert.ToString(total);
            }
        }

        private void Button4_Click(object sender, EventArgs e)
        {
            try
            {
                if (this.dataGridView1.SelectedRows.Count == 0)
                {
                    MessageBox.Show("Debe Seleccionar una Hora");
                    return;
                }
                int idReservaSeleccionada = Convert.ToInt32(this.dataGridView1.SelectedRows[0].Cells[0].Value.ToString());
                int total = 0;
                WindowsFormsApp1.ServiceCliente.ArrayOfInt listaIdHora = new ServiceCliente.ArrayOfInt();
                for (int i = 0; i < this.dataGridView1.SelectedRows.Count; i++)
                {
                    total = total + Convert.ToInt32(this.dataGridView1.SelectedRows[i].Cells["monto"].Value.ToString());
                    listaIdHora.Add(Convert.ToInt32(this.dataGridView1.SelectedRows[i].Cells["idReserva"].Value.ToString()));
                }
                ServiceCliente.Venta venta = new ServiceCliente.Venta();
                venta.FechaPago = DateTime.Now;
                venta.MedioPago = (ServiceCliente.MedioPago)this.comboMedioPago.SelectedItem;
                venta.Monto = total;
                ServiceCliente.EstadoVenta esVenta = new ServiceCliente.EstadoVenta();
                esVenta.IdEstadoVenta = 2;
                venta.EstadoVenta = esVenta;
                ServiceCliente.WebServiceClienteSoapClient soapClient = new ServiceCliente.WebServiceClienteSoapClient();
                ServiceCliente.StatusResponce responce = soapClient.guardarVentaRealizadaService(venta, listaIdHora);
                if (responce.Estado.Equals("error"))
                {
                    MessageBox.Show(responce.Mensaje, "Error");
                    return;
                }
                MessageBox.Show(responce.Mensaje);
                llenarGridParaVenta();
                return;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, " Error ");
            }
        }

        private void llenarGridParaVenta()
        {
            try
            {
                ServiceCliente.WebServiceClienteSoapClient soapClient = new ServiceCliente.WebServiceClienteSoapClient();
                List<ServiceCliente.HoraAtencion> listReserva = new List<ServiceCliente.HoraAtencion>();
                ServiceCliente.HoraAtencion[] listaSoap = soapClient.horasPorRutPacienteMasVentaService(this.rutMisReservas.Text);
                if (listaSoap != null)
                {
                    for (int i = 0; i < listaSoap.Length; i++)
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
                this.dataGridView1.Columns.Add("monto", "Valor Consulta");
                this.dataGridView1.Columns.Add("estadoPago", "Estago Pago");
                this.dataGridView1.Columns.Add("formaPago", "Forma Pago");
                this.dataGridView1.Columns.Add("idVenta", "IdVenta");
                this.dataGridView1.Columns["idReserva"].Visible = false;
                this.dataGridView1.Columns["idVenta"].Visible = false;
                if (listReserva == null || listReserva.Count == 0)
                {
                    this.dataGridView1.Rows.Clear();
                    MessageBox.Show(" No se encontraron reservas para ese rut");
                    return;
                }
                foreach (ServiceCliente.HoraAtencion res in listReserva)
                {
                    String nombrePaciente = res.Paciente.Nombre + " " + res.Paciente.ApellidoPaterno
                        + " " + res.Paciente.ApellidoMaterno;
                    String nombreTerapeuta = res.Terapeuta.Nombre + " " + res.Terapeuta.ApellidoPaterno
                        + " " + res.Terapeuta.ApellidoMaterno;
                    String fecha = res.Fecha.Day + "/" + res.Fecha.Month + "/" + res.Fecha.Year;
                    this.dataGridView1.Rows.Add(res.IdHora, res.EspecialidadClinica.Nombre,
                        res.Paciente.Rut, nombrePaciente, fecha, res.IdBloque.HoraInicio, res.IdBloque.HoraFin
                        , nombreTerapeuta, res.EstadoReserva.Descripcion, res.EspecialidadClinica.Precio
                        , res.Venta!=null ? res.Venta.EstadoVenta.Descripcion: null
                        , res.Venta != null ? res.Venta.MedioPago.Nombre : null
                        , res.Venta != null ? res.Venta.IdVenta.ToString() :null);
                }
                this.dataGridView1.ClearSelection();
                this.btnCancelar.Enabled = true;
                this.btnConfirmar.Enabled = true;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, " Error ");
            }
        }

        private void Button5_Click(object sender, EventArgs e)
        {
            llenarGridParaVenta();
        }

        private void TextBox2_TextChanged(object sender, EventArgs e)
        {

        }
    }
}
