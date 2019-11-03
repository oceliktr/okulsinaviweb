using System;
using System.Data;
using MySql.Data.MySqlClient;
namespace DAL
{
    public class KOgrenmeAlanlariInfo
    {
        public int Id { get; set; }
        public int BransId { get; set; }
        public int Sinif { get; set; }
        public int AnaKat { get; set; }
        public int AlanNo { get; set; }
        public string OgrenmeAlani { get; set; }
    }

    public class KOgrenmeAlanlariDB
    {
        readonly HelperDb helper = new HelperDb();

        public DataTable KayitlariGetir()
        {
            const string sql = "select * from kogrenmealanlari order by Id asc";
            return helper.ExecuteDataSet(sql).Tables[0];
        }
        public DataTable KayitlariGetir(int anaKat)
        {
            const string sql = "select * from kogrenmealanlari where AnaKat=?AnaKat order by Id asc";
            MySqlParameter p = new MySqlParameter("?AnaKat", MySqlDbType.Int32) { Value = anaKat };
            return helper.ExecuteDataSet(sql,p).Tables[0];
        }
        public DataTable KayitlariGetir(int brans, int sinif)
        {
            const string sql = "select *,IF(AnaKat=0,AlanNo*10+AnaKat,AnaKat*10+AlanNo) as Alanlar from kogrenmealanlari where BransId=?BransId and Sinif=?Sinif order by Alanlar asc";
            MySqlParameter[] p =
            {
                new MySqlParameter("?BransId", MySqlDbType.Int32),
                new MySqlParameter("?Sinif", MySqlDbType.Int32)
            };

            p[0].Value = brans;
            p[1].Value = sinif;
            return helper.ExecuteDataSet(sql, p).Tables[0];
        }
        public DataTable KayitlariGetir(int anaKat,int brans,int sinif)
        {
            const string sql = "select * from kogrenmealanlari where AnaKat=?AnaKat and BransId=?BransId and Sinif=?Sinif order by Id asc";
            MySqlParameter[] p =
            {
                new MySqlParameter("?AnaKat", MySqlDbType.Int32),
                new MySqlParameter("?BransId", MySqlDbType.Int32),
                new MySqlParameter("?Sinif", MySqlDbType.Int32)
            };

            p[0].Value = anaKat;
            p[1].Value = brans;
            p[2].Value = sinif;
            return helper.ExecuteDataSet(sql, p).Tables[0];
        }
        public KOgrenmeAlanlariInfo KayitBilgiGetir(string cmdText, params MySqlParameter[] param)
        {
            MySqlDataReader dr = helper.ExecuteReader(cmdText, param);
            return Tabloalanlar(dr);
        }

        private static KOgrenmeAlanlariInfo Tabloalanlar(MySqlDataReader dr)
        {
            KOgrenmeAlanlariInfo info = new KOgrenmeAlanlariInfo();
            while (dr.Read())
            {
                info.Id = dr.GetMySayi("Id");
                info.BransId = dr.GetMySayi("BransId");
                info.Sinif = dr.GetMySayi("Sinif");
                info.AnaKat = dr.GetMySayi("AnaKat");
                info.AlanNo = dr.GetMySayi("AlanNo");
                info.OgrenmeAlani = dr.GetMyMetin("OgrenmeAlani");
            }
            dr.Close();

            return info;
        }

        public KOgrenmeAlanlariInfo KayitBilgiGetir(int id)
        {
            string cmdText = "select * from kogrenmealanlari where Id=?Id";
            MySqlParameter param = new MySqlParameter("?Id", MySqlDbType.Int32) { Value = id };
            MySqlDataReader dr = helper.ExecuteReader(cmdText, param);
            return Tabloalanlar(dr);
        }

        public void KayitSil(int id)
        {
            const string sql = "delete from kogrenmealanlari where Id=?Id";
            MySqlParameter p = new MySqlParameter("?Id", MySqlDbType.Int32) { Value = id };
            helper.ExecuteNonQuery(sql, p);
        }

