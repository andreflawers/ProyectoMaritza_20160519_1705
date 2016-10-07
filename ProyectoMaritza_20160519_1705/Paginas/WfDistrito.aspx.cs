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
    public partial class WfDistrito : System.Web.UI.Page
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
            //txtNombreDistrito.Enabled = false;
            Mostrar_Lista();
            cargarCombo();
        }

        private void Mostrar_Lista()
        {
            CCDistrito oCCDistrito = new CCDistrito();
            List<Distrito> oListDistrito = new List<Distrito>();
            oListDistrito = oCCDistrito.listarDistrito();
            grd_distrito.DataSource = oListDistrito;
            grd_distrito.DataBind();
        }

        private void cargarCombo()
        {
            CCProvincia oCCProvincia = new CCProvincia();
            List<Provincia> oListProvincia = new List<Provincia>();
            oListProvincia = oCCProvincia.listarProvincia();
            cbn_provincia.DataSource = oListProvincia;
            cbn_provincia.DataTextField = "nombreProvincia";
            cbn_provincia.DataValueField = "IdProvincia";
            cbn_provincia.DataBind();
        }

        [WebMethod]
        public static String Save_Datos_Distrito(string modo_edicion, int IdDistrito, String nombreDistrito, int IdProvincia)
        {
            resultado_transac oResultadoTransac = new resultado_transac();
            Distrito oDistrito = new Distrito();
            oDistrito.IdDistrito = IdDistrito;
            oDistrito.nombreDistrito = nombreDistrito;
            oDistrito.IdProvincia = IdProvincia;
            oResultadoTransac.nro = 0;

            CCDistrito oCCDistrito = new CCDistrito();
            if (modo_edicion == "N")
            {
                oResultadoTransac.msg_error = "Insertado con éxito";
                int nro_anterior = oCCDistrito.listarDistrito().Count;
                oCCDistrito.insertarDistrito(oDistrito);
                int nro_nuevo = oCCDistrito.listarDistrito().Count;
                List<Distrito> oListDistrito = new List<Distrito>();
                oListDistrito = oCCDistrito.listarDistrito();
                oResultadoTransac.new_codigo = oListDistrito[oListDistrito.Count - 1].IdDistrito.ToString();
                List<String> oListString = new List<String>();
                oListString.Add(oListDistrito[oListDistrito.Count - 1].IdDistrito.ToString());
                oListString.Add(oListDistrito[oListDistrito.Count - 1].nombreDistrito);
                oListString.Add(oListDistrito[oListDistrito.Count - 1].IdProvincia.ToString());
                oListString.Add(oListDistrito[oListDistrito.Count - 1].nombreProvincia);
                oResultadoTransac.oList = oListString;
                if (nro_nuevo > nro_anterior) oResultadoTransac.nro = 1;
            }
            else
            {
                oCCDistrito.actualizarDistrito(oDistrito);
                List<Distrito> oListDistrito = oCCDistrito.listarDistrito(); ;
                for (int i = 0; i < oListDistrito.Count; i++)
                {
                    int xx = oListDistrito[i].IdDistrito;
                    string yy = oListDistrito[i].nombreDistrito;
                    if (xx == IdDistrito && yy == nombreDistrito) oResultadoTransac.nro = 1;
                }
                oResultadoTransac.msg_error = "Editado con éxito";
            }
            var serializador = new JavaScriptSerializer();
            String cadena_retorno = serializador.Serialize(oResultadoTransac);
            return cadena_retorno;
        }

        [WebMethod]
        public static String Delete_Distrito(int IdDistrito)
        {
            resultado_transac oResultadoTransac = new resultado_transac();
            CCDistrito oCCDistrito = new CCDistrito();
            if (!oCCDistrito.eliminarDistrito(IdDistrito))
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