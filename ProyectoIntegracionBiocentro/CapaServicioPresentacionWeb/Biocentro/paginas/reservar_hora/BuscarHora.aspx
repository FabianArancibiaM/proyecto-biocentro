<%@ Page Title="" Language="C#" MasterPageFile="~/Biocentro/paginas/MasterPage.Master" AutoEventWireup="true" CodeBehind="BuscarHora.aspx.cs" Inherits="CapaServicioPresentacionWeb.Biocentro.paginas.reservar_hora.BuscarHora" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="col-xs-3 col-sm-3 col-md-3 col-lg-3"></div>
    <div class="col-xs-6 col-sm-6 col-md-6 col-lg-6">
        <div class="row m-b-md">
            <div class="col-xs-6 col-sm-6 col-md-6 col-lg-6">
                <asp:button id="btnEspecialidad" runat="server" OnClick="botonEspecialidadClick" class="btn btn-md btn-primary btn-block form-group" Text="Por Especialidad">
                </asp:button>
             </div>
             <div class="col-xs-6 col-sm-6 col-md-6 col-lg-6">
                  <asp:button id="btnTerapeuta" runat="server" OnClick="botonEspecialistaClick" class="btn btn-md btn-primary btn-block form-group" Text="Por Terapeuta">
                </asp:button>
            </div>
        </div>
        <div class="row text-center form-group">
            <asp:DropDownList ID="ddlEspecialidad" CssClass="form-control" runat="server" ClientIDMode="Static"></asp:DropDownList>
            <asp:DropDownList ID="ddlTerapeuta" CssClass="form-control" runat="server" ClientIDMode="Static"></asp:DropDownList>
        </div>
        <div class="row text-center form-group">
            <asp:Button ID="btnBuscar" runat="server" Text="Buscar" OnClick="btnBuscar_Click"  class="btn btn-md btn-primary form-group" />
        </div>
    </div>

    <div class="col-xs-3 col-sm-3 col-md-3 col-lg-3">

    </div>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="footer" runat="server">
</asp:Content>