        public void KayitEkle(KOgrenmeAlanlariInfo info)
        {
            const string sql = @"insert into kogrenmealanlari (BransId,Sinif,AnaKat,OgrenmeAlani,AlanNo) values (?BransId,?Sinif,?AnaKat,?OgrenmeAlani,?AlanNo)";
            MySqlParameter[] pars =
            {
             new MySqlParameter("?BransId", MySqlDbType.Int32),
             new MySqlParameter("?Sinif", MySqlDbType.Int32),
             new MySqlParameter("?AnaKat", MySqlDbType.Int32),
             new MySqlParameter("?AlanNo", MySqlDbType.Int32),
             new MySqlParameter("?OgrenmeAlani", MySqlDbType.String)
            };
            pars[0].Value = info.BransId;
            pars[1].Value = info.Sinif;
            pars[2].Value = info.AnaKat;
            pars[3].Value = info.AlanNo;
            pars[4].Value = info.OgrenmeAlani;
            helper.ExecuteNonQuery(sql, pars);
        }

        public void KayitGuncelle(KOgrenmeAlanlariInfo info)
        {
            const string sql = @"update kogrenmealanlari set BransId=?BransId,Sinif=?Sinif,AnaKat=?AnaKat,OgrenmeAlani=?OgrenmeAlani,AlanNo=?AlanNo where Id=?Id";
            MySqlParameter[] pars =
            {
             new MySqlParameter("?BransId", MySqlDbType.Int32),
             new MySqlParameter("?Sinif", MySqlDbType.Int32),
             new MySqlParameter("?AnaKat", MySqlDbType.Int32),
             new MySqlParameter("?OgrenmeAlani", MySqlDbType.String),
             new MySqlParameter("?AlanNo", MySqlDbType.Int32),
             new MySqlParameter("?Id", MySqlDbType.Int32)
            };
            pars[0].Value = info.BransId;
            pars[1].Value = info.Sinif;
            pars[2].Value = info.AnaKat;
            pars[3].Value = info.OgrenmeAlani;
            pars[4].Value = info.AlanNo;
            pars[5].Value = info.Id;
            helper.ExecuteNonQuery(sql, pars);
        }
        public bool KayitKontrol(int bransId, int sinif,int alanNo,int anaKat)
        {
            string cmdText = "select count(Id) from kogrenmealanlari where BransId=?BransId and Sinif=?Sinif and AlanNo=?AlanNo and AnaKat=?AnaKat";
            MySqlParameter[] pars =
            {
             new MySqlParameter("?BransId", MySqlDbType.Int32),
             new MySqlParameter("?Sinif", MySqlDbType.Int32),
             new MySqlParameter("?AlanNo", MySqlDbType.Int32),
             new MySqlParameter("?AnaKat", MySqlDbType.Int32)
            };
            pars[0].Value = bransId;
            pars[1].Value = sinif;
            pars[2].Value = alanNo;
            pars[3].Value = anaKat;
            bool sonuc = Convert.ToInt32(helper.ExecuteScalar(cmdText, pars)) > 0;
            return sonuc;
        }
        public bool KayitKontrol(int bransId, int sinif, int alanNo, int anaKat, int id)
        {
            string cmdText = "select count(Id) from kogrenmealanlari where BransId=?BransId and Sinif=?Sinif and AlanNo=?AlanNo and AnaKat=?AnaKat and Id<>?Id";
            MySqlParameter[] pars =
            {
             new MySqlParameter("?BransId", MySqlDbType.Int32),
             new MySqlParameter("?Sinif", MySqlDbType.Int32),
             new MySqlParameter("?AlanNo", MySqlDbType.Int32),
             new MySqlParameter("?AnaKat", MySqlDbType.Int32),
             new MySqlParameter("?Id", MySqlDbType.Int32)
            };
            pars[0].Value = bransId;
            pars[1].Value = sinif;
            pars[2].Value = alanNo;
            pars[3].Value = anaKat;
            pars[4].Value = id;
            bool sonuc = Convert.ToInt32(helper.ExecuteScalar(cmdText, pars)) > 0;
            return sonuc;
        }
    }
}
