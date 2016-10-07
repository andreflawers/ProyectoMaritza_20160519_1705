using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Entidad;
using Controladora;
using System.Web.Services;
using System.Web.Script.Serialization;

namespace ProyectoMaritza_20160519_1705.Paginas
{
    public partial class WfArea : System.Web.UI.Page
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

            if (Page.IsPostBack) return;
            //txtNombreArea.Enabled = false;
            Mostrar_Lista();
        }

        private void Mostrar_Lista()
        {
            CCArea oCCArea = new CCArea();
            List<Area> oListArea = new List<Area>();
            oListArea = oCCArea.listarArea();
            grd_area.DataSource = oListArea;
            grd_area.DataBind();
        }

        [WebMethod]
        public static String Save_Datos_Area(string modo_edicion, int IdArea, String nombreArea)
        {
            resultado_transac oResultadoTransac = new resultado_transac();
            Area oArea = new Area();
            oArea.IdArea = IdArea;
            oArea.nombreArea = nombreArea;

            CCArea oCCArea = new CCArea();
            if (modo_edicion == "N")
            {
                oResultadoTransac.msg_error = "Insertado con éxito";
                oCCArea.insertarArea(oArea);
                List<Area> oListArea = new List<Area>();
                oListArea = oCCArea.listarArea();
                oResultadoTransac.new_codigo = oListArea[oListArea.Count - 1].IdArea.ToString();
                List<String> oListString = new List<String>();
                oListString.Add(oListArea[oListArea.Count - 1].IdArea.ToString());
                oListString.Add(oListArea[oListArea.Count - 1].nombreArea);
                oResultadoTransac.oList = oListString;
            }
            else
            {
                oCCArea.actualizarArea(oArea);
                oResultadoTransac.msg_error = "Editado con éxito";
            }
            var serializador = new JavaScriptSerializer();
            String cadena_retorno = serializador.Serialize(oResultadoTransac);
            return cadena_retorno;
        }

        [WebMethod]
        public static String Delete_Area(int IdArea)
        {
            resultado_transac oResultadoTransac = new resultado_transac();
            CCArea oCCArea = new CCArea();
            if (!oCCArea.eliminarArea(IdArea))
            {
                oResultadoTransac.msg_error = "No se pudo eliminar";
                var serializador = new JavaScriptSerializer();
                String cadena_retorno = serializador.Serialize(oResultadoTransac);
                return cadena_retorno;
            }
            else
            {
                oResultadoTransac.msg_error = "Eliminado con éxito";
                var serializador = new JavaScriptSerializer();
                String cadena_retorno = serializador.Serialize(oResultadoTransac);
                return cadena_retorno;
            }
        }
    }
}