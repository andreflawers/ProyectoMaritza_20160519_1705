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
    public class CCUsuario
    {
        public static List<Usuario> Usuario_Listar(string error)
        {
            List<Usuario> lista_usuarios = new List<Usuario>();
            SqlConnection conn = null;
            try
            {
                conn = new AdministracionConexion().getConexion();
                lista_usuarios = CDUsuario.Usuario_Listar(conn);
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
            return lista_usuarios;
        }

        public static Usuario Usuario_Agregar(Usuario obj_Usuario, string error)
        {
            SqlConnection conn = null;
            try
            {
                conn = new AdministracionConexion().getConexion();

                CDUsuario.Usuario_insertar(conn, obj_Usuario);
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
            return obj_Usuario;
        }

        public static String Usuario_Eliminar(Usuario obj_Usuario, String error)
        {
            SqlConnection conn = null;
            try
            {
                conn = new AdministracionConexion().getConexion();

                Boolean validez = CDUsuario.Usuario_Eliminar(conn, obj_Usuario);

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
        public static string Usuario_Modificar(Usuario obj_Usuario, String error)
        {
            SqlConnection conn = null;
            try
            {
                bool permiso = false;
                conn = new AdministracionConexion().getConexion();

                permiso = CDUsuario.Usuario_Actualizar(conn, obj_Usuario);

                if (permiso == true) return error = "";
                else return error = "Usuario modificado existente.";
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

        public static void Usuario_ModificarEstado(Usuario obj_Usuario, String error)
        {
            SqlConnection conn = null;
            try
            {
                conn = new AdministracionConexion().getConexion();

                CDUsuario.Usuario_ActualizarEstado(conn, obj_Usuario);

                error = "";
            }
            catch (Exception ex)
            {
                error = "Se econtro un error al Modificar el Estado : " + ex.Message;
            }
            finally
            {
                AdministracionConexion.CloseConexion(conn);
            }
        }

        public static List<Usuario> Usuario_ListarDatos(Usuario oUsuario, string error)
        {
            List<Usuario> lista_Usuario = new List<Usuario>();
            SqlConnection conn = null;
            try
            {
                conn = new AdministracionConexion().getConexion();
                lista_Usuario = CDUsuario.Usuario_ListarDatos(oUsuario, conn);
                error = "";
            }
            catch (Exception ex)
            {
                error = "Se encontro un error en el Listar Datos: " + ex.Message;
            }
            finally
            {
                AdministracionConexion.CloseConexion(conn);
            }
            return lista_Usuario;
        }

        public static List<Usuario> Usuario_Personal_Listar(string error)
        {
            List<Usuario> lista_Personal = new List<Usuario>();
            SqlConnection conn = null;
            try
            {
                conn = new AdministracionConexion().getConexion();
                lista_Personal = CDUsuario.Usuario_Personal_Listar(conn);
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
            return lista_Personal;
        }
    }
}
