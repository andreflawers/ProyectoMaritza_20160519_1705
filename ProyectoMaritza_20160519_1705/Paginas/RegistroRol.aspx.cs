using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using Controladora;
using Entidad;
using System.Collections;
using System.Web.Services;

namespace ProyectoMaritza_20160519_1705.Paginas
{
    public partial class RegistroRol : System.Web.UI.Page
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
                Rol_Listar();
                Interfaz_Listar();
                LimpiarEntradas();
            }
        }

        private void Rol_Listar()
        {
            List<Rol> lista_Rol = new List<Rol>();
            string error = "";
            lista_Rol = CCRol.Rol_Listar(error);
            gdvListaRol.DataSource = lista_Rol;
            gdvListaRol.DataBind();
        }

        private void Interfaz_Listar()
        {
            List<Interfaz> lista_Interfaz = new List<Interfaz>();
            string error = "";
            lista_Interfaz = CCInterfaz.Interfaz_ListarActivos(error);
            gdvListarPoppupInterfaces.DataSource = lista_Interfaz;
            gdvListarPoppupInterfaces.DataBind();
        }

        /*-----------------------------------------------------------------*/
        protected void btnAgregar_Click(object sender, EventArgs e)
        {
            bool check = false;
            foreach (GridViewRow grd_Row in this.gdvListarPoppupInterfaces.Rows)
            {
                CheckBox chk_Publicar = grd_Row.FindControl("chkSeleccionar") as CheckBox;
                if (chk_Publicar.Checked)
                {
                    check = true;
                }
            }

            if (txtDescripcionRol.Text.Trim() != "" && check == true)
            {
                int existente = 0;
                Rol oRol = new Rol();
                oRol.nombreRol = txtDescripcionRol.Text.Trim();

                List<Interfaz> lista_Interfaz = new List<Interfaz>();
                string error = "";
                lista_Interfaz = CCInterfaz.Interfaz_Listar(error);

                foreach (GridViewRow grd_Row in this.gdvListarPoppupInterfaces.Rows)
                {
                    CheckBox chk_Publicar = grd_Row.FindControl("chkSeleccionar") as CheckBox;
                    if (chk_Publicar.Checked)
                    {
                        oRol.IdInterfaz.Add(((DataControlFieldCell)grd_Row.Controls[0]).Text.Trim());
                    }
                }

                string errorRol = "";

                existente = CCRol.Rol_Agregar(oRol, lista_Interfaz, errorRol).IdRol;
                Rol_Listar();
                LimpiarEntradas();
                if (existente != 0)
                {
                    lblAlert.Text = "Rol agregado con éxito.";
                    lblAlert.CssClass = "alertSuccess";
                }
                else
                {
                    lblAlert.Text = "Rol existente.";
                    lblAlert.CssClass = "alertDanger";
                }
            }
            LimpiarEntradas();
        }
        /*-----------------------------------------------------------------*/

        protected void lnkEditar_Click(object sender, EventArgs e)
        {

        }

        [WebMethod]
        public static List<Interfaz> ObtenerInterfacesRol(string IdRolModificar)
        {
            Rol oRol = new Rol();
            String error = "";

            oRol.IdRol = int.Parse(IdRolModificar);
            return CCRol.Interfaz_Rol_ListarActivos(oRol, error);
        }

        public void LimpiarEntradas()
        {
            txtDescripcionRol.Text = "";
            txtIdModificar.Text = "";

            foreach (GridViewRow grd_Row in this.gdvListarPoppupInterfaces.Rows)
            {
                CheckBox chk_Publicar = grd_Row.FindControl("chkSeleccionar") as CheckBox;
                chk_Publicar.Checked = false;
            }
        }

        /*-----------------------------------------------------------------*/
        protected void btnModificar_Click(object sender, EventArgs e)
        {
            lblAlert.Text = "";
            bool check = false;
            foreach (GridViewRow grd_Row in this.gdvListarPoppupInterfaces.Rows)
            {
                CheckBox chk_Publicar = grd_Row.FindControl("chkSeleccionar") as CheckBox;
                if (chk_Publicar.Checked)
                {
                    check = true;
                }
            }

            if (txtDescripcionRol.Text.Trim() != "" && check == true && txtIdModificar.Text.Trim() != "")
            {
                Rol oRol = new Rol();
                oRol.nombreRol = txtDescripcionRol.Text.Trim();
                oRol.IdRol = int.Parse(txtIdModificar.Text.Trim());

                foreach (GridViewRow grd_Row in this.gdvListarPoppupInterfaces.Rows)
                {
                    CheckBox chk_Publicar = grd_Row.FindControl("chkSeleccionar") as CheckBox;
                    if (chk_Publicar.Checked)
                    {
                        oRol.IdInterfaz.Add(((DataControlFieldCell)grd_Row.Controls[0]).Text.Trim());
                    }
                }

                string error = "";

                error = CCRol.Rol_Modificar(oRol, error);

                Rol_Listar();
                Interfaz_Listar();
                LimpiarEntradas();

                HabilitarBotones(true, false);
                if (error == "")
                {
                    lblAlert.Text = "Rol modificado con éxito.";
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

        public void HabilitarBotones(Boolean estado_1, Boolean estado_2)
        {
            btnAgregar.Enabled = estado_1;
            btnModificar.Enabled = estado_2;
        }

        /*-----------------------------------------------------------------*/
        protected void lnkEliminar_Click(object sender, EventArgs e)
        {
            Rol oRol = new Rol();
            string error = "";
            LinkButton lnk = (LinkButton)sender;
            String codigo = lnk.CommandArgument;
            oRol.IdRol = int.Parse(codigo);

            error = CCRol.Rol_Eliminar(oRol, error);

            Rol_Listar();
            LimpiarEntradas();

            HabilitarBotones(true, false);

            if (error == "")
            {
                lblAlert.Text = "Rol eliminado con éxito.";
                lblAlert.CssClass = "alertSuccess";
            }
            else
            {
                lblAlert.Text = error;
                lblAlert.CssClass = "alertDanger";
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
    }
}