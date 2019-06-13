using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace CapaServicioPresentacionWeb.Biocentro.paginas.reservar_hora
{
    public partial class BuscarHora : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                ServiceCliente.WebServiceClienteSoapClient soapClient = new ServiceCliente.WebServiceClienteSoapClient();
                ServiceCliente.EspecialidadClinica[] especialidades = soapClient.generarListaEspecialidadService();
                this.ddlEspecialidad.DataSource = especialidades;
                this.ddlEspecialidad.DataTextField = "Nombre";
                this.ddlEspecialidad.DataValueField = "IdEspecialidadClinica";
                this.ddlEspecialidad.DataBind();
                this.ddlEspecialidad.Items.Insert(0, new ListItem("Seleccionar Especialidad", ""));
                this.ddlEspecialidad.SelectedIndex = 0;

                ServiceCliente.EspecialidadTerapeuta[] terapeutas = soapClient.generarListaEspecialistaService();
                var terapeutas_filtrado = (from t in terapeutas
                                           select new
                                           {
                                               t.Empleado.IdEmpleado,
                                               t.Empleado.Nombre,
                                               t.Empleado.ApellidoPaterno,
                                               t.Empleado.ApellidoMaterno,
                                               IdTerapeuta = String.Format("{0}", t.Empleado.IdEmpleado),
                                               NombreTerapeuta = String.Format("{0} {1} {1}", t.Empleado.Nombre,
                                                           t.Empleado.ApellidoPaterno, t.Empleado.ApellidoMaterno)
                                           }).Distinct();

                this.ddlTerapeuta.DataSource = terapeutas_filtrado;
                this.ddlTerapeuta.DataTextField = "NombreTerapeuta";
                this.ddlTerapeuta.DataValueField = "IdTerapeuta";
                this.ddlTerapeuta.DataBind();
                this.ddlTerapeuta.Items.Insert(0, new ListItem("Seleccionar Terapeuta", ""));
                this.ddlTerapeuta.SelectedIndex = 0;
            }

        }
        protected void btnBuscar_Click(object sender, EventArgs e)
        {
            try
            {
                ServiceCliente.Empleado persona = null;
                if (this.ddlTerapeuta.SelectedIndex > 0)
                {
                    persona = new ServiceCliente.Empleado();
                    persona.IdEmpleado = Convert.ToInt32(this.ddlTerapeuta.SelectedItem.Value);
                }
                ServiceCliente.EspecialidadClinica especialidad = null;
                if (this.ddlEspecialidad.SelectedIndex > 0)
                {
                    especialidad = new ServiceCliente.EspecialidadClinica();
                    especialidad.IdEspecialidadClinica = Convert.ToInt32(this.ddlEspecialidad.SelectedItem.Value);
                }
                DateTime? fecha = null;
                /*if (this.dateTimePicker1.Value.CompareTo(DateTime.Today) < 0)
                {
                    return;
                }*/

                ServiceCliente.WebServiceClienteSoapClient soapClient = new ServiceCliente.WebServiceClienteSoapClient();
                List<ServiceCliente.HoraAtencion> listHoraAtencion = new List<ServiceCliente.HoraAtencion>();
                listHoraAtencion.AddRange(soapClient.buscarHorasDisponiblesService(especialidad, fecha, persona));
                if (listHoraAtencion == null || listHoraAtencion.Count == 0)
                {
                    return;
                }

                Session["horas"] = listHoraAtencion;
                Response.Write("<script language='javascript'>window.location='HorasDisponibles.aspx';</script>");
            }
            catch (Exception ex)
            {
                ShowMessage("Error al guardar: "+ ex.Message);
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
    }
}