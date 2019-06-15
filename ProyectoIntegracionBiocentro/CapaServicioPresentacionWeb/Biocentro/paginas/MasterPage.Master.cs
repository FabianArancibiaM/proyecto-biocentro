using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;

namespace CapaServicioPresentacionWeb.Biocentro.paginas
{
    public partial class MasterPage : System.Web.UI.MasterPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["empleado"]==null)
            {
                this.menuLogOut.Visible = false;
            }
            else
            {
                this.menuLogOut.Visible = true;
            }
        }

        protected void cerrarSession_click(object sender, EventArgs e)
        {
            Session["empleado"] = null;
        }

        protected void validarSession_Click(object sender, EventArgs e)
        {
            Session["empleado"] = null;
            Response.Write("<script language='javascript'>window.location='/Biocentro/paginas/reservar_hora/InicioReserva.aspx';</script>");
        }
    }
}