using Controladora;
using Entidad;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace ProyectoMaritza_20160519_1705.Paginas
{
    public partial class ControlAsistencia : System.Web.UI.Page
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

            if (!Page.IsPostBack) return;
        }
        private void LlenarGrillaPorFecha()
        {
            CCAsistencia oCCAsistencia = new CCAsistencia();
            string desde = inpDesde.Value;
            string hasta = inpHasta.Value;
            DataTable oDt = oCCAsistencia.Asistencia_Listar_PorFecha(desde, hasta);
            this.grdControl.DataSource = oDt;
            this.grdControl.DataBind();
        }
        private void LimpiarPantalla()
        {
            chkBuscarFecha.Checked = false;
            chkBuscarHorarioySucursal.Checked = false;
            chkBuscarPersonal.Checked = false;
            txtApellido.Text = "";
            txtCargo.Text = "";
            txtHorario.Text = "";
            txtSucursal.Text = "";
            inpDesde.Value = "";
            inpHasta.Value = "";
        }
        private void LlenarGrillaPorHorarioySucursal()
        {
            CCAsistencia oCCAsistencia = new CCAsistencia();
            string descripcion = txtHorario.Text;
            string nombre = txtSucursal.Text;
            DataTable oDt = oCCAsistencia.Asistencia_Listar_PorHorarioySucursal(descripcion, nombre);
            this.grdControl.DataSource = oDt;
            this.grdControl.DataBind();
        }
        private void LlenarGrillaPorPersonal()
        {
            CCAsistencia oCCAsistencia = new CCAsistencia();
            string apellidos = txtApellido.Text;
            string cargo = txtCargo.Text;
            DataTable oDt = oCCAsistencia.Asistencia_Listar_PorPersonal(apellidos, cargo);
            this.grdControl.DataSource = oDt;
            this.grdControl.DataBind();
        }
        private void LlenarGrillaPorFechayPersonal()
        {
            CCAsistencia oCCAsistencia = new CCAsistencia();
            string desde = inpDesde.Value;
            string hasta = inpHasta.Value;
            string apellidos = txtApellido.Text;
            string cargo = txtCargo.Text;
            DataTable oDt = oCCAsistencia.Asistencia_Listar_PorFechayPersonal(desde, hasta, apellidos, cargo);
            this.grdControl.DataSource = oDt;
            this.grdControl.DataBind();
        }
        private void LlenarGrillaPorFechayHorario()
        {
            CCAsistencia oCCAsistencia = new CCAsistencia();
            string desde = inpDesde.Value;
            string hasta = inpHasta.Value;
            string descripcion = txtHorario.Text;
            string nombre = txtSucursal.Text;
            DataTable oDt = oCCAsistencia.Asistencia_Listar_PorFechayHorario(desde, hasta, descripcion, nombre);
            this.grdControl.DataSource = oDt;
            this.grdControl.DataBind();
        }
        private void LlenarGrillaPorHorarioyPersonal()
        {
            CCAsistencia oCCAsistencia = new CCAsistencia();
            string descripcion = txtHorario.Text;
            string nombre = txtSucursal.Text;
            string apellidos = txtApellido.Text;
            string cargo = txtCargo.Text;
            DataTable oDt = oCCAsistencia.Asistencia_Listar_PorHorarioyPersonal(descripcion, nombre, apellidos, cargo);
            this.grdControl.DataSource = oDt;
            this.grdControl.DataBind();
        }
        private void LlenarGrillaPorFecha_HorarioyPersonal()
        {
            CCAsistencia oCCAsistencia = new CCAsistencia();
            string desde = inpDesde.Value;
            string hasta = inpHasta.Value;
            string descripcion = txtHorario.Text;
            string nombre = txtSucursal.Text;
            string apellidos = txtApellido.Text;
            string cargo = txtCargo.Text;
            DataTable oDt = oCCAsistencia.Asistencia_Listar_PorFecha_HorarioyPersonal(desde, hasta, descripcion, nombre, apellidos, cargo);
            this.grdControl.DataSource = oDt;
            this.grdControl.DataBind();
        }

        protected void btnBuscar_Click(object sender, EventArgs e)
        {
            if (chkBuscarFecha.Checked == true)
            {
                LlenarGrillaPorFecha();
            }
            if (chkBuscarHorarioySucursal.Checked == true)
            {
                LlenarGrillaPorHorarioySucursal();
            }
            if (chkBuscarPersonal.Checked == true)
            {
                LlenarGrillaPorPersonal();
            }
            if (chkBuscarFecha.Checked == true && chkBuscarPersonal.Checked == true)
            {
                LlenarGrillaPorFechayPersonal();
            }
            if (chkBuscarFecha.Checked == true && chkBuscarHorarioySucursal.Checked == true)
            {
                LlenarGrillaPorFechayHorario();
            }
            if (chkBuscarHorarioySucursal.Checked == true && chkBuscarPersonal.Checked == true)
            {
                LlenarGrillaPorHorarioyPersonal();
            }
            if (chkBuscarFecha.Checked == true && chkBuscarHorarioySucursal.Checked == true && chkBuscarPersonal.Checked == true)
            {
                LlenarGrillaPorFecha_HorarioyPersonal();
            }
            LimpiarPantalla();
        }
    }
}