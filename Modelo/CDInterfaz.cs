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
    public class CDInterfaz
    {
        public static List<Interfaz> Interfaz_Listar(SqlConnection conn)
        {
            List<Interfaz> lista_interfaz = new List<Interfaz>();
            try
            {
                using (SqlCommand cmd = new SqlCommand("sp_lis_Interfaz_listar", conn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    SqlDataReader dr_result = cmd.ExecuteReader();
                    var num = 0;

                    if (dr_result.HasRows)
                    {
                        while (dr_result.Read())
                        {
                            var estadoInterfaz = "Inactivo";
                            num = num + 1;
                            if (dr_result["estadoInterfaz"].ToString() == "1") estadoInterfaz = "Activo";
                            else if (dr_result["estadoInterfaz"].ToString() == "0") estadoInterfaz = "Inactivo";
                            Interfaz obj_Interfaz = new Interfaz(num,
                                int.Parse(dr_result["IdInterfaz"].ToString()),
                                dr_result["nombreInterfaz"].ToString(),
                                estadoInterfaz,
                                0,
                                int.Parse(dr_result["IdModulo"].ToString()),
                                dr_result["nombreModulo"].ToString());

                            lista_interfaz.Add(obj_Interfaz);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return lista_interfaz;
        }

        public static Interfaz Interfaz_insertar(SqlConnection conn, Interfaz obj_Interfaz, List<Rol> oRol)
        {
            try
            {
                using (SqlCommand cmd = new SqlCommand("sp_ins_Interfaz_registrar", conn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add("@IdInterfaz", SqlDbType.Int).Direction = ParameterDirection.Output;
                    cmd.Parameters.Add("@nombreInterfaz", SqlDbType.VarChar, 50).Value = obj_Interfaz.nombreInterfaz;
                    cmd.Parameters.Add("@IdModulo", SqlDbType.Int).Value = obj_Interfaz.IdModulo;
                    cmd.Parameters.Add("@estadoInterfaz", SqlDbType.Char, 1).Value = "1";
                    cmd.ExecuteNonQuery();

                    obj_Interfaz.IdInterfaz = int.Parse(cmd.Parameters["@IdInterfaz"].Value.ToString());
                }

                if (obj_Interfaz.IdInterfaz != 0)
                {
                    foreach (var item in oRol)
                    {
                        using (SqlCommand cmd = new SqlCommand("sp_ins_Interfaz_registrarInterfaz_Rol", conn))
                        {
                            cmd.CommandType = CommandType.StoredProcedure;
                            cmd.Parameters.Add("@IdRol", SqlDbType.Int).Value = item.IdRol;
                            cmd.Parameters.Add("@IdInterfaz", SqlDbType.Int).Value = obj_Interfaz.IdInterfaz;
                            cmd.ExecuteNonQuery();
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return obj_Interfaz;
        }

        public static Boolean Interfaz_Eliminar(SqlConnection conn, Interfaz obj_Interfaz)
        {
            try
            {
                int permiso = 0;

                using (SqlCommand cmd = new SqlCommand("sp_lis_Interfaz_listarInterfaces_RolEstado", conn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add("@permiso", SqlDbType.Int).Direction = ParameterDirection.Output;
                    cmd.Parameters.Add("@IdInterfaz", SqlDbType.Int).Value = obj_Interfaz.IdInterfaz;
                    cmd.ExecuteNonQuery();

                    permiso = int.Parse(cmd.Parameters["@permiso"].Value.ToString());
                }

                if (permiso == 0) return false;

                using (SqlCommand cmd = new SqlCommand("sp_del_Interfaz_eliminar", conn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add("@IdInterfaz", SqlDbType.Int).Value = obj_Interfaz.IdInterfaz;
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
        public static bool Interfaz_Actualizar(SqlConnection conn, Interfaz obj_Interfaz)
        {
            try
            {
                int existente = 1;
                using (SqlCommand cmd = new SqlCommand("sp_upd_Interfaz_actualizar", conn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add("@existente", SqlDbType.Char, 1).Direction = ParameterDirection.Output;
                    cmd.Parameters.Add("@IdInterfaz", SqlDbType.Int).Value = obj_Interfaz.IdInterfaz;
                    cmd.Parameters.Add("@nombreInterfaz", SqlDbType.VarChar, 50).Value = obj_Interfaz.nombreInterfaz;
                    cmd.Parameters.Add("@IdModulo", SqlDbType.Int).Value = obj_Interfaz.IdModulo;
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


        public static int Interfaz_ActualizarEstado(SqlConnection conn, Interfaz obj_Interfaz, List<Rol> lista_Rol)
        {
            try
            {
                int validar = 1;
                if (obj_Interfaz.estadoInterfaz == "0")
                {
                    foreach (var item in lista_Rol)
                    {
                        using (SqlCommand cmd = new SqlCommand("sp_bus_Interfaz_ValidarCambioEstado", conn))
                        {
                            cmd.CommandType = CommandType.StoredProcedure;
                            cmd.Parameters.Add("@validar", SqlDbType.Int).Direction = ParameterDirection.Output;
                            cmd.Parameters.Add("@IdRol", SqlDbType.Int).Value = item.IdRol;
                            cmd.Parameters.Add("@IdInterfaz", SqlDbType.Int).Value = obj_Interfaz.IdInterfaz;
                            cmd.ExecuteNonQuery();

                            validar = int.Parse(cmd.Parameters["@validar"].Value.ToString());
                        }
                    }
                }

                if (validar == 1)
                {
                    using (SqlCommand cmd = new SqlCommand("sp_upd_Interfaz_actualizarEstado", conn))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.Add("@IdInterfaz", SqlDbType.Int).Value = obj_Interfaz.IdInterfaz;
                        cmd.Parameters.Add("@estadoInterfaz", SqlDbType.Char, 1).Value = obj_Interfaz.estadoInterfaz;
                        cmd.ExecuteNonQuery();
                    }
                }

                return validar;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public static List<Interfaz> Interfaz_ListarActivos(SqlConnection conn)
        {
            List<Interfaz> lista_interfaz = new List<Interfaz>();
            try
            {
                using (SqlCommand cmd = new SqlCommand("sp_lis_Interfaz_listarActivos", conn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    SqlDataReader dr_result = cmd.ExecuteReader();
                    var num = 0;

                    if (dr_result.HasRows)
                    {
                        while (dr_result.Read())
                        {
                            var estadoInterfaz = "Inactivo";
                            num = num + 1;
                            if (dr_result["estadoInterfaz"].ToString() == "1") estadoInterfaz = "Activo";
                            else if (dr_result["estadoInterfaz"].ToString() == "0") estadoInterfaz = "Inactivo";
                            Interfaz obj_Interfaz = new Interfaz(num,
                                int.Parse(dr_result["IdInterfaz"].ToString()),
                                dr_result["nombreInterfaz"].ToString(),
                                estadoInterfaz,
                                0,
                                0,
                                "");

                            lista_interfaz.Add(obj_Interfaz);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return lista_interfaz;
        }
    }
}
