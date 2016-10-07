using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Entidad;
using Modelo;
using System.Data;
using System.Data.SqlClient;
namespace Controladora
{
    public class CCRol
    {
        public static List<Rol> Rol_Listar(string error)
        {
            List<Rol> lista_roles = new List<Rol>();
            SqlConnection conn = null;
            try
            {
                conn = new AdministracionConexion().getConexion();
                lista_roles = CDRol.Rol_Listar(conn);
                error = "";
            }
            catch (Exception ex)
            {
                error = "Se encontro un error en el Listar : " + ex.Message;
            }
            finally
            {
                AdministracionConexion.CloseConexion(conn);
            }
            return lista_roles;
        }

        public static Rol Rol_Agregar(Rol obj_Rol, List<Interfaz> obj_Interfaz, string error)
        {
            SqlConnection conn = null;
            try
            {
                conn = new AdministracionConexion().getConexion();

                CDRol.Rol_insertar(conn, obj_Rol, obj_Interfaz);
                error = "";
            }
            catch (Exception ex)
            {
                error = "Se encontro un error en el Grabar : " + ex.Message;
            }
            finally
            {
                AdministracionConexion.CloseConexion(conn);
            }
            return obj_Rol;
        }

        public static String Rol_Eliminar(Rol obj_Rol, String error)
        {
            SqlConnection conn = null;
            try
            {
                conn = new AdministracionConexion().getConexion();

                Boolean validez = CDRol.Rol_Eliminar(conn, obj_Rol);

                if (validez == false) error = "No se pudo Eliminar, existen Usuarios relacionadas.";
                else error = "";

                return error;
            }
            catch (Exception ex)
            {
                error = "Se encontro un error al Eliminar : " + ex.Message;
                return error;
            }
            finally
            {
                AdministracionConexion.CloseConexion(conn);
            }
        }

/*-----------------------------------------------------------------*/
        public static string Rol_Modificar(Rol obj_Rol, String error)
        {
            SqlConnection conn = null;
            try
            {
                bool permiso = false;
                conn = new AdministracionConexion().getConexion();

                permiso = CDRol.Rol_Actualizar(conn, obj_Rol);

                if (permiso == true) return error = "";
                else return error = "Rol modificado existente.";
            }
            catch (Exception ex)
            {
                return error = "Se encontro un error al Modificar : " + ex.Message;
            }
            finally
            {
                AdministracionConexion.CloseConexion(conn);
            }
        }
/*-----------------------------------------------------------------*/

        public static List<Interfaz> Interfaz_Rol_ListarActivos(Rol obj_Rol, string error)
        {
            List<Interfaz> lista_Interfaz = new List<Interfaz>();
            SqlConnection conn = null;
            try
            {
                conn = new AdministracionConexion().getConexion();
                lista_Interfaz = CDRol.Interfaz_Rol_ListarActivos(obj_Rol, conn);
                error = "";
            }
            catch (Exception ex)
            {
                error = "Se encontro un error en el Listar : " + ex.Message;
            }
            finally
            {
                AdministracionConexion.CloseConexion(conn);
            }
            return lista_Interfaz;
        }
    }
}
