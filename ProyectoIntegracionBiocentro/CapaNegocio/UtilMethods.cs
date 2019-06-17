using CapaConexion;
using CapaDTO;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;

namespace CapaNegocio
{
    public class UtilMethods
    {
        private Conexion conexion;

        private Conexion Conexion { get => conexion; set => conexion = value; }
        private void configurarConexion()
        {
            this.conexion = new Conexion();
            this.conexion.NombreBaseDeDatos = "BIOCENTRO_DB";
            //Conex casa
            this.conexion.CadenaConexion = "Data Source=lfdbserver.database.windows.net;Initial Catalog=BIOCENTRO_DB;User ID=us_lfloresl;Password=PA$$W0RD;Connect Timeout=30;Encrypt=True;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False";
            //Conex pega
            //this.conexion.CadenaConexion = "Data Source=DESKTOP-E5RAPBM\\SQLSERVER;Initial Catalog=BIOCENTRO_DB;Integrated Security=True";
        }

        public int? guardarEliminarActualizarObjeto(String sqlInsert,Boolean esInsert)
        {
            this.configurarConexion();
            this.conexion.CadenaSQL = sqlInsert;
            this.conexion.EsSelect = false;
            this.conexion.EsInsert = esInsert;
            this.conexion.conectar();
            return this.conexion.IdAsignado;
        }
        public DataSet listarObjetoConTablaEspecifica(String sqlSelect,String nombreTabla)
        {
            this.configurarConexion();
            this.conexion.NombreTabla = nombreTabla;
            this.conexion.CadenaSQL = sqlSelect;
            this.conexion.EsSelect = true;
            this.conexion.conectar();
            return this.conexion.DbDataSet;
        }

        public DataSet listarObjetoMultiTabla(String sqlSelect)
        {
            this.configurarConexion();
            this.conexion.CadenaSQL = sqlSelect;
            this.conexion.EsMasiva = true;
            this.conexion.conectar();
            return this.conexion.DbDataSet;
        }

        public bool enviarEmailReserva(Paciente paciente, HoraAtencion horaAtencion)
        {
            //servidor SMTP de gmail
            SmtpClient client = new SmtpClient("smtp.gmail.com");
            client.Port = 587;
            client.EnableSsl = true;
            //nos autenticamos con nuestra cuenta de gmail
            client.Credentials = new NetworkCredential("fermechile@gmail.com", "ferme123");

            MailMessage mail = new MailMessage();
            mail.From = new MailAddress("contactobiocentro@gmail.com", "Biocentro");
            mail.To.Add(paciente.Correo);
            mail.Subject = "Reserva N°" + horaAtencion.IdHora + ", " + horaAtencion.EspecialidadClinica.Nombre;
            mail.Body = "<!DOCTYPE html> " +
                            "<body style=\"text = 'black'\" style=\"font-family:'Arial'\">" +
                                "<h3>¡Su reserva se ha registrado exitosamente! </h3>" +
                                "<br>Puede acceder a nuestro portal para confirmar o anular su hora. Los detalles de su reserva son los siguiente:" +
                                "<br><p><b>Paciente:</b> " + paciente.Nombre + " " + paciente.ApellidoPaterno + " " + paciente.ApellidoMaterno + "</p>" +
                                "<p><b>Especialidad:</b> " + horaAtencion.EspecialidadClinica.Nombre + 
                                "&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<b>Terapeuta:</b> " +
                                horaAtencion.Terapeuta.Nombre + " " + horaAtencion.Terapeuta.ApellidoPaterno + " " + horaAtencion.Terapeuta.ApellidoMaterno +"</p>"+
                                " <p><b>Fecha:</b> " +  horaAtencion.Fecha.ToString("dd/MM/yyyy ") + " de " + horaAtencion.IdBloque.HoraInicio + ":00 - " +
                                    horaAtencion.IdBloque.HoraFin + ":00" +
                                "&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<b>Lugar:</b> " + horaAtencion.Sala.Nombre + ", Miguel Claro 195, Providencia" + 
                                "<hr>" +
                                "<br><br><img src=cid:imgFirma>" +
                            "</body>" +
                         "</html>";
            mail.IsBodyHtml = true;

            AlternateView htmlView = AlternateView.CreateAlternateViewFromString(mail.Body, null, "text/html");
            //Agregar imagen
            string rutaBase = AppDomain.CurrentDomain.BaseDirectory;
            string nombreImagen = @"img\logo.jpg";
            string rutaImagen = Path.Combine(rutaBase, nombreImagen);

            LinkedResource theEmailImage = new LinkedResource(rutaImagen);
            theEmailImage.ContentId = "imgFirma";
            //Agregar imagen a la vista alternativa
            htmlView.LinkedResources.Add(theEmailImage);
            //Agregar vista al email
            mail.AlternateViews.Add(htmlView);
            mail.BodyEncoding = UTF8Encoding.UTF8;
            mail.DeliveryNotificationOptions = DeliveryNotificationOptions.OnFailure;

            try
            {
                client.Send(mail);
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

    }
}
