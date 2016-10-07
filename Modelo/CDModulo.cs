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
    public class CDModulo
    {
        public static List<Modulo> Modulo_Listar(SqlConnection conn)
        {
            List<Modulo> lista_modulo = new List<Modulo>();
            try
            {
                using (SqlCommand cmd = new SqlCommand("sp_lis_Modulo_listar", conn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    SqlDataReader dr_result = cmd.ExecuteReader();
                    var num = 0;

                    if (dr_result.HasRows)
                    {
                        while (dr_result.Read())
                        {
                            num = num + 1;
                            Modulo obj_modulo = new Modulo(num,
                                int.Parse(dr_result["IdModulo"].ToString()),
                                dr_result["nombreModulo"].ToString());

                            lista_modulo.Add(obj_modulo);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return lista_modulo;
        }

        public static Modulo Modulo_insertar(SqlConnection conn, Modulo obj_modulo)
        {
            try
            {
                using (SqlCommand cmd = new SqlCommand("sp_ins_Modulo_registrar", conn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add("@IdModulo", SqlDbType.Int).Direction = ParameterDirection.Output;
                    cmd.Parameters.Add("@nombreModulo", SqlDbType.VarChar, 50).Value = obj_modulo.nombreModulo;
                    cmd.ExecuteNonQuery();

                    obj_modulo.IdModulo = int.Parse(cmd.Parameters["@IdModulo"].Value.ToString());
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return obj_modulo;
        }

        public static Boolean Modulo_Eliminar(SqlConnection conn, Modulo obj_modulo)
        {
            try
            {
                using (SqlCommand cmd = new SqlCommand("sp_del_Modulo_eliminar", conn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add("@IdModulo", SqlDbType.Int).Value = obj_modulo.IdModulo;
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
        public static bool Modulo_Actualizar(SqlConnection conn, Modulo obj_modulo)
        {
            try
            {
                int existencia = 1;
                using (SqlCommand cmd = new SqlCommand("sp_upd_Modulo_actualizar", conn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add("@existente", SqlDbType.Int).Direction = ParameterDirection.Output;
                    cmd.Parameters.Add("@IdModulo", SqlDbType.Int).Value = obj_modulo.IdModulo;
                    cmd.Parameters.Add("@nombreModulo", SqlDbType.VarChar, 50).Value = obj_modulo.nombreModulo;
                    cmd.ExecuteNonQuery();

                    existencia = int.Parse(cmd.Parameters["@existente"].Value.ToString());
                }

                if (existencia == 0) return true;
                else return false;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
/*-----------------------------------------------------------------*/
    }
}
