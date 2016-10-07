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
    public partial class WfProvincia : System.Web.UI.Page
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
            //txtNombreProvincia.Enabled = false;
            // cbn_departamento.Enabled = false;
            Mostrar_Lista();
            cargarCombo();
        }

        private void Mostrar_Lista()
        {
            CCProvincia oCCProvincia = new CCProvincia();
            List<Provincia> oListProvincia = new List<Provincia>();
            oListProvincia = oCCProvincia.listarProvincia();
            //grd_provincia.Columns[2].Visible = true;
            grd_provincia.DataSource = oListProvincia;
            grd_provincia.DataBind();
            //grd_provincia.Columns[2].Visible = false;
        }

        private void cargarCombo()
        {
            CCDepartamento oCCDepartamento = new CCDepartamento();
            List<Departamento> oListDepartamento = new List<Departamento>();
            oListDepartamento = oCCDepartamento.listarDepartamento();
            cbn_departamento.DataSource = oListDepartamento;
            cbn_departamento.DataTextField = "nombreDepartamento";
            cbn_departamento.DataValueField = "IdDepartamento";
            cbn_departamento.DataBind();
        }

        [WebMethod]
        public static String Save_Datos_Provincia(string modo_edicion, int IdProvincia, String nombreProvincia, int IdDepartamento)
        {
            resultado_transac oResultadoTransac = new resultado_transac();
            Provincia oProvincia = new Provincia();
            oProvincia.IdProvincia = IdProvincia;
            oProvincia.nombreProvincia = nombreProvincia;
            oProvincia.IdDepartamento = IdDepartamento;
            CCProvincia oCCProvincia = new CCProvincia();
            oResultadoTransac.nro = 0;

            if (modo_edicion == "N")
            {
                oResultadoTransac.msg_error = "Insertado con éxito";
                int nro_anterior = oCCProvincia.listarProvincia().Count;
                oCCProvincia.insertarProvincia(oProvincia);
                int nro_nuevo = oCCProvincia.listarProvincia().Count;
                List<Provincia> oListProvincia = new List<Provincia>();
                oListProvincia = oCCProvincia.listarProvincia();
                oResultadoTransac.new_codigo = oListProvincia[oListProvincia.Count - 1].IdProvincia.ToString();
                List<String> oListString = new List<String>();
                oListString.Add(oListProvincia[oListProvincia.Count - 1].IdProvincia.ToString());
                oListString.Add(oListProvincia[oListProvincia.Count - 1].nombreProvincia);
                oListString.Add(oListProvincia[oListProvincia.Count - 1].IdDepartamento.ToString());
                oListString.Add(oListProvincia[oListProvincia.Count - 1].nombreDepartamento);
                oResultadoTransac.oList = oListString;
                if (nro_nuevo > nro_anterior) oResultadoTransac.nro = 1;
            }
            else
            {
                oCCProvincia.actualizarProvincia(oProvincia);
                List<Provincia> oListProvincia = oCCProvincia.listarProvincia();
                for (int i = 0; i < oListProvincia.Count; i++)
                {
                    int xx = oListProvincia[i].IdProvincia;
                    string yy = oListProvincia[i].nombreProvincia;
                    if (xx == IdProvincia && yy == nombreProvincia) oResultadoTransac.nro = 1;
                }
                oResultadoTransac.msg_error = "Editado con éxito";
            }
            var serializador = new JavaScriptSerializer();
            String cadena_retorno = serializador.Serialize(oResultadoTransac);
            return cadena_retorno;
        }

        [WebMethod]
        public static String Delete_Provincia(int IdProvincia)
        {
            resultado_transac oResultadoTransac = new resultado_transac();
            CCProvincia oCCProvincia = new CCProvincia();
            if (!oCCProvincia.eliminarProvincia(IdProvincia))
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