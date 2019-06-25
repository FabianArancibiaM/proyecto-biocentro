<%@ Page Title="" Language="C#" MasterPageFile="~/Biocentro/paginas/MasterPage.Master" AutoEventWireup="true" CodeBehind="MisHorasDetalle.aspx.cs" Inherits="CapaServicioPresentacionWeb.Biocentro.paginas.reservar_hora.MisHorasDetalle" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="separador-lg"></div>
    <h3 class="text-center">Mis horas</h3>
    <div class="separador-sm"></div>
    <div class="row">
        <div class="col-xs-12 col-sm-6 col-md-6 col-lg-6 form-group">
            <label for="lblPaciente">Nombre</label>
            <asp:Label ID="lblPaciente" runat="server" ></asp:Label><br />
            <label for="lblRut">RUT</label>
            <asp:Label ID="lblRut" runat="server" ></asp:Label>
        </div>
    </div>  
    <div class="separador-sm"></div>
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
                    <b><asp:Label ID="lblConfirmada" runat="server" Enabled="false" Text='<%# Eval("Confirmada").ToString() == "2"? "Confirmada" : "" %>' /></b>
                    <asp:Button ID="btnConfirmar" runat="server" CssClass="btn btn-sm btn-primary" 
                        OnClick="btnConfirmar_Click" Text="Confirmar" Visible='<%# Eval("Confirmada").ToString() == "2"? false : true %>'/> 
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField>
                <ItemTemplate> 
                    <asp:Button ID="btnAnular" runat="server" CssClass="btn btn-sm btn-danger" 
                        OnClick="btnAnular_Click" Text="Anular" Visible='<%# Eval("Confirmada").ToString() == "2"? false : true %>'/> 
                </ItemTemplate>
            </asp:TemplateField>
        </Columns>
    </asp:GridView>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="footer" runat="server">
</asp:Content>
