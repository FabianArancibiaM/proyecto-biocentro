<%@ Page Title="" Language="C#" MasterPageFile="~/Biocentro/paginas/MasterPage.Master" AutoEventWireup="true" CodeBehind="InicioReserva.aspx.cs" Inherits="CapaServicioPresentacionWeb.Biocentro.paginas.reservar_hora.InicioReserva" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="row text-center">
        <div class="separador-lg">&nbsp;</div>
        <div class="col-xs-12 col-sm-11 col-md-9 col-lg-9 col-centered">
            <div class="col-xs-12 col-sm-5 col-md-5 col-lg-5 form-group">
                <div>
                    <i class="fa fa-calendar-alt fa-8x form-group" aria-hidden="true"></i>
                </div>
                <asp:Button ID="btnMisHoras" class="btn btn-lg btn-primary btn-block" 
                    runat="server" Text="Mis horas" OnClick="btnMisHoras_Click" />
            </div>
            <div class="col-xs-12 col-sm-2 col-md-2 col-lg-2"></div>
            <div class="col-xs-12 col-sm-5 col-md-5 col-lg-5 form-group">
                <div>
                  <i class="fa fa-calendar-plus fa-8x form-group" aria-hidden="true"></i>         
                </div>
                <asp:Button ID="btnInicioReserva" class="btn btn-lg btn-primary btn-block" 
                    runat="server" Text="Reservar hora" OnClick="btnInicioReserva_Click" />
            </div>
        </div>
    </div>
</asp:Content>
