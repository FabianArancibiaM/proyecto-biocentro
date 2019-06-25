using CapaServicioPresentacionWeb.Biocentro.paginas.helpers;
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
        Commons commons;
        protected void Page_Load(object sender, EventArgs e)
        {
            commons = new Commons(Page);

            if (Session["id_hora"] == null || Session["paciente"] == null)
            {
                Response.Redirect("~/Biocentro/paginas/reservar_hora/InicioReserva.aspx");
            }

            if (!IsPostBack)
            {
                int idHoraAtencion = (int)Session["id_hora"];
                ServiceCliente.WebServiceClienteSoapClient soapClient = new ServiceCliente.WebServiceClienteSoapClient();
                ServiceCliente.HoraAtencion horaAtencion = soapClient.buscarDetalleHoraService(idHoraAtencion);

                string fechaHora = horaAtencion.Fecha.ToString("dd/MM/yyyy ") + " de " + horaAtencion.IdBloque.HoraInicio + ":00 - " +
                                   horaAtencion.IdBloque.HoraFin + ":00";
                this.lblFechaHora.Text = fechaHora;
                this.lblLugar.Text = horaAtencion.Sala.Nombre + ", Miguel Claro 195, Providencia";
                this.lblEspecialidad.Text = horaAtencion.EspecialidadClinica.Nombre;
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
                Response.Write("<script language='javascript'>window.location='MisHoras.aspx';</script>");
            }
            catch (Exception ex)
            {
                commons.ShowMessage("Error", "Ocurrió un error al cargar la página", "error");
            }
        }
    }
}