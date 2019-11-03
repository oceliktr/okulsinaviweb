using System;
using System.Data;
using DAL;
using MySql.Data.MySqlClient;
public class SbMaddeKokleriInfo
{
    public int Id { get; set; }
    public int UyeId { get; set; }
    public int MaddeTuru { get; set; }
    public int Sinif { get; set; }
    public int Brans { get; set; }
    public string Kazanim { get; set; }
    public string SoruKoku { get; set; }
    public string ZorlukOgretmen { get; set; }
    public string Bilgi { get; set; }
    public int Durum { get; set; }

}

public class SbMaddeKokleriDB
{
    readonly HelperDb helper = new HelperDb();

    public DataTable KayitlariGetir()
    {
        const string sql = "select * from SbMaddeKokleri order by Id asc";
        return helper.ExecuteDataSet(sql).Tables[0];
    }
    public DataTable KayitlariGetir(string sql)
    {
        return helper.ExecuteDataSet(sql).Tables[0];
    }
    public DataTable KayitlariGetir(int uyeId)
    {
        const string sql = "select * from SbMaddeKokleri where UyeId=?UyeId order by Id desc";
        MySqlParameter p = new MySqlParameter("?UyeId",MySqlDbType.Int32){Value = uyeId};
        return helper.ExecuteDataSet(sql,p).Tables[0];
    }
    public SbMaddeKokleriInfo KayitBilgiGetir(string cmdText, params MySqlParameter[] param)
    {
        MySqlDataReader dr = helper.ExecuteReader(cmdText, param);
        SbMaddeKokleriInfo info = new SbMaddeKokleriInfo();
        while (dr.Read())
        {
            info.Id = dr.GetMySayi("Id");
            info.MaddeTuru = dr.GetMySayi("MaddeTuru");
            info.Sinif = dr.GetMySayi("Sinif");
            info.Brans = dr.GetMySayi("Brans");
            info.Kazanim = dr.GetMyMetin("Kazanim");
            info.SoruKoku = dr.GetMyMetin("SoruKoku");
            info.ZorlukOgretmen = dr.GetMyMetin("ZorlukOgretmen");
            info.Bilgi = dr.GetMyMetin("Bilgi");
        }
        dr.Close();

        return info;
    }

    public SbMaddeKokleriInfo KayitBilgiGetir(int id)
    {
        string cmdText = "select * from sbmaddekokleri where Id=?Id";
        MySqlParameter param = new MySqlParameter("?Id", MySqlDbType.Int32) { Value = id };
        MySqlDataReader dr = helper.ExecuteReader(cmdText, param);
        
        return TabloAlalnlar(dr);
    }

    private static SbMaddeKokleriInfo TabloAlalnlar(MySqlDataReader dr)
    {
        SbMaddeKokleriInfo info = new SbMaddeKokleriInfo();
        while (dr.Read())
        {
            info.Id = dr.GetMySayi("Id");
            info.UyeId = dr.GetMySayi("UyeId");
            info.MaddeTuru = dr.GetMySayi("MaddeTuru");
            info.Sinif = dr.GetMySayi("Sinif");
            info.Brans = dr.GetMySayi("Brans");
            info.Kazanim = dr.GetMyMetin("Kazanim");
            info.SoruKoku = dr.GetMyMetin("SoruKoku");
            info.ZorlukOgretmen = dr.GetMyMetin("ZorlukOgretmen");
            info.Durum = dr.GetMySayi("Durum");
            info.Bilgi = dr.GetMyMetin("Bilgi");
        }

        dr.Close();
        return info;
    }

    public void KayitSil(int id)
    {
        const string sql = "delete from sbmaddekokleri where Id=?Id";
        MySqlParameter p = new MySqlParameter("?Id", MySqlDbType.Int32) { Value = id };
        helper.ExecuteNonQuery(sql, p);
    }

    public void KayitEkle(SbMaddeKokleriInfo info)
    {
        const string sql = @"insert into sbmaddekokleri (MaddeTuru,Sinif,Brans,Kazanim,SoruKoku,ZorlukOgretmen,Bilgi,UyeId,Durum) values (?MaddeTuru,?Sinif,?Brans,?Kazanim,?SoruKoku,?ZorlukOgretmen,?Bilgi,?UyeId,?Durum)";
        MySqlParameter[] pars =
        {
             new MySqlParameter("?MaddeTuru", MySqlDbType.Int32),
             new MySqlParameter("?Sinif", MySqlDbType.Int32),
             new MySqlParameter("?Brans", MySqlDbType.Int32),
             new MySqlParameter("?Kazanim", MySqlDbType.String),
             new MySqlParameter("?SoruKoku", MySqlDbType.String),
             new MySqlParameter("?ZorlukOgretmen", MySqlDbType.String),
             new MySqlParameter("?Bilgi", MySqlDbType.String),
             new MySqlParameter("?UyeId", MySqlDbType.Int32),
             new MySqlParameter("?Durum", MySqlDbType.Int32)
        };
        pars[0].Value = info.MaddeTuru;
        pars[1].Value = info.Sinif;
        pars[2].Value = info.Brans;
        pars[3].Value = info.Kazanim;
        pars[4].Value = info.SoruKoku;
        pars[5].Value = info.ZorlukOgretmen;
        pars[6].Value = info.Bilgi;
        pars[7].Value = info.UyeId;
        pars[8].Value = info.Durum;
        helper.ExecuteNonQuery(sql, pars);
    }

    public void KayitGuncelle(SbMaddeKokleriInfo info)
    {
        const string sql = @"update sbmaddekokleri set SoruKoku=?SoruKoku,ZorlukOgretmen=?ZorlukOgretmen,Bilgi=?Bilgi,Durum=?Durum where Id=?Id";
        MySqlParameter[] pars =
        {
             new MySqlParameter("?SoruKoku", MySqlDbType.String),
             new MySqlParameter("?ZorlukOgretmen", MySqlDbType.String),
             new MySqlParameter("?Bilgi", MySqlDbType.String),
             new MySqlParameter("?Durum", MySqlDbType.Int32),
             new MySqlParameter("?Id", MySqlDbType.Int32)
        };
        pars[0].Value = info.SoruKoku;
        pars[1].Value = info.ZorlukOgretmen;
        pars[2].Value = info.Bilgi;
        pars[3].Value = info.Durum;
        pars[4].Value = info.Id;
        helper.ExecuteNonQuery(sql, pars);
    }
    public int SonKayitId()
    {
        string cmdText = "SELECT Id FROM sbmaddekokleri ORDER BY Id DESC LIMIT 1";
        int sonuc = Convert.ToInt32(helper.ExecuteScalar(cmdText));
        return sonuc;
    }
}

