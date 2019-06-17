<%@ Page Title="" Language="C#" MasterPageFile="~/Biocentro/paginas/MasterPage.Master" AutoEventWireup="true" CodeBehind="BuscarHora.aspx.cs" Inherits="CapaServicioPresentacionWeb.Biocentro.paginas.reservar_hora.BuscarHora" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="separador-md">&nbsp;</div>
    <h3 class="text-center">Buscar horas</h3>
    <div class="col-xs-5 col-sm-5 col-md-5 col-lg-5 text-center col-centered">
        <div class="row m-b-md">
            <div class="btn-group form-group">
                <asp:button ID="btnEspecialidad" runat="server" OnClick="botonEspecialidadClick" CssClass="btn btn-md btn-success" 
                    Text="Por Especialidad" ClientIDMode="Static"></asp:button>
                <asp:button ID="btnTerapeuta" runat="server" OnClick="botonEspecialistaClick" CssClass="btn btn-md btn-basic" 
                    Text="Por Terapeuta" ClientIDMode="Static"></asp:button>
            </div>
            <div class="separador-sm">&nbsp;</div>
            <asp:DropDownList ID="ddlEspecialidad" class="form-control" runat="server" ClientIDMode="Static"></asp:DropDownList>
            <asp:DropDownList ID="ddlTerapeuta" class="form-control" runat="server" ClientIDMode="Static"></asp:DropDownList>
       </div>       
        <div class="separador-md">&nbsp;</div>
        <div class="row form-group">
            <asp:Button ID="btnBuscar" runat="server" Text="Buscar" OnClick="btnBuscar_Click"  class="btn btn-md btn-primary form-group" />
        </div>
    </div>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="footer" runat="server">
</asp:Content>
