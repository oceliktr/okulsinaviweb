using System;
using System.Collections.Generic;
using System.Data;
using DAL;
using MySql.Data.MySqlClient;
public class CkKarneKutukInfo
{
    private int v1;
    private int v2;
    private string v3;
    private int v4;
    private string v5;
    private int v6;
    private string v7;
    private string v8;
    private int v9;
    private string v10;
    private int v11;

    public int Id { get; set; }
    public int OpaqId { get; set; }
    public string IlAdi { get; set; }
    public string IlceAdi { get; set; }
    public int KurumKodu { get; set; }
    public string KurumAdi { get; set; }
    public int OgrenciNo { get; set; }
    public string Adi { get; set; }
    public string Soyadi { get; set; }
    public int Sinifi { get; set; }
    public string Sube { get; set; }
    public int SinavId { get; set; }
    public int DersKodu { get; set; }
    public string KatilimDurumu { get; set; }
    public string KitapcikTuru { get; set; }
    public string Cevaplar { get; set; }


    public CkKarneKutukInfo()
    {
        
    }

    public CkKarneKutukInfo(int kurumKodu, string kurumAdi)
    {
        KurumKodu = kurumKodu;
        KurumAdi = kurumAdi;
    }
    public CkKarneKutukInfo(int id, int opaqId, string ilceAdi, int kurumKodu, string kurumAdi, int ogrenciNo, string adi, string soyadi, int sinifi, string sube, int sinavId, string katilimDurumu, string kitapcikTuru, string cevaplar)
    {
        Id = id;
        OpaqId = opaqId;
        IlceAdi = ilceAdi;
        KurumKodu = kurumKodu;
        KurumAdi = kurumAdi;
        OgrenciNo = ogrenciNo;
        Adi = adi;
        Soyadi = soyadi;
        Sinifi = sinifi;
        Sube = sube;
        SinavId = sinavId;
        KatilimDurumu = katilimDurumu;
        KitapcikTuru = kitapcikTuru;
        Cevaplar = cevaplar;
    }
}

public class CkKarneKutukDB
{
    readonly HelperDb helper = new HelperDb();
    public DataTable KayitlariGetir()
    {
        const string sql = "select * from ckkarnekutuk order by Id asc";
        return helper.ExecuteDataSet(sql).Tables[0];
    }
    public DataTable KayitlariGetir(int sinavId,string ilceAdi, int sinif)
    {
        const string sql = "select DISTINCT KurumKodu,IlceAdi, KurumAdi, SinavId from ckkarnekutuk where SinavId=?SinavId and IlceAdi=?IlceAdi and Sinifi=?Sinifi order by KurumAdi asc";
        MySqlParameter[] pars =
        {
            new MySqlParameter("?SinavId", MySqlDbType.Int32),
            new MySqlParameter("?IlceAdi", MySqlDbType.String),
            new MySqlParameter("?Sinifi", MySqlDbType.Int32)
        };
        pars[0].Value = sinavId;
        pars[1].Value = ilceAdi;
        pars[2].Value = sinif;

        var table =helper.ExecuteDataSet(sql,pars).Tables[0];
        return table;
    }
    public DataTable KayitlariGetir(int sinavId, int kurumKodu,int sinif)
    {
        const string sql = "select  DISTINCT KurumKodu,IlceAdi, KurumAdi, SinavId from ckkarnekutuk where SinavId=?SinavId and KurumKodu=?KurumKodu and Sinifi=?Sinifi order by KurumAdi asc";
        MySqlParameter[] pars =
        {
            new MySqlParameter("?SinavId", MySqlDbType.Int32),
            new MySqlParameter("?KurumKodu", MySqlDbType.Int32),
            new MySqlParameter("?Sinifi", MySqlDbType.Int32)
        };
        pars[0].Value = sinavId;
        pars[1].Value = kurumKodu;
        pars[2].Value = sinif;

        var table = helper.ExecuteDataSet(sql, pars).Tables[0];
        return table;
    }

