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
    public class CCModulo
    {
        public static List<Modulo> Modulo_Listar(string error)
        {
            List<Modulo> lista_Modulos = new List<Modulo>();
            SqlConnection conn = null;
            try
            {
                conn = new AdministracionConexion().getConexion();
                lista_Modulos = CDModulo.Modulo_Listar(conn);
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
            return lista_Modulos;
        }

        public static Modulo Modulo_Agregar(Modulo obj_modulo, string error)
        {
            SqlConnection conn = null;
            try
            {
                conn = new AdministracionConexion().getConexion();

                CDModulo.Modulo_insertar(conn, obj_modulo);
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
            return obj_modulo;
        }

        public static String Modulo_Eliminar(Modulo obj_modulo, String error)
        {
            SqlConnection conn = null;
            try
            {
                conn = new AdministracionConexion().getConexion();

                Boolean validez = CDModulo.Modulo_Eliminar(conn, obj_modulo);

                if (validez == false) error = "No se pudo Eliminar, existen Interfaces relacionadas.";
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
        public static string Modulo_Modificar(Modulo obj_modulo, String error)
        {
            SqlConnection conn = null;
            try
            {
                conn = new AdministracionConexion().getConexion();
                bool existencia = false;
                existencia = CDModulo.Modulo_Actualizar(conn, obj_modulo);

                if (existencia == false) return error = "Módulo modificado existente.";
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
    }
}
