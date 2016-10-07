using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Entidad;
using Controladora;

namespace ProyectoMaritza_20160519_1705
{
    public partial class Plantilla : System.Web.UI.MasterPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            Logueo oLogin = new Logueo();
            oLogin = (Logueo)Session["oDatosUsuario"];
            if (oLogin == null)
            {
                Response.Redirect("LogueoSistema.aspx");
            }

        }

        public List<MenuSistema> EstructuraMenu()
        {
            string error = "";
            Logueo oLogin = new Logueo();
            oLogin = (Logueo)Session["oDatosUsuario"];

            List<MenuSistema> lista_Menu = new List<MenuSistema>();
            lista_Menu = CCLogueo.Menu_Listar(oLogin, error);
            return lista_Menu;
        }

        public static string ToUrlSlug(string value)
        {

            //First to lower case
            value = value.ToLowerInvariant();

            //Remove all accents
            var bytes = Encoding.GetEncoding("Cyrillic").GetBytes(value);
            value = Encoding.ASCII.GetString(bytes);

            //Replace spaces
            value = Regex.Replace(value, @"\s", "-", RegexOptions.Compiled);

            //Remove invalid chars
            value = Regex.Replace(value, @"[^a-z0-9\s-_]", "", RegexOptions.Compiled);

            //Trim dashes from end
            value = value.Trim('-', '_');

            //Replace double occurences of - or _
            value = Regex.Replace(value, @"([-_]){2,}", "$1", RegexOptions.Compiled);

            return value;
        }

        public static string ObtenerNombreUrl()
        {
            string url_actual = HttpContext.Current.Request.Url.AbsolutePath;
            char delimitador = '/';
            string[] arreglo_partes_url = url_actual.Split(delimitador);
            return (arreglo_partes_url[arreglo_partes_url.Length - 1]).Replace(".aspx", "").Trim();
        }

        protected void lnkSalirSistema_Click(object sender, EventArgs e)
        {
            Session.Clear();
            Response.Redirect("LogueoSistema.aspx");
        }
    }
}