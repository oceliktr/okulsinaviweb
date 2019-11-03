using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using DAL;
using MySql.Data.MySqlClient;


public class KutukIslemleriInfo
{
    public int Id { get; set; }
    public string Ilce { get; set; }
    public string KurumKodu { get; set; }
    public string KurumAdi { get; set; }
    public string TcKimlik { get; set; }
    public string Adi { get; set; }
    public string Soyadi { get; set; }
    public string OkulNo { get; set; }
    public string Sinif { get; set; }
    public string Sube { get; set; }
    public int OkulSayisi { get; set; }
    public int OgrenciSayisi { get; set; }

    public KutukIslemleriInfo()
    {
        
    }
    public KutukIslemleriInfo(string ilce, string kurumKodu, string kurumAdi, string tcKimlik, string adi, string soyadi, string okulNo, string sinif, string sube)
    {
        Ilce = ilce;
        KurumKodu = kurumKodu;
        KurumAdi = kurumAdi;
        TcKimlik = tcKimlik;
        Adi = adi;
        Soyadi = soyadi;
        OkulNo = okulNo;
        Sinif = sinif;
        Sube = sube;
    }
    public KutukIslemleriInfo(string ilce, string kurumKodu, string kurumAdi)
    {
        Ilce = ilce;
        KurumKodu = kurumKodu;
        KurumAdi = kurumAdi;
    }
}

public class KutukIslemleriDB
{
    readonly HelperDb helper = new HelperDb();

    public DataTable KayitlariGetir()
    {
        const string sql = "select * from kutukislemleri order by Id asc";
        return helper.ExecuteDataSet(sql).Tables[0];
    }
    public DataTable EklenenKurumlariGetir()
    {
        const string sql = "select DISTINCT(kutukislemleri.KurumKodu), kutukislemleri.Ilce,kutukislemleri.KurumAdi from kutukislemleri order by Id desc";
        return helper.ExecuteDataSet(sql).Tables[0];
    }
    public DataTable SubeleriGetir()
    {
        const string sql = "select DISTINCT(KurumKodu),Ilce,KurumAdi,Sube,Sinif from kutukislemleri order by Ilce,KurumAdi,Sube";
        return helper.ExecuteDataSet(sql).Tables[0];
    }
    public DataTable OkullariGetir()
    {
        const string sql = "select DISTINCT(KurumKodu),Ilce,KurumAdi,Sinif from kutukislemleri order by Ilce,KurumAdi";
        return helper.ExecuteDataSet(sql).Tables[0];
    }
    public KutukIslemleriInfo KayitBilgiGetir(string cmdText, params MySqlParameter[] param)
    {
        MySqlDataReader dr = helper.ExecuteReader(cmdText, param);
        KutukIslemleriInfo info = new KutukIslemleriInfo();
        while (dr.Read())
        {
            info.Id = dr.GetMySayi("Id");
            info.Ilce = dr.GetMyMetin("Ilce");
            info.KurumKodu = dr.GetMyMetin("KurumKodu");
            info.KurumAdi = dr.GetMyMetin("KurumAdi");
            info.TcKimlik = dr.GetMyMetin("TcKimlik");
            info.Adi = dr.GetMyMetin("Adi");
            info.Soyadi = dr.GetMyMetin("Soyadi");
            info.OkulNo = dr.GetMyMetin("OkulNo");
            info.Sinif = dr.GetMyMetin("Sinif");
            info.Sube = dr.GetMyMetin("Sube");
        }
        dr.Close();

        return info;
    }

