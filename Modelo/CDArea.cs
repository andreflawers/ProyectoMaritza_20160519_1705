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
    public class CDArea
    {
        public List<Area> listarArea()
        {

            String procedure = "sp_sel_area_listar";
            try
            {
                List<Area> oListArea = new List<Area>();
                SqlConnection oSqlConnection = new SqlConnection();
                AdministracionConexion oAdministracionConexion = new AdministracionConexion();
                oSqlConnection = oAdministracionConexion.getConexion();
                SqlCommand oSqlCommand = new SqlCommand(procedure, oSqlConnection);
                oSqlCommand.CommandType = CommandType.StoredProcedure;
                SqlDataReader oSqlDataReader = oSqlCommand.ExecuteReader();
                while (oSqlDataReader.Read())
                {
                    Area oArea = new Area();
                    oArea.IdArea = ((int)oSqlDataReader["IdArea"]);
                    oArea.nombreArea = ((String)oSqlDataReader["nombreArea"]);
                    oListArea.Add(oArea);
                }
                oSqlDataReader.Close();
                oSqlConnection.Close();
                return oListArea;

            }
            catch (Exception e)
            {
                e.ToString();
                return null;
            }
        }

        public Area buscarArea(int IdArea)
        {
            String procedure = "sp_sel_area_buscar";
            try
            {
                SqlConnection oSqlConnection = new SqlConnection();
                AdministracionConexion oAdministracionConexion = new AdministracionConexion();
                oSqlConnection = oAdministracionConexion.getConexion();

                SqlCommand SqlCommand = new SqlCommand(procedure, oSqlConnection);
                SqlCommand.CommandType = CommandType.StoredProcedure;
                SqlCommand.Parameters.Add("@IdArea", SqlDbType.Int).Value = IdArea;
                SqlDataReader oSqlDataReader = SqlCommand.ExecuteReader();
                Area oArea = new Area();
                oArea.nombreArea = "";
                while (oSqlDataReader.Read())
                {
                    oArea.IdArea = ((int)oSqlDataReader["IdArea"]);
                    oArea.nombreArea = ((String)oSqlDataReader["nombreArea"]);

                    oSqlDataReader.Close();
                    oSqlConnection.Close();
                    return oArea;
                }
                return oArea;
            }
            catch
            {
                return null;
            }
        }

        public bool insertarArea(Area oArea)
        {
            String pro_inserta = "sp_ins_area_registro";
            try
            {
                SqlConnection oSqlConnection = new SqlConnection();
                AdministracionConexion oAdministracionConexion = new AdministracionConexion();
                oSqlConnection = oAdministracionConexion.getConexion();

                SqlCommand oSqlCommand = new SqlCommand(pro_inserta, oSqlConnection);
                oSqlCommand.CommandType = CommandType.StoredProcedure;
                oSqlCommand.Parameters.Add("@nombreArea", SqlDbType.VarChar, 50).Value = oArea.nombreArea;
                oSqlCommand.ExecuteNonQuery();
                oSqlConnection.Close();
                return true;
            }
            catch
            {
                return false;
            }
        }

        public bool actualizarArea(Area oArea)
        {
            String pro_actualiza = "sp_upd_area_modificar";
            try
            {
                SqlConnection oSqlConnection = new SqlConnection();
                AdministracionConexion oAdministracionConexion = new AdministracionConexion();
                oSqlConnection = oAdministracionConexion.getConexion();
                SqlCommand SqlCommand = new SqlCommand(pro_actualiza, oSqlConnection);
                SqlCommand.CommandType = CommandType.StoredProcedure;

                SqlCommand.Parameters.Add("@IdArea", SqlDbType.Int).Value = oArea.IdArea;
                SqlCommand.Parameters.Add("@nombreArea", SqlDbType.VarChar, 50).Value = oArea.nombreArea;
                SqlCommand.ExecuteNonQuery();
                oSqlConnection.Close();
                return true;
            }
            catch
            {
                return false;
            }
        }

        public bool eliminarArea(int IdArea)
        {
            String pro_eliminar = "sp_del_area_eliminar";
            try
            {
                SqlConnection oSqlConnection = new SqlConnection();
                AdministracionConexion oAdministracionConexion = new AdministracionConexion();
                oSqlConnection = oAdministracionConexion.getConexion();
                SqlCommand SqlCommand = new SqlCommand(pro_eliminar, oSqlConnection);
                SqlCommand.CommandType = CommandType.StoredProcedure;
                SqlCommand.Parameters.Add("@IdArea", SqlDbType.Int).Value = IdArea;
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
