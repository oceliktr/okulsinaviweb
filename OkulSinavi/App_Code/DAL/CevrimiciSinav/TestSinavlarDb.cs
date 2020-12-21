using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;

/// <summary>
/// Summary description for TestSinavlarDb
/// </summary>
public class TestSinavlarDb
{
    private readonly HelperDb helper = new HelperDb();

    public DataTable KayitlariGetir()
    { string sql = "select * from testsinavlar ORDER BY Aktif desc,Id desc";
       return helper.ExecuteDataSet(sql).Tables[0];
    }
    public DataTable KayitlariGetir(string kurumkodu)
    {
        string sql = "select * from testsinavlar where Kurumlar Like '%," + kurumkodu + ",%' ORDER BY Aktif desc,Id desc";
        return helper.ExecuteDataSet(sql).Tables[0];
    }
    public DataTable KayitlariGetir(int sinif,string kurumkodu)
    {
         string sql = "select * from testsinavlar where Sinif=?Sinif and Kurumlar Like '%,"+kurumkodu+",%' ORDER BY Aktif desc,Id desc";
        MySqlParameter[] pars =
        {
            new MySqlParameter("?Sinif", MySqlDbType.Int32)
        };
        pars[0].Value = sinif;
        return helper.ExecuteDataSet(sql, pars).Tables[0];
    }
    public List<TestSinavlarInfo> TumSinavlar(string kurumkodu)
    {
         string sql = "select * from testsinavlar where Kurumlar Like '%," + kurumkodu + ",%' ORDER BY Aktif desc,Id desc";
        DataTable dt = helper.ExecuteDataSet(sql).Tables[0];
        List<TestSinavlarInfo> table = new List<TestSinavlarInfo>();
        foreach (DataRow k in dt.Rows)
        {
            table.Add(new TestSinavlarInfo(Convert.ToInt32(k["Id"].ToString()), Convert.ToInt32(k["DonemId"].ToString()), Convert.ToInt32(k["Sinif"].ToString()), Convert.ToInt32(k["Puanlama"].ToString()), k["SinavAdi"].ToString(), k["Aciklama"].ToString(), Convert.ToInt32(k["Aktif"].ToString()),  Convert.ToInt32(k["OturumTercihi"].ToString()), Convert.ToInt32(k["BeklemeSuresi"].ToString())));
        }
        return table;
    }
    public List<TestSinavlarInfo> AktifSinavlar(int sinif,string kurumkodu)
    {
         string sql = @"SELECT t.* from testsinavlar as t WHERE Kurumlar Like '%," + kurumkodu + ",%' and t.Sinif=?Sinif and t.Aktif=1 ORDER BY t.Id desc";
        MySqlParameter[] pars =
        {
            new MySqlParameter("?Sinif", MySqlDbType.Int32)
        };
        pars[0].Value = sinif;

        DataTable dt = helper.ExecuteDataSet(sql, pars).Tables[0];
        List<TestSinavlarInfo> table = new List<TestSinavlarInfo>();
        foreach (DataRow k in dt.Rows)
        {
            table.Add(new TestSinavlarInfo(Convert.ToInt32(k["Id"].ToString()), Convert.ToInt32(k["DonemId"].ToString()), Convert.ToInt32(k["Sinif"].ToString()), Convert.ToInt32(k["Puanlama"].ToString()), k["SinavAdi"].ToString(), k["Aciklama"].ToString(), Convert.ToInt32(k["Aktif"].ToString()), Convert.ToInt32(k["OturumTercihi"].ToString()), Convert.ToInt32(k["BeklemeSuresi"].ToString())));
        }
        return table;
    }
    
    private static TestSinavlarInfo TabloAlanlar(MySqlDataReader dr)
    {
        TestSinavlarInfo info = new TestSinavlarInfo();
        while (dr.Read())
        {
            info.Id = dr.GetMySayi("Id");
            info.DonemId = dr.GetMySayi("DonemId");
            info.Aciklama = dr.GetMyMetin("Aciklama");
            info.Kurumlar = dr.GetMyMetin("Kurumlar");
            info.Aktif = dr.GetMySayi("Aktif");
            info.Sinif = dr.GetMySayi("Sinif");
            info.Puanlama = dr.GetMySayi("Puanlama");
            info.SinavAdi = dr.GetMyMetin("SinavAdi");
            info.OturumTercihi = dr.GetMySayi("OturumTercihi");
            info.BeklemeSuresi = dr.GetMySayi("BeklemeSuresi");
        }

        dr.Close();
        return info;
    }

