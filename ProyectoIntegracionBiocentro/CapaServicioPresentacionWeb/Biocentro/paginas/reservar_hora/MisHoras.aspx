<%@ Page Title="" Language="C#" MasterPageFile="~/Biocentro/paginas/MasterPage.Master" AutoEventWireup="true" CodeBehind="MisHoras.aspx.cs" Inherits="CapaServicioPresentacionWeb.Biocentro.paginas.reservar_hora.MisHoras" %>
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
                <asp:Label Text="Rut"  runat="server" CssClass="separacion"></asp:Label>
                <asp:TextBox runat="server" ID="txtRut" CssClass="separacion"></asp:TextBox> 
                <asp:Label runat="server"  Text="Email" CssClass="separacion"></asp:Label>
                <asp:TextBox runat="server" ID="txtEmail" CssClass="separacion"></asp:TextBox> 
            </div>
            <asp:Button ID="btnBuscar" class="btn btn-lg btn-primary btn-block" 
                runat="server" Text="Buscar" OnClick="btnBuscar_Click"  />
        </div>
        <div class="col-xs-12 col-sm-1 col-md-2 col-lg-2"></div>
    </div>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="footer" runat="server">
</asp:Content>
