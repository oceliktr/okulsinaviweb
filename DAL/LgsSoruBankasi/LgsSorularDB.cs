using System;
using System.Data;
using DAL;
using MySql.Data.MySqlClient;

namespace DAL
{
    public class LgsSorularInfo
    {
        public int Id { get; set; }
        public int KullaniciId { get; set; }
        public int SinavId { get; set; }
        public int BransId { get; set; }
        public int Sinif { get; set; }
        public string Kazanim { get; set; }
        public string SoruUrl { get; set; }
        public DateTime Tarih { get; set; }
        public int Onay { get; set; }


    }

    public class LgsSorularDB
    {
        readonly HelperDb helper = new HelperDb();

        public DataTable KayitlariGetir()
        {
            const string sql = "select * from lgssorular order by Id desc";
            return helper.ExecuteDataSet(sql).Tables[0];
        }
        public DataTable KayitlariGetir(int sinavId, int kullanicId, int bransId, int sinif, int durum)
        {
            string sql = @"SELECT k.AdiSoyadi, b.BransAdi,lgs.* FROM lgssorular AS lgs
                            left JOIN branslar AS b ON lgs.BransId=b.Id
                            left JOIN kullanicilar AS k ON k.Id=lgs.KullaniciId where lgs.Onay=?Onay";

            if (sinavId != 0)
                sql += " and lgs.SinavId=?SinavId";
            if (kullanicId != 0)
                sql += " and lgs.KullaniciId=?KullaniciId";
            if (bransId != 0)
                sql += " and lgs.BransId=?BransId";
            if (sinif != 0)
                sql += " and lgs.Sinif=?Sinif";

            sql += " order by lgs.Id desc";

            MySqlParameter[] p =
            {
            new MySqlParameter("?SinavId", MySqlDbType.Int32),
            new MySqlParameter("?KullaniciId", MySqlDbType.Int32),
            new MySqlParameter("?BransId", MySqlDbType.Int32),
            new MySqlParameter("?Sinif", MySqlDbType.Int32),
            new MySqlParameter("?Onay", MySqlDbType.Int32)
            };

            p[0].Value = sinavId;
            p[1].Value = kullanicId;
            p[2].Value = bransId;
            p[3].Value = sinif;
            p[4].Value = durum;
            return helper.ExecuteDataSet(sql, p).Tables[0];
        }
        public DataTable KayitlariGetir(int kullaniciId)
        {
            const string sql = "select * from lgssorular where KullaniciId=?KullaniciId order by Id desc";
            MySqlParameter param = new MySqlParameter("?KullaniciId", MySqlDbType.Int32) { Value = kullaniciId };
            return helper.ExecuteDataSet(sql, param).Tables[0];
        }
        public LgsSorularInfo KayitBilgiGetir(string cmdText, params MySqlParameter[] param)
        {
            MySqlDataReader dr = helper.ExecuteReader(cmdText, param);
            LgsSorularInfo info = new LgsSorularInfo();
            while (dr.Read())
            {
                info.Id = dr.GetMySayi("Id");
                info.KullaniciId = dr.GetMySayi("KullaniciId");
                info.SinavId = dr.GetMySayi("SinavId");
                info.BransId = dr.GetMySayi("BransId");
                info.Sinif = dr.GetMySayi("Sinif");
                info.Kazanim = dr.GetMyMetin("Kazanim");
                info.SoruUrl = dr.GetMyMetin("SoruUrl");
                info.Onay = dr.GetMySayi("Onay");
            }
            dr.Close();

            return info;
        }

        public LgsSorularInfo KayitBilgiGetir(int id, int kullaniciId)
        {
            string cmdText = "select * from lgssorular where Id=?Id && KullaniciId=?KullaniciId";
            MySqlParameter[] param =
            {
            new MySqlParameter("?Id", MySqlDbType.Int32),
            new MySqlParameter("?KullaniciId", MySqlDbType.Int32)
        };
            param[0].Value = id;
            param[1].Value = kullaniciId;
            MySqlDataReader dr = helper.ExecuteReader(cmdText, param);
            LgsSorularInfo info = new LgsSorularInfo();
            while (dr.Read())
            {
                info.Id = dr.GetMySayi("Id");
                info.KullaniciId = dr.GetMySayi("KullaniciId");
                info.SinavId = dr.GetMySayi("SinavId");
                info.BransId = dr.GetMySayi("BransId");
                info.Sinif = dr.GetMySayi("Sinif");
                info.Kazanim = dr.GetMyMetin("Kazanim");
                info.SoruUrl = dr.GetMyMetin("SoruUrl");
                info.Onay = dr.GetMySayi("Onay");
            }
            dr.Close();

            return info;
        }
        public LgsSorularInfo KayitBilgiGetir(int id)
        {
            string cmdText = "select * from lgssorular where Id=?Id";
            MySqlParameter[] param =
            {
                new MySqlParameter("?Id", MySqlDbType.Int32)
            };
            param[0].Value = id;
            MySqlDataReader dr = helper.ExecuteReader(cmdText, param);
            LgsSorularInfo info = new LgsSorularInfo();
            while (dr.Read())
            {
                info.Id = dr.GetMySayi("Id");
                info.KullaniciId = dr.GetMySayi("KullaniciId");
                info.SinavId = dr.GetMySayi("SinavId");
                info.BransId = dr.GetMySayi("BransId");
                info.Sinif = dr.GetMySayi("Sinif");
                info.Kazanim = dr.GetMyMetin("Kazanim");
                info.SoruUrl = dr.GetMyMetin("SoruUrl");
                info.Onay = dr.GetMySayi("Onay");
            }
            dr.Close();

            return info;
        }

