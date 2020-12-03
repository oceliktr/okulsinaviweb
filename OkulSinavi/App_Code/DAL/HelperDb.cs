using System;
using System.Configuration;
using System.Data;
using MySql.Data.MySqlClient;

//Versiyon 2015
public class HelperDb
{
    private readonly string ConnStr = ConfigurationManager.ConnectionStrings["ConnStr"].ToString();

    public int MyExecuteNonQuery(string sql, params MySqlParameter[] pars)
    {
        MySqlConnection conn = new MySqlConnection(ConnStr);
        MySqlCommand cmd = new MySqlCommand(sql, conn);
        ParametreEkle(pars, cmd);
        conn.Open();
        int s = cmd.ExecuteNonQuery();
        conn.Close();
        cmd.Dispose();
        conn.Dispose();

        return s;
    }
    private void ParametreEkle(MySqlParameter[] pars, MySqlCommand cmd)
    {
        foreach (MySqlParameter p in pars)
        {
            cmd.Parameters.Add(p);
        }
    }
    public int ExecuteNonQuery(string cmdText, params MySqlParameter[] param)
    {
        MySqlConnection conn = new MySqlConnection(ConnStr);
        MySqlCommand cmd = null;
        try
        {
            cmd = new MySqlCommand { Connection = conn };
            conn.Open();
            cmd.CommandText = cmdText;
            SqlCommandParam(cmd, param);

            return cmd.ExecuteNonQuery();
        }
        finally
        {
            if (conn.State != ConnectionState.Closed)
                conn.Close();
            cmd.Dispose();
            conn.Dispose();
        }
    }
    public int ExecuteNonQuery(out long sonId, string cmdText, params MySqlParameter[] param)
    {
        MySqlConnection conn = new MySqlConnection(ConnStr);
        MySqlCommand cmd = null;
        try
        {
            cmd = new MySqlCommand { Connection = conn };
            conn.Open();
            cmd.CommandText = cmdText;
            SqlCommandParam(cmd, param);

            int x = cmd.ExecuteNonQuery();
            sonId = 0;
            return x;
        }
        finally
        {
            if (conn.State != ConnectionState.Closed)
                conn.Close();
            cmd.Dispose();
            conn.Dispose();
        }
    }
    public MySqlDataReader ExecuteReader(string cmdText, params MySqlParameter[] param)
    {
        MySqlConnection conn = new MySqlConnection(ConnStr);
        conn.StateChange += conn_StateChange;
        MySqlCommand cmd = null;
        try
        {
            cmd = new MySqlCommand(cmdText, conn);

            SqlCommandParam(cmd, param);
            conn.Open();
            return cmd.ExecuteReader(CommandBehavior.CloseConnection);
        }
        catch (Exception ex)
        {
            if (conn.State != ConnectionState.Closed)
                conn.Close();
            throw ex;
        }
        finally
        {
            cmd.Dispose();
        }
    }
    ///<summary>
    ///Kayıt var mı yok mu varsa True yoksa False değer döndürür
    ///</summary>
    ///<param name="cmdText">Sql cümlesi</param>
    ///<param name="param">Parametreler</param>
    public bool KayitKontrol(string cmdText, params MySqlParameter[] param)
    {
        MySqlConnection conn = new MySqlConnection(ConnStr);
        conn.StateChange += new StateChangeEventHandler(conn_StateChange);
        MySqlCommand cmd = null;
        try
        {
            cmd = new MySqlCommand(cmdText, conn);
            SqlCommandParam(cmd, param);
            conn.Open();
            return cmd.ExecuteReader(CommandBehavior.CloseConnection).HasRows;
        }
        catch (Exception ex)
        {
            if (conn.State != ConnectionState.Closed)
                conn.Close();
            throw ex;
        }
        finally
        {
            if (conn.State != ConnectionState.Closed)
                conn.Close();
            cmd.Dispose();
            conn.Dispose();
        }
    }
    void conn_StateChange(object sender, StateChangeEventArgs e)
    {
        if (e.CurrentState == ConnectionState.Closed)
        {
            MySqlConnection conn = sender as MySqlConnection;
            conn.Dispose();
        }
    }
    public DataSet ExecuteDataSet(string cmdText, params MySqlParameter[] param)
    {
        MySqlConnection conn = new MySqlConnection(ConnStr);
        MySqlCommand cmd = null;
        MySqlDataAdapter da = null;
        try
        {
            cmd = new MySqlCommand(cmdText, conn);
            da = new MySqlDataAdapter(cmd);
            DataSet ds = new DataSet();
            SqlCommandParam(cmd, param);
            conn.Open();
            da.Fill(ds);
            return ds;
        }
        catch (Exception ex)
        {
            if (conn.State != ConnectionState.Closed)
                conn.Close();
            throw ex;
        }
        finally
        {
            if (conn.State != ConnectionState.Closed)
                conn.Close();
            da.Dispose();
            cmd.Dispose();
            conn.Dispose();
        }
    }
    public DataRow ExecuteDataRow(string cmdText, params MySqlParameter[] param)
    {
        MySqlConnection conn = new MySqlConnection(ConnStr);
        MySqlCommand cmd = null;
        MySqlDataAdapter da = null;
        try
        {
            cmd = new MySqlCommand(cmdText, conn);
            da = new MySqlDataAdapter(cmd);
            DataSet ds = new DataSet();
            SqlCommandParam(cmd, param);
            conn.Open();
            da.Fill(ds);
            return ds.Tables[0].Rows[0];
        }
        catch (Exception ex)
        {
            if (conn.State != ConnectionState.Closed)
                conn.Close();
            throw ex;
        }
        finally
        {
            if (conn.State != ConnectionState.Closed)
                conn.Close();
            da.Dispose();
            cmd.Dispose();
            conn.Dispose();
        }
    }
    public object ExecuteScalar(string cmdText, params MySqlParameter[] param)
    {
        MySqlConnection conn = new MySqlConnection(ConnStr);
        MySqlCommand cmd = null;
        try
        {
            cmd = new MySqlCommand(cmdText, conn);
            SqlCommandParam(cmd, param);
            conn.Open();
            return cmd.ExecuteScalar();
        }
        finally
        {
            if (conn.State != ConnectionState.Closed)
                conn.Close();
            cmd.Dispose();
            conn.Dispose();
        }
    }
    private void SqlCommandParam(MySqlCommand cmd, params MySqlParameter[] param)
    {
        for (int i = 0; i < param.Length; i++)
        {
            cmd.Parameters.Add(param[i]);
        }
    }
}