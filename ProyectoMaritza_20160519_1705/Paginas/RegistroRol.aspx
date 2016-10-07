<%@ Page Title="" Language="C#" MasterPageFile="~/Plantilla.Master" AutoEventWireup="true" CodeBehind="RegistroRol.aspx.cs" Inherits="ProyectoMaritza_20160519_1705.Paginas.RegistroRol" 
    EnableEventValidation="false"%>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <section class="FormRol">
        <form action="/" method="post" style="margin-top: 19px">
            <fieldset class="form-inline">
                <legend>Registro Roles</legend>
                <div class="form-group">
                    <asp:Label ID="lblDescripcionRol" runat="server" Text="Nombre de Rol : "></asp:Label>
                    <asp:TextBox ID="txtDescripcionRol" runat="server" maxlength="50" CssClass="form-control"></asp:TextBox>
                </div>
                <div class="clas_interfaz">
                    <asp:Label ID="lblInterfaces" runat="server" Text="Seleccione Interfaces : "></asp:Label>
                    <div class="botonSeleccionInterfaces">
                        <figure>
                            <img src="../imagenes/grid.png" alt="Imgen no disponibe" />
                        </figure>
                    </div>
                    <div class="poppup_interfaz">
                        <figure class="close_poppup">
                            <img src="../imagenes/close.png" alt="Imgen no disponibe" />
                        </figure>
                        <section>
                            <div>
                                <asp:Label ID="lblSeleccioneInterfaces" runat="server" Text="Seleccione Interfaces : "></asp:Label>
                            </div>
                            <asp:GridView ID="gdvListarPoppupInterfaces" runat="server" AutoGenerateColumns="False">
                                <Columns>
                                    <asp:BoundField DataField="IdInterfaz" HeaderText="IdInterfaz" />
                                    <asp:BoundField DataField="NumInterfaz" HeaderText="N°" />
                                    <asp:BoundField DataField="nombreInterfaz" HeaderText="Interfaz" />
                                    <asp:TemplateField>
                                        <ItemTemplate>
                                            <asp:CheckBox ID="chkSeleccionar" runat="server" />
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                </Columns>
                            </asp:GridView>
                        </section>
                    </div>
                </div>
                <asp:TextBox ID="txtIdModificar" runat="server"></asp:TextBox>
                <div>
                    <asp:Button ID="btnAgregar" CssClass="btn btn-primary" runat="server" Text="Agregar" OnClick="btnAgregar_Click" />
                    <asp:Button ID="btnModificar" CssClass="btn btn-primary" runat="server" Text="Modificar" OnClick="btnModificar_Click" Enabled="False" />
                    <asp:Button ID="btnCancelar" CssClass="btn btn-primary" runat="server" Text="Cancelar" OnClick="btnCancelar_Click" />
                </div>
                <div class="Alert">
                        <asp:Label ID="lblAlert" runat="server" Text=""></asp:Label>
                </div>
                <div>
                    <asp:GridView ID="gdvListaRol" runat="server" AutoGenerateColumns="False">
                        <Columns>
                            <asp:BoundField DataField="IdRol" HeaderText="IdRol" />
                            <asp:BoundField DataField="NumRol" HeaderText="N°" />
                            <asp:BoundField DataField="NombreRol" HeaderText="Rol" />
                            <asp:BoundField DataField="cantidadInterfaces" HeaderText="N° de Interfaces" />
                            <asp:TemplateField>
                                <ItemTemplate>
                                    <asp:LinkButton ID="lnkEditar" runat="server" OnClick="lnkEditar_Click">Editar</asp:LinkButton>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField>
                                <ItemTemplate>
                                    <asp:LinkButton ID="lnkEliminar" runat="server" CommandArgument='<%# Eval("IdRol") %>' OnClick="lnkEliminar_Click" OnClientClick="return confirm('¿Desea eliminar la fila?');">Eliminar</asp:LinkButton>
                                </ItemTemplate>
                            </asp:TemplateField>
                        </Columns>
                    </asp:GridView>
                </div>
            </fieldset>
        </form>
    </section>
</asp:Content>
