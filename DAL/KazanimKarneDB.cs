using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using DAL;
using MySql.Data.MySqlClient;
public class KazanimKarneInfo
{
    public int Id { get; set; }
    public int SinavId { get; set; }
    public int KurumKodu { get; set; }
    public int BransId { get; set; }
    public int Sinif { get; set; }
    public string Sube { get; set; }
    public int DogruSayisi { get; set; }
    public int YanlisSayisi { get; set; }
    public int BosSayisi { get; set; }
    public int OgrenciSayisi { get; set; }
    public string KitapcikTuru { get; set; }
    public string KazanimNo { get; set; }
    public string Kazanim { get; set; }

    public KazanimKarneInfo()
    {

    }
    public KazanimKarneInfo(string kazanimNo, string kazanim)
    {
        KazanimNo = kazanimNo;
        Kazanim = kazanim;
    }
    public KazanimKarneInfo(int dogruSayisi, int yanlisSayisi, int bosSayisi, int ogrenciSayisi)
    {
        DogruSayisi = dogruSayisi;
        YanlisSayisi = yanlisSayisi;
        BosSayisi = bosSayisi;
        OgrenciSayisi = ogrenciSayisi;
    }
}

public class KazanimKarneDB
{
    readonly HelperDb helper = new HelperDb();
    public List<KazanimKarneInfo> IlKazanimlariGetir(int sinavId, int sinif, int bransId)
    {
        try
        {
            
            string sql = @"select Distinct(kazanimkarne.KazanimNo),kazanimkarne.Kazanim from kazanimkarne where SinavId=?SinavId and Sinif=?Sinif and BransId=?Brans";

            MySqlParameter[] param =
            {
                new MySqlParameter("?SinavId", MySqlDbType.Int32),
                new MySqlParameter("?Sinif", MySqlDbType.Int32),
                new MySqlParameter("?Brans", MySqlDbType.Int32)
            };
            param[0].Value = sinavId;
            param[1].Value = sinif;
            param[2].Value = bransId;
            DataTable veriler = helper.ExecuteDataSet(sql, param).Tables[0];
            return (from DataRow row in veriler.Rows select new KazanimKarneInfo(row["KazanimNo"].ToString(), row["Kazanim"].ToString())).ToList();

        }
        catch (Exception)
        {
            return null;
        }

    }
    public List<KazanimKarneInfo> IlceKazanimlariGetir(int sinavId, int sinif, int bransId, string kurumTxt)
    {
        try
        {
            kurumTxt = kurumTxt.Substring(0, kurumTxt.Length - 2);

            string sql = @"select Distinct(kazanimkarne.KazanimNo),kazanimkarne.Kazanim from kazanimkarne where SinavId=?SinavId and Sinif=?Sinif and BransId=?Brans and (" + kurumTxt + ")";

            MySqlParameter[] param =
            {
                new MySqlParameter("?SinavId", MySqlDbType.Int32),
                new MySqlParameter("?Sinif", MySqlDbType.Int32),
                new MySqlParameter("?Brans", MySqlDbType.Int32)
            };
            param[0].Value = sinavId;
            param[1].Value = sinif;
            param[2].Value = bransId;
            DataTable veriler = helper.ExecuteDataSet(sql, param).Tables[0];
            return (from DataRow row in veriler.Rows select new KazanimKarneInfo(row["KazanimNo"].ToString(), row["Kazanim"].ToString())).ToList();

        }
        catch (Exception)
        {
            return null;
        }

    }
    public List<KazanimKarneInfo> OkulKazanimlariGetir(int sinavId, int sinif, int bransId, int kurumKodu)
    {
        try
        {
            string sql = @"select Distinct(kazanimkarne.KazanimNo),kazanimkarne.Kazanim from kazanimkarne where SinavId=?SinavId and Sinif=?Sinif and BransId=?Brans and KurumKodu=?KurumKodu";

            MySqlParameter[] param =
            {
                new MySqlParameter("?SinavId", MySqlDbType.Int32),
                new MySqlParameter("?Sinif", MySqlDbType.Int32),
                new MySqlParameter("?Brans", MySqlDbType.Int32),
                new MySqlParameter("?KurumKodu", MySqlDbType.Int32)
            };
            param[0].Value = sinavId;
            param[1].Value = sinif;
            param[2].Value = bransId;
            param[3].Value = kurumKodu;
            DataTable veriler = helper.ExecuteDataSet(sql, param).Tables[0];
            return (from DataRow row in veriler.Rows select new KazanimKarneInfo(row["KazanimNo"].ToString(), row["Kazanim"].ToString())).ToList();

        }
        catch (Exception)
        {
            return null;
        }

    }
    public List<KazanimKarneInfo> IlKazanimlariHesapla(int sinavId, int sinif, int bransId, string kazanimNo)
    {
        string sql = @"select Sum(DogruSayisi) as DogruSayisi,Sum(YanlisSayisi) as YanlisSayisi,Sum(BosSayisi) as BosSayisi,OgrenciSayisi from kazanimkarne where SinavId=?SinavId and Sinif=?Sinif and BransId=?Brans and KazanimNo=?KazanimNo";

        MySqlParameter[] param =
        {
            new MySqlParameter("?SinavId", MySqlDbType.Int32),
            new MySqlParameter("?Sinif", MySqlDbType.Int32),
            new MySqlParameter("?Brans", MySqlDbType.Int32),
            new MySqlParameter("?KazanimNo", MySqlDbType.String)
        };
        param[0].Value = sinavId;
        param[1].Value = sinif;
        param[2].Value = bransId;
        param[3].Value = kazanimNo;
        DataTable veriler = helper.ExecuteDataSet(sql, param).Tables[0];

        return (from DataRow row in veriler.Rows select new KazanimKarneInfo(Convert.ToInt32(row["DogruSayisi"]), Convert.ToInt32(row["YanlisSayisi"]), Convert.ToInt32(row["BosSayisi"]), Convert.ToInt32(row["OgrenciSayisi"]))).ToList();

    }
    public List<KazanimKarneInfo> IlceKazanimlariHesapla(int sinavId, int sinif, int bransId, string kurumTxt, string kazanimNo)
    {

        kurumTxt = kurumTxt.Substring(0, kurumTxt.Length - 2);

        string sql = @"select Sum(DogruSayisi) as DogruSayisi,Sum(YanlisSayisi) as YanlisSayisi,Sum(BosSayisi) as BosSayisi,OgrenciSayisi from kazanimkarne where SinavId=?SinavId and Sinif=?Sinif and BransId=?Brans and (" + kurumTxt + ") and KazanimNo=?KazanimNo";

        MySqlParameter[] param =
        {
            new MySqlParameter("?SinavId", MySqlDbType.Int32),
            new MySqlParameter("?Sinif", MySqlDbType.Int32),
            new MySqlParameter("?Brans", MySqlDbType.Int32),
            new MySqlParameter("?KazanimNo", MySqlDbType.String)
        };
        param[0].Value = sinavId;
        param[1].Value = sinif;
        param[2].Value = bransId;
        param[3].Value = kazanimNo;
        DataTable veriler = helper.ExecuteDataSet(sql, param).Tables[0];

        return (from DataRow row in veriler.Rows select new KazanimKarneInfo(Convert.ToInt32(row["DogruSayisi"]), Convert.ToInt32(row["YanlisSayisi"]), Convert.ToInt32(row["BosSayisi"]), Convert.ToInt32(row["OgrenciSayisi"]))).ToList();

    }
    public List<KazanimKarneInfo> OkulKazanimlariHesapla(int sinavId, int sinif, int bransId, int kurumKodu, string kazanimNo)
    {
        string sql = @"select Sum(DogruSayisi) as DogruSayisi,Sum(YanlisSayisi) as YanlisSayisi,Sum(BosSayisi) as BosSayisi,OgrenciSayisi from kazanimkarne where SinavId=?SinavId and Sinif=?Sinif and BransId=?Brans and KurumKodu=?KurumKodu and KazanimNo=?KazanimNo";

        MySqlParameter[] param =
        {
            new MySqlParameter("?SinavId", MySqlDbType.Int32),
            new MySqlParameter("?Sinif", MySqlDbType.Int32),
            new MySqlParameter("?Brans", MySqlDbType.Int32),
            new MySqlParameter("?KurumKodu", MySqlDbType.Int32),
            new MySqlParameter("?KazanimNo", MySqlDbType.String)
        };
        param[0].Value = sinavId;
        param[1].Value = sinif;
        param[2].Value = bransId;
        param[3].Value = kurumKodu;
        param[4].Value = kazanimNo;
        DataTable veriler = helper.ExecuteDataSet(sql, param).Tables[0];

        return (from DataRow row in veriler.Rows select new KazanimKarneInfo(Convert.ToInt32(row["DogruSayisi"]), Convert.ToInt32(row["YanlisSayisi"]), Convert.ToInt32(row["BosSayisi"]), Convert.ToInt32(row["OgrenciSayisi"]))).ToList();

    }
    public int IlceOgrenciSayisi(int sinavId, int sinif, int bransId, string kurumTxt, string kazanim)
    {
        try
        {
            kurumTxt = kurumTxt.Substring(0, kurumTxt.Length - 2);
            string sql = @"select sum(OgrenciSayisi) as OgrenciSayisi from kazanimkarne where SinavId=?SinavId and Sinif=?Sinif and BransId=?Brans and (" + kurumTxt + ") and KazanimNo=?KazanimNo";

            MySqlParameter[] param =
            {
                new MySqlParameter("?SinavId", MySqlDbType.Int32),
                new MySqlParameter("?Sinif", MySqlDbType.Int32),
                new MySqlParameter("?Brans", MySqlDbType.Int32),
                new MySqlParameter("?KazanimNo", MySqlDbType.String)
            };
            param[0].Value = sinavId;
            param[1].Value = sinif;
            param[2].Value = bransId;
            param[3].Value = kazanim;

            int sonuc = string.IsNullOrEmpty(helper.ExecuteScalar(sql, param).ToString()) ? 0 : Convert.ToInt32(helper.ExecuteScalar(sql, param));
            return sonuc;
        }
        catch (Exception)
        {
            return 0;
        }
    }
    public int OkulOgrenciSayisi(int sinavId, int sinif, int bransId, int kurumKodu, string kazanim)
    {
        try
        {
            string sql = @"select sum(OgrenciSayisi) as OgrenciSayisi from kazanimkarne where SinavId=?SinavId and Sinif=?Sinif and BransId=?Brans and KurumKodu=?KurumKodu and KazanimNo=?KazanimNo";

            MySqlParameter[] param =
            {
                new MySqlParameter("?SinavId", MySqlDbType.Int32),
                new MySqlParameter("?Sinif", MySqlDbType.Int32),
                new MySqlParameter("?Brans", MySqlDbType.Int32),
                new MySqlParameter("?KazanimNo", MySqlDbType.String),
                new MySqlParameter("?KurumKodu", MySqlDbType.Int32)
            };
            param[0].Value = sinavId;
            param[1].Value = sinif;
            param[2].Value = bransId;
            param[3].Value = kazanim;
            param[4].Value = kurumKodu;

            int sonuc = string.IsNullOrEmpty(helper.ExecuteScalar(sql, param).ToString()) ? 0 : Convert.ToInt32(helper.ExecuteScalar(sql, param));
            return sonuc;
        }
        catch (Exception)
        {
            return 0;
        }
    }

