<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="registrar_pago.aspx.cs" Inherits="CapaServicioPresentacionWeb.Biocentro.paginas.pago_hora.registrar_pago" %>


<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title></title>
    <link rel="stylesheet" href="https://stackpath.bootstrapcdn.com/bootstrap/4.3.1/css/bootstrap.min.css" integrity="sha384-ggOyR0iXCbMQv3Xipma34MD+dH/1fQ784/j6cY/iJTQUOhcWr7x9JvoRxT2MZw1T" crossorigin="anonymous">
    <script src="https://stackpath.bootstrapcdn.com/bootstrap/4.3.1/js/bootstrap.min.js" integrity="sha384-JjSmVgyd0p3pXB1rRibZUAYoIIy6OrQ6VrjIEaFf/nJGzIxFDsf4x0xIM+B07jRM" crossorigin="anonymous"></script>
    <script src="https://code.jquery.com/jquery-3.3.1.slim.min.js" integrity="sha384-q8i/X+965DzO0rT7abK41JStQIAqVgRVzpbzo5smXKp4YfRvH+8abtTE1Pi6jizo" crossorigin="anonymous"></script>
    <script src="https://cdnjs.cloudflare.com/ajax/libs/popper.js/1.14.7/umd/popper.min.js" integrity="sha384-UO2eT0CpHqdSJQ6hJty5KVphtPhzWj9WO1clHTMGa3JDZwrnQq4sF86dIHNDz0W1" crossorigin="anonymous"></script>
    <style type="text/css">
        .select{
            margin: 10px 0px;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server" >
        <div class="container-fluid">
            <div class="card" >
                <h1 class="display-4 mx-auto" >Recibir Pago</h1>
                <div class="card-header form-row">
                    <h3 class="col-md-3">Ingrese Rut Paciente:</h3>
                    <asp:TextBox ID="txtBuscarPaciente" runat="server" CssClass="col-md-4 form-control"></asp:TextBox>
                    <asp:Button ID="btnBuscarPaciente" runat="server" OnClick="btnBuscarPaciente_Click" Text="Buscar" CssClass="col-md-2" />
                </div >
                <hr style="color:black;margin-top: 3%; border-top: 4px solid"/>
                <div class="modal-body form-row">
                    <h3 class="col-md-2">Nombre</h3>
                    <asp:TextBox CssClass="col-md-4" ID="txtNombrePaciente" runat="server" Enabled="False" ></asp:TextBox>
                    <h3 class="col-md-1">Rut</h3>
                    <asp:TextBox CssClass="col-md-4" ID="txtRutPaciente1" runat="server" Enabled="False"></asp:TextBox>
                </div>
            </div>
            <div class="float-left row">
                <div style="border:1px solid black; height:100px; width:400px;" class="container-fluid col-md-7">
                    <asp:GridView ID="tablaResumen" runat="server" AutoGenerateColumns="false" CssClass="table table-striped">
                <Columns>
                    <asp:TemplateField HeaderText="TERAPEUTA" >
                        <ItemTemplate>
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
                            <asp:CheckBox ID="CheckBox1" runat="server" AutoPostBack="true" OnCheckedChanged="sumarHorasSeleccionadas" Enabled="true"/>
                        </ItemTemplate>
                    </asp:TemplateField>
                </Columns>
                <emptydatatemplate>
                  <asp:image id="NoDataImage" runat="server" HeaderText="Pagar"/>                       
                    Sin Datos Para Mostrar.
                </emptydatatemplate> 
            </asp:GridView>
                </div>
                <div class="col-4 col-md-4">
                    <div >
                        <h3>Total:</h3>
                        <asp:TextBox ID="txtTotal" runat="server" Enabled="false"/>
                    </div>
                    <asp:DropDownList id="comboMedioPago"
                        AutoPostBack="True"
                        runat="server" CssClass="btn-group btn btn-secondary dropdown-toggle select" >
                        <asp:ListItem Selected="True" Value="White"> White </asp:ListItem>
                        <asp:ListItem Value="Silver"> Silver </asp:ListItem>
                        <asp:ListItem Value="DarkGray"> Dark Gray </asp:ListItem>
                        <asp:ListItem Value="Khaki"> Khaki </asp:ListItem>
                        <asp:ListItem Value="DarkKhaki"> Dark Khaki </asp:ListItem>
                    </asp:DropDownList>
                    <asp:Button ID="Button2" runat="server" OnClick="guardarPago" Text="Registrar Pago" CssClass="btn btn-primary " />
                </div>
            </div>  
        </div>
    </form>
</body>
</html>
