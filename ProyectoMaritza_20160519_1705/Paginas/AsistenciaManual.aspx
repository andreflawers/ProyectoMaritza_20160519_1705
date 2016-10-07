<%@ Page Title="" Language="C#" MasterPageFile="~/Plantilla.Master" AutoEventWireup="true" CodeBehind="AsistenciaManual.aspx.cs" Inherits="ProyectoMaritza_20160519_1705.Paginas.AsistenciaManual" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <script src="../js/jquery-1.8.3.min.js"></script>
    <script type="text/javascript">
        $(document).ready(function () {
            var timerID = null;
            var timerRunning = false;
            function stopclock() {
                if (timerRunning)
                    clearTimeout(timerID);
                timerRunning = false;
            }
            function showtime() {
                var now = new Date();
                var hours = now.getHours();
                var minutes = now.getMinutes();
                var seconds = now.getSeconds();
                var timeValue = "" + hours
                timeValue += ((minutes < 10) ? ":0" : ":") + minutes
                timeValue += ((seconds < 10) ? ":0" : ":") + seconds
                document.getElementById('<%=lblMostrarHora.ClientID%>').innerText = timeValue;
                timerID = setTimeout(showtime, 1000);
                timerRunning = true;
            }
            function startclock() {
                stopclock();
                showtime();
            }
            startclock();
        });
        function valida(e) {
            tecla = (document.all) ? e.keyCode : e.which;
            //Tecla de retroceso para borrar, siempre la permite
            if (tecla == 8) {
                return true;
            }
            // Patron de entrada, en este caso solo acepta numeros
            patron = /[0-9]/;
            tecla_final = String.fromCharCode(tecla);
            return patron.test(tecla_final);
        }
        function fn_valida_datos() {
            var ls_mensaje_error = "";
            var dni = document.getElementById('<%=txtDNI.ClientID%>').value;
            if (dni == "" || dni == null) {
                ls_mensaje_error = ls_mensaje_error + "Debe registrar el número de DNI.";
            }
            else {
                if (dni.length < 8) {
                    ls_mensaje_error = ls_mensaje_error + "El DNI debe tener 8 caracteres.";
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
        function HideLabel() {
            var seconds = 10;
            setTimeout(function () {
                document.getElementById("<%=txtDNI.ClientID %>").value = "";
                document.getElementById("<%=lblMensaje.ClientID %>").innerText = "";
                document.getElementById("<%=txtApellidosyNombres.ClientID %>").value = "";
            }, seconds * 1000);
        };
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <h3 class="text-center">INGRESO DE ASISTENCIA MANUAL</h3>
    <h5 class="text-center">Panificadora Maritza</h5>
    <fieldset onload="startclock();">
        <div class="form-group has-success">
            <asp:Label ID="lblDNI" runat="server" CssClass="control-label col-md-2" Text="DNI: "></asp:Label>
            <div class="col-md-2">
                <asp:TextBox ID="txtDNI" CssClass="form-control" MaxLength="8" runat="server" onkeypress="return valida(event)"></asp:TextBox>
                <br />
                <asp:Label ID="lblMensaje" runat="server" colspan="2" Text=""></asp:Label>
            </div>
            <asp:Label ID="lblFecha" runat="server" CssClass="control-label col-md-1" Text="Fecha: "></asp:Label>
            <div class="col-md-2">
                <asp:Label ID="lblMostarFecha" CssClass="form-control" runat="server" Text=""></asp:Label>
            </div>

            <div class="col-md-3">
                <asp:Label ID="lblHora" runat="server" CssClass="control-label col-md-3" Text="Hora: "></asp:Label>
                <div class="col-md-8">
                    <asp:Label ID="lblMostrarHora" CssClass="form-control" EnableViewState="true" ViewStateMode="Enabled" runat="server" Text=""></asp:Label>
                </div>
            </div>
        </div><br /><br /><br /><br />
        <div class="form-group has-success">
            <br />
            <div class="col-lg-8">
                <asp:Label ID="lblApellidosyNombres" runat="server" CssClass="control-label col-md-5" Text="Apellidos y Nombres: "></asp:Label>
                <div class="col-md-6">
                    <asp:TextBox ID="txtApellidosyNombres" Enabled="false" CssClass="form-control" runat="server"></asp:TextBox>
                </div>
            </div>
            <div class="col-md-2">
                <asp:Button class="btn btn-primary" ID="btnMarcar" runat="server" Text="Marcar" OnClick="btnMarcar_Click" OnClientClick="return fn_valida_datos();"></asp:Button>
            </div>
        </div>
        
    </fieldset>

    <%--Tabla de Contenido--%>
    <fieldset>
        <div class="table-responsive">
            <table class="table table-striped table-bordered table-hover table-condensed">
                <tr class="danger">
                    <asp:GridView ID="grdAsistencia" runat="server" AutoGenerateColumns="False" CssClass="table table-striped" GridLines="None" Width="100%">
                        <Columns>
                            <asp:BoundField DataField="IdAsistencia" HeaderText="ID Asistencia" />
                            <asp:BoundField DataField="diaAsistencia" HeaderText="Dia de la Asistencia" />
                            <asp:BoundField DataField="Apellidos y Nombres" HeaderText="Apellidos y Nombres" />
                            <asp:BoundField DataField="horaLlegadaAsistencia" HeaderText="Hora de Llegada" />
                            <asp:BoundField DataField="horaSalidaAsistencia" HeaderText="Hora de Salida" />
                            <asp:BoundField DataField="tardanzaAsistencia" HeaderText="Tardanza" />
                        </Columns>
                    </asp:GridView>

                </tr>
            </table>
        </div>
    </fieldset>
</asp:Content>
