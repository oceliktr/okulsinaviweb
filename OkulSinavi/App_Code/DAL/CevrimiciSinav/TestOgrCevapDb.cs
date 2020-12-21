using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;

/// <summary>
/// Summary description for TestCevapDb
/// </summary>
public class TestOgrCevapDb
{
    private readonly HelperDb helper = new HelperDb();

    public DataTable KayitlariGetir()
    {
        const string sql = "select * from testogrcevaplar order by Id asc";
        return helper.ExecuteDataSet(sql).Tables[0];
    }
    public DataTable KayitlariGetir(int sinavId,string opaqId)
    {
        const string sql = @"SELECT ot.OturumAdi,ot.Sure, oc.* from testogrcevaplar AS oc 
                            INNER JOIN testoturumlar AS ot ON ot.Id=oc.OturumId
                            WHERE oc.SinavId=?SinavId AND oc.OpaqId=?OpaqId";
        MySqlParameter[] p =
        {
            new MySqlParameter("?SinavId", MySqlDbType.Int32),
            new MySqlParameter("?OpaqId", MySqlDbType.String)
        };
        p[0].Value = sinavId;
        p[1].Value = opaqId;
        return helper.ExecuteDataSet(sql,p).Tables[0];
    }
    public DataTable KayitlariGetir(string opaqId)
    {
        const string sql = "select * from testogrcevaplar  WHERE OpaqId=?OpaqId order by Id asc";
        MySqlParameter p = new MySqlParameter("?OpaqId", MySqlDbType.String) { Value = opaqId };

        return helper.ExecuteDataSet(sql, p).Tables[0];
    }
    public DataTable OgrenciIslemleri(string opaqId)
    {
        const string sql = @"SELECT s.SinavAdi,o.OturumAdi,oc.* from testogrcevaplar AS oc 
                            INNER JOIN testsinavlar AS s ON s.Id=oc.SinavId
                            INNER JOIN testoturumlar AS o ON o.Id=oc.OturumId
                            WHERE oc.OpaqId=?OpaqId order by oc.Id desc";
        MySqlParameter p = new MySqlParameter("?OpaqId", MySqlDbType.String) { Value = opaqId };

        return helper.ExecuteDataSet(sql, p).Tables[0];
    }

