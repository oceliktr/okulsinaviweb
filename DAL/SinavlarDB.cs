using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using MySql.Data.MySqlClient;

namespace DAL
{
    public class SinavlarInfo
    {
        public int Id { get; set; }
        public string SinavAdi { get; set; }
        public string DonemAdi { get; set; }
        public string KitapcikTurleri { get; set; }
        public int SinavId { get; set; }
        public int VeriGirisi { get; set; }
        public SinavlarInfo()
        { }
        public SinavlarInfo(int id, string sinavAdi)
        {
            Id = id;
            SinavAdi = sinavAdi;
        }
        public SinavlarInfo(int id, string sinavAdi,string kitapcikTurleri)
        {
            Id = id;
            SinavAdi = sinavAdi;
            KitapcikTurleri = kitapcikTurleri;
        }
    }

    public class SinavlarDb
    {
        private readonly HelperDb helper = new HelperDb();

        public DataTable KayitlariGetir()
        {
            const string sql = "select * from sinavlar order by Id asc";
            return helper.ExecuteDataSet(sql).Tables[0];
        }
        public List<SinavlarInfo> KayitlariDiziyeGetir()
        {
            string sql = "select * from sinavlar order by Id asc";

            DataTable veriler = helper.ExecuteDataSet(sql).Tables[0];

            return
                (from DataRow row in veriler.Rows
                 select new SinavlarInfo(Convert.ToInt32(row["Id"]), row["SinavAdi"].ToString(), row["KitapcikTurleri"].ToString())).ToList();
        }
        public SinavlarInfo KayitBilgiGetir(string cmdText, params MySqlParameter[] param)
        {
            MySqlDataReader dr = helper.ExecuteReader(cmdText, param);
            return TabloAlanlar(dr);
        }
        private static SinavlarInfo TabloAlanlar(MySqlDataReader dr)
        {
            SinavlarInfo info = new SinavlarInfo();
            while (dr.Read())
            {
                info.Id = dr.GetMySayi("Id");
                info.SinavId = dr.GetMySayi("SinavId");
                info.SinavAdi = dr.GetMyMetin("SinavAdi");
                info.DonemAdi = dr.GetMyMetin("DonemAdi");
                info.KitapcikTurleri = dr.GetMyMetin("KitapcikTurleri");
                info.VeriGirisi = dr.GetMySayi("VeriGirisi");
            }
            dr.Close();

            return info;
        }
        public SinavlarInfo KayitBilgiGetir(int id)
        {
            string sql = "select * from sinavlar where Id=?Id";
            MySqlParameter p = new MySqlParameter("?Id", MySqlDbType.Int32) {Value = id};
            MySqlDataReader dr = helper.ExecuteReader(sql, p);
            return TabloAlanlar(dr);
        }
        public SinavlarInfo AktifSinavAdi()
        {
            string sql =
                "SELECT Ayarlar.Id, sinavlar.SinavAdi,sinavlar.DonemAdi,sinavlar.KitapcikTurleri, ayarlar.SinavId, ayarlar.VeriGirisi FROM ayarlar INNER JOIN sinavlar ON ayarlar.SinavId = sinavlar.Id WHERE ayarlar.Id=1";
            MySqlDataReader dr = helper.ExecuteReader(sql);
            return TabloAlanlar(dr);
        }
        public void KayitSil(int id)
        {
            const string sql = "delete from sinavlar where Id=?Id";
            MySqlParameter p = new MySqlParameter("?Id", MySqlDbType.Int32) { Value = id };
            helper.ExecuteNonQuery(sql, p);
        }
        public void KayitEkle(SinavlarInfo info)
        {
            const string sql = @"insert into sinavlar (SinavAdi,DonemAdi) values (?SinavAdi,?DonemAdi)";
            MySqlParameter[] pars =
            {
                new MySqlParameter("?SinavAdi", MySqlDbType.String),
                new MySqlParameter("?DonemAdi", MySqlDbType.String)
            };
            pars[0].Value = info.SinavAdi;
            pars[1].Value = info.DonemAdi;
            helper.ExecuteNonQuery(sql, pars);
        }
        public void KayitGuncelle(SinavlarInfo info)
        {
            const string sql = @"update sinavlar set SinavAdi=?SinavAdi where Id=?Id";
            MySqlParameter[] pars =
            {
                new MySqlParameter("?SinavAdi", MySqlDbType.String),
                new MySqlParameter("?DonemAdi", MySqlDbType.String),
                new MySqlParameter("?Id", MySqlDbType.Int32)
            };
            pars[0].Value = info.SinavAdi;
            pars[1].Value = info.DonemAdi;
            pars[2].Value = info.Id;
            helper.ExecuteNonQuery(sql, pars);
        }
    }

}
