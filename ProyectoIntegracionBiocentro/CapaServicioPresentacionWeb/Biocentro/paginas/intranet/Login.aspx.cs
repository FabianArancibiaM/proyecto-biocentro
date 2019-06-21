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
            try
            {
                if (Session["empleado"] != null)
                {
                    Response.Write("<script language='javascript'>window.location='PaginaPrincipal.aspx';</script>");
                }
            }
            catch(Exception ex)
            {
                ShowMessage("Error al ingresar al portal");
            }
        }

        protected void btnLogearseClick(object sender, EventArgs e)
        {
            try
            {
                if (this.txtUsuario.Text.Trim().Length==0)
                {
                    ShowMessage("Debe ingresar su Usuario");
                    return;
                }
                if (this.txtContraseña.Text.Trim().Length == 0)
                {
                    ShowMessage("Debe ingresar su Contraseña");
                    return;
                }
                ServiceCliente.WebServiceClienteSoapClient soapClient = new ServiceCliente.WebServiceClienteSoapClient();
                ServiceCliente.Empleado empleado = soapClient.loginService(this.txtUsuario.Text, this.txtContraseña.Text);
                if (empleado == null)
                {
                    ShowMessage("Usuario o clave incorrectos");
                    return;
                }
                Session["empleado"] = empleado;
                Response.Write("<script language='javascript'>window.location='PaginaPrincipal.aspx';</script>");
            }
            catch (Exception ex)
            {
                ShowMessage("Error inesperado al iniciar sesión. Inténtelo nuevamente");
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