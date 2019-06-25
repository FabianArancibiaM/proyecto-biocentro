<%@ Page Title="" Language="C#" MasterPageFile="~/Biocentro/paginas/MasterPage.Master" AutoEventWireup="true" CodeBehind="PaginaPrincipal.aspx.cs" Inherits="CapaServicioPresentacionWeb.Biocentro.paginas.intranet.PaginaPrincipal" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">        
    <div class="separador-md"></div>
    <h3 class=" text-center"><asp:Label runat="server" ID="msjBienvenida"></asp:Label></h3>
    <div class="separador-lg"></div>       
    <div class="col-xs-12 col-sm-8 col-md-4 col-lg-4 col-centered text-center">
        <i class="fa fa-balance-scale fa-8x form-group" aria-hidden="true"></i>       
        <asp:Button ID="btnRegistrarPagos" class="btn btn-lg btn-primary btn-block" 
        runat="server" Text="Registrar Pago" OnClick="btnRegistrarPago_Click" />
    </div>
</asp:Content>
