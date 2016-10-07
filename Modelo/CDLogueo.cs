using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Entidad;
using System.Data.SqlClient;
using System.Data;
namespace Modelo
{
    public class CDLogueo
    {
        public static Logueo Logueo_Validar(SqlConnection conn, Logueo obj_Logueo)
        {
            try
            {
                using (SqlCommand cmd = new SqlCommand("sp_bus_ConfirmarAccesoUsuario", conn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add("@IdUsuario", SqlDbType.Int).Direction = ParameterDirection.Output;
                    cmd.Parameters.Add("@nombreUsuario", SqlDbType.VarChar, 40).Value = obj_Logueo.usuario;
                    cmd.Parameters.Add("@passwordUsuario", SqlDbType.Char, 10).Value = obj_Logueo.password;
                    cmd.ExecuteNonQuery();
                    obj_Logueo.IdUsuario = int.Parse(cmd.Parameters["@IdUsuario"].Value.ToString());
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return obj_Logueo;
        }

/*-----------------------------------------------------------------*/
        public static bool Password_Actualizar(SqlConnection conn, Logueo obl_Logueo, string password_old)
        {
            try
            {
                int permiso = 0;

                using (SqlCommand cmd = new SqlCommand("sp_upd_Usuario_actualizarPassword", conn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add("@permiso", SqlDbType.Char, 1).Direction = ParameterDirection.Output;
                    cmd.Parameters.Add("@IdUsuario", SqlDbType.Int).Value = obl_Logueo.IdUsuario;
                    cmd.Parameters.Add("@passwordUsuario", SqlDbType.Char, 10).Value = obl_Logueo.password;
                    cmd.Parameters.Add("@password_old", SqlDbType.Char, 10).Value = password_old;
                    cmd.ExecuteNonQuery();
                    permiso = int.Parse(cmd.Parameters["@permiso"].Value.ToString());
                }

                if (permiso == 1) return true;
                else return false;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
/*-----------------------------------------------------------------*/

        public static List<MenuSistema> MenuSistema_Listar(Logueo oLogueo, SqlConnection conn)
        {
            List<MenuSistema> lista_MenuSistema = new List<MenuSistema>();
            try
            {
                using (SqlCommand cmd = new SqlCommand("sp_lis_Usuario_listarMenu", conn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add("@IdUsuario", SqlDbType.Int).Value = oLogueo.IdUsuario;
                    cmd.ExecuteNonQuery();
                    SqlDataReader dr_result = cmd.ExecuteReader();
                    var num = 0;

                    if (dr_result.HasRows)
                    {
                        while (dr_result.Read())
                        {
                            num = num + 1;
                            MenuSistema obj_MenuSistema = new MenuSistema(num,
                                int.Parse(dr_result["IdModulo"].ToString()),
                                dr_result["nombreModulo"].ToString(),
                                int.Parse(dr_result["IdInterfaz"].ToString()),
                                dr_result["nombreInterfaz"].ToString(),
                                dr_result["estadoInterfazRol"].ToString()
                                );

                            lista_MenuSistema.Add(obj_MenuSistema);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return lista_MenuSistema;
        }

        public static List<string> Validar_Pagina_Acceso(Logueo oLogueo, SqlConnection conn)
        {
            List<string> lista_interfaces = new List<string>();
            try
            {
                string nombre_pagina = "";
                using (SqlCommand cmd = new SqlCommand("sp_bus_Interfaz_nombrePagina", conn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add("@IdUsuario", SqlDbType.Int).Value = oLogueo.IdUsuario;
                    cmd.ExecuteNonQuery();
                    SqlDataReader dr_result = cmd.ExecuteReader();

                    if (dr_result.HasRows)
                    {
                        while (dr_result.Read())
                        {
                            nombre_pagina = dr_result["nombreInterfaz"].ToString();

                            lista_interfaces.Add(nombre_pagina);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return lista_interfaces;
        }
    }
}
