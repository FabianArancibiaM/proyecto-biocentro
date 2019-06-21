using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace CapaServicioPresentacionWeb.Biocentro.paginas.reservar_hora
{
    public partial class MisHoras : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void btnBuscar_Click(object sender, EventArgs e)
        {
            try
            {
                if(string.IsNullOrEmpty(this.txtRut.Text))
                {
                    ShowMessage("Debe ingresar SU RUT");
                    return;
                }
                if(string.IsNullOrEmpty(this.txtEmail.Text))
                {
                    ShowMessage("Debe ingresar su correo");
                    return;
                }
                ServiceCliente.WebServiceClienteSoapClient soapClient = new ServiceCliente.WebServiceClienteSoapClient();
                List<ServiceCliente.HoraAtencion> misHoras = new List<ServiceCliente.HoraAtencion>();
                ServiceCliente.HoraAtencion[] listaHoras = soapClient.listaReservasPorRutAndCorreoService(txtRut.Text, txtEmail.Text);
                if(listaHoras == null || listaHoras.Length == 0)
                {
                    ShowMessage("No tiene horas asociadas");
                    return;
                }
                misHoras.AddRange(listaHoras);
                if (misHoras == null || misHoras.Count == 0)
                {
                    ShowMessage("No tiene horas asociadas");
                    return;
                }
                Session["misHoras"] = misHoras;
                Response.Write("<script language='javascript'>window.location='MisHorasDetalle.aspx';</script>");
            }
            catch (Exception ex)
            {
                ShowMessage("Error inesperado al buscar sus horas. Inténtelo nuevamente");
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