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
    public partial class WfMovimiento : System.Web.UI.Page
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
            cargarCombo();
        }
        private void cargarCombo()
        {
            CCTipoMovimiento oCCTipoMovimiento = new CCTipoMovimiento();
            List<TipoMovimiento> oListTipoMovimiento = new List<TipoMovimiento>();
            oListTipoMovimiento = oCCTipoMovimiento.listarTipoMovimiento();
            ddl_tipo_mov_popup.DataSource = oListTipoMovimiento;
            ddl_tipo_mov_popup.DataTextField = "nombreTipoMovimiento";
            ddl_tipo_mov_popup.DataValueField = "IdTipoMovimiento";
            ddl_tipo_mov_popup.DataBind();
        }

        [WebMethod]
        public static String buscar_personal(string num_doc)
        {
            CCPersonal oCCPersonal = new CCPersonal();
            Result_transaccion oResult_transaccion = new Result_transaccion();
            Personal oPersonal = oCCPersonal.consultarPersonal(num_doc);
            var serializador = new JavaScriptSerializer();
            String cadena_retorno = serializador.Serialize(oPersonal);
            return cadena_retorno;
        }

        [WebMethod]
        public static String listar_movimiento(int personal_id)
        {
            CCMovimiento oCCMovimientos = new CCMovimiento();
            Result_transaccion oResult_transaccion = new Result_transaccion();
            List<Movimiento> oListMovimientos = oCCMovimientos.obtenerMovimiento(personal_id);
            var serializador = new JavaScriptSerializer();
            String cadena_retorno = serializador.Serialize(oListMovimientos);
            return cadena_retorno;
        }

        [WebMethod]
        public static String eliminar_movimiento(int IdMovimiento)
        {
            Result_transaccion oResult_transaccion = new Result_transaccion();
            CCMovimiento oCCMovimientos = new CCMovimiento();
            if (!oCCMovimientos.eliminarMovimiento(IdMovimiento))
            {
                oResult_transaccion.msg_error = "No se pudo eliminar";
                var serializador = new JavaScriptSerializer();
                String cadena_retorno = serializador.Serialize(oResult_transaccion);
                return cadena_retorno;
            }
            else
            {
                oResult_transaccion.msg_error = "Eliminado con éxito";
                var serializador = new JavaScriptSerializer();
                String cadena_retorno = serializador.Serialize(oResult_transaccion);
                return cadena_retorno;
            }
        }

        [WebMethod]
        public static String guardar_movimiento(string modo_edicion, int IdMovimiento, string descripcionMovimiento, int IdPersonal, string fechaMovimiento, decimal montoMovimiento, int IdTipoMovimiento)
        {
            Result_transaccion oResult_transaccion = new Result_transaccion();
            Movimiento oMovimientos = new Movimiento();
            oMovimientos.IdMovimiento = IdMovimiento;
            oMovimientos.descripcionMovimiento = descripcionMovimiento;
            oMovimientos.fechaMovimiento = fechaMovimiento;
            oMovimientos.montoMovimiento = montoMovimiento;
            oMovimientos.IdPersonal = IdPersonal;
            oMovimientos.tipoMovimiento.IdTipoMovimiento = IdTipoMovimiento;
            CCMovimiento oCCMovimientos = new CCMovimiento();
            List<Object> oListObject = new List<Object>();
            if (modo_edicion == "E")
            {
                oCCMovimientos.actualizarMovimiento(oMovimientos);
                oResult_transaccion.msg_error = "Editado con éxito";
                oListObject.Add(modo_edicion);
                oListObject.Add(oMovimientos);
            }
            else
            {
                oCCMovimientos.insertarMovimiento(oMovimientos);
                List<Movimiento> oListMovimientos = new List<Movimiento>();
                oListMovimientos = oCCMovimientos.obtenerMovimiento(IdPersonal);
                oListObject.Add(modo_edicion);
                oListObject.Add(oListMovimientos[oListMovimientos.Count - 1]);
            }
            var serializador = new JavaScriptSerializer();
            String cadena_retorno = serializador.Serialize(oListObject);
            return cadena_retorno;
        }
    }
}