using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using MySql.Data.MySqlClient;

namespace DAL
{
    public class KurumlarInfo
    {
        public int Id { get; set; }
        public string KurumKodu { get; set; }
        public string KurumAdi { get; set; }
        public string Email { get; set; }
        public int IlceId { get; set; }
        public string KurumTuru { get; set; }
        public string Tur { get; set; }
        public string IlceAdi { get; set; }

        public KurumlarInfo()
        {
                
        }

        public KurumlarInfo(string kurumKodu, string kurumAdi, int ilceId, string ilceAdi)
        {
            KurumKodu = kurumKodu;
            KurumAdi = kurumAdi;
            IlceId = ilceId;
            IlceAdi = ilceAdi;
        }
        public KurumlarInfo(string kurumKodu)
        {
            KurumKodu = kurumKodu;
        }

        public KurumlarInfo(string kurumKodu, string kurumveIlceAdi)
        {
            KurumKodu = kurumKodu;
            KurumAdi = kurumveIlceAdi; //il�e ad�yla birle�tirilecek
        }
    }
    public class KurumlarDb
    {
        readonly HelperDb _helper = new HelperDb();

        public DataTable KayitlariGetir()
        {
            const string sql = "select * from kurumlar order by Id asc";
            return _helper.ExecuteDataSet(sql).Tables[0];
        }
        public DataTable KurumTurleri()
        {
            const string sql = "select DISTINCT(Tur) from kurumlar order by Tur asc";
            return _helper.ExecuteDataSet(sql).Tables[0];
        }
        public DataTable OkullariGetir(int ilce)
        {
            string sql = "select * from kurumlar where IlceId=?Ilce order by KurumAdi asc";
            MySqlParameter pars = new MySqlParameter("?Ilce", MySqlDbType.Int32) {Value = ilce};
            return _helper.ExecuteDataSet(sql, pars).Tables[0];
        }
        public DataTable SinavaGirenOkullariGetir(int sinavId)
        {
            string sql = @"SELECT DISTINCT ogrenciler.KurumKodu ,kurumlar.KurumAdi,ilceler.Id,ilceler.IlceAdi, CONCAT(ilceler.IlceAdi,' - ',kurumlar.KurumAdi) as IlceOkul FROM ogrenciler
                            INNER JOIN Kurumlar ON ogrenciler.KurumKodu = kurumlar.KurumKodu
                            INNER JOIN ilceler ON ilceler.Id = kurumlar.IlceId
                            WHERE ogrenciler.SinavId = ?SinavId and kurumlar.KurumKodu IS NOT NULL order by ilceler.IlceAdi,kurumlar.KurumAdi asc";
            MySqlParameter pars = new MySqlParameter("?SinavId", MySqlDbType.Int32) { Value = sinavId };
            return _helper.ExecuteDataSet(sql, pars).Tables[0];
        }
        public DataTable SinavaGirenOkullariGetir(int sinavId,int ilceId)
        {
            string sql = @"SELECT DISTINCT ogrenciler.KurumKodu ,kurumlar.KurumAdi FROM ogrenciler
                            INNER JOIN Kurumlar ON ogrenciler.KurumKodu = kurumlar.KurumKodu and kurumlar.IlceId=?IlceId
                            WHERE ogrenciler.SinavId =?SinavId and kurumlar.KurumKodu IS NOT NULL order by kurumlar.KurumAdi asc";
            MySqlParameter[] p =
            {
                new MySqlParameter("?SinavId", MySqlDbType.Int32),
                new MySqlParameter("?IlceId", MySqlDbType.Int32)
            };
            p[0].Value = sinavId;
            p[1].Value = ilceId;
            return _helper.ExecuteDataSet(sql, p).Tables[0];
        }
        public List<KurumlarInfo> SinavaGirenOkullariDiziyeGetir(int sinavId, int ilceId)
        {
            string sql = @"SELECT DISTINCT ogrenciler.KurumKodu ,kurumlar.KurumAdi,ilceler.IlceAdi, kurumlar.IlceId, CONCAT(ilceler.IlceAdi,' - ',kurumlar.KurumAdi) as IlceOkul FROM ogrenciler
                            INNER JOIN Kurumlar ON ogrenciler.KurumKodu = kurumlar.KurumKodu and kurumlar.IlceId=?IlceId
                            INNER JOIN ilceler ON ilceler.Id = kurumlar.IlceId
                            WHERE ogrenciler.SinavId = ?SinavId and kurumlar.KurumKodu IS NOT NULL order by ilceler.IlceAdi,kurumlar.KurumAdi asc";
            MySqlParameter[] p =
            {
                new MySqlParameter("?SinavId", MySqlDbType.Int32),
                new MySqlParameter("?IlceId", MySqlDbType.Int32)
            };
            p[0].Value = sinavId;
            p[1].Value = ilceId;
            DataTable veriler = _helper.ExecuteDataSet(sql, p).Tables[0];
            return (from DataRow row in veriler.Rows select new KurumlarInfo(row["KurumKodu"].ToString(),row["KurumAdi"].ToString(),Convert.ToInt32(row["IlceId"]),row["IlceAdi"].ToString())).ToList();
        }
        public List<KurumlarInfo> SinavaGirenOkullariDiziyeGetir(int sinavId)
        {
            string sql = @"SELECT DISTINCT ogrenciler.KurumKodu ,kurumlar.KurumAdi,ilceler.IlceAdi, kurumlar.IlceId, CONCAT(ilceler.IlceAdi,' - ',kurumlar.KurumAdi) as IlceOkul FROM ogrenciler
                            INNER JOIN Kurumlar ON ogrenciler.KurumKodu = kurumlar.KurumKodu
                            INNER JOIN ilceler ON ilceler.Id = kurumlar.IlceId
                            WHERE ogrenciler.SinavId = ?SinavId and kurumlar.KurumKodu IS NOT NULL order by ilceler.IlceAdi,kurumlar.KurumAdi asc";
            MySqlParameter pars = new MySqlParameter("?SinavId", MySqlDbType.Int32) { Value = sinavId };
            DataTable veriler = _helper.ExecuteDataSet(sql, pars).Tables[0];
            return (from DataRow row in veriler.Rows select new KurumlarInfo(row["KurumKodu"].ToString(), row["KurumAdi"].ToString(), Convert.ToInt32(row["IlceId"]), row["IlceAdi"].ToString())).ToList();
        }
        public List<KurumlarInfo> IlceveOkuluDiziyeGetir()
        {
            string sql = @"select okul.KurumKodu as KurumKodu,ilce.IlceAdi as IlceAdi,okul.KurumAdi as KurumAdi,CONCAT(ilce.IlceAdi, ' - ', okul.KurumAdi) AS IlceveKurumAdi from ilceler as ilce
                        left outer join kurumlar as okul on okul.IlceId = ilce.Id order by ilce.Id asc, okul.KurumAdi asc, okul.KurumAdi asc ";

            DataTable veriler = _helper.ExecuteDataSet(sql).Tables[0];

            return (from DataRow row in veriler.Rows
                select new KurumlarInfo(row["KurumKodu"].ToString(), row["IlceveKurumAdi"].ToString())).ToList();
        }

