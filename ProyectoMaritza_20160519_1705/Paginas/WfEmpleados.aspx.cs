using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;
using System.Web.UI;
using System.Web.UI.WebControls;
using Entidad;
using Controladora;
using System.Data;

namespace ProyectoMaritza_20160519_1705.Paginas
{
    public partial class WfEmpleados : System.Web.UI.Page
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

            if (!IsPostBack)
            {
            }
        }
        protected void btn_nuevo_Click(object sender, EventArgs e)
        {

        }
        public List<TipoDocumento> listar_tipo_de_documento()
        {
            CCTipoDocumento objCCtipoDeDocumento = new CCTipoDocumento();
            return objCCtipoDeDocumento.listarTipoDocumento();
        }
        public List<Area> listar_area()
        {
            CCArea objCCArea = new CCArea();
            return objCCArea.listarArea();
        }
        public List<NivelInstruccion> listar_nivel_escolaridad()
        {
            CCNivelInstruccion objCCnivelEscolaridad = new CCNivelInstruccion();
            return objCCnivelEscolaridad.listarNivelInstruccion();
        }
        public List<Sucursal> listar_sucursal_empresa()
        {
            CCSucursal objCCsucursal = new CCSucursal();
            return objCCsucursal.listarSucursal();
        }
        public DataTable listar_personal()
        {
            CCPersonal objCCPersonal = new CCPersonal();
            return objCCPersonal.listar_personal();
        }
        public List<Departamento> listar_departamentos() 
        {
            CCDepartamento oCCDepartamento = new CCDepartamento();
            return oCCDepartamento.listarDepartamento();
        }
        [WebMethod]
        public static List<Provincia> listarProvincia(int departamento_id) 
        {
            CCProvincia oCCProvincia = new CCProvincia();
            return oCCProvincia.listar_provincia_por_departamento(departamento_id);
        }
        [WebMethod]
        public static List<Distrito> listarDistrito(int provincia_id)
        {
            CCDistrito oCCDistrito = new CCDistrito();
            return oCCDistrito.listar_distrito_por_provincia(provincia_id);
        }
        public List<Cargo> Listar_cargos()
        {
            CCCargo oCCcargo = new CCCargo();
            return oCCcargo.listarCargo();
        }

        [WebMethod]
        public static int inserta_personal(String ape_paterno, String ape_materno, String nombre, String fecha_nacimiento,
            String sexo_personal, String nivel_escolaridad, String telefono_personal, String estado_civil,
            String tipo_documento, String numero_documento, int distrito_personal, String direccion_personal,
            int sucursal_id, int personal_area, String fecha_ingreso_personal, String planilla,int id_cargo)
        {
            Personal objPersonal = new Personal();
            objPersonal.personal_apellido_paterno = ape_paterno;
            objPersonal.personal_apellido_materno = ape_materno;
            objPersonal.personal_nombre = nombre;
            objPersonal.personal_fecha_nacimiento = fecha_nacimiento;
            objPersonal.personal_sexo = sexo_personal;
            objPersonal.nivel_escolaridad_id = int.Parse(nivel_escolaridad);
            objPersonal.personal_telefono = telefono_personal;
            objPersonal.personal_estado_civil = estado_civil;
            objPersonal.tipo_documento_id = int.Parse(tipo_documento);
            objPersonal.numero_documento = numero_documento;
            objPersonal.personal_distrito_id = distrito_personal;
            objPersonal.personal_direccion = direccion_personal;
            objPersonal.sucursal_id = sucursal_id;
            objPersonal.area_id = personal_area;
            objPersonal.persona_fecha_de_ingreso = fecha_ingreso_personal;
            objPersonal.personal_planilla = planilla;
            objPersonal.cargo_id = id_cargo;
            CCPersonal objCCpersonal = new CCPersonal();
            return objCCpersonal.insertar_personal(objPersonal);
        }

        [WebMethod]
        public static List<Distrito> filtrarDistrito(int pSC)
        {
            CCDistrito objCCdsitrito = new CCDistrito();
            return objCCdsitrito.listarDistrito();
        }

        [WebMethod]
        public static int actualizarPersonal(int personal_id, String ape_paterno, String ape_materno, String nombre, String fecha_nacimiento,
             String sexo_personal, String nivel_escolaridad, String telefono_personal, String estado_civil,
             String tipo_documento, String numero_documento, int distrito_personal, String direccion_personal,
             int sucursal_id, int personal_area, String fecha_ingreso_personal, String planilla,int id_cargo)
        {
            Personal objPersonal = new Personal();
            objPersonal.personal_id = personal_id;
            objPersonal.personal_apellido_paterno = ape_paterno;
            objPersonal.personal_apellido_materno = ape_materno;
            objPersonal.personal_nombre = nombre;
            objPersonal.personal_fecha_nacimiento = fecha_nacimiento;
            objPersonal.personal_sexo = sexo_personal;
            objPersonal.nivel_escolaridad_id = int.Parse(nivel_escolaridad);
            objPersonal.personal_telefono = telefono_personal;
            objPersonal.personal_estado_civil = estado_civil;
            objPersonal.tipo_documento_id = int.Parse(tipo_documento);
            objPersonal.numero_documento = numero_documento;
            objPersonal.personal_distrito_id = distrito_personal;
            objPersonal.personal_direccion = direccion_personal;
            objPersonal.sucursal_id = sucursal_id;
            objPersonal.area_id = personal_area;
            objPersonal.persona_fecha_de_ingreso = fecha_ingreso_personal;
            objPersonal.personal_planilla = planilla;
            objPersonal.cargo_id = id_cargo;
            CCPersonal objCCpersonal = new CCPersonal();
            return objCCpersonal.actualizar_personal(objPersonal);
        }

        [WebMethod]
        public static bool eliminarPersonal(int personal_id)
        {
            CCPersonal objCCPersonal = new CCPersonal();
            return objCCPersonal.eliminar_personal(personal_id);
        }
    }
}