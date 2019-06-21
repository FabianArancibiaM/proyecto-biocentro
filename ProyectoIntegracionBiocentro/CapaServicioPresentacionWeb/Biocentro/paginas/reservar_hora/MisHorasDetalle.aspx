<%@ Page Title="" Language="C#" MasterPageFile="~/Biocentro/paginas/MasterPage.Master" AutoEventWireup="true" CodeBehind="MisHorasDetalle.aspx.cs" Inherits="CapaServicioPresentacionWeb.Biocentro.paginas.reservar_hora.MisHorasDetalle" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <asp:GridView ID="gvwMisHoras" class="table table-design" runat="server"  AutoGenerateColumns="False" DataKeyNames="Id"  AutoPostBack="true">
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
                        <asp:Button ID="btnConfirmar" runat="server" CssClass="btn btn-sm btn-primary" 
                         OnClick="btnConfirmar_Click" Text="Confirmar"/> 
                    </span>                    
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField>
                <ItemTemplate> 
                    <span>    <i class="fa fa-check-square-o"></i>
                        <asp:Button ID="btnAnular" runat="server" CssClass="btn btn-sm btn-danger" 
                         OnClick="btnAnular_Click" Text="Anular"/> 
                    </span>                    
                </ItemTemplate>
            </asp:TemplateField>
        </Columns>
    </asp:GridView>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="footer" runat="server">
</asp:Content>
