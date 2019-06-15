<%@ Page Title="" Language="C#" MasterPageFile="~/Biocentro/paginas/MasterPage.Master" AutoEventWireup="true" CodeBehind="RegistrarPago.aspx.cs" Inherits="CapaServicioPresentacionWeb.Biocentro.paginas.intranet.RegistrarPago" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style type="text/css">
        .table th{
            background-color:khaki;
            border-top:1px solid black;
            border-color : black;
        }
        .table{
            margin:30px 0px;
        }
        .select{
            margin: 10px 0px;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="container-fluid">
            <div class="card" >
                <h1 class="display-4 mx-auto" >Recibir Pago</h1>
                <div class="card-header form-row">
                    <h3 class="col-3">Ingrese Rut Paciente:</h3>
                    <asp:TextBox ID="txtBuscarPaciente" runat="server" CssClass="col-md-2 form-control"></asp:TextBox>
                    <asp:Button ID="btnBuscarPaciente" runat="server" OnClick="btnBuscarPaciente_Click" Text="Buscar" CssClass="btn btn-md btn-primary btn-block" />
                </div >
                <hr style="color:black;margin-top: 3%; border-top: 4px solid"/>
                <div class="modal-body form-row">
                    <h3 class="col-md-1" style="margin:5px 42px 0px 0px;;">Nombre:</h3>
                    <asp:TextBox CssClass="col-md-5" ID="txtNombrePaciente" runat="server" Enabled="False" ></asp:TextBox>
                    <h3 class="col-md-1" style="margin:5px 3px 0px 0px;">Rut:</h3>
                    <asp:TextBox CssClass="col-md-4" ID="txtRutPaciente1" runat="server" Enabled="False"></asp:TextBox>
                </div>
            </div>
            <div class="row" style="margin-top:15px">
                <div class="container-fluid col-md-12">
                    <asp:GridView ID="tablaResumen" runat="server" AutoGenerateColumns="false" CssClass="table thead-dark table-bordered table-hover">
                        <Columns>
                            <asp:TemplateField HeaderText="TERAPEUTA" >
                                <ItemTemplate>
                                    <asp:Label ID="lblIdHora" runat="server" Enabled="false" Text='<%#Eval("IdHora")%>' Visible="false"/>
                                    <asp:Label ID="lblTerapeuta" runat="server" Enabled="false" Text='<%#Eval("Terapeuta.Nombre")+ " " + Eval("Terapeuta.ApellidoPaterno")+ " " + Eval("Terapeuta.ApellidoMaterno")%>'/>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:BoundField DataField="EspecialidadClinica.Nombre" HeaderText="ESPECIALIDAD" />
                            <asp:BoundField DataField="Fecha" HeaderText="FECHA" />
                            <asp:TemplateField HeaderText="HORA" >
                                <ItemTemplate>
                                    <asp:Label ID="lblHora" runat="server" Enabled="false" Text='<%#Eval("IdBloque.HoraInicio")+ " - " + Eval("IdBloque.HoraFin") + " Hrs" %>'/>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="VALOR" >
                                <ItemTemplate>
                                    <asp:Label ID="lblValor" runat="server" Enabled="false" Text='<%# "$ "+Eval("EspecialidadClinica.Precio")%>'/>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Pagar" >
                                <ItemTemplate>
                                    <asp:CheckBox ID='CheckBox1' runat='server' AutoPostBack='false' OnCheckedChanged='sumarHorasSeleccionadas' Enabled='true'/>
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
                <hr style="color:black;margin-top: 3%; border-top: 4px solid"/>
                <div style="margin:5px 0px 30px 0px;" class="col-md-10" >
                    <h3 style="margin-top:5px">Total:</h3>
                        <asp:TextBox ID="txtTotal" runat="server" Enabled="false" />
                        <div style="margin-top:0px;">
                            <asp:DropDownList  id="comboMedioPago"
                            AutoPostBack="false"
                            runat="server" CssClass="btn-group btn btn-secondary dropdown-toggle select" >
                        </asp:DropDownList>
                            <asp:Button ID="Button2" runat="server" OnClick="guardarPago"  Text="Registrar Pago" CssClass="btn btn-primary " />
                        </div>
                </div>
            </div>  
        </div>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="footer" runat="server">
    
</asp:Content>
