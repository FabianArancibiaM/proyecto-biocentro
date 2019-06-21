using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using CapaServicioPresentacionWeb.Biocentro.paginas.helpers;

namespace CapaServicioPresentacionWeb.Biocentro.paginas.reservar_hora
{
    public partial class MisHoras : System.Web.UI.Page
    {
        Commons commons;
        protected void Page_Load(object sender, EventArgs e)
        {
            commons = new Commons(Page);
        }

        protected void btnBuscar_Click(object sender, EventArgs e)
        {
            try
            {
                if(string.IsNullOrEmpty(this.txtRut.Text))
                {
                    commons.ShowMessage("Debe ingresar SU RUT");
                    return;
                }
                if(string.IsNullOrEmpty(this.txtEmail.Text))
                {
                    commons.ShowMessage("Debe ingresar su correo");
                    return;
                }
                ServiceCliente.WebServiceClienteSoapClient soapClient = new ServiceCliente.WebServiceClienteSoapClient();
                List<ServiceCliente.HoraAtencion> misHoras = new List<ServiceCliente.HoraAtencion>();
                ServiceCliente.HoraAtencion[] listaHoras = soapClient.listaReservasPorRutAndCorreoService(txtRut.Text, txtEmail.Text);
                if(listaHoras == null || listaHoras.Length == 0)
                {
                    commons.ShowMessage("No tiene horas asociadas");
                    return;
                }
                misHoras.AddRange(listaHoras);
                if (misHoras == null || misHoras.Count == 0)
                {
                    commons.ShowMessage("No tiene horas asociadas");
                    return;
                }
                Session["misHoras"] = misHoras;
                Response.Write("<script language='javascript'>window.location='MisHorasDetalle.aspx';</script>");
            }
            catch (Exception ex)
            {
                commons.ShowMessage("Error inesperado al buscar sus horas. Inténtelo nuevamente");
            }
        }
    }
}