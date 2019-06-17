using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace CapaServicioPresentacionWeb.Biocentro.paginas.reservar_hora
{
    public partial class DetalleHora : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["id_hora"] == null || Session["paciente"] == null)
            {
                Response.Redirect("~/Biocentro/paginas/reservar_hora/InicioReserva.aspx");
            }

            if (!IsPostBack)
            {
                int idHoraAtencion = (int)Session["id_hora"];
                ServiceCliente.WebServiceClienteSoapClient soapClient = new ServiceCliente.WebServiceClienteSoapClient();
                ServiceCliente.HoraAtencion horaAtencion = soapClient.buscarDetalleHoraService(idHoraAtencion);

                string fechaHora = horaAtencion.Fecha.ToString("dd/Mm/yyyy ") + " de " + horaAtencion.IdBloque.HoraInicio + ":00 - " +
                                   horaAtencion.IdBloque.HoraFin + ":00";
                this.lblFechaHora.Text = fechaHora;
                string lugar = horaAtencion.Sala.Nombre + " Miguel Claro 195, oficina 610, Providencia";
                this.lblLugar.Text = lugar;
                string especialidad = horaAtencion.EspecialidadClinica.Nombre;
                this.lblEspecialidad.Text = especialidad;
                string terapeuta = horaAtencion.Terapeuta.Nombre + " " + horaAtencion.Terapeuta.ApellidoPaterno +
                                   " " + horaAtencion.Terapeuta.ApellidoMaterno;
                this.lblTerapeuta.Text = terapeuta;
                ServiceCliente.Paciente paciente = (ServiceCliente.Paciente)Session["paciente"];
                this.lblIdHora.Text = "Reserva N° " + idHoraAtencion.ToString();
                this.lblPaciente.Text = paciente.Nombre + " " + paciente.ApellidoPaterno + " " + paciente.ApellidoMaterno;
                this.lblEmail.Text = "Se ha enviado un correo a " + paciente.Correo + " con los detalles de la reserva";
            }
        }
        
        protected void btnMisHorasOnclick(object sender, EventArgs e)
        {
            try
            {
                Response.Write("<script language='javascript'>window.location='InicioReserva.aspx';</script>");
            }
            catch (Exception ex)
            {
                ShowMessage("Ocurrió un error al cargar la pagina");
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
    }
}