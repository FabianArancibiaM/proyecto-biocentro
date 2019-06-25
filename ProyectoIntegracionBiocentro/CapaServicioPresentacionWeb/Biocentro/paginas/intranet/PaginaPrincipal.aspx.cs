using CapaServicioPresentacionWeb.Biocentro.paginas.helpers;
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
        Commons commons;
        protected void Page_Load(object sender, EventArgs e)
        {
            commons = new Commons(Page);

            try
            {
                if (Session["empleado"] == null)
                {
                    Response.Write("<script language='javascript'>window.location='Login.aspx';</script>");
                }
                else
                {
                    ServiceCliente.Empleado empleado = (ServiceCliente.Empleado)Session["empleado"];
                    this.msjBienvenida.Text = "Bienvenid@ " + empleado.Nombre + " " + empleado.ApellidoPaterno + " " + empleado.ApellidoMaterno;
                }
            }
            catch(Exception ex)
            {
                this.msjBienvenida.Text = "Bienvenid@";
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
                commons.ShowMessage("Error", "Error al cargar la página", "error");
            }
        }
    }
}