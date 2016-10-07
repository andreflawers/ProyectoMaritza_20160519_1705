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
    public class CDNivelInstruccion
    {
        public List<NivelInstruccion> listarNivelInstruccion()
        {

            String procedure = "sp_sel_nivelInstruccion_listar";
            try
            {
                List<NivelInstruccion> oListNivelInstrucion = new List<NivelInstruccion>();
                SqlConnection oSqlConnection = new SqlConnection();
                AdministracionConexion oAdministracionConexion = new AdministracionConexion();
                oSqlConnection = oAdministracionConexion.getConexion();
                SqlCommand oSqlCommand = new SqlCommand(procedure, oSqlConnection);
                oSqlCommand.CommandType = CommandType.StoredProcedure;
                SqlDataReader oSqlDataReader = oSqlCommand.ExecuteReader();
                while (oSqlDataReader.Read())
                {
                    NivelInstruccion oNivelInstruccion = new NivelInstruccion();
                    oNivelInstruccion.IdNivelInstruccion = ((int)oSqlDataReader["IdNivelInstruccion"]);
                    oNivelInstruccion.nombreNivelInstruccion = ((String)oSqlDataReader["nombreNivelInstruccion"]);
                    oListNivelInstrucion.Add(oNivelInstruccion);
                }
                oSqlDataReader.Close();
                oSqlConnection.Close();
                return oListNivelInstrucion;

            }
            catch (Exception e)
            {
                e.ToString();
                return null;
            }
        }

        public NivelInstruccion buscarNivelInstruccion(int IdNivelInstruccion)
        {
            String procedure = "sp_sel_nivelInstruccion_buscar";
            try
            {
                SqlConnection oSqlConnection = new SqlConnection();
                AdministracionConexion oAdministracionConexion = new AdministracionConexion();
                oSqlConnection = oAdministracionConexion.getConexion();

                SqlCommand SqlCommand = new SqlCommand(procedure, oSqlConnection);
                SqlCommand.CommandType = CommandType.StoredProcedure;
                SqlCommand.Parameters.Add("@IdNivelInstruccion", SqlDbType.Int).Value = IdNivelInstruccion;
                SqlDataReader oSqlDataReader = SqlCommand.ExecuteReader();
                NivelInstruccion oNivelInstruccion = new NivelInstruccion();
                oNivelInstruccion.nombreNivelInstruccion = "";
                while (oSqlDataReader.Read())
                {
                    oNivelInstruccion.IdNivelInstruccion = ((int)oSqlDataReader["IdNivelInstruccion"]);
                    oNivelInstruccion.nombreNivelInstruccion = ((String)oSqlDataReader["nombreNivelInstruccion"]);

                    oSqlDataReader.Close();
                    oSqlConnection.Close();
                    return oNivelInstruccion;
                }
                return oNivelInstruccion;
            }
            catch
            {
                return null;
            }
        }

        public bool insertarNivelInstruccion(NivelInstruccion oNivelInstruccion)
        {
            String pro_inserta = "sp_ins_nivelInstruccion_registro";
            try
            {
                SqlConnection oSqlConnection = new SqlConnection();
                AdministracionConexion oAdministracionConexion = new AdministracionConexion();
                oSqlConnection = oAdministracionConexion.getConexion();

                SqlCommand oSqlCommand = new SqlCommand(pro_inserta, oSqlConnection);
                oSqlCommand.CommandType = CommandType.StoredProcedure;
                oSqlCommand.Parameters.Add("@nombreNivelInstruccion", SqlDbType.VarChar, 40).Value = oNivelInstruccion.nombreNivelInstruccion;
                oSqlCommand.ExecuteNonQuery();
                oSqlConnection.Close();
                return true;
            }
            catch
            {
                return false;
            }
        }

        public bool actualizarNivelInstruccion(NivelInstruccion oNivelInstruccion)
        {
            String pro_actualiza = "sp_upd_nivelInstruccion_modificar";
            try
            {
                SqlConnection oSqlConnection = new SqlConnection();
                AdministracionConexion oAdministracionConexion = new AdministracionConexion();
                oSqlConnection = oAdministracionConexion.getConexion();
                SqlCommand SqlCommand = new SqlCommand(pro_actualiza, oSqlConnection);
                SqlCommand.CommandType = CommandType.StoredProcedure;

                SqlCommand.Parameters.Add("@IdNivelInstruccion", SqlDbType.Int).Value = oNivelInstruccion.IdNivelInstruccion;
                SqlCommand.Parameters.Add("@nombreNivelInstruccion", SqlDbType.VarChar, 40).Value = oNivelInstruccion.nombreNivelInstruccion;
                SqlCommand.ExecuteNonQuery();
                oSqlConnection.Close();
                return true;
            }
            catch
            {
                return false;
            }
        }

        public bool eliminarNivelInstruccion(int IdNivelInstruccion)
        {
            String pro_eliminar = "sp_del_nivelInstruccion_eliminar";
            try
            {
                SqlConnection oSqlConnection = new SqlConnection();
                AdministracionConexion oAdministracionConexion = new AdministracionConexion();
                oSqlConnection = oAdministracionConexion.getConexion();
                SqlCommand SqlCommand = new SqlCommand(pro_eliminar, oSqlConnection);
                SqlCommand.CommandType = CommandType.StoredProcedure;
                SqlCommand.Parameters.Add("@IdNivelInstruccion", SqlDbType.Int).Value = IdNivelInstruccion;
                SqlCommand.ExecuteNonQuery();
                oSqlConnection.Close();
                return true;
            }
            catch
            {
                return false;
            }
        }
    }
}
