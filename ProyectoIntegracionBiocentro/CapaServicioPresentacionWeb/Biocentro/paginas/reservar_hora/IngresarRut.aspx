<%@ Page Title="" Language="C#" MasterPageFile="~/Biocentro/paginas/MasterPage.Master" AutoEventWireup="true" CodeBehind="IngresarRut.aspx.cs" Inherits="CapaServicioPresentacionWeb.Biocentro.paginas.reservar_hora.IngresarRut" EnableEventValidation="false" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div>
        <h3>Reservar hora</h3>
        <div class="row">
            <div class="col-xs-12 col-sm-8 col-md-6 col-lg-6">
                <label for="txtRut">RUT</label>
                <br />
                <asp:TextBox ID="txtRut" runat="server"></asp:TextBox><asp:Button ID="btn_buscar_rut" runat="server" Text="Siguiente" class="btn btn-md btn-primary btn-block" OnClick="btn_buscar_rut_Click"/>
                <br />
                <span class="placeholder">Ingrese su RUT sin puntos ni guión</span>
            </div>
        </div>
        <div class="m-t-md">
            <h4>Hora seleccionada</h4>
            <div class="row">
                <div class="col-xs-12 col-sm-6 col-md-6 col-lg-6">
                    <label for="lblFechaHora">Fecha</label><br />
                    <asp:Label ID="lblFechaHora" runat="server" Text="09/07/2019 09:00-10:00"></asp:Label>
                </div>
                <div class="col-xs-12 col-sm-6 col-md-6 col-lg-6">
                    <label for="lblLugar">Lugar de atención</label><br />
                    <asp:Label ID="lblLugar" runat="server" Text="Sala 4, Providencia 180, Ñuñoa"></asp:Label>
                </div>
            </div>
            <div class="row">
                <div class="col-xs-12 col-sm-6 col-md-6 col-lg-6">
                    <label for="lblEspecialidad">Especialidad</label><br />
                    <asp:Label ID="lblEspecialidad" runat="server" Text="Masoterapia"></asp:Label>
                </div>
                <div class="col-xs-12 col-sm-6 col-md-6 col-lg-6">
                    <label for="lblTerapeuta">Terapeuta</label><br />
                    <asp:Label ID="lblTerapeuta" runat="server" Text="Juan Perez Soto"></asp:Label>
                </div>
            </div>
             <div class="row">
                <div class="col-xs-12 col-sm-3 col-md-3 col-lg-2">
                    <asp:Button ID="volver" class="btn btn-md btn-primary btn-block" runat="server" Text="Volver" />
                </div>
            </div>
        </div>
    </div>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="footer" runat="server">
</asp:Content>
