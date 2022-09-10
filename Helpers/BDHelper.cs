using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;

namespace Examen.Helpers
{
    public class BDHelper
    {
        protected SqlConnection con;
        protected SqlCommand cmd;
        protected SqlDataReader rd;
        public static string mensaje;

        public string CadenaConexion;

        public BDHelper()
        {
            CadenaConexion = ConfigurationManager.ConnectionStrings["ConexionVDV"].ToString();
            con = new SqlConnection(CadenaConexion);
        }
        public DataTable Select(string query)
        {
            DataTable obj = new DataTable();
            cmd = new SqlCommand();
            cmd.CommandText = query;
            cmd.Connection = con;

            try
            {
                if (con.State == ConnectionState.Closed) con.Open();
                rd = cmd.ExecuteReader();
                obj.Load(rd);
                return obj;
            }
            catch (SqlException ex)
            {
                mensaje = ex.Message;
            }
            catch (Exception ex)
            {
                mensaje = ex.Message;
            }
            if (con.State == ConnectionState.Open)
                con.Close();
            return null;
        }
        public bool EjecutaSP(string procedimientoAlmacenado, List<SqlParameter> parametros)
        {
            DataTable dt = new DataTable();
            con = new SqlConnection(CadenaConexion);
            cmd = new SqlCommand();
            cmd.Connection = con;
            cmd.CommandText = procedimientoAlmacenado;
            cmd.CommandType = CommandType.StoredProcedure;
           
            foreach (SqlParameter item in parametros)
                cmd.Parameters.Add(item);
            cmd.Connection = con;
            
            try
            {
                con.Open();
                cmd.ExecuteNonQuery();
                return true;
            }
            catch (SqlException e)
            { mensaje = e.Message; }
            finally
            {
                if (con.State == ConnectionState.Open)
                    con.Close();
            }
            return false;
        }
        public DataTable EjecutaSPQuery(string procedimientoAlmacenado, List<SqlParameter> parametros)
        {
            DataTable dt = new DataTable();
            con = new SqlConnection(CadenaConexion);
            cmd = new SqlCommand();
            cmd.Connection = con;
            cmd.CommandText = procedimientoAlmacenado;
            cmd.CommandType = CommandType.StoredProcedure;

            foreach (SqlParameter p in parametros)
                cmd.Parameters.Add(p);

            try
            {
                con.Open();
                rd = cmd.ExecuteReader();
                dt.Load(rd);
                return dt;
            }
            catch (SqlException e)
            { mensaje = e.Message; }
            finally
            {
                if (con.State == ConnectionState.Open)
                    con.Close();
            }
            return dt;
        }
    }
}