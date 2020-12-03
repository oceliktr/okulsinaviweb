using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using MySql.Data.MySqlClient;

/// <summary>
/// Summary description for TestOgrPuanDb
/// </summary>
public class TestOkulPuanDb
{
    private readonly HelperDb helper = new HelperDb();

    public DataTable KayitlariGetir()
    {
        const string sql = "select * from testokulpuanlar order by Id asc";
        return helper.ExecuteDataSet(sql).Tables[0];
    }


    public List<TestOkulPuanInfo> KayitlariDiziyeGetir(int sinavId,int kurumKodu)
    {
        const string sql = "select * from testokulpuanlar  WHERE SinavId=?SinavId and KurumKodu=?KurumKodu order by Id asc";
        MySqlParameter[] p =
        {
            new MySqlParameter("?SinavId", MySqlDbType.Int32),
            new MySqlParameter("?KurumKodu", MySqlDbType.Int32),
        };

        p[0].Value = sinavId;
        p[1].Value = kurumKodu;
        DataTable dt = helper.ExecuteDataSet(sql, p).Tables[0];
        List<TestOkulPuanInfo> table = new List<TestOkulPuanInfo>();
        foreach (DataRow k in dt.Rows)
        {
            table.Add(new TestOkulPuanInfo(Convert.ToInt32(k["Id"].ToString()), Convert.ToInt32(k["SinavId"].ToString()), Convert.ToInt32(k["KurumKodu"].ToString()), Convert.ToInt32(k["BransId"].ToString()), Convert.ToInt32(k["Dogru"].ToString()), Convert.ToInt32(k["Yanlis"].ToString()), Convert.ToInt32(k["Bos"].ToString())));
        }
        return table;
    }

    //private static TestOkulPuanInfo TabloAlanlar(MySqlDataReader dr)
    //{
    //    TestOkulPuanInfo info = new TestOkulPuanInfo();
    //    while (dr.Read())
    //    {
    //        info.Id = dr.GetMySayi("Id");
    //        info.SinavId = dr.GetMySayi("SinavId");
    //        info.BransId = dr.GetMySayi("BransId");
    //        info.KurumKodu = dr.GetMySayi("KurumKodu");
    //        info.Dogru = dr.GetMySayi("Dogru");
    //        info.Yanlis = dr.GetMySayi("Yanlis");
    //        info.Bos = dr.GetMySayi("Bos");
    //    }

    //    dr.Close();
    //    return info;
    //}

    public void KayitSil(int id)
    {
        const string sql = "delete from testokulpuanlar where Id=?Id";
        MySqlParameter p = new MySqlParameter("?Id", MySqlDbType.Int32) { Value = id };
        helper.ExecuteNonQuery(sql, p);
    }
    public void SinavPuanlariniSil(int sinavId)
    {
        const string sql = "delete from testokulpuanlar where SinavId=?SinavId";
        MySqlParameter p = new MySqlParameter("?SinavId", MySqlDbType.Int32) { Value = sinavId };
        helper.ExecuteNonQuery(sql, p);
    }

    public void KayitEkle(TestOkulPuanInfo info)
    {
        const string sql = @"insert into testokulpuanlar (SinavId,BransId,KurumKodu,Dogru,Yanlis,Bos) values (?SinavId,?BransId,?KurumKodu,?Dogru,?Yanlis,?Bos)";
        MySqlParameter[] pars =
        {
                new MySqlParameter("?SinavId", MySqlDbType.Int32),
                new MySqlParameter("?BransId", MySqlDbType.Int32),
                new MySqlParameter("?KurumKodu", MySqlDbType.Int32),
                new MySqlParameter("?Dogru", MySqlDbType.Int32),
                new MySqlParameter("?Yanlis", MySqlDbType.Int32),
                new MySqlParameter("?Bos", MySqlDbType.String)
        };
        pars[0].Value = info.SinavId;
        pars[1].Value = info.BransId;
        pars[2].Value = info.KurumKodu;
        pars[3].Value = info.Dogru;
        pars[4].Value = info.Yanlis;
        pars[5].Value = info.Bos;
        helper.ExecuteNonQuery(sql, pars);
    }

    public void KayitGuncelle(TestOkulPuanInfo info)
    {
        const string sql = @"update testokulpuanlar set SinavId=?SinavId,BransId=?BransId,KurumKodu=?KurumKodu,Dogru=?Dogru,Yanlis=?Yanlis,Bos=?Bos where Id=?Id";
        MySqlParameter[] pars =
        {
            new MySqlParameter("?SinavId", MySqlDbType.Int32),
            new MySqlParameter("?BransId", MySqlDbType.Int32),
            new MySqlParameter("?KurumKodu", MySqlDbType.Int32),
            new MySqlParameter("?Dogru", MySqlDbType.Int32),
            new MySqlParameter("?Yanlis", MySqlDbType.Int32),
            new MySqlParameter("?Bos", MySqlDbType.String),
                new MySqlParameter("?Id", MySqlDbType.Int32)
            };
        pars[0].Value = info.SinavId;
        pars[1].Value = info.BransId;
        pars[2].Value = info.KurumKodu;
        pars[3].Value = info.Dogru;
        pars[4].Value = info.Yanlis;
        pars[5].Value = info.Bos;
        pars[6].Value = info.Id;
        helper.ExecuteNonQuery(sql, pars);
    }
    public void DYBGuncelle(TestOkulPuanInfo info)
    {
        const string sql = @"update testokulpuanlar set Dogru=?Dogru,Yanlis=?Yanlis,Bos=?Bos where SinavId=?SinavId and BransId=?BransId and KurumKodu=?KurumKodu";
        MySqlParameter[] pars =
        {
            new MySqlParameter("?SinavId", MySqlDbType.Int32),
            new MySqlParameter("?BransId", MySqlDbType.Int32),
            new MySqlParameter("?KurumKodu", MySqlDbType.Int32),
            new MySqlParameter("?Dogru", MySqlDbType.Int32),
            new MySqlParameter("?Yanlis", MySqlDbType.Int32),
            new MySqlParameter("?Bos", MySqlDbType.String),
        };
        pars[0].Value = info.SinavId;
        pars[1].Value = info.BransId;
        pars[2].Value = info.KurumKodu;
        pars[3].Value = info.Dogru;
        pars[4].Value = info.Yanlis;
        pars[5].Value = info.Bos;
        helper.ExecuteNonQuery(sql, pars);
    }
}