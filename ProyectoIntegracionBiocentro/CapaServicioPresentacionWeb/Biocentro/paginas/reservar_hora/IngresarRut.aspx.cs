using CapaServicioPresentacionWeb.Biocentro.paginas.helpers;
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
        Commons commons;
        protected void Page_Load(object sender, EventArgs e)
        {
            commons = new Commons(Page);

            try
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

                    string fechaHora = horaAtencion.Fecha.ToString("dd/MM/yyyy ") + " de " + horaAtencion.IdBloque.HoraInicio + ":00 - " +
                                       horaAtencion.IdBloque.HoraFin + ":00";
                    this.lblFechaHora.Text = fechaHora;
                    string lugar = horaAtencion.Sala.Nombre + ", Miguel Claro 195, Providencia";
                    this.lblLugar.Text = lugar;
                    string especialidad = horaAtencion.EspecialidadClinica.Nombre;
                    this.lblEspecialidad.Text = especialidad;
                    string terapeuta = horaAtencion.Terapeuta.Nombre + " " + horaAtencion.Terapeuta.ApellidoPaterno +
                                       " " + horaAtencion.Terapeuta.ApellidoMaterno;
                    this.lblTerapeuta.Text = terapeuta;
                }
            }
            catch(Exception ex)
            {
                commons.ShowMessage("Ocurrio un error al cargar la pagina");
            }
        }

        protected void btnVolver(object sender, EventArgs e)
        {
            try
            {
                Response.Write("<script language='javascript'>window.location='HorasDisponibles.aspx';</script>");
            }
            catch (Exception ex)
            {
                commons.ShowMessage("Ocurrio un error al cargar la pagina");
            }
        }

        protected void btn_buscar_rut_Click(object sender, EventArgs e)
        {
            try
            {
                if (this.txtRut.Text.Trim().Length == 0)
                {
                    commons.ShowMessage("Error: Ingrese un rut");
                    return;
                }
                if (!commons.validarRut(this.txtRut.Text))
                {
                    commons.ShowMessage("Error: el RUT ingresado inválido");
                    return;
                }
                string rut = this.txtRut.Text;
                Session["rut"] = rut;
                Response.Write("<script language='javascript'>window.location='DatosPaciente.aspx';</script>");
            }
            catch (Exception ex)
            {
                commons.ShowMessage("Error al intentar cargar la pagina");
            }
        }
    }
}