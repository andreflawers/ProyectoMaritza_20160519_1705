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
    public partial class WfDepartamento : System.Web.UI.Page
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
            //txtNombreDepartamento.Enabled = false;
            Mostrar_Lista();
        }

        private void Mostrar_Lista()
        {
            CCDepartamento oCCDepartamento = new CCDepartamento();
            List<Departamento> oListDepartamento = new List<Departamento>();
            oListDepartamento = oCCDepartamento.listarDepartamento();
            grd_departamento.DataSource = oListDepartamento;
            grd_departamento.DataBind();
        }

        [WebMethod]
        public static String Save_Datos_Departamento(string modo_edicion, int IdDepartamento, String nombreDepartamento)
        {
            resultado_transac oResultadoTransac = new resultado_transac();
            Departamento oDepartamento = new Departamento();
            oDepartamento.IdDepartamento = IdDepartamento;
            oDepartamento.nombreDepartamento = nombreDepartamento;
            CCDepartamento oCCDepartamento = new CCDepartamento();
            oResultadoTransac.nro = 0;

            if (modo_edicion == "N")
            {
                oResultadoTransac.msg_error = "Insertado con éxito";
                int nro_anterior = oCCDepartamento.listarDepartamento().Count;
                oCCDepartamento.insertarDepartamento(oDepartamento);
                int nro_nuevo = oCCDepartamento.listarDepartamento().Count;
                List<Departamento> oListDepartamento = new List<Departamento>();
                oListDepartamento = oCCDepartamento.listarDepartamento();
                oResultadoTransac.new_codigo = oListDepartamento[oListDepartamento.Count - 1].IdDepartamento.ToString();
                List<String> oListString = new List<string>();
                oListString.Add(oListDepartamento[oListDepartamento.Count - 1].IdDepartamento.ToString());
                oListString.Add(oListDepartamento[oListDepartamento.Count - 1].nombreDepartamento);
                oResultadoTransac.oList = oListString;
                if (nro_nuevo > nro_anterior) oResultadoTransac.nro = 1;
            }
            else
            {
                oCCDepartamento.actualizarDepartamento(oDepartamento);
                List<Departamento> oListDepartamento = oCCDepartamento.listarDepartamento();
                for (int i = 0; i < oListDepartamento.Count;i++ )
                {
                    int xx = oListDepartamento[i].IdDepartamento;
                    string yy = oListDepartamento[i].nombreDepartamento;
                    if (xx == IdDepartamento && yy == nombreDepartamento) oResultadoTransac.nro = 1;
                }
                oResultadoTransac.msg_error = "Editado con éxito";
            }
            var serializador = new JavaScriptSerializer();
            String cadena_retorno = serializador.Serialize(oResultadoTransac);
            return cadena_retorno;
        }

        [WebMethod]
        public static String Delete_Departamento(int IdDepartamento)
        {
            resultado_transac oResultadoTransac = new resultado_transac();
            CCDepartamento oCCDepartamento = new CCDepartamento();

            if (!oCCDepartamento.eliminarDepartamento(IdDepartamento))
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