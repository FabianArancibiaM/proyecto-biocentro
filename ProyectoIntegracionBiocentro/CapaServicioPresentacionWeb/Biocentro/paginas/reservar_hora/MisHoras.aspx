<%@ Page Title="" Language="C#" MasterPageFile="~/Biocentro/paginas/MasterPage.Master" AutoEventWireup="true" CodeBehind="MisHoras.aspx.cs" Inherits="CapaServicioPresentacionWeb.Biocentro.paginas.reservar_hora.MisHoras" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="col-xs-12 col-sm-6 col-md-4 col-lg-4 col-centered">
        <div class="separador-lg"></div>
        <h3 class="text-center">Mis horas</h3>
        <div class="separador-sm"></div>
        <div class="form-group" >
            <label for="txtRut">RUT</label>
            <asp:TextBox ID="txtRut" runat="server" CssClass="form-control"></asp:TextBox>
            <div class="separador-sm"></div>
            <label for="txtEmail">Correo electrónico</label>
            <asp:TextBox ID="txtEmail" runat="server" CssClass="form-control"></asp:TextBox>
        </div>
        <div class="separador-md"></div>
        <asp:Button ID="btnBuscar" class="btn btn-md btn-primary btn-block" 
            runat="server" Text="Buscar" OnClick="btnBuscar_Click"  />
    </div>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="footer" runat="server">
</asp:Content>
