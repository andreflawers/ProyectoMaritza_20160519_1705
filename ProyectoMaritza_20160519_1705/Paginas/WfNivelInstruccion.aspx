<%@ Page Title="" Language="C#" MasterPageFile="~/Plantilla.Master" AutoEventWireup="true" CodeBehind="WfNivelInstruccion.aspx.cs" Inherits="ProyectoMaritza_20160519_1705.Paginas.WfNivelInstruccion" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
     <script src="../js/jquery-1.8.3.min.js"></script>
    <script src="../js/ajax_nivelInstruccion.js"></script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <asp:HiddenField ID="hidden_transac" runat="server" /> 
	    <div " style="width:inherit; background-color:white" >
                     <div class="panel-heading" style="text-align:center; background-color:#F2F5A9">
                         <h3>Nivel de Instrucción</h3>
                      </div>
	       
                     <div class="panel-primary">
                         
                    <div class="panel panel-body">
                       <div class="form-group row">
                             <div class="col-sm-3"><asp:Label ID="lblNombreNivelInstruccion" class="form-control-label"  runat="server" Text=" Nombre Nivel Instruccion :"></asp:Label></div>
                             <div class="col-sm-6"><asp:TextBox ID="txtNombreNivelInstruccion"  class="form-control" runat="server"></asp:TextBox></div>
                       </div>
                       <div class="form-group row">
                           <div class="col-md-2"> <asp:Button ID="btnRegistra" runat="server" Text="Nuevo" CssClass="btn btn-primary"/> </div>
                           <div class="col-md-2"> <asp:Button ID="btnGuardar" runat="server" Text="Guardar" CssClass="btn btn-success" /> </div>
                           <div class="col-md-2"> <asp:Button ID="btnCancelar" runat="server" Text="Cancelar" CssClass="btn btn-danger" /> </div>
                       </div>
               
                    </div>
                      </div>

            <asp:Label ID="lblErrores" style="color:red" runat="server" Text=""></asp:Label>
            <div class="table-responsive">     
        <asp:GridView ID="grd_nivelInstruccion" Width="100%" runat="server" AutoGenerateColumns="False" CssClass="table table-striped" GridLines="None" >
                            <Columns>
                                <asp:BoundField DataField="IdNivelInstruccion" HeaderText="ID NivelInstruccion"/>
                                <asp:BoundField DataField="nombreNivelInstruccion" HeaderText="Nombre Nivel Instruccion" />
                                <asp:TemplateField>
                                    <ItemTemplate>
                                        <asp:Button ID="btnEditar" runat="server" Text="Editar" CssClass="linkin btn btn-info" />
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField>
                                    <ItemTemplate>
                                        <asp:Button ID="btnEliminar" runat="server" Text="Eliminar" CssClass="linkin_eliminar btn btn-warning"  />
                                    </ItemTemplate>
                                </asp:TemplateField>
                               
                            </Columns>
         </asp:GridView>
                </div>
                            
                        
           
             </div>
</asp:Content>