        public void KayitSil(int id)
        {
            const string sql = "delete from lgssorular where Id=?Id";
            MySqlParameter p = new MySqlParameter("?Id", MySqlDbType.Int32) { Value = id };
            helper.ExecuteNonQuery(sql, p);
        }

        public void KayitEkle(LgsSorularInfo info)
        {
            const string sql = @"insert into lgssorular (KullaniciId,SinavId,BransId,Sinif,Kazanim,SoruUrl,Tarih,Onay) values (?KullaniciId,?SinavId,?BransId,?Sinif,?Kazanim,?SoruUrl,?Tarih,?Onay)";
            MySqlParameter[] pars =
            {
             new MySqlParameter("?KullaniciId", MySqlDbType.Int32),
             new MySqlParameter("?SinavId", MySqlDbType.Int32),
             new MySqlParameter("?BransId", MySqlDbType.Int32),
             new MySqlParameter("?Sinif", MySqlDbType.Int32),
             new MySqlParameter("?Kazanim", MySqlDbType.String),
             new MySqlParameter("?SoruUrl", MySqlDbType.String),
             new MySqlParameter("?Tarih", MySqlDbType.DateTime),
             new MySqlParameter("?Onay", MySqlDbType.Int32),
            };
            pars[0].Value = info.KullaniciId;
            pars[1].Value = info.SinavId;
            pars[2].Value = info.BransId;
            pars[3].Value = info.Sinif;
            pars[4].Value = info.Kazanim;
            pars[5].Value = info.SoruUrl;
            pars[6].Value = info.Tarih;
            pars[7].Value = info.Onay;
            helper.ExecuteNonQuery(sql, pars);
        }
        public void KayitGuncelleAdmin(LgsSorularInfo info)
        {
            const string sql = @"update lgssorular set KullaniciId=?KullaniciId,SinavId=?SinavId,BransId=?BransId,Sinif=?Sinif,Kazanim=?Kazanim,SoruUrl=?SoruUrl,Onay=?Onay where Id=?Id";
            MySqlParameter[] pars =
            {
                new MySqlParameter("?KullaniciId", MySqlDbType.Int32),
                new MySqlParameter("?SinavId", MySqlDbType.Int32),
                new MySqlParameter("?BransId", MySqlDbType.Int32),
                new MySqlParameter("?Sinif", MySqlDbType.Int32),
                new MySqlParameter("?Kazanim", MySqlDbType.String),
                new MySqlParameter("?SoruUrl", MySqlDbType.String),
                new MySqlParameter("?Onay", MySqlDbType.Int32),
                new MySqlParameter("?Id", MySqlDbType.Int32),
            };
            pars[0].Value = info.KullaniciId;
            pars[1].Value = info.SinavId;
            pars[2].Value = info.BransId;
            pars[3].Value = info.Sinif;
            pars[4].Value = info.Kazanim;
            pars[5].Value = info.SoruUrl;
            pars[6].Value = info.Onay;
            pars[7].Value = info.Id;
            helper.ExecuteNonQuery(sql, pars);
        }
        public void KayitGuncelle(LgsSorularInfo info)
        {
            const string sql = @"update lgssorular set Kazanim=?Kazanim,SoruUrl=?SoruUrl where Id=?Id";
            MySqlParameter[] pars =
            {
                new MySqlParameter("?Kazanim", MySqlDbType.String),
                new MySqlParameter("?SoruUrl", MySqlDbType.String),
                new MySqlParameter("?Id", MySqlDbType.Int32)
            };
            pars[0].Value = info.Kazanim;
            pars[1].Value = info.SoruUrl;
            pars[2].Value = info.Id;
            helper.ExecuteNonQuery(sql, pars);
        }
        public void KayitOnay(int id,int onay)
        {
            const string sql = @"update lgssorular set Onay=?Onay where Id=?Id";
            MySqlParameter[] pars =
            {
                new MySqlParameter("?Onay", MySqlDbType.Int32),
                new MySqlParameter("?Id", MySqlDbType.Int32)
            };
            pars[0].Value = onay;
            pars[1].Value = id;
            helper.ExecuteNonQuery(sql, pars);
        }
    }

}