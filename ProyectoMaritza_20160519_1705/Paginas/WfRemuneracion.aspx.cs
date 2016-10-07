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
    public partial class WfRemuneracion : System.Web.UI.Page
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
            CCRemuneracion oCCRemuneracion = new CCRemuneracion();
            List<Remuneracion> oListRemuneracion = new List<Remuneracion>();
            oListRemuneracion = oCCRemuneracion.listarRemuneracion();
            grd_remuneracion.DataSource = oListRemuneracion;
            grd_remuneracion.DataBind();
        }

        [WebMethod]
        public static String Save_Datos_Remuneracion(string modo_edicion, int IdRemuneracion, String nombreRemuneracion, decimal montoRemuneracion)
        {
            resultado_transac oResultadoTransac = new resultado_transac();
            Remuneracion oRemuneracion = new Remuneracion();
            oRemuneracion.IdRemuneracion = IdRemuneracion;
            oRemuneracion.nombreRemuneracion = nombreRemuneracion;
            oRemuneracion.montoRemuneracion = montoRemuneracion;
            CCRemuneracion oCCRemuneracion = new CCRemuneracion();

            if (modo_edicion == "N")
            {
                oResultadoTransac.msg_error = "Insertado con éxito";
                oCCRemuneracion.insertarRemuneracion(oRemuneracion);
                List<Remuneracion> oListRemuneracion = new List<Remuneracion>();
                oListRemuneracion = oCCRemuneracion.listarRemuneracion();
                oResultadoTransac.new_codigo = oListRemuneracion[oListRemuneracion.Count - 1].IdRemuneracion.ToString();
                List<String> oListString = new List<String>();
                oListString.Add(oListRemuneracion[oListRemuneracion.Count - 1].IdRemuneracion.ToString());
                oListString.Add(oListRemuneracion[oListRemuneracion.Count - 1].nombreRemuneracion);
                oListString.Add(oListRemuneracion[oListRemuneracion.Count - 1].montoRemuneracion.ToString());
                oResultadoTransac.oList = oListString;
            }
            else
            {
                oCCRemuneracion.actualizarRemuneracion(oRemuneracion);
                oResultadoTransac.msg_error = "Editado con éxito";
            }
            var serializador = new JavaScriptSerializer();
            String cadena_retorno = serializador.Serialize(oResultadoTransac);
            return cadena_retorno;
        }

        [WebMethod]
        public static String Delete_Remuneracion(int IdRemuneracion)
        {
            resultado_transac oResultadoTransac = new resultado_transac();
            CCRemuneracion oCCRemuneracion = new CCRemuneracion();
            if (!oCCRemuneracion.eliminarRemuneracion(IdRemuneracion))
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