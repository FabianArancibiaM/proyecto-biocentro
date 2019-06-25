using CapaServicioPresentacionWeb.Biocentro.paginas.helpers;
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
        Commons commons;
        protected void Page_Load(object sender, EventArgs e)
        {
            commons = new Commons(this.Page);
            try
            {
                if (Session["empleado"] != null)
                {
                    Response.Write("<script language='javascript'>window.location='PaginaPrincipal.aspx';</script>");
                }
            }
            catch(Exception ex)
            {
                commons.ShowMessage("Error", "Ocurrió un error al ingresar al portal", "error");
            }
        }

        protected void btnLogearseClick(object sender, EventArgs e)
        {
            try
            {
                if (this.txtUsuario.Text.Trim().Length==0)
                {
                    commons.ShowMessage("Error", "Debe ingresar su Usuario", "Error");
                    return;
                }
                if (this.txtContraseña.Text.Trim().Length == 0)
                {
                    commons.ShowMessage("Error", "Debe ingresar su Contraseña", "Error");
                    return;
                }
                ServiceCliente.WebServiceClienteSoapClient soapClient = new ServiceCliente.WebServiceClienteSoapClient();
                ServiceCliente.Empleado empleado = soapClient.loginService(this.txtUsuario.Text, this.txtContraseña.Text);
                if (empleado == null)
                {
                    commons.ShowMessage("Error", "Usuario o contraseña incorrectos", "Error");
                    return;
                }
                Session["empleado"] = empleado;
                Response.Write("<script language='javascript'>window.location='PaginaPrincipal.aspx';</script>");
            }
            catch (Exception ex)
            {
                commons.ShowMessage("Error", "Error inesperado al iniciar sesión. Inténtelo nuevamente.", "error");
            }
        }
    }
}