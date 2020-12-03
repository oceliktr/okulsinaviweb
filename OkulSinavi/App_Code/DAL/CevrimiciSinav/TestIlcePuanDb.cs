using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using MySql.Data.MySqlClient;

/// <summary>
/// Summary description for TestIlcePuanDb
/// </summary>
public class TestIlcePuanDb
{
    private readonly HelperDb helper = new HelperDb();
    public DataTable IlceAnlikKatilimOranlari(int sinavId)
    {
        const string sql = @"SELECT i.IlceAdi,
                                (SELECT COUNT(DISTINCT(oc.OpaqId)) FROM testogrcevaplar AS oc
                                INNER JOIN testkutuk AS k ON oc.OpaqId=k.OpaqId
                                WHERE k.IlceAdi=i.IlceAdi AND oc.SinavId=?SinavId) AS KatilanOgrenciSayisi,
                                (SELECT COUNT(k2.OpaqId) FROM testkutuk AS k2 WHERE k2.IlceAdi=i.IlceAdi) AS OgrenciSayisi,
                                (SELECT COUNT(DISTINCT(k.KurumKodu)) FROM testogrcevaplar AS oc
                                INNER JOIN testkutuk AS k ON oc.OpaqId=k.OpaqId
                                WHERE k.IlceAdi=i.IlceAdi AND oc.SinavId=?SinavId) AS KatilanKurumSayisi,
                                (SELECT COUNT(DISTINCT(k2.KurumKodu)) FROM testkutuk AS k2 WHERE k2.IlceAdi=i.IlceAdi) AS KurumSayisi

                                FROM ilceler AS i";
        MySqlParameter p = new MySqlParameter("?SinavId", MySqlDbType.Int32) { Value = sinavId };
        return helper.ExecuteDataSet(sql, p).Tables[0];
    }
    public DataTable KatilimOranlari(int sinavId)
    {
        const string sql = @"SELECT ip.IlceAdi,ip.KurumKodu,k.KurumAdi,ip.OgrSayisi AS KatilanOgrSayisi,(SELECT COUNT(Id) FROM testkutuk WHERE KurumKodu=ip.KurumKodu) AS OgrenciSayisi FROM testilcepuanlar AS ip
                            LEFT JOIN kurumlar AS k ON k.KurumKodu=ip.KurumKodu
                            WHERE ip.SinavId=?SinavId ORDER BY ip.OgrSayisi DESC ";
            MySqlParameter p = new MySqlParameter("?SinavId", MySqlDbType.Int32) { Value = sinavId };
        return helper.ExecuteDataSet(sql,p).Tables[0];
    }
    public DataTable OkulPuanOrtalamalari(int sinavId)
    {
        const string sql = @"SELECT i.SinavId,i.IlceAdi,k.KurumAdi,i.OgrSayisi,i.Dogru,i.Yanlis,i.Puan FROM testilcepuanlar AS i
                                LEFT JOIN kurumlar AS k ON k.KurumKodu=i.KurumKodu
                                WHERE i.SinavId=?SinavId ORDER BY i.Puan desc";
        MySqlParameter p = new MySqlParameter("?SinavId", MySqlDbType.Int32) { Value = sinavId };
        return helper.ExecuteDataSet(sql, p).Tables[0];
    }
    public DataTable IlcePuanOrtalamalari(int sinavId)
    {
        const string sql = @"SELECT i.IlceAdi,SUM(i.OgrSayisi) AS OgrSayisi,SUM(i.Dogru)/SUM(i.OgrSayisi) AS Dogru,SUM(i.Yanlis)/SUM(i.OgrSayisi) AS Yanlis,AVG(i.Puan) AS Puan FROM testilcepuanlar AS i
                            WHERE i.SinavId=?SinavId GROUP BY i.IlceAdi ORDER BY puan DESC";
        MySqlParameter p = new MySqlParameter("?SinavId", MySqlDbType.Int32) { Value = sinavId };
        return helper.ExecuteDataSet(sql, p).Tables[0];
    }
    public DataTable IlceKatilimOranlari(int sinavId)
    {
        const string sql = @"SELECT 
                            ip.IlceAdi,
                            COUNT(ip.KurumKodu) AS KatilanKurumSayisi,(SELECT COUNT(DISTINCT(tk.KurumKodu)) FROM testkutuk AS tk WHERE tk.IlceAdi=ip.IlceAdi) AS ToplamKurumSayisi,
                            SUM(ip.OgrSayisi) AS KatilanOgrenciSayisi,(SELECT COUNT(tk.OpaqId) FROM testkutuk AS tk WHERE tk.IlceAdi=ip.IlceAdi) AS ToplamOgrenciSayisi,
                            AVG(ip.Puan) AS PuanOrtalama
                            FROM testilcepuanlar AS ip 
                            WHERE ip.SinavId=?SinavId 
                            GROUP BY ip.IlceAdi 
                            ORDER BY PuanOrtalama DESC";
        MySqlParameter p = new MySqlParameter("?SinavId", MySqlDbType.Int32) { Value = sinavId };
        return helper.ExecuteDataSet(sql, p).Tables[0];
    }
    public DataTable KayitlariGetir(int sinavId, int kurumKodu)
    {
        const string sql = @"SELECT k.KurumAdi,ip.* FROM testilcepuanlar AS ip 
                            INNER JOIN kurumlar AS k ON k.KurumKodu=ip.KurumKodu
                            WHERE ip.KurumKodu=?KurumKodu AND ip.SinavId=?SinavId";
        MySqlParameter[] p =
        {
            new MySqlParameter("?SinavId", MySqlDbType.Int32),
            new MySqlParameter("?KurumKodu", MySqlDbType.Int32)
        };
        p[0].Value = sinavId;
        p[1].Value = kurumKodu;
        return helper.ExecuteDataSet(sql, p).Tables[0];
    }
    public DataTable KayitlariGetir(int sinavId, string ilceAdi)
    {
        const string sql = @"SELECT k.KurumAdi,ip.* FROM testilcepuanlar AS ip 
                            INNER JOIN kurumlar AS k ON k.KurumKodu=ip.KurumKodu
                            WHERE ip.IlceAdi=?IlceAdi AND ip.SinavId=?SinavId ORDER BY k.KurumAdi";
        MySqlParameter[] p =
        {
            new MySqlParameter("?SinavId", MySqlDbType.Int32),
            new MySqlParameter("?IlceAdi", MySqlDbType.String)
        };
        p[0].Value = sinavId;
        p[1].Value = ilceAdi;
        return helper.ExecuteDataSet(sql, p).Tables[0];
    }

