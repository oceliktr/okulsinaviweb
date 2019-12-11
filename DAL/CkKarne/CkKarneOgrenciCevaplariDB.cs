using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using MySql.Data.MySqlClient;

namespace DAL.CkKarne
{
    public class CkKarneOgrenciCevaplariInfo
    {
        public int Id { get; set; }
        public int SinavId { get; set; }
        public long OpaqId { get; set; }
        public int KurumKodu { get; set; }
        public int Sinif { get; set; }
        public string Ilce { get; set; }
        public string Sube { get; set; }
        public string KitapcikTuru { get; set; }
        public string KatilimDurumu { get; set; }
        public string Cevaplar { get; set; }

        public CkKarneOgrenciCevaplariInfo()
        {
            
        }

        public CkKarneOgrenciCevaplariInfo(int id, int sinavId, long opaqId, int kurumKodu, int sinif, string ilce, string sube, string kitapcikTuru, string katilimDurumu, string cevaplar)
        {
            Id = id;
            SinavId = sinavId;
            OpaqId = opaqId;
            KurumKodu = kurumKodu;
            Sinif = sinif;
            Ilce = ilce;
            Sube = sube;
            KitapcikTuru = kitapcikTuru;
            KatilimDurumu = katilimDurumu;
            Cevaplar = cevaplar;
        }
    }
    public class CkKarneOgrenciCevaplariDB
    {
        readonly HelperDb helper = new HelperDb();
        public DataTable KayitlariGetir()
        {
            const string sql = "select * from ckogrencicevaplari order by Id asc";
            return helper.ExecuteDataSet(sql).Tables[0];
        }
        public List<CkKarneOgrenciCevaplariInfo> KayitlariDizeGetir(int sinavId)
        {
            string sql = "select * from ckogrencicevaplari where SinavId=?SinavId";
            MySqlParameter[] p =
            {
                new MySqlParameter("?SinavId", MySqlDbType.Int32)
            };
            p[0].Value = sinavId;

            DataTable dt = helper.ExecuteDataSet(sql, p).Tables[0];
            List<CkKarneOgrenciCevaplariInfo> karne = new List<CkKarneOgrenciCevaplariInfo>();
            foreach (DataRow k in dt.Rows)
            {
                karne.Add(new CkKarneOgrenciCevaplariInfo(Convert.ToInt32(k["Id"]), Convert.ToInt32(k["SinavId"]), Convert.ToInt64(k["OpaqId"]), Convert.ToInt32(k["KurumKodu"]), Convert.ToInt32(k["Sinif"]), k["Ilce"].ToString(), k["Sube"].ToString(),
                    k["KitapcikTuru"].ToString(), k["KatilimDurumu"].ToString(), k["Cevaplar"].ToString()));
            }
            return karne;
        }
        public List<CkKarneOgrenciCevaplariInfo> KayitlariDizeGetir(int sinavId, int kurumKodu)
        {
            string sql = "select * from ckogrencicevaplari where SinavId=?SinavId and KurumKodu=?KurumKodu";
            MySqlParameter[] p =
            {
                new MySqlParameter("?SinavId", MySqlDbType.Int32),
                new MySqlParameter("?KurumKodu", MySqlDbType.Int32)
            };
            p[0].Value = sinavId;
            p[1].Value = kurumKodu;

            DataTable dt = helper.ExecuteDataSet(sql, p).Tables[0];
            List<CkKarneOgrenciCevaplariInfo> karne = new List<CkKarneOgrenciCevaplariInfo>();
            foreach (DataRow k in dt.Rows)
            {
                karne.Add(new CkKarneOgrenciCevaplariInfo(Convert.ToInt32(k["Id"]), Convert.ToInt32(k["SinavId"]), Convert.ToInt64(k["OpaqId"]), Convert.ToInt32(k["KurumKodu"]), Convert.ToInt32(k["Sinif"]), k["Ilce"].ToString(), k["Sube"].ToString(),
                    k["KitapcikTuru"].ToString(), k["KatilimDurumu"].ToString(), k["Cevaplar"].ToString()));
            }
            return karne;
        }
        public CkKarneOgrenciCevaplariInfo KayitBilgiGetir(string cmdText, params MySqlParameter[] param)
        {
            MySqlDataReader dr = helper.ExecuteReader(cmdText, param);
            CkKarneOgrenciCevaplariInfo info = new CkKarneOgrenciCevaplariInfo();
            while (dr.Read())
            {
                info.Id = dr.GetMySayi("Id");
                info.SinavId = dr.GetMySayi("SinavId");
                info.OpaqId = dr.GetMyLong("OpaqId");
                info.KurumKodu = dr.GetMySayi("KurumKodu");
                info.Sinif = dr.GetMySayi("Sinif");
                info.Ilce = dr.GetMyMetin("Ilce");
                info.Sube = dr.GetMyMetin("Sube");
                info.KitapcikTuru = dr.GetMyMetin("KitapcikTuru");
                info.KatilimDurumu = dr.GetMyMetin("KatilimDurumu");
                info.Cevaplar = dr.GetMyMetin("Cevaplar");
            }
            dr.Close();

            return info;
        }

