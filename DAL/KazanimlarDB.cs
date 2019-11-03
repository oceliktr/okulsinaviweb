using System;
using System.Data;
using MySql.Data.MySqlClient;
namespace DAL
{
    public class KazanimlarInfo
    {
        public int Id { get; set; }
        public int BransId { get; set; }
        public int Sinif { get; set; }
        public int OgrenmeAlani { get; set; }
        public int AltOgrenmeAlani { get; set; }
        public int KazanimNo { get; set; }
        public string Kazanim { get; set; }
    }

    public class KazanimlarDB
    {
        readonly HelperDb helper = new HelperDb();

        public DataTable KayitlariGetir()
        {
            const string sql = "select * from kazanimlar order by Id asc";
            return helper.ExecuteDataSet(sql).Tables[0];
        }
        public DataTable KayitlariGetir(int brans, int sinif)
        {
            const string sql = "select * from kazanimlar where BransId=?BransId and Sinif=?Sinif order by OgrenmeAlani,AltOgrenmeAlani asc";
            MySqlParameter[] p =
            {
                new MySqlParameter("?BransId", MySqlDbType.Int32),
                new MySqlParameter("?Sinif", MySqlDbType.Int32)
            };

            p[0].Value = brans;
            p[1].Value = sinif;
            return helper.ExecuteDataSet(sql, p).Tables[0];
        }
        public DataTable KayitlariGetir(int brans, int sinif, int ogrenmeAlani, int altOgrenmeAlani)
        {
            const string sql = "select * from kazanimlar where BransId=?BransId and Sinif=?Sinif and OgrenmeAlani=?OgrenmeAlani and AltOgrenmeAlani=?AltOgrenmeAlani order by KazanimNo asc";
            MySqlParameter[] p =
            {
                new MySqlParameter("?BransId", MySqlDbType.Int32),
                new MySqlParameter("?Sinif", MySqlDbType.Int32),
                new MySqlParameter("?OgrenmeAlani", MySqlDbType.Int32),
                new MySqlParameter("?AltOgrenmeAlani", MySqlDbType.Int32)
            };

            p[0].Value = brans;
            p[1].Value = sinif;
            p[2].Value = ogrenmeAlani;
            p[3].Value = altOgrenmeAlani;
            return helper.ExecuteDataSet(sql, p).Tables[0];
        }
        public KazanimlarInfo KayitBilgiGetir(string cmdText, params MySqlParameter[] param)
        {
            MySqlDataReader dr = helper.ExecuteReader(cmdText, param);
            return TabloAlanlar(dr);
        }

        private static KazanimlarInfo TabloAlanlar(MySqlDataReader dr)
        {
            KazanimlarInfo info = new KazanimlarInfo();
            while (dr.Read())
            {
                info.Id = dr.GetMySayi("Id");
                info.BransId = dr.GetMySayi("BransId");
                info.Sinif = dr.GetMySayi("Sinif");
                info.OgrenmeAlani = dr.GetMySayi("OgrenmeAlani");
                info.AltOgrenmeAlani = dr.GetMySayi("AltOgrenmeAlani");
                info.KazanimNo = dr.GetMySayi("KazanimNo");
                info.Kazanim = dr.GetMyMetin("Kazanim");
            }
            dr.Close();

            return info;
        }

        public KazanimlarInfo KayitBilgiGetir(int id)
        {
            string cmdText = "select * from kazanimlar where Id=?Id";
            MySqlParameter param = new MySqlParameter("?Id", MySqlDbType.Int32) { Value = id };
            MySqlDataReader dr = helper.ExecuteReader(cmdText, param);
            return TabloAlanlar(dr);
        }

        public void KayitSil(int id)
        {
            const string sql = "delete from kazanimlar where Id=?Id";
            MySqlParameter p = new MySqlParameter("?Id", MySqlDbType.Int32) { Value = id };
            helper.ExecuteNonQuery(sql, p);
        }

        public void KayitEkle(KazanimlarInfo info)
        {
            const string sql = @"insert into kazanimlar (BransId,Sinif,OgrenmeAlani,AltOgrenmeAlani,Kazanim,KazanimNo) values (?BransId,?Sinif,?OgrenmeAlani,?AltOgrenmeAlani,?Kazanim,?KazanimNo)";
            MySqlParameter[] pars =
            {
             new MySqlParameter("?BransId", MySqlDbType.Int32),
             new MySqlParameter("?Sinif", MySqlDbType.Int32),
             new MySqlParameter("?OgrenmeAlani", MySqlDbType.Int32),
             new MySqlParameter("?AltOgrenmeAlani", MySqlDbType.Int32),
             new MySqlParameter("?Kazanim", MySqlDbType.String),
             new MySqlParameter("?KazanimNo", MySqlDbType.Int32)
            };
            pars[0].Value = info.BransId;
            pars[1].Value = info.Sinif;
            pars[2].Value = info.OgrenmeAlani;
            pars[3].Value = info.AltOgrenmeAlani;
            pars[4].Value = info.Kazanim;
            pars[5].Value = info.KazanimNo;
            helper.ExecuteNonQuery(sql, pars);
        }