    //public List<TestIlcePuanInfo> KayitlariDiziyeGetir(int sinavId, int KurumKodu)
    //{
    //    const string sql = "select * from testilcepuanlar  WHERE KurumKodu=?KurumKodu order by Id asc";
    //    MySqlParameter[] p =
    //    {
    //        new MySqlParameter("?SinavId", MySqlDbType.Int32),
    //        new MySqlParameter("?KurumKodu", MySqlDbType.Int32)
    //    };
    //    p[0].Value = sinavId;
    //    p[1].Value = KurumKodu;
    //    DataTable dt = helper.ExecuteDataSet(sql, p).Tables[0];
    //    List<TestIlcePuanInfo> table = new List<TestIlcePuanInfo>();
    //    foreach (DataRow k in dt.Rows)
    //    {
    //        table.Add(new TestIlcePuanInfo(Convert.ToInt32(k["Id"].ToString()), Convert.ToInt32(k["SinavId"].ToString()), k["IlceAdi"].ToString(),Convert.ToInt32(k["KurumKodu"].ToString()), Convert.ToInt32(k["Dogru"].ToString()), Convert.ToInt32(k["Yanlis"].ToString()), Convert.ToInt32(k["Bos"].ToString()), Convert.ToDecimal(k["Puan"].ToString())));
    //    }
    //    return table;
    //}

