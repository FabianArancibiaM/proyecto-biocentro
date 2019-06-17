<%@ Page Title="" Language="C#" MasterPageFile="~/Biocentro/paginas/MasterPage.Master" AutoEventWireup="true" CodeBehind="DatosPaciente.aspx.cs" Inherits="CapaServicioPresentacionWeb.Biocentro.paginas.reservar_hora.DatosPaciente" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="col-xs-12 col-sm-12 col-md-10 col-lg-10 m-t-md col-centered">
        <h3 class="text-center">Datos paciente</h3>
        <div class="separador-sm">&nbsp;</div>
        <div class="row">
            <div class="col-xs-12 col-sm-6 col-md-6 col-lg-6 form-group">
                <label for="txtRut">RUT</label><br />
                <asp:TextBox ID="txtRut" runat="server" CssClass="form-control"></asp:TextBox>
            </div>
            <div class="col-xs-12 col-sm-6 col-md-6 col-lg-6 form-group">
                <label for="txtNombre">Nombres</label><br />
                <asp:TextBox ID="txtNombre" runat="server" CssClass="form-control"></asp:TextBox>
            </div>
        </div>
        <div class="row">
            <div class="col-xs-12 col-sm-6 col-md-6 col-lg-6 form-group">
                <label for="txtApPaterno">Apellido Paterno</label><br />
                <asp:TextBox ID="txtApPaterno" runat="server" CssClass="form-control"></asp:TextBox>
            </div>
            <div class="col-xs-12 col-sm-6 col-md-6 col-lg-6 form-group">
                <label for="txtApMaterno">Apellido Materno</label><br />
                <asp:TextBox ID="txtApMaterno" runat="server" CssClass="form-control"></asp:TextBox>
            </div>
        </div>
        <div class="row">
            <div class="col-xs-12 col-sm-6 col-md-6 col-lg-4 form-group">
                <label for="fechaNac">Fecha Nacimiento</label><br />
                <asp:TextBox ID="fechaNac" runat="server" TextMode="Date" CssClass="form-control"></asp:TextBox>
            </div>
            <div class="col-lg-2"> </div>
            <div class="col-xs-12 col-sm-6 col-md-6 col-lg-6 form-group">
                <label for="radioSexo">Sexo</label><br />
                <asp:RadioButton ID="radioHombre" runat="server" GroupName="radioSexo" Text=" Hombre"/>
                <asp:RadioButton ID="radioMujer" runat="server" GroupName="radioSexo" Text=" Mujer"/>
            </div>           
        </div>
            <div class="row">
            <div class="col-xs-12 col-sm-6 col-md-6 col-lg-6 form-group">
                <label for="txtCorreo">Correo electrónico</label><br />
                <asp:TextBox ID="txtCorreo" runat="server" CssClass="form-control"></asp:TextBox>
            </div>
            <div class="col-xs-12 col-sm-6 col-md-6 col-lg-4 form-group">
                <label for="txtTelefono">Teléfono</label><br />
                <asp:TextBox ID="txtTelefono" runat="server" CssClass="form-control"></asp:TextBox>
            </div>
            </div>
            <div class="separador-sm">&nbsp;</div>
            <div class="row text-center">
            <asp:Button ID="btnReservar" runat="server" Text="Reservar" OnClick="btnReservar_Click"  class="btn btn-md btn-primary form-group" />
        </div>
        <div class="separador-sm">&nbsp;</div>
    </div>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="footer" runat="server">
</asp:Content>
