using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace CapaServicioPresentacionWeb.Biocentro.paginas.reservar_hora
{
    public partial class MisHorasFinal : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            CargarHora();
        }

        private void CargarHora()
        {
            // label confirmacion
            Char idConfirmacion = (Char)Session["idConfirmacion"];
            if (idConfirmacion.Equals('1'))
            {
                lblConfirmacionHora.Text = "confirmada";
            }
            else
            {
                lblConfirmacionHora.Text = "anulada";
            }

            //id hora
            int idHora = (int)Session["idHora"];
            lblIdHora.Text = idHora.ToString();

            //detalle hora
            ServiceCliente.HoraAtencion horaAtencion = new ServiceCliente.HoraAtencion();
            List<ServiceCliente.HoraAtencion> horaAtencionsDetalle = (List<ServiceCliente.HoraAtencion>)Session["misHoras"];
            horaAtencion = horaAtencionsDetalle.Where(h => h.IdHora == idHora).FirstOrDefault();
            //llenamos los controles
            lblFechaHora.Text = horaAtencion.Fecha.ToString();
            lblEspecialidad.Text = horaAtencion.EspecialidadClinica.Nombre;
            lblTerapeuta.Text = horaAtencion.Terapeuta.Nombre + " " + horaAtencion.Terapeuta.ApellidoPaterno;
            lblLugar.Text = horaAtencion.Sala.Nombre+ ", Providencia 180, Ñuñoa";
        }
    }
}