<%@ Page Title="" Language="C#" MasterPageFile="~/Biocentro/paginas/master_page.Master" AutoEventWireup="true" CodeBehind="horas_disponibles.aspx.cs" Inherits="CapaPresentacion.Biocentro.paginas.reservar_hora.horas_disponibles" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    Horas disponibles para/con juan perez
    <asp:GridView ID="gvHorasDisponibles" runat="server"  AutoGenerateColumns="False" DataKeyNames="RUT_PER"  AutoPostBack="true">
        <Columns>
            <asp:BoundField DataField="RUT_PER" HeaderText="RUT" />
            <asp:BoundField DataField="NOMBRE_PER" HeaderText="Nombre" />
            <asp:TemplateField>
                <ItemTemplate>
                    <asp:Button ID="btnDetalle" runat="server" CssClass="boton" OnClick="btnDetalle_Click" Text="Detalle" />
                </ItemTemplate>
            </asp:TemplateField>
        </Columns>
    </asp:GridView>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="footer" runat="server">
</asp:Content>
