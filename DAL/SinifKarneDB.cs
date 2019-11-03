using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using DAL;
using MySql.Data.MySqlClient;
public class SinifKarneInfo
{
    public int Id { get; set; }
    public int SinavId { get; set; }
    public int KurumKodu { get; set; }
    public int Sinif { get; set; }
    public int Brans { get; set; }
    public int Soru { get; set; }
    public int Dogru { get; set; }
    public int Yanlis { get; set; }
    public int Bos { get; set; }
    public string Sube { get; set; }
    public string KitapcikTuru { get; set; }
    public SinifKarneInfo()
    { }

    public SinifKarneInfo(int dogru, int yanlis, int bos)
    {
        Dogru = dogru;
        Yanlis = yanlis;
        Bos = bos;
    }
}

public class SinifKarneDB
{
    readonly HelperDb helper = new HelperDb();

    public DataTable KayitlariGetir()
    {
        const string sql = "select * from sinifkarne order by Id asc";
        return helper.ExecuteDataSet(sql).Tables[0];
    }

    public SinifKarneInfo KayitBilgiGetir(string cmdText, params MySqlParameter[] param)
    {
        MySqlDataReader dr = helper.ExecuteReader(cmdText, param);
        SinifKarneInfo info = new SinifKarneInfo();
        while (dr.Read())
        {
            info.Id = dr.GetMySayi("Id");
            info.SinavId = dr.GetMySayi("SinavId");
            info.KurumKodu = dr.GetMySayi("KurumKodu");
            info.Sinif = dr.GetMySayi("Sinif");
            info.Brans = dr.GetMySayi("Brans");
            info.Soru = dr.GetMySayi("Soru");
            info.Dogru = dr.GetMySayi("Dogru");
            info.Yanlis = dr.GetMySayi("Yanlis");
            info.Bos = dr.GetMySayi("Bos");
            info.Sube = dr.GetMyMetin("Sube");
            info.KitapcikTuru = dr.GetMyMetin("KitapcikTuru");
        }
        dr.Close();

        return info;
    }
    public SinifKarneInfo KayitBilgiGetir(int id)
    {
        string cmdText = "select * from sinifkarne where Id=?Id";
        MySqlParameter param = new MySqlParameter("?Id", MySqlDbType.Int32) { Value = id };
        MySqlDataReader dr = helper.ExecuteReader(cmdText, param);
        SinifKarneInfo info = new SinifKarneInfo();
        while (dr.Read())
        {
            info.Id = dr.GetMySayi("Id");
            info.SinavId = dr.GetMySayi("SinavId");
            info.KurumKodu = dr.GetMySayi("KurumKodu");
            info.Sinif = dr.GetMySayi("Sinif");
            info.Brans = dr.GetMySayi("Brans");
            info.Soru = dr.GetMySayi("Soru");
            info.Dogru = dr.GetMySayi("Dogru");
            info.Yanlis = dr.GetMySayi("Yanlis");
            info.Bos = dr.GetMySayi("Bos");
            info.Sube = dr.GetMyMetin("Sube");
            info.KitapcikTuru = dr.GetMyMetin("KitapcikTuru");
        }
        dr.Close();

        return info;
    }
    public List<SinifKarneInfo> DogruYanlisBosSayisi(int sinavId, int kurumKodu, int sinif, int brans, string sqlTxt)
    {
        try
        {
            sqlTxt = sqlTxt.Substring(0, sqlTxt.Length - 2);

            string sql = @"select Sum(Dogru) as Dogru from sinifkarne 
                        where SinavId=?SinavId and KurumKodu=?KurumKodu and Sinif=?Sinif and Brans=?Brans and (" + sqlTxt + ")";

            MySqlParameter[] param =
            {
                new MySqlParameter("?SinavId", MySqlDbType.Int32),
                new MySqlParameter("?KurumKodu", MySqlDbType.Int32),
                new MySqlParameter("?Sinif", MySqlDbType.Int32),
                new MySqlParameter("?Brans", MySqlDbType.Int32)
            };
            param[0].Value = sinavId;
            param[1].Value = kurumKodu;
            param[2].Value = sinif;
            param[3].Value = brans;
            DataTable veriler = helper.ExecuteDataSet(sql, param).Tables[0];

            return (from DataRow row in veriler.Rows select new SinifKarneInfo(Convert.ToInt32(row["Dogru"]), Convert.ToInt32(row["Yanlis"]), Convert.ToInt32(row["Bos"]))).ToList();
        }
        catch (Exception)
        {
            List<SinifKarneInfo> lst = new List<SinifKarneInfo> {new SinifKarneInfo(-1,-1,-1)};
            return lst;
        }

    }
    public List<SinifKarneInfo> DogruYanlisBosSayisi(int sinavId, int kurumKodu, int sinif, string sube, int brans, string sqlTxt)
    {
        try
        {
            sqlTxt = sqlTxt.Substring(0, sqlTxt.Length - 2);

            string sql = @"select Sum(Dogru) as Dogru,Sum(Yanlis) as Yanlis,Sum(Bos) as Bos from sinifkarne 
                        where SinavId=?SinavId and KurumKodu=?KurumKodu and Sinif=?Sinif and Sube=?Sube and Brans=?Brans and (" + sqlTxt + ")";

            MySqlParameter[] param =
            {
                new MySqlParameter("?SinavId", MySqlDbType.Int32),
                new MySqlParameter("?KurumKodu", MySqlDbType.Int32),
                new MySqlParameter("?Sinif", MySqlDbType.Int32),
                new MySqlParameter("?Sube", MySqlDbType.String),
                new MySqlParameter("?Brans", MySqlDbType.Int32)
            };
            param[0].Value = sinavId;
            param[1].Value = kurumKodu;
            param[2].Value = sinif;
            param[3].Value = sube;
            param[4].Value = brans;
            DataTable veriler = helper.ExecuteDataSet(sql, param).Tables[0];

            return (from DataRow row in veriler.Rows select new SinifKarneInfo(Convert.ToInt32(row["Dogru"]), Convert.ToInt32(row["Yanlis"]), Convert.ToInt32(row["Bos"]))).ToList();

        }
        catch (Exception)
        {
            List<SinifKarneInfo> lst = new List<SinifKarneInfo> { new SinifKarneInfo(-1, -1,-1) };
            return lst;

        }
    }
    public List<SinifKarneInfo> IlceDogruSayisi(int sinavId, int ilceId, int sinif, int brans, string sqlTxt)
    {
        try
        {
            sqlTxt = sqlTxt.Substring(0, sqlTxt.Length - 2);
            string sql = @"select Sum(Dogru) as Dogru,Sum(Yanlis) as Yanlis,Sum(Bos) as Bos from sinifkarne 
                         inner join kurumlar on kurumlar.KurumKodu = sinifkarne.KurumKodu and kurumlar.IlceId = ?IlceId
                        where SinavId=?SinavId  and Sinif=?Sinif and Brans=?Brans and (" + sqlTxt + ")";

            MySqlParameter[] param =
            {
                new MySqlParameter("?SinavId", MySqlDbType.Int32),
                new MySqlParameter("?IlceId", MySqlDbType.Int32),
                new MySqlParameter("?Sinif", MySqlDbType.Int32),
                new MySqlParameter("?Brans", MySqlDbType.Int32)
            };
            param[0].Value = sinavId;
            param[1].Value = ilceId;
            param[2].Value = sinif;
            param[3].Value = brans;
            DataTable veriler = helper.ExecuteDataSet(sql, param).Tables[0];

            return (from DataRow row in veriler.Rows select new SinifKarneInfo(Convert.ToInt32(row["Dogru"]), Convert.ToInt32(row["Yanlis"]), Convert.ToInt32(row["Bos"]))).ToList();
        }
        catch (Exception)
        {
            List<SinifKarneInfo> lst = new List<SinifKarneInfo> { new SinifKarneInfo(-1,-1,-1) };
            return lst;
        }

    }
    public void KayitSil(int sinavId)
    {
        const string sql = "delete from sinifkarne where SinavId=?SinavId";
        MySqlParameter p = new MySqlParameter("?SinavId", MySqlDbType.Int32) { Value = sinavId };
        helper.ExecuteNonQuery(sql, p);
    }
    public void KayitSil(int sinavId,int kurumKodu)
    {
        const string sql = "delete from sinifkarne where SinavId=?SinavId and KurumKodu=?KurumKodu";
        MySqlParameter[] pars =
        {
            new MySqlParameter("?SinavId", MySqlDbType.Int32),
            new MySqlParameter("?KurumKodu", MySqlDbType.Int32)
        };
        pars[0].Value = sinavId;
        pars[1].Value = kurumKodu;
        helper.ExecuteNonQuery(sql, pars);
    }

