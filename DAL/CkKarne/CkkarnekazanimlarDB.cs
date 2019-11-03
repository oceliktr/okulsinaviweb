using System;
using System.Collections.Generic;
using System.Data;
using DAL;
using MySql.Data.MySqlClient;
public class CkKarneKazanimlarInfo
{
    public int Id { get; set; }
    public int SinavId { get; set; }
    public int Sinif { get; set; }
    public int BransId { get; set; }
    public string KazanimNo { get; set; }
    public string KazanimAdi { get; set; }
    public string KazanimAdiOgrenci { get; set; }
    public string Sorulari { get; set; }

    public CkKarneKazanimlarInfo()
    {
        
    }

    public CkKarneKazanimlarInfo(int id, int sinavId, int sinif, int bransId, string kazanimNo, string kazanimAdi, string kazanimAdiOgrenci, string sorulari)
    {
        Id = id;
        SinavId = sinavId;
        Sinif = sinif;
        BransId = bransId;
        KazanimNo = kazanimNo;
        KazanimAdi = kazanimAdi;
        KazanimAdiOgrenci = kazanimAdiOgrenci;
        Sorulari = sorulari;
    }
}
public class CkKarneKazanimlardB
{
    readonly HelperDb helper = new HelperDb();
    public DataTable KayitlariGetir()
    {
        const string sql = "select * from ckkarnekazanimlar order by Id asc";
        return helper.ExecuteDataSet(sql).Tables[0];
    }
    public List<CkKarneKazanimlarInfo> KayitlariDizeGetir(int sinavId)
    {
        string sql = "select * from ckkarnekazanimlar where SinavId=?SinavId";
        MySqlParameter[] p =
        {
            new MySqlParameter("?SinavId", MySqlDbType.Int32)
        };
        p[0].Value = sinavId;

        DataTable dt = helper.ExecuteDataSet(sql, p).Tables[0];
        List<CkKarneKazanimlarInfo> karne = new List<CkKarneKazanimlarInfo>();
        foreach (DataRow k in dt.Rows)
        {
            karne.Add(new CkKarneKazanimlarInfo(Convert.ToInt32(k["Id"]), Convert.ToInt32(k["SinavId"]), Convert.ToInt32(k["Sinif"]), Convert.ToInt32(k["BransId"]),
                 k["KazanimNo"].ToString(), k["KazanimAdi"].ToString(), k["KazanimAdiOgrenci"].ToString(), k["Sorulari"].ToString()));
        }
        return karne;
    }
    public CkKarneKazanimlarInfo KayitBilgiGetir(string cmdText, params MySqlParameter[] param)
    {
        MySqlDataReader dr = helper.ExecuteReader(cmdText, param);
        CkKarneKazanimlarInfo info = new CkKarneKazanimlarInfo();
        while (dr.Read())
        {
            info.Id = dr.GetMySayi("Id");
            info.SinavId = dr.GetMySayi("SinavId");
            info.Sinif = dr.GetMySayi("Sinif");
            info.BransId = dr.GetMySayi("BransId");
            info.KazanimNo = dr.GetMyMetin("KazanimNo");
            info.KazanimAdi = dr.GetMyMetin("KazanimAdi");
            info.KazanimAdiOgrenci = dr.GetMyMetin("KazanimAdiOgrenci");
            info.Sorulari = dr.GetMyMetin("Sorulari");
        }
        dr.Close();

        return info;
    }

    public CkKarneKazanimlarInfo KayitBilgiGetir(int id)
    {
        string cmdText = "select * from ckkarnekazanimlar where Id=?Id";
        MySqlParameter param = new MySqlParameter("?Id", MySqlDbType.Int32) { Value = id };
        MySqlDataReader dr = helper.ExecuteReader(cmdText, param);
        CkKarneKazanimlarInfo info = new CkKarneKazanimlarInfo();
        while (dr.Read())
        {
            info.Id = dr.GetMySayi("Id");
            info.SinavId = dr.GetMySayi("SinavId");
            info.Sinif = dr.GetMySayi("Sinif");
            info.BransId = dr.GetMySayi("BransId");
            info.KazanimNo = dr.GetMyMetin("KazanimNo");
            info.KazanimAdi = dr.GetMyMetin("KazanimAdi");
            info.KazanimAdiOgrenci = dr.GetMyMetin("KazanimAdiOgrenci");
            info.Sorulari = dr.GetMyMetin("Sorulari");
        }
        dr.Close();

        return info;
    }

    public void KayitSil(int id)
    {
        const string sql = "delete from ckkarnekazanimlar where Id=?Id";
        MySqlParameter p = new MySqlParameter("?Id", MySqlDbType.Int32) { Value = id };
        helper.ExecuteNonQuery(sql, p);
    }

    public void KayitEkle(CkKarneKazanimlarInfo info)
    {
        const string sql = @"insert into ckkarnekazanimlar (SinavId,Sinif,BransId,KazanimNo,KazanimAdi,KazanimAdiOgrenci,Sorulari) values (?SinavId,?Sinif,?BransId,?KazanimNo,?KazanimAdi,?KazanimAdiOgrenci,?Sorulari)";
        MySqlParameter[] pars =
        {
             new MySqlParameter("?SinavId", MySqlDbType.Int32),
             new MySqlParameter("?Sinif", MySqlDbType.Int32),
             new MySqlParameter("?BransId", MySqlDbType.Int32),
             new MySqlParameter("?KazanimNo", MySqlDbType.String),
             new MySqlParameter("?KazanimAdi", MySqlDbType.String),
             new MySqlParameter("?KazanimAdiOgrenci", MySqlDbType.String),
             new MySqlParameter("?Sorulari", MySqlDbType.String)
        };
        pars[0].Value = info.SinavId;
        pars[1].Value = info.Sinif;
        pars[2].Value = info.BransId;
        pars[3].Value = info.KazanimNo;
        pars[4].Value = info.KazanimAdi;
        pars[5].Value = info.KazanimAdiOgrenci;
        pars[6].Value = info.Sorulari;
        helper.ExecuteNonQuery(sql, pars);
    }

    public void KayitGuncelle(CkKarneKazanimlarInfo info)
    {
        const string sql = @"update ckkarnekazanimlar set SinavId=?SinavId,Sinif=?Sinif,BransId=?BransId,KazanimNo=?KazanimNo,KazanimAdi=?KazanimAdi,KazanimAdiOgrenci=?KazanimAdiOgrenci,Sorulari=?Sorulari where Id=?Id";
        MySqlParameter[] pars =
        {
            new MySqlParameter("?SinavId", MySqlDbType.Int32),
            new MySqlParameter("?Sinif", MySqlDbType.Int32),
            new MySqlParameter("?BransId", MySqlDbType.Int32),
            new MySqlParameter("?KazanimNo", MySqlDbType.String),
            new MySqlParameter("?KazanimAdi", MySqlDbType.String),
            new MySqlParameter("?KazanimAdiOgrenci", MySqlDbType.String),
            new MySqlParameter("?Sorulari", MySqlDbType.String),
            new MySqlParameter("?Id", MySqlDbType.Int32),
        };
        pars[0].Value = info.SinavId;
        pars[1].Value = info.Sinif;
        pars[2].Value = info.BransId;
        pars[3].Value = info.KazanimNo;
        pars[4].Value = info.KazanimAdi;
        pars[5].Value = info.KazanimAdiOgrenci;
        pars[6].Value = info.Sorulari;
        pars[7].Value = info.Id;
        helper.ExecuteNonQuery(sql, pars);
    }
}

