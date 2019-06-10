<%@ Page Title="" Language="C#" MasterPageFile="~/Biocentro/paginas/master_page.Master" AutoEventWireup="true" CodeBehind="ingresar_rut.aspx.cs" Inherits="CapaPresentacion.Biocentro.paginas.reservar_hora.ingresar_rut" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div>
        <h3>Reservar hora</h3>
        <div class="row">
            <div class="col-xs-12 col-sm-8 col-md-8 col-lg-8">
                <label for="txtRut">RUT</label><br />
                <asp:TextBox ID="txtRut" runat="server"></asp:TextBox>
                <span class="placeholder">Ingrese su RUT sin puntos ni guión</span>
            </div>
            <div class="col-xs-12 col-sm-4 col-md-4 col-lg-4">
                <button id="btn_buscar_rut" class="btn btn-md btn-primary btn-block">Siguiente</button>
            </div>
        </div>
        <div>
            <h5>Hora seleccionada</h5>
            <div class="row">
                <div class="col-xs-12 col-sm-6 col-md-6 col-lg-6">
                    <label for="fechaHora">Fecha</label>
                    <asp:Label ID="fechaHora" runat="server" Text="09/07/2019 09:00-10:00"></asp:Label>
                </div>
                <div class="col-xs-12 col-sm-6 col-md-6 col-lg-6">
                    <label for="lugar">Lugar de atención</label>
                    <asp:Label ID="lugar" runat="server" Text="Sala 4, Providencia 180, Ñuñoa"></asp:Label>
                </div>
            </div>
            <div class="row">
                <div class="col-xs-12 col-sm-6 col-md-6 col-lg-6">
                    <label for="especialidad">Especialidad</label>
                    <asp:Label ID="especialidad" runat="server" Text="Masoterapia"></asp:Label>
                </div>
                <div class="col-xs-12 col-sm-6 col-md-6 col-lg-6">
                    <label for="terapeuta">Terapeuta</label>
                    <asp:Label ID="terapeuta" runat="server" Text="Juan Perez Soto"></asp:Label>
                </div>
            </div>
            <asp:Button ID="volver" class="btn btn-md btn-primary btn-block" runat="server" Text="Volver" />
        </div>
    </div>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="footer" runat="server">
</asp:Content>
