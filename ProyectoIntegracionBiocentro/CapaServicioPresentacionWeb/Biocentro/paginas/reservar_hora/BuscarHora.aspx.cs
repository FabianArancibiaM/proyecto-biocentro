﻿using CapaServicioPresentacionWeb.Biocentro.paginas.helpers;
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
        Commons commons;
        protected void Page_Load(object sender, EventArgs e)
        {
            commons = new Commons(Page);

            if (!IsPostBack)
            {
                Session["horas"] = null;
                cargarEspecialista();
                cargarEspecialidades();
                this.ddlEspecialidad.Visible = true;
                this.ddlTerapeuta.Visible = false;
            }
        }
        protected void botonEspecialidadClick(object sender, EventArgs e)
        {
            this.ddlTerapeuta.Visible = false;
            this.ddlTerapeuta.SelectedIndex = 0;
            this.ddlEspecialidad.Visible = true;
            this.btnTerapeuta.CssClass = "btn  btn-md btn-basic";
            this.btnEspecialidad.CssClass = "btn btn-md btn-success";
        }
        protected void botonEspecialistaClick(object sender, EventArgs e)
        {
            this.ddlEspecialidad.Visible = false;
            this.ddlEspecialidad.SelectedIndex = 0;
            this.ddlTerapeuta.Visible = true;
            this.btnEspecialidad.CssClass = "btn btn-md btn-basic";
            this.btnTerapeuta.CssClass = "btn btn-md btn-success";
        }
        private void cargarEspecialista()
        {
            try
            {
                ServiceCliente.WebServiceClienteSoapClient soapClient = new ServiceCliente.WebServiceClienteSoapClient();
                ServiceCliente.EspecialidadTerapeuta[] terapeutas = soapClient.generarListaEspecialistaService();
                var terapeutas_filtrado = (from t in terapeutas
                                           select new
                                           {
                                               t.Empleado.IdEmpleado,
                                               t.Empleado.Nombre,
                                               t.Empleado.ApellidoPaterno,
                                               t.Empleado.ApellidoMaterno,
                                               IdTerapeuta = String.Format("{0}", t.Empleado.IdEmpleado),
                                               NombreTerapeuta = String.Format("{0} {1} {2}", t.Empleado.Nombre,
                                                           t.Empleado.ApellidoPaterno, t.Empleado.ApellidoMaterno)
                                           }).Distinct();

                this.ddlTerapeuta.DataSource = terapeutas_filtrado;
                this.ddlTerapeuta.DataTextField = "NombreTerapeuta";
                this.ddlTerapeuta.DataValueField = "IdTerapeuta";
                this.ddlTerapeuta.DataBind();
                this.ddlTerapeuta.Items.Insert(0, new ListItem("Seleccionar Terapeuta", ""));
                this.ddlTerapeuta.SelectedIndex = 0;
            }
            catch (Exception ex)
            {
                commons.ShowMessage("Error", "Error al cargar las Especialidades", "error");
            }
        }
        private void cargarEspecialidades()
        {
            try
            {
                ServiceCliente.WebServiceClienteSoapClient soapClient = new ServiceCliente.WebServiceClienteSoapClient();
                ServiceCliente.EspecialidadClinica[] especialidades = soapClient.generarListaEspecialidadService();
                this.ddlEspecialidad.DataSource = especialidades;
                this.ddlEspecialidad.DataTextField = "Nombre";
                this.ddlEspecialidad.DataValueField = "IdEspecialidadClinica";
                this.ddlEspecialidad.DataBind();
                this.ddlEspecialidad.Items.Insert(0, new ListItem("Seleccionar Especialidad", ""));
                this.ddlEspecialidad.SelectedIndex = 0;
            }
            catch (Exception ex)
            {
                commons.ShowMessage("Error", "Error al cargar las Especialidades", "error");
            }
        }
        protected void btnBuscar_Click(object sender, EventArgs e)
        {
            try
            {
                if(this.ddlTerapeuta.SelectedIndex == 0 && this.ddlEspecialidad.SelectedIndex == 0)
                {
                    commons.ShowMessage("Atención", "Debe seleccionar una especialidad o un terapeuta", "warning");
                    return;
                }

                ServiceCliente.Empleado terapeuta = null;
                if (this.ddlTerapeuta.SelectedIndex > 0)
                {
                    terapeuta = new ServiceCliente.Empleado();
                    terapeuta.IdEmpleado = Convert.ToInt32(this.ddlTerapeuta.SelectedItem.Value);
                    terapeuta.Nombre = this.ddlTerapeuta.SelectedItem.Text;
                    Session["terapeuta"] = this.ddlTerapeuta.SelectedItem.Text;
                }
                ServiceCliente.EspecialidadClinica especialidad = null;
                if (this.ddlEspecialidad.SelectedIndex > 0)
                {
                    especialidad = new ServiceCliente.EspecialidadClinica();
                    especialidad.IdEspecialidadClinica = Convert.ToInt32(this.ddlEspecialidad.SelectedItem.Value);
                    especialidad.Nombre = this.ddlEspecialidad.SelectedItem.Text;
                    Session["especialidad"] = this.ddlEspecialidad.SelectedItem.Text;
                }
                DateTime? fecha = null;
                ServiceCliente.WebServiceClienteSoapClient soapClient = new ServiceCliente.WebServiceClienteSoapClient();
                List<ServiceCliente.HoraAtencion> listHoraAtencion = new List<ServiceCliente.HoraAtencion>();
                ServiceCliente.HoraAtencion[] horasDisponibles = soapClient.buscarHorasDisponiblesService(especialidad, fecha, terapeuta);

                String horaPara = "";
                if (especialidad != null)
                {
                    horaPara = " para " + especialidad.Nombre;
                }
                if (terapeuta != null)
                {
                    horaPara = " con " + terapeuta.Nombre;
                }
                if (horasDisponibles == null || horasDisponibles.Length == 0)
                {
                    commons.ShowMessage("Atención", "No se encontraron horas disponibles" + horaPara, "warning");

                    return;
                }
                listHoraAtencion.AddRange(horasDisponibles);
                if (listHoraAtencion == null || listHoraAtencion.Count == 0)
                {
                    commons.ShowMessage("Atención", "No se encontraron horas disponibles" + horaPara, "warning");
                    return;
                }

                Session["horas"] = listHoraAtencion;
                Response.Write("<script language='javascript'>window.location='HorasDisponibles.aspx';</script>");
            }
            catch (Exception ex)
            {
                commons.ShowMessage("Error", "Error al buscar las horas disponibles", "error"); commons.ShowMessage("Error", "Error al buscar las horas disponibles", "error");

            }
        }
    }
}