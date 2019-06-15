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
            if (Session["empleado"] ==null)
            {
                Response.Write("<script language='javascript'>window.location='Login.aspx';</script>");
            }
            else
            {
                ServiceCliente.Empleado empleado = (ServiceCliente.Empleado)Session["empleado"];
                this.msjBienvenida.Text = "Bienvenido/a " + empleado.Nombre + " " + empleado.ApellidoPaterno + " " + empleado.ApellidoMaterno;
            }
            
        }

        protected void btnRegistrarPago_Click(object sender, EventArgs e)
        {
            Response.Write("<script language='javascript'>window.location='RegistrarPago.aspx';</script>");
        }
    }
}