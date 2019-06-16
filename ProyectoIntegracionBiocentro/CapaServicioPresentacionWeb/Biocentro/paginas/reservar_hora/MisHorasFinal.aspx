<%@ Page Title="" Language="C#" MasterPageFile="~/Biocentro/paginas/MasterPage.Master" AutoEventWireup="true" CodeBehind="MisHorasFinal.aspx.cs" Inherits="CapaServicioPresentacionWeb.Biocentro.paginas.reservar_hora.MisHorasFinal" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
     <div>
        <div class="m-t-md">
            <h4>Hora número
                <asp:Label ID="lblIdHora" runat="server"></asp:Label>
&nbsp;ha sido
                <asp:Label ID="lblConfirmacionHora" runat="server"></asp:Label>
&nbsp;con éxito!</h4>
            <div class="row">
                <div class="col-xs-12 col-sm-6 col-md-6 col-lg-6">
                    <label for="lblFechaHora">Fecha</label><br />
                    <asp:Label ID="lblFechaHora" runat="server" Text=""></asp:Label>
                </div>
                <div class="col-xs-12 col-sm-6 col-md-6 col-lg-6">
                    <label for="lblLugar">Lugar de atención</label><br />
                    <asp:Label ID="lblLugar" runat="server" Text=""></asp:Label>
                </div>
            </div>
            <div class="row">
                <div class="col-xs-12 col-sm-6 col-md-6 col-lg-6">
                    <label for="lblEspecialidad">Especialidad</label><br />
                    <asp:Label ID="lblEspecialidad" runat="server" Text=""></asp:Label>
                </div>
                <div class="col-xs-12 col-sm-6 col-md-6 col-lg-6">
                    <label for="lblTerapeuta">Terapeuta</label><br />
                    <asp:Label ID="lblTerapeuta" runat="server" Text=""></asp:Label>
                </div>
            </div>
             <div class="row">
                <div class="col-xs-12 col-sm-3 col-md-3 col-lg-2">
                    <asp:Button ID="volver" class="btn btn-md btn-primary btn-block" runat="server" Text="Volver" />
                </div>
            </div>
        </div>
    </div>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="footer" runat="server">
</asp:Content>
