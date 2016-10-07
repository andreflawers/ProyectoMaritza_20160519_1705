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
    public partial class WfDescuento : System.Web.UI.Page
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
            CCDescuento oCCDescuento = new CCDescuento();
            List<Descuento> oListDescuento = new List<Descuento>();
            oListDescuento = oCCDescuento.listarDescuento();
            grd_descuento.DataSource = oListDescuento;
            grd_descuento.DataBind();
        }

        [WebMethod]
        public static String Save_Datos_Descuento(string modo_edicion, int IdDescuento, String nombreDescuento, decimal montoDescuento)
        {
            resultado_transac oResultadoTransac = new resultado_transac();
            Descuento oDescuento = new Descuento();
            oDescuento.IdDescuento = IdDescuento;
            oDescuento.nombreDescuento = nombreDescuento;
            oDescuento.montoDescuento = montoDescuento;
            CCDescuento oCCDescuento = new CCDescuento();

            if (modo_edicion == "N")
            {
                oResultadoTransac.msg_error = "Insertado con éxito";
                oCCDescuento.insertarDescuento(oDescuento);
                List<Descuento> oListDescuento = new List<Descuento>();
                oListDescuento = oCCDescuento.listarDescuento();
                oResultadoTransac.new_codigo = oListDescuento[oListDescuento.Count - 1].IdDescuento.ToString();
                List<String> oListString = new List<String>();
                oListString.Add(oListDescuento[oListDescuento.Count - 1].IdDescuento.ToString());
                oListString.Add(oListDescuento[oListDescuento.Count - 1].nombreDescuento);
                oListString.Add(oListDescuento[oListDescuento.Count - 1].montoDescuento.ToString());
                oResultadoTransac.oList = oListString;
            }
            else
            {
                oCCDescuento.actualizarDescuento(oDescuento);
                oResultadoTransac.msg_error = "Editado con éxito";
            }
            var serializador = new JavaScriptSerializer();
            String cadena_retorno = serializador.Serialize(oResultadoTransac);
            return cadena_retorno;
        }

        [WebMethod]
        public static String Delete_Descuento(int IdDescuento)
        {
            resultado_transac oResultadoTransac = new resultado_transac();
            CCDescuento oCCDescuento = new CCDescuento();

            if (!oCCDescuento.eliminarDescuento(IdDescuento))
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