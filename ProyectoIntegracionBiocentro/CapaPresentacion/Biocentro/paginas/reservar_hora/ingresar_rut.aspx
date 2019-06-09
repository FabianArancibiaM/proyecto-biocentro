<%@ Page Title="" Language="C#" MasterPageFile="~/Biocentro/paginas/master_page.Master" AutoEventWireup="true" CodeBehind="ingresar_rut.aspx.cs" Inherits="CapaPresentacion.Biocentro.paginas.reservar_hora.ingresar_rut" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div>
        <h3>Reservar hora</h3>
        <div class="row">
            <div class="col-xs-12 col-sm-8 col-md-8 col-lg-8">
                <label for="txt_rut">RUT</label><br />
                <input type="text" id="txt_rut"/>
                <span></span>
            </div>
            <div class="col-xs-12 col-sm-4 col-md-4 col-lg-4">
                <button id="btn_buscar_rut" class="btn btn-md btn-primary btn-block">Siguiente</button>
            </div>
        </div>
        <div>
            <h5>Hora seleccionada</h5>
            <div class="row">
                <div class="col-xs-12 col-sm-6 col-md-6 col-lg-6">
                    <label for="fecha_hora">Fecha</label>
                    <p id="fecha_hora"></p>
                </div>
                <div class="col-xs-12 col-sm-6 col-md-6 col-lg-6">
                    <label for="lugar">Lugar de atención</label>
                    <p id="lugar"></p>
                </div>
            </div>
            <div class="row">
                <div class="col-xs-12 col-sm-6 col-md-6 col-lg-6">
                    <label for="especialidad">Especialidad</label>
                    <p id="especialidad"></p>
                </div>
                <div class="col-xs-12 col-sm-6 col-md-6 col-lg-6">
                    <label for="terapeuta">Terapeuta</label>
                    <p id="terapeuta"></p>
                </div>
            </div>
            <button class="btn btn-md btn-primary btn-block">Volver</button>
        </div>
    </div>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="footer" runat="server">
</asp:Content>
