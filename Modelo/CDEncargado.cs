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
    public class CDEncargado
    {
        public List<Encargado> listarEncarga()
        {

            String procedure = "sp_lis_encargado";
            try
            {
                List<Encargado> oListEncargado = new List<Encargado>();
                SqlConnection oSqlConnection = new SqlConnection();
                AdministracionConexion oAdministracionConexion = new AdministracionConexion();
                oSqlConnection = oAdministracionConexion.getConexion();
                SqlCommand oSqlCommand = new SqlCommand(procedure, oSqlConnection);
                oSqlCommand.CommandType = CommandType.StoredProcedure;
                SqlDataReader oSqlDataReader = oSqlCommand.ExecuteReader();
                while (oSqlDataReader.Read())
                {
                    Encargado oEncargado = new Encargado();
                    oEncargado.IdEncargado = ((int)oSqlDataReader["IdPersonal"]);
                    oEncargado.personal_apellido_materno = ((String)oSqlDataReader["apellidoMaternoPersonal"]);
                    oEncargado.personal_apellido_paterno = ((String)oSqlDataReader["apellidoPaternoPersonal"]);
                    oEncargado.personal_nombre = ((String)oSqlDataReader["nombrePersonal"]);
                    oListEncargado.Add(oEncargado);
                }
                oSqlDataReader.Close();
                oSqlConnection.Close();
                return oListEncargado;

            }
            catch (Exception e)
            {
                e.ToString();
                return null;
            }
        }
    }
}