    public KutukIslemleriInfo KayitBilgiGetir(int id)
    {
        const string sql = "select * from kutukislemleri where Id=?Id";
        MySqlParameter p = new MySqlParameter("?Id", MySqlDbType.Int32) { Value = id };
        MySqlDataReader dr = helper.ExecuteReader(sql, p);
        KutukIslemleriInfo info = new KutukIslemleriInfo();
        while (dr.Read())
        {
            info.Id = dr.GetMySayi("Id");
            info.Ilce = dr.GetMyMetin("Ilce");
            info.KurumKodu = dr.GetMyMetin("KurumKodu");
            info.KurumAdi = dr.GetMyMetin("KurumAdi");
            info.TcKimlik = dr.GetMyMetin("TcKimlik");
            info.Adi = dr.GetMyMetin("Adi");
            info.Soyadi = dr.GetMyMetin("Soyadi");
            info.OkulNo = dr.GetMyMetin("OkulNo");
            info.Sinif = dr.GetMyMetin("Sinif");
            info.Sube = dr.GetMyMetin("Sube");
        }
        dr.Close();

        return info;
    }
    public void KayitSil()
    {
        const string sql = "delete from kutukislemleri";
        helper.ExecuteNonQuery(sql);
    }

    public void KayitSil(int id)
    {
        const string sql = "delete from kutukislemleri where Id=?Id";
        MySqlParameter p = new MySqlParameter("?Id", MySqlDbType.Int32) { Value = id };
        helper.ExecuteNonQuery(sql, p);
    }
    public void KayitSil(string kurumKodu)
    {
        const string sql = "delete from kutukislemleri where KurumKodu=?KurumKodu";
        MySqlParameter p = new MySqlParameter("?KurumKodu", MySqlDbType.Int32) { Value = kurumKodu };
        helper.ExecuteNonQuery(sql, p);
    }
    public void KayitEkle(KutukIslemleriInfo info)
    {
        const string sql = @"insert into kutukislemleri (Ilce,KurumKodu,KurumAdi,TcKimlik,Adi,Soyadi,OkulNo,Sinif,Sube) values (?Ilce,?KurumKodu,?KurumAdi,?TcKimlik,?Adi,?Soyadi,?OkulNo,?Sinif,?Sube)";
        MySqlParameter[] pars = {
                new MySqlParameter("?Ilce", MySqlDbType.VarChar),
                new MySqlParameter("?KurumKodu", MySqlDbType.VarChar),
                new MySqlParameter("?KurumAdi", MySqlDbType.VarChar),
                new MySqlParameter("?TcKimlik", MySqlDbType.VarChar),
                new MySqlParameter("?Adi", MySqlDbType.VarChar),
                new MySqlParameter("?Soyadi", MySqlDbType.VarChar),
                new MySqlParameter("?OkulNo", MySqlDbType.VarChar),
                new MySqlParameter("?Sinif", MySqlDbType.VarChar),
                new MySqlParameter("?Sube", MySqlDbType.VarChar),
            };
        pars[0].Value = info.Ilce;
        pars[1].Value = info.KurumKodu;
        pars[2].Value = info.KurumAdi;
        pars[3].Value = info.TcKimlik;
        pars[4].Value = info.Adi;
        pars[5].Value = info.Soyadi;
        pars[6].Value = info.OkulNo;
        pars[7].Value = info.Sinif;
        pars[8].Value = info.Sube;
        helper.ExecuteNonQuery(sql, pars);
    }
    public List<KutukIslemleriInfo> KayitlariDiziyeGetir()
    {
        string sql = "select * from kutukislemleri";
        DataTable veriler = helper.ExecuteDataSet(sql).Tables[0];
        return (from DataRow row in veriler.Rows select new KutukIslemleriInfo(row["Ilce"].ToString(), row["KurumKodu"].ToString(), row["KurumAdi"].ToString(), row["TcKimlik"].ToString(), row["Adi"].ToString(), row["Soyadi"].ToString(),row["OkulNo"].ToString(), row["Sinif"].ToString(), row["Sube"].ToString())).ToList();
    }
    public List<KutukIslemleriInfo> OkullariDiziyeGetir()
    {
        string sql = "select DISTINCT(KurumKodu),KurumAdi,Ilce from kutukislemleri";
        DataTable veriler = helper.ExecuteDataSet(sql).Tables[0];
        return (from DataRow row in veriler.Rows select new KutukIslemleriInfo(row["Ilce"].ToString(), row["KurumKodu"].ToString(), row["KurumAdi"].ToString())).ToList();
    }
    public void KayitGuncelle(KutukIslemleriInfo info)
    {
        const string sql = @"update kutukislemleri set Ilce=?Ilce,KurumKodu=?KurumKodu,KurumAdi=?KurumAdi,TcKimlik=?TcKimlik,Adi=?Adi,Soyadi=?Soyadi,OkulNo=?OkulNo,Sinif=?Sinif,Sube=?Sube where Id=?Id";
        MySqlParameter[] pars =
        {
                new MySqlParameter("?Ilce", MySqlDbType.VarChar),
                new MySqlParameter("?KurumKodu", MySqlDbType.VarChar),
                new MySqlParameter("?KurumAdi", MySqlDbType.VarChar),
                new MySqlParameter("?TcKimlik", MySqlDbType.VarChar),
                new MySqlParameter("?Adi", MySqlDbType.VarChar),
                new MySqlParameter("?Soyadi", MySqlDbType.VarChar),
                new MySqlParameter("?OkulNo", MySqlDbType.VarChar),
                new MySqlParameter("?Sinif", MySqlDbType.VarChar),
                new MySqlParameter("?Sube", MySqlDbType.VarChar),
                new MySqlParameter("?Id", MySqlDbType.Int32),
            };
        pars[0].Value = info.Ilce;
        pars[1].Value = info.KurumKodu;
        pars[2].Value = info.KurumAdi;
        pars[3].Value = info.TcKimlik;
        pars[4].Value = info.Adi;
        pars[5].Value = info.Soyadi;
        pars[6].Value = info.OkulNo;
        pars[7].Value = info.Sinif;
        pars[8].Value = info.Sube;
        pars[9].Value = info.Id;
        helper.ExecuteNonQuery(sql, pars);
    }
    public int OkulOgrenciSayisi(string kurumKodu)
    {
        try
        {
            string sql = "select Count(Id) from kutukislemleri where KurumKodu=?KurumKodu";
            MySqlParameter[] pars =
            {
                    new MySqlParameter("?KurumKodu", MySqlDbType.VarChar)
                };
            pars[0].Value = kurumKodu;
            int mesajSayisi = Convert.ToInt32(helper.ExecuteScalar(sql, pars));
            return mesajSayisi;
        }
        catch (Exception)
        {
            return 0;
        }
    }
    public int SubeOgrenciSayisi(string kurumKodu, string sube)
    {
            string sql = "select Count(Id) from kutukislemleri where KurumKodu=?KurumKodu and Sube=?Sube";
            MySqlParameter[] pars =
            {
                    new MySqlParameter("?KurumKodu", MySqlDbType.VarChar),
                    new MySqlParameter("?Sube", MySqlDbType.VarChar)
                };
            pars[0].Value = kurumKodu;
            pars[1].Value = sube;
            int mesajSayisi = Convert.ToInt32(helper.ExecuteScalar(sql, pars));
            return mesajSayisi;
   
    }
    public int SubeSayisi(string kurumKodu)
    {
            string sql = "SELECT COUNT(DISTINCT(Sube)) AS SubeSayisi FROM kutukislemleri where KurumKodu=?KurumKodu";
            MySqlParameter[] pars =
            {
                    new MySqlParameter("?KurumKodu", MySqlDbType.VarChar)
                };
            pars[0].Value = kurumKodu;
            int mesajSayisi = Convert.ToInt32(helper.ExecuteScalar(sql, pars));
            return mesajSayisi;
        
    }
    public KutukIslemleriInfo OkulOgrenciSayisi()
    {
        string sql = "select COUNT(Id) as OgrenciSayisi,COUNT(DISTINCT(KurumKodu)) as OkulSayisi from kutukislemleri";
        MySqlDataReader dr = helper.ExecuteReader(sql);
        KutukIslemleriInfo info = new KutukIslemleriInfo();
        while (dr.Read())
        {
            info.OkulSayisi = dr.GetMySayi("OkulSayisi");
            info.OgrenciSayisi = dr.GetMySayi("OgrenciSayisi");
        }
        dr.Close();
        return info;
    }
}