        public void KayitGuncelle(KazanimlarInfo info)
        {
            const string sql = @"update kazanimlar set BransId=?BransId,Sinif=?Sinif,OgrenmeAlani=?OgrenmeAlani,AltOgrenmeAlani=?AltOgrenmeAlani,Kazanim=?Kazanim,KazanimNo=?KazanimNo where Id=?Id";
            MySqlParameter[] pars =
            {
             new MySqlParameter("?BransId", MySqlDbType.Int32),
             new MySqlParameter("?Sinif", MySqlDbType.Int32),
             new MySqlParameter("?OgrenmeAlani", MySqlDbType.Int32),
             new MySqlParameter("?AltOgrenmeAlani", MySqlDbType.Int32),
             new MySqlParameter("?Kazanim", MySqlDbType.String),
             new MySqlParameter("?KazanimNo", MySqlDbType.Int32),
             new MySqlParameter("?Id", MySqlDbType.Int32)
            };
            pars[0].Value = info.BransId;
            pars[1].Value = info.Sinif;
            pars[2].Value = info.OgrenmeAlani;
            pars[3].Value = info.AltOgrenmeAlani;
            pars[4].Value = info.Kazanim;
            pars[5].Value = info.KazanimNo;
            pars[6].Value = info.Id;
            helper.ExecuteNonQuery(sql, pars);
        }
        public bool KayitKontrol(int bransId, int sinif, int alanNo, int altAlanNo)
        {
            string cmdText = "select count(Id) from kazanimlar where BransId=?BransId and Sinif=?Sinif and OgrenmeAlani=?OgrenmeAlani and AltOgrenmeAlani=?AltOgrenmeAlani";
            MySqlParameter[] pars =
            {
             new MySqlParameter("?BransId", MySqlDbType.Int32),
             new MySqlParameter("?Sinif", MySqlDbType.Int32),
             new MySqlParameter("?OgrenmeAlani", MySqlDbType.Int32),
             new MySqlParameter("?AltOgrenmeAlani", MySqlDbType.Int32),
            };
            pars[0].Value = bransId;
            pars[1].Value = sinif;
            pars[2].Value = alanNo;
            pars[3].Value = altAlanNo;
            bool sonuc = Convert.ToInt32(helper.ExecuteScalar(cmdText, pars)) > 0;
            return sonuc;
        }
        public bool KayitKontrol(int bransId, int sinif, int alanNo, int altAlanNo, int kazanimNo)
        {
            string cmdText = "select count(Id) from kazanimlar where BransId=?BransId and Sinif=?Sinif and OgrenmeAlani=?OgrenmeAlani and AltOgrenmeAlani=?AltOgrenmeAlani and KazanimNo=?KazanimNo";
            MySqlParameter[] pars =
            {
             new MySqlParameter("?BransId", MySqlDbType.Int32),
             new MySqlParameter("?Sinif", MySqlDbType.Int32),
             new MySqlParameter("?OgrenmeAlani", MySqlDbType.Int32),
             new MySqlParameter("?AltOgrenmeAlani", MySqlDbType.Int32),
             new MySqlParameter("?KazanimNo", MySqlDbType.Int32)
            };
            pars[0].Value = bransId;
            pars[1].Value = sinif;
            pars[2].Value = alanNo;
            pars[3].Value = altAlanNo;
            pars[4].Value = kazanimNo;
            bool sonuc = Convert.ToInt32(helper.ExecuteScalar(cmdText, pars)) > 0;
            return sonuc;
        }
        public bool KayitKontrol(int bransId, int sinif, int alanNo, int altAlanNo, int kazanimNo, int id)
        {
            string cmdText = "select count(Id) from kazanimlar where BransId=?BransId and Sinif=?Sinif and OgrenmeAlani=?OgrenmeAlani and AltOgrenmeAlani=?AltOgrenmeAlani and KazanimNo=?KazanimNo and Id<>?Id";
            MySqlParameter[] pars =
            {
             new MySqlParameter("?BransId", MySqlDbType.Int32),
             new MySqlParameter("?Sinif", MySqlDbType.Int32),
             new MySqlParameter("?OgrenmeAlani", MySqlDbType.Int32),
             new MySqlParameter("?AltOgrenmeAlani", MySqlDbType.Int32),
             new MySqlParameter("?KazanimNo", MySqlDbType.Int32),
             new MySqlParameter("?Id", MySqlDbType.Int32)
            };
            pars[0].Value = bransId;
            pars[1].Value = sinif;
            pars[2].Value = alanNo;
            pars[3].Value = altAlanNo;
            pars[4].Value = kazanimNo;
            pars[5].Value = id;
            bool sonuc = Convert.ToInt32(helper.ExecuteScalar(cmdText, pars)) > 0;
            return sonuc;
        }
    }

}