    public DataTable SinifSubeSayilari(int sinavId, int sinif)
    {
        string sql =@"SELECT DISTINCT(ktk.KurumKodu),ktk.IlceAdi,ktk.KurumAdi,ktk.Sinifi,COUNT(Id) AS OgrenciSayisi
                    FROM ckkarnekutuk AS ktk WHERE ktk.SinavId=?SinavId AND ktk.Sinifi=?Sinifi
                    GROUP BY ktk.KurumKodu,ktk.Sinifi
                    ORDER BY ktk.IlceAdi,ktk.KurumAdi";

        MySqlParameter[] pars =
        {
            new MySqlParameter("?SinavId", MySqlDbType.Int32),
            new MySqlParameter("?Sinifi", MySqlDbType.Int32)
        };
        pars[0].Value = sinavId;
        pars[1].Value = sinif;

        var table = helper.ExecuteDataSet(sql, pars).Tables[0];
        return table;
    }
    public List<CkKarneKutukInfo> IlceninOkullari(int sinif, string ilce)
    {
        string sql = "SELECT DISTINCT ktk.KurumKodu,ktk.KurumAdi FROM ckkarnekutuk AS ktk WHERE ktk.Sinifi=?Sinif AND ktk.IlceAdi=?Ilce";
        MySqlParameter[] p =
        {
            new MySqlParameter("?Sinif", MySqlDbType.Int32),
            new MySqlParameter("?Ilce", MySqlDbType.String)
        };
        p[0].Value = sinif;
        p[1].Value = ilce;

        DataTable dt = helper.ExecuteDataSet(sql, p).Tables[0];
        List<CkKarneKutukInfo> karne = new List<CkKarneKutukInfo>();
        foreach (DataRow k in dt.Rows)
        {
            karne.Add(new CkKarneKutukInfo(Convert.ToInt32(k["KurumKodu"]), k["KurumAdi"].ToString()));
        }
        return karne;
    }

    public List<CkKarneKutukInfo> KayitlariDizeGetir(int sinavId, int kurumKodu)
    {
        string sql = "select * from ckkarnekutuk where SinavId=?SinavId and KurumKodu=?KurumKodu";
        MySqlParameter[] p =
        {
            new MySqlParameter("?SinavId", MySqlDbType.Int32),
            new MySqlParameter("?KurumKodu", MySqlDbType.Int32)
        };
        p[0].Value = sinavId;
        p[1].Value =  kurumKodu;

        DataTable dt = helper.ExecuteDataSet(sql, p).Tables[0];
        List<CkKarneKutukInfo> karne = new List<CkKarneKutukInfo>();
        foreach (DataRow k in dt.Rows)
        {
            karne.Add(new CkKarneKutukInfo(Convert.ToInt32(k["Id"]), Convert.ToInt32(k["OpaqId"]), k["IlceAdi"].ToString(), 
                Convert.ToInt32(k["KurumKodu"]),k["KurumAdi"].ToString(), Convert.ToInt32(k["OgrenciNo"]), k["Adi"].ToString(), k["Soyadi"].ToString(), Convert.ToInt32(k["Sinifi"]),k["Sube"].ToString(), Convert.ToInt32(k["SinavId"]), k["KatilimDurumu"].ToString(), k["KitapcikTuru"].ToString(), k["Cevaplar"].ToString()));
        }
        return karne;
    }
    public CkKarneKutukInfo KayitBilgiGetir(string cmdText, params MySqlParameter[] param)
    {
        MySqlDataReader dr = helper.ExecuteReader(cmdText, param);
        var info = TabloAlanlar(dr);

        return info;
    }

    private static CkKarneKutukInfo TabloAlanlar(MySqlDataReader dr)
    {
        CkKarneKutukInfo info = new CkKarneKutukInfo();
        while (dr.Read())
        {
            info.Id = dr.GetMySayi("Id");
            info.OpaqId = dr.GetMySayi("OpaqId");
            info.IlAdi = dr.GetMyMetin("IlAdi");
            info.IlceAdi = dr.GetMyMetin("IlceAdi");
            info.KurumKodu = dr.GetMySayi("KurumKodu");
            info.KurumAdi = dr.GetMyMetin("KurumAdi");
            info.OgrenciNo = dr.GetMySayi("OgrenciNo");
            info.Adi = dr.GetMyMetin("Adi");
            info.Soyadi = dr.GetMyMetin("Soyadi");
            info.Sinifi = dr.GetMySayi("Sinifi");
            info.Sube = dr.GetMyMetin("Sube");
            info.SinavId = dr.GetMySayi("SinavId");
            info.DersKodu = dr.GetMySayi("DersKodu");
            info.KatilimDurumu = dr.GetMyMetin("KatilimDurumu");
            info.KitapcikTuru = dr.GetMyMetin("KitapcikTuru");
            info.Cevaplar = dr.GetMyMetin("Cevaplar");
        }

        dr.Close();
        return info;
    }

