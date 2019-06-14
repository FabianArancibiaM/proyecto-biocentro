<%@ Page Title="" Language="C#" MasterPageFile="~/Biocentro/paginas/MasterPage.Master" AutoEventWireup="true" CodeBehind="HorasDisponibles.aspx.cs" Inherits="CapaServicioPresentacionWeb.Biocentro.paginas.reservar_hora.HorasDisponibles" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <asp:GridView ID="gvHorasDisponibles" class="table" CssClass="table table-striped table-bordered table-sm" runat="server"  AutoGenerateColumns="false" DataKeyNames="Id"  AutoPostBack="false" onrowcommand="gvHorasDisponibles_SelectedIndexChanged">
        <Columns>
            <asp:BoundField DataField="Id" HeaderText="Id" />
            <asp:BoundField DataField="Fecha" HeaderText="Fecha" />
            <asp:BoundField DataField="HoraInicio" HeaderText="Hora Inicio" />
            <asp:BoundField DataField="HoraFin" HeaderText="Hora Fin" />
            <asp:BoundField DataField="Especialidad" HeaderText="Especialidad" />
            <asp:BoundField DataField="NombreTerapeuta" HeaderText="Terapeuta" />
            <asp:ButtonField CommandName="seleccionarBtn" runat="server" Text="S" HeaderText="Secelcionar"/>
            <asp:TemplateField>
                <ItemTemplate>              
                    
                </ItemTemplate>
            </asp:TemplateField>
        </Columns>
    </asp:GridView>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="footer"  runat="server">
</asp:Content>
