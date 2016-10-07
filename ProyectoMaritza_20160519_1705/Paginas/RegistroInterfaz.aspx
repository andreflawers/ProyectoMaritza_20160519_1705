<%@ Page Title="" Language="C#" MasterPageFile="~/Plantilla.Master" AutoEventWireup="true" CodeBehind="RegistroInterfaz.aspx.cs" Inherits="ProyectoMaritza_20160519_1705.Paginas.RegistroInterfaz" 
    EnableEventValidation="false"%>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <section class="FormInterfaz">
        <form action="/" method="post" style="margin-top: 19px">
            <fieldset class="form-inline">
                <legend>Registro Interfaces</legend>
                <div class="form-group">
                    <asp:Label ID="lblDescripcionInterfaz" runat="server" Text="Nombre de Interfaz : "></asp:Label>
                    <asp:TextBox ID="txtDescripcionInterfaz" runat="server" maxlength="50"  CssClass="form-control"></asp:TextBox>
                </div>
                <div class="clas_modulo">
                    <asp:Label ID="lblModulo" runat="server" Text="Seleccione Módulo : "></asp:Label>
                    <div class="botonSeleccionModulo">
                        <figure>
                            <img src="../imagenes/grid.png" alt="Imgen no disponibe" />
                        </figure>
                    </div>
                    <div class="poppup_modulo">
                        <figure class="close_poppup">
                            <img src="../imagenes/close.png" alt="Imgen no disponibe" />
                        </figure>
                        <section>
                            <div>
                                <asp:Label ID="lblNombreModulo" runat="server" Text="Nombre del Modulo : " Enabled="True"></asp:Label>
                                <asp:TextBox ID="txtNombreModulo" runat="server" Enabled="False" CssClass="form-control"></asp:TextBox>
                            </div>
                            <asp:GridView ID="gdvListarModulos" runat="server" AutoGenerateColumns="False">
                                <Columns>
                                    <asp:BoundField DataField="IdModulo" HeaderText="IdModulo" />
                                    <asp:BoundField DataField="NumModulo" HeaderText="N°" />
                                    <asp:BoundField DataField="nombreModulo" HeaderText="Modulo" />
                                    <asp:TemplateField>
                                        <ItemTemplate>
                                            <asp:LinkButton ID="lblSeleccionar" runat="server" CommandArgument='<%# Eval("IdModulo") %>' Text="Seleccionar" OnClick="lblSeleccionar_Click"></asp:LinkButton>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                </Columns>
                            </asp:GridView>
                        </section>
                    </div>
                </div>
                <asp:TextBox ID="txtSeleccionModulo" runat="server"></asp:TextBox>
                <asp:TextBox ID="txtInterfazModificar" runat="server"></asp:TextBox>
                <div>
                    <asp:Button ID="btnAgregar" CssClass="btn btn-primary" runat="server" Text="Agregar" OnClick="btnAgregar_Click" />
                    <asp:Button ID="btnModificar" CssClass="btn btn-primary" runat="server" Text="Modificar" OnClick="btnModificar_Click" Enabled="False" />
                    <asp:Button ID="btnCancelar" CssClass="btn btn-primary" runat="server" Text="Cancelar" OnClick="btnCancelar_Click" />
                </div>
                <div class="Alert">
                    <asp:Label ID="lblAlert" runat="server" Text=""></asp:Label>
                </div>
                <div>
                    <asp:GridView ID="gdvListaInterfaz" runat="server" AutoGenerateColumns="False">
                        <Columns>
                            <asp:BoundField DataField="IdModulo" HeaderText="IdModulo" />
                            <asp:BoundField DataField="IdInterfaz" HeaderText="IdInterfaz" />
                            <asp:BoundField DataField="NumInterfaz" HeaderText="N°" />
                            <asp:BoundField DataField="nombreInterfaz" HeaderText="Interfaz" />
                            <asp:TemplateField HeaderText="Estado">
                                <ItemTemplate>
                                    <asp:Label ID="lblEstadoInterfaz" runat="server" Text='<%# Eval("estadoInterfaz") %>'></asp:Label>
                                    <asp:CheckBox ID="chkEstadoInterfaz" runat="server" Checked='<%# Eval("estadoInterfaz") == "Activo" ? true : false %>' />
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:BoundField DataField="nombreModulo" HeaderText="Modulo" />
                            <asp:TemplateField>
                                <ItemTemplate>
                                    <asp:LinkButton ID="lnkEditar" runat="server" CommandArgument='<%# Eval("NumInterfaz") %>' OnClick="lnkEditar_Click">Editar</asp:LinkButton>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField>
                                <ItemTemplate>
                                    <asp:LinkButton ID="lnkEliminar" runat="server" CommandArgument='<%# Eval("IdInterfaz") %>' OnClick="lnkEliminar_Click" OnClientClick="return confirm('¿Desea eliminar la fila?');">Eliminar</asp:LinkButton>
                                </ItemTemplate>
                            </asp:TemplateField>
                        </Columns>
                    </asp:GridView>
                    
                </div>
            </fieldset>
        </form>
    </section>
</asp:Content>
