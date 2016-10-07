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
    public partial class WfFeriados : System.Web.UI.Page
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
            CCFeriados oCCFeriados = new CCFeriados();
            List<Feriados> oListFeriados = new List<Feriados>();
            oListFeriados = oCCFeriados.listarFeriados();
            grd_feriados.DataSource = oListFeriados;
            grd_feriados.DataBind();
        }

        [WebMethod]
        public static String Save_Datos_Feriados(string modo_edicion, int idFeriado, String fechaFeriado)
        {
            resultado_transac oResultadoTransac = new resultado_transac();
            Feriados oFeriados = new Feriados();
            oFeriados.idFeriado = idFeriado;
            oFeriados.fechaFeriado = fechaFeriado;
            CCFeriados oCCFeriados = new CCFeriados();
            if (modo_edicion == "N")
            {
                oResultadoTransac.msg_error = "Insertado con éxito";
                oCCFeriados.insertarFeriados(oFeriados);
                List<Feriados> oListFeriados = new List<Feriados>();
                oListFeriados = oCCFeriados.listarFeriados();
                oResultadoTransac.new_codigo = oListFeriados[oListFeriados.Count - 1].idFeriado.ToString();
                List<String> oListString = new List<String>();
                oListString.Add(oListFeriados[oListFeriados.Count - 1].idFeriado.ToString());
                oListString.Add(oListFeriados[oListFeriados.Count - 1].fechaFeriado.ToString());
                oResultadoTransac.oList = oListString;
            }
            else
            {
                oCCFeriados.actualizarFeriados(oFeriados);
                oResultadoTransac.msg_error = "Editado con éxito";
            }
            var serializador = new JavaScriptSerializer();
            String cadena_retorno = serializador.Serialize(oResultadoTransac);
            return cadena_retorno;
        }

        [WebMethod]
        public static String Delete_Feriados(int idFeriado)
        {
            resultado_transac oResultadoTransac = new resultado_transac();
            CCFeriados oCCFeriados = new CCFeriados();
            if (!oCCFeriados.eliminarFeriados(idFeriado))
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