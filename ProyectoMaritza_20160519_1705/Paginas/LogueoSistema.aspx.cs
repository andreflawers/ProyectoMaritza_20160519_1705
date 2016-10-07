using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Entidad;
using Controladora;
using System.Text;
using System.Security.Cryptography;

namespace ProyectoMaritza_20160519_1705.Paginas
{
    public partial class LogueoSistema : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void btnLogueo_Click(object sender, EventArgs e)
        {
            if (txtUsuario.Text.Trim() != "" && txtPassword.Text.Trim() != "")
            {
                string error = "";
                Logueo oLogueo = new Logueo();
                oLogueo.usuario = txtUsuario.Text.Trim();
                oLogueo.password = EncryptKey(txtPassword.Text.Trim()).Substring(0, 10);

                oLogueo = CCLogueo.Logueo_Validar(oLogueo, error);

                if (oLogueo.IdUsuario == 0)
                {
                    lblAlert.Text = "Usuario o contraseña incorrectos.";
                    lblAlert.CssClass = "alertDanger";
                }
                else if (oLogueo.IdUsuario == -1)
                {
                    lblAlert.Text = "Usuario Inactivo.";
                    lblAlert.CssClass = "alertDanger";
                }
                else
                {
                    oLogueo.password = "";
                    Session.Add("oDatosUsuario", oLogueo);
                    Response.Redirect("EntradaPrincipal.aspx");
                }
                txtPassword.Text = "";
                txtUsuario.Text = "";
            }
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