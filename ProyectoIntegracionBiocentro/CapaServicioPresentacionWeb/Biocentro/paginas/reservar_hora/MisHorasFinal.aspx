<%@ Page Title="" Language="C#" MasterPageFile="~/Biocentro/paginas/MasterPage.Master" AutoEventWireup="true" CodeBehind="MisHorasFinal.aspx.cs" Inherits="CapaServicioPresentacionWeb.Biocentro.paginas.reservar_hora.MisHorasFinal" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="m-t-md col-xs-12 col-sm-10 col-md-10 col-lg-10 col-centered"> 
        <div class="separador-sm"></div>
        <h3 class="text-center">¡Hora <asp:Label ID="lblConfirmacionHora" runat="server"></asp:Label> exitosamente!</h3>
        <div class="separador-sm">&nbsp;</div>
        <h4>Reserva N°<asp:Label ID="lblIdHora" runat="server"></asp:Label></h4>
        <div class="separador-sm">&nbsp;</div>
        <div class="row">
            <div class="col-xs-12 col-sm-6 col-md-6 col-lg-6 form-group">
                <label for="lblEspecialidad">Especialidad</label><br />
                <asp:Label ID="lblEspecialidad" runat="server" ></asp:Label>
            </div>
            <div class="col-xs-12 col-sm-6 col-md-6 col-lg-6">
                <label for="lblTerapeuta">Terapeuta</label><br />
                <asp:Label ID="lblTerapeuta" runat="server" ></asp:Label>
            </div>
        </div>
        <div class="row">
            <div class="col-xs-12 col-sm-6 col-md-6 col-lg-6 form-group">
                <label for="lblFechaHora">Fecha</label><br />
                <asp:Label ID="lblFechaHora" runat="server" ></asp:Label>
            </div>
            <div class="col-xs-12 col-sm-6 col-md-6 col-lg-6">
                <label for="lblLugar">Lugar de atención</label><br />
                <asp:Label ID="lblLugar" runat="server" ></asp:Label>
            </div>
        </div>    
        <div class="separador-md"></div>
        <div class="row">
            <div class="col-xs-12 col-sm-3 col-md-3 col-lg-2">
                <asp:Button ID="volver" class="btn btn-md btn-primary btn-block" OnClick="btnMisHoras" runat="server" Text="Ir a Mis horas" />
            </div>
        </div>
    </div>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="footer" runat="server">
</asp:Content>
