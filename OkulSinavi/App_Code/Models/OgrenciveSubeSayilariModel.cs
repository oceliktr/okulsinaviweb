using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using MySql.Data.MySqlClient;

/// <summary>
/// Summary description for OgrenciveSubeSayilariModel
/// </summary>
public class OgrenciveSubeSayilariModel
{
    public int KurumKodu { get; set; }
    public string IlceAdi { get; set; }
    public string KurumAdi { get; set; }
    public int SubeSayisi { get; set; }
    public int OgrenciSayisi { get; set; }

    public OgrenciveSubeSayilariModel(int kurumKodu, string ilceAdi, string kurumAdi, int subeSayisi, int ogrenciSayisi)
    {
        KurumKodu = kurumKodu;
        IlceAdi = ilceAdi;
        KurumAdi = kurumAdi;
        SubeSayisi = subeSayisi;
        OgrenciSayisi = ogrenciSayisi;
    }
}

public class OgrenciveSubeSayilariDb
{
    private readonly HelperDb helper = new HelperDb();
    public List<OgrenciveSubeSayilariModel> OgrenciSayilari(int donem,int sinif, int ilce)
    {
        string sql = @"SELECT t1.KurumKodu,i.IlceAdi,k.KurumAdi,COUNT(t1.KurumKodu)  AS OgrenciSayisi
                        FROM testkutuk AS t1
                        INNER JOIN kurumlar AS k ON k.KurumKodu = t1.KurumKodu
                        INNER JOIN ilceler AS i ON i.Id = k.IlceId
                        WHERE t1.DonemId = ?DonemId AND t1.Sinifi = ?Sinif";

        if (ilce != 0)
            sql += " AND  k.IlceId=?IlceId";

        sql += " GROUP BY t1.KurumKodu order by i.IlceAdi,k.KurumAdi";
        MySqlParameter[] p =
        {
            new MySqlParameter("?DonemId", MySqlDbType.Int32),
            new MySqlParameter("?Sinif", MySqlDbType.Int32),
            new MySqlParameter("?IlceId", MySqlDbType.Int32)
        };
        p[0].Value = donem;
        p[1].Value = sinif;
        p[2].Value = ilce;

        DataTable dt = helper.ExecuteDataSet(sql, p).Tables[0];
        List<OgrenciveSubeSayilariModel> karne = new List<OgrenciveSubeSayilariModel>();
        foreach (DataRow k in dt.Rows)
        {
            int kurumKodu = Convert.ToInt32(k["KurumKodu"]);
            int subeSayisi = SubeSayisi(donem, kurumKodu);
            karne.Add(new OgrenciveSubeSayilariModel(kurumKodu, k["IlceAdi"].ToString(), k["KurumAdi"].ToString(), subeSayisi, Convert.ToInt32(k["OgrenciSayisi"])));
        }
        return karne;
    }

    public int SubeSayisi(int donem,int kurumKodu)
    {
        const string cmdText = "select COUNT(DISTINCT(Sube)) from testkutuk where DonemId=?DonemId and KurumKodu=?KurumKodu";
        MySqlParameter[] p =
        {
            new MySqlParameter("?DonemId", MySqlDbType.Int32),
            new MySqlParameter("?KurumKodu", MySqlDbType.Int32)
        };
        p[0].Value = donem;
        p[1].Value = kurumKodu;
        int sonuc = Convert.ToInt32(helper.ExecuteScalar(cmdText, p));
        return sonuc;
    }


}