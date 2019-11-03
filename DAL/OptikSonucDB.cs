using System;
using System.Data;
using MySql.Data.MySqlClient;

namespace DAL
{
    public class OptikSonucInfo
    {
        public int Id { get; set; }
        public int BransId { get; set; }
        public int SinavId { get; set; }
        public int OgrenciId { get; set; }
        public int KurumKodu { get; set; }
        public int SoruNo { get; set; }
        public int Puani { get; set; }
        public string Secenek { get; set; }
    }

    public class OptikSonucDB
    {
        readonly HelperDb helper = new HelperDb();

        public DataTable KayitlariGetir()
        {
            const string sql = "select * from optiksonuc order by Id asc";
            return helper.ExecuteDataSet(sql).Tables[0];
        }
       
        public OptikSonucInfo KayitBilgiGetir(string cmdText, params MySqlParameter[] param)
        {
            MySqlDataReader dr = helper.ExecuteReader(cmdText, param);
            OptikSonucInfo info = TaboAlanlar(dr);

            return info;
        }

        private static OptikSonucInfo TaboAlanlar(MySqlDataReader dr)
        {
            OptikSonucInfo info = new OptikSonucInfo();
            while (dr.Read())
            {
                info.Id = dr.GetMySayi("Id");
                info.SinavId = dr.GetMySayi("SinavId");
                info.BransId = dr.GetMySayi("BransId");
                info.OgrenciId = dr.GetMySayi("OgrenciId");
                info.KurumKodu = dr.GetMySayi("KurumKodu");
                info.SoruNo = dr.GetMySayi("SoruNo");
                info.Puani = dr.GetMySayi("Puani");
                info.Secenek = dr.GetMyMetin("Secenek");
            }
            dr.Close();
            return info;
        }

        public OptikSonucInfo KayitBilgiGetir(int id)
        {
            string cmdText = "select * from optiksonuc where Id=?Id";
            MySqlParameter param = new MySqlParameter("?Id", MySqlDbType.Int32) { Value = id };
            MySqlDataReader dr = helper.ExecuteReader(cmdText, param);
            OptikSonucInfo info = TaboAlanlar(dr);

            return info;
        }
        public OptikSonucInfo KayitBilgiGetir(int sinavId,int bransId,int ogrId,int soruNo)
        {
            string cmdText = "select * from optiksonuc where SinavId=?SinavId and BransId=?BransId and OgrenciId=?OgrenciId and SoruNo=?SoruNo";
            MySqlParameter[] param =
            {
                new MySqlParameter("?SinavId", MySqlDbType.Int32),
                new MySqlParameter("?BransId", MySqlDbType.Int32),
                new MySqlParameter("?OgrenciId", MySqlDbType.Int32),
                new MySqlParameter("?SoruNo", MySqlDbType.Int32)
            };
            param[0].Value = sinavId;
            param[1].Value = bransId;
            param[2].Value = ogrId;
            param[3].Value = soruNo;

            MySqlDataReader dr = helper.ExecuteReader(cmdText, param);
            OptikSonucInfo info = TaboAlanlar(dr);

            return info;
        }

        public void KayitSil(int id)
        {
            const string sql = "delete from optiksonuc where Id=?Id";
            MySqlParameter p = new MySqlParameter("?Id", MySqlDbType.Int32) { Value = id };
            helper.ExecuteNonQuery(sql, p);
        }
        public void KayitSil(int sinavId,bool tr)
        {
            const string sql = "delete from optiksonuc where SinavId=?SinavId";
            MySqlParameter p = new MySqlParameter("?SinavId", MySqlDbType.Int32) { Value = sinavId };
            helper.ExecuteNonQuery(sql, p);
        }

        public void KayitEkle(OptikSonucInfo info)
        {
            const string sql = @"insert into optiksonuc (SinavId,OgrenciId,SoruNo,Secenek,BransId,KurumKodu) values (?SinavId,?OgrenciId,?SoruNo,?Secenek,?BransId,?KurumKodu)";
            MySqlParameter[] pars =
            {
                new MySqlParameter("?SinavId", MySqlDbType.Int32),
                new MySqlParameter("?OgrenciId", MySqlDbType.Int32),
                new MySqlParameter("?KurumKodu", MySqlDbType.Int32),
                new MySqlParameter("?SoruNo", MySqlDbType.Int32),
                new MySqlParameter("?Secenek", MySqlDbType.String),
                new MySqlParameter("?BransId", MySqlDbType.String)
            };
            pars[0].Value = info.SinavId;
            pars[1].Value = info.OgrenciId;
            pars[2].Value = info.KurumKodu;
            pars[3].Value = info.SoruNo;
            pars[4].Value = info.Secenek;
            pars[5].Value = info.BransId;
            helper.ExecuteNonQuery(sql, pars);
        }

