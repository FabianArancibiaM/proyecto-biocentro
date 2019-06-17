<%@ Page Title="" Language="C#" MasterPageFile="~/Biocentro/paginas/MasterPage.Master" AutoEventWireup="true" CodeBehind="DetalleHora.aspx.cs" Inherits="CapaServicioPresentacionWeb.Biocentro.paginas.reservar_hora.DetalleHora" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">   
        <div class="m-t-md col-xs-12 col-sm-10 col-md-10 col-lg-10 col-centered"> 
             <h3 class="text-center">¡Hora reservada exitosamente!</h3>
             <div class="separador-sm">&nbsp;</div>
             <h4 class="tex-center"><asp:Label ID="lblIdHora" runat="server" Text=""></asp:Label></h4>
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
            <div class="row">
                <div class="col-xs-12 col-sm-6 col-md-6 col-lg-6 form-group">
                    <label for="lblPaciente">Paciente</label><br />
                    <asp:Label ID="lblPaciente" runat="server" ></asp:Label>
                </div>
            </div>   
            <div class="separador-sm">&nbsp;</div>
            <i><asp:Label ID="lblEmail" runat="server" Text=""></asp:Label></i>
            <div class="separador-md">&nbsp;</div>
            <div class="row">
                <div class="col-xs-12 col-sm-4 col-md-4 col-lg-3">
                    <asp:Button ID="btnMisHoras" class="btn btn-md btn-primary btn-block" OnClick="btnMisHorasOnclick" runat="server" Text="Ir a Mis horas" />
                </div>
            </div>     
            <div class="separador-sm">&nbsp;</div>
        </div>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="footer" runat="server">
</asp:Content>
