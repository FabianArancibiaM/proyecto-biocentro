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
            if(Session["misHoras"] == null)
            {
                Response.Write("<script language='javascript'>window.location='MisHoras.aspx';</script>");
            }

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
                                                              h.Terapeuta.ApellidoPaterno, h.Terapeuta.ApellidoMaterno),
                                            Confirmada = h.EstadoReserva.IdEstado
                                        };
                //cargamos grilla
                gvwMisHoras.DataSource = horas_disponibles;
                gvwMisHoras.DataBind();
                this.lblRut.Text = commons.formatearRut(horas.First().Paciente.Rut);
                this.lblPaciente.Text = horas.First().Paciente.Nombre + " " + horas.First().Paciente.ApellidoPaterno + " " + horas.First().Paciente.ApellidoMaterno;
            }
            catch (Exception ex)
            {
                commons.ShowMessage("Error", "Ocurrió un error inesperado al cargar las horas. Inténtelo nuevamente", "error");
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
                commons.ShowMessage("Error", "Ocurrió un error al confirmar la hora. Inténtelo nuevamente.", "error");
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
                commons.ShowMessage("Error", "Ocurrió un error al anular la hora. Inténtelo nuevamente.", "error");
            }
        }
    }
}