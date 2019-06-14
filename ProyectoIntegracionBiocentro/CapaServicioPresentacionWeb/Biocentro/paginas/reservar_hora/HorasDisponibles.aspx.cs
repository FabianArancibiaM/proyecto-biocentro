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
            if (!IsPostBack)
            {
                cargarHoras();
            }
        }

        private void cargarHoras()
        {
            try
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
            catch (Exception ex)
            {
                ShowMessage("Error al cargar las horas : " + ex.Message);
            }
        }

        protected void seleccionarHora(object sender, EventArgs e)
        {
            try
            {
                ServiceCliente.HoraAtencion hora = new ServiceCliente.HoraAtencion();
                for (int i = 0; i < this.gvHorasDisponibles.Rows.Count; i++)
                {
                    GridViewRow row = this.gvHorasDisponibles.Rows[i];
                    bool isChecked2 = ((CheckBox)row.FindControl("CheckBox1")).Checked;
                    if (isChecked2)
                    {
                        hora.IdHora = Convert.ToInt32(((Label)row.FindControl("lblIdHora")).Text);
                        hora.EspecialidadClinica = new ServiceCliente.EspecialidadClinica();
                        hora.EspecialidadClinica.Precio = Convert.ToInt32(((Label)row.FindControl("lblValor")).Text.Replace("$ ", ""));
                    }
                }
                //Session["horas"] = listHoraAtencion;
                Response.Write("<script language='javascript'>window.location='HorasDisponibles.aspx';</script>");
            }
            catch(Exception ex)
            {
                ShowMessage("Error al seleccionar la hora : " + ex.Message);
            }
        }
        public void ShowMessage(string message)
        {
            System.Text.StringBuilder sb = new System.Text.StringBuilder();
            sb.Append("<script type = 'text/javascript'> ");
            sb.Append("window.onload=function(){");
            sb.Append("alert('");
            sb.Append(message);
            sb.Append("')};");
            sb.Append("</script>");
            ClientScript.RegisterClientScriptBlock(this.GetType(), "alert", sb.ToString());
        }

        protected void gvHorasDisponibles_SelectedIndexChanged(object sender, GridViewCommandEventArgs e)
        {
            
            try
            {
                if (e.CommandName == "seleccionarBtn")
                {
                    int index = Convert.ToInt32(e.CommandArgument);

                    GridViewRow selectedRow = this.gvHorasDisponibles.Rows[index];
                    TableCell contactName = selectedRow.Cells[0];
                    string idHoraSeleccionada = contactName.Text;
                    Session["idHoraSeleccionada"] = idHoraSeleccionada;
                    Response.Write("<script language='javascript'>window.location='HorasDisponibles.aspx';</script>");
                }
            }
            catch (Exception ex)
            {
                ShowMessage("Error al seleccionar la hora : " + ex.Message);
            }
        }
    }
}