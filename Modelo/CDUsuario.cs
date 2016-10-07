using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Entidad;
using System.Data;
using System.Data.SqlClient;

namespace Modelo
{
    public class CDUsuario
    {
        public static List<Usuario> Usuario_Listar(SqlConnection conn)
        {
            List<Usuario> lista_Usuario = new List<Usuario>();
            try
            {
                using (SqlCommand cmd = new SqlCommand("sp_lis_Usuario_listar", conn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    SqlDataReader dr_result = cmd.ExecuteReader();
                    var num = 0;

                    if (dr_result.HasRows)
                    {
                        while (dr_result.Read())
                        {
                            var estadoUsuario = "Inactivo";
                            num = num + 1;
                            if (dr_result["estadoUsuario"].ToString() == "1") estadoUsuario = "Activo";
                            else if (dr_result["estadoUsuario"].ToString() == "0") estadoUsuario = "Inactivo";
                            Usuario obj_Usuario = new Usuario(num,
                                int.Parse(dr_result["IdUsuario"].ToString()),
                                dr_result["nombreUsuario"].ToString(),
                                "",
                                estadoUsuario,
                                0,
                                int.Parse(dr_result["IdRol"].ToString()),
                                dr_result["nombreRol"].ToString(),
                                0,
                                int.Parse(dr_result["IdPersonal"].ToString()),
                                dr_result["nombrePersonal"].ToString(),
                                dr_result["apellidoPaternoPersonal"].ToString(),
                                dr_result["apellidoMaternoPersonal"].ToString(),
                                dr_result["nombreTipoDocumento"].ToString(),
                                dr_result["numeroDocumentoPersonal"].ToString());

                            lista_Usuario.Add(obj_Usuario);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return lista_Usuario;
        }

        public static Usuario Usuario_insertar(SqlConnection conn, Usuario obj_Usuario)
        {
            try
            {
                using (SqlCommand cmd = new SqlCommand("sp_ins_Usuario_registrar", conn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add("@IdUsuario", SqlDbType.Int).Direction = ParameterDirection.Output;
                    cmd.Parameters.Add("@nombreUsuario", SqlDbType.VarChar, 40).Value = obj_Usuario.nombreUsuario;
                    cmd.Parameters.Add("@passwordUsuario", SqlDbType.Char, 10).Value = obj_Usuario.passwordUsuario;
                    cmd.Parameters.Add("@IdPersonal", SqlDbType.Int).Value = obj_Usuario.IdPersonal;
                    cmd.Parameters.Add("@IdRol", SqlDbType.Int).Value = obj_Usuario.IdRol;
                    cmd.ExecuteNonQuery();

                    obj_Usuario.IdUsuario = int.Parse(cmd.Parameters["@IdUsuario"].Value.ToString());
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return obj_Usuario;
        }

        public static Boolean Usuario_Eliminar(SqlConnection conn, Usuario obj_Usuario)
        {
            try
            {
                using (SqlCommand cmd = new SqlCommand("sp_del_Usuario_eliminar", conn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add("@IdUsuario", SqlDbType.Int).Value = obj_Usuario.IdUsuario;
                    cmd.ExecuteNonQuery();
                }
                return true;
            }
            catch (Exception ex)
            {
                //throw ex;
                return false;
            }
        }

/*-----------------------------------------------------------------*/
        public static bool Usuario_Actualizar(SqlConnection conn, Usuario obj_Usuario)
        {
            try
            {
                int existente = 1;
                using (SqlCommand cmd = new SqlCommand("sp_upd_Usuario_actualizar", conn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add("@existente", SqlDbType.Char, 1).Direction = ParameterDirection.Output;
                    cmd.Parameters.Add("@IdUsuario", SqlDbType.Int).Value = obj_Usuario.IdUsuario;
                    cmd.Parameters.Add("@nombreUsuario", SqlDbType.VarChar, 40).Value = obj_Usuario.nombreUsuario;
                    cmd.Parameters.Add("@passwordUsuario", SqlDbType.Char, 10).Value = obj_Usuario.passwordUsuario;
                    cmd.Parameters.Add("@IdPersonal", SqlDbType.Int).Value = obj_Usuario.IdPersonal;
                    cmd.Parameters.Add("@IdRol", SqlDbType.Int).Value = obj_Usuario.IdRol;
                    cmd.ExecuteNonQuery();

                    existente = int.Parse(cmd.Parameters["@existente"].Value.ToString());
                }

                if (existente == 0) return true;
                else return false;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
/*-----------------------------------------------------------------*/


        public static void Usuario_ActualizarEstado(SqlConnection conn, Usuario obj_Usuario)
        {
            try
            {
                using (SqlCommand cmd = new SqlCommand("sp_upd_Usuario_actualizarEstado", conn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add("@IdUsuario", SqlDbType.Int).Value = obj_Usuario.IdUsuario;
                    cmd.Parameters.Add("@estadoUsuario", SqlDbType.Char, 1).Value = obj_Usuario.estadoUsuario;
                    cmd.ExecuteNonQuery();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public static List<Usuario> Usuario_Personal_Listar(SqlConnection conn)
        {
            List<Usuario> lista_Personal = new List<Usuario>();
            try
            {
                using (SqlCommand cmd = new SqlCommand("sp_lis_Usuario_listarPersonal", conn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    SqlDataReader dr_result = cmd.ExecuteReader();
                    var num = 0;

                    if (dr_result.HasRows)
                    {
                        while (dr_result.Read())
                        {
                            num = num + 1;
                            Usuario obj_Usuario = new Usuario(0,
                                0,
                                "",
                                "",
                                "",
                                0,
                                0,
                                "",
                                num,
                                int.Parse(dr_result["IdPersonal"].ToString()),
                                dr_result["nombrePersonal"].ToString(),
                                dr_result["apellidoPaternoPersonal"].ToString(),
                                dr_result["apellidoMaternoPersonal"].ToString(),
                                dr_result["nombreTipoDocumento"].ToString(),
                                dr_result["numeroDocumentoPersonal"].ToString());

                            lista_Personal.Add(obj_Usuario);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return lista_Personal;
        }

        public static List<Usuario> Usuario_ListarDatos(Usuario oUsuario, SqlConnection conn)
        {
            List<Usuario> lista_Usuario = new List<Usuario>();
            try
            {
                using (SqlCommand cmd = new SqlCommand("sp_lis_Usuario_listarDatos", conn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add("@IdUsuario", SqlDbType.Int).Value = oUsuario.IdUsuario;
                    cmd.ExecuteNonQuery();
                    SqlDataReader dr_result = cmd.ExecuteReader();
                    var num = 0;

                    if (dr_result.HasRows)
                    {
                        while (dr_result.Read())
                        {
                            num = num + 1;
                            Usuario obj_Usuario = new Usuario(num,
                                int.Parse(dr_result["IdUsuario"].ToString()),
                                dr_result["nombreUsuario"].ToString(),
                                "",
                                "",
                                0,
                                int.Parse(dr_result["IdRol"].ToString()),
                                dr_result["nombreRol"].ToString(),
                                0,
                                int.Parse(dr_result["IdPersonal"].ToString()),
                                dr_result["nombrePersonal"].ToString(),
                                dr_result["apellidoPaternoPersonal"].ToString(),
                                dr_result["apellidoMaternoPersonal"].ToString(),
                                dr_result["nombreTipoDocumento"].ToString(),
                                dr_result["numeroDocumentoPersonal"].ToString());

                            lista_Usuario.Add(obj_Usuario);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return lista_Usuario;
        }
    }
}
