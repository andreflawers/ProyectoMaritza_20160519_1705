<%@ Page Title="" Language="C#" MasterPageFile="~/Plantilla.Master" AutoEventWireup="true" CodeBehind="RegistrarModulo.aspx.cs" Inherits="ProyectoMaritza_20160519_1705.Paginas.RegistrarModulo" 
    EnableEventValidation="false"%>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <section class="FormModulo">
        <form action="/" method="post" style="margin-top: 19px">
            <fieldset class="form-inline">
                <legend>Registro Módulo</legend>
                <div class="form-group">
                    <asp:Label ID="lblDescripcionModulo" runat="server" Text="Nombre de Módulo : "></asp:Label>
                    <asp:TextBox ID="txtDescripcionModulo" runat="server" maxlength="50" CssClass="form-control"></asp:TextBox>
                </div>
                <asp:TextBox ID="txtIdModificar" runat="server"></asp:TextBox>
                <div>
                    <asp:Button ID="btnAgregar" CssClass="btn btn-primary" runat="server" Text="Agregar" OnClick="btnAgregar_Click"  />
                    <asp:Button ID="btnModificar" CssClass="btn btn-primary" runat="server" Text="Modificar" Enabled="False" OnClick="btnModificar_Click" />
                    <asp:Button ID="btnCancelar" CssClass="btn btn-primary" runat="server" Text="Cancelar" OnClick="btnCancelar_Click"/>
                </div>
                <div class="Alert">
                    <asp:Label ID="lblAlert" runat="server" Text=""></asp:Label>
                </div>
                <div>
                    <asp:GridView ID="gdvListaModulos" runat="server" AutoGenerateColumns="False">
                        <Columns>
                            <asp:BoundField DataField="IdModulo" HeaderText="Modulo Id" />
                            <asp:BoundField DataField="NumModulo" HeaderText="N°" />
                            <asp:BoundField DataField="nombreModulo" HeaderText="Descripcion" />
                            <asp:TemplateField>
                                <ItemTemplate>
                                    <asp:LinkButton ID="lnkEditar" runat="server" CommandArgument='<%# Eval("NumModulo") %>' OnClick="lnkEditar_Click">Editar</asp:LinkButton>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField>
                                <ItemTemplate>
                                    <asp:LinkButton ID="lnkEliminar" runat="server" CommandArgument='<%# Eval("IdModulo") %>' OnClick="lnkEliminar_Click" OnClientClick="return confirm('¿Desea eliminar la fila?');">Eliminar</asp:LinkButton>
                                </ItemTemplate>
                            </asp:TemplateField>
                        </Columns>
                    </asp:GridView>
                </div>
            </fieldset>
        </form>
    </section>
</asp:Content>
