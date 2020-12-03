using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;

/// <summary>
/// Summary description for TestlerDb
/// </summary>
public class TestSorularDb
{
    private readonly HelperDb helper = new HelperDb();

    public DataTable KayitlariGetir()
    {
        const string sql = "select * from testsorular order by Id asc";
        return helper.ExecuteDataSet(sql).Tables[0];
    }
    public DataTable KayitlariGetir(int oturumId)
    {
        string sql = @"SELECT b.BransAdi,t.* from testsorular AS t 
                       LEFT JOIN branslar AS b ON b.Id=t.BransId
                       WHERE t.OturumId=?OturumId Order By t.SoruNo asc";
        MySqlParameter p = new MySqlParameter("?OturumId", MySqlDbType.Int32) {Value = oturumId};
        return helper.ExecuteDataSet(sql,p).Tables[0];
    }
    public List<TestSorularInfo> KayitlariDizeGetir(int oturumId)
    {
        string sql = "select * from testsorular where OturumId=?OturumId Order By SoruNo";
        MySqlParameter p = new MySqlParameter("?OturumId", MySqlDbType.Int32) { Value = oturumId };

        DataTable dt = helper.ExecuteDataSet(sql, p).Tables[0];
        List<TestSorularInfo> karne = new List<TestSorularInfo>();
        foreach (DataRow k in dt.Rows)
        {
            karne.Add(new TestSorularInfo(Convert.ToInt32(k["Id"].ToString()), Convert.ToInt32(k["OturumId"].ToString()), Convert.ToInt32(k["BransId"].ToString()),  Convert.ToInt32(k["SoruNo"].ToString()), k["Soru"].ToString(),k["Cevap"].ToString()));
        }
        return karne;
    }

    private static TestSorularInfo TabloAlanlar(MySqlDataReader dr)
    {
        TestSorularInfo info = new TestSorularInfo();
        while (dr.Read())
        {
            info.Id = dr.GetMySayi("Id");
            info.OturumId = dr.GetMySayi("OturumId");
            info.BransId = dr.GetMySayi("BransId");
            info.SoruNo = dr.GetMySayi("SoruNo");
            info.Soru = dr.GetMyMetin("Soru");
            info.Cevap = dr.GetMyMetin("Cevap");
        }

        dr.Close();
        return info;
    }
    public TestSorularInfo KayitBilgiGetir(int id)
    {
        string cmdText = "select * from testsorular where Id=?Id";
        MySqlParameter p = new MySqlParameter("?Id", MySqlDbType.Int32) { Value = id };

        MySqlDataReader dr = helper.ExecuteReader(cmdText, p);
        var info = TabloAlanlar(dr);

        return info;
    }
    public bool KayitKontrol(int oturumId, int soru,int id)
    {
        string cmdText = "select OturumId,SoruNo,BransId from testsorular where OturumId=?OturumId and SoruNo=?SoruNo and Id<>?Id";
        MySqlParameter[] pars =
        {
            new MySqlParameter("?OturumId", MySqlDbType.Int32),
            new MySqlParameter("?SoruNo", MySqlDbType.Int32),
            new MySqlParameter("?Id", MySqlDbType.Int32)
        };
        pars[0].Value = oturumId;
        pars[1].Value = soru;
        pars[2].Value = id;
        bool sonuc = Convert.ToInt32(helper.ExecuteScalar(cmdText, pars)) > 0;
        return sonuc;
    }
    public TestSorularInfo KayitBilgiGetir(int oturumId, int soru)
    {
        string cmdText = "select * from testsorular where OturumId=?OturumId and SoruNo=?SoruNo";
        MySqlParameter[] pars =
        {
            new MySqlParameter("?OturumId", MySqlDbType.Int32),
            new MySqlParameter("?SoruNo", MySqlDbType.Int32)
        };
        pars[0].Value = oturumId;
        pars[1].Value = soru;

        MySqlDataReader dr = helper.ExecuteReader(cmdText, pars);
        var info = TabloAlanlar(dr);

        return info;
    }
    public int IlkSoruyuGetir(int oturumId, int bransId)
    {
        string cmdText = "select SoruNo from testsorular where OturumId=?OturumId and BransId=?BransId ORDER BY SoruNo ASC LIMIT 1";
        MySqlParameter[] pars =
        {
            new MySqlParameter("?OturumId", MySqlDbType.Int32),
            new MySqlParameter("?BransId", MySqlDbType.Int32)
        };
        pars[0].Value = oturumId;
        pars[1].Value = bransId;

        MySqlDataReader dr = helper.ExecuteReader(cmdText, pars);
        var info = TabloAlanlar(dr);

        return info.SoruNo;
    }

