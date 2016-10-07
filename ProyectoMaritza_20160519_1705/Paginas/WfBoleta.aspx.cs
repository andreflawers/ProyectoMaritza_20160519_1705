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
    public partial class WfBoleta : System.Web.UI.Page
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
        }


        [WebMethod]

        public static String consultar_num_boleta(string num_boleta)
        {
            CCBoleta oCCBoleta = new CCBoleta();
            Boleta oBoleta = new Boleta();
            Result_transaccion oResult_transaccion = new Result_transaccion();
            string num_bol = oCCBoleta.consultarNumBoleta(num_boleta);
            var serializador = new JavaScriptSerializer();
            String cadena_retorno = serializador.Serialize(num_bol);
            return cadena_retorno;
        }
        [WebMethod]
        public static String listar_boleta(string num_doc, int mes, int ano)
        {
            CCBoleta oCCBoleta = new CCBoleta();
            Result_transaccion oResult_transaccion = new Result_transaccion();
            Boleta oBoleta = oCCBoleta.consultarBoleta(num_doc, mes, ano);
            var serializador = new JavaScriptSerializer();
            String cadena_retorno = serializador.Serialize(oBoleta);
            return cadena_retorno;
        }

        [WebMethod]
        public static String listar_boleta_remuneracion(string num_doc)
        {
            CCBoleta oCCBoleta = new CCBoleta();
            Result_transaccion oResult_transaccion = new Result_transaccion();
            List<Remuneracion> oListRemuneracion = oCCBoleta.consultarBoletaRemuneracion(num_doc);
            var serializador = new JavaScriptSerializer();
            String cadena_retorno = serializador.Serialize(oListRemuneracion);
            return cadena_retorno;
        }

        [WebMethod]
        public static String listar_boleta_movimiento(string num_doc, int mes, int ano)
        {
            CCBoleta oCCBoleta = new CCBoleta();
            Result_transaccion oResult_transaccion = new Result_transaccion();
            List<Movimiento> oListMovimiento = oCCBoleta.consultarMovimientos(num_doc, mes, ano);
            var serializador = new JavaScriptSerializer();
            String cadena_retorno = serializador.Serialize(oListMovimiento);
            return cadena_retorno;
        }

        [WebMethod]
        public static String listar_descuento_planilla(string par)
        {
            CCBoleta oCCBoleta = new CCBoleta();
            Result_transaccion oResult_transaccion = new Result_transaccion();
            List<DescuentoPlanilla> oListDescuentoPlanilla = oCCBoleta.consultarDescuentoPlanilla(par);
            var serializador = new JavaScriptSerializer();
            String cadena_retorno = serializador.Serialize(oListDescuentoPlanilla);
            return cadena_retorno;
        }

        [WebMethod]
        public static String listar_boleta_descuento(string num_doc)
        {
            CCBoleta oCCBoleta = new CCBoleta();
            Result_transaccion oResult_transaccion = new Result_transaccion();
            List<Descuento> oListDescuento = oCCBoleta.consultarDescuentos(num_doc);
            var serializador = new JavaScriptSerializer();
            String cadena_retorno = serializador.Serialize(oListDescuento);
            return cadena_retorno;
        }

        [WebMethod]
        public static bool insertar_boleta_final(int id_personal, string fecha_emision_boleta, string neto_a_cobrar, string id_boleta, int id_descuento_planilla, string monto_boleta_planilla, int mes, int ano)
        {
            //List<Object> oListObject = new List<object>();
            CCBoleta oCCBoleta = new CCBoleta();
            Result_transaccion oResult_transaccion = new Result_transaccion();
            //oListObject.Add();
            oCCBoleta.insertar_boleta(id_personal, DateTime.Parse(fecha_emision_boleta), decimal.Parse(neto_a_cobrar), id_boleta, id_descuento_planilla, decimal.Parse(monto_boleta_planilla), mes, ano);
            //var serializador = new JavaScriptSerializer();
            //String cadena_retorno = serializador.Serialize();
            //return cadena_retorno;
            return true;
        }
    }
}