        public DataTable IlceveOkuluBirlestirGetir(string tur)
        {
            string sql = @"select okul.KurumKodu as KurumKodu,ilce.IlceAdi as IlceAdi,okul.KurumAdi as KurumAdi,CONCAT(ilce.IlceAdi, ' - ', okul.KurumAdi) AS IlceveKurumAdi from ilceler as ilce
                        left outer join kurumlar as okul on okul.IlceId = ilce.Id where okul.Tur=?Tur order by ilce.Id asc, okul.KurumAdi asc, okul.KurumAdi asc ";
            MySqlParameter p = new MySqlParameter("?Tur", MySqlDbType.String) { Value = tur };

            return _helper.ExecuteDataSet(sql,p).Tables[0];
        }
        public KurumlarInfo KayitBilgiGetir(string kurumKodu)
        {
            const string sqlText = @"select kurumlar.*,ilceler.IlceAdi from kurumlar
                                    INNER JOIN ilceler ON ilceler.Id = kurumlar.IlceId where KurumKodu=?KurumKodu";
            MySqlParameter p = new MySqlParameter("?KurumKodu", MySqlDbType.String){Value = kurumKodu};
            MySqlDataReader dr = _helper.ExecuteReader(sqlText, p);
            KurumlarInfo info = TabloAlanlar(dr);

            return info;
        }
        public KurumlarInfo KayitBilgiGetir(int ilce, int kurumId, string uyeKurumKodu)
        {
            const string sqlText = "select * from kurumlar where (IlceId=?IlceId and Id=?Id and KurumKodu=?KurumKodu)";
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
            KurumlarInfo info = TabloAlanlar(dr);

            return info;
        }
        public KurumlarInfo KayitBilgiGetir(string cmdText, params MySqlParameter[] param)
        {
            MySqlDataReader dr = _helper.ExecuteReader(cmdText, param);
            KurumlarInfo info = TabloAlanlar(dr);

            return info;
        }
        public KurumlarInfo KayitBilgiGetir(int id)
        {
            string cmdText = "select * from kurumlar where Id=?Id";
            MySqlParameter param = new MySqlParameter("?Id", MySqlDbType.Int32) { Value = id };
            MySqlDataReader dr = _helper.ExecuteReader(cmdText, param);
            KurumlarInfo info = TabloAlanlar(dr);

            return info;
        }
        public string KurumAdi(int kurumKodu)
        {
            string cmdText = "select * from kurumlar where KurumKodu=?KurumKodu";
            MySqlParameter param = new MySqlParameter("?KurumKodu", MySqlDbType.VarChar) { Value = kurumKodu };
            MySqlDataReader dr = _helper.ExecuteReader(cmdText, param);
            KurumlarInfo info = new KurumlarInfo();
            while (dr.Read())
            {
                info.KurumAdi = dr.GetMyMetin("KurumAdi");
            }
            dr.Close();
            
            return info.KurumAdi;
        }
        private static KurumlarInfo TabloAlanlar(MySqlDataReader dr)
        {
            KurumlarInfo info = new KurumlarInfo();
            while (dr.Read())
            {
                info.Id = dr.GetMySayi("Id");
                info.KurumKodu = dr.GetMyMetin("KurumKodu");
                info.Email = dr.GetMyMetin("Email");
                info.KurumAdi = dr.GetMyMetin("KurumAdi");
                info.IlceId = dr.GetMySayi("IlceId");
                info.KurumTuru = dr.GetMyMetin("KurumTuru");
                info.Tur = dr.GetMyMetin("Tur");
                info.IlceAdi = dr.GetMyMetin("IlceAdi");
            }
            dr.Close();
            return info;
        }
        public void KayitSil(int id)
        {
            const string sql = "delete from kurumlar where Id=?Id";
            MySqlParameter p = new MySqlParameter("?Id", MySqlDbType.Int32) { Value = id };
            _helper.ExecuteNonQuery(sql, p);
        }
        public void KayitEkle(KurumlarInfo info)
        {
            const string sql = @"insert into kurumlar (KurumKodu,KurumAdi,Email,IlceId,KurumTuru,Tur) values (?KurumKodu,?KurumAdi,?Email,?IlceId,?KurumTuru,?Tur)";
            MySqlParameter[] pars = 
            {
             new MySqlParameter("?KurumKodu", MySqlDbType.String),
             new MySqlParameter("?KurumAdi", MySqlDbType.String),
             new MySqlParameter("?Email", MySqlDbType.String),
             new MySqlParameter("?IlceId", MySqlDbType.Int32),
             new MySqlParameter("?KurumTuru", MySqlDbType.String),
             new MySqlParameter("?Tur", MySqlDbType.String)
            };
            pars[0].Value = info.KurumKodu;
            pars[1].Value = info.KurumAdi;
            pars[2].Value = info.Email;
            pars[3].Value = info.IlceId;
            pars[4].Value = info.KurumTuru;
            pars[5].Value = info.Tur;
            _helper.ExecuteNonQuery(sql, pars);
        }
        public void KayitGuncelle(KurumlarInfo info)
        {
            const string sql = @"update kurumlar set KurumKodu=?KurumKodu,KurumAdi=?KurumAdi,Email=?Email,IlceId=?IlceId,KurumTuru=?KurumTuru,Tur=?Tur where Id=?Id";
            MySqlParameter[] pars = 
            {
             new MySqlParameter("?KurumKodu", MySqlDbType.String),
             new MySqlParameter("?KurumAdi", MySqlDbType.String),
             new MySqlParameter("?Email", MySqlDbType.String),
             new MySqlParameter("?IlceId", MySqlDbType.Int32),
             new MySqlParameter("?KurumTuru", MySqlDbType.String),
             new MySqlParameter("?Tur", MySqlDbType.String),
             new MySqlParameter("?Id", MySqlDbType.Int32),
            };
            pars[0].Value = info.KurumKodu;
            pars[1].Value = info.KurumAdi;
            pars[2].Value = info.Email;
            pars[3].Value = info.IlceId;
            pars[4].Value = info.KurumTuru;
            pars[5].Value = info.Tur;
            pars[6].Value = info.Id;
            _helper.ExecuteNonQuery(sql, pars);
            
        }

