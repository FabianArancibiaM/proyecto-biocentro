<%@ Page Title="" Language="C#" MasterPageFile="~/Biocentro/paginas/MasterPage.Master" AutoEventWireup="true" CodeBehind="MisHoras.aspx.cs" Inherits="CapaServicioPresentacionWeb.Biocentro.paginas.reservar_hora.MisHoras" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style>
        .separacion{
            margin:15px 0px;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="col-xs-12 col-sm-6 col-md-4 col-lg-4 col-centered">
        <div class="form-group" >
            <label for="txtRut">RUT</label>
            <asp:TextBox ID="txtRut" runat="server" CssClass="form-control"></asp:TextBox>
            <label for="txtEmail">Correo electrónico</label>
            <asp:TextBox ID="txtEmail" runat="server" CssClass="form-control"></asp:TextBox>
        </div>
        <asp:Button ID="btnBuscar" class="btn btn-md btn-primary btn-block" 
            runat="server" Text="Buscar" OnClick="btnBuscar_Click"  />
    </div>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="footer" runat="server">
</asp:Content>
