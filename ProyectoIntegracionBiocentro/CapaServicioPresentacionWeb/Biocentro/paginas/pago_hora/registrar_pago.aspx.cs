using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace CapaServicioPresentacionWeb.Biocentro.paginas.pago_hora
{
    public partial class registrar_pago : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                cargarMedioPago();
            }
        }

        private void cargarMedioPago()
        {
            try
            {
                this.comboMedioPago.Items.Clear();
                ServiceCliente.WebServiceClienteSoapClient soapClient = new ServiceCliente.WebServiceClienteSoapClient();
                List<ServiceCliente.MedioPago> listaMedioPago = new List<ServiceCliente.MedioPago>();
                listaMedioPago.AddRange(soapClient.generarListaMedioPagoService());
                if (listaMedioPago == null)
                {
                    ShowMessage("No se encontraron datos");
                    return;
                }
                this.comboMedioPago.Items.Add(new ListItem("Seleccionar", "0"));
                foreach (ServiceCliente.MedioPago var in listaMedioPago)
                {
                    this.comboMedioPago.Items.Add(new ListItem(var.Nombre, var.IdMedioPago.ToString()));
                }
                this.comboMedioPago.DataBind();
                this.comboMedioPago.SelectedIndex = 0;
            }
            catch (Exception ex)
            {
                ShowMessage("Se produjo un error: " + ex.Message);
            }
        }

        protected void btnBuscarPaciente_Click(object sender, EventArgs e)
        {

            try
            {
                if (this.txtBuscarPaciente.Text.Length == 0)
                {
                    ShowMessage("Error: Ingrese un rut");
                }
                ServiceCliente.WebServiceClienteSoapClient soapClient = new ServiceCliente.WebServiceClienteSoapClient();
                this.tablaResumen.DataSource = null;
                this.tablaResumen.DataBind();
                List<ServiceCliente.HoraAtencion> listReserva = new List<ServiceCliente.HoraAtencion>();
                ServiceCliente.HoraAtencion[] listaSoap = soapClient.horasPorRutPacienteMasVentaService(this.txtBuscarPaciente.Text);
                if (listaSoap == null)
                {
                    return;
                }
                if (listaSoap != null)
                {
                    for (int i = 0; i < listaSoap.Length; i++)
                    {
                        listReserva.Add(listaSoap[i]);
                    }
                }
                this.txtRutPaciente1.Text = listReserva.First().Paciente.Rut;
                this.txtNombrePaciente.Text = listReserva.First().Paciente.Nombre + " " + listReserva.First().Paciente.ApellidoPaterno + " " + listReserva.First().Paciente.ApellidoMaterno;
                this.tablaResumen.DataSource = listReserva;
                this.tablaResumen.DataBind();
            }
            catch (Exception ex)
            {
                ShowMessage("Se produjo un error: " + ex.Message);
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
                    this.txtTotal.Text = total.ToString();
                }
            }
            catch (Exception ex)
            {
                ShowMessage("Se produjo un error: " + ex.Message);
            }
        }

        protected void guardarPago(object sender, EventArgs e)
        {

            try
            {
                List<ServiceCliente.HoraAtencion> listReserva = new List<ServiceCliente.HoraAtencion>();
                for (int i = 0; i < this.tablaResumen.Rows.Count; i++)
                {
                    GridViewRow row = this.tablaResumen.Rows[i];
                    bool isChecked2 = ((CheckBox)row.FindControl("CheckBox1")).Checked;
                    if (isChecked2)
                    {
                        listReserva.Add((ServiceCliente.HoraAtencion)this.tablaResumen.Rows[i].DataItem);
                    }
                }
                if (listReserva.Count <= 0)
                {
                    ShowMessage("Error: Debe seleccionar una hora a pagar.");
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
                //venta.MedioPago = (ServiceCliente.MedioPago)this.comboMedioPago.SelectedItem;
                venta.Monto = total;
                ServiceCliente.EstadoVenta esVenta = new ServiceCliente.EstadoVenta();
                esVenta.IdEstadoVenta = 2;
                venta.EstadoVenta = esVenta;
                ServiceCliente.WebServiceClienteSoapClient soapClient = new ServiceCliente.WebServiceClienteSoapClient();
                ServiceCliente.StatusResponce responce = soapClient.guardarVentaRealizadaService(venta, listaIdHora);
                if (responce.Estado.Equals("error"))
                {
                    ShowMessage("Se produjo un error: " + responce.Mensaje);
                    return;
                }
                ShowMessage(responce.Mensaje);
                return;
            }
            catch (Exception ex)
            {
                ShowMessage("Se produjo un error: " + ex.Message);
            }
        }
    }
}