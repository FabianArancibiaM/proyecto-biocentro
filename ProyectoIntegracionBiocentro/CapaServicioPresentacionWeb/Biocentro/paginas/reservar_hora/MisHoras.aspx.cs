using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using CapaServicioPresentacionWeb.Biocentro.paginas.helpers;

namespace CapaServicioPresentacionWeb.Biocentro.paginas.reservar_hora
{
    public partial class MisHoras : System.Web.UI.Page
    {
        Commons commons;
        protected void Page_Load(object sender, EventArgs e)
        {
            commons = new Commons(Page);
        }
        protected void btnBuscar_Click(object sender, EventArgs e)
        {
            try
            {
                if(string.IsNullOrEmpty(this.txtRut.Text))
                {
                    commons.ShowMessage("Error", "Debe ingresar su RUT", "error");
                    return;
                }
                if(string.IsNullOrEmpty(this.txtEmail.Text))
                {
                    commons.ShowMessage("Error", "Debe ingresar su correo", "error");
                    return;
                }
                if (!commons.validarRut(this.txtRut.Text))
                {
                    commons.ShowMessage("Error", "El formato del RUT ingresado es inválido", "error");
                    return;
                }
                if (!commons.ComprobarFormatoEmail(this.txtEmail.Text))
                {
                    commons.ShowMessage("Error", "El formato del correo ingresado  es inválido", "error");
                    return;
                }
                ServiceCliente.WebServiceClienteSoapClient soapClient = new ServiceCliente.WebServiceClienteSoapClient();
                List<ServiceCliente.HoraAtencion> misHoras = new List<ServiceCliente.HoraAtencion>();
                ServiceCliente.HoraAtencion[] listaHoras = soapClient.listaReservasPorRutAndCorreoService(txtRut.Text, txtEmail.Text);
                if(listaHoras == null || listaHoras.Length == 0)
                {
                    commons.ShowMessage("Atención", "No tiene horas asociadas", "warning");
                    return;
                }
               // misHoras.AddRange(listaHoras);
                for (int i = 0; i < listaHoras.Length; i++)
                {
                    //Mostrar reservas que no hayan sido anuladas,
                    if (listaHoras[i].EstadoReserva.IdEstado != 3)
                    {
                        misHoras.Add(listaHoras[i]);
                    }
                }
                if (misHoras == null || misHoras.Count == 0)
                {
                    commons.ShowMessage("Atención", "No tiene horas asociadas", "warning");
                    return;
                }
                Session["misHoras"] = misHoras;
                Response.Write("<script language='javascript'>window.location='MisHorasDetalle.aspx';</script>");
            }
            catch (Exception ex)
            {
                commons.ShowMessage("Error", "Error inesperado al buscar sus horas. Inténtelo nuevamente", "error");
            }
        }
    }
}