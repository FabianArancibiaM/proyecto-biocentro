﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace CapaServicioPresentacionWeb.Biocentro.paginas.reservar_hora
{
    public partial class InicioReserva : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }
        protected void btnMisHoras_Click(object sender, EventArgs e)
        {
            
            try
            {
                Response.Write("<script language='javascript'>window.location='../mis_horas/inicio_mis_horas.aspx';</script>");
            }
            catch (Exception ex)
            {
                ShowMessage("Error al intentar cargar la pagina");
            }
        }

        protected void btnInicioReserva_Click(object sender, EventArgs e)
        {
            
            try
            {
                Response.Write("<script language='javascript'>window.location='BuscarHora.aspx';</script>");
            }
            catch (Exception ex)
            {
                ShowMessage("Error al intentar cargar la pagina");
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