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
    public partial class WfCargo : System.Web.UI.Page
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
            CCCargo oCCCargo = new CCCargo();
            List<Cargo> oListCargo = new List<Cargo>();
            oListCargo = oCCCargo.listarCargo();
            grd_cargo.DataSource = oListCargo;
            grd_cargo.DataBind();
        }

        [WebMethod]
        public static String Save_Datos_Cargo(string modo_edicion, int IdCargo, String nombreCargo, decimal montoSalarioCargo)
        {
            resultado_transac oResultadoTransac = new resultado_transac();
            Cargo oCargo = new Cargo();
            oCargo.IdCargo = IdCargo;
            oCargo.nombreCargo = nombreCargo;
            oCargo.montoSalarioCargo = montoSalarioCargo;
            CCCargo oCCCargo = new CCCargo();

            if (modo_edicion == "N")
            {
                oResultadoTransac.msg_error = "Insertado con éxito";
                oCCCargo.insertarCargo(oCargo);
                List<Cargo> oListCargo = new List<Cargo>();
                oListCargo = oCCCargo.listarCargo();
                oResultadoTransac.new_codigo = oListCargo[oListCargo.Count - 1].IdCargo.ToString();
                List<String> oListString = new List<String>();
                oListString.Add(oListCargo[oListCargo.Count - 1].IdCargo.ToString());
                oListString.Add(oListCargo[oListCargo.Count - 1].nombreCargo);
                oListString.Add(oListCargo[oListCargo.Count - 1].montoSalarioCargo.ToString());
                oResultadoTransac.oList = oListString;
            }
            else
            {
                oCCCargo.actualizarCargo(oCargo);
                oResultadoTransac.msg_error = "Editado con éxito";
            }
            var serializador = new JavaScriptSerializer();
            String cadena_retorno = serializador.Serialize(oResultadoTransac);
            return cadena_retorno;
        }

        [WebMethod]
        public static String Delete_Cargo(int IdCargo)
        {
            resultado_transac oResultadoTransac = new resultado_transac();
            CCCargo oCCCargo = new CCCargo();
            if (!oCCCargo.eliminarCargo(IdCargo))
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