    public CkKarneKutukInfo KayitBilgiGetir(int id)
    {
        string cmdText = "select * from ckkarnekutuk where Id=?Id";
        MySqlParameter param = new MySqlParameter("?Id", MySqlDbType.Int32) { Value = id };
        MySqlDataReader dr = helper.ExecuteReader(cmdText, param);
        var info = TabloAlanlar(dr);

        return info;
    }
    public CkKarneKutukInfo KayitBilgiGetir(int sinif, int kurumkodu)
    {
        //Kütükte her hangi bir öðrencinin sýnýf ve kurum kodu verilen herhangi bir öðrencinin okul bilgisi
        string sql = "SELECT * FROM ckkarnekutuk AS ktk WHERE ktk.Sinifi=?Sinif AND ktk.KurumKodu=?KurumKodu";
        MySqlParameter[] p =
        {
            new MySqlParameter("?Sinif", MySqlDbType.Int32),
            new MySqlParameter("?KurumKodu", MySqlDbType.Int32)
        };
        p[0].Value = sinif;
        p[1].Value = kurumkodu;

        MySqlDataReader dr = helper.ExecuteReader(sql, p);
        var info = TabloAlanlar(dr);

        return info;
    }

    public void KayitSil(int id)
    {
        const string sql = "delete from ckkarnekutuk where Id=?Id";
        MySqlParameter p = new MySqlParameter("?Id", MySqlDbType.Int32) { Value = id };
        helper.ExecuteNonQuery(sql, p);
    }
    public void SinaviSil(int sinavId)
    {
        const string sql = "delete from ckkarnekutuk where SinavId=?SinavId";
        MySqlParameter p = new MySqlParameter("?SinavId", MySqlDbType.Int32) { Value = sinavId };
        helper.ExecuteNonQuery(sql, p);
    }
    public void KayitEkle(CkKarneKutukInfo info)
    {
        const string sql = @"insert into ckkarnekutuk (OpaqId,IlceAdi,KurumKodu,KurumAdi,OgrenciNo,Adi,Soyadi,Sinifi,Sube,SinavId,KatilimDurumu,KitapcikTuru,Cevaplar) values (?OpaqId,?IlceAdi,?KurumKodu,?KurumAdi,?OgrenciNo,?Adi,?Soyadi,?Sinifi,?Sube,?SinavId,?KatilimDurumu,?KitapcikTuru,?Cevaplar)";
        MySqlParameter[] pars =
        {
         new MySqlParameter("?OpaqId", MySqlDbType.Int32),
         new MySqlParameter("?IlceAdi", MySqlDbType.String),
         new MySqlParameter("?KurumKodu", MySqlDbType.Int32),
         new MySqlParameter("?KurumAdi", MySqlDbType.String),
         new MySqlParameter("?OgrenciNo", MySqlDbType.Int32),
         new MySqlParameter("?Adi", MySqlDbType.String),
         new MySqlParameter("?Soyadi", MySqlDbType.String),
         new MySqlParameter("?Sinifi", MySqlDbType.Int32),
         new MySqlParameter("?Sube", MySqlDbType.String),
         new MySqlParameter("?SinavId", MySqlDbType.Int32),
         new MySqlParameter("?KatilimDurumu", MySqlDbType.String),
         new MySqlParameter("?KitapcikTuru", MySqlDbType.String),
         new MySqlParameter("?Cevaplar", MySqlDbType.String)
        };
        pars[0].Value = info.OpaqId;
        pars[1].Value = info.IlceAdi;
        pars[2].Value = info.KurumKodu;
        pars[3].Value = info.KurumAdi;
        pars[4].Value = info.OgrenciNo;
        pars[5].Value = info.Adi;
        pars[6].Value = info.Soyadi;
        pars[7].Value = info.Sinifi;
        pars[8].Value = info.Sube;
        pars[9].Value = info.SinavId;
        pars[10].Value = info.KatilimDurumu;
        pars[11].Value = info.KitapcikTuru;
        pars[12].Value = info.Cevaplar;
        helper.ExecuteNonQuery(sql, pars);
    }

