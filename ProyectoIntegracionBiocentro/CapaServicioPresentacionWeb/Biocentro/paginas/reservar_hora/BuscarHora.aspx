<%@ Page Title="" Language="C#" MasterPageFile="~/Biocentro/paginas/MasterPage.Master" AutoEventWireup="true" CodeBehind="BuscarHora.aspx.cs" Inherits="CapaServicioPresentacionWeb.Biocentro.paginas.reservar_hora.BuscarHora" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <script>
        $(document).on('click', 'btnEspecialidad', function () {
            alert('btnEspecialidad');
            $('#ddlEspecialidad').removeClass('hidden');
            $('#ddlTerapeuta').addClass('hidden');
        });
        $(document).on('click', 'btnTerapeuta', function () {
            alert('btnTerapeuta');
            $('#ddlTerapeuta').removeClass('hidden');
            $('#ddlEspecialidad').addClass('hidden');
        });
    </script>
    <div class="col-xs-3 col-sm-3 col-md-3 col-lg-3"></div>
    <div class="col-xs-6 col-sm-6 col-md-6 col-lg-6">
        <div class="row">
            <div class="col-xs-6 col-sm-6 col-md-6 col-lg-6">
                <span id="btnEspecialidad"  class="btn btn-md btn-primary btn-block form-group">
                Por Especialidad</span>
             </div>
             <div class="col-xs-6 col-sm-6 col-md-6 col-lg-6">
                <span id="btnTerapeuta"  class="btn btn-md btn-primary btn-block form-group">
                Por Terapeuta</span>
            </div>
        </div>
        <div class="row text-center form-group">
            <asp:DropDownList ID="ddlEspecialidad" CssClass="form-control" runat="server" ClientIDMode="Static"></asp:DropDownList>
            <asp:DropDownList ID="ddlTerapeuta" CssClass="hidden form-control" runat="server" ClientIDMode="Static"></asp:DropDownList>
        </div>
        <div class="row text-center form-group">
            <asp:Button ID="btnBuscar" runat="server" Text="Buscar" OnClick="btnBuscar_Click"/>
        </div>
    </div>

    <div class="col-xs-3 col-sm-3 col-md-3 col-lg-3">

    </div>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="footer" runat="server">
</asp:Content>
