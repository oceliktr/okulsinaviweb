using System;
using System.Collections.Generic;
using System.Data;
using MySql.Data.MySqlClient;

namespace DAL
{
    public class KarneRaporInfo
    {
        public int SinavId { get; set; }
        public int IlceId { get; set; }
        public int KurumKodu { get; set; }
        public int BransId { get; set; }
        public int Sinif { get; set; }
        public string Sube { get; set; }
        public string Grup { get; set; }
        public int Sayac { get; set; }

        public KarneRaporInfo()
        {
            
        }
        public KarneRaporInfo(int sinif, int ilce, int bransId, int kurumKodu, string sube, string grup)
        {
            Sinif = sinif;
            IlceId = ilce;
            BransId = bransId;
            KurumKodu = kurumKodu;
            Sube = sube;
            Grup = grup;
        }
        public KarneRaporInfo(int sinavId, int ilceId, int kurumKodu, int bransId, int sinif, string sube, string grup, int sayac)
        {
            SinavId = sinavId;
            IlceId = ilceId;
            KurumKodu = kurumKodu;
            BransId = bransId;
            Sinif = sinif;
            Sube = sube;
            Grup = grup;
            Sayac = sayac;
        }
    }

    public class KarneRaporDB
    {
        readonly HelperDb helper = new HelperDb();

        public List<KarneRaporInfo> KayitlariDizeGetir(int sinavId)
        {
            string sql = "select * from karnerapor where SinavId=?SinavId";
            MySqlParameter p = new MySqlParameter("?SinavId",MySqlDbType.Int32){Value = sinavId};
          DataTable dt=  helper.ExecuteDataSet(sql, p).Tables[0];
            List<KarneRaporInfo> karne = new List<KarneRaporInfo>();
            foreach (DataRow k in dt.Rows)
            {
               karne.Add(new KarneRaporInfo(Convert.ToInt32(k["Sinif"]), Convert.ToInt32(k["IlceId"]), Convert.ToInt32(k["BransId"]), Convert.ToInt32(k["KurumKodu"]),k["Sube"].ToString(),k["Grup"].ToString())); 
            }
            return karne;
        }
        private static KarneRaporInfo TabloAlanlar(MySqlDataReader dr)
        {
            KarneRaporInfo info = new KarneRaporInfo();
            while (dr.Read())
            {
                info.SinavId = dr.GetMySayi("SinavId");
                info.KurumKodu = dr.GetMySayi("KurumKodu");
                info.BransId = dr.GetMySayi("BransId");
                info.Sinif = dr.GetMySayi("Sinif");
                info.Sube = dr.GetMyMetin("Sube");
                info.Sayac = dr.GetMySayi("Sayac");
                info.IlceId = dr.GetMySayi("IlceId");
                info.Grup = dr.GetMyMetin("Grup");
            }
            dr.Close();

            return info;
        }
        
        public KarneRaporInfo KayitBilgiGetir(int sinavId, int ilceId, int bransId, int kurumKodu, int sinif, string sube,string grup)
        {
            string cmdText = "select * from karnerapor where SinavId=?SinavId and IlceId=?IlceId and BransId=?BransId and KurumKodu=?KurumKodu and Sinif=?Sinif and Sube=?Sube and Grup=?Grup";
            MySqlParameter[] pars =
            {
                 new MySqlParameter("?SinavId", MySqlDbType.Int32),
                 new MySqlParameter("?IlceId", MySqlDbType.Int32),
                 new MySqlParameter("?BransId", MySqlDbType.Int32),
                 new MySqlParameter("?KurumKodu", MySqlDbType.Int32),
                 new MySqlParameter("?Sinif", MySqlDbType.Int32),
                 new MySqlParameter("?Sube", MySqlDbType.String),
                 new MySqlParameter("?Grup", MySqlDbType.String)
            };
            pars[0].Value = sinavId;
            pars[1].Value = ilceId;
            pars[2].Value = bransId;
            pars[3].Value = kurumKodu;
            pars[4].Value = sinif;
            pars[5].Value = sube;
            pars[6].Value = grup;
            MySqlDataReader dr = helper.ExecuteReader(cmdText, pars);
            return TabloAlanlar(dr);
        }
        
