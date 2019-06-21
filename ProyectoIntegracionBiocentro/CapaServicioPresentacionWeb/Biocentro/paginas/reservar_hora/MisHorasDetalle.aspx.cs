using CapaServicioPresentacionWeb.Biocentro.paginas.helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace CapaServicioPresentacionWeb.Biocentro.paginas.reservar_hora
{
    public partial class MisHorasDetalle : System.Web.UI.Page
    {
        Commons commons;
        protected void Page_Load(object sender, EventArgs e)
        {
            commons = new Commons(Page);

            if (!IsPostBack)
            {
                CargarGrilla();
            }
        }
        private void CargarGrilla()
        {
            try
            {
                //crear objeto lista que carga sesion
                List<ServiceCliente.HoraAtencion> horas = (List<ServiceCliente.HoraAtencion>)Session["misHoras"];
                //formatear lista
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
                //cargamos grilla
                gvwMisHoras.DataSource = horas_disponibles;
                gvwMisHoras.DataBind();
            }
            catch (Exception ex)
            {
                commons.ShowMessage("Error inesperado al cargar las horas");
            }
        }

        protected void btnConfirmar_Click(object sender, EventArgs e)
        {
            try
            {
                //Obtener el boton que envió la solicitud
                Button btn = (Button)sender;
                //Obtener la fila del boton
                GridViewRow row = (GridViewRow)btn.NamingContainer;
                //Obtener la id del detalle a eliminar
                int id = int.Parse(gvwMisHoras.DataKeys[row.RowIndex].Value.ToString());
                Char idConfirmacion = '1';

                //llamamos a servicio de confirmación de hora
                ServiceCliente.WebServiceClienteSoapClient servicio = new ServiceCliente.WebServiceClienteSoapClient();
                servicio.rechazarConfirmarReservaService(idConfirmacion, id);

                //crea sesion con id_hora
                Session["idHora"] = id;
                Session["idConfirmacion"] = idConfirmacion;
                Response.Write("<script language='javascript'>window.location='MisHorasFinal.aspx';</script>");
            }
            catch (Exception ex)
            {
                commons.ShowMessage("Error al seleccionar hora horas : " + ex.Message);
            }

        }
        protected void btnAnular_Click(object sender, EventArgs e)
        {
            try
            {
                //Obtener el boton que envió la solicitud
                Button btn = (Button)sender;
                //Obtener la fila del boton
                GridViewRow row = (GridViewRow)btn.NamingContainer;
                //Obtener la id del detalle a eliminar
                int id = int.Parse(gvwMisHoras.DataKeys[row.RowIndex].Value.ToString());
                Char idConfirmacion = '0';

                //llamamos a servicio de confirmación de hora
                ServiceCliente.WebServiceClienteSoapClient servicio = new ServiceCliente.WebServiceClienteSoapClient();
                servicio.rechazarConfirmarReservaService(idConfirmacion, id);

                Session["idHora"] = id;
                Session["idConfirmacion"] = idConfirmacion;
               Response.Write("<script language='javascript'>window.location='MisHorasFinal.aspx';</script>");
            }
            catch (Exception ex)
            {
                commons.ShowMessage("Error al seleccionar hora horas : " + ex.Message);
            }

        }
    }
}