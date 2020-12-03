using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using Newtonsoft.Json;

/// <summary>
/// Summary description for TestKutukDb
/// </summary>
public class TestKutukDb
{
    private readonly HelperDb helper = new HelperDb();

    public DataTable KayitlariGetir()
    {
        const string sql = "select * from testkutuk order by Id asc";
        return helper.ExecuteDataSet(sql).Tables[0];
    }
    public DataTable OgrenciAra(string aranan)
    {
         string sql = string.Format("select * from testkutuk AS k WHERE k.OpaqId LIKE '%{0}%' OR IlceAdi LIKE '%{1}%' OR Adi LIKE '%{1}%' OR Soyadi LIKE '%{1}%' OR KurumKodu LIKE '%{1}%'",aranan.Md5Sifrele(),aranan);
        return helper.ExecuteDataSet(sql).Tables[0];
    }
    public DataTable OgrenciAra(int donem,string opaqId, string ogrAdi, string ogrSoyadi)
    {
        string sql = @"SELECT k.*,ok.KurumAdi from testkutuk AS k 
                                    LEFT JOIN kurumlar AS ok ON  ok.KurumKodu=k.KurumKodu 
                                    WHERE k.DonemId=" + donem+" and (k.Id=0";
        if (opaqId != "")
            sql += @" or k.OpaqId = '" + opaqId + "'";
        if(ogrAdi!="")
            sql += " or k.Adi LIKE '%" + ogrAdi + "%'";
        if (ogrSoyadi != "")
            sql += " or k.Soyadi LIKE '%"+ ogrSoyadi+"%'";

        sql += ")";

        return helper.ExecuteDataSet(sql).Tables[0];
    }
    public DataTable KayitlariGetir(string ilceAdi, int sinif)
    {
        const string sql = "select DISTINCT KurumKodu,IlceAdi from testkutuk where IlceAdi=?IlceAdi and Sinifi=?Sinifi order by Adi,Soyadi asc";
        MySqlParameter[] pars =
        {
                new MySqlParameter("?IlceAdi", MySqlDbType.String),
                new MySqlParameter("?Sinifi", MySqlDbType.Int32)
            };
        pars[0].Value = ilceAdi;
        pars[1].Value = sinif;

        var table = helper.ExecuteDataSet(sql, pars).Tables[0];
        return table;
    }

    public DataTable KayitlariGetir(int kurumKodu, int sinif)
    {
        const string sql = "select * from testkutuk where KurumKodu=?KurumKodu and Sinifi=?Sinifi order by Sinifi,Sube,Adi,Soyadi asc";
        MySqlParameter[] pars =
        {
            new MySqlParameter("?KurumKodu", MySqlDbType.Int32),
            new MySqlParameter("?Sinifi", MySqlDbType.Int32)
        };
        pars[0].Value = kurumKodu;
        pars[1].Value = sinif;

        var table = helper.ExecuteDataSet(sql, pars).Tables[0];
        return table;
    }

    public DataTable KayitlariGetir(int donem,int kurumKodu, int sinif)
    {
        const string sql = "select * from testkutuk where DonemId=?Donem and KurumKodu=?KurumKodu and Sinifi=?Sinifi order by Sinifi,Sube,Adi,Soyadi asc";
        MySqlParameter[] pars =
        {
                new MySqlParameter("?KurumKodu", MySqlDbType.Int32),
                new MySqlParameter("?Sinifi", MySqlDbType.Int32),
                new MySqlParameter("?Donem", MySqlDbType.Int32)
        };
        pars[0].Value = kurumKodu;
        pars[1].Value = sinif;
        pars[2].Value = donem;

        var table = helper.ExecuteDataSet(sql, pars).Tables[0];
        return table;
    }
    public string Kutuk(int donem, int sinif)
    {
        const string sql = @"SELECT t.*,k.KurumAdi from testkutuk AS t 
        INNER JOIN kurumlar AS k ON k.KurumKodu=t.KurumKodu
        WHERE t.DonemId=?Donem AND t.Sinifi=?Sinifi order by k.KurumAdi,t.Sinifi,t.Sube,t.Adi,t.Soyadi asc";
        MySqlParameter[] pars =
        {
            new MySqlParameter("?Sinifi", MySqlDbType.Int32),
            new MySqlParameter("?Donem", MySqlDbType.Int32)
        };
        pars[0].Value = sinif;
        pars[1].Value = donem;

        var dt = helper.ExecuteDataSet(sql, pars).Tables[0];

        List<TestKutukKurumJoinModel> kutuk = new List<TestKutukKurumJoinModel>();
        foreach (DataRow k in dt.Rows)
        {
            kutuk.Add(new TestKutukKurumJoinModel(Convert.ToInt32(k["Id"]), k["IlceAdi"].ToString(), Convert.ToInt32(k["KurumKodu"]), k["KurumAdi"].ToString(),
                k["Adi"].ToString(), k["Soyadi"].ToString(), Convert.ToInt32(k["Sinifi"]), k["Sube"].ToString()));
        }
        return JsonConvert.SerializeObject(kutuk);
    }

    public DataTable SinifSubeSayilari(int sinif)
    {
        string sql = @"SELECT DISTINCT(ktk.KurumKodu),ktk.IlceAdi,ktk.Sinifi,COUNT(Id) AS OgrenciSayisi
                    FROM testkutuk AS ktk WHERE ktk.Sinifi=?Sinifi
                    GROUP BY ktk.KurumKodu,ktk.Sinifi
                    ORDER BY ktk.IlceAdi";

        MySqlParameter[] pars =
        {
                new MySqlParameter("?Sinifi", MySqlDbType.Int32)
            };
        pars[0].Value = sinif;

        var table = helper.ExecuteDataSet(sql, pars).Tables[0];
        return table;
    }

    public List<TestKutukInfo> IlceninOkullari(int sinif, string ilce)
    {
        string sql = "SELECT DISTINCT ktk.KurumKodu FROM testkutuk AS ktk WHERE ktk.Sinifi=?Sinif AND ktk.IlceAdi=?Ilce";
        MySqlParameter[] p =
        {
                new MySqlParameter("?Sinif", MySqlDbType.Int32),
                new MySqlParameter("?Ilce", MySqlDbType.String)
            };
        p[0].Value = sinif;
        p[1].Value = ilce;

        DataTable dt = helper.ExecuteDataSet(sql, p).Tables[0];
        List<TestKutukInfo> karne = new List<TestKutukInfo>();
        foreach (DataRow k in dt.Rows)
        {
            karne.Add(new TestKutukInfo(Convert.ToInt32(k["KurumKodu"])));
        }
        return karne;
    }

    public List<TestKutukInfo> IlceninOkullari(string ilce)
    {
        string sql = "SELECT DISTINCT ktk.KurumKodu, FROM testkutuk AS ktk WHERE ktk.IlceAdi=?Ilce";
        MySqlParameter[] p =
        {
            new MySqlParameter("?Ilce", MySqlDbType.String)
        };
        p[0].Value = ilce;

        DataTable dt = helper.ExecuteDataSet(sql, p).Tables[0];
        List<TestKutukInfo> karne = new List<TestKutukInfo>();
        foreach (DataRow k in dt.Rows)
        {
            karne.Add(new TestKutukInfo(Convert.ToInt32(k["KurumKodu"])));
        }
        return karne;
    }

    public List<TestKutukInfo> KayitlariDizeGetir(int kurumKodu)
    {
        string sql = "select * from testkutuk where KurumKodu=?KurumKodu";
        MySqlParameter[] p =
        {
                new MySqlParameter("?KurumKodu", MySqlDbType.Int32)
            };
        p[0].Value = kurumKodu;

        DataTable dt = helper.ExecuteDataSet(sql, p).Tables[0];
        List<TestKutukInfo> karne = new List<TestKutukInfo>();
        foreach (DataRow k in dt.Rows)
        {
            karne.Add(new TestKutukInfo(Convert.ToInt32(k["Id"]), k["OpaqId"].ToString(), k["IlceAdi"].ToString(),
                Convert.ToInt32(k["KurumKodu"]), k["Adi"].ToString(), k["Soyadi"].ToString(), Convert.ToInt32(k["Sinifi"]), k["Sube"].ToString()));
        }
        return karne;
    }

    public TestKutukInfo KayitBilgiGetir(string cmdText, params MySqlParameter[] param)
    {
        MySqlDataReader dr = helper.ExecuteReader(cmdText, param);
        var info = TabloAlanlar(dr);

        return info;
    }

    private static TestKutukInfo TabloAlanlar(MySqlDataReader dr)
    {
        TestKutukInfo info = new TestKutukInfo();
        while (dr.Read())
        {
            info.Id = dr.GetMySayi("Id");
            info.DonemId = dr.GetMySayi("DonemId");
            info.OpaqId = dr.GetMyMetin("OpaqId");
            info.IlceAdi = dr.GetMyMetin("IlceAdi");
            info.KurumKodu = dr.GetMySayi("KurumKodu");
            info.Adi = dr.GetMyMetin("Adi");
            info.Soyadi = dr.GetMyMetin("Soyadi");
            info.Sinifi = dr.GetMySayi("Sinifi");
            info.Sube = dr.GetMyMetin("Sube");
        }

        dr.Close();
        return info;
    }
    public TestKutukInfo KayitBilgiGetir(string opaqId)
    {
        string cmdText = "select * from testkutuk where OpaqId=?OpaqId";
        MySqlParameter param = new MySqlParameter("?OpaqId", MySqlDbType.String) { Value = opaqId };
        MySqlDataReader dr = helper.ExecuteReader(cmdText, param);
        var info = TabloAlanlar(dr);

        return info;
    }
    public TestKutukInfo KayitBilgiGetir(int donem, string opaqId)
    {
        string cmdText = "select * from testkutuk where DonemId=?DonemId  and OpaqId=?OpaqId";
        MySqlParameter[] p =
        {
            new MySqlParameter("?DonemId", MySqlDbType.Int32),
            new MySqlParameter("?OpaqId", MySqlDbType.String)
        };
        p[0].Value = donem;
        p[1].Value =  opaqId;
        MySqlDataReader dr = helper.ExecuteReader(cmdText, p);
        var info = TabloAlanlar(dr);

        return info;
    }
    public TestKutukInfo KayitBilgiGetir(int donem,int kurumKodu,string opaqId)
    {
        string cmdText = "select * from testkutuk where DonemId=?DonemId and KurumKodu=?KurumKodu and OpaqId=?OpaqId";
        MySqlParameter[] p =
        {
            new MySqlParameter("?DonemId", MySqlDbType.Int32),
            new MySqlParameter("?KurumKodu", MySqlDbType.Int32),
            new MySqlParameter("?OpaqId", MySqlDbType.String)
        };
        p[0].Value = donem;
        p[1].Value = kurumKodu;
        p[2].Value = opaqId;
        MySqlDataReader dr = helper.ExecuteReader(cmdText, p);
        var info = TabloAlanlar(dr);

        return info;
    }
    public TestKutukInfo KayitBilgiGetir(int id)
    {
        string cmdText = "select * from testkutuk where Id=?Id";
        MySqlParameter param = new MySqlParameter("?Id", MySqlDbType.Int32) { Value = id };
        MySqlDataReader dr = helper.ExecuteReader(cmdText, param);
        var info = TabloAlanlar(dr);

        return info;
    }

    public TestKutukInfo KayitBilgiGetir(int sinif, int kurumkodu)
    {
        //Kütükte her hangi bir öğrencinin sınıf ve kurum kodu verilen herhangi bir öğrencinin okul bilgisi
        string sql = "SELECT * FROM testkutuk AS ktk WHERE ktk.Sinifi=?Sinif AND ktk.KurumKodu=?KurumKodu";
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

    public TestKutukInfo OgrenciBilgiGetir(int kurumkodu, string opaqId)
    {
        //Kütükte her hangi bir öğrencinin sınıf ve kurum kodu verilen herhangi bir öğrencinin okul bilgisi
        string sql = "SELECT * FROM testkutuk AS ktk WHERE ktk.OpaqId=?OpaqId AND ktk.KurumKodu=?KurumKodu";
        MySqlParameter[] p =
        {
            new MySqlParameter("?OpaqId", MySqlDbType.String),
            new MySqlParameter("?KurumKodu", MySqlDbType.Int32)
        };
        p[0].Value = opaqId;
        p[1].Value = kurumkodu;

        MySqlDataReader dr = helper.ExecuteReader(sql, p);
        var info = TabloAlanlar(dr);

        return info;
    }

    public int KayitSil(string opaqId)
    {
        const string sql = "delete from testkutuk where OpaqId=?OpaqId";
        MySqlParameter p = new MySqlParameter("?OpaqId", MySqlDbType.String) { Value = opaqId };
        int res = helper.ExecuteNonQuery(sql, p);

        if (res>0)
        {
            TestLogDb logDb = new TestLogDb();
            logDb.KayitSilOgrenci(opaqId);


            TestOgrCevapDb ogrCevapDb = new TestOgrCevapDb();
            ogrCevapDb.OgrenciCevaplariniSil(opaqId);

            TestOgrPuanDb ogrPuanDb = new TestOgrPuanDb();
            ogrPuanDb.OgrenciPuanlariniSil(opaqId);
        }

        return res;
    }

    public int KayitEkle(TestKutukInfo info)
    {
        const string sql = @"insert into testkutuk (OpaqId,IlceAdi,KurumKodu,Adi,Soyadi,Sinifi,Sube,DonemId) values (?OpaqId,?IlceAdi,?KurumKodu,?Adi,?Soyadi,?Sinifi,?Sube,?DonemId)";
        MySqlParameter[] pars =
        {
                new MySqlParameter("?OpaqId", MySqlDbType.String),
                new MySqlParameter("?IlceAdi", MySqlDbType.String),
                new MySqlParameter("?KurumKodu", MySqlDbType.Int32),
                new MySqlParameter("?Adi", MySqlDbType.String),
                new MySqlParameter("?Soyadi", MySqlDbType.String),
                new MySqlParameter("?Sinifi", MySqlDbType.Int32),
                new MySqlParameter("?Sube", MySqlDbType.String),
                new MySqlParameter("?DonemId", MySqlDbType.Int32)
            };
        pars[0].Value = info.OpaqId;
        pars[1].Value = info.IlceAdi;
        pars[2].Value = info.KurumKodu;
        pars[3].Value = info.Adi;
        pars[4].Value = info.Soyadi;
        pars[5].Value = info.Sinifi;
        pars[6].Value = info.Sube;
        pars[7].Value = info.DonemId;
        return helper.ExecuteNonQuery(sql, pars);
    }

    public int KayitGuncelle(TestKutukInfo info)
    {
        const string sql = @"update testkutuk set OpaqId=?OpaqId,IlceAdi=?IlceAdi,KurumKodu=?KurumKodu,Adi=?Adi,Soyadi=?Soyadi,Sinifi=?Sinifi,Sube=?Sube where Id=?Id";
        MySqlParameter[] pars =
        {
                new MySqlParameter("?OpaqId", MySqlDbType.String),
                new MySqlParameter("?IlceAdi", MySqlDbType.String),
                new MySqlParameter("?KurumKodu", MySqlDbType.Int32),
                new MySqlParameter("?Adi", MySqlDbType.String),
                new MySqlParameter("?Soyadi", MySqlDbType.String),
                new MySqlParameter("?Sinifi", MySqlDbType.Int32),
                new MySqlParameter("?Sube", MySqlDbType.String),
                new MySqlParameter("?Id", MySqlDbType.Int32)
            };
        pars[0].Value = info.OpaqId;
        pars[1].Value = info.IlceAdi;
        pars[2].Value = info.KurumKodu;
        pars[3].Value = info.Adi;
        pars[4].Value = info.Soyadi;
        pars[5].Value = info.Sinifi;
        pars[6].Value = info.Sube;
        pars[7].Value = info.Id;
        return helper.ExecuteNonQuery(sql, pars);
    }
    public int GirisGuncelle(string opaqId)
    {
        const string sql = @"update testkutuk set SonGiris=?SonGiris where OpaqId=?OpaqId";
        MySqlParameter[] pars =
        {
            new MySqlParameter("?SonGiris", MySqlDbType.DateTime),
            new MySqlParameter("?OpaqId", MySqlDbType.String)
        };
        pars[0].Value = GenelIslemler.YerelTarih();
        pars[1].Value = opaqId;
        return helper.ExecuteNonQuery(sql, pars);
    }
    public bool KayitKontrol(string opaqId, int id)
    {
        string cmdText = "select Id,OpaqId from testkutuk where OpaqId=?OpaqId and Id<>?Id";
        MySqlParameter[] pars =
        {
            new MySqlParameter("?OpaqId", MySqlDbType.String),
            new MySqlParameter("?Id", MySqlDbType.Int32)
        };
        pars[0].Value = opaqId;
        pars[1].Value = id;
        bool sonuc = Convert.ToInt32(helper.ExecuteScalar(cmdText, pars)) > 0;
        return sonuc;
    }
    public bool KayitKontrol(int donem,string opaqId, int id)
    {
        string cmdText = "select Id from testkutuk where DonemId=?DonemId and OpaqId=?OpaqId and Id<>?Id";
        MySqlParameter[] pars =
        {
            new MySqlParameter("?DonemId", MySqlDbType.Int32),
            new MySqlParameter("?OpaqId", MySqlDbType.String),
            new MySqlParameter("?Id", MySqlDbType.Int32)
        };
        pars[0].Value = donem;
        pars[1].Value = opaqId;
        pars[2].Value = id;
        var res = helper.ExecuteScalar(cmdText, pars);
        bool sonuc = Convert.ToInt32(res) > 0;
        return sonuc;
    }
    public int KayitSayisi(int donem)
    {
        const string cmdText = "select count(Id) from testkutuk where DonemId=?DonemId";
        MySqlParameter pars = new MySqlParameter("?DonemId", MySqlDbType.Int32) { Value = donem };
        int sonuc = Convert.ToInt32(helper.ExecuteScalar(cmdText, pars));
        return sonuc;
    }
}