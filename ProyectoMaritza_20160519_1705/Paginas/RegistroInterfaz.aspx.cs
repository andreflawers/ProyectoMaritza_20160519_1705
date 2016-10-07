using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using Controladora;
using Entidad;
using System.Web.Services;

namespace ProyectoMaritza_20160519_1705.Paginas
{
    public partial class RegistroInterfaz : System.Web.UI.Page
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

            if (!Page.IsPostBack)
            {
                Interfaz_Listar();
                Modulo_Listar();
            }
        }

        private void Interfaz_Listar()
        {
            List<Interfaz> lista_Interfaz = new List<Interfaz>();
            string error = "";
            lista_Interfaz = CCInterfaz.Interfaz_Listar(error);
            gdvListaInterfaz.DataSource = lista_Interfaz;
            gdvListaInterfaz.DataBind();
        }

        private void Modulo_Listar()
        {
            List<Modulo> lista_Modulos = new List<Modulo>();
            string error = "";
            lista_Modulos = CCModulo.Modulo_Listar(error);
            gdvListarModulos.DataSource = lista_Modulos;
            gdvListarModulos.DataBind();
        }

        protected void lnkEditar_Click(object sender, EventArgs e)
        {
        }

        /*-----------------------------------------------------------------*/
        protected void btnModificar_Click(object sender, EventArgs e)
        {
            lblAlert.Text = "";
            if (txtDescripcionInterfaz.Text.Trim() != "" && txtSeleccionModulo.Text.Trim() != "" && txtInterfazModificar.Text.Trim() != "")
            {
                Interfaz oInterfaz = new Interfaz();
                String error = "";

                oInterfaz.IdInterfaz = int.Parse(txtInterfazModificar.Text.Trim());
                oInterfaz.nombreInterfaz = txtDescripcionInterfaz.Text.Trim();
                oInterfaz.IdModulo = int.Parse(txtSeleccionModulo.Text.Trim());
                error = CCInterfaz.Interfaz_Modificar(oInterfaz, error);

                Interfaz_Listar();
                LimpiarEntradas();

                HabilitarBotones(true, false);
                if (error == "")
                {
                    lblAlert.Text = "Interfaz modificada con éxito.";
                    lblAlert.CssClass = "alertSuccess";
                }
                else
                {
                    lblAlert.Text = error;
                    lblAlert.CssClass = "alertDanger";
                }

            }
            LimpiarEntradas();
        }
        /*-----------------------------------------------------------------*/

        [WebMethod]
        public static String ModificarEstadoInterfaz(string estadoInterfazNum, string idEstadoInterfaz)
        {
            Interfaz oInterfaz = new Interfaz();
            List<Rol> lista_Rol = new List<Rol>();
            String error = "";

            lista_Rol = CCRol.Rol_Listar(error);
            oInterfaz.estadoInterfaz = estadoInterfazNum;
            oInterfaz.IdInterfaz = int.Parse(idEstadoInterfaz);
            return CCInterfaz.Interfaz_ModificarEstado(oInterfaz, lista_Rol, error) + "";
        }

        /*-----------------------------------------------------------------*/
        protected void lnkEliminar_Click(object sender, EventArgs e)
        {
            Interfaz oInterfaz = new Interfaz();
            string error = "";
            LinkButton lnk = (LinkButton)sender;
            String codigo = lnk.CommandArgument;
            oInterfaz.IdInterfaz = int.Parse(codigo);

            error = CCInterfaz.Interfaz_Eliminar(oInterfaz, error);

            Interfaz_Listar();
            LimpiarEntradas();

            HabilitarBotones(true, false);
            if (error == "")
            {
                lblAlert.Text = "Interfaz eliminada con éxito.";
                lblAlert.CssClass = "alertSuccess";
            }
            else
            {
                lblAlert.Text = error;
                lblAlert.CssClass = "alertDanger";
            }
        }
        /*-----------------------------------------------------------------*/

        protected void lblSeleccionar_Click(object sender, EventArgs e)
        {
        }

        /*-----------------------------------------------------------------*/
        protected void btnAgregar_Click(object sender, EventArgs e)
        {
            if (txtDescripcionInterfaz.Text.Trim() != "" && txtSeleccionModulo.Text.Trim() != "")
            {
                int existente = 0;
                Interfaz oInterfaz = new Interfaz();

                List<Rol> oRol = new List<Rol>();
                string errornew = "";
                oRol = CCRol.Rol_Listar(errornew);

                oInterfaz.nombreInterfaz = txtDescripcionInterfaz.Text.Trim();
                oInterfaz.IdModulo = int.Parse(txtSeleccionModulo.Text.Trim());
                string error = "";

                existente = CCInterfaz.Interfaz_Agregar(oInterfaz, oRol, error).IdInterfaz;
                Interfaz_Listar();
                LimpiarEntradas();
                if (existente != 0)
                {
                    lblAlert.Text = "Interfaz agregada con éxito.";
                    lblAlert.CssClass = "alertSuccess";
                }
                else
                {
                    lblAlert.Text = "Interfaz Existente.";
                    lblAlert.CssClass = "alertDanger";
                }
            }
            LimpiarEntradas();
        }
        /*-----------------------------------------------------------------*/

        /*-----------------------------------------------------------------*/
        protected void btnCancelar_Click(object sender, EventArgs e)
        {
            LimpiarEntradas();
            HabilitarBotones(true, false);

            lblAlert.Text = "";
            lblAlert.CssClass = "";
        }
        /*-----------------------------------------------------------------*/

        public void LimpiarEntradas()
        {
            txtDescripcionInterfaz.Text = "";
            txtInterfazModificar.Text = "";
            txtNombreModulo.Text = "";
            txtSeleccionModulo.Text = "";
        }

        public void HabilitarBotones(Boolean estado_1, Boolean estado_2)
        {
            btnAgregar.Enabled = estado_1;
            btnModificar.Enabled = estado_2;
        }
    }
}