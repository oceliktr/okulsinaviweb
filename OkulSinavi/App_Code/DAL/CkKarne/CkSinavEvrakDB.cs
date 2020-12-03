using System;
using System.Data;
using MySql.Data.MySqlClient;


    public class CkSinavEvrakInfo
    {
        public int Id { get; set; }
        public string Aciklama { get; set; }
        public string Url { get; set; }
        public string Kurumlar { get; set; }
        public DateTime BaslangicTarihi { get; set; }
        public DateTime BitisTarihi { get; set; }
        public int Hit { get; set; }
    }

    public class CkSinavEvrakDb
    {
        private readonly HelperDb _helper = new HelperDb();

        public DataTable KayitlariGetir()
        {
            const string sql = "select * from sinavevrak order by Id asc";
            return _helper.ExecuteDataSet(sql).Tables[0];
        }
        public DataTable KayitlariGetir(string kurumKodu)
        {
            string sql = string.Format("select * from sinavevrak where Kurumlar like '%{0}%' order by Id asc", kurumKodu);
            return _helper.ExecuteDataSet(sql).Tables[0];
        }

        public CkSinavEvrakInfo KayitBilgiGetir(string cmdText, params MySqlParameter[] param)
        {
            MySqlDataReader dr = _helper.ExecuteReader(cmdText, param);
            return TabloAlanlar(dr);
        }

        private static CkSinavEvrakInfo TabloAlanlar(MySqlDataReader dr)
        {
            CkSinavEvrakInfo info = new CkSinavEvrakInfo();
            while (dr.Read())
            {
                info.Id = dr.GetMySayi("Id");
                info.Aciklama = dr.GetMyMetin("Aciklama");
                info.Url = dr.GetMyMetin("Url");
                info.Kurumlar = dr.GetMyMetin("Kurumlar");
                info.BaslangicTarihi = dr.GetMyTarih("BaslangicTarihi");
                info.BitisTarihi = dr.GetMyTarih("BitisTarihi");
                info.Hit = dr.GetMySayi("Hit");
            }
            dr.Close();

            return info;
        }

        public CkSinavEvrakInfo KayitBilgiGetir(int id)
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

        public void KayitEkle(CkSinavEvrakInfo info)
        {
            const string sql =
                @"insert into sinavevrak (Aciklama,Url,Kurumlar,BaslangicTarihi,BitisTarihi) values (?Aciklama,?Url,?Kurumlar,?BaslangicTarihi,?BitisTarihi)";
            MySqlParameter[] pars =
            {
                new MySqlParameter("?Aciklama", MySqlDbType.String),
                new MySqlParameter("?Url", MySqlDbType.String),
                new MySqlParameter("?Kurumlar", MySqlDbType.String),
                new MySqlParameter("?BaslangicTarihi", MySqlDbType.DateTime),
                new MySqlParameter("?BitisTarihi", MySqlDbType.DateTime)
            };
            pars[0].Value = info.Aciklama;
            pars[1].Value = info.Url;
            pars[2].Value = info.Kurumlar;
            pars[3].Value = info.BaslangicTarihi;
            pars[4].Value = info.BitisTarihi;
            _helper.ExecuteNonQuery(sql, pars);
        }

        public void KayitGuncelle(CkSinavEvrakInfo info)
        {
            const string sql =
                @"update sinavevrak set Aciklama=?Aciklama,Url=?Url,Kurumlar=?Kurumlar,BaslangicTarihi=?BaslangicTarihi,BitisTarihi=?BitisTarihi where Id=?Id";
            MySqlParameter[] pars =
            {
                new MySqlParameter("?Aciklama", MySqlDbType.String),
                new MySqlParameter("?Url", MySqlDbType.String),
                new MySqlParameter("?Kurumlar", MySqlDbType.String),
                new MySqlParameter("?BaslangicTarihi", MySqlDbType.DateTime),
                new MySqlParameter("?BitisTarihi", MySqlDbType.DateTime),
                new MySqlParameter("?Id", MySqlDbType.Int32)
            };
            pars[0].Value = info.Aciklama;
            pars[1].Value = info.Url;
            pars[2].Value = info.Kurumlar;
            pars[3].Value = info.BaslangicTarihi;
            pars[4].Value = info.BitisTarihi;
            pars[5].Value = info.Id;
            _helper.ExecuteNonQuery(sql, pars);
        }

        public void KayitGuncelle(int hit,int id)
        {
            const string sql =@"update sinavevrak set Hit=?Hit where Id=?Id";
            MySqlParameter[] pars =
            {
                new MySqlParameter("?Hit", MySqlDbType.Int32),
                new MySqlParameter("?Id", MySqlDbType.Int32)
            };
            pars[0].Value = hit;
            pars[1].Value = id;
            _helper.ExecuteNonQuery(sql, pars);
        }
    }

