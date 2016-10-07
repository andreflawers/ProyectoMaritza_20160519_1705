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
    public class CDDepartamento
    {
        public List<Departamento> listarDepartamento()
        {

            String procedure = "sp_sel_departamento_listar";
            try
            {
                List<Departamento> oListDepartamento = new List<Departamento>();
                SqlConnection oSqlConnection = new SqlConnection();
                AdministracionConexion oAdministracionConexion = new AdministracionConexion();
                oSqlConnection = oAdministracionConexion.getConexion();
                SqlCommand oSqlCommand = new SqlCommand(procedure, oSqlConnection);
                oSqlCommand.CommandType = CommandType.StoredProcedure;
                SqlDataReader oSqlDataReader = oSqlCommand.ExecuteReader();
                while (oSqlDataReader.Read())
                {
                    Departamento oDepartamento = new Departamento();
                    oDepartamento.IdDepartamento = ((int)oSqlDataReader["IdDepartamento"]);
                    oDepartamento.nombreDepartamento = ((String)oSqlDataReader["nombreDepartamento"]);
                    oListDepartamento.Add(oDepartamento);
                }
                oSqlDataReader.Close();
                oSqlConnection.Close();
                return oListDepartamento;

            }
            catch (Exception e)
            {
                e.ToString();
                return null;
            }
        }

        public Departamento buscarDepartamento(int IdDepartamento)
        {
            String procedure = "sp_sel_departamento_buscar";
            try
            {
                SqlConnection oSqlConnection = new SqlConnection();
                AdministracionConexion oAdministracionConexion = new AdministracionConexion();
                oSqlConnection = oAdministracionConexion.getConexion();

                SqlCommand SqlCommand = new SqlCommand(procedure, oSqlConnection);
                SqlCommand.CommandType = CommandType.StoredProcedure;
                SqlCommand.Parameters.Add("@IdDepartamento", SqlDbType.Int).Value = IdDepartamento;
                SqlDataReader oSqlDataReader = SqlCommand.ExecuteReader();
                Departamento oDepartamento = new Departamento();
                oDepartamento.nombreDepartamento = "";
                while (oSqlDataReader.Read())
                {
                    oDepartamento.IdDepartamento = ((int)oSqlDataReader["IdDepartamento"]);
                    oDepartamento.nombreDepartamento = ((String)oSqlDataReader["nombreDepartamento"]);

                    oSqlDataReader.Close();
                    oSqlConnection.Close();
                    return oDepartamento;
                }
                return oDepartamento;
            }
            catch
            {
                return null;
            }
        }

        public bool insertarDepartamento(Departamento oDepartamento)
        {
            String pro_inserta = "sp_ins_departamento_registro";
            try
            {
                SqlConnection oSqlConnection = new SqlConnection();
                AdministracionConexion oAdministracionConexion = new AdministracionConexion();
                oSqlConnection = oAdministracionConexion.getConexion();

                SqlCommand oSqlCommand = new SqlCommand(pro_inserta, oSqlConnection);
                oSqlCommand.CommandType = CommandType.StoredProcedure;
                oSqlCommand.Parameters.Add("@nombreDepartamento", SqlDbType.VarChar, 50).Value = oDepartamento.nombreDepartamento;
                oSqlCommand.ExecuteNonQuery();
                oSqlConnection.Close();
                return true;
            }
            catch
            {
                return false;
            }
        }

        public bool actualizarDepartamento(Departamento oDepartamento)
        {
            String pro_actualiza = "sp_upd_departamento_modificar";
            try
            {
                SqlConnection oSqlConnection = new SqlConnection();
                AdministracionConexion oAdministracionConexion = new AdministracionConexion();
                oSqlConnection = oAdministracionConexion.getConexion();
                SqlCommand SqlCommand = new SqlCommand(pro_actualiza, oSqlConnection);
                SqlCommand.CommandType = CommandType.StoredProcedure;

                SqlCommand.Parameters.Add("@IdDepartamento", SqlDbType.Int).Value = oDepartamento.IdDepartamento;
                SqlCommand.Parameters.Add("@nombreDepartamento", SqlDbType.VarChar, 50).Value = oDepartamento.nombreDepartamento;
                SqlCommand.ExecuteNonQuery();
                oSqlConnection.Close();
                return true;
            }
            catch
            {
                return false;
            }
        }

        public bool eliminarDepartamento(int IdDepartamento)
        {
            String pro_eliminar = "sp_del_departamento_eliminar";
            try
            {
                SqlConnection oSqlConnection = new SqlConnection();
                AdministracionConexion oAdministracionConexion = new AdministracionConexion();
                oSqlConnection = oAdministracionConexion.getConexion();
                SqlCommand SqlCommand = new SqlCommand(pro_eliminar, oSqlConnection);
                SqlCommand.CommandType = CommandType.StoredProcedure;
                SqlCommand.Parameters.Add("@IdDepartamento", SqlDbType.Int).Value = IdDepartamento;
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
