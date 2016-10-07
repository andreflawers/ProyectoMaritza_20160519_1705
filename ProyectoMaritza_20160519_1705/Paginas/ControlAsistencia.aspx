<%@ Page Title="" Language="C#" MasterPageFile="~/Plantilla.Master" AutoEventWireup="true" CodeBehind="ControlAsistencia.aspx.cs" Inherits="ProyectoMaritza_20160519_1705.Paginas.ControlAsistencia" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <script src="../js/jquery-1.8.3.min.js"></script>
    <script type="text/javascript">
        function hideDivFecha(obj) {
            if (obj.checked == true) {
                document.getElementById("porFecha").style.display = 'block';
            }
            else {
                document.getElementById("porFecha").style.display = 'none';
            }
        }
        function hideDivHorarioySucursal(obj) {
            if (obj.checked == true) {
                document.getElementById("porHorarioySucursal").style.display = 'block';
            }
            else {
                document.getElementById("porHorarioySucursal").style.display = 'none';
            }
        }
        function hideDivPersonal(obj) {
            if (obj.checked == true) {
                document.getElementById("porPersonal").style.display = 'block';
            }
            else {
                document.getElementById("porPersonal").style.display = 'none';
            }
        }
        function fn_valida_datos() {
            var ls_mensaje_error = "";
            var porfecha = document.getElementById('<%=chkBuscarFecha.ClientID%>').checked;
            var porHorario = document.getElementById('<%=chkBuscarHorarioySucursal.ClientID%>').checked;
            var porPersonal = document.getElementById('<%=chkBuscarPersonal.ClientID%>').checked;
            if (porfecha == false && porHorario == false && porPersonal == false) {
                ls_mensaje_error = ls_mensaje_error + "Debe seleccionar por lo menos una opción.";
            }
            else {
                if (porfecha == true) {
                    var desde = document.getElementById('<%=inpDesde.ClientID%>').value;
                    var hasta = document.getElementById('<%=inpHasta.ClientID%>').value;
                    if (desde == "" && hasta == "") { ls_mensaje_error = ls_mensaje_error + "Debe seleccionar la fecha de Inicio y de fin.  "; }
                    if (desde != "" && hasta == "") { ls_mensaje_error = ls_mensaje_error + "Debe seleccionar la fecha fin.  "; }
                    if (desde == "" && hasta != "") { ls_mensaje_error = ls_mensaje_error + "Debe seleccionar la fecha de Inicio.  "; }
                    else { if (hasta < desde) { ls_mensaje_error = ls_mensaje_error + "La fecha fin no puede ser menor a la fecha de Inicio.  "; } }
                }
                if (porHorario == true) {
                    var Horario = document.getElementById('<%=txtHorario.ClientID%>').value;
                    var Sucursal = document.getElementById('<%=txtSucursal.ClientID%>').value;
                    if (Horario == "" && Sucursal == "") { ls_mensaje_error = ls_mensaje_error + "Debe ingresar por lo menos 1 de los 2.  "; }
                }
                if (porPersonal == true) {
                    var apellido = document.getElementById('<%=txtApellido.ClientID%>').value;
                    var cargo = document.getElementById('<%=txtCargo.ClientID%>').value;
                    if (apellido == "" && cargo == "") { ls_mensaje_error = ls_mensaje_error + "Debe ingresar por lo menos 1 de los 2.  "; }
                }
            }
            if (ls_mensaje_error == "") {
                document.getElementById('<%=lblMensaje.ClientID%>').innerText = "";
                return true;
            }
            else {
                document.getElementById('<%=lblMensaje.ClientID%>').style.color = "#FF0000";
                document.getElementById('<%=lblMensaje.ClientID%>').innerText = ls_mensaje_error;
                return false;
            }
        }
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <h2 class="text-center">CONTROL DE ASISTENCIA</h2>
    <h4 class="text-center">Panificadora Maritza</h4>
    <fieldset>
        <div class="form-group has-success">
            <div class="col-md-12">
                <asp:CheckBox ID="chkBuscarFecha" Text="Buscar por Fecha" runat="server" onClick="hideDivFecha(this)"></asp:CheckBox><br /><br />
                <div id="porFecha" style="display: none;">
                    <div class="col-md-4">
                        <asp:Label ID="lblFechaEntrada" runat="server" Text="Desde:  "></asp:Label>
                        <input type="date" id="inpDesde" runat="server" />
                    </div>
                    <div class="col-md-4">
                        <asp:Label ID="lblFechaSalida" runat="server" Text="Hasta:  "></asp:Label>
                        <input type="date" id="inpHasta" runat="server" />
                    </div>
                </div><br /><br /><br />
            </div>
        </div>
        <br /><br /><br />
        <div class="form-group has-success">
            <div class="col-md-9">
                <asp:CheckBox ID="chkBuscarHorarioySucursal" Text="Buscar por Turno y Local" runat="server" onClick="hideDivHorarioySucursal(this)"></asp:CheckBox><br /><br />
                <div id="porHorarioySucursal" style="display: none;">
                    <div class="col-md-4">
                        <asp:Label ID="lblHorario" CssClass="control-label col-md-3" runat="server" Text="Horario:  "></asp:Label>
                        <div class="col-md-7">
                            <asp:TextBox ID="txtHorario" runat="server"></asp:TextBox>
                        </div>
                    </div>
                    <div class="col-md-4">
                        <asp:Label ID="lblSucursal" CssClass="control-label col-md-3" runat="server" Text="Sucursal:  "></asp:Label>
                        <div class="col-md-8">
                            <asp:TextBox ID="txtSucursal" runat="server"></asp:TextBox>
                        </div>
                    </div>
                </div><br /><br /><br />
            </div>
        </div>
        <br /><br /><br />
        <div class="form-group has-success">
            <div class="col-md-9">
                <asp:CheckBox ID="chkBuscarPersonal" runat="server" Text="Buscar por DNI, Nombres y Apellidos" onClick="hideDivPersonal(this)" /><br /><br />
                <div id="porPersonal" style="display: none;">
                    <div class="col-md-6">
                        <asp:Label ID="lblApellidos" runat="server" class="control-label col-md-3" Text="Apellidos y Nombres:"></asp:Label>
                        <div class="col-md-4">
                            <asp:TextBox ID="txtApellido" runat="server"></asp:TextBox>
                        </div>

                    </div>
                    <div class="col-md-4">
                        <asp:Label ID="lblDNI" runat="server" class="control-label col-md-3" Text="Cargo"></asp:Label>
                        <div class="col-md-3">
                            <asp:TextBox ID="txtCargo" runat="server"></asp:TextBox>
                        </div>
                    </div>
                </div><br /><br /><br />
            </div>
        </div>
        <br /><br /><br /><br /><br /><br /><br /><br />
        <asp:Label ID="lblMensaje" runat="server" Text=""></asp:Label><br />
        <asp:Button ID="btnBuscar" runat="server" Text="Buscar" class="btn btn-primary" OnClick="btnBuscar_Click" OnClientClick="return fn_valida_datos();" />

    </fieldset>
    <br /><br />
    <div>
        <asp:GridView ID="grdControl" CssClass="table table-striped" GridLines="None" Width="100%" runat="server"></asp:GridView>
    </div>
</asp:Content>
