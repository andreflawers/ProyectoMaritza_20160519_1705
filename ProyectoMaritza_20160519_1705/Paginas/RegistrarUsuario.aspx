<%@ Page Title="" Language="C#" MasterPageFile="~/Plantilla.Master" AutoEventWireup="true" CodeBehind="RegistrarUsuario.aspx.cs" Inherits="ProyectoMaritza_20160519_1705.Paginas.RegistrarUsuario" 
    EnableEventValidation="false"%>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <section class="FormUsuario">
        <form action="/" method="post" style="margin-top: 19px">
            <fieldset class="form-inline">
                <legend>Registro Usuarios</legend>
                <div class="form-group">
                    <asp:Label ID="lblUsuario" runat="server" Text="Nombre de Usuario : "></asp:Label>
                    <asp:TextBox ID="txtNombreUsuario" runat="server" maxlength="40" CssClass="form-control"></asp:TextBox>
                </div>
                <div class="clas_personal">
                    <asp:Label ID="lblPersonal" runat="server" Text="Seleccione Personal : "></asp:Label>
                    <div class="botonSeleccionPersonal">
                        <figure>
                            <img src="../imagenes/grid.png" alt="Imgen no disponibe" />
                        </figure>
                    </div>
                    <div class="poppup_personal">
                        <figure class="close_poppup">
                            <img src="../imagenes/close.png" alt="Imgen no disponibe" />
                        </figure>
                        <section>
                            <div>
                                <asp:Label ID="lblNombresPersonal" runat="server" Text="Nombres del Personal : " Enabled="True"></asp:Label>
                                <asp:TextBox ID="txtNombresPersonal" runat="server" Enabled="False" CssClass="form-control"></asp:TextBox>
                            </div>
                            <div>
                                <asp:Label ID="lblApellidosPersonal" runat="server" Text="Apellidos del Personal : " Enabled="True"></asp:Label>
                                <asp:TextBox ID="txtApellidosPersonal" runat="server" Enabled="False" CssClass="form-control"></asp:TextBox>
                            </div>
                            <div>
                                <asp:Label ID="lblDocumento" runat="server" Text="Documento del Personal : " Enabled="True"></asp:Label>
                                <asp:TextBox ID="txtDocumento" runat="server" Enabled="False" CssClass="form-control"></asp:TextBox>
                            </div>
                            <asp:TextBox ID="txtIdPersonal" runat="server"></asp:TextBox>
                            <asp:GridView ID="gdvListarPoppupPersonal" runat="server" AutoGenerateColumns="False">
                                <Columns>
                                    <asp:BoundField DataField="IdPersonal" HeaderText="IdPersonal" />
                                    <asp:BoundField DataField="NumPersonal" HeaderText="N°" />
                                    <asp:BoundField DataField="nombrePersonal" HeaderText="Nombres del Personal" />
                                    <asp:TemplateField HeaderText="Apellidos Del Personal">
                                        <ItemTemplate>
                                            <asp:Label ID="lblApellidoPaterno" runat="server" Text='<%# Eval("apellidoPaternoPersonal") %>'></asp:Label>
                                            <asp:Label ID="lblApellidoMaterno" runat="server" Text='<%# Eval("apellidoMaternoPersonal") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Documento Personal">
                                        <ItemTemplate>
                                            <asp:Label ID="lblnombreTipoDocumento" runat="server" Text='<%# Eval("nombreTipoDocumento") %>'></asp:Label>&nbsp;:&nbsp;
                                            <asp:Label ID="lblnumeroDocumentoPersonal" runat="server" Text='<%# Eval("numeroDocumentoPersonal") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField>
                                        <ItemTemplate>
                                            <asp:LinkButton ID="lbkSeleccionarPersonal" runat="server" CommandArgument='<%# Eval("IdPersonal") %>' Text="Seleccionar"></asp:LinkButton>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                </Columns>
                            </asp:GridView>
                        </section>
                    </div>
                </div>
                <div class="clas_rol">
                    <asp:Label ID="lblRol" runat="server" Text="Seleccione Rol de Usuario : "></asp:Label>
                    <div class="botonSeleccionRol">
                        <figure>
                            <img src="../imagenes/grid.png" alt="Imgen no disponibe" />
                        </figure>
                    </div>
                    <div class="poppup_rol">
                        <figure class="close_poppup">
                            <img src="../imagenes/close.png" alt="Imgen no disponibe" />
                        </figure>
                        <section>
                            <div>
                                <asp:Label ID="lblPoppupRol" runat="server" Text="Rol : " Enabled="True"></asp:Label>
                                <asp:TextBox ID="txtPoppupRol" runat="server" Enabled="False" CssClass="form-control"></asp:TextBox>
                            </div>
                            <asp:TextBox ID="txtIdRol" runat="server"></asp:TextBox>
                            <asp:GridView ID="gdvListarPoppupRol" runat="server" AutoGenerateColumns="False">
                                <Columns>
                                    <asp:BoundField DataField="IdRol" HeaderText="IdRol" />
                                    <asp:BoundField DataField="NumRol" HeaderText="N°" />
                                    <asp:BoundField DataField="nombreRol" HeaderText="Rol" />
                                    <asp:TemplateField>
                                        <ItemTemplate>
                                            <asp:LinkButton ID="lbkSeleccionarRol" runat="server" CommandArgument='<%# Eval("IdRol") %>' Text="Seleccionar"></asp:LinkButton>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                </Columns>
                            </asp:GridView>
                        </section>
                    </div>
                </div>
                <div class="pass form-group">
                    <div class="your-pass">
                        <asp:Label ID="lblContrasena" runat="server" Text="Contraseña de Usuario : "></asp:Label>
                        <asp:TextBox ID="txtContrasena" Type="password" runat="server" maxlength="10" minlength="10" CssClass="form-control"></asp:TextBox>
                    </div>
                    <div class="consult-modificar-pass">
                        <asp:CheckBox ID="chkModificarContrsena" runat="server" Text="Modificar Contraseña" />
                    </div>
                </div>
                <div class="confirmar-pass form-group">
                    <asp:Label ID="lblConfirmarContrasena" runat="server" Text="Confirmar Contraseña : "></asp:Label>
                    <asp:TextBox ID="txtConfirmarContrasena" Type="password" runat="server" maxlength="10" minlength="10" CssClass="form-control"></asp:TextBox>
                </div>
                <asp:TextBox ID="txtIdModificar" runat="server"></asp:TextBox>
                <div>
                    <asp:Button ID="btnAgregar" CssClass="btn btn-primary" runat="server" Text="Agregar" OnClick="btnAgregar_Click" />
                    <asp:Button ID="btnModificar" CssClass="btn btn-primary" runat="server" Text="Modificar" Enabled="False" OnClick="btnModificar_Click" />
                    <asp:Button ID="btnCancelar" CssClass="btn btn-primary" runat="server" Text="Cancelar" OnClick="btnCancelar_Click" />
                </div>
                <div class="Alert">
                    <asp:Label ID="lblAlert" runat="server" Text=""></asp:Label>
                </div>
                <div>
                    <asp:GridView ID="gdvListaUsuarios" runat="server" AutoGenerateColumns="False">
                        <Columns>
                            <asp:BoundField DataField="IdPersonal" HeaderText="IdPersonal" />
                            <asp:BoundField DataField="IdRol" HeaderText="IdRol" />
                            <asp:BoundField DataField="IdUsuario" HeaderText="IdUsuario" />
                            <asp:BoundField DataField="NumUsuario" HeaderText="N°" />
                            <asp:BoundField DataField="nombreUsuario" HeaderText="Usuario" />
                            <asp:TemplateField HeaderText="Estado">
                                <ItemTemplate>
                                    <asp:Label ID="Label1" runat="server" Text='<%# Eval("estadoUsuario") %>'></asp:Label>
                                    <asp:CheckBox ID="CheckBox1" runat="server" Checked='<%# Eval("estadoUsuario") == "Activo" ? true : false %>' />
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:BoundField DataField="nombreRol" HeaderText="Rol" />
                            <asp:BoundField DataField="nombrePersonal" HeaderText="Nombre Personal" />
                            <asp:TemplateField HeaderText="Apellidos Del Personal">
                                <ItemTemplate>
                                    <asp:Label ID="lblApellidoPaterno" runat="server" Text='<%# Eval("apellidoPaternoPersonal") %>'></asp:Label>&nbsp;
                                    <asp:Label ID="lblApellidoMaterno" runat="server" Text='<%# Eval("apellidoMaternoPersonal") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Documento Personal">
                                <ItemTemplate>
                                    <asp:Label ID="lblnombreTipoDocumento" runat="server" Text='<%# Eval("nombreTipoDocumento") %>'></asp:Label>&nbsp;:&nbsp;
                                    <asp:Label ID="lblnumeroDocumentoPersonal" runat="server" Text='<%# Eval("numeroDocumentoPersonal") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField>
                                <ItemTemplate>
                                    <asp:LinkButton ID="lnkEditar" runat="server" CommandArgument='<%# Eval("NumUsuario") %>'>Editar</asp:LinkButton>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField>
                                <ItemTemplate>
                                    <asp:LinkButton ID="lnkEliminar" runat="server" CommandArgument='<%# Eval("IdUsuario") %>' OnClick="lnkEliminar_Click" OnClientClick="return confirm('¿Desea eliminar la fila?');">Eliminar</asp:LinkButton>
                                </ItemTemplate>
                            </asp:TemplateField>
                        </Columns>
                    </asp:GridView>
                </div>
                
            </fieldset>
        </form>
    </section>
</asp:Content>
