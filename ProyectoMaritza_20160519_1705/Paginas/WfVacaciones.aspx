<%@ Page Title="" Language="C#" MasterPageFile="~/Plantilla.Master" AutoEventWireup="true" CodeBehind="WfVacaciones.aspx.cs" Inherits="ProyectoMaritza_20160519_1705.Paginas.WfVacaciones" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <script src="../js/jquery-1.7.1.min.js"></script>
    <script src="../js/jquery-ui-1.8.20.min.js"></script>
    <script src="../js/jquery-1.8.3.min.js"></script>
    <script src="../js/ajax_vacaciones.js"></script>
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
                    <h2 class="col-md-12">VACACIONES DEL PERSONAL</h2>
                </div>
                <div class="form-group">
                    <asp:Label ID="lblNumDoc" CssClass="control-label col-md-2 col-md-offset-1" runat="server" Text="DNI:"></asp:Label>
                    <div class="col-md-5">
                        <asp:TextBox ID="txtNumDoc" CssClass="form-control" runat="server" MaxLength="8"  OnKeyPress="return fn_solo_enteros(event);"></asp:TextBox>
                        <div id="errorDNI"></div>
                    </div>
                    <div class="col-md-3">
                        <asp:Button ID="btnBuscar" CssClass="btn btn-block btn-primary" runat="server" Text="Buscar" />
                    </div>
                </div>
                <div class="form-group">
                    <asp:Label ID="lblApellidoPaterno" CssClass="control-label col-md-2 col-md-offset-1" runat="server" Text="Apelidos y Nombres:"></asp:Label>
                    <div class="col-md-8">
                        <asp:TextBox ID="txtApellidoPaterno" Enabled="false" CssClass="form-control" runat="server"></asp:TextBox>
                    </div>
                </div>
                <div class="form-group ">
                    <h4 class="col-md-11 col-md-offset-1">Vacaciones</h4>
                </div>
                <div class="form-group">
                    <div class="col-md-10 col-md-offset-1">
                        <table id="grd_vacaciones" class="table table-bordered table-striped">
                            <thead>
                                <tr>
                                    <th>Código</th>
                                    <th>Fecha inicio</th>
                                    <th>Fecha fin</th>
                                    <th>Descripción</th>
                                    <th style="display:none;">Id Encargado</th>
                                    <th>Encargado</th>
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
                        <asp:Button ID="btnAdelanto" CssClass="btn btn-block btn-primary" runat="server" Text="Agregar Adelanto" />
                    </div>
                </div>
                <%--<div class="form-group">
                    <asp:Label ID="lbl_dias_disp" CssClass="control-label col-md-2 col-md-offset-1" runat="server" Text="Días Disponibles:"></asp:Label>
                    <div class="col-md-1">
                        <asp:TextBox ID="txt_dias_disp" Enabled="false" CssClass="form-control" runat="server"></asp:TextBox>
                    </div>
                </div>--%>
                <%--ESTO NO SE DEBE VER--%>
                <div id="MyModal" class="modal fade" role="dialog">
                    <div class="modal-dialog">
                        <div class="modal-content">
                            <div class="modal-header">
                                INGRESO VACACIONES
                            </div>
                            <div class="modal-body">
                                <div class="form-group">
                                    <asp:Label ID="lbl_desde_popup" CssClass="control-label col-md-1 col-md-offset-1" runat="server" Text="Desde:"></asp:Label>
                                    <div class="col-md-4">
                                        <asp:TextBox ID="txt_desde_popup" CssClass="form-control" runat="server" type="date"></asp:TextBox>
                                    </div>
                                    <asp:Label ID="lbl_hasta_popup" CssClass="control-label col-md-1" runat="server" Text="Hasta:"></asp:Label>
                                    <div class="col-md-4">
                                        <asp:TextBox ID="txt_hasta_popup" CssClass="form-control" runat="server" type="date"></asp:TextBox>
                                        <div id="errorHasta"></div>
                                    </div>
                                </div>
                        
                                <div class="form-group">
                                    <asp:Label ID="lbl_desc_popup" CssClass="control-label col-md-10 col-md-offset-1" runat="server" Text="Descripción:"></asp:Label>
                                </div>

                                <div class="form-group">
                                    <div class="col-md-10 col-md-offset-1">
                                        <asp:TextBox ID="txt_desc_popup" CssClass="form-control" runat="server" MaxLength="40"></asp:TextBox>
                                        <div id="errorDesc"></div>
                                    </div>
                                </div>
                                <div class="form-group">
                                    <asp:Label ID="lbl_encargado_popup" CssClass="control-label col-md-10 col-md-offset-1" runat="server" Text="Encargado:"></asp:Label>
                                </div>

                                <div class="form-group">
                                    <div class="col-md-10 col-md-offset-1">
                                        <input type="hidden" id="encargado_id" value="0"/>
                                        <input type="text" id="filtrarEncargado" class=" form-control" />
                                        <div id="errorFiltrar"></div>
                                        <%--<asp:TextBox ID="txt_encargado_popup" CssClass="form-control" runat="server"></asp:TextBox>--%>
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
