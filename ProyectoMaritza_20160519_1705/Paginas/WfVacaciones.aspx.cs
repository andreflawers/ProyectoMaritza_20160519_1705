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
    public partial class WfVacaciones : System.Web.UI.Page
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
        public static String listar_vacaciones(int personal_id)
        {
            CCVacacion oCCVacaciones = new CCVacacion();
            Result_transaccion oResult_transaccion = new Result_transaccion();
            List<Vacacion> oListVacaciones = oCCVacaciones.obtenerVacaciones(personal_id);
            var serializador = new JavaScriptSerializer();
            String cadena_retorno = serializador.Serialize(oListVacaciones);
            return cadena_retorno;
        }

        [WebMethod]
        public static String eliminar_vacaciones(int IdVacaciones)
        {
            Result_transaccion oResult_transaccion = new Result_transaccion();
            CCVacacion oCCVacaciones = new CCVacacion();
            if (!oCCVacaciones.eliminarVacaciones(IdVacaciones))
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
        public static String filtrar_encargado(int valor)
        {
            CCEncargado oCCEncargado = new CCEncargado();
            List<Encargado> oListEncargado = oCCEncargado.listarEncargado();
            var serializador = new JavaScriptSerializer();
            String cadena_retorno = serializador.Serialize(oListEncargado);
            return cadena_retorno;
        }

        [WebMethod]
        public static String guardar_vacaciones(string modo_edicion, int IdVacaciones, string fechaInicioVacaciones, string fechaFinVacaciones, string descripcionVacaciones, int IdPersonal, int IdEncargado)
        {
            Result_transaccion oResult_transaccion = new Result_transaccion();
            Vacacion oVacaciones = new Vacacion();
            oVacaciones.IdVacacion = IdVacaciones;
            oVacaciones.fechaInicioVacacion = fechaInicioVacaciones;
            oVacaciones.fechaFinVacacion = fechaFinVacaciones;
            oVacaciones.descripcionVacacion = descripcionVacaciones;
            oVacaciones.IdPersonal = IdPersonal;
            oVacaciones.encargado.IdEncargado = IdEncargado;
            CCVacacion oCCVacaciones = new CCVacacion();
            List<Object> oListObject = new List<object>();
            if (modo_edicion == "E")
            {
                oCCVacaciones.actualizarVacaciones(oVacaciones);
                oResult_transaccion.msg_error = "Editado con éxito";
                oListObject.Add(modo_edicion);
                oListObject.Add(oVacaciones);
            }
            else
            {
                oCCVacaciones.insertarVacaciones(oVacaciones);
                oResult_transaccion.msg_error = "Eliminado con éxito";
                List<Vacacion> oListVacaciones = new List<Vacacion>();
                oListVacaciones = oCCVacaciones.obtenerVacaciones(IdPersonal);
                oListObject.Add(modo_edicion);
                oListObject.Add(oListVacaciones[oListVacaciones.Count - 1]);
            }
            var serializador = new JavaScriptSerializer();
            String cadena_retorno = serializador.Serialize(oListObject);
            return cadena_retorno;
        }
    }
}