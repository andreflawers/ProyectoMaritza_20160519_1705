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
    public partial class WfTipoPermiso : System.Web.UI.Page
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
            Mostrar_Lista();
        }

        private void Mostrar_Lista()
        {
            CCTipoPermiso oCCTipoPermiso = new CCTipoPermiso();
            List<TipoPermiso> oListTipoPermiso = new List<TipoPermiso>();
            oListTipoPermiso = oCCTipoPermiso.listarTipoPermiso();
            grd_tipoPermiso.DataSource = oListTipoPermiso;
            grd_tipoPermiso.DataBind();
        }

        [WebMethod]
        public static String Save_Datos_TipoPermiso(string modo_edicion, int IdTipoPermiso, String nombreTipoPermiso, decimal remuneracionTipoPermiso)
        {
            resultado_transac oResultadoTransac = new resultado_transac();
            TipoPermiso oTipoPermiso = new TipoPermiso();
            oTipoPermiso.IdTipoPermiso = IdTipoPermiso;
            oTipoPermiso.nombreTipoPermiso = nombreTipoPermiso;
            oTipoPermiso.remuneracionTipoPermiso = remuneracionTipoPermiso;
            CCTipoPermiso oCCTipoPermiso = new CCTipoPermiso();

            if (modo_edicion == "N")
            {
                oResultadoTransac.msg_error = "Insertado con éxito";
                oCCTipoPermiso.insertarTipoPermiso(oTipoPermiso);
                List<TipoPermiso> oListPermiso = new List<TipoPermiso>();
                oListPermiso = oCCTipoPermiso.listarTipoPermiso();
                oResultadoTransac.new_codigo = oListPermiso[oListPermiso.Count - 1].IdTipoPermiso.ToString();
                List<String> oListString = new List<String>();
                oListString.Add(oListPermiso[oListPermiso.Count - 1].IdTipoPermiso.ToString());
                oListString.Add(oListPermiso[oListPermiso.Count - 1].nombreTipoPermiso);
                oListString.Add(oListPermiso[oListPermiso.Count - 1].remuneracionTipoPermiso.ToString());
                oResultadoTransac.oList = oListString;
            }
            else
            {
                oCCTipoPermiso.actualizarTipoPermiso(oTipoPermiso);
                oResultadoTransac.msg_error = "Editado con éxito";
            }
            var serializador = new JavaScriptSerializer();
            String cadena_retorno = serializador.Serialize(oResultadoTransac);
            return cadena_retorno;
        }

        [WebMethod]
        public static String Delete_TipoPermiso(int IdTipoPermiso)
        {
            resultado_transac oResultadoTransac = new resultado_transac();
            CCTipoPermiso oCCTipoPermiso = new CCTipoPermiso();
            if (!oCCTipoPermiso.eliminarTipoPermiso(IdTipoPermiso))
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