        public bool KayitKontrol(string uyeKurumKodu)
        {
            string cmdText = "select count(Id) from kurumlar where KurumKodu=?KurumKodu";
            MySqlParameter[] pars = 
            {
                new MySqlParameter("?KurumKodu", MySqlDbType.String)
            };
            pars[0].Value = uyeKurumKodu;
            bool sonuc = Convert.ToInt32(_helper.ExecuteScalar(cmdText, pars)) > 0;
            return sonuc;
        }
        public bool KayitKontrol(string uyeEmail, bool mail)
        {
            string cmdText = "select count(Id) from kurumlar where Email=?Email";
            MySqlParameter[] pars = 
            {
                new MySqlParameter("?Email", MySqlDbType.String)
            };
            pars[0].Value = uyeEmail;
            bool sonuc = Convert.ToInt32(_helper.ExecuteScalar(cmdText, pars)) > 0;
            return sonuc;
        }
        public bool KayitKontrol(int id)
        {
            string cmdText = "select count(Id) from kurumlar where Id=?Id";
            MySqlParameter[] pars = 
            {
                new MySqlParameter("?Id", MySqlDbType.Int32)
            };
            pars[0].Value = id;
            bool sonuc = Convert.ToInt32(_helper.ExecuteScalar(cmdText, pars)) > 0;
            return sonuc;
        }
    }
}
