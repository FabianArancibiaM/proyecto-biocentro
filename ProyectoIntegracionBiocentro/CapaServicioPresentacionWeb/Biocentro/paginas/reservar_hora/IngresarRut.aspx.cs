using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace CapaServicioPresentacionWeb.Biocentro.paginas.reservar_hora
{
    public partial class IngresarRut : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                //Si no se ha envido ningun valor redirecciona a la página de inicio
                if (Session["id_hora"] == null)
                {
                    Response.Redirect("~/Biocentro/paginas/reservar_hora/InicioReserva.aspx");
                }
                if (!IsPostBack)
                {
                    //Recupera el valor enviado desde la página anterior
                    int idHoraAtencion = (int)Session["id_hora"];
                    ServiceCliente.WebServiceClienteSoapClient soapClient = new ServiceCliente.WebServiceClienteSoapClient();
                    ServiceCliente.HoraAtencion horaAtencion = soapClient.buscarDetalleHoraService(idHoraAtencion);

                    string fechaHora = horaAtencion.Fecha.ToString("dd/MM/yyyy ") + " de " + horaAtencion.IdBloque.HoraInicio + ":00 - " +
                                       horaAtencion.IdBloque.HoraFin + ":00";
                    this.lblFechaHora.Text = fechaHora;
                    string lugar = horaAtencion.Sala.Nombre + ", Miguel Claro 195, Providencia";
                    this.lblLugar.Text = lugar;
                    string especialidad = horaAtencion.EspecialidadClinica.Nombre;
                    this.lblEspecialidad.Text = especialidad;
                    string terapeuta = horaAtencion.Terapeuta.Nombre + " " + horaAtencion.Terapeuta.ApellidoPaterno +
                                       " " + horaAtencion.Terapeuta.ApellidoMaterno;
                    this.lblTerapeuta.Text = terapeuta;
                }
            }
            catch(Exception ex)
            {
                ShowMessage("Ocurrio un error al cargar la pagina");
            }
        }

        protected void btnVolver(object sender, EventArgs e)
        {
            try
            {
                Response.Write("<script language='javascript'>window.location='HorasDisponibles.aspx';</script>");
            }
            catch (Exception ex)
            {
                ShowMessage("Ocurrio un error al cargar la pagina");
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

        protected void btn_buscar_rut_Click(object sender, EventArgs e)
        {
            try
            {
                if (this.txtRut.Text.Trim().Length == 0)
                {
                    ShowMessage("Error: Ingrese un rut");
                    return;
                }
                if (!validarRut(this.txtRut.Text))
                {
                    ShowMessage("Error: Rut ingresado invalido");
                    return;
                }
                string rut = this.txtRut.Text;
                Session["rut"] = rut;
                Response.Write("<script language='javascript'>window.location='DatosPaciente.aspx';</script>");
            }
            catch (Exception ex)
            {
                ShowMessage("Error al intentar cargar la pagina");
            }
        }
        public bool validarRut(string rut)
        {
            bool validacion = false;
            try
            {
                rut = rut.ToUpper();
                rut = rut.Replace(".", "");
                rut = rut.Replace("-", "");
                int rutAux = int.Parse(rut.Substring(0, rut.Length - 1));

                char dv = char.Parse(rut.Substring(rut.Length - 1, 1));

                int m = 0, s = 1;
                for (; rutAux != 0; rutAux /= 10)
                {
                    s = (s + rutAux % 10 * (9 - m++ % 6)) % 11;
                }
                if (dv == (char)(s != 0 ? s + 47 : 75))
                {
                    validacion = true;
                }
            }
            catch (Exception)
            {
            }
            return validacion;
        }
    }
}