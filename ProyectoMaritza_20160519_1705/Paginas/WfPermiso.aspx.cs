using Controladora;
using Entidad;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Script.Serialization;
using System.Web.Services;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace ProyectoMaritza_20160519_1705.Paginas
{
    public partial class WfPermiso : System.Web.UI.Page
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
            cargarCombo();
        }

        private void cargarCombo()
        {
            CCTipoPermiso oCCTipoPermiso = new CCTipoPermiso();
            List<TipoPermiso> oListTipoPermiso = new List<TipoPermiso>();
            oListTipoPermiso = oCCTipoPermiso.listarTipoPermiso();
            drp_tipoPermiso.DataSource = oListTipoPermiso;
            drp_tipoPermiso.DataTextField = "nombreTipoPermiso";
            drp_tipoPermiso.DataValueField = "IdTipoPermiso";
            this.drp_tipoPermiso.DataBind();
        }

        [WebMethod]
        public static String buscar_personal(string num_doc)
        {
            CCPersonal oCCPersonal = new CCPersonal();
            Result_transaccion oResult_transaccion = new Result_transaccion();
            Personal oPersonal = oCCPersonal.consultarPersonal(num_doc);
            var serializador = new JavaScriptSerializer();
            String cadena_retorno = serializador.Serialize(oPersonal);
            return cadena_retorno;
        }


        [WebMethod]
        public static String listar_permisos(int personal_id)
        {
            CCPermiso oCCPermiso = new CCPermiso();
            Result_transaccion oResult_transaccion = new Result_transaccion();
            List<Permiso> oListPermisos = oCCPermiso.obtenerPermisos(personal_id);
            var serializador = new JavaScriptSerializer();
            String cadena_retorno = serializador.Serialize(oListPermisos);
            return cadena_retorno;
        }



        [WebMethod]
        public static String eliminar_permisos(int IdPermiso)
        {
            Result_transaccion oResult_transaccion = new Result_transaccion();
            CCPermiso oCCPermiso = new CCPermiso();
            if (!oCCPermiso.eliminarPermiso(IdPermiso))
            {
                oResult_transaccion.msg_error = "No se pudo eliminar";
                var serializador = new JavaScriptSerializer();
                String cadena_retorno = serializador.Serialize(oResult_transaccion);
                return cadena_retorno;
            }
            else
            {
                oResult_transaccion.msg_error = "Eliminado con éxito";
                var serializador = new JavaScriptSerializer();
                String cadena_retorno = serializador.Serialize(oResult_transaccion);
                return cadena_retorno;
            }
        }

        [WebMethod]
        public static String guardar_permiso(string modo_edicion, int IdPermiso, string fechaIniPermiso, string fechaFinPermiso, string justPermiso, int IdPersonal, int IdTipoPermiso, string nombreTipoPermiso)
        {
            Result_transaccion oResult_transaccion = new Result_transaccion();
            Permiso oPermiso = new Permiso();
            oPermiso.IdPermiso = IdPermiso;
            oPermiso.fechaIniPermiso = fechaIniPermiso;
            oPermiso.fechaFinPermiso = fechaFinPermiso;
            oPermiso.justPermiso = justPermiso;
            oPermiso.IdPersonal = IdPersonal;
            oPermiso.tipoPermiso.IdTipoPermiso = IdTipoPermiso;
            oPermiso.tipoPermiso.nombreTipoPermiso = nombreTipoPermiso;
            CCPermiso oCCPermiso = new CCPermiso();
            List<Object> oListObject = new List<object>();
            if (modo_edicion == "E")
            {
                oCCPermiso.actualizarPermiso(oPermiso);
                oResult_transaccion.msg_error = "Editado con éxito";
                oListObject.Add(modo_edicion);
                oListObject.Add(oPermiso);
            }
            else
            {
                oCCPermiso.insertarPermiso(oPermiso);
                oResult_transaccion.msg_error = "";
                List<Permiso> oListPermiso = new List<Permiso>();
                oListPermiso = oCCPermiso.obtenerPermisos(IdPersonal);
                oListObject.Add(modo_edicion);
                oListObject.Add(oListPermiso[oListPermiso.Count - 1]);
            }
            var serializador = new JavaScriptSerializer();
            String cadena_retorno = serializador.Serialize(oListObject);
            return cadena_retorno;
        }
    }
}