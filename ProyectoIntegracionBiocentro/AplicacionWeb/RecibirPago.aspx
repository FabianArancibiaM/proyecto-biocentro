<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="RecibirPago.aspx.cs" Inherits="AplicacionWeb.WebForm2" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title></title>
    <link rel="stylesheet" href="https://stackpath.bootstrapcdn.com/bootstrap/4.3.1/css/bootstrap.min.css" 
        integrity="sha384-ggOyR0iXCbMQv3Xipma34MD+dH/1fQ784/j6cY/iJTQUOhcWr7x9JvoRxT2MZw1T" crossorigin="anonymous"/>
</head>
<body>
    <form id="form1" runat="server">
        <div class="card" style="left:10%;right:10%;width: 80%;position: relative;top:50px">
            <h1 class="display-4" style="text-align:center" >Recibir Pago</h1>
            <div class="card-header">
                <h3 >Ingrese Rut Paciente:</h3>
                <asp:TextBox ID="txtBuscarPaciente" runat="server" ></asp:TextBox>
                <asp:Button ID="btnBuscarPaciente" runat="server" OnClick="btnBuscarPaciente_Click" Text="Buscar" CssClass="btn btn-primary" />
            </div >
            <hr style="color:black;margin-top: 3%; border-top: 4px solid"/>
            <div class="modal-body">
                <h3 ">Nombre:</h3>
                <asp:TextBox ID="txtNombrePaciente" runat="server" Enabled="False"></asp:TextBox>
                <h3 >Rut:</h3>
                <asp:TextBox ID="txtRutPaciente1" runat="server" Enabled="False"></asp:TextBox>
            </div>
        </div>
        <div style="left:10%;right:10%;width: 80%;position: relative;top:50px">
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
        <div style="left:10%;right:10%;width: 80%;position: relative;top:50px">
            <div >
                <h3>Total:</h3>
                <asp:TextBox ID="txtTotal" runat="server" />
                <asp:Button ID="Button1" runat="server" OnClick="guardarPago" Text="Registrar Pago" CssClass="btn btn-primary" />
            </div>
            <asp:DropDownList id="ColorList"
                AutoPostBack="True"
                runat="server" CssClass="btn-group btn btn-secondary dropdown-toggle">
                <asp:ListItem Selected="True" Value="White"> White </asp:ListItem>
                <asp:ListItem Value="Silver"> Silver </asp:ListItem>
                <asp:ListItem Value="DarkGray"> Dark Gray </asp:ListItem>
                <asp:ListItem Value="Khaki"> Khaki </asp:ListItem>
                <asp:ListItem Value="DarkKhaki"> Dark Khaki </asp:ListItem>
            </asp:DropDownList>
        </div>
        
    </form>
</body>
</html>
