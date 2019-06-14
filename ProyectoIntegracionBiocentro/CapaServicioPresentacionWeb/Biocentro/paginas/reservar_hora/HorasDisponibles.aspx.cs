using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace CapaServicioPresentacionWeb.Biocentro.paginas.reservar_hora
{
    public partial class HorasDisponibles : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            List<ServiceCliente.HoraAtencion> horas = (List<ServiceCliente.HoraAtencion>)Session["horas"];
            var horas_disponibles = from h in horas
                                    select new
                                    {
                                        Id = String.Format("{0}", h.IdHora),
                                        Fecha = h.Fecha.ToString("dd/MM/yyyy"),
                                        HoraInicio = String.Format("{0}:00", h.IdBloque.HoraInicio),
                                        HoraFin = String.Format("{0}:00", h.IdBloque.HoraFin),
                                        Especialidad = String.Format("{0}", h.EspecialidadClinica.Nombre),
                                        NombreTerapeuta = String.Format("{0} {1} {2}", h.Terapeuta.Nombre,
                                                          h.Terapeuta.ApellidoPaterno, h.Terapeuta.ApellidoMaterno)
                                    };
            gvHorasDisponibles.DataSource = horas_disponibles;
            gvHorasDisponibles.DataBind();
        }

        protected void btnSeleccionar_Click(object sender, EventArgs e)
        {
            //Obtener el boton que envió la solicitud
            Button btn = (Button)sender;
            //Obtener la fila del boton
            GridViewRow row = (GridViewRow)btn.NamingContainer;
            //Obtener la id del detalle a eliminar
            int id = int.Parse(gvHorasDisponibles.DataKeys[row.RowIndex].Value.ToString());
            //Redirecciona a página con detalle del nº de la orden
            //Response.Redirect("~/Biocentro/paginas/reservar_hora/IngresarRut.aspx?id_hora=" + id);
            Session["id_hora"] = id;
            Response.Write("<script language='javascript'>window.location='IngresarRut.aspx';</script>");
        }
    }
}