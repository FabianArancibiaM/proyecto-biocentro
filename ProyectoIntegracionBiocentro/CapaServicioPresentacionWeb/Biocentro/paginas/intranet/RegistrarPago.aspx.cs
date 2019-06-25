using CapaServicioPresentacionWeb.Biocentro.paginas.helpers;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace CapaServicioPresentacionWeb.Biocentro.paginas.intranet
{
    public partial class RegistrarPago : System.Web.UI.Page
    {
        Commons commons;
        protected void Page_Load(object sender, EventArgs e)
        {
            commons = new Commons(Page);
            try
            {
                //Validar inicio de sesión
                if (Session["empleado"] == null)
                {
                    Response.Write("<script language='javascript'>window.location='Login.aspx';</script>");
                }

                ServiceCliente.Empleado empleado = (ServiceCliente.Empleado)Session["empleado"];
                //Validar empleado recepctionista
                if (empleado.Cargo.IdCargo != 1)
                {
                    Response.Write("<script language='javascript'>window.location='Login.aspx';</script>");
                }

                validarTabla();
                if (!IsPostBack)
                {
                    cargarMedioPago();
                    this.datosReserva.Visible = false;
                }
            }
            catch (Exception ex)
            {
                commons.ShowMessage("Error", "Se produjo un error al cargar la página", "error");
            }
        }

        private void validarTabla()
        {
            try
            {
                if (this.tablaResumen != null)
                {
                    for (int i = 0; i < this.tablaResumen.Rows.Count; i++)
                    {
                        GridViewRow row = this.tablaResumen.Rows[i];
                        if (((Label)row.FindControl("lblEstado")).Text.Trim().Length > 0)
                        {
                            ((Label)row.FindControl("lblEstado")).Visible = true;
                            ((CheckBox)row.FindControl("CheckBox1")).Visible = false;
                        }
                        else
                        {
                            ((CheckBox)row.FindControl("CheckBox1")).Visible = true;
                            ((Label)row.FindControl("lblEstado")).Visible = false;
                        }
                    }
                }
            }
            catch(Exception ex)
            {
                commons.ShowMessage("Error", "Se produjo un error al cargar la página", "error");
            }
        }

        private void cargarMedioPago()
        {
            try
            {
                this.comboMedioPago.Items.Clear();
                ServiceCliente.WebServiceClienteSoapClient soapClient = new ServiceCliente.WebServiceClienteSoapClient();
                List<ServiceCliente.MedioPago> listaMedioPago = new List<ServiceCliente.MedioPago>();
                ServiceCliente.MedioPago[] mediosPago = soapClient.generarListaMedioPagoService();
                if(mediosPago == null || mediosPago.Length == 0)
                {
                    commons.ShowMessage("Error", "Se produjo un error al cargar los medios de pago", "error");
                    return;
                }
                listaMedioPago.AddRange(mediosPago);
                if (listaMedioPago == null || listaMedioPago.Count == 0)
                {
                    commons.ShowMessage("Error", "Se produjo un error al cargar los medios de pago", "error");
                    return;
                }
                this.comboMedioPago.Items.Add(new ListItem("Seleccionar Medio de pago", "0"));
                foreach (ServiceCliente.MedioPago var in listaMedioPago)
                {
                    this.comboMedioPago.Items.Add(new ListItem(var.Nombre, var.IdMedioPago.ToString()));
                }
                this.comboMedioPago.DataBind();
                this.comboMedioPago.SelectedIndex = 0;
            }
            catch (Exception ex)
            {
                commons.ShowMessage("Error", "Se produjo un error al cargar los medios de pago", "error");
            }
        }

        protected void btnBuscarPaciente_Click(object sender, EventArgs e)
        {
            try
            {
                if (string.IsNullOrEmpty(this.txtBuscarPaciente.Text.Trim()))
                {
                    commons.ShowMessage("Error", "Debe ingresar el RUT del paciente", "error");
                    return;
                }
                if (!commons.validarRut(this.txtBuscarPaciente.Text))
                {
                    commons.ShowMessage("Error", "El formato del RUT ingresado es incorrecto", "error");
                    return;
                }
                //Limpiar Gridview
                ServiceCliente.WebServiceClienteSoapClient soapClient = new ServiceCliente.WebServiceClienteSoapClient();
                this.tablaResumen.DataSource = null;
                this.tablaResumen.DataBind();
                //Buscar horas registradas
                ServiceCliente.HoraAtencion[] listaHoras = soapClient.horasPorRutPacienteMasVentaService(commons.formatearRut(this.txtBuscarPaciente.Text).Replace(".", ""));
                if (listaHoras == null || listaHoras.Count() == 0)
                {
                    commons.ShowMessage("Atención", "El paciente no tiene horas registradas", "warning");
                    return;
                }
                List<ServiceCliente.HoraAtencion> listReserva = new List<ServiceCliente.HoraAtencion>();                
                for (int i = 0; i < listaHoras.Length; i++)
                {
                    //Mostrar reservas que no hayan sido anuladas, y no hayan sido pagadas aún
                    if(listaHoras[i].EstadoReserva.IdEstado != 3)
                    {
                        listReserva.Add(listaHoras[i]);
                    }
                }
                this.datosReserva.Visible = true;
                this.txtRutPaciente.Text = commons.formatearRut(listReserva.First().Paciente.Rut);
                this.txtNombrePaciente.Text = listReserva.First().Paciente.Nombre + " " + listReserva.First().Paciente.ApellidoPaterno + " " + listReserva.First().Paciente.ApellidoMaterno;
                this.tablaResumen.DataSource = listReserva;
                this.tablaResumen.DataBind();
                this.txtTotal.Text = "0";
                validarTabla();
            }
            catch (Exception ex)
            {
                commons.ShowMessage("Error", "Se produjo un error al cargar las reservas del paciente", "error");
            }
        }

        protected void sumarHorasSeleccionadas(object sender, EventArgs e)
        {
            try
            {
                if (IsPostBack)
                {
                    int total = 0;
                    for (int i = 0; i < this.tablaResumen.Rows.Count; i++)
                    {
                        GridViewRow row = this.tablaResumen.Rows[i];
                        bool isChecked2 = ((CheckBox)row.FindControl("CheckBox1")).Checked;
                        if (isChecked2)
                        {
                            int valor = Convert.ToInt32(((Label)row.FindControl("lblValor")).Text.Replace("$ ", ""));
                            total = total + valor;
                        }
                    }
                    this.txtTotal.Text = total.ToString("N0", new CultureInfo("es-ES"));
                }
            }
            catch (Exception ex)
            {
                commons.ShowMessage("Error", "Se produjo un error al calcular el  total", "error");
            }
        }

        protected void guardarPago(object sender, EventArgs e)
        {
            try
            {
                if (this.comboMedioPago.SelectedItem.Value.Equals("0"))
                {
                    commons.ShowMessage("Error", "Debe seleccionar un medio de pago", "error");
                    return;
                }
                List<ServiceCliente.HoraAtencion> listReserva = new List<ServiceCliente.HoraAtencion>();
                for (int i = 0; i < this.tablaResumen.Rows.Count; i++)
                {
                    GridViewRow row = this.tablaResumen.Rows[i];
                    bool isChecked2 = ((CheckBox)row.FindControl("CheckBox1")).Checked;
                    if (isChecked2)
                    {
                        ServiceCliente.HoraAtencion hora = new ServiceCliente.HoraAtencion();
                        hora.IdHora = Convert.ToInt32(((Label)row.FindControl("lblIdHora")).Text);
                        hora.EspecialidadClinica = new ServiceCliente.EspecialidadClinica();
                        hora.EspecialidadClinica.Precio = Convert.ToInt32(((Label)row.FindControl("lblValor")).Text.Replace("$ ", ""));
                        listReserva.Add(hora);
                    }
                }
                if (listReserva.Count <= 0)
                {
                    commons.ShowMessage("Error", "Debe seleccionar al menos una hora a pagar", "error");
                    return;
                }
                int total = 0;
                ServiceCliente.ArrayOfInt listaIdHora = new ServiceCliente.ArrayOfInt();
                foreach (ServiceCliente.HoraAtencion h in listReserva)
                {
                    total = total + h.EspecialidadClinica.Precio;
                    listaIdHora.Add(h.IdHora);
                }
                ServiceCliente.Venta venta = new ServiceCliente.Venta();
                venta.FechaPago = DateTime.Now;
                ServiceCliente.MedioPago medio = new ServiceCliente.MedioPago();
                medio.IdMedioPago = Convert.ToInt32(this.comboMedioPago.SelectedItem.Value);
                venta.MedioPago = medio;
                venta.Monto = total;
                ServiceCliente.EstadoVenta esVenta = new ServiceCliente.EstadoVenta();
                esVenta.IdEstadoVenta = 2;
                venta.EstadoVenta = esVenta;
                ServiceCliente.WebServiceClienteSoapClient soapClient = new ServiceCliente.WebServiceClienteSoapClient();
                ServiceCliente.StatusResponce responce = soapClient.guardarVentaRealizadaService(venta, listaIdHora);
                if (responce.Estado.Equals("error"))
                {
                    commons.ShowMessage("Error", "Se produjo un error al registrar el pago", "error");
                    return;
                }
                commons.ShowMessage("Pago exitoso", "El pago se registró exitosamente", "success");
                this.comboMedioPago.SelectedIndex = 0;
                this.txtTotal.Text = "0";
                btnBuscarPaciente_Click(sender, e);
                return;
            }
            catch (Exception ex)
            {
                commons.ShowMessage("Error", "Se produjo un error al registrar el pago", "error");
            }
        }
    }
}