using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using MySql.Data.MySqlClient;

/// <summary>
/// Summary description for TestIstatistik
/// </summary>
public class TestIstatistikDb
{
    private readonly HelperDb helper = new HelperDb();
    public TestIstatistik SinavIstatistik(int sinavId, int sinif)
    {
        const string sql = @"SELECT Count(DISTINCT(OpaqId)) as SinavaGirenSayisi, 
                            (SELECT COUNT(Id)  FROM testkutuk WHERE testkutuk.Sinifi=?Sinifi) AS ToplamOgrenciSayisi,
                            (SELECT COUNT(DISTINCT k.KurumKodu) FROM testogrcevaplar AS oc INNER JOIN testkutuk AS k ON k.OpaqId=oc.OpaqId AND oc.SinavId=?SinavId) AS SinavaKatilanKurumSayisi,
                            (SELECT COUNT(DISTINCT KurumKodu) FROM testkutuk WHERE Sinifi=?Sinifi) AS KurumSayisi
                            FROM testogrcevaplar WHERE SinavId=?SinavId";
        MySqlParameter[] p =
        {
            new MySqlParameter("?SinavId", MySqlDbType.Int32),
            new MySqlParameter("?Sinifi", MySqlDbType.Int32),
        };
        p[0].Value = sinavId;
        p[1].Value = sinif;
        MySqlDataReader dr = helper.ExecuteReader(sql, p);

        TestIstatistik info = new TestIstatistik();
        while (dr.Read())
        {
            info.SinavaGirenSayisi = dr.GetMySayi("SinavaGirenSayisi");
            info.ToplamOgrenciSayisi = dr.GetMySayi("ToplamOgrenciSayisi");
            info.SinavaKatilanKurumSayisi = dr.GetMySayi("SinavaKatilanKurumSayisi");
            info.KurumSayisi = dr.GetMySayi("KurumSayisi");
        }

        return info;
    }
    public TestIstatistikDb()
    {
        //
        // TODO: Add constructor logic here
        //
    }
}