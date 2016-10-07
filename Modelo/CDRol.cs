using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Entidad;
using System.Data;
using System.Data.SqlClient;
using System.Collections;
namespace Modelo
{
    public class CDRol
    {
        public static List<Rol> Rol_Listar(SqlConnection conn)
        {
            List<Rol> lista_Rol = new List<Rol>();
            try
            {
                using (SqlCommand cmd = new SqlCommand("sp_lis_Rol_Listar", conn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    SqlDataReader dr_result = cmd.ExecuteReader();
                    var num = 0;
                    ArrayList arregloIdInterfaces = null;
                    ArrayList arregloNombresInterfaces = null;

                    if (dr_result.HasRows)
                    {
                        while (dr_result.Read())
                        {
                            num = num + 1;
                            Rol obj_Rol = new Rol(num,
                                int.Parse(dr_result["IdRol"].ToString()),
                                dr_result["nombreRol"].ToString(),
                                int.Parse(dr_result["cantidadInterfaces"].ToString()),
                                0,
                                arregloIdInterfaces,
                                arregloNombresInterfaces);

                            lista_Rol.Add(obj_Rol);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return lista_Rol;
        }

        public static Rol Rol_insertar(SqlConnection conn, Rol obj_Rol, List<Interfaz> obj_Interfaz)
        {
            try
            {
                using (SqlCommand cmd = new SqlCommand("sp_ins_Rol_registrar", conn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add("@IdRol", SqlDbType.Int).Direction = ParameterDirection.Output;
                    cmd.Parameters.Add("@nombreRol", SqlDbType.VarChar, 50).Value = obj_Rol.nombreRol;
                    cmd.ExecuteNonQuery();

                    obj_Rol.IdRol = int.Parse(cmd.Parameters["@IdRol"].Value.ToString());
                }

                if (obj_Rol.IdRol != 0)
                {
                    foreach (var item in obj_Interfaz)
                    {
                        using (SqlCommand cmd = new SqlCommand("sp_ins_Interfaz_Rol_registrar", conn))
                        {
                            cmd.CommandType = CommandType.StoredProcedure;
                            cmd.Parameters.Add("@IdRol", SqlDbType.Int).Value = obj_Rol.IdRol;
                            cmd.Parameters.Add("@IdInterfaz", SqlDbType.VarChar, 50).Value = item.IdInterfaz;
                            cmd.ExecuteNonQuery();
                        }
                    }

                    foreach (var item in obj_Rol.IdInterfaz)
                    {
                        using (SqlCommand cmd = new SqlCommand("sp_ins_Interfaz_Rol_registrarActivar", conn))
                        {
                            cmd.CommandType = CommandType.StoredProcedure;
                            cmd.Parameters.Add("@IdRol", SqlDbType.Int).Value = obj_Rol.IdRol;
                            cmd.Parameters.Add("@IdInterfaz", SqlDbType.VarChar, 50).Value = item;
                            cmd.ExecuteNonQuery();
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return obj_Rol;
        }

        public static Boolean Rol_Eliminar(SqlConnection conn, Rol obj_Rol)
        {
            try
            {
                int verificar = 0;
                using (SqlCommand cmd = new SqlCommand("sp_bus_Rol_eliminar_verificar", conn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add("@verificar", SqlDbType.Int).Direction = ParameterDirection.Output;
                    cmd.Parameters.Add("@IdRol", SqlDbType.Int).Value = obj_Rol.IdRol;
                    cmd.ExecuteNonQuery();

                    verificar = int.Parse(cmd.Parameters["@verificar"].Value.ToString());
                }
                if (verificar == 0) return false;

                using (SqlCommand cmd = new SqlCommand("sp_del_Rol_eliminarInterfaz_Rol", conn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add("@IdRol", SqlDbType.Int).Value = obj_Rol.IdRol;
                    cmd.ExecuteNonQuery();
                }

                using (SqlCommand cmd = new SqlCommand("sp_del_Rol_eliminar", conn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add("@IdRol", SqlDbType.Int).Value = obj_Rol.IdRol;
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
        public static bool Rol_Actualizar(SqlConnection conn, Rol obj_Rol)
        {
            try
            {
                int existente = 1;
                using (SqlCommand cmd = new SqlCommand("sp_upd_Rol_actualizarRol", conn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add("@existente", SqlDbType.Char, 1).Direction = ParameterDirection.Output;
                    cmd.Parameters.Add("@IdRol", SqlDbType.Int).Value = obj_Rol.IdRol;
                    cmd.Parameters.Add("@nombreRol", SqlDbType.VarChar, 50).Value = obj_Rol.nombreRol;
                    cmd.ExecuteNonQuery();

                    existente = int.Parse(cmd.Parameters["@existente"].Value.ToString());
                }
                if (existente == 1) return false;

                using (SqlCommand cmd = new SqlCommand("sp_upd_Rol_actualizarInterfaz_RolReset", conn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add("@IdRol", SqlDbType.Int).Value = obj_Rol.IdRol;
                    cmd.ExecuteNonQuery();
                }

                foreach (var item in obj_Rol.IdInterfaz)
                {
                    using (SqlCommand cmd = new SqlCommand("sp_upd_Rol_actualizarInterfaz_Rol", conn))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.Add("@IdRol", SqlDbType.Int).Value = obj_Rol.IdRol;
                        cmd.Parameters.Add("@IdInterfaz", SqlDbType.Int).Value = item;
                        cmd.ExecuteNonQuery();
                    }
                }
                return true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
/*-----------------------------------------------------------------*/

        public static List<Interfaz> Interfaz_Rol_ListarActivos(Rol obj_Rol, SqlConnection conn)
        {
            List<Interfaz> lista_Interfaz = new List<Interfaz>();
            try
            {
                using (SqlCommand cmd = new SqlCommand("sp_del_Interfaz_Rol_ListarActivos", conn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add("@IdRol", SqlDbType.Int).Value = obj_Rol.IdRol;
                    cmd.ExecuteNonQuery();
                    SqlDataReader dr_result = cmd.ExecuteReader();
                    var num = 0;

                    if (dr_result.HasRows)
                    {
                        while (dr_result.Read())
                        {
                            num = num + 1;
                            Interfaz obj_Interfaz = new Interfaz(num,
                                int.Parse(dr_result["IdInterfaz"].ToString()),
                                "",
                                "",
                                0,
                                0,
                                "");

                            lista_Interfaz.Add(obj_Interfaz);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return lista_Interfaz;
        }

    }
}
