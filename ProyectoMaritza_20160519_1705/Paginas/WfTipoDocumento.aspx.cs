using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Entidad;
using Controladora;
using System.Web.Script.Serialization;
using System.Web.Services;

namespace ProyectoMaritza_20160519_1705.Paginas
{
    public partial class WfTipoDocumento : System.Web.UI.Page
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
            //txtNombreTipoDocumento.Enabled = false;
            Mostrar_Lista();
        }

        private void Mostrar_Lista()
        {
            CCTipoDocumento oCCTipoDocumento = new CCTipoDocumento();
            List<TipoDocumento> oListCETipoDocumento = new List<TipoDocumento>();
            oListCETipoDocumento = oCCTipoDocumento.listarTipoDocumento();
            grd_tipo_documento.DataSource = oListCETipoDocumento;
            grd_tipo_documento.DataBind();
        }

        [WebMethod]
        public static String Save_Datos_Tipo_Documento(string modo_edicion, int IdTipoDocumento, String nombreTipoDocumento, int longitudTipoDocumento)
        {
            resultado_transac oResultadoTransac = new resultado_transac();
            TipoDocumento oCETipoDeDocumento = new TipoDocumento();
            oCETipoDeDocumento.IdTipoDocumento = IdTipoDocumento;
            oCETipoDeDocumento.nombreTipoDocumento = nombreTipoDocumento;
            oCETipoDeDocumento.longitudTipoDocumento = longitudTipoDocumento;
            oResultadoTransac.nro = 0;

            CCTipoDocumento oCCTipoDocumento = new CCTipoDocumento();
            if (modo_edicion == "N")
            {
                oResultadoTransac.msg_error = "Insertado con éxito";
                int nro_anterior = oCCTipoDocumento.listarTipoDocumento().Count;
                oCCTipoDocumento.insertarTipoDocumento(oCETipoDeDocumento);
                List<TipoDocumento> oListTipoDocumento = new List<TipoDocumento>();
                int nro_nuevo = oCCTipoDocumento.listarTipoDocumento().Count;
                oListTipoDocumento = oCCTipoDocumento.listarTipoDocumento();
                oResultadoTransac.new_codigo = oListTipoDocumento[oListTipoDocumento.Count - 1].IdTipoDocumento.ToString();
                List<String> oList = new List<String>();
                oList.Add(oListTipoDocumento[oListTipoDocumento.Count - 1].IdTipoDocumento.ToString());
                oList.Add(oListTipoDocumento[oListTipoDocumento.Count - 1].nombreTipoDocumento);
                oList.Add(oListTipoDocumento[oListTipoDocumento.Count - 1].longitudTipoDocumento.ToString());
                oResultadoTransac.oList = oList;
                if (nro_nuevo > nro_anterior) oResultadoTransac.nro = 1;
            }
            else
            {
                oCCTipoDocumento.actualizarTipoDocumento(oCETipoDeDocumento);
                List<TipoDocumento> oListTipoDocumento = oCCTipoDocumento.listarTipoDocumento(); ;
                for (int i = 0; i < oListTipoDocumento.Count; i++)
                {
                    int xx = oListTipoDocumento[i].IdTipoDocumento;
                    string yy = oListTipoDocumento[i].nombreTipoDocumento;
                    if (xx == IdTipoDocumento && yy == nombreTipoDocumento) oResultadoTransac.nro = 1;
                }
                oResultadoTransac.msg_error = "Editado con éxito";
            }
            var serializador = new JavaScriptSerializer();
            String cadena_retorno = serializador.Serialize(oResultadoTransac);
            return cadena_retorno;
        }

        [WebMethod]
        public static String Delete_Tipo_Documento(int IdTipoDocumento)
        {
            resultado_transac oResultadoTransac = new resultado_transac();
            CCTipoDocumento oCCTipoDocumento = new CCTipoDocumento();
            if (!oCCTipoDocumento.eliminarTipoDocumento(IdTipoDocumento))
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

        protected void grd_tipo_documento_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
    }
}