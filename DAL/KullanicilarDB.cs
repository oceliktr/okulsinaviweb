using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using MySql.Data.MySqlClient;

namespace DAL
{
    public class KullanicilarInfo
    {
        public int Id { get; set; }
        public string Sifre { get; set; }
        public string AdiSoyadi { get; set; }
        public string KurumKodu { get; set; }
        public string TcKimlik { get; set; }
        public string Email { get; set; }
        public string CepTlf { get; set; }
        public int IlceId { get; set; }
        public int Bransi { get; set; }
        public string Yetki { get; set; }
        public string Grup { get; set; }
        public DateTime OncekiGiris { get; set; }
        public DateTime SonGiris { get; set; }
        public int GirisSayisi { get; set; }
        public KullanicilarInfo()
        {

        }
        public KullanicilarInfo(int id, int bransi, string grup, string yetki)
        {
            Id = id;
            Bransi = bransi;
            Grup = grup;
            Yetki = yetki;
        }
    }
    public class KullanicilarDb
    {
        readonly HelperDb _helper = new HelperDb();

        public DataTable OgretmenleriGetir(int brans)
        {
            const string sql = "select * from kullanicilar where Yetki like '%Ogretmen|%' and Bransi=?Bransi order by Bransi,Grup asc";
            MySqlParameter p = new MySqlParameter("?Bransi", MySqlDbType.Int32)
            {
                Value = brans
            }; 
            return _helper.ExecuteDataSet(sql,p).Tables[0];
        }
        public DataTable OgretmenleriGetir()
        {
            const string sql = "select * from kullanicilar where Yetki like '%Ogretmen|%' order by Bransi asc";
            return _helper.ExecuteDataSet(sql).Tables[0];
        }
        public DataTable KayitlariGetir(int ilceId, string kurumKodu, int brans)
        {
            string sql = "";

            if (ilceId != 0 && kurumKodu == "" && brans == 0)
                sql = "select * from kullanicilar where IlceId=?IlceId order by Id asc";
            if (ilceId != 0 && kurumKodu != "" && brans == 0)
                sql = "select * from kullanicilar where IlceId=?IlceId and KurumKodu=?KurumKodu order by Id asc";
            if (ilceId != 0 && kurumKodu == "" && brans != 0)
                sql = "select * from kullanicilar where IlceId=?IlceId and Bransi=?Bransi order by Id asc";
            if (ilceId != 0 && kurumKodu != "" && brans != 0)
                sql = "select * from kullanicilar where IlceId=?IlceId and Bransi=?Bransi and KurumKodu=?KurumKodu order by Id asc";
            if (ilceId == 0 && kurumKodu == "" && brans != 0)
                sql = "select * from kullanicilar where Bransi=?Bransi order by Id asc";


            MySqlParameter[] p = 
            {
               new MySqlParameter("?IlceId", MySqlDbType.Int32),
               new MySqlParameter("?KurumKodu", MySqlDbType.String),
               new MySqlParameter("?Bransi", MySqlDbType.Int32)
            };

            p[0].Value = ilceId;
            p[1].Value = kurumKodu;
            p[2].Value = brans;
            return _helper.ExecuteDataSet(sql, p).Tables[0];
        }
        public List<KullanicilarInfo> KayitlariDiziyeGetir(int bransi, string yetki)
        {
            string sql = "select * from kullanicilar where Bransi=?Bransi and Yetki like Concat('%',?Yetki,'%')";
            MySqlParameter[] p =
            {
                new MySqlParameter("?Bransi", MySqlDbType.Int32),
                new MySqlParameter("?Yetki",MySqlDbType.String) 
            };
            p[0].Value = bransi;
            p[1].Value = yetki;

            DataTable veriler = _helper.ExecuteDataSet(sql, p).Tables[0];

            return (from DataRow row in veriler.Rows select new KullanicilarInfo(Convert.ToInt32(row["Id"]), Convert.ToInt32(row["Bransi"]), row["Grup"].ToString(), row["Yetki"].ToString())).ToList();
        }
        public List<KullanicilarInfo> KayitlariDiziyeGetir(int bransi, string yetki,string grup)
        {
            string sql = "select * from kullanicilar where Bransi=?Bransi and Yetki like Concat('%',?Yetki,'%') and Grup=?Grup";
            MySqlParameter[] p =
            {
                new MySqlParameter("?Bransi", MySqlDbType.Int32),
                new MySqlParameter("?Yetki",MySqlDbType.String),
                new MySqlParameter("?Grup",MySqlDbType.String) 
            };
            p[0].Value = bransi;
            p[1].Value = yetki;
            p[2].Value = grup;

            DataTable veriler = _helper.ExecuteDataSet(sql, p).Tables[0];

            return (from DataRow row in veriler.Rows select new KullanicilarInfo(Convert.ToInt32(row["Id"]), Convert.ToInt32(row["Bransi"]), row["Grup"].ToString(), row["Yetki"].ToString())).ToList();
        }
        public KullanicilarInfo KayitBilgiGetir(string tcKimlik)
        {
            const string sqlText = "select * from kullanicilar where (TcKimlik=?TcKimlik)";
            MySqlParameter p = new MySqlParameter("?TcKimlik", MySqlDbType.String)
            {
                Value = tcKimlik
            };
            MySqlDataReader dr = _helper.ExecuteReader(sqlText, p);
            KullanicilarInfo info = TabloAlanlar(dr);

            return info;
        }
        public KullanicilarInfo KayitBilgiGetir(int ilce, int kurumId, string uyeKurumKodu)
        {
            const string sqlText = "select * from kullanicilar where (IlceId=?IlceId and Id=?Id and KurumKodu=?KurumKodu)";
            MySqlParameter[] p =
            {
                new MySqlParameter("?IlceId", MySqlDbType.Int32),
                new MySqlParameter("?Id", MySqlDbType.Int32),
                new MySqlParameter("?KurumKodu", MySqlDbType.String)
            };
            p[0].Value = ilce;
            p[1].Value = kurumId;
            p[2].Value = uyeKurumKodu;
            MySqlDataReader dr = _helper.ExecuteReader(sqlText, p);
            KullanicilarInfo info = TabloAlanlar(dr);

            return info;
        }
        public KullanicilarInfo KayitBilgiGetir(int id)
        {
            string cmdText = "select * from kullanicilar where Id=?Id";
            MySqlParameter param = new MySqlParameter("?Id", MySqlDbType.Int32) { Value = id };
            MySqlDataReader dr = _helper.ExecuteReader(cmdText, param);
            KullanicilarInfo info = TabloAlanlar(dr);

            return info;
        }
        public KullanicilarInfo KayitBilgiGetir(string tcKimlik, string sifre)
        {
            const string sqlText = "select * from kullanicilar where (TcKimlik=?TcKimlik and Sifre=?Sifre)";
            MySqlParameter[] pars = 
            {
                new MySqlParameter("?TcKimlik", MySqlDbType.String),
                new MySqlParameter("?Sifre", MySqlDbType.String)
            };
            pars[0].Value = tcKimlik;
            pars[1].Value = sifre;
            MySqlDataReader dr = _helper.ExecuteReader(sqlText, pars);
            KullanicilarInfo info = TabloAlanlar(dr);

            return info;
        }
        private static KullanicilarInfo TabloAlanlar(MySqlDataReader dr)
        {
            KullanicilarInfo info = new KullanicilarInfo();
            while (dr.Read())
            {
                info.Id = dr.GetMySayi("Id");
                info.Sifre = dr.GetMyMetin("Sifre");
                info.KurumKodu = dr.GetMyMetin("KurumKodu");
                info.TcKimlik = dr.GetMyMetin("TcKimlik");
                info.AdiSoyadi = dr.GetMyMetin("AdiSoyadi");
                info.Email = dr.GetMyMetin("Email");
                info.CepTlf = dr.GetMyMetin("CepTlf");
                info.IlceId = dr.GetMySayi("IlceId");
                info.Bransi = dr.GetMySayi("Bransi");
                info.Yetki = dr.GetMyMetin("Yetki");
                info.Grup = dr.GetMyMetin("Grup");
                info.OncekiGiris = dr.GetMyTarih("OncekiGiris");
                info.SonGiris = dr.GetMyTarih("SonGiris");
                info.GirisSayisi = dr.GetMySayi("GirisSayisi");
            }
            dr.Close();
            return info;
        }
        public void KayitSil(int id)
        {
            const string sql = "delete from kullanicilar where Id=?Id";
            MySqlParameter p = new MySqlParameter("?Id", MySqlDbType.Int32) { Value = id };
            _helper.ExecuteNonQuery(sql, p);
        }
        public void KayitEkle(KullanicilarInfo info)
        {
            const string sql = @"insert into kullanicilar (TcKimlik,Sifre,KurumKodu,Email,IlceId,Yetki,AdiSoyadi,Bransi) values (?TcKimlik,?Sifre,?KurumKodu,?Email,?IlceId,?Yetki,?AdiSoyadi,?Bransi)";
            MySqlParameter[] pars = 
            {
             new MySqlParameter("?TcKimlik", MySqlDbType.String),
             new MySqlParameter("?Sifre", MySqlDbType.String),
             new MySqlParameter("?KurumKodu", MySqlDbType.String),
             new MySqlParameter("?Email", MySqlDbType.String),
             new MySqlParameter("?IlceId", MySqlDbType.Int32),
             new MySqlParameter("?Yetki", MySqlDbType.String),
             new MySqlParameter("?AdiSoyadi", MySqlDbType.String),
             new MySqlParameter("?Bransi", MySqlDbType.String)
            };
            pars[0].Value = info.TcKimlik;
            pars[1].Value = info.Sifre;
            pars[2].Value = info.KurumKodu;
            pars[3].Value = info.Email;
            pars[4].Value = info.IlceId;
            pars[5].Value = info.Yetki;
            pars[6].Value = info.AdiSoyadi;
            pars[7].Value = info.Bransi;
            _helper.ExecuteNonQuery(sql, pars);
        }
        public void KayitGuncelle(KullanicilarInfo info)
        {
            const string sql = @"update kullanicilar set Sifre=?Sifre,KurumKodu=?KurumKodu,Email=?Email,IlceId=?IlceId,Yetki=?Yetki,AdiSoyadi=?AdiSoyadi,Bransi=?Bransi,TcKimlik=?TcKimlik where Id=?Id";
            MySqlParameter[] pars = 
            {
             new MySqlParameter("?Sifre", MySqlDbType.String),
             new MySqlParameter("?KurumKodu", MySqlDbType.String),
             new MySqlParameter("?Email", MySqlDbType.String),
             new MySqlParameter("?IlceId", MySqlDbType.Int32),
             new MySqlParameter("?Yetki", MySqlDbType.String),
             new MySqlParameter("?AdiSoyadi", MySqlDbType.String),
             new MySqlParameter("?Bransi", MySqlDbType.String),
             new MySqlParameter("?TcKimlik", MySqlDbType.String),
             new MySqlParameter("?Id", MySqlDbType.Int32)
            };
            pars[0].Value = info.Sifre;
            pars[1].Value = info.KurumKodu;
            pars[2].Value = info.Email;
            pars[3].Value = info.IlceId;
            pars[4].Value = info.Yetki;
            pars[5].Value = info.AdiSoyadi;
            pars[6].Value = info.Bransi;
            pars[7].Value = info.TcKimlik;
            pars[8].Value = info.Id;
            _helper.ExecuteNonQuery(sql, pars);

        }
        public void KullaniciBilgiGuncelle(KullanicilarInfo info)
        {
            const string sql = @"update kullanicilar set Sifre=?Sifre,Email=?Email,CepTlf=?CepTlf where Id=?Id";
            MySqlParameter[] pars = 
            {
             new MySqlParameter("?Sifre", MySqlDbType.String),
             new MySqlParameter("?Email", MySqlDbType.String),
             new MySqlParameter("?CepTlf", MySqlDbType.String),
             new MySqlParameter("?Id", MySqlDbType.Int32)
            };
            pars[0].Value = info.Sifre;
            pars[1].Value = info.Email;
            pars[2].Value = info.CepTlf;
            pars[3].Value = info.Id;
            _helper.ExecuteNonQuery(sql, pars);

        }
        public void KayitGuncelle(int girisSayisi, int id, DateTime oncekiGiris)
        {
            string sql = @"update kullanicilar set SonGiris=?SonGiris,OncekiGiris=?OncekiGiris,GirisSayisi=?GirisSayisi where Id=?Id";
            MySqlParameter[] pars =
            {
             new MySqlParameter("?SonGiris", MySqlDbType.DateTime),
             new MySqlParameter("?OncekiGiris", MySqlDbType.DateTime),
             new MySqlParameter("?GirisSayisi", MySqlDbType.Int32),
             new MySqlParameter("?Id", MySqlDbType.Int32),
            };
            pars[0].Value = DateTime.Now;
            pars[1].Value = oncekiGiris;
            pars[2].Value = girisSayisi;
            pars[3].Value = id;
            _helper.ExecuteNonQuery(sql, pars);
        }
        public void KayitGuncelle(int id, string sifre)
        {
            const string sql = @"update kullanicilar set Sifre=?Sifre where Id=?Id";
            MySqlParameter[] pars =
            {
             new MySqlParameter("?Sifre", MySqlDbType.String),
             new MySqlParameter("?Id", MySqlDbType.Int32)
            };
            pars[0].Value = sifre;
            pars[1].Value = id;
            _helper.ExecuteNonQuery(sql, pars);
        }
        public void KayitGrupGuncelle(int id, string grup)
        {
            const string sql = @"update kullanicilar set Grup=?Grup where Id=?Id";
            MySqlParameter[] pars =
            {
             new MySqlParameter("?Grup", MySqlDbType.String),
             new MySqlParameter("?Id", MySqlDbType.Int32)
            };
            pars[0].Value = grup;
            pars[1].Value = id;
            _helper.ExecuteNonQuery(sql, pars);
        }
        public bool KayitKontrol(string tcKimlik, string sifre)
        {
            const string cmdText = "select count(Id) from kullanicilar where TcKimlik=?TcKimlik and Sifre=?Sifre";
            MySqlParameter[] pars = 
            {
                new MySqlParameter("?TcKimlik", MySqlDbType.String),
                new MySqlParameter("?Sifre", MySqlDbType.String)
            };
            pars[0].Value = tcKimlik;
            pars[1].Value = sifre;
            bool sonuc = Convert.ToInt32(_helper.ExecuteScalar(cmdText, pars)) > 0;
            return sonuc;
        }
        public bool KayitKontrol(string tcKimlik, int x)
        {
            string cmdText = "select count(Id) from kullanicilar where TcKimlik=?TcKimlik";
            MySqlParameter[] pars = 
            {
                new MySqlParameter("?TcKimlik", MySqlDbType.String)
            };
            pars[0].Value = tcKimlik;
            bool sonuc = Convert.ToInt32(_helper.ExecuteScalar(cmdText, pars)) > 0;
            return sonuc;
        }
        public bool KayitKontrol(int id)
        {
            string cmdText = "select count(Id) from kullanicilar where Id=?Id";
            MySqlParameter[] pars = 
            {
                new MySqlParameter("?Id", MySqlDbType.Int32)
            };
            pars[0].Value = id;
            bool sonuc = Convert.ToInt32(_helper.ExecuteScalar(cmdText, pars)) > 0;
            return sonuc;
        }
        public void YeniSifreOlustur(int id, string ozelKod)
        {
            const string sql = @"update Kullanicilar set Sifre=?Sifre where Id=?Id";
            MySqlParameter[] pars =
            {
             new MySqlParameter("?Sifre", MySqlDbType.String),
             new MySqlParameter("?Id", MySqlDbType.Int32)
            };
            pars[0].Value = ozelKod;
            pars[1].Value = id;
            _helper.ExecuteNonQuery(sql, pars);
        }
    }
}
