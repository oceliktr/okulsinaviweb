using System;
using System.Data;
using MySql.Data.MySqlClient;

namespace DAL
{



    public class SinavEvrakInfo
    {
        public int Id { get; set; }
        public int SinavId { get; set; }
        public string Url { get; set; }
        public string Kurumlar { get; set; }
        public DateTime BaslangicTarihi { get; set; }
        public DateTime BitisTarihi { get; set; }
        public DateTime Indirdi { get; set; }
        public int Hit { get; set; }
    }

    public class SinavEvrakDb
    {
        private readonly HelperDb _helper = new HelperDb();

        public DataTable KayitlariGetir()
        {
            const string sql = "select sinavevrak.*,sinavlar.SinavAdi from sinavevrak,sinavlar where sinavevrak.SinavId=sinavlar.Id order by Id asc";
            return _helper.ExecuteDataSet(sql).Tables[0];
        }
        public DataTable KayitlariGetir(string kurumKodu)
        {
            string sql = string.Format("select sinavevrak.*,sinavlar.SinavAdi from sinavevrak,sinavlar where sinavevrak.SinavId=sinavlar.Id and sinavevrak.Kurumlar like '%{0}%' order by Id asc",kurumKodu);
            return _helper.ExecuteDataSet(sql).Tables[0];
        }

        public SinavEvrakInfo KayitBilgiGetir(string cmdText, params MySqlParameter[] param)
        {
            MySqlDataReader dr = _helper.ExecuteReader(cmdText, param);
            return TabloAlanlar(dr);
        }

        private static SinavEvrakInfo TabloAlanlar(MySqlDataReader dr)
        {
            SinavEvrakInfo info = new SinavEvrakInfo();
            while (dr.Read())
            {
                info.Id = dr.GetMySayi("Id");
                info.SinavId = dr.GetMySayi("SinavId");
                info.Url = dr.GetMyMetin("Url");
                info.Kurumlar = dr.GetMyMetin("Kurumlar");
                info.BaslangicTarihi = dr.GetMyTarih("BaslangicTarihi");
                info.BitisTarihi = dr.GetMyTarih("BitisTarihi");
                info.Indirdi = dr.GetMyTarih("Indirdi");
                info.Hit = dr.GetMySayi("Hit");
            }
            dr.Close();

            return info;
        }

        public SinavEvrakInfo KayitBilgiGetir(int id)
        {
            string cmdText = "select * from sinavevrak where Id=?Id";
            MySqlParameter param = new MySqlParameter("?Id", MySqlDbType.Int32) {Value = id};
            MySqlDataReader dr = _helper.ExecuteReader(cmdText, param);
            return TabloAlanlar(dr);
        }

        public void KayitSil(int id)
        {
            const string sql = "delete from sinavevrak where Id=?Id";
            MySqlParameter p = new MySqlParameter("?Id", MySqlDbType.Int32) {Value = id};
            _helper.ExecuteNonQuery(sql, p);
        }

        public void KayitEkle(SinavEvrakInfo info)
        {
            const string sql =
                @"insert into sinavevrak (SinavId,Url,Kurumlar,BaslangicTarihi,BitisTarihi) values (?SinavId,?Url,?Kurumlar,?BaslangicTarihi,?BitisTarihi)";
            MySqlParameter[] pars =
            {
                new MySqlParameter("?SinavId", MySqlDbType.Int32),
                new MySqlParameter("?Url", MySqlDbType.String),
                new MySqlParameter("?Kurumlar", MySqlDbType.String),
                new MySqlParameter("?BaslangicTarihi", MySqlDbType.DateTime),
                new MySqlParameter("?BitisTarihi", MySqlDbType.DateTime)
            };
            pars[0].Value = info.SinavId;
            pars[1].Value = info.Url;
            pars[2].Value = info.Kurumlar;
            pars[3].Value = info.BaslangicTarihi;
            pars[4].Value = info.BitisTarihi;
            _helper.ExecuteNonQuery(sql, pars);
        }

        public void KayitGuncelle(SinavEvrakInfo info)
        {
            const string sql =
                @"update sinavevrak set SinavId=?SinavId,Url=?Url,Kurumlar=?Kurumlar,BaslangicTarihi=?BaslangicTarihi,BitisTarihi=?BitisTarihi where Id=?Id";
            MySqlParameter[] pars =
            {
                new MySqlParameter("?SinavId", MySqlDbType.Int32),
                new MySqlParameter("?Url", MySqlDbType.String),
                new MySqlParameter("?Kurumlar", MySqlDbType.String),
                new MySqlParameter("?BaslangicTarihi", MySqlDbType.DateTime),
                new MySqlParameter("?BitisTarihi", MySqlDbType.DateTime),
                new MySqlParameter("?Id", MySqlDbType.Int32)
            };
            pars[0].Value = info.SinavId;
            pars[1].Value = info.Url;
            pars[2].Value = info.Kurumlar;
            pars[3].Value = info.BaslangicTarihi;
            pars[4].Value = info.BitisTarihi;
            pars[5].Value = info.Id;
            _helper.ExecuteNonQuery(sql, pars);
        }

        public void KayitGuncelle(int hit,DateTime tarih,int id)
        {
            const string sql =@"update sinavevrak set Hit=?Hit,Indirdi=?Indirdi where Id=?Id";
            MySqlParameter[] pars =
            {
                new MySqlParameter("?Hit", MySqlDbType.Int32),
                new MySqlParameter("?Indirdi", MySqlDbType.DateTime),
                new MySqlParameter("?Id", MySqlDbType.Int32)
            };
            pars[0].Value = hit;
            pars[1].Value = DateTime.Now;
            pars[2].Value = id;
            _helper.ExecuteNonQuery(sql, pars);
        }
    }
}
