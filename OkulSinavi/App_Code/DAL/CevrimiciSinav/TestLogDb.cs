using MySql.Data.MySqlClient;
using System;
using System.Data;

/// <summary>
/// Summary description for TestLogDb
/// </summary>
public class TestLogDb
{
    private readonly HelperDb helper = new HelperDb();

    public DataTable KayitlariGetir(string opaqId)
    {
        const string sql = "select * from testlog where OpaqId=?OpaqId order by Tarih desc limit 50";
        MySqlParameter pars = new MySqlParameter("?OpaqId", MySqlDbType.String) {Value = opaqId};

        var table = helper.ExecuteDataSet(sql, pars).Tables[0];
        return table;
    }
    public DataTable LogAra(string aranan)
    {
        string sql = string.Format(@"SELECT l.*,k.Adi,k.Soyadi from testlog AS l 
                                     INNER JOIN testkutuk AS k ON l.OpaqId = k.OpaqId 
                                    WHERE l.OpaqId LIKE '%{0}%' OR l.Rapor LIKE '%{0}%' OR l.Grup LIKE '%{0}%' order  by Tarih desc limit 200", aranan);
        return helper.ExecuteDataSet(sql).Tables[0];
    }
    private static TestLogInfo TabloAlanlar(MySqlDataReader dr)
    {
        TestLogInfo info = new TestLogInfo();
        while (dr.Read())
        {
            info.Id = dr.GetMySayi("Id");
            info.OpaqId = dr.GetMyMetin("OpaqId");
            info.Grup = dr.GetMyMetin("Grup");
            info.Rapor = dr.GetMyMetin("Rapor");
            info.Tarih = dr.GetMyTarih("Tarih");
        }

        dr.Close();
        return info;
    }
    public int KayitSil(int gun)
    {
        const string sql = "delete from testlog where Tarih>?Tarih";
        MySqlParameter p = new MySqlParameter("?Tarih", MySqlDbType.DateTime) { Value = GenelIslemler.YerelTarih().AddDays(gun) };
        return helper.ExecuteNonQuery(sql, p);
    }
    public int KayitSilOgrenci(string opaqId)
    {
        const string sql = "delete from testlog where OpaqId=?OpaqId";
        MySqlParameter p = new MySqlParameter("?OpaqId", MySqlDbType.String) { Value = opaqId };
        return helper.ExecuteNonQuery(sql, p);
    }

    public void KayitEkle(TestLogInfo info)
    {
        const string sql = @"insert into testlog (OpaqId,Grup,Rapor,Tarih) values (?OpaqId,?Grup,?Rapor,?Tarih)";
        MySqlParameter[] pars =
        {
                new MySqlParameter("?OpaqId", MySqlDbType.String),
                new MySqlParameter("?Grup", MySqlDbType.String),
                new MySqlParameter("?Rapor", MySqlDbType.String),
                new MySqlParameter("?Tarih", MySqlDbType.DateTime)
        };
        pars[0].Value = info.OpaqId;
        pars[1].Value = info.Grup;
        pars[2].Value = info.Rapor;
        pars[3].Value = GenelIslemler.YerelTarih();
        helper.ExecuteNonQuery(sql, pars);
    }

}