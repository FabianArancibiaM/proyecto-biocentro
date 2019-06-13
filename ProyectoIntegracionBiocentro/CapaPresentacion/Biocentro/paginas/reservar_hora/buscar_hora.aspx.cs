using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using CapaNegocio;
using CapaDTO;

namespace CapaPresentacion.Biocentro.paginas.reservar_hora
{
    public partial class buscar_hora : System.Web.UI.Page
    {
        NegocioService negocioService = new NegocioService();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                List <EspecialidadClinica> especialidades = negocioService.generarListaEspecialidad();
                ddlEspecialidad.DataSource = especialidades;
                ddlEspecialidad.DataTextField = "Nombre";
                ddlEspecialidad.DataValueField = "IdEspecialidadClinica";
                ddlEspecialidad.DataBind();
                ddlEspecialidad.Items.Insert(0, new ListItem("Seleccionar Especialidad", ""));
                ddlEspecialidad.SelectedIndex = 0;

                List<EspecialidadTerapeuta> terapeutas = negocioService.generarListaEspecialista();
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
                              
                ddlTerapeuta.DataSource = terapeutas_filtrado;
                ddlTerapeuta.DataTextField = "NombreTerapeuta";
                ddlTerapeuta.DataValueField = "IdTerapeuta"; 
                ddlTerapeuta.DataBind();
                ddlTerapeuta.Items.Insert(0, new ListItem("Seleccionar Terapeuta", ""));
                ddlTerapeuta.SelectedIndex = 0;
            }
        }

        protected void btnBuscar_Click(object sender, EventArgs e)
        {
            string claseEspecialidad = ddlEspecialidad.CssClass;
            string claseTerapeuta = ddlTerapeuta.CssClass;

            if(claseEspecialidad.IndexOf("hidden")==-1)
            {
                int idEspecialidad = Convert.ToInt32(ddlEspecialidad.SelectedValue);
                //List<HoraAtencion> horasEspecialidad = negocioService.buscarHorasEspecialidad(idEspecialidad);
                //Session["horas"] = horasEspecialidad;
                Response.Write("<script language='javascript'>window.location='horas_disponibles.aspx';</script>");

            }
            if (claseTerapeuta.IndexOf("hidden") == -1)
            {
                string idTerapeuta = ddlTerapeuta.SelectedValue;
                //negocioService.buscarHorasDisponibles();
            }
        }        
    }
}