    public TestSinavlarInfo KayitBilgiGetir(int id)
    {
        string cmdText = "select * from testsinavlar where Id=?Id";
        MySqlParameter param = new MySqlParameter("?Id", MySqlDbType.Int32) { Value = id };
        MySqlDataReader dr = helper.ExecuteReader(cmdText, param);
        var info = TabloAlanlar(dr);

        return info;
    }
    public TestSinavlarInfo KayitBilgiGetir(int id, string kurumkodu)
    {
        string cmdText = "select * from testsinavlar where Id=?Id and Kurumlar Like '%," + kurumkodu + ",%'";
        MySqlParameter param = new MySqlParameter("?Id", MySqlDbType.Int32) { Value = id };
        MySqlDataReader dr = helper.ExecuteReader(cmdText, param);
        var info = TabloAlanlar(dr);

        return info;
    }
    public TestSinavlarInfo KayitBilgiGetir(TestSinavlarInfo snv)
    {
        string cmdText = "select * from testsinavlar where Sinif=?Sinif and SinavAdi=?SinavAdi and Kurumlar=?Kurumlar Order By Id desc limit 1";
        MySqlParameter[] pars =
        {
            new MySqlParameter("?Sinif", MySqlDbType.Int32),
            new MySqlParameter("?SinavAdi", MySqlDbType.String),
            new MySqlParameter("?Kurumlar", MySqlDbType.String)
        };
        pars[0].Value = snv.Sinif;
        pars[1].Value = snv.SinavAdi;
        pars[2].Value = snv.Kurumlar;
        MySqlDataReader dr = helper.ExecuteReader(cmdText, pars);
        var info = TabloAlanlar(dr);

        return info;
    }
    public int KayitSil(int id)
    {
        TestOturumlarDb oturumlarDb = new TestOturumlarDb();
        oturumlarDb.OturumlariSil(id);

        TestIlcePuanDb ilcePuanDb= new TestIlcePuanDb();
        ilcePuanDb.SinavPuanlariniSil(id);

        TestOgrPuanDb ogrPuanDb = new TestOgrPuanDb();
        ogrPuanDb.SinavPuanlariniSil(id);

        TestOkulPuanDb okulPuanDb= new TestOkulPuanDb();
        okulPuanDb.SinavPuanlariniSil(id);

        const string sql = "delete from testsinavlar where Id=?Id";
        MySqlParameter p = new MySqlParameter("?Id", MySqlDbType.Int32) { Value = id };
        return helper.ExecuteNonQuery(sql, p);
    }

    public long KayitEkle(TestSinavlarInfo info)
    {
        const string sql = @"insert into testsinavlar (DonemId,SinavAdi,Aciklama,Sinif,Puanlama,Aktif,OturumTercihi,BeklemeSuresi,Kurumlar) values (?DonemId,?SinavAdi,?Aciklama,?Sinif,?Puanlama,?Aktif,?OturumTercihi,?BeklemeSuresi,?Kurumlar)";
        MySqlParameter[] pars =
        {
            new MySqlParameter("?DonemId", MySqlDbType.Int32),
            new MySqlParameter("?SinavAdi", MySqlDbType.String),
            new MySqlParameter("?Aciklama", MySqlDbType.String),
            new MySqlParameter("?Sinif", MySqlDbType.Int32),
            new MySqlParameter("?Puanlama", MySqlDbType.Int32),
            new MySqlParameter("?Aktif", MySqlDbType.Int32),
            new MySqlParameter("?OturumTercihi", MySqlDbType.Int32),
            new MySqlParameter("?BeklemeSuresi", MySqlDbType.Int32),
            new MySqlParameter("?Kurumlar", MySqlDbType.String)
        };
        pars[0].Value = 0;
        pars[1].Value = info.SinavAdi;
        pars[2].Value = info.Aciklama;
        pars[3].Value = info.Sinif;
        pars[4].Value = info.Puanlama;
        pars[5].Value = info.Aktif;
        pars[6].Value = info.OturumTercihi;
        pars[7].Value = info.BeklemeSuresi;
        pars[8].Value = info.Kurumlar;

        long sonId;
        helper.ExecuteNonQuery(out sonId, sql, pars);
        return sonId;
    }

    public int KayitGuncelle(TestSinavlarInfo info)
    {
        const string sql = @"update testsinavlar set SinavAdi=?SinavAdi,Aciklama=?Aciklama,Sinif=?Sinif,Puanlama=?Puanlama,Aktif=?Aktif,OturumTercihi=?OturumTercihi,BeklemeSuresi=?BeklemeSuresi where Id=?Id";
        MySqlParameter[] pars =
        {
                new MySqlParameter("?SinavAdi", MySqlDbType.String),
                new MySqlParameter("?Aciklama", MySqlDbType.String),
                new MySqlParameter("?Sinif", MySqlDbType.Int32),
                new MySqlParameter("?Puanlama", MySqlDbType.Int32),
                new MySqlParameter("?Aktif", MySqlDbType.Int32),
                new MySqlParameter("?OturumTercihi", MySqlDbType.Int32),
                new MySqlParameter("?BeklemeSuresi", MySqlDbType.Int32),
                new MySqlParameter("?Id", MySqlDbType.Int32)
            };
        pars[0].Value = info.SinavAdi;
        pars[1].Value = info.Aciklama;
        pars[2].Value = info.Sinif;
        pars[3].Value = info.Puanlama;
        pars[4].Value = info.Aktif;
        pars[5].Value = info.OturumTercihi;
        pars[6].Value = info.BeklemeSuresi;
        pars[7].Value = info.Id;
        return helper.ExecuteNonQuery(sql, pars);
    }

    public int KayitSayisi(string kurumkodu)
    {
         string cmdText = "select count(Id) from testsinavlar where Kurumlar Like '%," + kurumkodu + ",%'";
         int sonuc = Convert.ToInt32(helper.ExecuteScalar(cmdText));
        return sonuc;
    }

}