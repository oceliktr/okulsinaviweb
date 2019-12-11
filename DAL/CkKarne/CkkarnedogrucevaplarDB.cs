using System;
using System.Collections.Generic;
using System.Data;
using DAL;
using MySql.Data.MySqlClient;
public class CkKarneDogruCevaplarInfo
{
    public int Id { get; set; }
    public int SinavId { get; set; }
    public int Sinif { get; set; }
    public int BransId { get; set; }
    public string KitapcikTuru { get; set; }
    public string Cevaplar { get; set; }

    public CkKarneDogruCevaplarInfo()
    {
        
    }

    public CkKarneDogruCevaplarInfo(int id, int sinavId,int sinif, int bransId, string kitapcikTuru, string cevaplar)
    {
        Id = id;
        SinavId = sinavId;
        Sinif = sinif;
        BransId = bransId;
        KitapcikTuru = kitapcikTuru;
        Cevaplar = cevaplar;
    }
}

public class CkKarneDogruCevaplarDB
{
    readonly HelperDb helper = new HelperDb();

    public DataTable KayitlariGetir()
    {
        const string sql = "select * from ckkarnedogrucevaplar order by Id asc";
        return helper.ExecuteDataSet(sql).Tables[0];
    }
    public List<CkKarneDogruCevaplarInfo> KayitlariDizeGetir(int sinavId)
    {
        string sql = "select * from ckkarnedogrucevaplar where SinavId=?SinavId";
        MySqlParameter[] p =
        {
            new MySqlParameter("?SinavId", MySqlDbType.Int32)
        };
        p[0].Value = sinavId;

        DataTable dt = helper.ExecuteDataSet(sql, p).Tables[0];
        List<CkKarneDogruCevaplarInfo> karne = new List<CkKarneDogruCevaplarInfo>();
        foreach (DataRow k in dt.Rows)
        {
            karne.Add(new CkKarneDogruCevaplarInfo(Convert.ToInt32(k["Id"]), Convert.ToInt32(k["SinavId"]), Convert.ToInt32(k["Sinif"]), Convert.ToInt32(k["BransId"]), k["KitapcikTuru"].ToString(), k["Cevaplar"].ToString()));
        }
        return karne;
    }
    public CkKarneDogruCevaplarInfo KayitBilgiGetir(string cmdText, params MySqlParameter[] param)
    {
        MySqlDataReader dr = helper.ExecuteReader(cmdText, param);
        CkKarneDogruCevaplarInfo info = new CkKarneDogruCevaplarInfo();
        while (dr.Read())
        {
            info.Id = dr.GetMySayi("Id");
            info.SinavId = dr.GetMySayi("SinavId");
            info.Sinif = dr.GetMySayi("Sinif");
            info.BransId = dr.GetMySayi("BransId");
            info.KitapcikTuru = dr.GetMyMetin("KitapcikTuru");
            info.Cevaplar = dr.GetMyMetin("Cevaplar");
        }
        dr.Close();

        return info;
    }

    public CkKarneDogruCevaplarInfo KayitBilgiGetir(int id)
    {
        string cmdText = "select * from ckkarnedogrucevaplar where Id=?Id";
        MySqlParameter param = new MySqlParameter("?Id", MySqlDbType.Int32) { Value = id };
        MySqlDataReader dr = helper.ExecuteReader(cmdText, param);
        CkKarneDogruCevaplarInfo info = new CkKarneDogruCevaplarInfo();
        while (dr.Read())
        {
            info.Id = dr.GetMySayi("Id");
            info.SinavId = dr.GetMySayi("SinavId");
            info.Sinif = dr.GetMySayi("Sinif");
            info.BransId = dr.GetMySayi("BransId");
            info.KitapcikTuru = dr.GetMyMetin("KitapcikTuru");
            info.Cevaplar = dr.GetMyMetin("Cevaplar");
        }
        dr.Close();

        return info;
    }

    public void KayitSil(int id)
    {
        const string sql = "delete from ckkarnedogrucevaplar where Id=?Id";
        MySqlParameter p = new MySqlParameter("?Id", MySqlDbType.Int32) { Value = id };
        helper.ExecuteNonQuery(sql, p);
    }
    public void SinaviSil(int sinavId)
    {
        const string sql = "delete from ckkarnedogrucevaplar where SinavId=?SinavId";
        MySqlParameter p = new MySqlParameter("?SinavId", MySqlDbType.Int32) { Value = sinavId };
        helper.ExecuteNonQuery(sql, p);
    }
    public void KayitEkle(CkKarneDogruCevaplarInfo info)
    {
        const string sql = @"insert into ckkarnedogrucevaplar (SinavId,Sinif,BransId,KitapcikTuru,Cevaplar) values (?SinavId,?Sinif,?BransId,?KitapcikTuru,?Cevaplar)";
        MySqlParameter[] pars =
        {
 new MySqlParameter("?SinavId", MySqlDbType.Int32),
 new MySqlParameter("?Sinif", MySqlDbType.Int32),
 new MySqlParameter("?BransId", MySqlDbType.Int32),
 new MySqlParameter("?KitapcikTuru", MySqlDbType.String),
 new MySqlParameter("?Cevaplar", MySqlDbType.String),
};
        pars[0].Value = info.SinavId;
        pars[1].Value = info.Sinif;
        pars[2].Value = info.BransId;
        pars[3].Value = info.KitapcikTuru;
        pars[4].Value = info.Cevaplar;
        helper.ExecuteNonQuery(sql, pars);
    }

    public void KayitGuncelle(CkKarneDogruCevaplarInfo info)
    {
        const string sql = @"update ckkarnedogrucevaplar set SinavId=?SinavId,Sinif=?Sinif,BransId=?BransId,KitapcikTuru=?KitapcikTuru,Cevaplar=?Cevaplar where Id=?Id";
        MySqlParameter[] pars =
        {
 new MySqlParameter("?SinavId", MySqlDbType.Int32),
 new MySqlParameter("?Sinif", MySqlDbType.Int32),
 new MySqlParameter("?BransId", MySqlDbType.Int32),
 new MySqlParameter("?KitapcikTuru", MySqlDbType.String),
 new MySqlParameter("?Cevaplar", MySqlDbType.String),
 new MySqlParameter("?Id", MySqlDbType.Int32),
};
        pars[0].Value = info.SinavId;
        pars[1].Value = info.Sinif;
        pars[2].Value = info.BransId;
        pars[3].Value = info.KitapcikTuru;
        pars[4].Value = info.Cevaplar;
        pars[5].Value = info.Id;
        helper.ExecuteNonQuery(sql, pars);
    }
}