        public void KayitSil(int sinavId, int ilceId, int bransId, int kurumKodu, int sinif, string sube, string grup)
        {
            string cmdText = "delete from karnerapor where SinavId=?SinavId and BransId=?BransId and IlceId=?IlceId and KurumKodu=?KurumKodu and Sinif=?Sinif and Sube=?Sube and Grup=?Grup";
            MySqlParameter[] pars =
            {
                new MySqlParameter("?SinavId", MySqlDbType.Int32),
                new MySqlParameter("?IlceId", MySqlDbType.Int32),
                new MySqlParameter("?BransId", MySqlDbType.Int32),
                new MySqlParameter("?KurumKodu", MySqlDbType.Int32),
                new MySqlParameter("?Sinif", MySqlDbType.Int32),
                new MySqlParameter("?Sube", MySqlDbType.String),
                new MySqlParameter("?Grup", MySqlDbType.String)
            };
            pars[0].Value = sinavId;
            pars[1].Value = ilceId;
            pars[2].Value = bransId;
            pars[3].Value = kurumKodu;
            pars[4].Value = sinif;
            pars[5].Value = sube;
            pars[6].Value = grup;

            helper.ExecuteNonQuery(cmdText, pars);
        }
        public void KayitEkle(int sinavId, int ilceId, int bransId, int kurumKodu, int sinif, string sube, string grup, int sayac)
        {
            const string sql = @"insert into karnerapor (SinavId,IlceId,KurumKodu,BransId,Sinif,Sube,Grup,Sayac) values (?SinavId,?IlceId,?KurumKodu,?BransId,?Sinif,?Sube,?Grup,?Sayac)";

            MySqlParameter[] pars =
            {
                new MySqlParameter("?SinavId", MySqlDbType.Int32),
                new MySqlParameter("?IlceId", MySqlDbType.Int32),
                new MySqlParameter("?BransId", MySqlDbType.Int32),
                new MySqlParameter("?KurumKodu", MySqlDbType.Int32),
                new MySqlParameter("?Sinif", MySqlDbType.Int32),
                new MySqlParameter("?Sube", MySqlDbType.String),
                new MySqlParameter("?Sayac", MySqlDbType.Int32),
                new MySqlParameter("?Grup", MySqlDbType.String)
            };
            pars[0].Value = sinavId;
            pars[1].Value = ilceId;
            pars[2].Value = bransId;
            pars[3].Value = kurumKodu;
            pars[4].Value = sinif;
            pars[5].Value = sube;
            pars[6].Value = sayac;
            pars[7].Value = grup;
            helper.ExecuteNonQuery(sql, pars);
        }
        public void KayitGuncelle(int sinavId, int ilceId, int bransId, int kurumKodu, int sinif, string sube, string grup,int sayac)
        {
            const string sql = @"update karnerapor set Sayac=?Sayac where SinavId=?SinavId and IlceId=?IlceId and BransId=?BransId and KurumKodu=?KurumKodu and Sinif=?Sinif and Sube=?Sube and Grup=?Grup";
            MySqlParameter[] pars =
            {
                 new MySqlParameter("?SinavId", MySqlDbType.Int32),
                 new MySqlParameter("?IlceId", MySqlDbType.Int32),
                 new MySqlParameter("?BransId", MySqlDbType.Int32),
                 new MySqlParameter("?KurumKodu", MySqlDbType.Int32),
                 new MySqlParameter("?Sinif", MySqlDbType.Int32),
                 new MySqlParameter("?Sube", MySqlDbType.String),
                 new MySqlParameter("?Sayac", MySqlDbType.Int32),
                new MySqlParameter("?Grup", MySqlDbType.String)
            };
            pars[0].Value = sinavId;
            pars[1].Value = ilceId;
            pars[2].Value = bransId;
            pars[3].Value = kurumKodu;
            pars[4].Value = sinif;
            pars[5].Value = sube;
            pars[6].Value = sayac;
            pars[7].Value = grup;
            helper.ExecuteNonQuery(sql, pars);
        }
    }


}
