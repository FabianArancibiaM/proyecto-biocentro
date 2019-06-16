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
                if (this.txtRut.Text.Length == 0)
                {
                    ShowMessage("Debe ingresar el Usuario");
                    return;
                }
                if (this.txtEmail.Text.Length == 0)
                {
                    ShowMessage("Debe ingresar la Contraseña");
                    return;
                }
                ServiceCliente.WebServiceClienteSoapClient soapClient = new ServiceCliente.WebServiceClienteSoapClient();
                List<ServiceCliente.HoraAtencion> misHoras = new List<ServiceCliente.HoraAtencion>();
                misHoras.AddRange(soapClient.listaReservasPorRutAndCorreoService(txtRut.Text, txtEmail.Text));
                if (misHoras != null)
                {
                    Session["misHoras"] = misHoras;
                    Response.Write("<script language='javascript'>window.location='MisHorasDetalle.aspx';</script>");
                }
                ShowMessage("Rut o Email Incorrectos");

            }
            catch (Exception)
            {

                throw;
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