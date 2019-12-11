using System;
using System.Data;
using DAL;
using MySql.Data.MySqlClient;

namespace DAL
{
    public class LgsKazanimlarInfo
    {
        public int Id { get; set; }
        public int BransId { get; set; }
        public int Sinif { get; set; }
        public string KazanimNo { get; set; }
        public string Kazanim { get; set; }
    }
}

public class LgsKazanimlarDB
{
    readonly HelperDb helper = new HelperDb();

    public DataTable KayitlariGetir()
    {
        const string sql = "select * from lgskazanimlar order by Id asc";
        return helper.ExecuteDataSet(sql).Tables[0];
    }
    public DataTable KayitlariGetir(int brans, int sinif)
    {
        const string sql = @"SELECT k.*,b.BransAdi FROM lgskazanimlar AS k
        INNER JOIN branslar AS b ON b.Id = k.BransId where BransId=?BransId and k.Sinif=?Sinif order by k.KazanimNo asc";
        MySqlParameter[] p =
        {
                new MySqlParameter("?BransId", MySqlDbType.Int32),
                new MySqlParameter("?Sinif", MySqlDbType.Int32)
            };

        p[0].Value = brans;
        p[1].Value = sinif;
        return helper.ExecuteDataSet(sql, p).Tables[0];
    }
    public DataTable KazanimNoKazanimBirlestir(int brans, int sinif)
    {
        const string sql = @"SELECT k.*,CONCAT(k.KazanimNo,' - ',k.Kazanim) AS KazanimNoKazanim FROM lgskazanimlar AS k WHERE k.BransId=?BransId and k.Sinif=?Sinif order by k.KazanimNo asc";
        MySqlParameter[] p =
        {
            new MySqlParameter("?BransId", MySqlDbType.Int32),
            new MySqlParameter("?Sinif", MySqlDbType.Int32)
        };

        p[0].Value = brans;
        p[1].Value = sinif;
        return helper.ExecuteDataSet(sql, p).Tables[0];
    }
    public LgsKazanimlarInfo KayitBilgiGetir(string cmdText, params MySqlParameter[] param)
    {
        MySqlDataReader dr = helper.ExecuteReader(cmdText, param);
        return TabloAlanlar(dr);
    }

    private static LgsKazanimlarInfo TabloAlanlar(MySqlDataReader dr)
    {
        LgsKazanimlarInfo info = new LgsKazanimlarInfo();
        while (dr.Read())
        {
            info.Id = dr.GetMySayi("Id");
            info.BransId = dr.GetMySayi("BransId");
            info.Sinif = dr.GetMySayi("Sinif");
            info.KazanimNo = dr.GetMyMetin("KazanimNo");
            info.Kazanim = dr.GetMyMetin("Kazanim");
        }
        dr.Close();

        return info;
    }

    public LgsKazanimlarInfo KayitBilgiGetir(int id)
    {
        string cmdText = "select * from lgskazanimlar where Id=?Id";
        MySqlParameter param = new MySqlParameter("?Id", MySqlDbType.Int32) { Value = id };
        MySqlDataReader dr = helper.ExecuteReader(cmdText, param);
        return TabloAlanlar(dr);
    }

    public void KayitSil(int id)
    {
        const string sql = "delete from lgskazanimlar where Id=?Id";
        MySqlParameter p = new MySqlParameter("?Id", MySqlDbType.Int32) { Value = id };
        helper.ExecuteNonQuery(sql, p);
    }

    public void KayitEkle(LgsKazanimlarInfo info)
    {
        const string sql = @"insert into lgskazanimlar (BransId,Sinif,Kazanim,KazanimNo) values (?BransId,?Sinif,?Kazanim,?KazanimNo)";
        MySqlParameter[] pars =
        {
             new MySqlParameter("?BransId", MySqlDbType.Int32),
             new MySqlParameter("?Sinif", MySqlDbType.Int32),
             new MySqlParameter("?Kazanim", MySqlDbType.String),
             new MySqlParameter("?KazanimNo", MySqlDbType.String)
            };
        pars[0].Value = info.BransId;
        pars[1].Value = info.Sinif;
        pars[2].Value = info.Kazanim;
        pars[3].Value = info.KazanimNo;
        helper.ExecuteNonQuery(sql, pars);
    }

    public void KayitGuncelle(LgsKazanimlarInfo info)
    {
        const string sql = @"update lgskazanimlar set BransId=?BransId,Sinif=?Sinif,Kazanim=?Kazanim,KazanimNo=?KazanimNo where Id=?Id";
        MySqlParameter[] pars =
        {
             new MySqlParameter("?BransId", MySqlDbType.Int32),
             new MySqlParameter("?Sinif", MySqlDbType.Int32),
             new MySqlParameter("?Kazanim", MySqlDbType.String),
             new MySqlParameter("?KazanimNo", MySqlDbType.String),
             new MySqlParameter("?Id", MySqlDbType.Int32)
            };
        pars[0].Value = info.BransId;
        pars[1].Value = info.Sinif;
        pars[2].Value = info.Kazanim;
        pars[3].Value = info.KazanimNo;
        pars[4].Value = info.Id;
        helper.ExecuteNonQuery(sql, pars);
    }

    public bool KayitKontrol(int bransId, int sinif, string kazanimNo)
    {
        string cmdText = "select count(Id) from lgskazanimlar where BransId=?BransId and Sinif=?Sinif and KazanimNo=?KazanimNo";
        MySqlParameter[] pars =
        {
             new MySqlParameter("?BransId", MySqlDbType.Int32),
             new MySqlParameter("?Sinif", MySqlDbType.Int32),
             new MySqlParameter("?KazanimNo", MySqlDbType.String)
            };
        pars[0].Value = bransId;
        pars[1].Value = sinif;
        pars[2].Value = kazanimNo;
        bool sonuc = Convert.ToInt32(helper.ExecuteScalar(cmdText, pars)) > 0;
        return sonuc;
    }
    public bool KayitKontrol(int bransId, int sinif, string kazanimNo, int id)
    {
        string cmdText = "select count(Id) from lgskazanimlar where BransId=?BransId and Sinif=?Sinif and KazanimNo=?KazanimNo and Id<>?Id";
        MySqlParameter[] pars =
        {
             new MySqlParameter("?BransId", MySqlDbType.Int32),
             new MySqlParameter("?Sinif", MySqlDbType.Int32),
             new MySqlParameter("?KazanimNo", MySqlDbType.String),
             new MySqlParameter("?Id", MySqlDbType.Int32)
            };
        pars[0].Value = bransId;
        pars[1].Value = sinif;
        pars[2].Value = kazanimNo;
        pars[3].Value = id;
        bool sonuc = Convert.ToInt32(helper.ExecuteScalar(cmdText, pars)) > 0;
        return sonuc;
    }
}

