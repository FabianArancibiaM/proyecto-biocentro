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
        protected void Page_Load(object sender, EventArgs e)
        {
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
                ShowMessage("Se produjo un error al cargar la pagina");
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
                    }
                    else
                    {
                        ShowMessage(registrarPaciente.Mensaje);
                    }
                }
            }
            catch(Exception ex)
            {
                ShowMessage("Ocurrió un error al intentar guardar");
            }
        }

        public void ShowMessage(string message)
        {
            System.Text.StringBuilder sb = new System.Text.StringBuilder();
            sb.Append("<script type = 'text/javascript'> ");
            sb.Append("window.onload=function(){");
            sb.Append("alert('");
            sb.Append(message);
            sb.Append("')};");
            sb.Append("</script>");
            ClientScript.RegisterClientScriptBlock(this.GetType(), "alert", sb.ToString());
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
        public bool validarRut(string rut)
        {
            bool validacion = false;
            try
            {
                rut = rut.ToUpper();
                rut = rut.Replace(".", "");
                rut = rut.Replace("-", "");
                int rutAux = int.Parse(rut.Substring(0, rut.Length - 1));

                char dv = char.Parse(rut.Substring(rut.Length - 1, 1));

                int m = 0, s = 1;
                for (; rutAux != 0; rutAux /= 10)
                {
                    s = (s + rutAux % 10 * (9 - m++ % 6)) % 11;
                }
                if (dv == (char)(s != 0 ? s + 47 : 75))
                {
                    validacion = true;
                }
            }
            catch (Exception)
            {
            }
            return validacion;
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
                ShowMessage("Error: Ingrese un RUT");
                return false;
            }
            if (this.txtNombre.Text.Trim().Length == 0)
            {
                ShowMessage("Error: Ingrese un Nombre");
                return false;
            }
            if (this.txtApPaterno.Text.Trim().Length == 0)
            {
                ShowMessage("Error: Ingrese su Apellido Paterno");
                return false;
            }
            if (this.txtApMaterno.Text.Trim().Length == 0)
            {
                ShowMessage("Error: Ingrese su Apellido Materno");
                return false;
            }
            if (Convert.ToDateTime(this.fechaNac.Text) >= DateTime.Now)
            {
                ShowMessage("Error: Seleccione una fecha de Nacimiento válida");
                return false;
            }
            if (this.txtCorreo.Text.Trim().Length == 0)
            {
                ShowMessage("Error: Ingrese su Correo");
                return false;
            }
            if (this.txtTelefono.Text.Trim().Length == 0)
            {
                ShowMessage("Error: Ingrese su número de teléfono");
                return false;
            }
            if (!radioHombre.Checked && !radioMujer.Checked && !radioOtro.Checked)
            {
                ShowMessage("Error: Seleccione su Sexo");
                return false;
            }
            if (!validarRut(this.txtRut.Text))
            {
                ShowMessage("Error: El RUT ingresado es inválido");
                return false;
            }
            if (!ComprobarFormatoEmail(this.txtCorreo.Text))
            {
                ShowMessage("Error: El Correo ingresado  es inválido");
                return false;
            }
            if (!ValidarTelefono(this.txtTelefono.Text))
            {
                ShowMessage("Error: El Número de Teléfono ingresado es inválido");
                return false;
            }
            return true;
        }
    }
}