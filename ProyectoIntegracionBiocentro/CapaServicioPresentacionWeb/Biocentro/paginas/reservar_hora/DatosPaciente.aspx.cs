using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace CapaServicioPresentacionWeb.Biocentro.paginas.reservar_hora
{
    public partial class DatosPaciente : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
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
                    this.fechaNac.Text = paciente.FechaNacimiento.ToString();
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
        protected void btnReservar_Click(object sender, EventArgs e)
        {
            ServiceCliente.Paciente paciente = new ServiceCliente.Paciente();
            if (Session["paciente"] != null)
            {
                paciente = (ServiceCliente.Paciente)Session["paciente"];
            }

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
            ShowMessage(registrarPaciente.Mensaje);
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
    }
}