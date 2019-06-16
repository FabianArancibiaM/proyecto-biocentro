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
        protected void Page_Load(object sender, EventArgs e)
        {

        }
        protected void btnMisHoras_Click(object sender, EventArgs e)
        {
            Response.Write("<script language='javascript'>window.location='MisHoras.aspx';</script>");
        }

        protected void btnInicioReserva_Click(object sender, EventArgs e)
        {
            Response.Write("<script language='javascript'>window.location='BuscarHora.aspx';</script>");
        }
    }
}