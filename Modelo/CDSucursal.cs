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
    public class CDSucursal
    {
        public List<Sucursal> listarSucursal()
        {

            String procedure = "sp_sel_sucursal_listar";
            try
            {
                List<Sucursal> oListSucursal = new List<Sucursal>();
                SqlConnection oSqlConnection = new SqlConnection();
                AdministracionConexion oAdministracionConexion = new AdministracionConexion();
                oSqlConnection = oAdministracionConexion.getConexion();
                SqlCommand oSqlCommand = new SqlCommand(procedure, oSqlConnection);
                oSqlCommand.CommandType = CommandType.StoredProcedure;
                SqlDataReader oSqlDataReader = oSqlCommand.ExecuteReader();
                while (oSqlDataReader.Read())
                {
                    Sucursal oSucursal = new Sucursal();
                    oSucursal.IdSucursal = ((int)oSqlDataReader["IdSucursal"]);
                    oSucursal.nombreSucursal = ((String)oSqlDataReader["nombreSucursal"]);
                    oSucursal.direccionSucursal = ((String)oSqlDataReader["direccionSucursal"]);
                    oSucursal.telefonoSucursal = ((String)oSqlDataReader["telefonoSucursal"]);
                    oSucursal.rucSucursal = ((String)oSqlDataReader["rucSucursal"]);
                    oSucursal.IdDistrito = ((int)oSqlDataReader["IdDistrito"]);
                    oSucursal.nombreDistrito = ((String)oSqlDataReader["nombreDistrito"]);
                    oListSucursal.Add(oSucursal);
                }
                oSqlDataReader.Close();
                oSqlConnection.Close();
                return oListSucursal;

            }
            catch (Exception e)
            {
                e.ToString();
                return null;
            }
        }

        public bool insertarSucursal(Sucursal oSucursal)
        {
            String pro_inserta = "sp_ins_sucursal_registro";
            try
            {
                SqlConnection oSqlConnection = new SqlConnection();
                AdministracionConexion oAdministracionConexion = new AdministracionConexion();
                oSqlConnection = oAdministracionConexion.getConexion();

                SqlCommand oSqlCommand = new SqlCommand(pro_inserta, oSqlConnection);
                oSqlCommand.CommandType = CommandType.StoredProcedure;
                oSqlCommand.Parameters.Add("@nombreSucursal", SqlDbType.VarChar, 50).Value = oSucursal.nombreSucursal;
                oSqlCommand.Parameters.Add("@direccionSucursal", SqlDbType.VarChar, 50).Value = oSucursal.direccionSucursal;
                oSqlCommand.Parameters.Add("@telefonoSucursal", SqlDbType.Char, 10).Value = oSucursal.telefonoSucursal;
                oSqlCommand.Parameters.Add("@rucSucursal", SqlDbType.Char, 10).Value = oSucursal.rucSucursal;
                oSqlCommand.Parameters.Add("@IdDistrito", SqlDbType.Int).Value = oSucursal.IdDistrito;
                oSqlCommand.ExecuteNonQuery();
                oSqlConnection.Close();
                return true;
            }
            catch
            {
                return false;
            }
        }

        public bool actualizarSucursal(Sucursal oSucursal)
        {
            String pro_actualiza = "sp_upd_sucursal_modificar";
            try
            {
                SqlConnection oSqlConnection = new SqlConnection();
                AdministracionConexion oAdministracionConexion = new AdministracionConexion();
                oSqlConnection = oAdministracionConexion.getConexion();
                SqlCommand SqlCommand = new SqlCommand(pro_actualiza, oSqlConnection);
                SqlCommand.CommandType = CommandType.StoredProcedure;
                SqlCommand.Parameters.Add("@IdSucursal", SqlDbType.Int).Value = oSucursal.IdSucursal;
                SqlCommand.Parameters.Add("@nombreSucursal", SqlDbType.VarChar, 50).Value = oSucursal.nombreSucursal;
                SqlCommand.Parameters.Add("@direccionSucursal", SqlDbType.VarChar, 50).Value = oSucursal.direccionSucursal;
                SqlCommand.Parameters.Add("@telefonoSucursal", SqlDbType.Char, 10).Value = oSucursal.telefonoSucursal;
                SqlCommand.Parameters.Add("@rucSucursal", SqlDbType.Char, 10).Value = oSucursal.rucSucursal;
                SqlCommand.Parameters.Add("@IdDistrito", SqlDbType.Int).Value = oSucursal.IdDistrito;
                SqlCommand.ExecuteNonQuery();
                oSqlConnection.Close();
                return true;
            }
            catch
            {
                return false;
            }
        }

        public bool eliminarSucursal(int IdSucursal)
        {
            String pro_eliminar = "sp_del_sucursal_eliminar";
            try
            {
                SqlConnection oSqlConnection = new SqlConnection();
                AdministracionConexion oAdministracionConexion = new AdministracionConexion();
                oSqlConnection = oAdministracionConexion.getConexion();
                SqlCommand SqlCommand = new SqlCommand(pro_eliminar, oSqlConnection);
                SqlCommand.CommandType = CommandType.StoredProcedure;
                SqlCommand.Parameters.Add("@IdSucursal", SqlDbType.Int).Value = IdSucursal;
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