    public void KayitGuncelle(CkKarneKutukInfo info)
    {
        const string sql = @"update ckkarnekutuk set OpaqId=?OpaqId,IlceAdi=?IlceAdi,KurumKodu=?KurumKodu,KurumAdi=?KurumAdi,OgrenciNo=?OgrenciNo,Adi=?Adi,Soyadi=?Soyadi,Sinifi=?Sinifi,Sube=?Sube,SinavId=?SinavId,KatilimDurumu=?KatilimDurumu,KitapcikTuru=?KitapcikTuru,Cevaplar=?Cevaplar where Id=?Id";
        MySqlParameter[] pars =
        {
             new MySqlParameter("?OpaqId", MySqlDbType.Int32),
             new MySqlParameter("?IlceAdi", MySqlDbType.String),
             new MySqlParameter("?KurumKodu", MySqlDbType.Int32),
             new MySqlParameter("?KurumAdi", MySqlDbType.String),
             new MySqlParameter("?OgrenciNo", MySqlDbType.Int32),
             new MySqlParameter("?Adi", MySqlDbType.String),
             new MySqlParameter("?Soyadi", MySqlDbType.String),
             new MySqlParameter("?Sinifi", MySqlDbType.Int32),
             new MySqlParameter("?Sube", MySqlDbType.String),
             new MySqlParameter("?KatilimDurumu", MySqlDbType.String),
             new MySqlParameter("?KitapcikTuru", MySqlDbType.String),
             new MySqlParameter("?Cevaplar", MySqlDbType.String)
            };
        pars[0].Value = info.OpaqId;
        pars[1].Value = info.IlceAdi;
        pars[2].Value = info.KurumKodu;
        pars[3].Value = info.KurumAdi;
        pars[4].Value = info.OgrenciNo;
        pars[5].Value = info.Adi;
        pars[6].Value = info.Soyadi;
        pars[7].Value = info.Sinifi;
        pars[8].Value = info.Sube;
        pars[9].Value = info.SinavId;
        pars[10].Value = info.KatilimDurumu;
        pars[11].Value = info.KitapcikTuru;
        pars[12].Value = info.Cevaplar;
        pars[13].Value = info.Id;
        helper.ExecuteNonQuery(sql, pars);
    }

    public int SinavaGirmeyenSayisi(int sinavId,int kurumKodu, int sinif)
    {
        string sql = @"SELECT COUNT(ktk.Id) FROM ckkarnekutuk AS ktk WHERE ktk.SinavId=?SinavId AND ktk.KurumKodu=?KurumKodu and ktk.Sinifi=?Sinifi AND                                   (ktk.KatilimDurumu='0' OR (
                        ktk.Cevaplar NOT LIKE '%A%' and ktk.Cevaplar NOT LIKE '%B%' and ktk.Cevaplar NOT LIKE '%C%' and ktk.Cevaplar NOT LIKE '%D%' and ktk.Cevaplar NOT LIKE '%E%'
                        ))";

        MySqlParameter[] pars =
        {
            new MySqlParameter("?SinavId", MySqlDbType.Int32),
            new MySqlParameter("?KurumKodu", MySqlDbType.Int32),
            new MySqlParameter("?Sinifi", MySqlDbType.Int32)
        };
        pars[0].Value = sinavId;
        pars[1].Value = kurumKodu;
        pars[2].Value = sinif;


        int ogrenciSayisi = Convert.ToInt32(helper.ExecuteScalar(sql, pars));
        return ogrenciSayisi;
    }
}

