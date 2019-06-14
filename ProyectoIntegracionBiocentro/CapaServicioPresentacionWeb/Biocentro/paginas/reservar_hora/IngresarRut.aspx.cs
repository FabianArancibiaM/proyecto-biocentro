using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace CapaServicioPresentacionWeb.Biocentro.paginas.reservar_hora
{
    public partial class IngresarRut : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            //Si no se ha envido ningun valor redirecciona a la página de inicio
            if (Session["id_hora"] == null)
            {
                Response.Redirect("~/Biocentro/paginas/reservar_hora/InicioReserva.aspx");
            }
            if (!IsPostBack)
            {
                //Recupera el valor enviado desde la página anterior
                int idHoraAtencion = (int)Session["id_hora"];
                ServiceCliente.WebServiceClienteSoapClient soapClient = new ServiceCliente.WebServiceClienteSoapClient();
                ServiceCliente.HoraAtencion horaAtencion = soapClient.buscarDetalleHoraService(idHoraAtencion);

                string fechaHora = horaAtencion.Fecha.ToString("dd/M/yyyy ") + " " + horaAtencion.IdBloque.HoraInicio + ":00 - " + 
                                   horaAtencion.IdBloque.HoraFin + ":00";
                this.lblFechaHora.Text = fechaHora;
                string lugar = horaAtencion.Sala.Nombre + " Av. Valenzuela Castillo,Ñuñoa";
                this.lblLugar.Text = lugar;
                string especialidad = horaAtencion.EspecialidadClinica.Nombre;
                this.lblEspecialidad.Text = especialidad;
                string terapeuta = horaAtencion.Terapeuta.Nombre + " " + horaAtencion.Terapeuta.ApellidoPaterno +
                                   " " + horaAtencion.Terapeuta.ApellidoMaterno;
                this.lblTerapeuta.Text = terapeuta;
            }
        }

        protected void btn_buscar_rut_Click(object sender, EventArgs e)
        {
            string rut = this.txtRut.Text;
            Session["rut"] = rut;
            Response.Write("<script language='javascript'>window.location='DatosPaciente.aspx';</script>");   
        }
    }
}