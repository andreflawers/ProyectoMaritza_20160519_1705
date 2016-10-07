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
    public partial class WfEmpresa : System.Web.UI.Page
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
            //txtRucEmpresa.Enabled = false;
            //txtNombreEmpresa.Enabled = false;
            //txtDireccionEmpresa.Enabled = false;
            //txtDescripcionEmpresa.Enabled = false;
            Mostrar_Lista();
        }

        private void Mostrar_Lista()
        {
            CCEmpresa oCCEmpresa = new CCEmpresa();
            List<Empresa> oListEmpresa = new List<Empresa>();
            oListEmpresa = oCCEmpresa.listarEmpresa();
            grd_empresa.DataSource = oListEmpresa;
            grd_empresa.DataBind();
        }

        [WebMethod]
        public static String Save_Datos_Empresa(string modo_edicion, String rucEmpresa, String nombreEmpresa, String direccionEmpresa, String descripcionEmpresa)
        {
            resultado_transac oResultadoTransac = new resultado_transac();
            Empresa oEmpresa = new Empresa();
            oEmpresa.rucEmpresa = rucEmpresa;
            oEmpresa.nombreEmpresa = nombreEmpresa;
            oEmpresa.direccionEmpresa = direccionEmpresa;
            oEmpresa.descripcionEmpresa = descripcionEmpresa;
            CCEmpresa oCCEmpresa = new CCEmpresa();
            oResultadoTransac.nro = 0;
            if (modo_edicion == "N")
            {
                oResultadoTransac.msg_error = "Insertado con éxito";
                int nro_anterior = oCCEmpresa.listarEmpresa().Count;
                oCCEmpresa.insertarEmpresa(oEmpresa);
                int nro_nuevo = oCCEmpresa.listarEmpresa().Count;
                List<Empresa> oListEmpresa = new List<Empresa>();
                oListEmpresa = oCCEmpresa.listarEmpresa();
                oResultadoTransac.new_codigo = rucEmpresa;
                List<String> oListString = new List<string>();
                oListString.Add(rucEmpresa);
                oListString.Add(nombreEmpresa);
                oListString.Add(direccionEmpresa);
                oListString.Add(descripcionEmpresa);
                oResultadoTransac.oList = oListString;
                if (nro_nuevo > nro_anterior) oResultadoTransac.nro = 1;
            }
            else
            {
                oCCEmpresa.actualizarEmpresa(oEmpresa);
                oResultadoTransac.msg_error = "Editado con éxito";
            }
            var serializador = new JavaScriptSerializer();
            String cadena_retorno = serializador.Serialize(oResultadoTransac);
            return cadena_retorno;
        }

        [WebMethod]
        public static String Delete_Empresa(String rucEmpresa)
        {
            resultado_transac oResultadoTransac = new resultado_transac();
            CCEmpresa oCCEmpresa = new CCEmpresa();
            if (!oCCEmpresa.eliminarEmpresa(rucEmpresa))
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