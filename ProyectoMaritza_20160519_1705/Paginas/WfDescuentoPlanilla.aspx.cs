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
    public partial class WfDescuentoPlanilla : System.Web.UI.Page
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
            CCDescuentoPlanilla oCCDescuentoPlanilla = new CCDescuentoPlanilla();
            List<DescuentoPlanilla> oListDescuentoPlanilla = new List<DescuentoPlanilla>();
            oListDescuentoPlanilla = oCCDescuentoPlanilla.listarDescuentoPlanilla();
            grd_descuentoPlanilla.DataSource = oListDescuentoPlanilla;
            grd_descuentoPlanilla.DataBind();
        }

        [WebMethod]
        public static String Save_Datos_Descuento_Planilla(string modo_edicion, int IdDescuentoPlanilla, String nombreDescuentoPlanilla, decimal montoDescuento)
        {
            resultado_transac oResultadoTransac = new resultado_transac();
            DescuentoPlanilla oDescuentoPlanilla = new DescuentoPlanilla();
            oDescuentoPlanilla.IdDescuentoPlanilla = IdDescuentoPlanilla;
            oDescuentoPlanilla.nombreDescuentoPlanilla = nombreDescuentoPlanilla;
            oDescuentoPlanilla.montoDescuento = montoDescuento;
            CCDescuentoPlanilla oCCDescuentoPlanilla = new CCDescuentoPlanilla();

            if (modo_edicion == "N")
            {
                oResultadoTransac.msg_error = "Insertado con éxito";
                oCCDescuentoPlanilla.insertarDescuentoPlanilla(oDescuentoPlanilla);
                List<DescuentoPlanilla> oListDescuentoPlanilla = new List<DescuentoPlanilla>();
                oListDescuentoPlanilla = oCCDescuentoPlanilla.listarDescuentoPlanilla();
                oResultadoTransac.new_codigo = oListDescuentoPlanilla[oListDescuentoPlanilla.Count - 1].IdDescuentoPlanilla.ToString();
                List<String> oListString = new List<string>();
                oListString.Add(oListDescuentoPlanilla[oListDescuentoPlanilla.Count - 1].IdDescuentoPlanilla.ToString());
                oListString.Add(oListDescuentoPlanilla[oListDescuentoPlanilla.Count - 1].nombreDescuentoPlanilla);
                oListString.Add(oListDescuentoPlanilla[oListDescuentoPlanilla.Count - 1].montoDescuento.ToString());
                oResultadoTransac.oList = oListString;
            }
            else
            {
                oCCDescuentoPlanilla.actualizarDescuentoPlanilla(oDescuentoPlanilla);
                oResultadoTransac.msg_error = "Editado con éxito";
            }
            var serializador = new JavaScriptSerializer();
            String cadena_retorno = serializador.Serialize(oResultadoTransac);
            return cadena_retorno;
        }

        [WebMethod]
        public static String Delete_Descuento_Planilla(int IdDescuentoPlanilla)
        {
            resultado_transac oResultadoTransac = new resultado_transac();
            CCDescuentoPlanilla oCCDescuentoPlanilla = new CCDescuentoPlanilla();

            if (!oCCDescuentoPlanilla.eliminarDescuentoPlanilla(IdDescuentoPlanilla))
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