    private static TestIlcePuanInfo TabloAlanlar(MySqlDataReader dr)
    {
        TestIlcePuanInfo info = new TestIlcePuanInfo();
        while (dr.Read())
        {
            info.Id = dr.GetMySayi("Id");
            info.SinavId = dr.GetMySayi("SinavId");
            info.IlceAdi = dr.GetMyMetin("IlceAdi");
            info.OgrSayisi = dr.GetMySayi("OgrSayisi");
            info.KurumKodu = dr.GetMySayi("KurumKodu");
            info.Dogru = dr.GetMySayi("Dogru");
            info.Yanlis = dr.GetMySayi("Yanlis");
            info.Bos = dr.GetMySayi("Bos");
            info.Puan = dr.GetDecimal("Puan");
        }

        dr.Close();
        return info;
    }

    public TestIlcePuanInfo KayitBilgiGetir(int sinavId, int KurumKodu)
    {
        string cmdText = "select * from testilcepuanlar where SinavId=?SinavId and KurumKodu=?KurumKodu";
        MySqlParameter[] pars =
        {
            new MySqlParameter("?SinavId", MySqlDbType.Int32),
            new MySqlParameter("?KurumKodu", MySqlDbType.Int32)
        };
        pars[0].Value = sinavId;
        pars[1].Value = KurumKodu;

        MySqlDataReader dr = helper.ExecuteReader(cmdText, pars);
        var info = TabloAlanlar(dr);

        return info;
    }


    public void SinavPuanlariniSil(int sinavId)
    {
        const string sql = "delete from testilcepuanlar where SinavId=?SinavId";
        MySqlParameter p = new MySqlParameter("?SinavId", MySqlDbType.Int32) { Value = sinavId };
        helper.ExecuteNonQuery(sql, p);
    }
    public void KayitEkle(TestIlcePuanInfo info)
    {
        const string sql = @"insert into testilcepuanlar (SinavId,KurumKodu,Dogru,Yanlis,Bos,Puan,IlceAdi,OgrSayisi) values (?SinavId,?KurumKodu,?Dogru,?Yanlis,?Bos,?Puan,?IlceAdi,?OgrSayisi)";
        MySqlParameter[] pars =
        {
                new MySqlParameter("?SinavId", MySqlDbType.Int32),
                new MySqlParameter("?KurumKodu", MySqlDbType.Int32),
                new MySqlParameter("?Dogru", MySqlDbType.Int32),
                new MySqlParameter("?Yanlis", MySqlDbType.Int32),
                new MySqlParameter("?Bos", MySqlDbType.String),
                new MySqlParameter("?Puan", MySqlDbType.Decimal),
                new MySqlParameter("?IlceAdi", MySqlDbType.String),
                new MySqlParameter("?OgrSayisi", MySqlDbType.Int32)
        };
        pars[0].Value = info.SinavId;
        pars[1].Value = info.KurumKodu;
        pars[2].Value = info.Dogru;
        pars[3].Value = info.Yanlis;
        pars[4].Value = info.Bos;
        pars[5].Value = info.Puan;
        pars[6].Value = info.IlceAdi;
        pars[7].Value = info.OgrSayisi;
        helper.ExecuteNonQuery(sql, pars);
    }

    public void KayitGuncelle(TestIlcePuanInfo info)
    {
        const string sql = @"update testilcepuanlar set Dogru=?Dogru,Yanlis=?Yanlis,Bos=?Bos,Puan=?Puan,OgrSayisi=?OgrSayisi where Id=?Id";
        MySqlParameter[] pars =
        {
                new MySqlParameter("?Dogru", MySqlDbType.Int32),
                new MySqlParameter("?Yanlis", MySqlDbType.Int32),
                new MySqlParameter("?Bos", MySqlDbType.Int32),
                new MySqlParameter("?Puan", MySqlDbType.Decimal),
                new MySqlParameter("?OgrSayisi", MySqlDbType.Int32),
                new MySqlParameter("?Id", MySqlDbType.Int32)
            };
        pars[0].Value = info.Dogru;
        pars[1].Value = info.Yanlis;
        pars[2].Value = info.Bos;
        pars[3].Value = info.Puan;
        pars[4].Value = info.OgrSayisi;
        pars[5].Value = info.Id;
        helper.ExecuteNonQuery(sql, pars);
    }
}