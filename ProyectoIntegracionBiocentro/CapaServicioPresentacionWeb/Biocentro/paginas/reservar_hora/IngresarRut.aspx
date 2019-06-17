<%@ Page Title="" Language="C#" MasterPageFile="~/Biocentro/paginas/MasterPage.Master" AutoEventWireup="true" CodeBehind="IngresarRut.aspx.cs" Inherits="CapaServicioPresentacionWeb.Biocentro.paginas.reservar_hora.IngresarRut" EnableEventValidation="false" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div>
        <h3 class="text-center">Reservar hora</h3>
        <div class="separador-sm">&nbsp;</div>
        <div class="row">
            <div class="col-xs-12 col-sm-8 col-md-7 col-lg-7 col-centered">
                <div class="col-xs-12 col-sm-6 col-md-6 col-lg-6 form-group">
                    <label for="txtRut">RUT</label>
                    <asp:TextBox ID="txtRut" runat="server" CssClass="form-control"></asp:TextBox>
                    <span class="placeholder">Ingrese su RUT sin puntos ni guión</span>
                </div>
                <div class="col-xs-12 col-sm-6 col-md-6 col-lg-6 form-group">
                    <asp:Button ID="btn_buscar_rut" runat="server" Text="Siguiente" class="btn btn-md btn-primary m-t-lg"
                        OnClick="btn_buscar_rut_Click"/>               
                </div>
            </div>
        </div>
        <div class="separador-sm">&nbsp;</div>
        <div class="m-t-md col-xs-12 col-sm-8 col-md-8 col-lg-8 col-centered">
            <hr />
            <h4>Hora seleccionada</h4>
            <div class="separador-sm">&nbsp;</div>          
            <div class="row">
                <div class="col-xs-12 col-sm-6 col-md-6 col-lg-6 form-group">
                    <label for="lblEspecialidad">Especialidad</label><br />
                    <asp:Label ID="lblEspecialidad" runat="server" ></asp:Label>
                </div>
                <div class="col-xs-12 col-sm-6 col-md-6 col-lg-6">
                    <label for="lblTerapeuta">Terapeuta</label><br />
                    <asp:Label ID="lblTerapeuta" runat="server" ></asp:Label>
                </div>
            </div>
              <div class="row">
                <div class="col-xs-12 col-sm-6 col-md-6 col-lg-6 form-group">
                    <label for="lblFechaHora">Fecha</label><br />
                    <asp:Label ID="lblFechaHora" runat="server" ></asp:Label>
                </div>
                <div class="col-xs-12 col-sm-6 col-md-6 col-lg-6">
                    <label for="lblLugar">Lugar de atención</label><br />
                    <asp:Label ID="lblLugar" runat="server" ></asp:Label>
                </div>
            </div>
            <div class="separador-md">&nbsp;</div>
             <div class="row">
                <div class="col-xs-12 col-sm-3 col-md-3 col-lg-2">
                    <asp:Button ID="volver" class="btn btn-md btn-primary btn-block" OnClick="btnVolver" runat="server" Text="Volver" />
                </div>
            </div>
            <div class="separador-md">&nbsp;</div>
        </div>
    </div>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="footer" runat="server">
</asp:Content>