    public int IlOgrenciSayisi(int sinavId, int sinif, int bransId, string kazanim)
    {
        try
        {
            string sql = @"select sum(OgrenciSayisi) as OgrenciSayisi from kazanimkarne where SinavId=?SinavId and Sinif=?Sinif and BransId=?Brans and KazanimNo=?KazanimNo";

            MySqlParameter[] param =
            {
                new MySqlParameter("?SinavId", MySqlDbType.Int32),
                new MySqlParameter("?Sinif", MySqlDbType.Int32),
                new MySqlParameter("?Brans", MySqlDbType.Int32),
                new MySqlParameter("?KazanimNo", MySqlDbType.String)
            };
            param[0].Value = sinavId;
            param[1].Value = sinif;
            param[2].Value = bransId;
            param[3].Value = kazanim;

            int sonuc = string.IsNullOrEmpty(helper.ExecuteScalar(sql, param).ToString()) ? 0 : Convert.ToInt32(helper.ExecuteScalar(sql, param));
            return sonuc;
        }
        catch (Exception)
        {
            return 0;
        }
    }
    public DataTable KayitlariGetir()
    {
        const string sql = "select * from KazanimKarne order by Id asc";
        return helper.ExecuteDataSet(sql).Tables[0];
    }
    public DataTable KayitlariGetir(int sinavId,  int bransId, int kurumKodu,int sinif,string sube)
    {
        const string sql = "select * from KazanimKarne where SinavId=?SinavId and Sinif=?Sinif and Sube=?Sube and BransId=?Brans and KurumKodu=?KurumKodu order by KazanimNo desc";
        MySqlParameter[] param =
        {
            new MySqlParameter("?SinavId", MySqlDbType.Int32),
            new MySqlParameter("?Sinif", MySqlDbType.Int32),
            new MySqlParameter("?Sube", MySqlDbType.String),
            new MySqlParameter("?Brans", MySqlDbType.Int32),
            new MySqlParameter("?KurumKodu", MySqlDbType.Int32)
        };
        param[0].Value = sinavId;
        param[1].Value = sinif;
        param[2].Value = sube;
        param[3].Value = bransId;
        param[4].Value = kurumKodu;
        return helper.ExecuteDataSet(sql,param).Tables[0];
    }

