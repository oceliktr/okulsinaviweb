using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using MySql.Data.MySqlClient;

namespace DAL
{



    public class BranslarInfo
    {
        public int Id { get; set; }
        public string BransAdi { get; set; }

        public BranslarInfo()
        {
        }

        public BranslarInfo(int id, string bransAdi)
        {
            Id = id;
            BransAdi = bransAdi;
        }
    }

    public class BranslarDb
    {
        private readonly HelperDb _helper = new HelperDb();

        public DataTable KayitlariGetir()
        {
            const string sql = "select * from branslar order by Id asc";
            return _helper.ExecuteDataSet(sql).Tables[0];
        }

        public List<BranslarInfo> KayitlariDiziyeGetir()
        {
            string sql = "select * from branslar order by BransAdi asc";

            DataTable veriler = _helper.ExecuteDataSet(sql).Tables[0];

            return
                (from DataRow row in veriler.Rows
                 select new BranslarInfo(Convert.ToInt32(row["Id"]), row["BransAdi"].ToString())).ToList();
        }

        public BranslarInfo KayitBilgiGetir(string cmdText, params MySqlParameter[] param)
        {
            MySqlDataReader dr = _helper.ExecuteReader(cmdText, param);
            BranslarInfo info = new BranslarInfo();
            while (dr.Read())
            {
                info.Id = dr.GetMySayi("Id");
                info.BransAdi = dr.GetMyMetin("BransAdi");
            }
            dr.Close();

            return info;
        }
        public string BransAdi(int bransId)
        {
            string cmdText = "select * from branslar where Id=?Id";
            MySqlParameter param = new MySqlParameter("?Id", MySqlDbType.Int32) { Value = bransId };
            MySqlDataReader dr = _helper.ExecuteReader(cmdText, param);
            BranslarInfo info = new BranslarInfo();
            while (dr.Read())
            {
                info.Id = dr.GetMySayi("Id");
                info.BransAdi = dr.GetMyMetin("BransAdi");
            }
            dr.Close();

            return info.BransAdi;
        }
        public BranslarInfo KayitBilgiGetir(int id)
        {
            string cmdText = "select * from branslar where Id=?Id";
            MySqlParameter param = new MySqlParameter("?Id", MySqlDbType.Int32) { Value = id };
            MySqlDataReader dr = _helper.ExecuteReader(cmdText, param);
            BranslarInfo info = new BranslarInfo();
            while (dr.Read())
            {
                info.Id = dr.GetMySayi("Id");
                info.BransAdi = dr.GetMyMetin("BransAdi");
            }
            dr.Close();

            return info;
        }
        public int KayitKontrol(int bransId)
        {
            const string cmdText = "select count(Id) from kitapcikcevap where BransId=?BransId";
            const string cmdText2 = "select count(Id) from konumlar where BransId=?BransId";
            const string cmdText3 = "select count(Id) from rubrik where BransId=?BransId";
            MySqlParameter pars = new MySqlParameter("?BransId", MySqlDbType.Int32) { Value = bransId };
            int sonuc = Convert.ToInt32(_helper.ExecuteScalar(cmdText, pars));
            sonuc += Convert.ToInt32(_helper.ExecuteScalar(cmdText2, pars));
            sonuc += Convert.ToInt32(_helper.ExecuteScalar(cmdText3, pars));
            return sonuc;
        }
        public void KayitSil(int id)
        {
            const string sql = "delete from branslar where Id=?Id";
            MySqlParameter p = new MySqlParameter("?Id", MySqlDbType.Int32) { Value = id };
            _helper.ExecuteNonQuery(sql, p);
        }

        public void KayitEkle(BranslarInfo info)
        {
            const string sql = @"insert into branslar (BransAdi) values (?BransAdi)";
            MySqlParameter[] pars =
            {
                new MySqlParameter("?BransAdi", MySqlDbType.String)
            };
            pars[0].Value = info.BransAdi;
            _helper.ExecuteNonQuery(sql, pars);
        }

        public void KayitGuncelle(BranslarInfo info)
        {
            const string sql = @"update branslar set BransAdi=?BransAdi where Id=?Id";
            MySqlParameter[] pars =
            {
                new MySqlParameter("?BransAdi", MySqlDbType.String),
                new MySqlParameter("?Id", MySqlDbType.Int32),
            };
            pars[0].Value = info.BransAdi;
            pars[1].Value = info.Id;
            _helper.ExecuteNonQuery(sql, pars);
        }
    }

}