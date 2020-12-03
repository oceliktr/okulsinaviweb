using System;
using System.Data;
using MySql.Data.MySqlClient;

    public class CkKarneLogInfo
    {
        public int Id { get; set; }
        public int Sinif { get; set; }
        public int Brans { get; set; }
        public int KullaniciId { get; set; }
        public int KurumKodu { get; set; }
        public DateTime Tarih { get; set; }
        public int Say { get; set; }
        public string Aciklama { get; set; }
    }

    public class CkKarneLogDB
    {
        readonly HelperDb helper = new HelperDb();

        public DataTable KayitlariGetir()
        {
            const string sql = "select * from ckkarnelog order by Id asc";
            return helper.ExecuteDataSet(sql).Tables[0];
        }

        public CkKarneLogInfo KayitBilgiGetir(string cmdText, params MySqlParameter[] param)
        {
            MySqlDataReader dr = helper.ExecuteReader(cmdText, param);
            CkKarneLogInfo info = new CkKarneLogInfo();
            while (dr.Read())
            {
                info.Id = dr.GetMySayi("Id");
                info.KullaniciId = dr.GetMySayi("KullaniciId");
                info.KurumKodu = dr.GetMySayi("KurumKodu");
                info.Sinif = dr.GetMySayi("Sinif");
                info.Brans = dr.GetMySayi("Brans");
                info.Say = dr.GetMySayi("Say");
                info.Aciklama = dr.GetMyMetin("Aciklama");
            }
            dr.Close();

            return info;
        }
        public CkKarneLogInfo KayitBilgiGetir(int userId, int kurumKodu)
        {
            string cmdText = "select * from ckkarnelog where KullaniciId=?KullaniciId and KurumKodu=?KurumKodu";
            MySqlParameter[] param = 
            {
                new MySqlParameter("?KullaniciId", MySqlDbType.Int32), 
                new MySqlParameter("?KurumKodu", MySqlDbType.Int32),
            };
            param[0].Value = userId;
            param[1].Value = kurumKodu;

            MySqlDataReader dr = helper.ExecuteReader(cmdText, param);
            CkKarneLogInfo info = new CkKarneLogInfo();
            while (dr.Read())
            {
                info.Id = dr.GetMySayi("Id");
                info.KullaniciId = dr.GetMySayi("KullaniciId");
                info.KurumKodu = dr.GetMySayi("KurumKodu");
                info.Say = dr.GetMySayi("Say");
                info.Sinif = dr.GetMySayi("Sinif");
                info.Brans = dr.GetMySayi("Brans");
                info.Aciklama = dr.GetMyMetin("Aciklama");
            }
            dr.Close();

            return info;
        }
        public CkKarneLogInfo KayitBilgiGetir(int userId, int kurumKodu,int sinif,int brans)
        {
            string cmdText = "select * from ckkarnelog where KullaniciId=?KullaniciId and KurumKodu=?KurumKodu and Sinif=?Sinif and Brans=?Brans";
            MySqlParameter[] param =
            {
                new MySqlParameter("?KullaniciId", MySqlDbType.Int32),
                new MySqlParameter("?KurumKodu", MySqlDbType.Int32),
                new MySqlParameter("?Sinif", MySqlDbType.Int32),
                new MySqlParameter("?Brans", MySqlDbType.Int32),
            };
            param[0].Value = userId;
            param[1].Value = kurumKodu;
            param[2].Value = sinif;
            param[3].Value = brans;

            MySqlDataReader dr = helper.ExecuteReader(cmdText, param);
            CkKarneLogInfo info = new CkKarneLogInfo();
            while (dr.Read())
            {
                info.Id = dr.GetMySayi("Id");
                info.KullaniciId = dr.GetMySayi("KullaniciId");
                info.KurumKodu = dr.GetMySayi("KurumKodu");
                info.Say = dr.GetMySayi("Say");
                info.Sinif = dr.GetMySayi("Sinif");
                info.Brans = dr.GetMySayi("Brans");
                info.Aciklama = dr.GetMyMetin("Aciklama");
            }
            dr.Close();

            return info;
        }
        public CkKarneLogInfo KayitBilgiGetir(int id)
        {
            string cmdText = "select * from ckkarnelog where Id=?Id";
            MySqlParameter param = new MySqlParameter("?Id", MySqlDbType.Int32) { Value = id };
            MySqlDataReader dr = helper.ExecuteReader(cmdText, param);
            CkKarneLogInfo info = new CkKarneLogInfo();
            while (dr.Read())
            {
                info.Id = dr.GetMySayi("Id");
                info.KullaniciId = dr.GetMySayi("KullaniciId");
                info.KurumKodu = dr.GetMySayi("KurumKodu");
                info.Say = dr.GetMySayi("Say");
                info.Sinif = dr.GetMySayi("Sinif");
                info.Brans = dr.GetMySayi("Brans");
                info.Aciklama = dr.GetMyMetin("Aciklama");
            }
            dr.Close();

            return info;
        }

        public void KayitSil(int id)
        {
            const string sql = "delete from ckkarnelog where Id=?Id";
            MySqlParameter p = new MySqlParameter("?Id", MySqlDbType.Int32) { Value = id };
            helper.ExecuteNonQuery(sql, p);
        }

        public void KayitEkle(CkKarneLogInfo info)
        {
            const string sql = @"insert into ckkarnelog (KullaniciId,KurumKodu,Tarih,Say,Sinif,Brans,Aciklama) values (?KullaniciId,?KurumKodu,?Tarih,?Say,?Sinif,?Brans,?Aciklama)";
            MySqlParameter[] pars =
            {
                new MySqlParameter("?KullaniciId", MySqlDbType.Int32),
                new MySqlParameter("?KurumKodu", MySqlDbType.Int32),
                new MySqlParameter("?Tarih", MySqlDbType.DateTime),
                new MySqlParameter("?Say", MySqlDbType.Int32),
                new MySqlParameter("?Sinif", MySqlDbType.Int32),
                new MySqlParameter("?Brans", MySqlDbType.Int32),
                new MySqlParameter("?Aciklama", MySqlDbType.VarChar),
            };
            pars[0].Value = info.KullaniciId;
            pars[1].Value = info.KurumKodu;
            pars[2].Value = info.Tarih;
            pars[3].Value = info.Say;
            pars[4].Value = info.Sinif;
            pars[5].Value = info.Brans;
            pars[6].Value = info.Aciklama;
            helper.ExecuteNonQuery(sql, pars);
        }

        public void KayitGuncelle(CkKarneLogInfo info)
        {
            const string sql = @"update ckkarnelog set KullaniciId=?KullaniciId,KurumKodu=?KurumKodu,Tarih=?Tarih,Say=?Say,Aciklama=?Aciklama where Id=?Id";
            MySqlParameter[] pars =
            {
                new MySqlParameter("?KullaniciId", MySqlDbType.Int32),
                new MySqlParameter("?KurumKodu", MySqlDbType.Int32),
                new MySqlParameter("?Tarih", MySqlDbType.DateTime),
                new MySqlParameter("?Say", MySqlDbType.Int32),
                new MySqlParameter("?Sinif", MySqlDbType.Int32),
                new MySqlParameter("?Brans", MySqlDbType.Int32),
                new MySqlParameter("?Aciklama", MySqlDbType.VarChar),
                new MySqlParameter("?Id", MySqlDbType.Int32),
            };
            pars[0].Value = info.KullaniciId;
            pars[1].Value = info.KurumKodu;
            pars[2].Value = info.Tarih;
            pars[3].Value = info.Say;
            pars[4].Value = info.Sinif;
            pars[5].Value = info.Brans;
            pars[6].Value = info.Aciklama;
            pars[7].Value = info.Id;
            helper.ExecuteNonQuery(sql, pars);
        }
    }
