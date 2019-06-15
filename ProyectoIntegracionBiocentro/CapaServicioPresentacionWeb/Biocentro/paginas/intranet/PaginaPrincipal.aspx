<%@ Page Title="" Language="C#" MasterPageFile="~/Biocentro/paginas/MasterPage.Master" AutoEventWireup="true" CodeBehind="PaginaPrincipal.aspx.cs" Inherits="CapaServicioPresentacionWeb.Biocentro.paginas.intranet.PaginaPrincipal" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
<div class="text-center">
        
        <div class="col-xs-12 col-sm-5 col-md-7 col-lg-4" style="left:30%">
            <div  style="margin:15px 0px;text-align:center; ">
            <asp:Label runat="server" ID="msjBienvenida"></asp:Label>
        </div>
            <div>
                <i class="fa fa-calendar-alt fa-4x form-group" aria-hidden="true"></i>
            </div>
            <asp:Button ID="btnRegistrarPagos" class="btn btn-lg btn-primary btn-block" 
                runat="server" Text="Registrar Pago" OnClick="btnRegistrarPago_Click" />
        </div>
    </div>
</asp:Content>