    public void KayitEkle(SinifKarneInfo info)
    {
        const string sql = @"insert into sinifkarne (SinavId,KurumKodu,Sinif,Brans,Soru,Dogru,Yanlis,Bos,Sube,KitapcikTuru) values (?SinavId,?KurumKodu,?Sinif,?Brans,?Soru,?Dogru,?Yanlis,?Bos,?Sube,?KitapcikTuru)";
        MySqlParameter[] pars =
        {
             new MySqlParameter("?SinavId", MySqlDbType.Int32),
             new MySqlParameter("?KurumKodu", MySqlDbType.Int32),
             new MySqlParameter("?Sinif", MySqlDbType.Int32),
             new MySqlParameter("?Brans", MySqlDbType.Int32),
             new MySqlParameter("?Soru", MySqlDbType.Int32),
             new MySqlParameter("?Dogru", MySqlDbType.Int32),
             new MySqlParameter("?Yanlis", MySqlDbType.Int32),
             new MySqlParameter("?Bos", MySqlDbType.Int32),
             new MySqlParameter("?Sube", MySqlDbType.String),
             new MySqlParameter("?KitapcikTuru", MySqlDbType.String)
        };
        pars[0].Value = info.SinavId;
        pars[1].Value = info.KurumKodu;
        pars[2].Value = info.Sinif;
        pars[3].Value = info.Brans;
        pars[4].Value = info.Soru;
        pars[5].Value = info.Dogru;
        pars[6].Value = info.Yanlis;
        pars[7].Value = info.Bos;
        pars[8].Value = info.Sube;
        pars[9].Value = info.KitapcikTuru;
        helper.ExecuteNonQuery(sql, pars);
    }

