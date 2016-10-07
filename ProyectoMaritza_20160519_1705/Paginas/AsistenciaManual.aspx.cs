using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Entidad;
using Controladora;
using System.Data;

namespace ProyectoMaritza_20160519_1705.Paginas
{
    public partial class AsistenciaManual : System.Web.UI.Page
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
            lblMostarFecha.Text = DateTime.Today.ToShortDateString();
            LlenarGrilla();
        }
        private void LlenarGrilla()
        {
            CCAsistencia oCCAsistencia = new CCAsistencia();
            DataTable oDt = oCCAsistencia.Asistencia_Listar(DateTime.Parse(lblMostarFecha.Text));
            this.grdAsistencia.DataSource = oDt;
            this.grdAsistencia.DataBind();
        }

        protected void btnMarcar_Click(object sender, EventArgs e)
        {
            Result_transaccion obj_transac = new Result_transaccion();
            Asistencia obj_asistencia = new Asistencia();
            obj_asistencia.msg = lblMensaje.Text;
            obj_asistencia.dni = txtDNI.Text;
            obj_asistencia.diaAsistencia = DateTime.Now.Date;
            obj_asistencia.hora = DateTime.Now.ToLocalTime();
            CCAsistencia.Asistencia_Grabar(obj_asistencia, obj_transac);
            if (obj_transac.resultado == 1)
            {
                txtApellidosyNombres.Text = CCAsistencia.Asistencia_Consultar_datos(txtDNI.Text, obj_transac);
                lblMensaje.ForeColor = System.Drawing.Color.Red;
                lblMensaje.Text = obj_asistencia.msg;
                LlenarGrilla();
            }
            ClientScript.RegisterStartupScript(this.GetType(), "alert", "HideLabel();", true);

        }
    }
}