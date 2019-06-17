<%@ Page Title="" Language="C#" MasterPageFile="~/Biocentro/paginas/MasterPage.Master" AutoEventWireup="true" CodeBehind="HorasDisponibles.aspx.cs" Inherits="CapaServicioPresentacionWeb.Biocentro.paginas.reservar_hora.HorasDisponibles" EnableEventValidation="false" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <h3 class="text-center">Horas Disponibles<asp:Label ID="lblTitulo" runat="server" Text=""></asp:Label></h3>
    <div class="separador-sm">&nbsp;</div>
    <div class="col-xs-11 col-sm-11 col-md-11 col-lg-11 text-center">
        <asp:GridView ID="gvHorasDisponibles" CssClass="table-design table-responsive" runat="server"  AutoGenerateColumns="False" 
        DataKeyNames="Id"  AutoPostBack="true">
            <Columns>
                <asp:BoundField DataField="Id" HeaderText="Id" />
                <asp:BoundField DataField="Fecha" HeaderText="Fecha" />
                <asp:BoundField DataField="HoraInicio" HeaderText="Hora Inicio" />
                <asp:BoundField DataField="HoraFin" HeaderText="Hora Fin" />
                <asp:BoundField DataField="Especialidad" HeaderText="Especialidad" />
                <asp:BoundField DataField="NombreTerapeuta" HeaderText="Terapeuta" />
                <asp:TemplateField>
                    <ItemTemplate> 
                        <b><span>
                            <asp:Button ID="btnSeleccionar" runat="server" CssClass="btn btn-sm btn-primary" 
                            OnClick="btnSeleccionar_Click" Text="Seleccionar"/> 
                        </span></b>                        
                    </ItemTemplate>
                </asp:TemplateField>
            </Columns>
        </asp:GridView>
        <div class="separador-md">&nbsp;</div>
        <div class="row">
            <div class="col-xs-12 col-sm-3 col-md-3 col-lg-2">
                <asp:Button ID="volver" class="btn btn-md btn-primary btn-block" OnClick="btnVolver" runat="server" Text="Volver" />
            </div>
        </div>
    <div class="separador-md">&nbsp;</div>
    </div>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="footer" runat="server">
</asp:Content>