    public void KayitGuncelle(SinifKarneInfo info)
    {
        const string sql = @"update sinifkarne set SinavId=?SinavId,KurumKodu=?KurumKodu,Sinif=?Sinif,Brans=?Brans,Soru=?Soru,Dogru=?Dogru,Yanlis=?Yanlis,Bos=?Bos,Sube=?Sube,KitapcikTuru=?KitapcikTuru where Id=?Id";
        MySqlParameter[] pars =
        {
 new MySqlParameter("?SinavId", MySqlDbType.Int32),
 new MySqlParameter("?KurumKodu", MySqlDbType.Int32),
 new MySqlParameter("?Sinif", MySqlDbType.Int32),
 new MySqlParameter("?Brans", MySqlDbType.Int32),
 new MySqlParameter("?Soru", MySqlDbType.Int32),
 new MySqlParameter("?Dogru", MySqlDbType.Int32),
 new MySqlParameter("?Yanlis", MySqlDbType.Int32),
 new MySqlParameter("?Bos", MySqlDbType.Int32),
 new MySqlParameter("?Sube", MySqlDbType.String),
 new MySqlParameter("?KitapcikTuru", MySqlDbType.String),
 new MySqlParameter("?Id", MySqlDbType.Int32),
};
        pars[0].Value = info.SinavId;
        pars[1].Value = info.KurumKodu;
        pars[2].Value = info.Sinif;
        pars[3].Value = info.Brans;
        pars[4].Value = info.Soru;
        pars[5].Value = info.Dogru;
        pars[6].Value = info.Yanlis;
        pars[7].Value = info.Bos;
        pars[8].Value = info.Sube;
        pars[9].Value = info.KitapcikTuru;
        pars[10].Value = info.Id;
        helper.ExecuteNonQuery(sql, pars);
    }
}

