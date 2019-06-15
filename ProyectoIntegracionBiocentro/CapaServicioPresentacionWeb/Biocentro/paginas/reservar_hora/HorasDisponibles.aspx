<%@ Page Title="" Language="C#" MasterPageFile="~/Biocentro/paginas/MasterPage.Master" AutoEventWireup="true" CodeBehind="HorasDisponibles.aspx.cs" Inherits="CapaServicioPresentacionWeb.Biocentro.paginas.reservar_hora.HorasDisponibles" EnableEventValidation="false" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    Horas Disponibles
    <asp:GridView ID="gvHorasDisponibles" class="table" runat="server"  AutoGenerateColumns="False" DataKeyNames="Id"  AutoPostBack="true">
        <Columns>
            <asp:BoundField DataField="Id" HeaderText="Id" />
            <asp:BoundField DataField="Fecha" HeaderText="Fecha" />
            <asp:BoundField DataField="HoraInicio" HeaderText="Hora Inicio" />
            <asp:BoundField DataField="HoraFin" HeaderText="Hora Fin" />
            <asp:BoundField DataField="Especialidad" HeaderText="Especialidad" />
            <asp:BoundField DataField="NombreTerapeuta" HeaderText="Terapeuta" />
            <asp:TemplateField>
                <ItemTemplate> 
                    <span>    <i class="fa fa-check-square-o"></i>
                        <asp:Button ID="btnSeleccionar" runat="server" CssClass="btn btn-sm" 
                        OnClick="btnSeleccionar_Click" Text=""/> 
                    </span>                    
                </ItemTemplate>
            </asp:TemplateField>
        </Columns>
    </asp:GridView>
    <div class="row">
                <div class="col-xs-12 col-sm-3 col-md-3 col-lg-2">
                    <asp:Button ID="volver" class="btn btn-md btn-primary btn-block" OnClick="btnVolver" runat="server" Text="Volver" />
                </div>
            </div>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="footer" runat="server">
</asp:Content>
