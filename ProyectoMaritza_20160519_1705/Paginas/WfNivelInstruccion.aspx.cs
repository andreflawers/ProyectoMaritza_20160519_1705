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
    public partial class WfNivelInstruccion : System.Web.UI.Page
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
            //txtNombreNivelInstruccion.Enabled = false;
            Mostrar_Lista();
        }

        private void Mostrar_Lista()
        {
            CCNivelInstruccion oCCNivelInstruccion = new CCNivelInstruccion();
            List<NivelInstruccion> oListNivelInstruccion = new List<NivelInstruccion>();
            oListNivelInstruccion = oCCNivelInstruccion.listarNivelInstruccion();
            grd_nivelInstruccion.DataSource = oListNivelInstruccion;
            grd_nivelInstruccion.DataBind();
        }

        [WebMethod]
        public static String Save_Datos_Nivel_Instruccion(string modo_edicion, int IdNivelInstruccion, String nombreNivelInstruccion)
        {
            resultado_transac oResultadoTransac = new resultado_transac();
            NivelInstruccion oNivelInstruccion = new NivelInstruccion();
            oNivelInstruccion.IdNivelInstruccion = IdNivelInstruccion;
            oNivelInstruccion.nombreNivelInstruccion = nombreNivelInstruccion;
            CCNivelInstruccion oCCNivelInstruccion = new CCNivelInstruccion();

            if (modo_edicion == "N")
            {
                oResultadoTransac.msg_error = "Insertado con éxito";
                oCCNivelInstruccion.insertarNivelInstruccion(oNivelInstruccion);
                List<NivelInstruccion> oListNivelInstruccion = new List<NivelInstruccion>();
                oListNivelInstruccion = oCCNivelInstruccion.listarNivelInstruccion();
                oResultadoTransac.new_codigo = oListNivelInstruccion[oListNivelInstruccion.Count - 1].IdNivelInstruccion.ToString();
                List<String> oListString = new List<String>();
                oListString.Add(oListNivelInstruccion[oListNivelInstruccion.Count - 1].IdNivelInstruccion.ToString());
                oListString.Add(oListNivelInstruccion[oListNivelInstruccion.Count - 1].nombreNivelInstruccion);
                oResultadoTransac.oList = oListString;
            }
            else
            {
                oCCNivelInstruccion.actualizarNivelInstruccion(oNivelInstruccion);
                oResultadoTransac.msg_error = "Editado con éxito";
            }
            var serializador = new JavaScriptSerializer();
            String cadena_retorno = serializador.Serialize(oResultadoTransac);
            return cadena_retorno;
        }

        [WebMethod]
        public static String Delete_Nivel_Instruccion(int IdNivelInstruccion)
        {
            resultado_transac oResultadoTransac = new resultado_transac();
            CCNivelInstruccion oCCNivelInstruccion = new CCNivelInstruccion();
            oCCNivelInstruccion.eliminarNivelInstruccion(IdNivelInstruccion);
            if (!oCCNivelInstruccion.eliminarNivelInstruccion(IdNivelInstruccion))
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