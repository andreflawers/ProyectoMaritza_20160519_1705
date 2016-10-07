using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using Controladora;
using Entidad;
using System.Text;
using System.Security.Cryptography;

namespace ProyectoMaritza_20160519_1705.Paginas
{
    public partial class CambiarContraseña : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }
        /*-----------------------------------------------------------------*/
        protected void btnModificar_Click(object sender, EventArgs e)
        {
            if (txtPasswordAnterior.Text.Trim() != "" &&
                txtNuevoPassword.Text.Trim() == txtConfirmarNuevoPassword.Text.Trim() &&
                txtNuevoPassword.Text.Trim() != "" && txtConfirmarNuevoPassword.Text.Trim() != "")
            {
                string error = "";
                string password_old = "";
                Logueo oLogueo = new Logueo();
                oLogueo = (Logueo)Session["oDatosUsuario"];
                oLogueo.password = EncryptKey(txtNuevoPassword.Text.Trim()).Substring(0, 10);

                password_old = EncryptKey(txtPasswordAnterior.Text.Trim());

                error = CCLogueo.Password_Modificar(oLogueo, password_old, error);

                txtPasswordAnterior.Text = "";
                txtNuevoPassword.Text = "";
                txtConfirmarNuevoPassword.Text = "";

                if (error == "")
                {
                    lblAlert.Text = "Se modifico su contraseña correctamente.";
                    lblAlert.CssClass = "alertSuccess";
                }
                else
                {
                    lblAlert.Text = error;
                    lblAlert.CssClass = "alertDanger";
                }

            }
            else
            {
                lblAlert.Text = "Confirme correctamente su nueva contraseña.";
                lblAlert.CssClass = "alertDanger";
            }
        }
        /*-----------------------------------------------------------------*/


        /*-----------------------------------------------------------------*/
        protected void btnCancelar_Click(object sender, EventArgs e)
        {
            txtNuevoPassword.Text = "";
            txtConfirmarNuevoPassword.Text = "";
            lblAlert.Text = "";
            lblAlert.CssClass = "";
        }
        /*-----------------------------------------------------------------*/

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