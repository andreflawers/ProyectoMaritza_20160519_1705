<%@ Page Title="" Language="C#" MasterPageFile="~/Plantilla.Master" AutoEventWireup="true" CodeBehind="WfBoleta.aspx.cs" Inherits="ProyectoMaritza_20160519_1705.Paginas.WfBoleta" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <script>
        function fn_solo_enteros(evt) {
            var charCode = (evt.which) ? evt.which : event.keyCode;
            var caracter = String.fromCharCode(charCode);
            if (/^([0-9])*$/.test(caracter)) {
                return true;
            }
            else {
                return false;
            }
        }
        function genPDF() {

            var specialElementHandlers = {
                '#bnt_buscar': function (element, renderer) {
                    return true;
                }
            };
            html2canvas(document.getElementById("boleta"), {
                onrendered: function (canvas) {
                    var img = canvas.toDataURL("image/png");
                    var doc = new jsPDF('p', 'in', [15, 12]);
                    doc.addImage(img, 'JPG', 0, 0.5);
                    doc.save('Boleta de pago.pdf');
                }
            });
        }
    </script>
    <script src="/js/jspdf.min.js" type="text/javascript"></script>
    <script src="/js/html2canvas.js" type="text/javascript"></script>
    <script src="../js/jquery-1.7.1.min.js"></script>
    <script src="../js/jquery-ui-1.8.20.min.js"></script>
    <script src="../js/jquery-1.8.3.min.js"></script>
    <script src="../js/ajax_boleta.js"></script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="">
        <div class="form-horizontal" id="boleta">
            <div class="form-group text-center">
                <h2 class="col-md-12">BOLETA DE PAGO DEL TRABAJADOR</h2>
            </div>

            <div class="form-group"> 
                <asp:Label ID="lbl_n_boleta" CssClass="control-label col-md-2 col-md-offset-6" runat="server" Text="N° Boleta:"></asp:Label>
                <div class="col-md-3">
                
                     <asp:TextBox ID="txt_n_boleta" Enabled="false" CssClass="form-control" runat="server" Text=""></asp:TextBox>
                </div>
            </div>

            <div class="form-group">
                <asp:Label ID="lbl_dni" CssClass="control-label col-md-2 col-md-offset-1" runat="server" Text="DNI:"></asp:Label>
           
                 <div class="col-md-5">
                    <asp:TextBox ID="txt_dni"  CssClass="form-control" runat="server" MaxLength="8"  OnKeyPress="return fn_solo_enteros(event);" ></asp:TextBox>
                </div>
                <div class="col-md-3" data-html2canvas-ignore="true">
                    <asp:Button ID="bnt_buscar" CssClass="btn btn-block btn-primary" runat="server" Text="Buscar"/>
                    <asp:Label ID="lbl_error" runat="server" Text="" ForeColor="Red"></asp:Label>
                </div>
            </div>
        

            <div class="form-group">
                <asp:Label ID="lbl_apellidos_nombres" CssClass="control-label col-md-2 col-md-offset-1" runat="server" Text="Apelidos y Nombres:"></asp:Label>
                <div class="col-md-8">
                    <asp:TextBox ID="txt_apellidos_nombres" Enabled="false" CssClass="form-control" runat="server"></asp:TextBox>
                </div>
            </div>
       
            <div class="form-group">
                <asp:Label ID="lbl_cargo" CssClass="control-label col-md-2 col-md-offset-1" runat="server" Text="Cargo:"></asp:Label>
                <div class="col-md-3">
                    <asp:TextBox ID="txt_cargo" Enabled="false" CssClass="form-control" runat="server"></asp:TextBox>
                </div>
                <asp:Label ID="lbl_modalidad" CssClass="control-label col-md-2" runat="server" Text="Modalidad:"></asp:Label>
                <div class="col-md-3">
                    <asp:TextBox ID="txt_modalidad" CssClass="form-control" Enabled="false" runat="server"></asp:TextBox>
                    <%--<asp:DropDownList CssClass="form-control" ID="ddl_modalidad" runat="server"></asp:DropDownList>--%>
                </div>
            </div>
            <div class="form-group">
                <asp:Label ID="lbl_ano" CssClass="control-label col-md-2 col-md-offset-1" runat="server" Text="Año:"></asp:Label>
                <div class="col-md-3">
                     <asp:TextBox ID="txt_ano" CssClass="form-control" runat="server" Enabled="true"></asp:TextBox>
                    <%--<asp:DropDownList ID="ddl_ano" CssClass="form-control" runat="server"></asp:DropDownList>--%>
                </div>
                <asp:Label ID="lbl_mes" CssClass="control-label col-md-2" runat="server" Text="Mes:"></asp:Label>
                <div class="col-md-3">
                    <%--<asp:TextBox ID="txt_mes" CssClass="form-control" Enabled="false" runat="server"></asp:TextBox>--%>
                    <asp:DropDownList ID="ddl_mes" Enabled="true" CssClass="form-control" runat="server"></asp:DropDownList>
                </div>
            </div>
            <div class="form-group">
                <asp:Label ID="lbl_dias_trabajados" CssClass="control-label col-md-2 col-md-offset-1" runat="server" Text="Dias Trabajados:"></asp:Label>
                <div class="col-md-3">
                    <asp:TextBox ID="txt_dias_trabajados" Enabled="false" CssClass="form-control" runat="server"></asp:TextBox>
                </div>
                <asp:Label ID="lbl_planilla_estado" CssClass="control-label col-md-2" runat="server" Text="Afiliación:"></asp:Label>
                <div class="col-md-3">
                    <asp:TextBox ID="txt_planilla_estado" CssClass="form-control" Enabled="false" runat="server"></asp:TextBox>
                </div>
            </div>
            <div class="form-group">
                <asp:Label ID="Label10" CssClass="control-label col-md-2 col-md-offset-1" runat="server" Text="Días Feriados:"></asp:Label>
                <div class="col-md-3">
                    <asp:TextBox ID="txt_dias_feriados" Enabled="false" CssClass="form-control" runat="server"></asp:TextBox>
                </div>
                <asp:Label ID="Label9" CssClass="control-label col-md-2" runat="server" Text="Días Dominical:"></asp:Label>
                <div class="col-md-3">
                
                    <asp:TextBox ID="txt_dias_dominicales" Enabled="false" CssClass="form-control" runat="server"></asp:TextBox>
                </div>
            </div>
            <div class="form-group">
                <asp:Label ID="lbl_tardanzas" CssClass="control-label col-md-2 col-md-offset-1" runat="server" Text="Tardanzas:"></asp:Label>
                <div class="col-md-3">
                    <asp:TextBox ID="txt_tardanzas" Enabled="false" CssClass="form-control" runat="server"></asp:TextBox>
                </div>
                <asp:Label ID="lbl_permisos" CssClass="control-label col-md-2" runat="server" Text="Días Permiso:"></asp:Label>
                <div class="col-md-3">
                
                    <asp:TextBox ID="txt_permiso" Enabled="false" CssClass="form-control" runat="server"></asp:TextBox>
                </div>
            </div>
            <div class="form-group">
                <asp:Label ID="lbl_vacaciones" CssClass="control-label col-md-2 col-md-offset-1" runat="server" Text="Días Vacaciones:"></asp:Label>
                <div class="col-md-3">
                    <asp:TextBox ID="txt_vacaciones" Enabled="false" CssClass="form-control" runat="server"></asp:TextBox>
                </div>
                
            </div>
            <div class="form-group">
                <div class="col-md-5 col-md-offset-1">
                     <table id="grd_remuneracion" class="table table-bordered">
                        <thead>
                            <tr>
                                <th colspan="2" style="text-align:center">
                                    REMUNERACIÓN
                                </th>
                            </tr>
                        </thead>
                        <tbody>

                        </tbody>
                    </table>
                    <table id="grd_aportacion" class="table table-bordered">
                        <thead>
                            <tr>
                                <th colspan="2" style="text-align:center">
                                    APORTACIONES DEL EMPLEADOR
                                </th>
                            </tr>
                        </thead>
                        <tbody>

                        </tbody>
                    </table>

                </div>
                <div class="col-md-5">
               
                     <table id="grd_descuento_planilla" class="table table-bordered">
                        <thead>
                            <tr>
                                <th colspan="2" style="text-align:center">
                                    DESCUENTO PLANILLA
                                </th>
                            </tr>
                        </thead>
                        <tbody>

                        </tbody>
        
                    </table>
                </div>

                <div class="col-md-5">
               
                     <table id="grd_descuento" class="table table-bordered">
                        <thead>
                            <tr>
                                <th colspan="2" style="text-align:center">
                                    DESCUENTOS
                                </th>
                            </tr>
                        </thead>
                        <tbody>

                        </tbody>
        
                    </table>
                </div>

                <div class="col-md-5">
               
                     <table id="grd_movimientos" class="table table-bordered">
                        <thead>
                            <tr>
                                <th colspan="2" style="text-align:center">
                                    MOVIMIENTOS
                                </th>
                            </tr>
                        </thead>
                        <tbody>

                        </tbody>
  
                    </table>
                </div>

            </div>
            <div class="form-group">
                <asp:Label ID="lbl_neto_a_pagar" CssClass="control-label col-md-2 col-md-offset-6" runat="server" Text="Neto a Pagar:"></asp:Label>
                <div class="col-md-3">
                    <asp:TextBox ID="txt_neto_a_pagar" Enabled="false" CssClass="form-control" runat="server"></asp:TextBox>
                </div>
            </div>
            <div class="form-group" data-html2canvas-ignore="true">
                <div class="col-md-3 col-md-offset-1">
                    <asp:Button ID="btn_guardar" CssClass="btn btn-block btn-primary" runat="server" Text="Guardar"/>
                </div>
                <div class="col-md-4">
                    <a id="btn_imprimir" href="javascript:genPDF()" class="btn btn-block btn-primary" role="button">Imprimir</a>
                </div>
                <div class="col-md-3">
                    <asp:Button ID="bnt_salir" PostBackUrl="~/Paginas/EntradaPrincipal.aspx" CssClass="btn btn-block btn-primary" runat="server" Text="Salir" />
                </div>
            </div>
        </div>
    </div>
</asp:Content>