    public KazanimKarneInfo KayitBilgiGetir(string cmdText, params MySqlParameter[] param)
    {
        MySqlDataReader dr = helper.ExecuteReader(cmdText, param);
        KazanimKarneInfo info = new KazanimKarneInfo();
        while (dr.Read())
        {
            info.Id = dr.GetMySayi("Id");
            info.SinavId = dr.GetMySayi("SinavId");
            info.KurumKodu = dr.GetMySayi("KurumKodu");
            info.BransId = dr.GetMySayi("BransId");
            info.Sinif = dr.GetMySayi("Sinif");
            info.Sube = dr.GetMyMetin("Sube");
            info.DogruSayisi = dr.GetMySayi("DogruSayisi");
            info.YanlisSayisi = dr.GetMySayi("YanlisSayisi");
            info.BosSayisi = dr.GetMySayi("BosSayisi");
            info.OgrenciSayisi = dr.GetMySayi("OgrenciSayisi");
            info.KitapcikTuru = dr.GetMyMetin("KitapcikTuru");
            info.KazanimNo = dr.GetMyMetin("KazanimNo");
            info.Kazanim = dr.GetMyMetin("Kazanim");
        }
        dr.Close();

        return info;
    }

    public KazanimKarneInfo KayitBilgiGetir(int id)
    {
        string cmdText = "select * from KazanimKarne where Id=?Id";
        MySqlParameter param = new MySqlParameter("?Id", MySqlDbType.Int32) { Value = id };
        MySqlDataReader dr = helper.ExecuteReader(cmdText, param);
        KazanimKarneInfo info = new KazanimKarneInfo();
        while (dr.Read())
        {
            info.Id = dr.GetMySayi("Id");
            info.SinavId = dr.GetMySayi("SinavId");
            info.KurumKodu = dr.GetMySayi("KurumKodu");
            info.BransId = dr.GetMySayi("BransId");
            info.Sinif = dr.GetMySayi("Sinif");
            info.Sube = dr.GetMyMetin("Sube");
            info.DogruSayisi = dr.GetMySayi("DogruSayisi");
            info.YanlisSayisi = dr.GetMySayi("YanlisSayisi");
            info.BosSayisi = dr.GetMySayi("BosSayisi");
            info.OgrenciSayisi = dr.GetMySayi("OgrenciSayisi");
            info.KitapcikTuru = dr.GetMyMetin("KitapcikTuru");
            info.KazanimNo = dr.GetMyMetin("KazanimNo");
            info.Kazanim = dr.GetMyMetin("Kazanim");
        }
        dr.Close();

        return info;
    }

