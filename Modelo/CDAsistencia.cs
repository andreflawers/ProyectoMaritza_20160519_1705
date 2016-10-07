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
    public class CDAsistencia
    {
        public static void Asistencia_Ingresar(SqlConnection conn, Asistencia obj_asistencia)
        {
            try
            {
                using (SqlCommand cmd = new SqlCommand("sp_ins_asistencia_ingreso_manual", conn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add("@dni", SqlDbType.VarChar, 15).Value = obj_asistencia.dni;
                    cmd.Parameters.Add("@diaAsistencia", SqlDbType.DateTime).Value = obj_asistencia.diaAsistencia;
                    cmd.Parameters.Add("@hora", SqlDbType.DateTime).Value = obj_asistencia.hora;
                    cmd.Parameters.Add("@msg", SqlDbType.Char, 55).Direction = ParameterDirection.Output;
                    cmd.ExecuteNonQuery();
                    obj_asistencia.msg = cmd.Parameters["@msg"].Value.ToString();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public static string Asistencia_Consulta_Datos(SqlConnection conn, string dni)
        {
            string apellido = "";
            try
            {
                using (SqlCommand cmd = new SqlCommand("sp_sel_personal_apellidos", conn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add("@dni", SqlDbType.VarChar, 15).Value = dni;
                    SqlDataReader dr_reesult = cmd.ExecuteReader();
                    if (dr_reesult.HasRows)
                    {
                        dr_reesult.Read();
                        apellido = dr_reesult["Apellidos y Nombre"].ToString();
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return apellido;
        }
        public DataTable Asistencia_Listar_Diaria(DateTime diaAsis)
        {

            String procedure = "sp_sel_asistencia_diaria";
            try
            {
                SqlConnection oSqlConnection = new SqlConnection();
                AdministracionConexion oAdministracionConexion = new AdministracionConexion();
                oSqlConnection = oAdministracionConexion.getConexion();
                SqlCommand oSqlCommand = new SqlCommand(procedure, oSqlConnection);
                oSqlCommand.CommandType = CommandType.StoredProcedure;
                oSqlCommand.Parameters.Add("@diaAsistencia", SqlDbType.DateTime).Value = diaAsis;
                oSqlCommand.ExecuteNonQuery();
                SqlDataAdapter oSqlDataAdapter = new SqlDataAdapter(oSqlCommand);
                DataTable oDataTable = new DataTable();
                oSqlDataAdapter.Fill(oDataTable);
                return oDataTable;

            }
            catch (Exception e)
            {
                e.ToString();
                return null;
            }
        }
        public DataTable Asistencia_Listar_PorFecha(string obj_desde, string obj_hasta)
        {

            String procedure = "sp_sel_asistencia_filtrar_fecha";
            try
            {
                SqlConnection oSqlConnection = new SqlConnection();
                AdministracionConexion oAdministracionConexion = new AdministracionConexion();
                oSqlConnection = oAdministracionConexion.getConexion();
                SqlCommand oSqlCommand = new SqlCommand(procedure, oSqlConnection);
                oSqlCommand.CommandType = CommandType.StoredProcedure;
                oSqlCommand.Parameters.Add("@desde", SqlDbType.Char, 10).Value = obj_desde;
                oSqlCommand.Parameters.Add("@hasta", SqlDbType.Char, 10).Value = obj_hasta;
                oSqlCommand.ExecuteNonQuery();
                SqlDataAdapter oSqlDataAdapter = new SqlDataAdapter(oSqlCommand);
                DataTable oDataTable = new DataTable();
                oSqlDataAdapter.Fill(oDataTable);
                return oDataTable;

            }
            catch (Exception e)
            {
                e.ToString();
                return null;
            }
        }
        public DataTable Asistencia_Listar_PorHorarioySucursal(string obj_descripcion, string obj_nombre)
        {

            String procedure = "sp_sel_asistencia_filtrar_horario_sucursal";
            try
            {
                SqlConnection oSqlConnection = new SqlConnection();
                AdministracionConexion oAdministracionConexion = new AdministracionConexion();
                oSqlConnection = oAdministracionConexion.getConexion();
                SqlCommand oSqlCommand = new SqlCommand(procedure, oSqlConnection);
                oSqlCommand.CommandType = CommandType.StoredProcedure;
                oSqlCommand.Parameters.Add("@descripcionHorario", SqlDbType.VarChar, 40).Value = obj_descripcion;
                oSqlCommand.Parameters.Add("@nombreSucursal", SqlDbType.VarChar, 50).Value = obj_nombre;
                oSqlCommand.ExecuteNonQuery();
                SqlDataAdapter oSqlDataAdapter = new SqlDataAdapter(oSqlCommand);
                DataTable oDataTable = new DataTable();
                oSqlDataAdapter.Fill(oDataTable);
                return oDataTable;

            }
            catch (Exception e)
            {
                e.ToString();
                return null;
            }
        }
        public DataTable Asistencia_Listar_PorPersonal(string obj_apellidos, string obj_cargo)
        {

            String procedure = "sp_sel_asistencia_filtrar_personal";
            try
            {
                SqlConnection oSqlConnection = new SqlConnection();
                AdministracionConexion oAdministracionConexion = new AdministracionConexion();
                oSqlConnection = oAdministracionConexion.getConexion();
                SqlCommand oSqlCommand = new SqlCommand(procedure, oSqlConnection);
                oSqlCommand.CommandType = CommandType.StoredProcedure;
                oSqlCommand.Parameters.Add("@apellidosynombre", SqlDbType.VarChar, 100).Value = obj_apellidos;
                oSqlCommand.Parameters.Add("@cargo", SqlDbType.VarChar, 30).Value = obj_cargo;
                oSqlCommand.ExecuteNonQuery();
                SqlDataAdapter oSqlDataAdapter = new SqlDataAdapter(oSqlCommand);
                DataTable oDataTable = new DataTable();
                oSqlDataAdapter.Fill(oDataTable);
                return oDataTable;

            }
            catch (Exception e)
            {
                e.ToString();
                return null;
            }
        }
        public DataTable Asistencia_Listar_PorFechayPersonal(string obj_desde, string obj_hasta, string obj_apellidos, string obj_cargo)
        {

            String procedure = "sp_sel_asistencia_PorFechayPersonal";
            try
            {
                SqlConnection oSqlConnection = new SqlConnection();
                AdministracionConexion oAdministracionConexion = new AdministracionConexion();
                oSqlConnection = oAdministracionConexion.getConexion();
                SqlCommand oSqlCommand = new SqlCommand(procedure, oSqlConnection);
                oSqlCommand.CommandType = CommandType.StoredProcedure;
                oSqlCommand.Parameters.Add("@desde", SqlDbType.Char, 10).Value = obj_desde;
                oSqlCommand.Parameters.Add("@hasta", SqlDbType.Char, 10).Value = obj_hasta;
                oSqlCommand.Parameters.Add("@apellidosynombre", SqlDbType.VarChar, 100).Value = obj_apellidos;
                oSqlCommand.Parameters.Add("@cargo", SqlDbType.VarChar, 30).Value = obj_cargo;
                oSqlCommand.ExecuteNonQuery();
                SqlDataAdapter oSqlDataAdapter = new SqlDataAdapter(oSqlCommand);
                DataTable oDataTable = new DataTable();
                oSqlDataAdapter.Fill(oDataTable);
                return oDataTable;

            }
            catch (Exception e)
            {
                e.ToString();
                return null;
            }
        }
        public DataTable Asistencia_Listar_PorFechayHorario(string obj_desde, string obj_hasta, string obj_descripcion, string obj_nombre)
        {

            String procedure = "sp_sel_asistencia_PorFechayHorario";
            try
            {
                SqlConnection oSqlConnection = new SqlConnection();
                AdministracionConexion oAdministracionConexion = new AdministracionConexion();
                oSqlConnection = oAdministracionConexion.getConexion();
                SqlCommand oSqlCommand = new SqlCommand(procedure, oSqlConnection);
                oSqlCommand.CommandType = CommandType.StoredProcedure;
                oSqlCommand.Parameters.Add("@desde", SqlDbType.Char, 10).Value = obj_desde;
                oSqlCommand.Parameters.Add("@hasta", SqlDbType.Char, 10).Value = obj_hasta;
                oSqlCommand.Parameters.Add("@descripcionHorario", SqlDbType.VarChar, 40).Value = obj_descripcion;
                oSqlCommand.Parameters.Add("@nombreSucursal", SqlDbType.VarChar, 50).Value = obj_nombre;
                oSqlCommand.ExecuteNonQuery();
                SqlDataAdapter oSqlDataAdapter = new SqlDataAdapter(oSqlCommand);
                DataTable oDataTable = new DataTable();
                oSqlDataAdapter.Fill(oDataTable);
                return oDataTable;

            }
            catch (Exception e)
            {
                e.ToString();
                return null;
            }
        }
        public DataTable Asistencia_Listar_PorHorarioyPersonal(string obj_descripcion, string obj_nombre, string obj_apellidos, string obj_cargo)
        {

            String procedure = "sp_sel_asistencia_PorHorarioyPersonal";
            try
            {
                SqlConnection oSqlConnection = new SqlConnection();
                AdministracionConexion oAdministracionConexion = new AdministracionConexion();
                oSqlConnection = oAdministracionConexion.getConexion();
                SqlCommand oSqlCommand = new SqlCommand(procedure, oSqlConnection);
                oSqlCommand.CommandType = CommandType.StoredProcedure;
                oSqlCommand.Parameters.Add("@descripcionHorario", SqlDbType.VarChar, 40).Value = obj_descripcion;
                oSqlCommand.Parameters.Add("@nombreSucursal", SqlDbType.VarChar, 50).Value = obj_nombre;
                oSqlCommand.Parameters.Add("@apellidosynombre", SqlDbType.VarChar, 100).Value = obj_apellidos;
                oSqlCommand.Parameters.Add("@cargo", SqlDbType.VarChar, 30).Value = obj_cargo;
                oSqlCommand.ExecuteNonQuery();
                SqlDataAdapter oSqlDataAdapter = new SqlDataAdapter(oSqlCommand);
                DataTable oDataTable = new DataTable();
                oSqlDataAdapter.Fill(oDataTable);
                return oDataTable;

            }
            catch (Exception e)
            {
                e.ToString();
                return null;
            }
        }
        public DataTable Asistencia_Listar_PorFecha_HorarioyPersonal(string obj_desde, string obj_hasta, string obj_descripcion, string obj_nombre, string obj_apellidos, string obj_cargo)
        {

            String procedure = "sp_sel_asistencia_PorFecha_HorarioyPersonal";
            try
            {
                SqlConnection oSqlConnection = new SqlConnection();
                AdministracionConexion oAdministracionConexion = new AdministracionConexion();
                oSqlConnection = oAdministracionConexion.getConexion();
                SqlCommand oSqlCommand = new SqlCommand(procedure, oSqlConnection);
                oSqlCommand.CommandType = CommandType.StoredProcedure;
                oSqlCommand.Parameters.Add("@desde", SqlDbType.Char, 10).Value = obj_desde;
                oSqlCommand.Parameters.Add("@hasta", SqlDbType.Char, 10).Value = obj_hasta;
                oSqlCommand.Parameters.Add("@descripcionHorario", SqlDbType.VarChar, 40).Value = obj_descripcion;
                oSqlCommand.Parameters.Add("@nombreSucursal", SqlDbType.VarChar, 50).Value = obj_nombre;
                oSqlCommand.Parameters.Add("@apellidosynombre", SqlDbType.VarChar, 100).Value = obj_apellidos;
                oSqlCommand.Parameters.Add("@cargo", SqlDbType.VarChar, 30).Value = obj_cargo;
                oSqlCommand.ExecuteNonQuery();
                SqlDataAdapter oSqlDataAdapter = new SqlDataAdapter(oSqlCommand);
                DataTable oDataTable = new DataTable();
                oSqlDataAdapter.Fill(oDataTable);
                return oDataTable;

            }
            catch (Exception e)
            {
                e.ToString();
                return null;
            }
        }
    }
}
