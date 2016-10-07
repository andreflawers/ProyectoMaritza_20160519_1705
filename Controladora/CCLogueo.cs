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
    public class CCLogueo
    {
        public static Logueo Logueo_Validar(Logueo oLogueo, string error)
        {
            Logueo lista_Logueos = new Logueo();
            SqlConnection conn = null;
            try
            {
                conn = new AdministracionConexion().getConexion();
                lista_Logueos = CDLogueo.Logueo_Validar(conn, oLogueo);
                error = "";
            }
            catch (Exception ex)
            {
                error = "Se econtro un error al Validar : " + ex.Message;
            }
            finally
            {
                AdministracionConexion.CloseConexion(conn);
            }
            return lista_Logueos;
        }

/*-----------------------------------------------------------------*/
        public static string Password_Modificar(Logueo obj_Logueo, string password_old, String error)
        {
            SqlConnection conn = null;
            try
            {
                conn = new AdministracionConexion().getConexion();

                Boolean validez = CDLogueo.Password_Actualizar(conn, obj_Logueo, password_old);
                if (validez == false) return error = "Contraseña Antigua incorrecta.";
                else return error = "";
            }
            catch (Exception ex)
            {
                return error = "Se econtro un error al Modificar Password : " + ex.Message;
            }
            finally
            {
                AdministracionConexion.CloseConexion(conn);
            }
        }
/*-----------------------------------------------------------------*/

        public static List<MenuSistema> Menu_Listar(Logueo oLogueo, string error)
        {
            List<MenuSistema> lista_Menu = new List<MenuSistema>();
            SqlConnection conn = null;
            try
            {
                conn = new AdministracionConexion().getConexion();
                lista_Menu = CDLogueo.MenuSistema_Listar(oLogueo, conn);
                error = "";
            }
            catch (Exception ex)
            {
                error = "Se econtro un error en el Listar el Menu : " + ex.Message;
            }
            finally
            {
                AdministracionConexion.CloseConexion(conn);
            }
            return lista_Menu;
        }

        public static List<string> Validar_Pagina_Acceso(Logueo oLogueo)
        {
            List<string> lista_interfaces = new List<string>();
            SqlConnection conn = null;
            try
            {
                conn = new AdministracionConexion().getConexion();
                return lista_interfaces = CDLogueo.Validar_Pagina_Acceso(oLogueo, conn);
            }
            catch (Exception ex)
            {
                lista_interfaces.Add("Se econtro un error en el Listar el Menu : " + ex.Message);
                return lista_interfaces;
            }
            finally
            {
                AdministracionConexion.CloseConexion(conn);
            }
        }
    }
}