    public int KayitSil(int id)
    {
        const string sql = "delete from testsorular where Id=?Id";
        MySqlParameter p = new MySqlParameter("?Id", MySqlDbType.Int32) { Value = id };
     return   helper.ExecuteNonQuery(sql, p);
    }
    public int OturumSorulariniSil(int oturumId)
    {
        const string sql = "delete from testsorular where OturumId=?OturumId";
        MySqlParameter p = new MySqlParameter("?OturumId", MySqlDbType.Int32) { Value = oturumId };
        return helper.ExecuteNonQuery(sql, p);
    }

    public int KayitEkle(TestSorularInfo info)
    {
        const string sql = @"insert into testsorular (OturumId,BransId,SoruNo,Soru,Cevap) values (?OturumId,?BransId,?SoruNo,?Soru,?Cevap)";
        MySqlParameter[] pars =
        {
                new MySqlParameter("?OturumId", MySqlDbType.Int32),
                new MySqlParameter("?BransId", MySqlDbType.Int32),
                new MySqlParameter("?SoruNo", MySqlDbType.Int32),
                new MySqlParameter("?Soru", MySqlDbType.String),
                new MySqlParameter("?Cevap", MySqlDbType.String),
            };
        pars[0].Value = info.OturumId;
        pars[1].Value = info.BransId;
        pars[2].Value = info.SoruNo;
        pars[3].Value = info.Soru;
        pars[4].Value = info.Cevap;
       return helper.ExecuteNonQuery(sql, pars);
    }

    public int KayitGuncelle(TestSorularInfo info)
    {
        const string sql = @"update testsorular set OturumId=?OturumId,BransId=?BransId,SoruNo=?SoruNo,Soru=?Soru,Cevap=?Cevap where Id=?Id";
        MySqlParameter[] pars =
        {
            new MySqlParameter("?OturumId", MySqlDbType.Int32),
            new MySqlParameter("?BransId", MySqlDbType.Int32),
            new MySqlParameter("?SoruNo", MySqlDbType.Int32),
            new MySqlParameter("?Soru", MySqlDbType.String),
            new MySqlParameter("?Cevap", MySqlDbType.String),
                new MySqlParameter("?Id", MySqlDbType.Int32)
            };
        pars[0].Value = info.OturumId;
        pars[1].Value = info.BransId;
        pars[2].Value = info.SoruNo;
        pars[3].Value = info.Soru;
        pars[4].Value = info.Cevap;
        pars[5].Value = info.Id;
      return  helper.ExecuteNonQuery(sql, pars);
    }

    public int SoruSayisi(int oturumId)
    {
        string cmdText = "select count(Id) from testsorular where OturumId=?Id";
        MySqlParameter[] pars =
        {
            new MySqlParameter("?Id", MySqlDbType.Int32)
        };
        pars[0].Value = oturumId;
        int sonuc = Convert.ToInt32(helper.ExecuteScalar(cmdText, pars));
        return sonuc;
    }
    public int SoruSayisi(int oturumId, int bransId)
    {
        string cmdText = "select count(Id) from testsorular where OturumId=?Id and BransId=?BransId";
        MySqlParameter[] pars =
        {
            new MySqlParameter("?Id", MySqlDbType.Int32),
            new MySqlParameter("?BransId", MySqlDbType.Int32)
        };
        pars[0].Value = oturumId;
        pars[1].Value = bransId;
        int sonuc = Convert.ToInt32(helper.ExecuteScalar(cmdText, pars));
        return sonuc;
    }
}