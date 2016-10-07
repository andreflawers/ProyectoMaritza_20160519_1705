<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="LogueoSistema.aspx.cs" Inherits="ProyectoMaritza_20160519_1705.Paginas.LogueoSistema" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />

    <title>Logueo de Sistema</title>
    <link href="../imagenes/favicon.png" rel="icon">
     <link href="../css/bootstrap.css" rel="stylesheet" />
    <link href="../css/style-login.css" rel="stylesheet" />
    <link href='http://fonts.googleapis.com/css?family=Open+Sans:700,300,600,800,400' rel='stylesheet' type='text/css'>
    <link href='http://fonts.googleapis.com/css?family=Marvel:400,700' rel='stylesheet' type='text/css'>

    <script src="../js/jquery-1.8.3.min.js"></script>
    <script src="../js/logica.js"></script>
</head>
<body>
    <h1>Login del Sistema</h1>
    <div class="login-form">
        <div class="form-info">

            <form id="formLogueo" runat="server" class="Logueo">
                <div class="col-md-12">
                   
                    <asp:TextBox ID="txtUsuario" runat="server" MaxLength="40" class="form-control" placeholder="Usuario"></asp:TextBox>
                </div><br /><br /><br />
                <div class="col-md-12">
                    
                    <asp:TextBox ID="txtPassword" runat="server" TextMode="Password" MaxLength="10" minlength="10" class="form-control"  placeholder="Password"></asp:TextBox>
                </div><br /><br /><br />
                <div class="col-md-12">
                    <asp:Button ID="btnLogueo" CssClass="btn btn-default" runat="server" Text="Logueo" OnClick="btnLogueo_Click" />
                </div><br /><br /><br />
                <div class="logueo-modulo Alert col-md-12">
                    <asp:Label ID="lblAlert" runat="server" Text=""></asp:Label>
                </div>
            </form>
        </div>
    </div>


</body>
</html>
