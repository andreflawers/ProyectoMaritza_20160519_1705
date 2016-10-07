using Entidad;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Modelo
{
    public class CDCargo
    {
        public List<Cargo> listarCargo()
        {

            String procedure = "sp_sel_cargo_listar";
            try
            {
                List<Cargo> oListCargo = new List<Cargo>();
                SqlConnection oSqlConnection = new SqlConnection();
                AdministracionConexion oCDConnection = new AdministracionConexion();
                oSqlConnection = oCDConnection.getConexion();
                SqlCommand oSqlCommand = new SqlCommand(procedure, oSqlConnection);
                oSqlCommand.CommandType = CommandType.StoredProcedure;
                SqlDataReader oSqlDataReader = oSqlCommand.ExecuteReader();
                while (oSqlDataReader.Read())
                {
                    Cargo oCargo = new Cargo();
                    oCargo.IdCargo = ((int)oSqlDataReader["IdCargo"]);
                    oCargo.nombreCargo = ((String)oSqlDataReader["nombreCargo"]);
                    oCargo.montoSalarioCargo = ((Decimal)(oSqlDataReader["montoSalarioCargo"]));
                    oListCargo.Add(oCargo);
                }
                oSqlDataReader.Close();
                oSqlConnection.Close();
                return oListCargo;

            }
            catch (Exception e)
            {
                e.ToString();
                return null;
            }
        }

        public bool insertarCargo(Cargo oCargo)
        {
            String pro_inserta = "sp_ins_cargo_registro";
            try
            {
                SqlConnection oSqlConnection = new SqlConnection();
                AdministracionConexion oCDConnection = new AdministracionConexion();
                oSqlConnection = oCDConnection.getConexion();

                SqlCommand oSqlCommand = new SqlCommand(pro_inserta, oSqlConnection);
                oSqlCommand.CommandType = CommandType.StoredProcedure;
                oSqlCommand.Parameters.Add("@nombreCargo", SqlDbType.VarChar, 30).Value = oCargo.nombreCargo;
                oSqlCommand.Parameters.Add("@montoSalarioCargo", SqlDbType.Decimal, 10).Value = oCargo.montoSalarioCargo;
                oSqlCommand.ExecuteNonQuery();
                oSqlConnection.Close();
                return true;
            }
            catch
            {
                return false;
            }
        }

        public bool actualizarCargo(Cargo oCargo)
        {
            String pro_actualiza = "sp_upd_cargo_modificar";
            try
            {
                SqlConnection oSqlConnection = new SqlConnection();
                AdministracionConexion oCDConnection = new AdministracionConexion();
                oSqlConnection = oCDConnection.getConexion();
                SqlCommand SqlCommand = new SqlCommand(pro_actualiza, oSqlConnection);
                SqlCommand.CommandType = CommandType.StoredProcedure;

                SqlCommand.Parameters.Add("@IdCargo", SqlDbType.Int).Value = oCargo.IdCargo;
                SqlCommand.Parameters.Add("@nombreCargo", SqlDbType.VarChar, 30).Value = oCargo.nombreCargo;
                SqlCommand.Parameters.Add("@montoSalarioCargo", SqlDbType.Decimal, 10).Value = oCargo.montoSalarioCargo;
                SqlCommand.ExecuteNonQuery();
                oSqlConnection.Close();
                return true;
            }
            catch
            {
                return false;
            }
        }

        public bool eliminarCargo(int IdCargo)
        {
            String pro_eliminar = "sp_del_cargo_eliminar";
            try
            {
                SqlConnection oSqlConnection = new SqlConnection();
                AdministracionConexion oCDConnection = new AdministracionConexion();
                oSqlConnection = oCDConnection.getConexion();
                SqlCommand SqlCommand = new SqlCommand(pro_eliminar, oSqlConnection);
                SqlCommand.CommandType = CommandType.StoredProcedure;
                SqlCommand.Parameters.Add("@IdCargo", SqlDbType.Int).Value = IdCargo;
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
