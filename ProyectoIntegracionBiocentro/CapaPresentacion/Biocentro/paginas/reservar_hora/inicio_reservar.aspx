<%@ Page Title="" Language="C#" MasterPageFile="~/Biocentro/paginas/master_page.Master" AutoEventWireup="true" CodeBehind="inicio_reservar.aspx.cs" Inherits="CapaPresentacion.Biocentro.paginas.reservar_hora.inicio_reservar" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
<div class="text-center">
        <div class="col-xs-3 col-sm-3 col-md-3 col-lg-3"></div>
        <div class="col-xs-3 col-sm-3 col-md-3 col-lg-3">
            <div>
                <i class="fa fa-calendar-alt fa-4x form-group" aria-hidden="true"></i>
            </div>
            <asp:Button ID="btnMisHoras" class="btn btn-lg btn-primary btn-block" 
                runat="server" Text="Mis horas" OnClick="btnMisHoras_Click" />
        </div>
        <div class="col-xs-3 col-sm-3 col-md-3 col-lg-3">
            <div>
              <i class="fa fa-calendar-plus fa-4x form-group" aria-hidden="true"></i>         
            </div>
            <asp:Button ID="btnInicioReserva" class="btn btn-lg btn-primary btn-block" 
                runat="server" Text="Reservar hora" OnClick="btnInicioReserva_Click" />
        </div>
        <div class="col-xs-3 col-sm-3 col-md-3 col-lg-3"></div>
    </div>
</asp:Content>
