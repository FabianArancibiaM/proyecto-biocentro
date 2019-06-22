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
                    ServiceCliente.Paciente paciente = soapClient.buscarPacienteService(rutPaciente);
                    this.txtRut.Text = rutPaciente;

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
                        this.txtCorreo.Text = paciente.Correo;
                        this.txtTelefono.Text = paciente.Telefono;
                    }
                }
            }
            catch(Exception ex)
            {
                commons.ShowMessage("Se produjo un error al cargar la pagina");
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
                    paciente.Rut = this.txtRut.Text;
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
                    paciente.Correo = this.txtCorreo.Text;
                    paciente.Telefono = this.txtTelefono.Text;

                    int idHoraAtencion = (int)Session["id_hora"];

                    ServiceCliente.WebServiceClienteSoapClient soapClient = new ServiceCliente.WebServiceClienteSoapClient();
                    ServiceCliente.StatusResponce registrarPaciente = soapClient.registrarPacienteService(paciente, idHoraAtencion);

                    if(registrarPaciente.Estado == "ok")
                    {
                        Session["paciente"] = paciente;
                        Response.Write("<script language='javascript'>window.location='DetalleHora.aspx';</script>");
                    }
                    else
                    {
                        commons.ShowMessage(registrarPaciente.Mensaje);
                    }
                }
            }
            catch(Exception ex)
            {
                commons.ShowMessage("Ocurrió un error al intentar guardar");
            }
        }

        public static bool ValidarTelefono(string strNumber)
        {
            Regex regex = new Regex("^[0-9]{7,10}|(\\S[0-9]{3})[0-9]{7,10}");
            Match match = regex.Match(strNumber);

            if (match.Success)
                return true;
            else
                return false;
        }
        public static bool ComprobarFormatoEmail(string sEmailAComprobar)
        {
            String sFormato;
            sFormato = "\\w+([-+.']\\w+)*@\\w+([-.]\\w+)*\\.\\w+([-.]\\w+)*";
            if (Regex.IsMatch(sEmailAComprobar, sFormato))
            {
                if (Regex.Replace(sEmailAComprobar, sFormato, String.Empty).Length == 0)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            else
            {
                return false;
            }
        }

        public bool ValidarDatosPaciente()
        {
            if (this.txtRut.Text.Trim().Length == 0)
            {
                commons.ShowMessage("Error: Ingrese un RUT");
                return false;
            }
            if (this.txtNombre.Text.Trim().Length == 0)
            {
                commons.ShowMessage("Error: Ingrese un Nombre");
                return false;
            }
            if (this.txtApPaterno.Text.Trim().Length == 0)
            {
                commons.ShowMessage("Error: Ingrese su Apellido Paterno");
                return false;
            }
            if (this.txtApMaterno.Text.Trim().Length == 0)
            {
                commons.ShowMessage("Error: Ingrese su Apellido Materno");
                return false;
            }
            if (Convert.ToDateTime(this.fechaNac.Text) >= DateTime.Now)
            {
                commons.ShowMessage("Error: Seleccione una fecha de Nacimiento válida");
                return false;
            }
            if (this.txtCorreo.Text.Trim().Length == 0)
            {
                commons.ShowMessage("Error: Ingrese su Correo");
                return false;
            }
            if (this.txtTelefono.Text.Trim().Length == 0)
            {
                commons.ShowMessage("Error: Ingrese su número de teléfono");
                return false;
            }
            if (!radioHombre.Checked && !radioMujer.Checked)
            {
                commons.ShowMessage("Error: Seleccione su Sexo");
                return false;
            }
            if (!commons.validarRut(this.txtRut.Text))
            {
                commons.ShowMessage("Error: El RUT ingresado es inválido");
                return false;
            }
            if (!ComprobarFormatoEmail(this.txtCorreo.Text))
            {
                commons.ShowMessage("Error: El Correo ingresado  es inválido");
                return false;
            }
            if (!ValidarTelefono(this.txtTelefono.Text))
            {
                commons.ShowMessage("Error: El Número de Teléfono ingresado es inválido");
                return false;
            }
            return true;
        }
    }
}