    public List<TestKutukInfo> PuaniHesaplanmayanlariGetir(int sinavId,int kurumkodu,bool x)
    {
        const string sql = @"SELECT op.OpaqId,k.Adi,k.Soyadi,k.Sinifi,k.Sube FROM testogrcevaplar AS op
                            INNER JOIN testkutuk AS k ON k.OpaqId=op.OpaqId                                                             
							WHERE op.SinavId=?SinavId AND k.KurumKodu=?KurumKodu AND NOT EXISTS (SELECT oc.OpaqId FROM testogrpuanlar AS oc WHERE oc.OpaqId=op.OpaqId)";
        MySqlParameter[] p =
        {
            new MySqlParameter("?SinavId", MySqlDbType.Int32),
            new MySqlParameter("?KurumKodu", MySqlDbType.Int32)
        };
        p[0].Value = sinavId;
        p[1].Value = kurumkodu;

        DataTable dt = helper.ExecuteDataSet(sql, p).Tables[0];
        List<TestKutukInfo> table = new List<TestKutukInfo>();
        foreach (DataRow k in dt.Rows)
        {
            table.Add(new TestKutukInfo(k["OpaqId"].ToString(), k["Adi"].ToString(), k["Soyadi"].ToString(), Convert.ToInt32(k["Sinifi"].ToString()), k["Sube"].ToString()));
        }
        return table;
    }
    public List<TestOgrCevapInfo> PuaniHesaplanmayanlariGetir(int sinavId, int kayitsayisi)
    {
        //    string sql = string.Format("select DISTINCT(OpaqId) from testogrcevaplar where SinavId=?SinavId and Dogru=0 and Yanlis=0 AND (Cevap LIKE '%A%' OR Cevap LIKE '%B%' OR  Cevap LIKE '%C%' OR Cevap LIKE '%D%' OR Cevap LIKE '%E%') order by Id asc Limit {0}", kayitsayisi);
        string sql = string.Format(@"SELECT op.OpaqId FROM testogrcevaplar AS op                                                              
							WHERE op.SinavId=?SinavId AND NOT EXISTS (SELECT oc.OpaqId FROM testogrpuanlar AS oc WHERE oc.OpaqId=op.OpaqId) Limit {0}", kayitsayisi);

        MySqlParameter p = new MySqlParameter("?SinavId", MySqlDbType.Int32) { Value = sinavId };
        DataTable dt = helper.ExecuteDataSet(sql, p).Tables[0];
        List<TestOgrCevapInfo> table = new List<TestOgrCevapInfo>();
        foreach (DataRow k in dt.Rows)
        {
            table.Add(new TestOgrCevapInfo(k["OpaqId"].ToString()));
        }
        return table;
    }
   

    public List<TestOgrCevapInfo> KayitlariDiziyeGetir(string opaqId)
    {
        const string sql = "select * from testogrcevaplar  WHERE OpaqId=?OpaqId order by Id asc";
        MySqlParameter p = new MySqlParameter("?OpaqId", MySqlDbType.String) { Value = opaqId };

        DataTable dt = helper.ExecuteDataSet(sql, p).Tables[0];
        List<TestOgrCevapInfo> table = new List<TestOgrCevapInfo>();
        foreach (DataRow k in dt.Rows)
        {
            table.Add(new TestOgrCevapInfo(Convert.ToInt32(k["Id"].ToString()), Convert.ToInt32(k["SinavId"].ToString()), Convert.ToInt32(k["OturumId"].ToString()), k["OpaqId"].ToString(), k["Cevap"].ToString(), Convert.ToInt32(k["Dogru"].ToString()), Convert.ToInt32(k["Yanlis"].ToString()), Convert.ToDateTime(k["Baslangic"].ToString()), Convert.ToInt32(k["Bitti"].ToString())));
        }
        return table;
    }

    private static TestOgrCevapInfo TabloAlanlar(MySqlDataReader dr)
    {
        TestOgrCevapInfo info = new TestOgrCevapInfo();
        while (dr.Read())
        {
            info.Id = dr.GetMySayi("Id");
            info.SinavId = dr.GetMySayi("SinavId");
            info.OturumId = dr.GetMySayi("OturumId");
            info.OpaqId = dr.GetMyMetin("OpaqId");
            info.Dogru = dr.GetMySayi("Dogru");
            info.Yanlis = dr.GetMySayi("Yanlis");
            info.Bitti = dr.GetMySayi("Bitti");
            info.Cevap = dr.GetMyMetin("Cevap");
            info.Baslangic = dr.GetMyTarih("Baslangic");
            info.Bitis = dr.GetMyTarih("Bitis");
            info.SonIslem = dr.GetMyTarih("SonIslem");
        }

        dr.Close();
        return info;
    }

    public TestOgrCevapInfo KayitBilgiGetir(int oturumId, string opaqId)
    {
        string cmdText = "select * from testogrcevaplar where OturumId=?OturumId and OpaqId=?OpaqId";
        MySqlParameter[] p =
        {
            new MySqlParameter("?OturumId", MySqlDbType.Int32),
            new MySqlParameter("?OpaqId", MySqlDbType.String)
        };
        p[0].Value = oturumId;
        p[1].Value = opaqId;

        MySqlDataReader dr = helper.ExecuteReader(cmdText, p);
        var info = TabloAlanlar(dr);

        return info;
    }


    public void KayitSil(int id)
    {
        const string sql = "delete from testogrcevaplar where Id=?Id";
        MySqlParameter p = new MySqlParameter("?Id", MySqlDbType.Int32) { Value = id };
        helper.ExecuteNonQuery(sql, p);
    }
    public void OgrenciCevaplariniSil(string opaqId)
    {
        const string sql = "delete from testogrcevaplar where OpaqId=?OpaqId";
        MySqlParameter p = new MySqlParameter("?OpaqId", MySqlDbType.String) { Value = opaqId };
        helper.ExecuteNonQuery(sql, p);
    }

    public void OturumCevaplariniSil(int oturumId)
    {
        const string sql = "delete from testogrcevaplar where OturumId=?OturumId";
        MySqlParameter p = new MySqlParameter("?OturumId", MySqlDbType.Int32) { Value = oturumId };
        helper.ExecuteNonQuery(sql, p);
    }
    public void KayitSil()
    {
        const string sql = "delete from testogrcevaplar";
        helper.ExecuteNonQuery(sql);
    }
    public void KayitEkle(TestOgrCevapInfo info)
    {
        const string sql = @"insert into testogrcevaplar (SinavId,OturumId,OpaqId,Dogru,Yanlis,Cevap,Baslangic) values (?SinavId,?OturumId,?OpaqId,?Dogru,?Yanlis,?Cevap,?Baslangic)";
        MySqlParameter[] pars =
        {
                new MySqlParameter("?SinavId", MySqlDbType.Int32),
                new MySqlParameter("?OturumId", MySqlDbType.Int32),
                new MySqlParameter("?OpaqId", MySqlDbType.String),
                new MySqlParameter("?Dogru", MySqlDbType.Int32),
                new MySqlParameter("?Yanlis", MySqlDbType.String),
                new MySqlParameter("?Cevap", MySqlDbType.String),
                new MySqlParameter("?Baslangic", MySqlDbType.DateTime)
        };
        pars[0].Value = info.SinavId;
        pars[1].Value = info.OturumId;
        pars[2].Value = info.OpaqId;
        pars[3].Value = info.Dogru;
        pars[4].Value = info.Yanlis;
        pars[5].Value = info.Cevap;
        pars[6].Value = GenelIslemler.YerelTarih();
        helper.ExecuteNonQuery(sql, pars);
    }

    public void CevapGuncelle(TestOgrCevapInfo info)
    {
        const string sql = @"update testogrcevaplar set Cevap=?Cevap,SonIslem=?SonIslem where OturumId=?OturumId and OpaqId=?OpaqId";
        MySqlParameter[] pars =
        {
            new MySqlParameter("?OturumId", MySqlDbType.Int32),
            new MySqlParameter("?OpaqId", MySqlDbType.String),
            new MySqlParameter("?Cevap", MySqlDbType.String),
            new MySqlParameter("?SonIslem", MySqlDbType.DateTime)
        };
        pars[0].Value = info.OturumId;
        pars[1].Value = info.OpaqId;
        pars[2].Value = info.Cevap;
        pars[3].Value = GenelIslemler.YerelTarih();
        helper.ExecuteNonQuery(sql, pars);
    }

    public int DogruYanlisGuncelle(TestOgrCevapInfo info)
    {
        const string sql = @"update testogrcevaplar set Dogru=?Dogru,Yanlis=?Yanlis,Bitis=?Bitis,Bitti=?Bitti where OturumId=?OturumId and OpaqId=?OpaqId";
        MySqlParameter[] pars =
        {
            new MySqlParameter("?OturumId", MySqlDbType.Int32),
            new MySqlParameter("?OpaqId", MySqlDbType.String),
            new MySqlParameter("?Dogru", MySqlDbType.Int32),
            new MySqlParameter("?Yanlis", MySqlDbType.Int32),
            new MySqlParameter("?Bitis", MySqlDbType.DateTime),
            new MySqlParameter("?Bitti", MySqlDbType.Int32)
        };
        pars[0].Value = info.OturumId;
        pars[1].Value = info.OpaqId;
        pars[2].Value = info.Dogru;
        pars[3].Value = info.Yanlis;
        pars[4].Value = GenelIslemler.YerelTarih();
        pars[5].Value = info.Bitti;
        return helper.ExecuteNonQuery(sql, pars);
    }

    public bool OturumKontrol(int oturumId, string opaqId)
    {
        string cmdText = "select Id from testogrcevaplar where OturumId=?OturumId and OpaqId=?OpaqId";
        MySqlParameter[] pars =
{
            new MySqlParameter("?OturumId", MySqlDbType.Int32),
            new MySqlParameter("?OpaqId", MySqlDbType.String)
        };
        pars[0].Value = oturumId;
        pars[1].Value = opaqId;
        bool sonuc = Convert.ToInt32(helper.ExecuteScalar(cmdText, pars)) > 0;
        return sonuc;
    }
    public bool KayitKontrol(string opaqId)
    {
        string cmdText = "select Id from testogrcevaplar where OpaqId=?OpaqId";
        MySqlParameter[] pars =
        {
            new MySqlParameter("?OpaqId", MySqlDbType.String)
        };
        pars[0].Value = opaqId;
        bool sonuc = Convert.ToInt32(helper.ExecuteScalar(cmdText, pars)) > 0;
        return sonuc;
    }
}