<%@ Page Title="" Language="C#" MasterPageFile="~/Plantilla.Master" AutoEventWireup="true" CodeBehind="WfCargo.aspx.cs" Inherits="ProyectoMaritza_20160519_1705.Paginas.WfCargo" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
     <script src="../js/jquery-1.8.3.min.js"></script>
    <script src="../js/ajax_cargo.js"></script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <asp:HiddenField ID="hidden_transac" runat="server" /> 
	   
	          <div  style="width:inherit; background:white" >
                 
                  <div class="panel-heading " style="text-align:center; background-color:#F2F5A9">                     
                         <h3>Cargo</h3>
                      </div>
                   </br>
                  <div class="panel-body"> 
                            <div class="form-group row">
                                <div class="col-sm-3">
                                    <asp:Label ID="lblNombreTipoCargo" runat="server" class="form-control-label" Text="Nombre Cargo :"></asp:Label>
                                </div>
                                <div class="col-sm-6">
                                    <asp:TextBox ID="txtNombreCargo" class="form-control" runat="server"></asp:TextBox>
                                </div>
                            </div>
                            <div class="form-group row">
                                <div class="col-sm-3">
                                    <asp:Label ID="lblMontoSalario" runat="server" class="form-control-label" Text="Monto Salario :"></asp:Label>
                                </div>
                                <div class="col-sm-6">
                                    <asp:TextBox ID="txtMontoSalario" type="text" class="form-control" runat="server"></asp:TextBox>
                                </div>
                            </div>
                            <div class="form-group row">
                                <div class="col-md-2">
                                    <asp:Button ID="btnRegistra" runat="server" Text="Nuevo" CssClass="btn btn-primary" />
                                </div>
                                <div class="col-md-2">
                                    <asp:Button ID="btnGuardar" runat="server" Text="Guardar" CssClass="btn btn-success" />
                                </div>
                                <div class="col-md-2">
                                    <asp:Button ID="btnCancelar" runat="server" Text="Cancelar" CssClass="btn btn-danger" />
                                </div>
                            </div>
                    </div>

                  <asp:Label ID="lblErrores" style="color:red" runat="server" Text=""></asp:Label>
             
                    <div class="table-responsive">                 
                  
                            <asp:GridView ID="grd_cargo" Width="100%" runat="server" AutoGenerateColumns="False" CssClass=" table table-striped footable" GridLines="None" style="text-align:left">
                                  <Columns>
                                      <asp:BoundField DataField="IdCargo" HeaderText="ID Cargo" />
                                      <asp:BoundField DataField="nombreCargo" HeaderText="Nombre Cargo" />
                                      <asp:BoundField DataField="montoSalarioCargo" HeaderText="Monto Salario" />
                                      <asp:TemplateField>
                                          <ItemTemplate>
                                              <asp:Button ID="btnEditar" runat="server" Text="Editar" CssClass="linkin btn btn-info" />
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
