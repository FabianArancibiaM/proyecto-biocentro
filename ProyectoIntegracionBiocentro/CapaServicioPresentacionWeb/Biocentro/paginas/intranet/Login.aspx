<%@ Page Title="" Language="C#" MasterPageFile="~/Biocentro/paginas/MasterPage.Master" AutoEventWireup="true" CodeBehind="Login.aspx.cs" Inherits="CapaServicioPresentacionWeb.Biocentro.paginas.intranet.Login" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style>
        .separacion{
            margin:15px 0px;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
<div class="text-center">
        <div class="col-xs-12 col-sm-5 col-md-4 col-lg-4" style="left: 30%;margin-top:40px;">
            <div >
                <asp:Label Text="Usuario"  runat="server" CssClass="separacion"></asp:Label>
                <asp:TextBox runat="server" ID="txtUsuario" CssClass="separacion"></asp:TextBox> 
                <asp:Label runat="server"  Text="Contraseña" CssClass="separacion"></asp:Label>
                <asp:TextBox runat="server" ID="txtContraseña" CssClass="separacion"></asp:TextBox> 
            </div>
            <asp:Button ID="btnLogin" class="btn btn-lg btn-primary btn-block" 
                runat="server" Text="Login" OnClick="btnLogearseClick" />
        </div>
        <div class="col-xs-12 col-sm-1 col-md-2 col-lg-2"></div>
    </div>
</asp:Content>
