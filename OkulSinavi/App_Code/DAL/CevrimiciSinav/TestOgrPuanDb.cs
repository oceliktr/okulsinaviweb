using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;

/// <summary>
/// Summary description for TestOgrPuanDb
/// </summary>
public class TestOgrPuanDb
{
    private readonly HelperDb helper = new HelperDb();

    public DataTable KayitlariGetir()
    {
        const string sql = "select * from testogrpuanlar order by Id asc";
        return helper.ExecuteDataSet(sql).Tables[0];
    }
    public DataTable SinavaKatilmayanKurumlar(int sinavId)
    {
        const string sql = @"select DISTINCT(tk.KurumKodu),k.KurumAdi,tk.IlceAdi from testkutuk AS tk 
                            LEFT JOIN kurumlar AS k ON tk.KurumKodu=k.KurumKodu
                            where tk.KurumKodu not in (select testogrpuanlar.KurumKodu from testogrpuanlar where SinavId=?SinavId) ORDER BY tk.IlceAdi,k.KurumAdi";
        MySqlParameter p = new MySqlParameter("?SinavId", MySqlDbType.Int32) { Value = sinavId };
        return helper.ExecuteDataSet(sql, p).Tables[0];
    }
    public DataTable OgrenciSiralama(int sinavId,int kurumKodu)
    {
        const string sql = @"SELECT tk.IlceAdi,k.KurumKodu,k.KurumAdi,tk.OpaqId,tk.Adi,tk.Soyadi,tk.Sinifi,tk.Sube,op.Dogru,op.Yanlis,op.Bos,op.Puan,op.SinavId FROM testogrpuanlar AS op
                            INNER JOIN kurumlar AS k ON k.KurumKodu=op.KurumKodu
                            INNER JOIN testkutuk AS tk ON tk.OpaqId=op.OpaqId
                            WHERE op.SinavId=?SinavId and op.KurumKodu=?KurumKodu ORDER BY op.Puan DESC";
        MySqlParameter[] p =
        {
            new MySqlParameter("?SinavId", MySqlDbType.Int32),
            new MySqlParameter("?KurumKodu", MySqlDbType.Int32)
        };
        p[0].Value = sinavId;
        p[1].Value = kurumKodu;
        return helper.ExecuteDataSet(sql, p).Tables[0];
    }
     public DataTable KayitlariGetir(int sinavId, int kurumKodu)
    {
        //const string sql = @"SELECT k.Adi,k.Soyadi,k.Sinifi,k.Sube,op.* FROM testogrpuanlar AS op
        //                    INNER JOIN testkutuk AS k ON k.OpaqId=op.OpaqId
        //                    WHERE op.KurumKodu=?KurumKodu AND op.SinavId=?SinavId ORDER BY k.Sinifi,k.Sube,k.Adi,k.Soyadi";
        string sql =string.Format(@"SELECT k.IlceAdi,krm.KurumAdi, k.Adi,k.Soyadi,k.Sinifi,k.Sube,op.KurumKodu,op.SinavId,op.OpaqId,op.Dogru,op.Yanlis,op.Bos,op.Puan FROM testogrpuanlar AS op
INNER JOIN testkutuk AS k ON k.OpaqId=op.OpaqId
INNER JOIN testsinavlar AS s ON s.Id=op.SinavId
INNER JOIN kurumlar AS krm ON krm.KurumKodu=op.KurumKodu
WHERE s.Kurumlar LIKE '%,{0},%' AND op.SinavId=?SinavId ORDER BY op.Puan,k.Sinifi,k.Sube,k.Adi,k.Soyadi DESC", kurumKodu);
        MySqlParameter[] p =
        {
            new MySqlParameter("?SinavId", MySqlDbType.Int32)
        };
        p[0].Value = sinavId;
        return helper.ExecuteDataSet(sql, p).Tables[0];
    }
    public DataTable KayitlariGetir(int sinavId, int kurumKodu, string ilceAdi)
    {
        const string sql = @"SELECT k.Adi,k.Soyadi,k.Sinifi,k.Sube,op.* FROM testogrpuanlar AS op
                            INNER JOIN testkutuk AS k ON k.OpaqId=op.OpaqId
                            WHERE k.IlceAdi=?IlceAdi AND op.KurumKodu=?KurumKodu AND op.SinavId=?SinavId ORDER BY k.Sinifi,k.Sube,k.Adi,k.Soyadi";
        MySqlParameter[] p =
        {
            new MySqlParameter("?SinavId", MySqlDbType.Int32),
            new MySqlParameter("?KurumKodu", MySqlDbType.Int32),
            new MySqlParameter("?IlceAdi", MySqlDbType.String)
        };
        p[0].Value = sinavId;
        p[1].Value = kurumKodu;
        p[2].Value = ilceAdi;
        return helper.ExecuteDataSet(sql, p).Tables[0];
    }
    public DataTable KayitlariGetir(string opaqId)
    {
        const string sql = @"SELECT sa.SinavAdi,oc.* FROM testogrpuanlar AS oc
                            INNER JOIN testsinavlar AS sa ON sa.Id=oc.SinavId
                            WHERE oc.OpaqId=?OpaqId order by oc.SinavId desc";
        MySqlParameter p = new MySqlParameter("?OpaqId", MySqlDbType.String) { Value = opaqId };

        return helper.ExecuteDataSet(sql, p).Tables[0];
    }

    public List<OgrenciPuanIlceJoinModel> KayitlariDiziyeGetir(int sinavId)
    {
        const string sql = @"SELECT k.IlceAdi,op.* from testogrpuanlar AS op 
                            INNER JOIN testkutuk AS k ON k.OpaqId = op.OpaqId
                            WHERE op.SinavId=?SinavId";
        MySqlParameter p = new MySqlParameter("?SinavId", MySqlDbType.Int32) { Value = sinavId };

        DataTable dt = helper.ExecuteDataSet(sql, p).Tables[0];
        List<OgrenciPuanIlceJoinModel> table = new List<OgrenciPuanIlceJoinModel>();
        foreach (DataRow k in dt.Rows)
        {
            try
            {
                table.Add(new OgrenciPuanIlceJoinModel(Convert.ToInt32(k["Id"].ToString()), Convert.ToInt32(k["SinavId"].ToString()), Convert.ToInt32(k["KurumKodu"].ToString()), k["OpaqId"].ToString(), Convert.ToInt32(k["Dogru"].ToString()), Convert.ToInt32(k["Yanlis"].ToString()), Convert.ToInt32(k["Bos"].ToString()), Convert.ToDecimal(k["Puan"].ToString()), k["IlceAdi"].ToString()));

            }
            catch (Exception)
            {
                //
            }
        }
        return table;
    }
    public List<TestOgrPuanInfo> KayitlariDiziyeGetir(string opaqId)
    {
        const string sql = "select * from testogrpuanlar  WHERE OpaqId=?OpaqId order by Id asc";
        MySqlParameter p = new MySqlParameter("?OpaqId", MySqlDbType.String) { Value = opaqId };

        DataTable dt = helper.ExecuteDataSet(sql, p).Tables[0];
        List<TestOgrPuanInfo> table = new List<TestOgrPuanInfo>();
        foreach (DataRow k in dt.Rows)
        {
            table.Add(new TestOgrPuanInfo(Convert.ToInt32(k["Id"].ToString()), Convert.ToInt32(k["SinavId"].ToString()), Convert.ToInt32(k["KurumKodu"].ToString()), k["OpaqId"].ToString(), Convert.ToInt32(k["Dogru"].ToString()), Convert.ToInt32(k["Yanlis"].ToString()), Convert.ToInt32(k["Bos"].ToString()), Convert.ToDecimal(k["Puan"].ToString())));
        }
        return table;
    }

    private static TestOgrPuanInfo TabloAlanlar(MySqlDataReader dr)
    {
        TestOgrPuanInfo info = new TestOgrPuanInfo();
        while (dr.Read())
        {
            info.Id = dr.GetMySayi("Id");
            info.SinavId = dr.GetMySayi("SinavId");
            info.KurumKodu = dr.GetMySayi("KurumKodu");
            info.OpaqId = dr.GetMyMetin("OpaqId");
            info.Dogru = dr.GetMySayi("Dogru");
            info.Yanlis = dr.GetMySayi("Yanlis");
            info.Bos = dr.GetMySayi("Bos");
            info.Puan = dr.GetDecimal("Puan");
        }

        dr.Close();
        return info;
    }
    public TestOgrPuanInfo KayitBilgiGetir(int sinavId, string opaqId)
    {
        string cmdText = "select * from testogrpuanlar where SinavId=?SinavId and OpaqId=?OpaqId";
        MySqlParameter[] p =
        {
            new MySqlParameter("?SinavId", MySqlDbType.Int32),
            new MySqlParameter("?OpaqId", MySqlDbType.String)
        };
        p[0].Value = sinavId;
        p[1].Value = opaqId;

        MySqlDataReader dr = helper.ExecuteReader(cmdText, p);
        var info = TabloAlanlar(dr);

        return info;
    }
    public TestOgrPuanInfo KayitBilgiGetir(int sinavId, int kurumKodu, string opaqId)
    {
        string cmdText = "select * from testogrpuanlar where SinavId=?SinavId and OpaqId=?OpaqId and KurumKodu=?KurumKodu";
        MySqlParameter[] p =
        {
            new MySqlParameter("?SinavId", MySqlDbType.Int32),
            new MySqlParameter("?KurumKodu", MySqlDbType.Int32),
            new MySqlParameter("?OpaqId", MySqlDbType.String)
        };
        p[0].Value = sinavId;
        p[1].Value = kurumKodu;
        p[2].Value = opaqId;

        MySqlDataReader dr = helper.ExecuteReader(cmdText, p);
        var info = TabloAlanlar(dr);

        return info;
    }
    public TestOgrPuanInfo KayitBilgiGetir(int sinavId, string ilceAdi, string opaqId)
    {
        string cmdText = @"SELECT op.* FROM testogrpuanlar AS op 
                        INNER JOIN testkutuk AS k ON k.IlceAdi=?IlceAdi
                        WHERE op.SinavId=?SinavId AND op.OpaqId=?OpaqId";
        MySqlParameter[] p =
        {
            new MySqlParameter("?SinavId", MySqlDbType.Int32),
            new MySqlParameter("?IlceAdi", MySqlDbType.String),
            new MySqlParameter("?OpaqId", MySqlDbType.String)
        };
        p[0].Value = sinavId;
        p[1].Value = ilceAdi;
        p[2].Value = opaqId;

        MySqlDataReader dr = helper.ExecuteReader(cmdText, p);
        var info = TabloAlanlar(dr);

        return info;
    }

    public void KayitSil(int id)
    {
        const string sql = "delete from testogrpuanlar where Id=?Id";
        MySqlParameter p = new MySqlParameter("?Id", MySqlDbType.Int32) { Value = id };
        helper.ExecuteNonQuery(sql, p);
    }
    public void OgrenciPuanlariniSil(string opaqId)
    {
        const string sql = "delete from testogrpuanlar where OpaqId=?OpaqId";
        MySqlParameter p = new MySqlParameter("?OpaqId", MySqlDbType.String) { Value = opaqId };
        helper.ExecuteNonQuery(sql, p);
    }
    public void SinavPuanlariniSil(int sinavId)
    {
        const string sql = "delete from testogrpuanlar where SinavId=?SinavId";
        MySqlParameter p = new MySqlParameter("?SinavId", MySqlDbType.Int32) { Value = sinavId };
        helper.ExecuteNonQuery(sql, p);
    }
    public int PuaniHesaplananOgrenciSayisi(int sinavId, int kurumKodu)
    {
        string sql = "select COUNT(Id) from testogrpuanlar where SinavId=?SinavId and KurumKodu=?KurumKodu";
        MySqlParameter[] p =
        {
            new MySqlParameter("?SinavId", MySqlDbType.Int32),
            new MySqlParameter("?KurumKodu", MySqlDbType.Int64)
        };
        p[0].Value = sinavId;
        p[1].Value = kurumKodu;

        int ogrenciSayisi = Convert.ToInt32(helper.ExecuteScalar(sql, p));
        return ogrenciSayisi;

    }
    public void KayitEkle(TestOgrPuanInfo info)
    {
        const string sql = @"insert into testogrpuanlar (SinavId,OpaqId,Dogru,Yanlis,Bos,Puan,KurumKodu) values (?SinavId,?OpaqId,?Dogru,?Yanlis,?Bos,?Puan,?KurumKodu)";
        MySqlParameter[] pars =
        {
                new MySqlParameter("?SinavId", MySqlDbType.Int32),
                new MySqlParameter("?OpaqId", MySqlDbType.String),
                new MySqlParameter("?Dogru", MySqlDbType.Int32),
                new MySqlParameter("?Yanlis", MySqlDbType.Int32),
                new MySqlParameter("?Bos", MySqlDbType.String),
                new MySqlParameter("?Puan", MySqlDbType.Decimal),
                new MySqlParameter("?KurumKodu", MySqlDbType.Int32)
        };
        pars[0].Value = info.SinavId;
        pars[1].Value = info.OpaqId;
        pars[2].Value = info.Dogru;
        pars[3].Value = info.Yanlis;
        pars[4].Value = info.Bos;
        pars[5].Value = info.Puan;
        pars[6].Value = info.KurumKodu;
        helper.ExecuteNonQuery(sql, pars);
    }

    public void KayitGuncelle(TestOgrPuanInfo info)
    {
        const string sql = @"update testogrpuanlar set SinavId=?SinavId,OpaqId=?OpaqId,Dogru=?Dogru,Yanlis=?Yanlis,Bos=?Bos,Puan=?Puan where Id=?Id";
        MySqlParameter[] pars =
        {
                new MySqlParameter("?SinavId", MySqlDbType.Int32),
                new MySqlParameter("?OpaqId", MySqlDbType.String),
                new MySqlParameter("?Dogru", MySqlDbType.Int32),
                new MySqlParameter("?Yanlis", MySqlDbType.Int32),
                new MySqlParameter("?Bos", MySqlDbType.Int32),
                new MySqlParameter("?Puan", MySqlDbType.Decimal),
                new MySqlParameter("?Id", MySqlDbType.Int32)
            };
        pars[0].Value = info.SinavId;
        pars[1].Value = info.OpaqId;
        pars[2].Value = info.Dogru;
        pars[3].Value = info.Yanlis;
        pars[4].Value = info.Bos;
        pars[5].Value = info.Puan;
        pars[6].Value = info.Id;
        helper.ExecuteNonQuery(sql, pars);
    }
}