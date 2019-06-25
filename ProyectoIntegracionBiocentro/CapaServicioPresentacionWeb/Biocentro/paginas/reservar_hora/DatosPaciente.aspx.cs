using CapaServicioPresentacionWeb.Biocentro.paginas.helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace CapaServicioPresentacionWeb.Biocentro.paginas.reservar_hora
{
    public partial class DatosPaciente : System.Web.UI.Page
    {
        Commons commons;
        protected void Page_Load(object sender, EventArgs e)
        {
            commons = new Commons(Page);

            try
            {
                if (Session["rut"] == null || Session["id_hora"] == null)
                {
                    Response.Redirect("~/Biocentro/paginas/reservar_hora/InicioReserva.aspx");
                }

                if (!IsPostBack)
                {
                    string rutPaciente = (string)Session["rut"];

                    ServiceCliente.WebServiceClienteSoapClient soapClient = new ServiceCliente.WebServiceClienteSoapClient();
                    ServiceCliente.Paciente paciente = soapClient.buscarPacienteService(commons.formatearRut(rutPaciente).Replace(".",""));
                    this.txtRut.Text = commons.formatearRut(rutPaciente);

                    if (paciente != null)
                    {
                        Session["paciente"] = paciente;
                        this.txtNombre.Text = paciente.Nombre;
                        this.txtApPaterno.Text = paciente.ApellidoPaterno;
                        this.txtApMaterno.Text = paciente.ApellidoMaterno;
                        this.fechaNac.Text = paciente.FechaNacimiento.ToString("yyyy-MM-dd");
                        if (paciente.Sexo == 'M')
                        {
                            this.radioMujer.Checked = true;
                        }
                        if (paciente.Sexo == 'H')
                        {
                            this.radioHombre.Checked = true;
                        }
                        if (paciente.Sexo == 'O')
                        {
                            this.radioOtro.Checked = true;
                        }
                        this.txtCorreo.Text = paciente.Correo;
                        this.txtTelefono.Text = paciente.Telefono;
                    }
                }
            }
            catch(Exception ex)
            {
                commons.ShowMessage("Error", "Se produjo un error al cargar la pagina", "error");
            }
        }
        protected void btnReservar_Click(object sender, EventArgs e)
        {
            try
            {
                ServiceCliente.Paciente paciente = new ServiceCliente.Paciente();
                if (Session["paciente"] != null)
                {
                    paciente = (ServiceCliente.Paciente)Session["paciente"];
                }

                if (ValidarDatosPaciente())
                {
                    paciente.Rut = this.txtRut.Text.Replace(".", "");
                    paciente.Nombre = this.txtNombre.Text;
                    paciente.ApellidoPaterno = this.txtApPaterno.Text;
                    paciente.ApellidoMaterno = this.txtApMaterno.Text;
                    paciente.FechaNacimiento = Convert.ToDateTime(this.fechaNac.Text);
                    if (this.radioHombre.Checked == true)
                    {
                        paciente.Sexo = 'H';
                    }
                    if (this.radioMujer.Checked == true)
                    {
                        paciente.Sexo = 'M';
                    }
                    if (this.radioOtro.Checked == true)
                    {
                        paciente.Sexo = 'O';
                    }
                    paciente.Correo = this.txtCorreo.Text;
                    paciente.Telefono = this.txtTelefono.Text;

                    int idHoraAtencion = (int)Session["id_hora"];

                    ServiceCliente.WebServiceClienteSoapClient soapClient = new ServiceCliente.WebServiceClienteSoapClient();
                    ServiceCliente.StatusResponce registrarPaciente = soapClient.registrarPacienteService(paciente, idHoraAtencion);

                    if(registrarPaciente.Estado == "ok")
                    {
                        Session["paciente"] = paciente;
                        Response.Write("<script language='javascript'>window.location='DetalleHora.aspx';</script>");
                        return;
                    }
                    else
                    {
                        commons.ShowMessage("Error", "Ocurrió un error al reservar la hora", "error");
                    }
                }
            }
            catch(Exception ex)
            {
                commons.ShowMessage("Error", "Ocurrió un error al reservar la hora", "error");
            }
        }
        
        public bool ValidarDatosPaciente()
        {
            if (string.IsNullOrEmpty(this.txtRut.Text.Trim()))
            {
                commons.ShowMessage("Error", "Ingrese su RUT", "error");
                return false;
            }
            if (string.IsNullOrEmpty(this.txtNombre.Text.Trim()))
            {
                commons.ShowMessage("Error", "Ingrese su Nombre", "error");
                return false;
            }
            if (string.IsNullOrEmpty(this.txtApPaterno.Text.Trim()))
            {
                commons.ShowMessage("Error", "Ingrese su Apellido Paterno", "error");
                return false;
            }
            if (string.IsNullOrEmpty(this.txtApMaterno.Text.Trim()))
            {
                commons.ShowMessage("Error", "Ingrese su Apellido Materno", "error");
                return false;
            }
            if (string.IsNullOrEmpty(this.fechaNac.Text.Trim()))
            {
                commons.ShowMessage("Error", "Seleccione una fecha de Nacimiento", "error");
                return false;
            }
            if (Convert.ToDateTime(this.fechaNac.Text) >= DateTime.Now)
            {
                commons.ShowMessage("Error", "Seleccione una fecha de Nacimiento válida", "error");
                return false;
            }
            if (string.IsNullOrEmpty(this.txtCorreo.Text.Trim()))
            {
                commons.ShowMessage("Error", "Ingrese su Correo electrónico", "error");
                return false;
            }
            if (string.IsNullOrEmpty(this.txtTelefono.Text.Trim()))
            {
                commons.ShowMessage("Error", "Ingrese su número de teléfono", "error");
                return false;
            }
            if (!radioHombre.Checked && !radioMujer.Checked && !radioOtro.Checked)
            {
                commons.ShowMessage("Error", "Seleccione su Sexo", "error");
                return false;
            }
            if (!commons.validarRut(this.txtRut.Text))
            {
                commons.ShowMessage("Error", "El RUT ingresado es inválido", "error");
                return false;
            }
            if (!commons.ComprobarFormatoEmail(this.txtCorreo.Text))
            {
                commons.ShowMessage("Error", "El Correo ingresado  es inválido", "error");
                return false;
            }
            if (!commons.ValidarTelefono(this.txtTelefono.Text))
            {
                commons.ShowMessage("Error", "El Número de Teléfono ingresado es inválido", "error");
                return false;
            }
            return true;
        }
    }
}