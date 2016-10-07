using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Entidad;
using Modelo;
using System.Data.SqlClient;

namespace Controladora
{
    public class CCInterfaz
    {
        public static List<Interfaz> Interfaz_Listar(string error)
        {
            List<Interfaz> lista_interfaces = new List<Interfaz>();
            SqlConnection conn = null;
            try
            {
                conn = new AdministracionConexion().getConexion();
                lista_interfaces = CDInterfaz.Interfaz_Listar(conn);
                error = "";
            }
            catch (Exception ex)
            {
                error = "Se econtro un error en el Listar : " + ex.Message;
            }
            finally
            {
                AdministracionConexion.CloseConexion(conn);
            }
            return lista_interfaces;
        }

        public static Interfaz Interfaz_Agregar(Interfaz obj_Interfaz, List<Rol> oRol, string error)
        {
            SqlConnection conn = null;
            try
            {
                conn = new AdministracionConexion().getConexion();

                CDInterfaz.Interfaz_insertar(conn, obj_Interfaz, oRol);
                error = "";
            }
            catch (Exception ex)
            {
                error = "Se econtro un error en el Grabar : " + ex.Message;
            }
            finally
            {
                AdministracionConexion.CloseConexion(conn);
            }
            return obj_Interfaz;
        }

        public static String Interfaz_Eliminar(Interfaz obj_Interfaz, String error)
        {
            SqlConnection conn = null;
            try
            {
                conn = new AdministracionConexion().getConexion();

                Boolean validez = CDInterfaz.Interfaz_Eliminar(conn, obj_Interfaz);

                if (validez == false) error = "No se pudo Eliminar, existen Roles relacionadas.";
                else error = "";

                return error;
            }
            catch (Exception ex)
            {
                error = "Se econtro un error al Eliminar : " + ex.Message;
                return error;
            }
            finally
            {
                AdministracionConexion.CloseConexion(conn);
            }
        }

/*-----------------------------------------------------------------*/
        public static string Interfaz_Modificar(Interfaz obj_Interfaz, String error)
        {
            SqlConnection conn = null;
            try
            {
                bool permiso = false;
                conn = new AdministracionConexion().getConexion();

                permiso = CDInterfaz.Interfaz_Actualizar(conn, obj_Interfaz);

                if (permiso == false) return error = "Interfaz modificada existente.";
                else return error = "";
            }
            catch (Exception ex)
            {
                return error = "Se econtro un error al Modificar : " + ex.Message;
            }
            finally
            {
                AdministracionConexion.CloseConexion(conn);
            }
        }
/*-----------------------------------------------------------------*/

        public static int Interfaz_ModificarEstado(Interfaz obj_Interfaz, List<Rol> lista_Rol, String error)
        {
            SqlConnection conn = null;
            try
            {
                conn = new AdministracionConexion().getConexion();

                error = "";

                return CDInterfaz.Interfaz_ActualizarEstado(conn, obj_Interfaz, lista_Rol);
            }
            catch (Exception ex)
            {
                error = "Se econtro un error al Modificar el Estado : " + ex.Message;
                return 0;
            }
            finally
            {
                AdministracionConexion.CloseConexion(conn);
            }
        }

        public static List<Interfaz> Interfaz_ListarActivos(string error)
        {
            List<Interfaz> lista_interfaces = new List<Interfaz>();
            SqlConnection conn = null;
            try
            {
                conn = new AdministracionConexion().getConexion();
                lista_interfaces = CDInterfaz.Interfaz_ListarActivos(conn);
                error = "";
            }
            catch (Exception ex)
            {
                error = "Se econtro un error en el Listar Activos : " + ex.Message;
            }
            finally
            {
                AdministracionConexion.CloseConexion(conn);
            }
            return lista_interfaces;
        }
    }
}
