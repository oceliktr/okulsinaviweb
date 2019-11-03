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
        public DateTime Tarihi { get; set; }
        public string SinavAdi { get; set; }
        public int SinavId { get; set; }
        public int VeriGirisi { get; set; }
        public SinavlarInfo()
        { }

        public SinavlarInfo(int id, string sinavAdi)
        {
            Id = id;
            SinavAdi = sinavAdi;
        }
    }

    public class SinavlarDb
    {
        private readonly HelperDb _helper = new HelperDb();

        public DataTable KayitlariGetir()
        {
            const string sql = "select * from sinavlar order by Id asc";
            return _helper.ExecuteDataSet(sql).Tables[0];
        }
        public List<SinavlarInfo> KayitlariDiziyeGetir()
        {
            string sql = "select * from sinavlar order by Id asc";

            DataTable veriler = _helper.ExecuteDataSet(sql).Tables[0];

            return
                (from DataRow row in veriler.Rows
                 select new SinavlarInfo(Convert.ToInt32(row["Id"]), row["SinavAdi"].ToString())).ToList();
        }
        public SinavlarInfo KayitBilgiGetir(string cmdText, params MySqlParameter[] param)
        {
            MySqlDataReader dr = _helper.ExecuteReader(cmdText, param);
            SinavlarInfo info = new SinavlarInfo();
            while (dr.Read())
            {
                info.Id = dr.GetMySayi("Id");
                info.SinavAdi = dr.GetMyMetin("SinavAdi");
                info.Tarihi = dr.GetMyTarih("Tarihi");
            }
            dr.Close();

            return info;
        }

        public SinavlarInfo KayitBilgiGetir(int id)
        {
            string sql = "select * from sinavlar where Id=?Id";
            MySqlParameter p = new MySqlParameter("?Id", MySqlDbType.Int32) {Value = id};
            MySqlDataReader dr = _helper.ExecuteReader(sql, p);
            SinavlarInfo info = new SinavlarInfo();
            while (dr.Read())
            {
                info.Id = dr.GetMySayi("Id");
                info.SinavAdi = dr.GetMyMetin("SinavAdi");
                info.Tarihi = dr.GetMyTarih("Tarihi");
            }
            dr.Close();

            return info;
        }

        public SinavlarInfo AktifSinavAdi()
        {
            string sql =
                "SELECT Ayarlar.Id, sinavlar.SinavAdi, ayarlar.SinavId, ayarlar.VeriGirisi FROM ayarlar INNER JOIN sinavlar ON ayarlar.SinavId = sinavlar.Id WHERE ayarlar.Id=1";
            MySqlDataReader dr = _helper.ExecuteReader(sql);
            SinavlarInfo info = new SinavlarInfo();
            while (dr.Read())
            {
                info.Id = dr.GetMySayi("Id");
                info.SinavAdi = dr.GetMyMetin("SinavAdi");
                info.Tarihi = dr.GetMyTarih("Tarihi");
                info.SinavId = dr.GetMySayi("SinavId");
                info.VeriGirisi = dr.GetMySayi("VeriGirisi");
            }
            dr.Close();

            return info;
        }

        public void KayitSil(int id)
        {
            const string sql = "delete from sinavlar where Id=?Id";
            MySqlParameter p = new MySqlParameter("?Id", MySqlDbType.Int32) { Value = id };
            _helper.ExecuteNonQuery(sql, p);
        }

        public void KayitEkle(SinavlarInfo info)
        {
            const string sql = @"insert into sinavlar (SinavAdi,Tarihi) values (?SinavAdi,?Tarihi)";
            MySqlParameter[] pars =
            {
                new MySqlParameter("?SinavAdi", MySqlDbType.String),
                new MySqlParameter("?Tarihi",MySqlDbType.DateTime) 
            };
            pars[0].Value = info.SinavAdi;
            pars[1].Value = info.Tarihi;
            _helper.ExecuteNonQuery(sql, pars);
        }

        public void KayitGuncelle(SinavlarInfo info)
        {
            const string sql = @"update sinavlar set SinavAdi=?SinavAdi,Tarihi=?Tarihi where Id=?Id";
            MySqlParameter[] pars =
            {
                new MySqlParameter("?SinavAdi", MySqlDbType.String),
                new MySqlParameter("?Tarihi",MySqlDbType.DateTime) ,
                new MySqlParameter("?Id", MySqlDbType.Int32)
            };
            pars[0].Value = info.SinavAdi;
            pars[1].Value = info.Tarihi;
            pars[2].Value = info.Id;
            _helper.ExecuteNonQuery(sql, pars);
        }
    }

}
