<%@ Page Title="" Language="C#" MasterPageFile="~/Plantilla.Master" AutoEventWireup="true" CodeBehind="WfTipoDocumento.aspx.cs" Inherits="ProyectoMaritza_20160519_1705.Paginas.WfTipoDocumento" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <script src="../js/jquery-1.8.3.min.js"></script>
    <script src="../js/ajax_tipoDocumento.js"></script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <asp:HiddenField ID="hidden_transac" runat="server" /> 
	   <div  style="width:inherit; background-color:white" >
                     <div class="panel-heading" style="text-align:center; background-color:#F2F5A9">
                         <h3>Tipo de Documento</h3>
                      </div>
	        
                    <div class="panel-primary">
                           
                          <div class="panel panel-body">
                       <div class="form-group row">
                             <div class="col-sm-3"><asp:Label ID="lblNombreTipoDocumento"  class="form-control-label" runat="server" Text=" Nombre Tipo Documento :"></asp:Label></div>
                             <div class="col-sm-6"><asp:TextBox ID="txtNombreTipoDocumento" class="form-control" runat="server"></asp:TextBox></div>
                       </div>
                        <div class="form-group row">
                             <div class="col-sm-3"><asp:Label ID="lblLongitudTipoDocumento"  class="form-control-label" runat="server" Text=" Longitud Tipo Documento :"></asp:Label></div>
                             <div class="col-sm-6"><asp:TextBox ID="txtLongitudTipoDocumento" class="form-control" runat="server"></asp:TextBox></div>
                       </div>
                       <div class="form-group row">
                           <div class="col-md-2"> <asp:Button ID="btnRegistraTipoDocumento" runat="server" Text="Nuevo" CssClass="btn btn-primary"/> </div>
                           <div class="col-md-2"> <asp:Button ID="btnGuardar" runat="server" Text="Guardar" CssClass="btn btn-success" /> </div>
                           <div class="col-md-2"> <asp:Button ID="btnCancelar" runat="server" Text="Cancelar" CssClass="btn btn-danger" /> </div>
                       </div>
                  </div>
               </div>
           <asp:Label ID="lblErrores" style="color:red" runat="server" Text=""></asp:Label>
           <div class="table-responsive">    
            
        <asp:GridView ID="grd_tipo_documento" Width="100%" runat="server" AutoGenerateColumns="False" CssClass="table table-striped" GridLines="None" OnSelectedIndexChanged="grd_tipo_documento_SelectedIndexChanged">
                            <Columns>
                                <asp:BoundField DataField="IdTipoDocumento" HeaderText="ID Tipo Documento"/>
                                <asp:BoundField DataField="nombreTipoDocumento" HeaderText="Nombre del Tipo Documento" />
                                <asp:BoundField DataField="longitudTipoDocumento" HeaderText="Nombre del Tipo Documento" />
                                <asp:TemplateField>
                                    <ItemTemplate>
                                        <asp:Button CssClass="linkin btn btn-info"  ID="btnEditar" runat="server" Text="Editar" />
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField>
                                    <ItemTemplate>
                                        <asp:Button ID="btnEliminar" runat="server" Text="Eliminar"  CssClass="linkin_eliminar btn btn-warning" />
                                    </ItemTemplate>
                                </asp:TemplateField>
                               
                            </Columns>
         </asp:GridView>
               </div>
                            
                        
            </div>
</asp:Content>
