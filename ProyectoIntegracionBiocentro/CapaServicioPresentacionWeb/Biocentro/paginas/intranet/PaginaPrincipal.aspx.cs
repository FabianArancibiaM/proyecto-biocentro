using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace CapaServicioPresentacionWeb.Biocentro.paginas.intranet
{
    public partial class PaginaPrincipal : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                if (Session["empleado"] == null)
                {
                    Response.Write("<script language='javascript'>window.location='Login.aspx';</script>");
                }
                else
                {
                    ServiceCliente.Empleado empleado = (ServiceCliente.Empleado)Session["empleado"];
                    this.msjBienvenida.Text = "Bienvenido/a " + empleado.Nombre + " " + empleado.ApellidoPaterno + " " + empleado.ApellidoMaterno;
                }
            }
            catch(Exception ex)
            {
                this.msjBienvenida.Text = "Bienvenido/a ";
            }
            
        }

        protected void btnRegistrarPago_Click(object sender, EventArgs e)
        {
            try
            {
                Response.Write("<script language='javascript'>window.location='RegistrarPago.aspx';</script>");
            }
            catch(Exception ex)
            {
                ShowMessage("No se encontro la pagina solicitada");
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