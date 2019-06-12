using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace CapaPresentacion.Biocentro.paginas.reservar_hora
{
    public partial class horas_disponibles : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {/*
            List<HoraAtencion> horas = (List <HoraAtencion>)  Session["horas"];
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
            gvHorasDisponibles.DataBind();*/
        }
        protected void btnSeleccionar_Click(object sender, EventArgs e)
        {

        }        
    }
}