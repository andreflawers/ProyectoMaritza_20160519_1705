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
    public partial class WfTipoMovimiento : System.Web.UI.Page
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
            CCTipoMovimiento oCCTipoMovimiento = new CCTipoMovimiento();
            List<TipoMovimiento> oListTipoMovimiento = new List<TipoMovimiento>();
            oListTipoMovimiento = oCCTipoMovimiento.listarTipoMovimiento();
            grd_tipoMovimiento.DataSource = oListTipoMovimiento;
            grd_tipoMovimiento.DataBind();
        }

        [WebMethod]
        public static String Save_Datos_TipoMovimiento(string modo_edicion, int IdTipoMovimiento, String nombreTipoMovimiento)
        {
            resultado_transac oResultadoTransac = new resultado_transac();
            TipoMovimiento oTipoMovimiento = new TipoMovimiento();
            oTipoMovimiento.IdTipoMovimiento = IdTipoMovimiento;
            oTipoMovimiento.nombreTipoMovimiento = nombreTipoMovimiento;
            CCTipoMovimiento oCCTipoMovimiento = new CCTipoMovimiento();

            if (modo_edicion == "N")
            {
                oResultadoTransac.msg_error = "Insertado con éxito";
                oCCTipoMovimiento.insertarTipoMovimiento(oTipoMovimiento);
                List<TipoMovimiento> oListTipoMovimiento = new List<TipoMovimiento>();
                oListTipoMovimiento = oCCTipoMovimiento.listarTipoMovimiento();
                oResultadoTransac.new_codigo = oListTipoMovimiento[oListTipoMovimiento.Count - 1].IdTipoMovimiento.ToString();
                List<String> oListString = new List<String>();
                oListString.Add(oListTipoMovimiento[oListTipoMovimiento.Count - 1].IdTipoMovimiento.ToString());
                oListString.Add(oListTipoMovimiento[oListTipoMovimiento.Count - 1].nombreTipoMovimiento);
                oResultadoTransac.oList = oListString;
            }
            else
            {
                oCCTipoMovimiento.actualizarTipoMovimiento(oTipoMovimiento);
                oResultadoTransac.msg_error = "Editado con éxito";
            }
            var serializador = new JavaScriptSerializer();
            String cadena_retorno = serializador.Serialize(oResultadoTransac);
            return cadena_retorno;
        }

        [WebMethod]
        public static String Delete_TipoMovimiento(int IdTipoMovimiento)
        {
            resultado_transac oResultadoTransac = new resultado_transac();
            CCTipoMovimiento oCCTipoMovimiento = new CCTipoMovimiento();
            if (!oCCTipoMovimiento.eliminarTipoMovimiento(IdTipoMovimiento))
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