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
    public partial class WfSucursal : System.Web.UI.Page
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
            //txtDireccionSucursal.Enabled = false;
            //txtNombreSucursal.Enabled = false;
            //txtRucSucursal.Enabled = false;
            //txtTelefonoSucursal.Enabled = false;
            Mostrar_Lista();
            cargarCombo();
        }

        private void Mostrar_Lista()
        {
            CCSucursal oCCSucursal = new CCSucursal();
            List<Sucursal> oListSucursal = new List<Sucursal>();
            oListSucursal = oCCSucursal.listarSucursal();
            grd_sucursal.DataSource = oListSucursal;
            grd_sucursal.DataBind();
        }

        private void cargarCombo()
        {
            CCDistrito oCCDistrito = new CCDistrito();
            List<Distrito> oListDistrito = new List<Distrito>();
            oListDistrito = oCCDistrito.listarDistrito();
            cbn_distrito.DataSource = oListDistrito;
            cbn_distrito.DataTextField = "nombreDistrito";
            cbn_distrito.DataValueField = "IdDistrito";
            cbn_distrito.DataBind();
        }

        [WebMethod]
        public static String Save_Datos_Sucursal(string modo_edicion, int IdSucursal, String nombreSucursal, String direccionSucursal, String telefonoSucursal, String rucSucursal, int IdDistrito)
        {
            resultado_transac oResultadoTransac = new resultado_transac();
            Sucursal oSucursal = new Sucursal();
            oSucursal.IdSucursal = IdSucursal;
            oSucursal.nombreSucursal = nombreSucursal;
            oSucursal.direccionSucursal = direccionSucursal;
            oSucursal.telefonoSucursal = telefonoSucursal;
            oSucursal.rucSucursal = rucSucursal;
            oSucursal.IdDistrito = IdDistrito;

            CCSucursal oCCSucursal = new CCSucursal();
            if (modo_edicion == "N")
            {
                oResultadoTransac.msg_error = "Insertado con éxito";
                oCCSucursal.insertarSucursal(oSucursal);
                List<Sucursal> oListSucursal = new List<Sucursal>();
                oListSucursal = oCCSucursal.listarSucursal();
                oResultadoTransac.new_codigo = oListSucursal[oListSucursal.Count - 1].IdSucursal.ToString();
                List<String> oListString = new List<String>();
                oListString.Add(oListSucursal[oListSucursal.Count - 1].IdSucursal.ToString());
                oListString.Add(oListSucursal[oListSucursal.Count - 1].nombreSucursal);
                oListString.Add(oListSucursal[oListSucursal.Count - 1].direccionSucursal);
                oListString.Add(oListSucursal[oListSucursal.Count - 1].telefonoSucursal);
                oListString.Add(oListSucursal[oListSucursal.Count - 1].rucSucursal);
                oListString.Add(oListSucursal[oListSucursal.Count - 1].IdDistrito.ToString());
                oListString.Add(oListSucursal[oListSucursal.Count - 1].nombreDistrito);
                oResultadoTransac.oList = oListString;
            }
            else
            {
                oCCSucursal.actualizarSucursal(oSucursal);
                oResultadoTransac.msg_error = "Editado con éxito";
            }
            var serializador = new JavaScriptSerializer();
            String cadena_retorno = serializador.Serialize(oResultadoTransac);
            return cadena_retorno;
        }

        [WebMethod]
        public static String Delete_Sucursal(int IdSucursal)
        {
            resultado_transac oResultadoTransac = new resultado_transac();
            CCSucursal oCCSucursal = new CCSucursal();
            if (!oCCSucursal.eliminarSucursal(IdSucursal))
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