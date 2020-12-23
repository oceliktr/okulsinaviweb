using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using MySql.Data.MySqlClient;

public class KullanicilarInfo
{
    public int Id { get; set; }
    public string Sifre { get; set; }
    public string AdiSoyadi { get; set; }
    public string KurumKodu { get; set; }
    public string TcKimlik { get; set; }
    public string Email { get; set; }
    public string CepTlf { get; set; }
    public int IlceId { get; set; }
    public string IlceAdi { get; set; } //detaylý bilgi sorgusu için
    public string KurumAdi { get; set; }//detaylý bilgi sorgusu için
    public string BransAdi { get; set; }//detaylý bilgi sorgusu için
    public int Bransi { get; set; }
    public string Yetki { get; set; }
    public int Sahip { get; set; }
    public DateTime OncekiGiris { get; set; }
    public DateTime SonGiris { get; set; }
    public int GirisSayisi { get; set; }
    public KullanicilarInfo()
    {

    }
    public KullanicilarInfo(int id, int bransi, string yetki)
    {
        Id = id;
        Bransi = bransi;
        Yetki = yetki;
    }
    public KullanicilarInfo(string kurumKodu)
    {
        Yetki = kurumKodu;
    }
}
public class KullanicilarDb
{
    readonly HelperDb _helper = new HelperDb();

