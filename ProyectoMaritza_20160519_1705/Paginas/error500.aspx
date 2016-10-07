<%@ Page Title="" Language="C#" MasterPageFile="~/Plantilla.Master" AutoEventWireup="true" CodeBehind="error500.aspx.cs" Inherits="ProyectoMaritza_20160519_1705.Paginas.error500" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <section class="error500">
        <figure>
            <img src="../imagenes/error500.png" alt="Imagen no disponible."/>
        </figure>
        <h1>Error 500</h1>
        <p>Error interno del servidor.</p>
    </section>
</asp:Content>
