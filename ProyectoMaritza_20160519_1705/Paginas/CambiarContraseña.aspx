<%@ Page Title="" Language="C#" MasterPageFile="~/Plantilla.Master" AutoEventWireup="true" CodeBehind="CambiarContraseña.aspx.cs" Inherits="ProyectoMaritza_20160519_1705.Paginas.CambiarContraseña" 
    EnableEventValidation="false"%>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <section class="FormModificarContrasena form-inline">
        <form action="/" method="post">
            <div class="update-pass form-group">
                <asp:Label ID="lblPasswordAnterior" runat="server" Text="Antigua Contraseña : "></asp:Label>
                <asp:TextBox ID="txtPasswordAnterior" runat="server" TextMode="Password" maxlength="10" minlength="10" CssClass="form-control"></asp:TextBox>
            </div>
            <div class="update-pass form-group">
                <asp:Label ID="lblNuevoPassword" runat="server" Text="Nueva Contraseña : "></asp:Label>
                <asp:TextBox ID="txtNuevoPassword" runat="server" TextMode="Password" maxlength="10" minlength="10" CssClass="form-control"></asp:TextBox>
            </div>
            <div class="update-pass form-group">
                <asp:Label ID="lblConfirmarNuevoPassword" runat="server" Text="Confirmar Nueva Contraseña : "></asp:Label>
                <asp:TextBox ID="txtConfirmarNuevoPassword" runat="server" TextMode="Password" maxlength="10" minlength="10" CssClass="form-control"></asp:TextBox>
            </div>
            <div>
                <asp:Button ID="btnModificar" CssClass="btn btn-primary" runat="server" Text="Modificar" OnClick="btnModificar_Click" />
                <asp:Button ID="btnCancelar" CssClass="btn btn-primary" runat="server" Text="Cancelar" OnClick="btnCancelar_Click" />
            </div>
            <div class="Alert">
                <asp:Label ID="lblAlert" runat="server" Text=""></asp:Label>
            </div>
        </form>
    </section>
</asp:Content>
