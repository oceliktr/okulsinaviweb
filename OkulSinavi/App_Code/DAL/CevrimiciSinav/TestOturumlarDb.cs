using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;

/// <summary>
/// Summary description for testoturumlarDb
/// </summary>
public class TestOturumlarDb
{
    private readonly HelperDb helper = new HelperDb();

    public DataTable KayitlariGetir()
    {
        const string sql = "select * from testoturumlar ORDER BY BitisTarihi desc";
        return helper.ExecuteDataSet(sql).Tables[0];
    }
    public DataTable DevamEdenSinavlar()
    {
        const string sql = @"SELECT s.Id,s.SinavAdi,s.Sinif,s.Puanlama,s.Aktif,s.OturumTercihi,s.BeklemeSuresi,s.Kurumlar,o.Id AS OturumId, o.OturumAdi,o.BaslamaTarihi,o.BitisTarihi,o.Sure FROM testoturumlar AS o
                            INNER JOIN testsinavlar AS s ON s.Id = o.SinavId WHERE o.BitisTarihi>=?Bugun AND s.Aktif=1 ORDER BY o.BitisTarihi ASC";
        MySqlParameter param = new MySqlParameter("?Bugun", MySqlDbType.Date)
        {
            Value = GenelIslemler.YerelTarih(true,true)
        };
        return helper.ExecuteDataSet(sql,param).Tables[0];
    }
    public DataTable KayitlariGetir(int sinavId)
    {
        const string sql = "select * from testoturumlar where SinavId=?SinavId ORDER BY SiraNo asc";
        MySqlParameter param = new MySqlParameter("?SinavId", MySqlDbType.Int32) { Value = sinavId };
        return helper.ExecuteDataSet(sql, param).Tables[0];
    }

    public List<TestOturumlarInfo> AktifTestler()
    {
        DateTime bugun = GenelIslemler.YerelTarih();
        const string sql = "select * from testoturumlar WHERE BaslamaTarihi<=?BaslamaTarihi AND BitisTarihi>=?BitisTarihi ORDER BY BitisTarihi";
        MySqlParameter[] pars =
        {
                new MySqlParameter("?BitisTarihi", MySqlDbType.DateTime),
                new MySqlParameter("?BaslamaTarihi", MySqlDbType.DateTime)
            };
        pars[0].Value = bugun;
        pars[1].Value = bugun;

        DataTable dt = helper.ExecuteDataSet(sql, pars).Tables[0];
        List<TestOturumlarInfo> table = new List<TestOturumlarInfo>();
        foreach (DataRow k in dt.Rows)
        {
            table.Add(new TestOturumlarInfo(Convert.ToInt32(k["Id"].ToString()), Convert.ToInt32(k["SinavId"].ToString()), Convert.ToInt32(k["SiraNo"].ToString()), Convert.ToInt32(k["Sure"].ToString()), k["OturumAdi"].ToString(), k["Aciklama"].ToString(), Convert.ToDateTime(k["BaslamaTarihi"].ToString()), Convert.ToDateTime(k["BitisTarihi"].ToString())));
        }
        return table;
    }

    public List<TestOturumlarInfo> Oturumlar(int sinavId)
    {
        const string sql = @"SELECT * from testoturumlar where SinavId=?SinavId order by SiraNo";
        MySqlParameter pars = new MySqlParameter("?SinavId", MySqlDbType.Int32) { Value = sinavId };

        DataTable dt = helper.ExecuteDataSet(sql, pars).Tables[0];
        List<TestOturumlarInfo> table = new List<TestOturumlarInfo>();
        foreach (DataRow k in dt.Rows)
        {
            table.Add(new TestOturumlarInfo(Convert.ToInt32(k["Id"].ToString()), Convert.ToInt32(k["SinavId"].ToString()), Convert.ToInt32(k["SiraNo"].ToString()), Convert.ToInt32(k["Sure"].ToString()), k["OturumAdi"].ToString(), k["Aciklama"].ToString(), Convert.ToDateTime(k["BaslamaTarihi"].ToString()), Convert.ToDateTime(k["BitisTarihi"].ToString())));
        }
        return table;
    }

    public OturumSinavJoinModel KayitBilgiGetir(int oturumId)
    {
        string cmdText = @"SELECT o.*,s.Sinif,s.SinavAdi,s.Aktif,s.Puanlama from testoturumlar AS o 
        INNER JOIN testsinavlar AS s ON s.Id = o.SinavId
        WHERE o.Id = ?Id";
        MySqlParameter param = new MySqlParameter("?Id", MySqlDbType.Int32) { Value = oturumId };
        MySqlDataReader dr = helper.ExecuteReader(cmdText, param);
        OturumSinavJoinModel info = new OturumSinavJoinModel();
        while (dr.Read())
        {
            info.Id = dr.GetMySayi("Id");
            info.Aciklama = dr.GetMyMetin("Aciklama");
            info.Sure = dr.GetMySayi("Sure");
            info.SiraNo = dr.GetMySayi("SiraNo");
            info.OturumAdi = dr.GetMyMetin("OturumAdi");
            info.BaslamaTarihi = dr.GetMyTarih("BaslamaTarihi");
            info.BitisTarihi = dr.GetMyTarih("BitisTarihi");
            info.Aktif = dr.GetMySayi("Aktif");
            info.Sinif = dr.GetMySayi("Sinif");
            info.Puanlama = dr.GetMySayi("Puanlama");
            info.SinavId = dr.GetMySayi("SinavId");
            info.SinavAdi = dr.GetMyMetin("SinavAdi");
        }

        dr.Close();
        return info;
    }