    public void KayitSil(int sinavId)
    {
        const string sql = "delete from KazanimKarne where SinavId=?SinavId";
        MySqlParameter p = new MySqlParameter("?SinavId", MySqlDbType.Int32) { Value = sinavId };
        helper.ExecuteNonQuery(sql, p);
    }

    public void KayitEkle(KazanimKarneInfo info)
    {
        const string sql = @"insert into KazanimKarne (SinavId,KurumKodu,BransId,Sinif,Sube,DogruSayisi,YanlisSayisi,BosSayisi,OgrenciSayisi,KitapcikTuru,KazanimNo,Kazanim) values (?SinavId,?KurumKodu,?BransId,?Sinif,?Sube,?DogruSayisi,?YanlisSayisi,?BosSayisi,?OgrenciSayisi,?KitapcikTuru,?KazanimNo,?Kazanim)";
        MySqlParameter[] pars =
        {
         new MySqlParameter("?SinavId", MySqlDbType.Int32),
         new MySqlParameter("?KurumKodu", MySqlDbType.Int32),
         new MySqlParameter("?BransId", MySqlDbType.Int32),
         new MySqlParameter("?Sinif", MySqlDbType.Int32),
         new MySqlParameter("?Sube", MySqlDbType.String),
         new MySqlParameter("?DogruSayisi", MySqlDbType.Int32),
         new MySqlParameter("?YanlisSayisi", MySqlDbType.Int32),
         new MySqlParameter("?BosSayisi", MySqlDbType.Int32),
         new MySqlParameter("?OgrenciSayisi", MySqlDbType.Int32),
         new MySqlParameter("?KitapcikTuru", MySqlDbType.String),
         new MySqlParameter("?KazanimNo", MySqlDbType.String),
         new MySqlParameter("?Kazanim", MySqlDbType.String),
        };
        pars[0].Value = info.SinavId;
        pars[1].Value = info.KurumKodu;
        pars[2].Value = info.BransId;
        pars[3].Value = info.Sinif;
        pars[4].Value = info.Sube;
        pars[5].Value = info.DogruSayisi;
        pars[6].Value = info.YanlisSayisi;
        pars[7].Value = info.BosSayisi;
        pars[8].Value = info.OgrenciSayisi;
        pars[9].Value = info.KitapcikTuru;
        pars[10].Value = info.KazanimNo;
        pars[11].Value = info.Kazanim;
        helper.ExecuteNonQuery(sql, pars);
    }

