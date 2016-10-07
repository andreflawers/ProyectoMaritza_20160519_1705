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
using System.Data;

namespace ProyectoMaritza_20160519_1705.Paginas
{
    public partial class PersonalHorario : System.Web.UI.Page
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
        public DataTable listar_horarios() 
        {
            CCHorario oCCHorario = new CCHorario();
            return oCCHorario.listar_horario();
        }
       [WebMethod]
        public static List<Horario> filtrarDniPersonal(int pSC) 
       {
           CCHorario objCChoraio = new CCHorario();
           return objCChoraio.listar_personal_dni();
       }
       [WebMethod]
       public static List<Horario> busquedaPorDNi(int personal_dni)
       {
           CCHorario objCChoraio = new CCHorario();
           return objCChoraio.busqueda_por_dni(personal_dni);
       }
        
       [WebMethod]
       public static bool insertarHorarioPersonal(int personal_id, String horario_inicial,String horario_final,String fecha_inicial, String fecha_final, String descripcion)
       {
           CCHorario oCChorario = new CCHorario();
           return oCChorario.insertar_horario_personal(personal_id, horario_inicial, horario_final, fecha_inicial,fecha_final, descripcion);
       }
        
       [WebMethod]
       public static bool modificarHorarioPersonal(int horario_id, String horario_inicial, String horario_final,String fecha_inicial, String fecha_final, String descripcion) 
       {
           CCHorario oCCHorario = new CCHorario();
           return oCCHorario.modificar_horario_personal(horario_id, horario_inicial, horario_final, fecha_inicial,fecha_final, descripcion);
       }
        
       [WebMethod]
       public static bool eliminarHorario(int horario_id) 
       {
           CCHorario objCChorario = new CCHorario();
           return objCChorario.eliminar_horario(horario_id);
       }
    }
}