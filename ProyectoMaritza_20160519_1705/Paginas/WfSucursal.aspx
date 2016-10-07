<%@ Page Title="" Language="C#" MasterPageFile="~/Plantilla.Master" AutoEventWireup="true" CodeBehind="WfSucursal.aspx.cs" Inherits="ProyectoMaritza_20160519_1705.Paginas.WfSucursal" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
     <script src="../js/jquery-1.8.3.min.js"></script>
    <script src="../js/ajax_sucursal.js"></script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <asp:HiddenField ID="hidden_transac" runat="server" /> 	   
	          <div  style="width:inherit; background-color:white" >
                    <div class="panel-heading " style="text-align:center; background-color:#F2F5A9">                     
                         <h3>Sucursal</h3>
                      </div>
	        
                     <div class="panel-primary">
                         
                          
                          <div class="panel panel-body">
                       <div class="form-group row">
                           
                             <div class="col-sm-2"><asp:Label ID="lblNombreSucursal" class="form-control-label" runat="server" Text=" Nombre Sucursal :"></asp:Label></div>
                             <div class="col-sm-6"><asp:TextBox ID="txtNombreSucursal" class="form-control" runat="server"></asp:TextBox></div>
                           
                       </div>
                       <div class="form-group row">
                             <div class="col-sm-2"><asp:Label ID="lblDireccionSucursal" class="form-control-label" runat="server" Text=" Direccion Sucursal :"></asp:Label></div>
                             <div class="col-sm-6"><asp:TextBox ID="txtDireccionSucursal" class="form-control" runat="server"></asp:TextBox></div>
                       </div>
                       <div class="form-group row">
                             <div class="col-sm-2"><asp:Label ID="lblTelefonoSucursal" class="form-control-label" runat="server" Text=" Telefono Sucursal :"></asp:Label></div>
                             <div class="col-sm-6"><asp:TextBox ID="txtTelefonoSucursal" class="form-control" runat="server"></asp:TextBox></div>
                       </div>
                       <div class="form-group row">
                             <div class="col-sm-2"><asp:Label ID="lblRucSucursal" class="form-control-label" runat="server" Text=" Ruc Sucursal :"></asp:Label></div>
                             <div class="col-sm-6"><asp:TextBox ID="txtRucSucursal" class="form-control" runat="server"></asp:TextBox></div>
                       </div>
                       <div class="form-group row">
                             <div class="col-md-2"><asp:Label ID="lblNombreDistrito" class="form-control-label" runat="server" Text="Distrito :"></asp:Label></div>
                             <div class="col-sm-6"> <asp:DropDownList ID="cbn_distrito" class="form-control" runat="server"></asp:DropDownList></div>
                       </div>
                       <div class="form-group row">
                           <div class="col-md-2"> <asp:Button ID="btnRegistra" runat="server"  Text="Nuevo" CssClass="btn btn-primary" /> </div>
                           <div class="col-md-2"> <asp:Button ID="btnGuardar" runat="server" Text="Guardar" CssClass="btn btn-success"/> </div> 
                           <div class="col-md-2"> <asp:Button ID="btnCancelar" runat="server" Text="Cancelar" CssClass="btn btn-danger" /> </div>
                           </div>
                   </div>             
       
                              </div>
                  <asp:Label ID="lblErrores" style="color:red" runat="server" Text=""></asp:Label>
                   <div class="table-responsive" >   
             
     <asp:GridView ID="grd_sucursal" Width="100%" runat="server" AutoGenerateColumns="False" CssClass="table footeable " GridLines="None" style="text-align:left">
                            <Columns>
                                <asp:BoundField DataField="IdSucursal" HeaderText="ID Sucursal" />
                                <asp:BoundField DataField="nombreSucursal" HeaderText="Nombre" />
                                <asp:BoundField DataField="direccionSucursal" HeaderText="Direccion" />  
                                <asp:BoundField DataField="telefonoSucursal" HeaderText="Telefono" />
                                <asp:BoundField DataField="rucSucursal" HeaderText="Ruc" />
                                <asp:BoundField DataField="IdDistrito" HeaderText="Ruc" />
                                <asp:BoundField DataField="nombreDistrito" HeaderText="Distrito" />
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
