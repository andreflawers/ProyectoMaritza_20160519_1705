using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Routing;
using System.Web.Security;
using System.Web.SessionState;

namespace ProyectoMaritza_20160519_1705
{
    public class Global : System.Web.HttpApplication
    {
        void rutasAmigables()
        {
            RouteTable.Routes.MapPageRoute("Usuario", "Paginas/gestionador-de-usuario.aspx", "~/Paginas/RegistrarUsuario.aspx");
            RouteTable.Routes.MapPageRoute("Modulo", "Paginas/gestionador-de-modulo.aspx", "~/Paginas/RegistrarModulo.aspx");
            RouteTable.Routes.MapPageRoute("Interfaz", "Paginas/gestionador-de-interfaz.aspx", "~/Paginas/RegistroInterfaz.aspx");
            RouteTable.Routes.MapPageRoute("Rol", "Paginas/gestionador-de-rol.aspx", "~/Paginas/RegistroRol.aspx");
            RouteTable.Routes.MapPageRoute("Area", "Paginas/gestionador-de-area.aspx", "~/Paginas/WfArea.aspx");
            RouteTable.Routes.MapPageRoute("Distrito", "Paginas/gestionador-de-distrito.aspx", "~/Paginas/WfDistrito.aspx");
            RouteTable.Routes.MapPageRoute("Empresa", "Paginas/gestionador-de-empresa.aspx", "~/Paginas/WfEmpresa.aspx");
            RouteTable.Routes.MapPageRoute("Sucursal", "Paginas/gestionador-de-sucursal.aspx", "~/Paginas/WfSucursal.aspx");
            RouteTable.Routes.MapPageRoute("Provincia", "Paginas/gestionador-de-provincia.aspx", "~/Paginas/WfProvincia.aspx");
            RouteTable.Routes.MapPageRoute("Departamento", "Paginas/gestionador-de-departamento.aspx", "~/Paginas/WfDepartamento.aspx");
            RouteTable.Routes.MapPageRoute("NivelInstruccion", "Paginas/gestionador-de-nivel-de-instruccion.aspx", "~/Paginas/WfNivelInstruccion.aspx");
            RouteTable.Routes.MapPageRoute("TipoDocumento", "Paginas/gestionador-de-tipo-de-documento.aspx", "~/Paginas/WfTipoDocumento.aspx");
            RouteTable.Routes.MapPageRoute("Cargo", "Paginas/gestionador-de-cargo.aspx", "~/Paginas/WfCargo.aspx");
            RouteTable.Routes.MapPageRoute("TipoMovimiento", "Paginas/gestionador-de-tipo-de-movimiento.aspx", "~/Paginas/WfTipoMovimiento.aspx");
            RouteTable.Routes.MapPageRoute("AsistenciaManual", "Paginas/gestionador-de-asistencia-manual.aspx", "~/Paginas/AsistenciaManual.aspx");
            RouteTable.Routes.MapPageRoute("ControlAsistencia", "Paginas/gestionador-de-control-de-asistencia.aspx", "~/Paginas/ControlAsistencia.aspx");
            RouteTable.Routes.MapPageRoute("TurnoyRotaciondeHorario", "Paginas/gestionador-de-turno-y-rotacion-de-horario.aspx", "~/Paginas/TurnoyRotaciondeHorario.aspx");
            RouteTable.Routes.MapPageRoute("Vacaciones", "Paginas/gestionador-de-vacaciones.aspx", "~/Paginas/WfVacaciones.aspx");
            RouteTable.Routes.MapPageRoute("Movimientos", "Paginas/gestionador-de-movimientos.aspx", "~/Paginas/WfMovimientos.aspx");
            RouteTable.Routes.MapPageRoute("Boleta", "Paginas/gestionador-de-boleta.aspx", "~/Paginas/WfBoleta.aspx");
            RouteTable.Routes.MapPageRoute("Remuneracion", "Paginas/gestionador-de-remuneracion.aspx", "~/Paginas/WfRemuneracion.aspx");
            RouteTable.Routes.MapPageRoute("Permiso", "Paginas/gestionador-de-permiso.aspx", "~/Paginas/WfPermiso.aspx");
            RouteTable.Routes.MapPageRoute("Empleados", "Paginas/gestionador-de-empleados.aspx", "~/Paginas/WfEmpleados.aspx");
            RouteTable.Routes.MapPageRoute("Descuento", "Paginas/gestionador-de-descuento.aspx", "~/Paginas/WfDescuento.aspx");
            RouteTable.Routes.MapPageRoute("DescuentoPlanilla", "Paginas/gestionador-de-descuento-de-planilla.aspx", "~/Paginas/WfDescuentoPlanilla.aspx");
            RouteTable.Routes.MapPageRoute("Feriados", "Paginas/gestionador-de-feriados.aspx", "~/Paginas/WfFeriados.aspx");
            RouteTable.Routes.MapPageRoute("TipoPermiso", "Paginas/gestionador-de-tipo-de-permiso.aspx", "~/Paginas/WfTipoPermiso.aspx");
        }

        protected void Application_Start(object sender, EventArgs e)
        {
            rutasAmigables();
        }

        protected void Session_Start(object sender, EventArgs e)
        {

        }

        protected void Application_BeginRequest(object sender, EventArgs e)
        {

        }

        protected void Application_AuthenticateRequest(object sender, EventArgs e)
        {

        }

        protected void Application_Error(object sender, EventArgs e)
        {

        }

        protected void Session_End(object sender, EventArgs e)
        {

        }

        protected void Application_End(object sender, EventArgs e)
        {

        }
    }
}