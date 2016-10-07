using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using Controladora;
using Entidad;
using System.Web.Services;
using System.Text;
using System.Security.Cryptography;

namespace ProyectoMaritza_20160519_1705.Paginas
{
    public partial class RegistrarUsuario : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

            //-----------------------------------------------------------------------
            Logueo oLogin = new Logueo();
            oLogin = (Logueo)Session["oDatosUsuario"];

            string nombre_pagina = Plantilla.ObtenerNombreUrl();

            List<string> paginas = CCLogueo.Validar_Pagina_Acceso(oLogin);
            int permiso = 0;
            foreach (var interfaz in paginas)
            {
                if (Plantilla.ToUrlSlug(interfaz) == nombre_pagina) permiso = 1;
            }
            if (permiso == 0) Response.Redirect("error404.aspx");
            //-----------------------------------------------------------------------

            if (!Page.IsPostBack)
            {
                Usuario_Listar();
                Rol_Listar();
                Personal_Listar();
                LimpiarEntradas();
            }
        }

        private void Usuario_Listar()
        {
            List<Usuario> lista_Usuario = new List<Usuario>();
            string error = "";
            lista_Usuario = CCUsuario.Usuario_Listar(error);
            gdvListaUsuarios.DataSource = lista_Usuario;
            gdvListaUsuarios.DataBind();
        }

        private void Rol_Listar()
        {
            List<Rol> lista_Rol = new List<Rol>();
            string error = "";
            lista_Rol = CCRol.Rol_Listar(error);
            gdvListarPoppupRol.DataSource = lista_Rol;
            gdvListarPoppupRol.DataBind();
        }

        private void Personal_Listar()
        {
            List<Usuario> lista_Personal = new List<Usuario>();
            string error = "";
            lista_Personal = CCUsuario.Usuario_Personal_Listar(error);
            gdvListarPoppupPersonal.DataSource = lista_Personal;
            gdvListarPoppupPersonal.DataBind();
        }

        /*-----------------------------------------------------------------*/
        protected void btnAgregar_Click(object sender, EventArgs e)
        {
            if (txtNombreUsuario.Text.Trim() != "" && txtIdPersonal.Text.Trim() != "" &&
                txtIdRol.Text.Trim() != "" && txtContrasena.Text.Trim() != "" &&
                txtConfirmarContrasena.Text.Trim() != "" &&
                txtContrasena.Text.Trim() == txtConfirmarContrasena.Text.Trim() &&
                txtContrasena.Text.Trim().Length == 10)
            {
                int existente = 0;
                string error = "";
                Usuario oUsuario = new Usuario();
                oUsuario.nombreUsuario = txtNombreUsuario.Text.Trim();
                oUsuario.passwordUsuario = EncryptKey(txtContrasena.Text.Trim()).Substring(0, 10);
                oUsuario.IdPersonal = int.Parse(txtIdPersonal.Text);
                oUsuario.IdRol = int.Parse(txtIdRol.Text.Trim());
                existente = CCUsuario.Usuario_Agregar(oUsuario, error).IdUsuario;
                LimpiarEntradas();
                Usuario_Listar();

                if (existente == 1)
                {
                    lblAlert.Text = "Usuario agregado con éxito.";
                    lblAlert.CssClass = "alertSuccess";
                }
                else
                {
                    lblAlert.Text = "Usuario existente.";
                    lblAlert.CssClass = "alertDanger";
                }
            }
            LimpiarEntradas();
        }
        /*-----------------------------------------------------------------*/

        /*-----------------------------------------------------------------*/
        protected void btnModificar_Click(object sender, EventArgs e)
        {
            lblAlert.Text = "";
            if (txtNombreUsuario.Text.Trim() != "" && txtIdPersonal.Text.Trim() != "" &&
                txtIdRol.Text.Trim() != "" &&
                txtContrasena.Text.Trim() == txtConfirmarContrasena.Text.Trim())
            {
                Usuario oUsuario = new Usuario();
                String error = "";

                oUsuario.IdUsuario = int.Parse(txtIdModificar.Text.Trim());
                oUsuario.nombreUsuario = txtNombreUsuario.Text.Trim();
                oUsuario.IdPersonal = int.Parse(txtIdPersonal.Text.Trim());
                oUsuario.IdRol = int.Parse(txtIdRol.Text.Trim());

                if (txtContrasena.Text.Trim() == "")
                {
                    oUsuario.passwordUsuario = "";
                    error = CCUsuario.Usuario_Modificar(oUsuario, error);
                }
                else if (txtContrasena.Text.Trim().Length == 10 && txtContrasena.Text.Trim() != "")
                {
                    oUsuario.passwordUsuario = EncryptKey(txtContrasena.Text.Trim()).Substring(0, 10);
                    error = CCUsuario.Usuario_Modificar(oUsuario, error);
                }

                Usuario_Listar();
                LimpiarEntradas();

                HabilitarBotones(true, false);

                if (error == "")
                {
                    lblAlert.Text = "Usuario modificado con éxito.";
                    lblAlert.CssClass = "alertSuccess";
                }
                else
                {
                    lblAlert.Text = error;
                    lblAlert.CssClass = "alertDanger";
                }

            }
            LimpiarEntradas();
        }
        /*-----------------------------------------------------------------*/

        /*-----------------------------------------------------------------*/
        protected void lnkEliminar_Click(object sender, EventArgs e)
        {
            Usuario oUsuario = new Usuario();
            string error = "";
            LinkButton lnk = (LinkButton)sender;
            String codigo = lnk.CommandArgument;
            oUsuario.IdUsuario = int.Parse(codigo);

            lblAlert.Text = CCUsuario.Usuario_Eliminar(oUsuario, error);

            Usuario_Listar();
            Rol_Listar();
            Personal_Listar();
            LimpiarEntradas();

            HabilitarBotones(true, false);
            lblAlert.Text = "Usuario eliminado con éxito.";
            lblAlert.CssClass = "alertSuccess";
        }
        /*-----------------------------------------------------------------*/

        /*-----------------------------------------------------------------*/
        protected void btnCancelar_Click(object sender, EventArgs e)
        {
            LimpiarEntradas();
            lblAlert.Text = "";
            lblAlert.CssClass = "";
        }
        /*-----------------------------------------------------------------*/

        [WebMethod]
        public static List<Usuario> ObtenerDatosUsuario(string IdUsuarioModificar)
        {
            Usuario oUsuario = new Usuario();
            String error = "";

            oUsuario.IdUsuario = int.Parse(IdUsuarioModificar);
            return CCUsuario.Usuario_ListarDatos(oUsuario, error);
        }

        [WebMethod]
        public static String ModificarEstadoUsuario(string estadoUsuarioNum, string idEstadoUsuario)
        {
            Usuario oUsuario = new Usuario();
            String error = "";

            oUsuario.estadoUsuario = estadoUsuarioNum;
            oUsuario.IdUsuario = int.Parse(idEstadoUsuario);
            CCUsuario.Usuario_ModificarEstado(oUsuario, error);
            return "respuesta";
        }

        public void HabilitarBotones(Boolean estado_1, Boolean estado_2)
        {
            btnAgregar.Enabled = estado_1;
            btnModificar.Enabled = estado_2;
        }

        public void LimpiarEntradas()
        {
            txtNombreUsuario.Text = "";
            txtContrasena.Text = "";
            txtConfirmarContrasena.Text = "";
            txtApellidosPersonal.Text = "";
            txtDocumento.Text = "";
            txtIdModificar.Text = "";
            txtIdPersonal.Text = "";
            txtIdRol.Text = "";
            txtNombresPersonal.Text = "";
            txtPoppupRol.Text = "";
            chkModificarContrsena.Checked = false;
        }

        public string EncryptKey(string cadena)
        {
            string key = "ABCDEFG54669525PQRSTUVWXYZabcdef852846opqrstuvwxyz";
            //arreglo de bytes donde guardaremos la llave
            byte[] keyArray;
            //arreglo de bytes donde guardaremos el texto
            //que vamos a encriptar
            byte[] Arreglo_a_Cifrar =
            UTF8Encoding.UTF8.GetBytes(cadena);

            //se utilizan las clases de encriptación
            //provistas por el Framework
            //Algoritmo MD5
            MD5CryptoServiceProvider hashmd5 =
            new MD5CryptoServiceProvider();
            //se guarda la llave para que se le realice
            //hashing
            keyArray = hashmd5.ComputeHash(
            UTF8Encoding.UTF8.GetBytes(key));

            hashmd5.Clear();

            //Algoritmo 3DAS
            TripleDESCryptoServiceProvider tdes =
            new TripleDESCryptoServiceProvider();

            tdes.Key = keyArray;
            tdes.Mode = CipherMode.ECB;
            tdes.Padding = PaddingMode.PKCS7;

            //se empieza con la transformación de la cadena
            ICryptoTransform cTransform =
            tdes.CreateEncryptor();

            //arreglo de bytes donde se guarda la
            //cadena cifrada
            byte[] ArrayResultado =
            cTransform.TransformFinalBlock(Arreglo_a_Cifrar,
            0, Arreglo_a_Cifrar.Length);

            tdes.Clear();

            //se regresa el resultado en forma de una cadena
            return Convert.ToBase64String(ArrayResultado,
                   0, ArrayResultado.Length);
        }
    }
}