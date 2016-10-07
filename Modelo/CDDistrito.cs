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
    public class CDDistrito
    {
        public List<Distrito> listarDistrito()
        {

            String procedure = "sp_sel_distrito_listar";
            try
            {
                List<Distrito> oListDistrito = new List<Distrito>();
                SqlConnection oSqlConnection = new SqlConnection();
                AdministracionConexion oAdministracionConexion = new AdministracionConexion();
                oSqlConnection = oAdministracionConexion.getConexion();
                SqlCommand oSqlCommand = new SqlCommand(procedure, oSqlConnection);
                oSqlCommand.CommandType = CommandType.StoredProcedure;
                SqlDataReader oSqlDataReader = oSqlCommand.ExecuteReader();
                while (oSqlDataReader.Read())
                {
                    Distrito oDistrito = new Distrito();
                    oDistrito.IdDistrito = ((int)oSqlDataReader["IdDistrito"]);
                    oDistrito.nombreDistrito = ((String)oSqlDataReader["nombreDistrito"]);
                    oDistrito.IdProvincia = ((int)oSqlDataReader["IdProvincia"]);
                    oDistrito.nombreProvincia = ((String)oSqlDataReader["nombreProvincia"]);
                    oListDistrito.Add(oDistrito);
                }
                oSqlDataReader.Close();
                oSqlConnection.Close();
                return oListDistrito;

            }
            catch (Exception e)
            {
                e.ToString();
                return null;
            }
        }

        public bool insertarDistrito(Distrito oDistrito)
        {
            String pro_inserta = "sp_ins_distrito_registro";
            try
            {
                SqlConnection oSqlConnection = new SqlConnection();
                AdministracionConexion oAdministracionConexion = new AdministracionConexion();
                oSqlConnection = oAdministracionConexion.getConexion();

                SqlCommand oSqlCommand = new SqlCommand(pro_inserta, oSqlConnection);
                oSqlCommand.CommandType = CommandType.StoredProcedure;
                oSqlCommand.Parameters.Add("@nombreDistrito", SqlDbType.VarChar, 50).Value = oDistrito.nombreDistrito;
                oSqlCommand.Parameters.Add("@IdProvincia", SqlDbType.Int).Value = oDistrito.IdProvincia;
                oSqlCommand.ExecuteNonQuery();
                oSqlConnection.Close();
                return true;
            }
            catch
            {
                return false;
            }
        }

        public bool actualizarDistrito(Distrito oDistrito)
        {
            String pro_actualiza = "sp_upd_distrito_modificar";
            try
            {
                SqlConnection oSqlConnection = new SqlConnection();
                AdministracionConexion oAdministracionConexion = new AdministracionConexion();
                oSqlConnection = oAdministracionConexion.getConexion();
                SqlCommand SqlCommand = new SqlCommand(pro_actualiza, oSqlConnection);
                SqlCommand.CommandType = CommandType.StoredProcedure;
                SqlCommand.Parameters.Add("@IdDistrito", SqlDbType.Int).Value = oDistrito.IdDistrito;
                SqlCommand.Parameters.Add("@nombreDistrito", SqlDbType.VarChar, 50).Value = oDistrito.nombreDistrito;
                SqlCommand.Parameters.Add("@IdProvincia", SqlDbType.Int).Value = oDistrito.IdProvincia;
                SqlCommand.ExecuteNonQuery();
                oSqlConnection.Close();
                return true;
            }
            catch
            {
                return false;
            }
        }

        public bool eliminarDistrito(int IdDistrito)
        {
            String pro_eliminar = "sp_del_distrito_eliminar";
            try
            {
                SqlConnection oSqlConnection = new SqlConnection();
                AdministracionConexion oAdministracionConexion = new AdministracionConexion();
                oSqlConnection = oAdministracionConexion.getConexion();
                SqlCommand SqlCommand = new SqlCommand(pro_eliminar, oSqlConnection);
                SqlCommand.CommandType = CommandType.StoredProcedure;
                SqlCommand.Parameters.Add("@IdDistrito", SqlDbType.Int).Value = IdDistrito;
                SqlCommand.ExecuteNonQuery();
                oSqlConnection.Close();
                return true;
            }
            catch
            {
                return false;
            }
        }
        public List<Distrito> listar_distrito_por_provincia(int provincia_id) 
        {
            String procedure = "sp_sel_distrito_by_id_provincia";
            try
            {
                SqlConnection oSqlConnection = new SqlConnection();
                AdministracionConexion oAdministracionConexion = new AdministracionConexion();
                oSqlConnection = oAdministracionConexion.getConexion();

                SqlCommand SqlCommand = new SqlCommand(procedure, oSqlConnection);
                SqlCommand.CommandType = CommandType.StoredProcedure;
                SqlCommand.Parameters.Add("@provincia_id", SqlDbType.Int).Value = provincia_id;
                SqlDataReader oSqlDataReader = SqlCommand.ExecuteReader();
                List<Distrito> oListDistrito = new List<Distrito>();
                while (oSqlDataReader.Read())
                {
                    Distrito oDitrito = new Distrito();
                    oDitrito.IdDistrito = ((int)oSqlDataReader["IdDistrito"]);
                    oDitrito.nombreDistrito = ((String)oSqlDataReader["nombreDistrito"]);
                    oListDistrito.Add(oDitrito);
                }
                oSqlDataReader.Close();
                oSqlConnection.Close();
                return oListDistrito;
            }
            catch
            {
                return null;
            }
        }
    }
}
