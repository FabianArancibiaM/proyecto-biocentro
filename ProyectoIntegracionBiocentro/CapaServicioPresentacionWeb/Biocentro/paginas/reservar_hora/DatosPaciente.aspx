<%@ Page Title="" Language="C#" MasterPageFile="~/Biocentro/paginas/MasterPage.Master" AutoEventWireup="true" CodeBehind="DatosPaciente.aspx.cs" Inherits="CapaServicioPresentacionWeb.Biocentro.paginas.reservar_hora.DatosPaciente" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="m-t-md">
            <h2>Datos paciente</h2>
            <div class="row">
                <div class="col-xs-12 col-sm-6 col-md-6 col-lg-6">
                    <label for="txtRut">RUT</label><br />
                    <asp:TextBox ID="txtRut" runat="server"></asp:TextBox>
                </div>
                <div class="col-xs-12 col-sm-6 col-md-6 col-lg-6">
                    <label for="txtNombre">Nombres</label><br />
                    <asp:TextBox ID="txtNombre" runat="server"></asp:TextBox>
                </div>
            </div>
            <div class="row">
               <div class="col-xs-12 col-sm-6 col-md-6 col-lg-6">
                    <label for="txtApPaterno">Apellido Paterno</label><br />
                    <asp:TextBox ID="txtApPaterno" runat="server"></asp:TextBox>
                </div>
                <div class="col-xs-12 col-sm-6 col-md-6 col-lg-6">
                    <label for="txtApMaterno">Apellido Materno</label><br />
                    <asp:TextBox ID="txtApMaterno" runat="server"></asp:TextBox>
                </div>
            </div>
            <div class="row">
                 <div class="col-xs-12 col-sm-6 col-md-6 col-lg-6">
                    <label for="fechaNac">Fecha Nacimiento</label><br />
                     <input type="text" value="9/23/2009" style="width: 100px;" runat="server"
                         name="fechaNac" id="fechaNac" class="hasDatepicker"/>
                </div>
                <div class="col-xs-12 col-sm-6 col-md-6 col-lg-6">
                    <label for="radioSexo">Sexo</label><br />
                    <asp:RadioButton ID="radioHombre" runat="server" GroupName="radioSexo" Text="Hombre"/>
                    <asp:RadioButton ID="radioMujer" runat="server" GroupName="radioSexo"  Text="Mujer" />
                    <asp:RadioButton ID="radioOtro" runat="server" GroupName="radioSexo"  Text="Otro"/>
                </div>
            </div>
             <div class="row">
               <div class="col-xs-12 col-sm-6 col-md-6 col-lg-6">
                    <label for="txtCorreo">Correo electrónico</label><br />
                    <asp:TextBox ID="txtCorreo" runat="server"></asp:TextBox>
                </div>
                <div class="col-xs-12 col-sm-6 col-md-6 col-lg-6">
                    <label for="txtTelefono">Teléfono</label><br />
                    <asp:TextBox ID="txtTelefono" runat="server"></asp:TextBox>
                </div>
            </div>
             <div class="row text-center">
                <asp:Button ID="btnReservar" runat="server" Text="Reservar" OnClick="btnReservar_Click"  class="btn btn-md btn-primary form-group" />
            </div>
        </div>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="footer" runat="server">
</asp:Content>
