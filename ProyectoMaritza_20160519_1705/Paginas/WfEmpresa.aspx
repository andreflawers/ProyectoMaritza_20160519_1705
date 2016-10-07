<%@ Page Title="" Language="C#" MasterPageFile="~/Plantilla.Master" AutoEventWireup="true" CodeBehind="WfEmpresa.aspx.cs" Inherits="ProyectoMaritza_20160519_1705.Paginas.WfEmpresa" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
     <script src="../js/jquery-1.8.3.min.js"></script>
    <script src="../js/ajax_empresa.js"></script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <asp:HiddenField ID="hidden_transac" runat="server" /> 

	   <div style="width:inherit; background-color:white" > 
           <div class="panel-heading" style="text-align:center; background-color:#F2F5A9">
                         <h3>Empresa</h3>
           </div>                     
                                   
                   <div class="panel-body">
                       <div class="form-group row">
                             <div class="col-sm-2"><asp:Label ID="lblRucEmpresa" class="form-control-label" runat="server" Text=" RUC EMPRESA :"></asp:Label></div>
                             <div class="col-sm-6"><asp:TextBox ID="txtRucEmpresa" class="form-control" runat="server"></asp:TextBox></div>
                       </div>
                       <div class="form-group row">
                             <div class="col-sm-2"><asp:Label ID="lblNombreEmpresa" class="form-control-label" runat="server" Text=" Nombre EMPRESA :"></asp:Label></div>
                             <div class="col-sm-6"><asp:TextBox ID="txtNombreEmpresa" class="form-control" runat="server"></asp:TextBox></div>
                       </div>
                       <div class="form-group row">
                             <div class="col-sm-2"><asp:Label ID="lblDireccionEmpresaa" class="form-control-label" runat="server" Text=" Dirección EMPRESA :"></asp:Label></div>
                             <div class="col-sm-6"><asp:TextBox ID="txtDireccionEmpresa" class="form-control" runat="server"></asp:TextBox></div>
                       </div>
                       <div class="form-group row">
                             <div class="col-sm-2"><asp:Label ID="lblDescripcionEmpresa" class="form-control-label" runat="server" Text=" Descripcion EMPRESA :"></asp:Label></div>
                             <div class="col-sm-6"><asp:TextBox ID="txtDescripcionEmpresa" class="form-control" runat="server"></asp:TextBox></div>
                       </div>
                       <div class="form-group row">
                           <div class="col-md-2"> <asp:Button ID="btnRegistra" runat="server" Text="Nuevo" CssClass="btn btn-primary"/> </div>
                           <div class="col-md-2"> <asp:Button ID="btnGuardar" runat="server" Text="Guardar" CssClass="btn btn-success" /> </div>
                           <div class="col-md-2"> <asp:Button ID="btnCancelar" runat="server" Text="Cancelar" CssClass="btn btn-danger" /> </div>
                       </div>
         
          </div>
           <asp:Label ID="lblErrores" style="color:red" runat="server" Text=""></asp:Label>
           <div class="table-responsive" >   
        <asp:GridView ID="grd_empresa" Width="100%" runat="server" AutoGenerateColumns="False" CssClass="table table-striped" GridLines="None" >
                            <Columns>
                                <asp:BoundField DataField="rucEmpresa" HeaderText="RUC Empresa"/>
                                <asp:BoundField DataField="nombreEmpresa" HeaderText="Nombre Empresa" />
                                <asp:BoundField DataField="direccionEmpresa" HeaderText="Direccion Empresa" />
                                <asp:BoundField DataField="descripcionEmpresa" HeaderText="Descripcion Empresa" />
                                <asp:TemplateField>
                                    <ItemTemplate>
                                        <asp:Button CssClass="linkin btn btn-info" ID="btnEditar" runat="server" Text="Editar" />
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField>
                                    <ItemTemplate>
                                        <asp:Button ID="btnEliminar" runat="server" Text="Eliminar" CssClass="linkin_eliminar btn btn-warning" />
                                    </ItemTemplate>
                                </asp:TemplateField>
                               
                            </Columns>
         </asp:GridView>
                             
                       
                </div>
            </div>
</asp:Content>
