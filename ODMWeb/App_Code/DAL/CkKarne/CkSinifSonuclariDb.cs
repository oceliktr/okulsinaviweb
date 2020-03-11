using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using MySql.Data.MySqlClient;

public class CkSinifSonuclariInfo
{
    public int Id { get; set; }
    public int SinavId { get; set; }
    public int KurumKodu { get; set; }
    public int Sinif { get; set; }
    public int OgrenciSayisi { get; set; }
    public string IlceAdi { get; set; }
    public string KurumAdi { get; set; }
    public string Sonuclar { get; set; }
}
public class CkSinifSonuclariDb
{
    readonly HelperDb helper = new HelperDb();
    public DataTable KayitlariGetir()
    {
        const string sql = "select * from cksinifsonuclari order by Id asc";
        return helper.ExecuteDataSet(sql).Tables[0];
    }
    public CkSinifSonuclariInfo KayitBilgiGetir(string cmdText, params MySqlParameter[] param)
    {
        MySqlDataReader dr = helper.ExecuteReader(cmdText, param);
        var info = TabloAlanlar(dr);

        return info;
    }

    private static CkSinifSonuclariInfo TabloAlanlar(MySqlDataReader dr)
    {
        CkSinifSonuclariInfo info = new CkSinifSonuclariInfo();
        while (dr.Read())
        {
            info.Id = dr.GetMySayi("Id");
            info.SinavId = dr.GetMySayi("SinavId");
            info.KurumKodu = dr.GetMySayi("KurumKodu");
            info.Sinif = dr.GetMySayi("Sinif");
            info.OgrenciSayisi = dr.GetMySayi("OgrenciSayisi");
            info.IlceAdi = dr.GetMyMetin("IlceAdi");
            info.KurumAdi = dr.GetMyMetin("KurumAdi");
            info.Sonuclar = dr.GetMyMetin("Sonuclar");
        }

        dr.Close();
        return info;
    }
    public CkSinifSonuclariInfo KayitBilgiGetir(int id)
    {
        string cmdText = "select * from cksinifsonuclari where Id=?Id";
        MySqlParameter param = new MySqlParameter("?Id", MySqlDbType.Int32) { Value = id };
        MySqlDataReader dr = helper.ExecuteReader(cmdText, param);
        var info = TabloAlanlar(dr);

        return info;
    }
    public void KayitSil(int id)
    {
        const string sql = "delete from cksinifsonuclari where Id=?Id";
        MySqlParameter p = new MySqlParameter("?Id", MySqlDbType.Int32) { Value = id };
        helper.ExecuteNonQuery(sql, p);
    }
    public void SinaviSil(int sinavId)
    {
        const string sql = "delete from cksinifsonuclari where SinavId=?SinavId";
        MySqlParameter p = new MySqlParameter("?SinavId", MySqlDbType.Int32) { Value = sinavId };
        helper.ExecuteNonQuery(sql, p);
    }
    public void KayitEkle(CkSinifSonuclariInfo info)
    {
        const string sql = @"insert into cksinifsonuclari (SinavId,KurumKodu,Sinif,OgrenciSayisi,IlceAdi,KurumAdi,Sonuclar) values (?SinavId,?KurumKodu,?Sinif,?OgrenciSayisi,?IlceAdi,?KurumAdi,?Sonuclar)";
        MySqlParameter[] pars =
        {
                new MySqlParameter("?SinavId", MySqlDbType.Int32),
                new MySqlParameter("?KurumKodu", MySqlDbType.Int32),
                new MySqlParameter("?Sinif", MySqlDbType.Int32),
                new MySqlParameter("?OgrenciSayisi", MySqlDbType.Int32),
                new MySqlParameter("?IlceAdi", MySqlDbType.String),
                new MySqlParameter("?KurumAdi", MySqlDbType.String),
                new MySqlParameter("?Sonuclar", MySqlDbType.String)
            };
        pars[0].Value = info.SinavId;
        pars[1].Value = info.KurumKodu;
        pars[2].Value = info.Sinif;
        pars[3].Value = info.OgrenciSayisi;
        pars[4].Value = info.IlceAdi;
        pars[5].Value = info.KurumAdi;
        pars[6].Value = info.Sonuclar;
        helper.ExecuteNonQuery(sql, pars);
    }

    public void KayitGuncelle(CkSinifSonuclariInfo info)
    {
        const string sql = @"update cksinifsonuclari set SinavId=?SinavId,KurumKodu=?KurumKodu,Sinif=?Sinif,OgrenciSayisi=?OgrenciSayisi,IlceAdi=?IlceAdi,KurumAdi=?KurumAdi,Sonuclar=?Sonuclar where Id=?Id";
        MySqlParameter[] pars =
        {
                new MySqlParameter("?SinavId", MySqlDbType.Int32),
                new MySqlParameter("?KurumKodu", MySqlDbType.Int32),
                new MySqlParameter("?Sinif", MySqlDbType.Int32),
                new MySqlParameter("?OgrenciSayisi", MySqlDbType.Int32),
                new MySqlParameter("?IlceAdi", MySqlDbType.String),
                new MySqlParameter("?KurumAdi", MySqlDbType.String),
                new MySqlParameter("?Sonuclar", MySqlDbType.String),
                new MySqlParameter("?Id", MySqlDbType.Int32)
            };
        pars[0].Value = info.SinavId;
        pars[1].Value = info.KurumKodu;
        pars[2].Value = info.Sinif;
        pars[3].Value = info.OgrenciSayisi;
        pars[4].Value = info.IlceAdi;
        pars[5].Value = info.KurumAdi;
        pars[6].Value = info.Sonuclar;
        pars[7].Value = info.Id;
        helper.ExecuteNonQuery(sql, pars);
    }
}
