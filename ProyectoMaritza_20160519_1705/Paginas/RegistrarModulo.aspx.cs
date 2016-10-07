using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using Controladora;
using Entidad;

namespace ProyectoMaritza_20160519_1705.Paginas
{
    public partial class RegistrarModulo : System.Web.UI.Page
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
                Modulo_Listar();
            }
        }

        private void Modulo_Listar()
        {
            List<Modulo> lista_Modulos = new List<Modulo>();
            string error = "";
            lista_Modulos = CCModulo.Modulo_Listar(error);
            gdvListaModulos.DataSource = lista_Modulos;
            gdvListaModulos.DataBind();
        }

        /*-----------------------------------------------------------------*/
        protected void btnAgregar_Click(object sender, EventArgs e)
        {
            if (txtDescripcionModulo.Text.Trim() != "")
            {
                int existente = 0;
                Modulo oModulo = new Modulo();
                oModulo.nombreModulo = txtDescripcionModulo.Text.Trim();
                string error = "";

                existente = CCModulo.Modulo_Agregar(oModulo, error).IdModulo;
                Modulo_Listar();
                LimpiarEntradas();
                if (existente == 1)
                {
                    lblAlert.Text = "Modulo existente.";
                    lblAlert.CssClass = "alertDanger";
                }
                else
                {
                    lblAlert.Text = "Módulo agregado con éxito.";
                    lblAlert.CssClass = "alertSuccess";
                }
            }
        }
        /*-----------------------------------------------------------------*/

        /*-----------------------------------------------------------------*/
        protected void btnModificar_Click(object sender, EventArgs e)
        {
            lblAlert.Text = "";
            if (txtDescripcionModulo.Text.Trim() != "")
            {
                Modulo oModulo = new Modulo();
                String error = "";

                oModulo.IdModulo = int.Parse(txtIdModificar.Text.Trim());
                oModulo.nombreModulo = txtDescripcionModulo.Text.Trim();
                error = CCModulo.Modulo_Modificar(oModulo, error);

                Modulo_Listar();
                LimpiarEntradas();
                HabilitarBotones(true, false);

                if (error == "")
                {
                    lblAlert.Text = "Módulo modificado con éxito.";
                    lblAlert.CssClass = "alertSuccess";
                }
                else
                {
                    lblAlert.Text = error;
                    lblAlert.CssClass = "alertDanger";
                }
            }
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

        protected void lnkEditar_Click(object sender, EventArgs e)
        {

        }

        /*-----------------------------------------------------------------*/
        protected void lnkEliminar_Click(object sender, EventArgs e)
        {
            Modulo oModulo = new Modulo();
            string error = "";
            LinkButton lnk = (LinkButton)sender;
            String codigo = lnk.CommandArgument;
            oModulo.IdModulo = int.Parse(codigo);

            error = CCModulo.Modulo_Eliminar(oModulo, error);

            Modulo_Listar();
            LimpiarEntradas();

            HabilitarBotones(true, false);

            if (error == "")
            {
                lblAlert.Text = "Módulo eliminado con éxito.";
                lblAlert.CssClass = "alertSuccess";
            }
            else
            {
                lblAlert.Text = error;
                lblAlert.CssClass = "alertDanger";
            }
        }
        /*-----------------------------------------------------------------*/

        public void LimpiarEntradas()
        {
            txtDescripcionModulo.Text = "";
            txtIdModificar.Text = "";
        }

        public void HabilitarBotones(Boolean estado_1, Boolean estado_2)
        {
            btnAgregar.Enabled = estado_1;
            btnModificar.Enabled = estado_2;
        }
    }
}