<%@ Page Title="" Language="C#" MasterPageFile="~/Plantilla.Master" AutoEventWireup="true" CodeBehind="WfProvincia.aspx.cs" Inherits="ProyectoMaritza_20160519_1705.Paginas.WfProvincia" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <script src="../js/jquery-1.8.3.min.js"></script>
    <script src="../js/ajax_provincia.js"></script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
     <asp:HiddenField ID="hidden_transac" runat="server" /> 
	    
	         <div  style="width:inherit; background-color:white" >
                   <div class="panel-heading" style="text-align:center; background-color:#F2F5A9">
                         <h3>Provincia</h3>
                      </div>
                     <div class="panel-primary">
                        
                          <div class="panel panel-body">
                       <div class="form-group row">
                             <div class="col-sm-2"><asp:Label ID="lblNombreProvincia" class="form-control-label" runat="server" Text=" Nombre Provincia :"></asp:Label></div>
                             <div class="col-sm-6"><asp:TextBox ID="txtNombreProvincia"  class="form-control" runat="server"></asp:TextBox></div>
                       </div>
                       <div class="form-group row">
                             <div class="col-sm-2"><asp:Label ID="lblNombreDepartamento" class="form-control-label" runat="server" Text="Departamento :"></asp:Label></div>
                             <div class="col-sm-6"> <asp:DropDownList ID="cbn_departamento" class="form-control" runat="server"></asp:DropDownList></div>
                       </div>
                       <div class="form-group row">
                           <div class="col-md-2"> <asp:Button ID="btnRegistra" runat="server" Text="Nuevo" CssClass="btn btn-primary"/> </div>
                           <div class="col-md-2"> <asp:Button ID="btnGuardar" runat="server" Text="Guardar" CssClass="btn btn-success"/> </div>
                           <div class="col-md-2"> <asp:Button ID="btnCancelar" runat="server" Text="Cancelar" CssClass="btn btn-danger" /> </div>
                       </div>
               
                </div>
              </div>
                 <asp:Label ID="lblErrores" style="color:red" runat="server" Text=""></asp:Label>
                   <div class="table-responsive" ">  
        <asp:GridView ID="grd_provincia" Width="100%" runat="server" AutoGenerateColumns="False" CssClass="table table-striped" GridLines="None">
                            <Columns>
                                <asp:BoundField DataField="IdProvincia" HeaderText="ID Provincia" />
                                <asp:BoundField DataField="nombreProvincia" HeaderText="Nombre Provincia" />
                                <asp:BoundField DataField="IdDepartamento" HeaderText="ID Departamento" />  
                                <asp:BoundField DataField="nombreDepartamento" HeaderText="Nombre Departamento" />
                                <asp:TemplateField>
                                    <ItemTemplate>
                                        <asp:Button ID="btnEditar" CssClass="linkin btn btn-info" runat="server" Text="Editar" />
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField>
                                    <ItemTemplate>
                                        <asp:Button ID="btnEliminar" runat="server" Text="Eliminar" class="linkin_eliminar" CssClass="linkin_eliminar btn btn-warning" />
                                    </ItemTemplate>
                                </asp:TemplateField>
                               
                            </Columns>
         </asp:GridView>
                             
                         
                </div>
            </div>
</asp:Content>
