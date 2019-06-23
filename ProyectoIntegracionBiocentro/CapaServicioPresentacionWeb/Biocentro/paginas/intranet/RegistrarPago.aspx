<%@ Page Title="" Language="C#" MasterPageFile="~/Biocentro/paginas/MasterPage.Master" AutoEventWireup="true" CodeBehind="RegistrarPago.aspx.cs" Inherits="CapaServicioPresentacionWeb.Biocentro.paginas.intranet.RegistrarPago" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="container-fluid">
        <div class="col-xs-12 col-sm-12 col-md-12 col-lg-12">
            <h1 class="display-4 mx-auto text-center" >Recibir Pago</h1>
            <div class="separador-md"></div >
            <div class="row">
                <div class="col-xs-12 col-sm-8 col-md-3 col-lg-3">
                    <label for="txtBuscarPaciente">Ingrese Rut Paciente</label>
                    <asp:TextBox ID="txtBuscarPaciente" runat="server" CssClass="form-control"></asp:TextBox>
                </div>
                <div class="col-xs-12 col-sm-4 col-md-2 col-lg-2">
                     <asp:Button ID="btnBuscarPaciente" runat="server" OnClick="btnBuscarPaciente_Click" Text="Buscar" CssClass="btn btn-primary m-t-lg" />            
                </div>
            </div>
            <div class="separador-sm"></div>
            <hr/>
            <div id="datosReserva" runat="server">
                <div class="row">
                    <label for="lblTerapeuta"  class="m-l-md">Nombre: </label>
                    <asp:Label ID="txtNombrePaciente" runat="server"></asp:Label>
                    <label for="lblTerapeuta" class="m-l-md">RUT: </label>
                    <asp:Label ID="txtRutPaciente" runat="server" ></asp:Label>
                </div>
                <div class="separador-md"></div>
                <div class="row">
                    <div class="col-xs-12 col-sm-12 col-md-8 col-lg-8">
                        <asp:GridView ID="tablaResumen" runat="server" AutoGenerateColumns="false" CssClass="table table-design">
                            <Columns>
                                <asp:TemplateField HeaderText="TERAPEUTA" >
                                    <ItemTemplate>
                                        <asp:Label ID="lblIdHora" runat="server" Enabled="false" Text='<%#Eval("IdHora")%>' Visible="false"/>
                                        <asp:Label ID="lblTerapeuta" runat="server" Enabled="false" Text='<%#Eval("Terapeuta.Nombre")+ " " + Eval("Terapeuta.ApellidoPaterno")+ " " + Eval("Terapeuta.ApellidoMaterno")%>'/>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:BoundField DataField="EspecialidadClinica.Nombre" HeaderText="ESPECIALIDAD" />
                                <asp:BoundField DataField="Fecha" HeaderText="FECHA" DataFormatString="{0:dd-MM-yyyy}" />
                                <asp:TemplateField HeaderText="HORA" >
                                    <ItemTemplate>
                                        <asp:Label ID="lblHora" runat="server" Enabled="false" Text='<%#Eval("IdBloque.HoraInicio")+ " - " + Eval("IdBloque.HoraFin") + " Hrs" %>'/>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="VALOR" >
                                    <ItemTemplate>
                                        <asp:Label ID="lblValor" runat="server" Enabled="false" Text='<%# "$ "+ Eval("EspecialidadClinica.Precio")%>' />
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Pagar" >
                                    <ItemTemplate>
                                        <asp:CheckBox ID='CheckBox1' runat='server' AutoPostBack='true' OnCheckedChanged='sumarHorasSeleccionadas' Enabled='true'/>
                                        <asp:Label ID="lblEstado" runat="server" Enabled="false" Text='<%# (Eval("Venta") != null)? Eval("Venta.EstadoVenta.Descripcion"):null%>'/>                                    
                                    </ItemTemplate>
                                </asp:TemplateField>
                            </Columns>
                            <emptydatatemplate>
                              <asp:image id="NoDataImage" runat="server" HeaderText="Pagar"/>                       
                                Sin Datos Para Mostrar.
                            </emptydatatemplate> 
                        </asp:GridView>
                    </div>
                    <div class="col-xs-12 col-sm-12 col-md-4 col-lg-4 text-center">
                        <div class="col-xs-12 col-sm-12 col-md-4 col-lg-4 text-center">
                            <h4>Total:</h4>
                        </div>
                        <div class="col-xs-12 col-sm-12 col-md-4 col-lg-4 text-center p-t-sm">
                            <asp:Label ID="txtTotal" runat="server" CssClass="text-md"></asp:Label>
                         </div>
                        <div class="separador-sm"></div>
                        <div class="col-xs-12 col-sm-12 col-md-12 col-lg-12">
                            <asp:DropDownList  id="comboMedioPago"
                                AutoPostBack="false"
                                runat="server" CssClass="form-control form-group" >
                            </asp:DropDownList>
                            <asp:Button ID="Button2" runat="server" OnClick="guardarPago" Text="Registrar Pago" CssClass="btn btn-primary " />
                        </div>
                    </div>
                </div>
            </div>
            <div class="separador-md"></div>
        </div>
    </div>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="footer" runat="server">
    
</asp:Content>
