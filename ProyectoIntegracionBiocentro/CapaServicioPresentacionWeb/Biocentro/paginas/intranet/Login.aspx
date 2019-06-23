<%@ Page Title="" Language="C#" MasterPageFile="~/Biocentro/paginas/MasterPage.Master" AutoEventWireup="true" CodeBehind="Login.aspx.cs" Inherits="CapaServicioPresentacionWeb.Biocentro.paginas.intranet.Login" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style>
        .separacion{
            margin:15px 0px;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="col-xs-12 col-sm-5 col-md-4 col-lg-4 col-centered">
        <div class="form-group">
            <asp:Label Text="Usuario" runat="server" CssClass="label separacion"></asp:Label><br />
            <asp:TextBox runat="server" ID="txtUsuario" CssClass="form-control separacion"></asp:TextBox> <br />
            <asp:Label runat="server"  Text="Contraseña" CssClass="label separacion"></asp:Label><br />
            <asp:TextBox runat="server" ID="txtContraseña" CssClass="form-control separacion" TextMode="Password"></asp:TextBox> <br />
        </div>
        <asp:Button ID="btnLogin" class="btn btn-md btn-primary btn-block" 
            runat="server" Text="Ingresar" OnClick="btnLogearseClick" />
    </div>
    <div class="col-xs-12 col-sm-1 col-md-2 col-lg-2"></div>
</asp:Content>
