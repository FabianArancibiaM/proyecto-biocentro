using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace CapaPresentacion.Biocentro.paginas.reservar_hora
{
    public partial class inicio_reservar : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void btnMisHoras_Click(object sender, EventArgs e)
        {
            Response.Write("<script language='javascript'>window.location='../mis_horas/inicio_mis_horas.aspx';</script>");
        }

        protected void btnInicioReserva_Click(object sender, EventArgs e)
        {
            Response.Write("<script language='javascript'>window.location='buscar_hora.aspx';</script>");
        }
    }
}