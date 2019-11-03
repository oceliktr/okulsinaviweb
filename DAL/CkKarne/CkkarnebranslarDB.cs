using System;
using System.Collections.Generic;
using System.Data;
using DAL;
using MySql.Data.MySqlClient;
public class CkKarneBranslarInfo
{
    public int Id { get; set; }
    public int SinavId { get; set; }
    public int BransId { get; set; }
    public string BransAdi { get; set; }

    public CkKarneBranslarInfo()
    {
        
    }

    public CkKarneBranslarInfo(int id, int sinavId, int bransId, string bransAdi)
    {
        Id = id;
        SinavId = sinavId;
        BransId = bransId;
        BransAdi = bransAdi;
    }
}

public class CkKarneBranslarDB
{
    readonly HelperDb helper = new HelperDb();

    public DataTable KayitlariGetir()
    {
        const string sql = "select * from ckkarnebranslar order by Id asc";
        return helper.ExecuteDataSet(sql).Tables[0];
    }
    public List<CkKarneBranslarInfo> KayitlariDizeGetir(int sinavId)
    {
        string sql = "select * from ckkarnebranslar where SinavId=?SinavId";
        MySqlParameter[] p =
        {
            new MySqlParameter("?SinavId", MySqlDbType.Int32)
        };
        p[0].Value = sinavId;

        DataTable dt = helper.ExecuteDataSet(sql, p).Tables[0];
        List<CkKarneBranslarInfo> karne = new List<CkKarneBranslarInfo>();
        foreach (DataRow k in dt.Rows)
        {
            karne.Add(new CkKarneBranslarInfo(Convert.ToInt32(k["Id"]), Convert.ToInt32(k["SinavId"]), Convert.ToInt32(k["BransId"]), k["BransAdi"].ToString()));
        }
        return karne;
    }
    public CkKarneBranslarInfo KayitBilgiGetir(string cmdText, params MySqlParameter[] param)
    {
        MySqlDataReader dr = helper.ExecuteReader(cmdText, param);
        CkKarneBranslarInfo info = new CkKarneBranslarInfo();
        while (dr.Read())
        {
            info.Id = dr.GetMySayi("Id");
            info.SinavId = dr.GetMySayi("SinavId");
            info.BransId = dr.GetMySayi("BransId");
            info.BransAdi = dr.GetMyMetin("BransAdi");
        }
        dr.Close();

        return info;
    }

    public CkKarneBranslarInfo KayitBilgiGetir(int id)
    {
        string cmdText = "select * from ckkarnebranslar where Id=?Id";
        MySqlParameter param = new MySqlParameter("?Id", MySqlDbType.Int32) { Value = id };
        MySqlDataReader dr = helper.ExecuteReader(cmdText, param);
        CkKarneBranslarInfo info = new CkKarneBranslarInfo();
        while (dr.Read())
        {
            info.Id = dr.GetMySayi("Id");
            info.SinavId = dr.GetMySayi("SinavId");
            info.BransId = dr.GetMySayi("BransId");
            info.BransAdi = dr.GetMyMetin("BransAdi");
        }
        dr.Close();

        return info;
    }

    public void KayitSil(int id)
    {
        const string sql = "delete from ckkarnebranslar where Id=?Id";
        MySqlParameter p = new MySqlParameter("?Id", MySqlDbType.Int32) { Value = id };
        helper.ExecuteNonQuery(sql, p);
    }

    public void KayitEkle(CkKarneBranslarInfo info)
    {
        const string sql = @"insert into ckkarnebranslar (SinavId,BransId,BransAdi) values (?SinavId,?BransId,?BransAdi)";
        MySqlParameter[] pars =
        {
         new MySqlParameter("?SinavId", MySqlDbType.Int32),
         new MySqlParameter("?BransId", MySqlDbType.Int32),
         new MySqlParameter("?BransAdi", MySqlDbType.String),
        };
        pars[0].Value = info.SinavId;
        pars[1].Value = info.BransId;
        pars[2].Value = info.BransAdi;
        helper.ExecuteNonQuery(sql, pars);
    }

    public void KayitGuncelle(CkKarneBranslarInfo info)
    {
        const string sql = @"update ckkarnebranslar set SinavId=?SinavId,BransId=?BransId,BransAdi=?BransAdi where Id=?Id";
        MySqlParameter[] pars =
        {
            new MySqlParameter("?SinavId", MySqlDbType.Int32),
            new MySqlParameter("?BransId", MySqlDbType.Int32),
            new MySqlParameter("?BransAdi", MySqlDbType.String),
         new MySqlParameter("?Id", MySqlDbType.Int32)
        };
        pars[0].Value = info.SinavId;
        pars[1].Value = info.BransId;
        pars[2].Value = info.BransAdi;
        pars[3].Value = info.Id;
        helper.ExecuteNonQuery(sql, pars);
    }
}

