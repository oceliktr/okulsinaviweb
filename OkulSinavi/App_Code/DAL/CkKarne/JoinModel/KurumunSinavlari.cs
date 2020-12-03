using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;

/// <summary>
/// Summary description for KurumunSinavlar
/// </summary>
public class KurumunSinavlari
{
    public int SinavId { get; set; }
    public string SinavAdi { get; set; }

    public KurumunSinavlari()
    {
        
    }

    public KurumunSinavlari(int sinavId, string sinavAdi)
    {
        SinavId = sinavId;
        SinavAdi = sinavAdi;
    }
}

public class KurumunSinavlariDb
{
    private readonly HelperDb helper = new HelperDb();
    public List<KurumunSinavlari> KayitlariDizeGetir(int sinif, int kurumKodu)
    {
        string sql = @"SELECT DISTINCT(ks.SinavId),sa.SinavAdi FROM ckkarnesonuclari AS ks 
                        INNER JOIN cksinavadi AS sa ON sa.SinavId = ks.SinavId
                        WHERE ks.KurumKodu=?KurumKodu AND ks.Sinif =?Sinif ORDER BY sa.Id ASC";
        MySqlParameter[] p =
        {
            new MySqlParameter("?KurumKodu", MySqlDbType.Int32),
            new MySqlParameter("?Sinif", MySqlDbType.Int32)
        };
        p[0].Value = kurumKodu;
        p[1].Value = sinif;

        DataTable dt = helper.ExecuteDataSet(sql, p).Tables[0];
        List<KurumunSinavlari> karne = new List<KurumunSinavlari>();
        foreach (DataRow k in dt.Rows)
        {
            karne.Add(new KurumunSinavlari(Convert.ToInt32(k["SinavId"]),  k["SinavAdi"].ToString()));
        }
        return karne;
    }
}