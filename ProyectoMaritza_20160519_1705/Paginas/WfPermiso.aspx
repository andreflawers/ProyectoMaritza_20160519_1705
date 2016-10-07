<%@ Page Title="" Language="C#" MasterPageFile="~/Plantilla.Master" AutoEventWireup="true" CodeBehind="WfPermiso.aspx.cs" Inherits="ProyectoMaritza_20160519_1705.Paginas.WfPermiso" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <script src="../js/jquery-1.7.1.min.js"></script>
    <script src="../js/jquery-ui-1.8.20.min.js"></script>
    <script src="../js/jquery-1.8.3.min.js"></script>
    <script src="../js/ajax_permiso.js"></script>
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
    <div class="box box-info">

        <div class="panel-heading">
            <h3>
                <center>Permisos</center>
            </h3>
        </div>
        <div class="panel-body">
            <div class="box-body">
                <div class="form-horizontal">
                    
                    <asp:Panel ID="Panel1" runat="server" BorderStyle="Ridge"  ScrollBars="Auto" BorderColor="Silver">
                    <br />
                    <div>
                    <div class="form-group col-sm-6 col-md-3">
                        <div class="col-xs-12 col-sm-12">
                            <asp:Label ID="lbl_dni" class="control-label " runat="server" Text="DNI"></asp:Label>
                        </div>
                        <div class="col-xs-12 col-sm-12">
                            <asp:TextBox ID="txt_numDoc" class=" form-control" runat="server" MaxLength="8"  OnKeyPress="return fn_solo_enteros(event);" ></asp:TextBox>
                            
                        </div>
                    </div>


                    <div class="form-group col-sm-6 col-md-3">
                        <br />
                        <div class="col-xs-12 col-sm-12">
                            <asp:Button ID="btn_buscar" CssClass="btn btn-block btn-primary" runat="server" Text="Buscar" />
                            <asp:Label ID="lblErrorId" runat="server" Text=""></asp:Label>
                        </div>
                        
                    </div>
                    <div class="form-group col-sm-6 col-md-6">

                        <div class="col-xs-12 col-sm-12">

                            <asp:Label ID="lbl_apePaterno" class="control-label" runat="server" Text="Apellidos y Nombres"></asp:Label>
                        </div>
                        <div class="col-xs-12 col-sm-12">

                         <asp:TextBox ID="txt_apePaterno" class=" form-control" runat="server" ReadOnly="true" ></asp:TextBox>
                        </div>
                    </div>
                    

                    </div>
                    </asp:Panel>
                    <br />
                    <asp:Panel ID="Panel2" runat="server" BorderStyle="Ridge"  ScrollBars="Auto" BorderColor="Silver">
                        <br />
                        <div>
                            <div class="form-group col-sm-6 col-md-3"  >
                       
                                <div class="col-xs-12 col-sm-12">

                                    <asp:Label ID="lbl_tipoPermio" class="control-label " runat="server" Text="Tipo de Permiso"></asp:Label>
                                </div>
                                <div class="col-xs-12 col-sm-12">

                                   <asp:DropDownList ID="drp_tipoPermiso" runat="server" class=" form-control"></asp:DropDownList>

                                </div>
                            </div>
                            <div class="form-group col-sm-6 col-md-6"  >
                       
                                <div class="col-xs-12 col-sm-12">

                                    <asp:Label ID="lbl_justificacion" class="control-label" runat="server" Text="Justificación"></asp:Label>
                                </div>
                                <div class="col-xs-12 col-sm-12">

                                 <asp:TextBox ID="txt_justificacion" MaxLength="50" class=" form-control" runat="server"></asp:TextBox>
                           
                                </div>
                            </div>
                    
                            <div class="form-group col-sm-6 col-md-3"  >

                             
                                <div class="col-xs-12 col-sm-12">
                                    
                                    <asp:Label ID="lbl_fechaInicio" class="control-label " runat="server" Text="De"></asp:Label>

                                </div>
                                <div class="col-xs-12 col-sm-12">


                                 <asp:TextBox ID="txt_fechaInicio" class=" form-control" runat="server" type="date" ></asp:TextBox>
                                    
                                </div>
                            </div>
                            <div class="form-group col-sm-6 col-md-3"  >
                       
                                <div class="col-xs-12 col-sm-12">

                                    <asp:Label ID="lbl_fechaFin" class="control-label" runat="server" Text="Hasta"></asp:Label>
                                </div>
                                <div class="col-xs-12 col-sm-12" >

                                 <asp:TextBox ID="txt_fechaFin"  class="form-control" runat="server" type="date" ></asp:TextBox>
                           
                                </div>
                            </div>
                     
                            </div>
                        </asp:Panel>
                    <br />
                    
                    <div class="form-group">
                        <div class="col-md-10 col-md-offset-1">
                            <table id="grd_permisos" class="table table-bordered table-striped">
                                <thead>
                                    <tr>
                                        <th>Código</th>
                                        <th>Fecha inicio</th>
                                        <th>Fecha fin</th>
                                        <th>Justificacion</th>
                                        <th>Razon</th>
                                        <th colspan="2"><center>Opciones</center></th>
                                    </tr>
                                </thead>
                                <tbody>
                                </tbody>
                            </table>        
                        </div>
                    </div>

                </div>

            </div>

        </div>

    </div>
    <div class="box box-success">
        <div class="box-footer">
            <div class="box-body pad table-responsive">
                <table class="table table-bordered text-center">
                    <tr class="">

                        <td>
                            <asp:Button ID="btn_nuevo" class="btn btn-success btn-lg "  runat="server" Text="Nuevo" />
                        </td>

                        <td>
                            <asp:Button ID="btn_grabar" class="btn btn-info btn-lg" runat="server" Text="Grabar" />
                        </td>
                        <td>
                            <asp:Button ID="btn_cancelar" class="btn btn-danger btn-lg" runat="server" Text="Cancelar" />
                        </td>
                    </tr>
                </table>
            </div>
        </div>
    </div>
            </div>
</asp:Content>
