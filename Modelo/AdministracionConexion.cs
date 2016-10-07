using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Modelo
{
    public class AdministracionConexion
    {
        SqlConnection osqlConexion = new SqlConnection();
        public SqlConnection getConexion()
        {
            try
            {
                osqlConexion.ConnectionString = "Server=tcp:calidadserver.database.windows.net,1433;Initial Catalog=Db_Panaderia_Std;Persist Security Info=False;User ID=martizarules;Password=(calidad666);MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;";
                osqlConexion.Open();
                return osqlConexion;
            }
            catch (Exception e)
            {
                return null;
            }
        }
        public static void CloseConexion(SqlConnection oConexion)
        {
            try
            {
                if (oConexion.State != ConnectionState.Closed)
                {
                    oConexion.Close();
                }
                oConexion.Dispose();
                oConexion = null;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