    /// <summary>
    /// Oturuma ait sınav bilgisi gelir
    /// </summary>
    /// <param name="oturumId"></param>
    /// <returns></returns>
    public TestOturumlarInfo SinavIdGetir(int oturumId)
    {
        string cmdText = @"SELECT SinavId from testoturumlar AS o WHERE o.Id = ?Id";
        MySqlParameter param = new MySqlParameter("?Id", MySqlDbType.Int32) { Value = oturumId };
        MySqlDataReader dr = helper.ExecuteReader(cmdText, param);
        TestOturumlarInfo info = new TestOturumlarInfo();
        while (dr.Read())
        {
            info.SinavId = dr.GetMySayi("SinavId");
        }

        dr.Close();
        return info;
    }

    public int KayitSil(int id)
    {
        const string sql = "delete from testoturumlar where Id=?Id";
        MySqlParameter p = new MySqlParameter("?Id", MySqlDbType.Int32) { Value = id };
        return helper.ExecuteNonQuery(sql, p);
    }
    public int OturumlariSil(int sinavId)
    {
        TestSorularDb sorularDb = new TestSorularDb();
        TestOgrCevapDb ogrCevapDb = new TestOgrCevapDb();


        DataTable dt = helper.ExecuteDataSet("SELECT * from testoturumlar where SinavId=" + sinavId).Tables[0];
        foreach (DataRow k in dt.Rows)
        {
            int oturumId = Convert.ToInt32(k["Id"].ToString());

            ogrCevapDb.OturumCevaplariniSil(oturumId);

            sorularDb.OturumSorulariniSil(oturumId);
        }

        const string sql = "delete from testoturumlar where SinavId=?SinavId";
        MySqlParameter p = new MySqlParameter("?SinavId", MySqlDbType.Int32) { Value = sinavId };
        return helper.ExecuteNonQuery(sql, p);
    }
    public long KayitEkle(TestOturumlarInfo info)
    {
        const string sql = @"insert into testoturumlar (OturumAdi,Aciklama,Sure,BaslamaTarihi,BitisTarihi,SinavId,SiraNo) values (?OturumAdi,?Aciklama,?Sure,?BaslamaTarihi,?BitisTarihi,?SinavId,?SiraNo);";
        MySqlParameter[] pars =
        {
                new MySqlParameter("?OturumAdi", MySqlDbType.String),
                new MySqlParameter("?Aciklama", MySqlDbType.String),
                new MySqlParameter("?Sure", MySqlDbType.Int32),
                new MySqlParameter("?BaslamaTarihi", MySqlDbType.DateTime),
                new MySqlParameter("?BitisTarihi", MySqlDbType.DateTime),
                new MySqlParameter("?SinavId", MySqlDbType.Int32),
                new MySqlParameter("?SiraNo", MySqlDbType.Int32)
            };
        pars[0].Value = info.OturumAdi;
        pars[1].Value = info.Aciklama;
        pars[2].Value = info.Sure;
        pars[3].Value = info.BaslamaTarihi;
        pars[4].Value = info.BitisTarihi;
        pars[5].Value = info.SinavId;
        pars[6].Value = info.SiraNo;

        long sonId;
        helper.ExecuteNonQuery(out sonId, sql, pars);
        return sonId;
    }

    public int KayitGuncelle(TestOturumlarInfo info)
    {
        const string sql = @"update testoturumlar set OturumAdi=?OturumAdi,Aciklama=?Aciklama,Sure=?Sure,BaslamaTarihi=?BaslamaTarihi,BitisTarihi=?BitisTarihi,SiraNo=?SiraNo where Id=?Id";
        MySqlParameter[] pars =
        {
                new MySqlParameter("?OturumAdi", MySqlDbType.String),
                new MySqlParameter("?Aciklama", MySqlDbType.String),
                new MySqlParameter("?Sure", MySqlDbType.Int32),
                new MySqlParameter("?BaslamaTarihi", MySqlDbType.DateTime),
                new MySqlParameter("?BitisTarihi", MySqlDbType.DateTime),
                new MySqlParameter("?SiraNo", MySqlDbType.Int32),
                new MySqlParameter("?Id", MySqlDbType.Int32)
            };
        pars[0].Value = info.OturumAdi;
        pars[1].Value = info.Aciklama;
        pars[2].Value = info.Sure;
        pars[3].Value = info.BaslamaTarihi;
        pars[4].Value = info.BitisTarihi;
        pars[5].Value = info.SiraNo;
        pars[6].Value = info.Id;
        return helper.ExecuteNonQuery(sql, pars);
    }

    public bool KayitKontrol(int sinavId, int siraNo, int id)
    {
        string cmdText = "select SinavId,SiraNo,Id from testoturumlar where SinavId=?SinavId and SiraNo=?SiraNo and Id<>?Id";
        MySqlParameter[] pars =
        {
            new MySqlParameter("?SinavId", MySqlDbType.Int32),
            new MySqlParameter("?SiraNo", MySqlDbType.Int32),
            new MySqlParameter("?Id", MySqlDbType.Int32)
        };
        pars[0].Value = sinavId;
        pars[1].Value = siraNo;
        pars[2].Value = id;
        bool sonuc = Convert.ToInt32(helper.ExecuteScalar(cmdText, pars)) > 0;
        return sonuc;
    }
}