        public void KayitGuncelle(OptikSonucInfo info)
        {
            const string sql = @"update optiksonuc set SinavId=?SinavId,OgrenciId=?OgrenciId,SoruNo=?SoruNo,Secenek=?Secenek,BransId=?BransId where Id=?Id";
            MySqlParameter[] pars =
            {
                new MySqlParameter("?SinavId", MySqlDbType.Int32),
                new MySqlParameter("?OgrenciId", MySqlDbType.Int32),
                new MySqlParameter("?SoruNo", MySqlDbType.Int32),
                new MySqlParameter("?Secenek", MySqlDbType.String),
                new MySqlParameter("?BransId", MySqlDbType.String),
                new MySqlParameter("?Id", MySqlDbType.Int32)
            };
            pars[0].Value = info.SinavId;
            pars[1].Value = info.OgrenciId;
            pars[2].Value = info.SoruNo;
            pars[3].Value = info.Secenek;
            pars[4].Value = info.BransId;
            pars[5].Value = info.Id;
            helper.ExecuteNonQuery(sql, pars);
        }
        public void KayitGuncelle(string secenek,int puani,int id)
        {
            const string sql = @"update optiksonuc set Secenek=?Secenek,Puani=?Puani where Id=?Id";
            MySqlParameter[] pars =
            {
                new MySqlParameter("?Secenek", MySqlDbType.String),
                new MySqlParameter("?Puani", MySqlDbType.Int32),
                new MySqlParameter("?Id", MySqlDbType.Int32)
            };
            pars[0].Value = secenek;
            pars[1].Value = puani;
            pars[2].Value = id;
            helper.ExecuteNonQuery(sql, pars);
        }
        public int OptikSinavNotu(int sinavId,int bransId,int ogrenciId)
        {
            try
            {
                const string sql = "SELECT SUM(optiksonuc.Puani) from optiksonuc where optiksonuc.SinavId=?SinavId and optiksonuc.OgrenciId=?OgrenciId and optiksonuc.BransId=?BransId";
                MySqlParameter[] pars =
                {
                new MySqlParameter("?SinavId", MySqlDbType.Int32),
                new MySqlParameter("?OgrenciId", MySqlDbType.Int32),
                new MySqlParameter("?BransId", MySqlDbType.Int32)
                };
                pars[0].Value = sinavId;
                pars[1].Value = ogrenciId;
                pars[2].Value = bransId;
                return Convert.ToInt32(helper.ExecuteScalar(sql, pars));
            }
            catch (Exception)
            {
                return 0;
            }
        }
        public bool KayitKontrol(int sinavId)
        {
            string cmdText = "select count(Id) from optiksonuc where SinavId=?SinavId";
            MySqlParameter[] pars =
            {
                new MySqlParameter("?SinavId", MySqlDbType.Int32)
            };
            pars[0].Value = sinavId;
            bool sonuc = Convert.ToInt32(helper.ExecuteScalar(cmdText, pars)) > 0;
            return sonuc;
        }
        /// <summary>
        /// Toplam sayýyý hesaplayan metod
        /// </summary>
        /// <param name="sinavId"></param>
        /// <param name="kurumKodu"></param>
        /// <param name="soruNo"></param>
        /// <param name="bos">Boþ olmayan cevaplarý da saymak için 1 ,boþ olan cevaplarý da saymak için 0</param>
        /// <param name="dogru">Doðrularý saymak için 1, Yanlýþ cevaplarý saymak için 0</param>
        /// <returns></returns>
        public int Hesapla(int sinavId, int kurumKodu, int soruNo, int bos, int dogru)
        {
            string sql = "select Count(Id) from optiksonuc where SoruNo=?SoruNo and SinavId=?SinavId and KurumKodu=?KurumKodu";
            if (bos == 1)
                sql += " and Secenek<>''";
            else
                sql += " and Secenek=''";
            if (dogru == 0)
                sql += " and Puani=0";
            else
                sql += " and Puani<>0";
            MySqlParameter[] pars =
            {
                new MySqlParameter("?SinavId", MySqlDbType.Int32),
                new MySqlParameter("?KurumKodu", MySqlDbType.Int32),
                new MySqlParameter("?SoruNo", MySqlDbType.Int32)
            };
            pars[0].Value = sinavId;
            pars[1].Value = kurumKodu;
            pars[2].Value = soruNo;
            return Convert.ToInt32(helper.ExecuteScalar(sql, pars));
        }
    }
}