using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace CapaServicioPresentacionWeb.Biocentro.paginas.intranet
{
    public partial class Login : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["empleado"] != null)
            {
                Response.Write("<script language='javascript'>window.location='PaginaPrincipal.aspx';</script>");
            }
        }

        protected void btnLogearseClick(object sender, EventArgs e)
        {
            try
            {
                if (this.txtUsuario.Text.Length==0)
                {
                    ShowMessage("Debe ingresar el Usuario");
                    return;
                }
                if (this.txtContraseña.Text.Length == 0)
                {
                    ShowMessage("Debe ingresar la Contraseña");
                    return;
                }
                ServiceCliente.WebServiceClienteSoapClient soapClient = new ServiceCliente.WebServiceClienteSoapClient();
                ServiceCliente.Empleado empleado = soapClient.loginService(this.txtUsuario.Text, this.txtContraseña.Text);
                if (empleado != null)
                {
                    Session["empleado"] = empleado;
                    Response.Write("<script language='javascript'>window.location='PaginaPrincipal.aspx';</script>");
                }
                ShowMessage("Usuario o clave incorrectos");

            }
            catch (Exception ex)
            {
                ShowMessage("Error al intentar logearse");
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