    public void KayitGuncelle(KazanimKarneInfo info)
    {
        const string sql = @"update KazanimKarne set SinavId=?SinavId,KurumKodu=?KurumKodu,BransId=?BransId,Sinif=?Sinif,Sube=?Sube,DogruSayisi=?DogruSayisi,YanlisSayisi=?YanlisSayisi,BosSayisi=?BosSayisi,OgrenciSayisi=?OgrenciSayisi,KitapcikTuru=?KitapcikTuru,KazanimNo=?KazanimNo,Kazanim=?Kazanim where Id=?Id";
        MySqlParameter[] pars =
        {
         new MySqlParameter("?SinavId", MySqlDbType.Int32),
         new MySqlParameter("?KurumKodu", MySqlDbType.Int32),
         new MySqlParameter("?BransId", MySqlDbType.Int32),
         new MySqlParameter("?Sinif", MySqlDbType.Int32),
         new MySqlParameter("?Sube", MySqlDbType.String),
         new MySqlParameter("?DogruSayisi", MySqlDbType.Int32),
         new MySqlParameter("?YanlisSayisi", MySqlDbType.Int32),
         new MySqlParameter("?BosSayisi", MySqlDbType.Int32),
         new MySqlParameter("?OgrenciSayisi", MySqlDbType.Int32),
         new MySqlParameter("?KitapcikTuru", MySqlDbType.String),
         new MySqlParameter("?KazanimNo", MySqlDbType.String),
         new MySqlParameter("?Kazanim", MySqlDbType.String),
         new MySqlParameter("?Id", MySqlDbType.Int32)
        };
        pars[0].Value = info.SinavId;
        pars[1].Value = info.KurumKodu;
        pars[2].Value = info.BransId;
        pars[3].Value = info.Sinif;
        pars[4].Value = info.Sube;
        pars[5].Value = info.DogruSayisi;
        pars[6].Value = info.YanlisSayisi;
        pars[7].Value = info.BosSayisi;
        pars[8].Value = info.OgrenciSayisi;
        pars[9].Value = info.KitapcikTuru;
        pars[10].Value = info.KazanimNo;
        pars[11].Value = info.Kazanim;
        pars[12].Value = info.Id;
        helper.ExecuteNonQuery(sql, pars);
    }
}

