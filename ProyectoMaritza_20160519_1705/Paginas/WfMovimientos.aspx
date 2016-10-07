<%@ Page Title="" Language="C#" MasterPageFile="~/Plantilla.Master" AutoEventWireup="true" CodeBehind="WfMovimientos.aspx.cs" Inherits="ProyectoMaritza_20160519_1705.Paginas.WfMovimiento" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <script src="../js/jquery-1.7.1.min.js"></script>
    <script src="../js/jquery-ui-1.8.20.min.js"></script>
    <script src="../js/jquery-1.8.3.min.js"></script>
    <script src="../js/ajax_movimiento.js"></script>
    <script type="text/javascript">
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
        function fn_sin_caract_esp(evt) {
            var charCode = (evt.which) ? evt.which : event.keyCode;
            var caracter = String.fromCharCode(charCode);
            if (/^([a-zA-Z])*$/.test(caracter)) {
                return true;
            }
            else {
                return false;
            }
        }
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <asp:HiddenField ID="hidden_transac" runat="server" />
    <div class="">
        <div class="form-horizontal">
            <div class="form-group text-center">
                <h2 class="col-md-12">MOVIMIENTOS DEL PERSONAL</h2>
            </div>
            <div class="form-group">
                <asp:Label ID="lblNumDoc" CssClass="control-label col-md-2 col-md-offset-1" runat="server" Text="DNI:"></asp:Label>
                <div class="col-md-5">
                    <asp:TextBox ID="txtNumDoc" CssClass="form-control" runat="server" MaxLength="8"  OnKeyPress="return fn_solo_enteros(event);"></asp:TextBox>
                </div>
                <div class="col-md-3">
                    <asp:Button ID="btnBuscar" CssClass="btn btn-block btn-primary" runat="server" Text="Buscar"/>
                    <asp:Label ID="lblErrorId" runat="server" Text=""></asp:Label>
                </div>
            </div>
            <div class="form-group">
                <asp:Label ID="lbl_apellido_paterno" CssClass="control-label col-md-2 col-md-offset-1" runat="server" Text="Apellidos y Nombres:"></asp:Label>
                <div class="col-md-8">
                    <asp:TextBox ID="txtApellidoPaterno" Enabled="false" CssClass="form-control" runat="server"></asp:TextBox>
                </div>
            </div>
            <div class="form-group ">
                <h4 class="col-md-11 col-md-offset-1">Movimientos</h4>
            </div>
            <div class="form-group">
                <div class="col-md-10 col-md-offset-1">
                    <table id="grd_movimientos" class="table table-bordered table-striped">
                        <thead>
                            <tr>
                                <th>Código</th>
                                <th>Descripción</th>
                                <th>Fecha</th>
                                <th>Monto</th>
                                <th style="display:none;">Id Movimiento</th>
                                <th>Movimiento</th>
                                <th colspan="2"><center>Opciones</center></th>
                            </tr>
                        </thead>
                        <tbody>
                        </tbody>
                    </table>        
                </div>
            </div>
            <div class="form-group">
                <div class="col-md-3 col-md-offset-8">
                    <asp:Button ID="btnMovimiento" CssClass="btn btn-block btn-primary" runat="server" Text="Agregar movimiento" />
                </div>
            </div>
            <%--<div class="form-group">
                <asp:Label ID="lbl_saldo_disp" CssClass="control-label col-md-2 col-md-offset-1" runat="server" Text="Saldo Disponible:"></asp:Label>
                <div class="col-md-2">
                    <asp:TextBox ID="txt_saldo_disp" Enabled="false" CssClass="form-control" runat="server"></asp:TextBox>
                </div>
            </div>--%>
            <%--ESTO NO SE DEBE VER--%>
            <div id="MyModal" class="modal fade" role="dialog">
                <div class="modal-dialog">
                    <div class="modal-content">
                        <div class="modal-header">
                            INGRESAR MOVIMIENTO
                        </div>
                        <div class="modal-body">
                            <div class="form-group">
                                <asp:Label ID="lbl_tipo_mov_popup" CssClass="control-label col-md-10 col-md-offset-1" runat="server" Text="Tipo de movimiento:"></asp:Label>
                            </div>
                            <div class="form-group">
                                <div class="col-md-10 col-md-offset-1">
                                    <asp:DropDownList ID="ddl_tipo_mov_popup" CssClass="form-control" runat="server"></asp:DropDownList>
                                </div>
                            </div>
                            <div class="form-group">
                                <asp:Label ID="lbl_fecha_popup" CssClass="control-label col-md-10 col-md-offset-1" runat="server" Text="Fecha:"></asp:Label>
                            </div>
                            <div class="form-group">
                                <div class="col-md-10 col-md-offset-1">
                                    <asp:TextBox ID="txt_fecha_popup" CssClass="form-control" runat="server" type="date" MaxLength="40"></asp:TextBox>
                                </div>
                            </div>
                            <div class="form-group">
                                <asp:Label ID="lbl_descr_popup" CssClass="control-label col-md-10 col-md-offset-1" runat="server" Text="Descripción:"></asp:Label>
                            </div>
                            <div class="form-group">
                                <div class="col-md-10 col-md-offset-1">
                                    <asp:TextBox ID="txt_descr_popup" CssClass="form-control" runat="server" MaxLength="40"></asp:TextBox>
                                </div>
                            </div>
                            <div class="form-group">
                                <asp:Label ID="lbl_monto" CssClass="control-label col-md-10 col-md-offset-1" runat="server" Text="Monto:"></asp:Label>
                            </div>
                            <div class="form-group">
                                <div class="col-md-10 col-md-offset-1">
                                    <asp:TextBox ID="txt_monto_popup" CssClass="form-control" runat="server"></asp:TextBox>
                                </div>
                            </div>
                            <div class=" form-group">
            
                                <div class="col-md-5 col-md-offset-1">
                                    <asp:Button ID="btn_guardar_popup"  CssClass="btn btn-block btn-primary" runat="server" Text="Guardar"  />
                                </div>
                                <div class="col-md-5">
                                    <asp:Button ID="btn_salir_popup" OnClientClick="return false;"  CssClass="btn btn-block btn-primary" runat="server" Text="Salir" data-dismiss="modal" />
                                </div>
                            </div>
                        </div>
                        <div class="modal-footer">
                            Modal Footer
                        </div>
                    </div>
                </div>
            </div>

            <%--ESTO NO SE DEBE VER--%>
        </div>
    </div>
</asp:Content>
