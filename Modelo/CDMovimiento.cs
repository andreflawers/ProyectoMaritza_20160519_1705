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
    public class CDMovimiento
    {
        public List<Movimiento> obtenerMovimiento(int personal_id)
        {
            String procedure = "sp_lis_movimientos";
            try
            {
                List<Movimiento> oListMovimientos = new List<Movimiento>();
                SqlConnection oSqlConnection = new SqlConnection();
                AdministracionConexion oAdministracionConexion = new AdministracionConexion();
                oSqlConnection = oAdministracionConexion.getConexion();
                SqlCommand oSqlCommand = new SqlCommand(procedure, oSqlConnection);
                oSqlCommand.CommandType = CommandType.StoredProcedure;
                oSqlCommand.Parameters.Add("@personal_id", SqlDbType.Int).Value = personal_id;
                oSqlCommand.Parameters.Add("@mes", SqlDbType.Int).Value = int.Parse(DateTime.Now.Month.ToString());
                oSqlCommand.Parameters.Add("@ano", SqlDbType.Int).Value = int.Parse(DateTime.Now.Year.ToString());
                SqlDataReader oSqlDataReader = oSqlCommand.ExecuteReader();
                while (oSqlDataReader.Read())
                {
                    Movimiento oMovimientos = new Movimiento();
                    oMovimientos.IdMovimiento = ((int)oSqlDataReader["IdMovimiento"]);
                    oMovimientos.descripcionMovimiento = ((String)oSqlDataReader["descripciónMovimiento"]);
                    oMovimientos.fechaMovimiento = Convert.ToDateTime(oSqlDataReader["fechaMovimiento"]).ToString("dd-MM-yyyy");
                    oMovimientos.montoMovimiento = ((decimal)oSqlDataReader["montoMovimiento"]);
                    oMovimientos.tipoMovimiento.IdTipoMovimiento = ((int)oSqlDataReader["IdTipoMovimiento"]);
                    oMovimientos.tipoMovimiento.nombreTipoMovimiento = ((String)oSqlDataReader["nombreTipoMovimiento"]);
                    oListMovimientos.Add(oMovimientos);
                }
                oSqlDataReader.Close();
                oSqlConnection.Close();
                return oListMovimientos;
            }
            catch (Exception e)
            {
                e.ToString();
                return null;
            }
        }
        public bool eliminarMovimiento(int IdMovimiento)
        {
            String pro_eliminar = "sp_del_movimientos";
            try
            {
                SqlConnection oSqlConnection = new SqlConnection();
                AdministracionConexion oAdministracionConexion = new AdministracionConexion();
                oSqlConnection = oAdministracionConexion.getConexion();
                SqlCommand SqlCommand = new SqlCommand(pro_eliminar, oSqlConnection);
                SqlCommand.CommandType = CommandType.StoredProcedure;
                SqlCommand.Parameters.Add("@IdMovimiento", SqlDbType.Int).Value = IdMovimiento;
                SqlCommand.ExecuteNonQuery();
                oSqlConnection.Close();
                return true;
            }
            catch
            {
                return false;
            }
        }
        public bool actualizarMovimiento(Movimiento oMovimientos)
        {
            String pro_actualiza = "sp_upd_movimientos_modificar";
            try
            {
                SqlConnection oSqlConnection = new SqlConnection();
                AdministracionConexion oAdministracionConexion = new AdministracionConexion();
                oSqlConnection = oAdministracionConexion.getConexion();
                SqlCommand SqlCommand = new SqlCommand(pro_actualiza, oSqlConnection);
                SqlCommand.CommandType = CommandType.StoredProcedure;

                SqlCommand.Parameters.Add("@IdMovimiento", SqlDbType.Int).Value = oMovimientos.IdMovimiento;
                SqlCommand.Parameters.Add("@descripcionMovimiento", SqlDbType.VarChar, 40).Value = oMovimientos.descripcionMovimiento;
                SqlCommand.Parameters.Add("@IdPersonal", SqlDbType.Int).Value = oMovimientos.IdPersonal;
                SqlCommand.Parameters.Add("@fechaMovimiento", SqlDbType.DateTime).Value = Convert.ToDateTime(oMovimientos.fechaMovimiento);
                SqlCommand.Parameters.Add("@montoMovimiento", SqlDbType.Decimal).Value = oMovimientos.montoMovimiento;
                SqlCommand.Parameters.Add("@IdTipoMovimiento", SqlDbType.Int).Value = oMovimientos.tipoMovimiento.IdTipoMovimiento;
                SqlCommand.ExecuteNonQuery();
                oSqlConnection.Close();
                return true;
            }
            catch
            {
                return false;
            }
        }
        public bool insertarMovimiento(Movimiento oMovimientos)
        {
            String pro_inserta = "sp_ins_movimientos_registro";
            try
            {
                SqlConnection oSqlConnection = new SqlConnection();
                AdministracionConexion oAdministracionConexion = new AdministracionConexion();
                oSqlConnection = oAdministracionConexion.getConexion();
                SqlCommand oSqlCommand = new SqlCommand(pro_inserta, oSqlConnection);
                oSqlCommand.CommandType = CommandType.StoredProcedure;
                oSqlCommand.Parameters.Add("@descripcionMovimiento", SqlDbType.VarChar, 40).Value = oMovimientos.descripcionMovimiento;
                oSqlCommand.Parameters.Add("@IdPersonal", SqlDbType.Int).Value = oMovimientos.IdPersonal;
                oSqlCommand.Parameters.Add("@fechaMovimiento", SqlDbType.DateTime).Value = Convert.ToDateTime(oMovimientos.fechaMovimiento);
                oSqlCommand.Parameters.Add("@montoMovimiento", SqlDbType.Decimal).Value = oMovimientos.montoMovimiento;
                oSqlCommand.Parameters.Add("@IdTipoMovimiento", SqlDbType.Int).Value = oMovimientos.tipoMovimiento.IdTipoMovimiento;
                oSqlCommand.ExecuteNonQuery();
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
