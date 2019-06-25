using CapaServicioPresentacionWeb.Biocentro.paginas.helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace CapaServicioPresentacionWeb.Biocentro.paginas.reservar_hora
{
    public partial class InicioReserva : System.Web.UI.Page
    {
        Commons commons;
        protected void Page_Load(object sender, EventArgs e)
        {
            commons = new Commons(Page);

            if (!IsPostBack)
            {
                Session["horas"] = null;
            }
        }
        protected void btnMisHoras_Click(object sender, EventArgs e)
        {
            
            try
            {
                Response.Write("<script language='javascript'>window.location='MisHoras.aspx';</script>");
            }
            catch (Exception ex)
            {
                commons.ShowMessage("Error", "Ocurrió un error al cargar la página", "error");
            }
        }

        protected void btnInicioReserva_Click(object sender, EventArgs e)
        {
            
            try
            {
                Response.Write("<script language='javascript'>window.location='BuscarHora.aspx';</script>");
            }
            catch (Exception ex)
            {
                commons.ShowMessage("Error", "Ocurrió un error al cargar la página", "error");
            }
        }
    }
}