        public CkKarneOgrenciCevaplariInfo KayitBilgiGetir(int id)
        {
            string cmdText = "select * from ckogrencicevaplari where Id=?Id";
            MySqlParameter param = new MySqlParameter("?Id", MySqlDbType.Int32) { Value = id };
            MySqlDataReader dr = helper.ExecuteReader(cmdText, param);
            CkKarneOgrenciCevaplariInfo info = new CkKarneOgrenciCevaplariInfo();
            while (dr.Read())
            {
                info.Id = dr.GetMySayi("Id");
                info.SinavId = dr.GetMySayi("SinavId");
                info.OpaqId = dr.GetMyLong("OpaqId");
                info.KurumKodu = dr.GetMySayi("KurumKodu");
                info.Sinif = dr.GetMySayi("Sinif");
                info.Ilce = dr.GetMyMetin("Ilce");
                info.Sube = dr.GetMyMetin("Sube");
                info.KitapcikTuru = dr.GetMyMetin("KitapcikTuru");
                info.KatilimDurumu = dr.GetMyMetin("KatilimDurumu");
                info.Cevaplar = dr.GetMyMetin("Cevaplar");
            }
            dr.Close();

            return info;
        }

        public void KayitSil(int id)
        {
            const string sql = "delete from ckogrencicevaplari where Id=?Id";
            MySqlParameter p = new MySqlParameter("?Id", MySqlDbType.Int32) { Value = id };
            helper.ExecuteNonQuery(sql, p);
        }

        public void KayitEkle(CkKarneOgrenciCevaplariInfo info)
        {
            const string sql = @"insert into ckogrencicevaplari (OpaqId,KurumKodu,Sinif,Ilce,Sube,KitapcikTuru,KatilimDurumu,Cevaplar,SinavId) values (?OpaqId,?KurumKodu,?Sinif,?Ilce,?Sube,?KitapcikTuru,?KatilimDurumu,?Cevaplar,?SinavId)";
            MySqlParameter[] pars =
            {
             new MySqlParameter("?OpaqId", MySqlDbType.Int64),
             new MySqlParameter("?KurumKodu", MySqlDbType.Int32),
             new MySqlParameter("?Sinif", MySqlDbType.Int32),
             new MySqlParameter("?Ilce", MySqlDbType.String),
             new MySqlParameter("?Sube", MySqlDbType.String),
             new MySqlParameter("?KitapcikTuru", MySqlDbType.String),
             new MySqlParameter("?KatilimDurumu", MySqlDbType.String),
             new MySqlParameter("?Cevaplar", MySqlDbType.String),
             new MySqlParameter("?SinavId", MySqlDbType.Int32),
            };
            pars[0].Value = info.OpaqId;
            pars[1].Value = info.KurumKodu;
            pars[2].Value = info.Sinif;
            pars[3].Value = info.Ilce;
            pars[4].Value = info.Sube;
            pars[5].Value = info.KitapcikTuru;
            pars[6].Value = info.KatilimDurumu;
            pars[7].Value = info.Cevaplar;
            pars[8].Value = info.SinavId;
            helper.ExecuteNonQuery(sql, pars);
        }

        public void KayitGuncelle(CkKarneOgrenciCevaplariInfo info)
        {
            const string sql = @"update ckogrencicevaplari set OpaqId=?OpaqId,KurumKodu=?KurumKodu,Sinif=?Sinif,Ilce=?Ilce,Sube=?Sube,KitapcikTuru=?KitapcikTuru,KatilimDurumu=?KatilimDurumu,Cevaplar=?Cevaplar,SinavId=?SinavId where Id=?Id";
            MySqlParameter[] pars =
            {
                new MySqlParameter("?OpaqId", MySqlDbType.Int64),
                 new MySqlParameter("?KurumKodu", MySqlDbType.Int32),
                 new MySqlParameter("?Sinif", MySqlDbType.Int32),
                 new MySqlParameter("?Ilce", MySqlDbType.String),
                 new MySqlParameter("?Sube", MySqlDbType.String),
                 new MySqlParameter("?KitapcikTuru", MySqlDbType.String),
                 new MySqlParameter("?KatilimDurumu", MySqlDbType.String),
                 new MySqlParameter("?Cevaplar", MySqlDbType.String),
                 new MySqlParameter("?SinavId", MySqlDbType.Int32),
                 new MySqlParameter("?Id", MySqlDbType.Int32)
            };
            pars[0].Value = info.OpaqId;
            pars[1].Value = info.KurumKodu;
            pars[2].Value = info.Sinif;
            pars[3].Value = info.Ilce;
            pars[4].Value = info.Sube;
            pars[5].Value = info.KitapcikTuru;
            pars[6].Value = info.KatilimDurumu;
            pars[7].Value = info.Cevaplar;
            pars[8].Value = info.SinavId;
            pars[9].Value = info.Id;
            helper.ExecuteNonQuery(sql, pars);
        }
    }
}