    public DataTable SoruYazarlariniGetir()
    {
        const string sql = @"SELECT u.Id,u.AdiSoyadi,u.Email,u.CepTlf,b.BransAdi,(SELECT COUNT(lgssorular.Id) FROM lgssorular WHERE lgssorular.KullaniciId=u.Id) AS SoruSayisi FROM kullanicilar AS u
                            LEFT JOIN branslar AS b ON b.Id = u.Bransi
                            WHERE u.Yetki like Concat('%LgsYazari%')";

        return _helper.ExecuteDataSet(sql).Tables[0];
    }
    public DataTable OgretmenleriGetir(string yetki, int bransId)
    {
        string sql = string.Format("select * from kullanicilar where Yetki like '%{0}%' and Bransi=?Bransi order by Bransi,AdiSoyadi asc", yetki);
        MySqlParameter p = new MySqlParameter("?Bransi", MySqlDbType.Int32)
        {
            Value = bransId
        };
        return _helper.ExecuteDataSet(sql, p).Tables[0];
    }
    public DataTable KayitlariGetir(int ilceId, string kurumKodu, int brans, string yetki)
    {
        string sql = "select * from kullanicilar where Id<>0";

        if (ilceId != 0)
            sql += " and IlceId=?IlceId";
        if (kurumKodu != "")
            sql += " and KurumKodu=?KurumKodu";
        if (brans != 0)
            sql += " and Bransi=?Bransi";
        if (yetki != "" && yetki != "Sahip")
            sql += " and Yetki like '%" + yetki + "%'";
        if (yetki == "Sahip")
            sql += " and Sahip=1";

        sql += " order by AdiSoyadi,KurumKodu,IlceId asc";

        MySqlParameter[] p =
        {
                new MySqlParameter("?IlceId", MySqlDbType.Int32),
                new MySqlParameter("?KurumKodu", MySqlDbType.String),
                new MySqlParameter("?Bransi", MySqlDbType.Int32)
            };

        p[0].Value = ilceId;
        p[1].Value = kurumKodu;
        p[2].Value = brans;
        return _helper.ExecuteDataSet(sql, p).Tables[0];
    }
    public List<KullanicilarInfo> KayitlariDiziyeGetir(string yetki)
    {
        string sql = "select * from kullanicilar where Yetki like Concat('%',?Yetki,'%')";
        MySqlParameter p = new MySqlParameter("?Yetki", MySqlDbType.String) { Value = yetki };

        DataTable veriler = _helper.ExecuteDataSet(sql, p).Tables[0];

        return (from DataRow row in veriler.Rows select new KullanicilarInfo(row["KurumKodu"].ToString())).ToList();
    }
    public List<KullanicilarInfo> KayitlariDiziyeGetir(int bransi, string yetki)
    {
        string sql = "select * from kullanicilar where Bransi=?Bransi and Yetki like Concat('%',?Yetki,'%')";
        MySqlParameter[] p =
        {
                new MySqlParameter("?Bransi", MySqlDbType.Int32),
                new MySqlParameter("?Yetki",MySqlDbType.String)
            };
        p[0].Value = bransi;
        p[1].Value = yetki;

        DataTable veriler = _helper.ExecuteDataSet(sql, p).Tables[0];

        return (from DataRow row in veriler.Rows select new KullanicilarInfo(Convert.ToInt32(row["Id"]), Convert.ToInt32(row["Bransi"]), row["Yetki"].ToString())).ToList();
    }
    public List<KullanicilarInfo> KayitlariDiziyeGetir(int bransi, string yetki, string grup)
    {
        string sql = "select * from kullanicilar where Bransi=?Bransi and Yetki like Concat('%',?Yetki,'%') and Grup=?Grup";
        MySqlParameter[] p =
        {
                new MySqlParameter("?Bransi", MySqlDbType.Int32),
                new MySqlParameter("?Yetki",MySqlDbType.String),
                new MySqlParameter("?Grup",MySqlDbType.String)
            };
        p[0].Value = bransi;
        p[1].Value = yetki;
        p[2].Value = grup;

        DataTable veriler = _helper.ExecuteDataSet(sql, p).Tables[0];

        return (from DataRow row in veriler.Rows select new KullanicilarInfo(Convert.ToInt32(row["Id"]), Convert.ToInt32(row["Bransi"]), row["Yetki"].ToString())).ToList();
    }
    public KullanicilarInfo KayitBilgiGetir(string tcKimlik)
    {
        const string sqlText = "select * from kullanicilar where (TcKimlik=?TcKimlik)";
        MySqlParameter p = new MySqlParameter("?TcKimlik", MySqlDbType.String)
        {
            Value = tcKimlik
        };
        MySqlDataReader dr = _helper.ExecuteReader(sqlText, p);
        KullanicilarInfo info = TabloAlanlar(dr);

        return info;
    }
    public KullanicilarInfo KayitBilgiGetir(int ilce, int kurumId, string uyeKurumKodu, string tcKimlik)
    {
        const string sqlText = "select * from kullanicilar INNER JOIN kurumlar on kurumlar.Id=?Id where (kullanicilar.IlceId=?IlceId and kullanicilar.KurumKodu=?KurumKodu and kullanicilar.TcKimlik=?TcKimlik)";
        MySqlParameter[] p =
        {
                new MySqlParameter("?IlceId", MySqlDbType.Int32),
                new MySqlParameter("?Id", MySqlDbType.Int32),
                new MySqlParameter("?KurumKodu", MySqlDbType.String),
                new MySqlParameter("?TcKimlik", MySqlDbType.String)
            };
        p[0].Value = ilce;
        p[1].Value = kurumId;
        p[2].Value = uyeKurumKodu;
        p[3].Value = tcKimlik;
        MySqlDataReader dr = _helper.ExecuteReader(sqlText, p);
        KullanicilarInfo info = TabloAlanlar(dr);

        return info;
    }
    public KullanicilarInfo KayitBilgiGetir(int id)
    {
        string cmdText = "select * from kullanicilar where Id=?Id";
        MySqlParameter param = new MySqlParameter("?Id", MySqlDbType.Int32) { Value = id };
        MySqlDataReader dr = _helper.ExecuteReader(cmdText, param);
        KullanicilarInfo info = TabloAlanlar(dr);

        return info;
    }
    public KullanicilarInfo KayitBilgiGetir(int id, bool detayli)
    {
        string cmdText = @"SELECT i.IlceAdi,k.KurumAdi, b.BransAdi,usr.* FROM kullanicilar AS usr
                                left JOIN branslar AS b ON usr.Bransi = b.Id
                                left JOIN kurumlar AS k ON k.KurumKodu = usr.KurumKodu
                                left JOIN ilceler AS i ON i.Id = k.IlceId
                                WHERE usr.Id =?Id";
        MySqlParameter param = new MySqlParameter("?Id", MySqlDbType.Int32) { Value = id };
        MySqlDataReader dr = _helper.ExecuteReader(cmdText, param);
        KullanicilarInfo info = TabloAlanlar(dr);

        return info;
    }
    public KullanicilarInfo KayitBilgiGetir(string tcKimlik, string sifre)
    {
        const string sqlText = "select * from kullanicilar where (TcKimlik=?TcKimlik and Sifre=?Sifre)";
        MySqlParameter[] pars =
        {
                new MySqlParameter("?TcKimlik", MySqlDbType.String),
                new MySqlParameter("?Sifre", MySqlDbType.String)
            };
        pars[0].Value = tcKimlik;
        pars[1].Value = sifre;
        MySqlDataReader dr = _helper.ExecuteReader(sqlText, pars);
        KullanicilarInfo info = TabloAlanlar(dr);

        return info;
    }
    private static KullanicilarInfo TabloAlanlar(MySqlDataReader dr)
    {
        KullanicilarInfo info = new KullanicilarInfo();
        while (dr.Read())
        {
            info.Id = dr.GetMySayi("Id");
            info.Sifre = dr.GetMyMetin("Sifre");
            info.KurumKodu = dr.GetMyMetin("KurumKodu");
            info.TcKimlik = dr.GetMyMetin("TcKimlik");
            info.AdiSoyadi = dr.GetMyMetin("AdiSoyadi");
            info.Email = dr.GetMyMetin("Email");
            info.CepTlf = dr.GetMyMetin("CepTlf");
            info.IlceId = dr.GetMySayi("IlceId");
            info.Bransi = dr.GetMySayi("Bransi");
            info.Yetki = dr.GetMyMetin("Yetki");
            info.Sahip = dr.GetMySayi("Sahip");
            info.OncekiGiris = dr.GetMyTarih("OncekiGiris");
            info.SonGiris = dr.GetMyTarih("SonGiris");
            info.GirisSayisi = dr.GetMySayi("GirisSayisi");

            info.IlceAdi = dr.GetMyMetin("IlceAdi");
            info.BransAdi = dr.GetMyMetin("BransAdi");
            info.KurumAdi = dr.GetMyMetin("KurumAdi");
        }
        dr.Close();
        return info;
    }
    public void KayitSil(int id)
    {
        const string sql = "delete from kullanicilar where Id=?Id";
        MySqlParameter p = new MySqlParameter("?Id", MySqlDbType.Int32) { Value = id };
        _helper.ExecuteNonQuery(sql, p);
    }
    public void KayitEkle(KullanicilarInfo info)
    {
        const string sql = @"insert into kullanicilar (TcKimlik,Sifre,KurumKodu,Email,IlceId,Yetki,AdiSoyadi,Bransi,CepTlf,OncekiGiris,SonGiris,Sahip) values (?TcKimlik,?Sifre,?KurumKodu,?Email,?IlceId,?Yetki,?AdiSoyadi,?Bransi,?CepTlf,?OncekiGiris,?SonGiris,?Sahip)";
        MySqlParameter[] pars =
        {
                new MySqlParameter("?TcKimlik", MySqlDbType.String),
                new MySqlParameter("?Sifre", MySqlDbType.String),
                new MySqlParameter("?KurumKodu", MySqlDbType.String),
                new MySqlParameter("?Email", MySqlDbType.String),
                new MySqlParameter("?IlceId", MySqlDbType.Int32),
                new MySqlParameter("?Yetki", MySqlDbType.String),
                new MySqlParameter("?AdiSoyadi", MySqlDbType.String),
                new MySqlParameter("?Bransi", MySqlDbType.String),
                new MySqlParameter("?CepTlf", MySqlDbType.String),
                new MySqlParameter("?OncekiGiris", MySqlDbType.DateTime),
                new MySqlParameter("?SonGiris", MySqlDbType.DateTime),
                new MySqlParameter("?Sahip", MySqlDbType.Int32)
            };
        pars[0].Value = info.TcKimlik;
        pars[1].Value = info.Sifre;
        pars[2].Value = info.KurumKodu;
        pars[3].Value = info.Email;
        pars[4].Value = info.IlceId;
        pars[5].Value = info.Yetki;
        pars[6].Value = info.AdiSoyadi;
        pars[7].Value = info.Bransi;
        pars[8].Value = info.CepTlf;
        pars[9].Value = GenelIslemler.YerelTarih();
        pars[10].Value = GenelIslemler.YerelTarih();
        pars[11].Value = info.Sahip;
        _helper.ExecuteNonQuery(sql, pars);
    }
    public void KayitGuncelle(KullanicilarInfo info)
    {
        const string sql = @"update kullanicilar set Sifre=?Sifre,KurumKodu=?KurumKodu,Email=?Email,IlceId=?IlceId,Yetki=?Yetki,AdiSoyadi=?AdiSoyadi,Bransi=?Bransi,TcKimlik=?TcKimlik,CepTlf=?CepTlf,Sahip=?Sahip where Id=?Id";
        MySqlParameter[] pars =
        {
                new MySqlParameter("?Sifre", MySqlDbType.String),
                new MySqlParameter("?KurumKodu", MySqlDbType.String),
                new MySqlParameter("?Email", MySqlDbType.String),
                new MySqlParameter("?IlceId", MySqlDbType.Int32),
                new MySqlParameter("?Yetki", MySqlDbType.String),
                new MySqlParameter("?AdiSoyadi", MySqlDbType.String),
                new MySqlParameter("?Bransi", MySqlDbType.String),
                new MySqlParameter("?TcKimlik", MySqlDbType.String),
                new MySqlParameter("?CepTlf", MySqlDbType.String),
                new MySqlParameter("?Sahip", MySqlDbType.Int32),
                new MySqlParameter("?Id", MySqlDbType.Int32)
            };
        pars[0].Value = info.Sifre;
        pars[1].Value = info.KurumKodu;
        pars[2].Value = info.Email;
        pars[3].Value = info.IlceId;
        pars[4].Value = info.Yetki;
        pars[5].Value = info.AdiSoyadi;
        pars[6].Value = info.Bransi;
        pars[7].Value = info.TcKimlik;
        pars[8].Value = info.CepTlf;
        pars[9].Value = info.Sahip;
        pars[10].Value = info.Id;
        _helper.ExecuteNonQuery(sql, pars);

    }
    public void KullaniciBilgiGuncelle(KullanicilarInfo info)
    {
        const string sql = @"update kullanicilar set Sifre=?Sifre,Email=?Email,CepTlf=?CepTlf,AdiSoyadi=?AdiSoyadi where Id=?Id";
        MySqlParameter[] pars =
        {
                new MySqlParameter("?Sifre", MySqlDbType.String),
                new MySqlParameter("?Email", MySqlDbType.String),
                new MySqlParameter("?CepTlf", MySqlDbType.String),
                new MySqlParameter("?AdiSoyadi", MySqlDbType.String),
                new MySqlParameter("?Id", MySqlDbType.Int32)
            };
        pars[0].Value = info.Sifre;
        pars[1].Value = info.Email;
        pars[2].Value = info.CepTlf;
        pars[3].Value = info.AdiSoyadi;
        pars[4].Value = info.Id;
        _helper.ExecuteNonQuery(sql, pars);

    }
    public void KayitGuncelle(int girisSayisi, int id, DateTime oncekiGiris)
    {
        string sql = @"update kullanicilar set SonGiris=?SonGiris,OncekiGiris=?OncekiGiris,GirisSayisi=?GirisSayisi where Id=?Id";
        MySqlParameter[] pars =
        {
                new MySqlParameter("?SonGiris", MySqlDbType.DateTime),
                new MySqlParameter("?OncekiGiris", MySqlDbType.DateTime),
                new MySqlParameter("?GirisSayisi", MySqlDbType.Int32),
                new MySqlParameter("?Id", MySqlDbType.Int32),
            };
        pars[0].Value = GenelIslemler.YerelTarih();
        pars[1].Value = oncekiGiris;
        pars[2].Value = girisSayisi;
        pars[3].Value = id;
        _helper.ExecuteNonQuery(sql, pars);
    }
    public void KayitGuncelle(int id, string tcKimlik, string sifre)
    {
        const string sql = @"update kullanicilar set TcKimlik=?TcKimlik,Sifre=?Sifre where Id=?Id";
        MySqlParameter[] pars =
        {
                new MySqlParameter("?TcKimlik", MySqlDbType.String),
                new MySqlParameter("?Sifre", MySqlDbType.String),
                new MySqlParameter("?Id", MySqlDbType.Int32)
            };
        pars[0].Value = tcKimlik;
        pars[1].Value = sifre;
        pars[2].Value = id;
        _helper.ExecuteNonQuery(sql, pars);
    }
    public void KayitGuncelle(int id, string sifre)
    {
        const string sql = @"update kullanicilar set Sifre=?Sifre where Id=?Id";
        MySqlParameter[] pars =
        {
                new MySqlParameter("?Sifre", MySqlDbType.String),
                new MySqlParameter("?Id", MySqlDbType.Int32)
            };
        pars[0].Value = sifre;
        pars[1].Value = id;
        _helper.ExecuteNonQuery(sql, pars);
    }
    public void KayitGrupGuncelle(int id, string grup)
    {
        const string sql = @"update kullanicilar set Grup=?Grup where Id=?Id";
        MySqlParameter[] pars =
        {
                new MySqlParameter("?Grup", MySqlDbType.String),
                new MySqlParameter("?Id", MySqlDbType.Int32)
            };
        pars[0].Value = grup;
        pars[1].Value = id;
        _helper.ExecuteNonQuery(sql, pars);
    }
    public bool KayitKontrol(string tcKimlik, string sifre)
    {
        const string cmdText = "select count(Id) from kullanicilar where TcKimlik=?TcKimlik and Sifre=?Sifre";
        MySqlParameter[] pars =
        {
                new MySqlParameter("?TcKimlik", MySqlDbType.String),
                new MySqlParameter("?Sifre", MySqlDbType.String)
            };
        pars[0].Value = tcKimlik;
        pars[1].Value = sifre;
        bool sonuc = Convert.ToInt32(_helper.ExecuteScalar(cmdText, pars)) > 0;
        return sonuc;
    }
    public bool KayitKontrol(string tcKimlik, int x)
    {
        string cmdText = "select count(Id) from kullanicilar where TcKimlik=?TcKimlik";
        MySqlParameter[] pars =
        {
                new MySqlParameter("?TcKimlik", MySqlDbType.String)
            };
        pars[0].Value = tcKimlik;
        bool sonuc = Convert.ToInt32(_helper.ExecuteScalar(cmdText, pars)) > 0;
        return sonuc;
    }
    public bool KayitKontrol(int id)
    {
        string cmdText = "select count(Id) from kullanicilar where Id=?Id";
        MySqlParameter[] pars =
        {
                new MySqlParameter("?Id", MySqlDbType.Int32)
            };
        pars[0].Value = id;
        bool sonuc = Convert.ToInt32(_helper.ExecuteScalar(cmdText, pars)) > 0;
        return sonuc;
    }
    public void YeniSifreOlustur(int id, string ozelKod)
    {
        const string sql = @"update Kullanicilar set Sifre=?Sifre where Id=?Id";
        MySqlParameter[] pars =
        {
                new MySqlParameter("?Sifre", MySqlDbType.String),
                new MySqlParameter("?Id", MySqlDbType.Int32)
            };
        pars[0].Value = ozelKod;
        pars[1].Value = id;
        _helper.ExecuteNonQuery(sql, pars);
    }
}
