using MySql.Data.MySqlClient;
using System.Data;

/// <summary>
/// Summary description for TestOkulCevapDb
/// </summary>
public class TestOkulCevapDb
{
    readonly HelperDb helper = new HelperDb();
    public DataTable KayitlariGetir()
    {
        const string sql = "select * from testokulcevaplar order by Id asc";
        return helper.ExecuteDataSet(sql).Tables[0];
    }

    public DataTable TamamlanmamisSinavlar(string opaqId)
    {
        string sql = @"SELECT DISTINCT(s.Id),s.SinavAdi FROM testogrcevaplar AS oc 
                    INNER JOIN testsinavlar AS s ON s.Id=oc.SinavId
                    WHERE oc.OpaqId=?OpaqId AND oc.Dogru=0 AND oc.Yanlis=0 AND 
                    (oc.Cevap LIKE '%A%' OR oc.Cevap LIKE '%B%' OR oc.Cevap LIKE '%C%' OR oc.Cevap LIKE '%D%' OR oc.Cevap LIKE '%E%')";
        MySqlParameter p = new MySqlParameter("?OpaqId", MySqlDbType.String) { Value = opaqId };
        return helper.ExecuteDataSet(sql,p).Tables[0];
    }

    private static TestOkulCevapInfo TabloAlanlar(MySqlDataReader dr)
    {
        TestOkulCevapInfo info = new TestOkulCevapInfo();
        while (dr.Read())
        {
            info.Id = dr.GetMySayi("Id");
            info.OturumId = dr.GetMySayi("OturumId");
            info.KurumKodu = dr.GetMySayi("KurumKodu");
            info.Dogru = dr.GetMySayi("Dogru");
            info.Yanlis = dr.GetMySayi("Yanlis");
            info.Bos = dr.GetMySayi("Bos");
            info.BransId = dr.GetMySayi("BransId");
        }

        dr.Close();
        return info;
    }
    public TestOkulCevapInfo KayitBilgiGetir(int oturumId, int kurumKodu,int bransId)
    {
        string cmdText = "select * from testokulcevaplar where OturumId=?OturumId and KurumKodu=?KurumKodu and BransId=?BransId";
        MySqlParameter[] pars =
        {
            new MySqlParameter("?OturumId", MySqlDbType.Int32),
            new MySqlParameter("?KurumKodu", MySqlDbType.Int32),
            new MySqlParameter("?BransId", MySqlDbType.Int32)
        };
        pars[0].Value = oturumId;
        pars[1].Value = kurumKodu;
        pars[2].Value = bransId;

        MySqlDataReader dr = helper.ExecuteReader(cmdText, pars);
        var info = TabloAlanlar(dr);

        return info;
    }
    public void KayitSil(int id)
    {
        const string sql = "delete from testokulcevaplar where Id=?Id";
        MySqlParameter p = new MySqlParameter("?Id", MySqlDbType.Int32) { Value = id };
        helper.ExecuteNonQuery(sql, p);
    }
    public void KayitEkle(TestOkulCevapInfo info)
    {
        const string sql = @"insert into testokulcevaplar (OturumId,KurumKodu,Dogru,Yanlis,Bos,BransId) values (?OturumId,?KurumKodu,?Dogru,?Yanlis,?Bos,?BransId)";
        MySqlParameter[] pars =
        {
                new MySqlParameter("?OturumId", MySqlDbType.Int32),
                new MySqlParameter("?KurumKodu", MySqlDbType.Int32),
                new MySqlParameter("?Dogru", MySqlDbType.Int32),
                new MySqlParameter("?Yanlis", MySqlDbType.Int32),
                new MySqlParameter("?Bos", MySqlDbType.Int32),
                new MySqlParameter("?BransId", MySqlDbType.Int32)
        };
        pars[0].Value = info.OturumId;
        pars[1].Value = info.KurumKodu;
        pars[2].Value = info.Dogru;
        pars[3].Value = info.Yanlis;
        pars[4].Value = info.Bos;
        pars[5].Value = info.BransId;
        helper.ExecuteNonQuery(sql, pars);
    }

    public void KayitGuncelle(TestOkulCevapInfo info)
    {
        const string sql = @"update testokulcevaplar set Dogru=?Dogru,Yanlis=?Yanlis,Bos=?Bos where OturumId=?OturumId and KurumKodu=?KurumKodu and BransId=?BransId";
        MySqlParameter[] pars =
        {
            new MySqlParameter("?Dogru", MySqlDbType.Int32),
            new MySqlParameter("?Yanlis", MySqlDbType.Int32),
            new MySqlParameter("?Bos", MySqlDbType.Int32),
            new MySqlParameter("?OturumId", MySqlDbType.Int32),
            new MySqlParameter("?KurumKodu", MySqlDbType.Int32),
            new MySqlParameter("?BransId", MySqlDbType.Int32)
        };
        pars[0].Value = info.Dogru;
        pars[1].Value = info.Yanlis;
        pars[2].Value = info.Bos;
        pars[3].Value = info.OturumId;
        pars[4].Value = info.KurumKodu;
        pars[5].Value = info.BransId;
        helper.ExecuteNonQuery(sql, pars);
    }

}