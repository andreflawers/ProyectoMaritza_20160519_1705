<%@ Page Title="" Language="C#" MasterPageFile="~/Plantilla.Master" AutoEventWireup="true" CodeBehind="WfDistrito.aspx.cs" Inherits="ProyectoMaritza_20160519_1705.Paginas.WfDistrito" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <script src="../js/jquery-1.8.3.min.js"></script>
    <script src="../js/ajax_distrito.js"></script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <asp:HiddenField ID="hidden_transac" runat="server" /> 
	   <div  style="width:inherit; background:white" >
                       <div class="panel-heading " style="text-align:center; background-color:#F2F5A9">                      
                         <h3>Distrito</h3>
                      </div>
                          
                    <div class="panel-primary">
                        <div class="panel panel-body"> 
                       <div class="form-group">
                           <div class="row">
                             <div class="col-md-2"><asp:Label ID="lblNombreDistrito" class="form-control-label"  runat="server" Text=" Nombre Distrito :"></asp:Label></div>
                             <div class="col-md-6"><asp:TextBox ID="txtNombreDistrito" class="form-control" runat="server"></asp:TextBox></div>
                            </div>
                       </div>
                       <div class="form-group">
                           <div class="row">
                             <div class="col-md-2"><asp:Label ID="lblNombreProvincia"  class="form-control-label" runat="server" Text="Provincia :"></asp:Label></div>
                             <div class="col-md-6"> <asp:DropDownList ID="cbn_provincia" class="form-control"   runat="server"></asp:DropDownList></div>
                         </div>
                       </div>
                       <div class="form-group">
                           <div class="row">
                           <div class="col-md-2"> <asp:Button ID="btnRegistra" runat="server" Text="Nuevo" CssClass="btn btn-primary" /> </div>
                           <div class="col-md-2"> <asp:Button ID="btnGuardar" runat="server" Text="Guardar" CssClass="btn btn-success"/> </div>
                           <div class="col-md-2"> <asp:Button ID="btnCancelar" runat="server" Text="Cancelar" CssClass="btn btn-danger"/> </div>
                           </div>
                       </div>
                            </div>
                   </div>
               <asp:Label ID="lblErrores" style="color:red" runat="server" Text=""></asp:Label>
              <div class="table-responsive"> 
        <asp:GridView ID="grd_distrito" Width="100%" runat="server" AutoGenerateColumns="False" CssClass="table table-striped footable" GridLines="None" style="text-align:left">
                            <Columns>
                                <asp:BoundField DataField="IdDistrito" HeaderText="ID Distrito" />
                                <asp:BoundField DataField="nombreDistrito" HeaderText="Nombre Distrito" />
                                <asp:BoundField DataField="IdProvincia" HeaderText="ID Provincia" />  
                                <asp:BoundField DataField="nombreProvincia" HeaderText="Nombre Provincia" />
                                <asp:TemplateField>
                                    <ItemTemplate>
                                        <asp:Button CssClass="linkin btn btn-info" ID="btnEditar" runat="server" Text="Editar" />
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField>
                                    <ItemTemplate>
                                        <asp:Button ID="btnEliminar" runat="server"  Text="Eliminar" CssClass="linkin_eliminar btn btn-warning" />
                                    </ItemTemplate>
                                </asp:TemplateField>
                               
                            </Columns>
         </asp:GridView>
                             
                       
               </div>   
              
    </div>
</asp:Content>
