using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using MySql.Data.MySqlClient;

namespace DAL
{
    public class SonucOptikInfo
    {
        public int BransId { get; set; }
        public int SinavId { get; set; }
        public int Oturum { get; set; }
        public int OgrenciId { get; set; }
        public int Sinif { get; set; }
        public string Sube { get; set; }
        public int KurumKodu { get; set; }
        public int SoruNo { get; set; }
        public int Puani { get; set; }
        public string Secenek { get; set; }
        public string KitapcikTuru { get; set; }
        public int DogruSayisi { get; set; }
        public int YanlisSayisi { get; set; }
        public int BosSayisi { get; set; }
        public SonucOptikInfo()
        { }

        public SonucOptikInfo(int bransId, int sinavId, int oturum, int ogrenciId, int sinif, string sube, int kurumKodu, int soruNo, int puani, string secenek, string kitapcikTuru)
        {
            BransId = bransId;
            SinavId = sinavId;
            Oturum = oturum;
            OgrenciId = ogrenciId;
            Sinif = sinif;
            Sube = sube;
            KurumKodu = kurumKodu;
            SoruNo = soruNo;
            Puani = puani;
            Secenek = secenek;
            KitapcikTuru = kitapcikTuru;
        }

        public SonucOptikInfo(int dogruSayisi, int yanlisSayisi, int bosSayisi)
        {
            DogruSayisi = dogruSayisi;
            YanlisSayisi = yanlisSayisi;
            BosSayisi = bosSayisi;
        }
    }

    public class SonucOptikDB
    {
        readonly HelperDb helper = new HelperDb();

        public List<SonucOptikInfo> BosSecenekler(int sinavId, int oturum,int bransId, int ogrenciId)
        {
            const string sql = "select * from sonucoptik where SinavId=?SinavId and Oturum=?Oturum and OgrenciId=?OgrenciId and Secenek='' and BransId=?BransId";

            MySqlParameter[] pars =
            {
                new MySqlParameter("?SinavId", MySqlDbType.Int32),
                new MySqlParameter("?Oturum", MySqlDbType.Int32),
                new MySqlParameter("?BransId", MySqlDbType.Int32),
                new MySqlParameter("?OgrenciId", MySqlDbType.Int32)
            };
            pars[0].Value = sinavId;
            pars[1].Value = oturum;
            pars[2].Value = bransId;
            pars[3].Value = ogrenciId;

            DataTable veriler = helper.ExecuteDataSet(sql, pars).Tables[0];

            return (from DataRow row in veriler.Rows select new SonucOptikInfo(Convert.ToInt32(row["BransId"]), Convert.ToInt32(row["SinavId"]), Convert.ToInt32(row["Oturum"]), Convert.ToInt32(row["OgrenciId"]), Convert.ToInt32(row["Sinif"]),row["Sube"].ToString(), Convert.ToInt32(row["KurumKodu"]), Convert.ToInt32(row["SoruNo"]), Convert.ToInt32(row["Puani"]), row["Secenek"].ToString(), row["KitapcikTuru"].ToString())).ToList();
        }
        public SonucOptikInfo KayitBilgiGetir(string cmdText, params MySqlParameter[] param)
        {
            MySqlDataReader dr = helper.ExecuteReader(cmdText, param);
            SonucOptikInfo info = TabloAlanlar(dr);

            return info;
        }
        private static SonucOptikInfo TabloAlanlar(MySqlDataReader dr)
        {
            SonucOptikInfo info = new SonucOptikInfo();
            while (dr.Read())
            {
                info.SinavId = dr.GetMySayi("SinavId");
                info.Oturum = dr.GetMySayi("Oturum");
                info.BransId = dr.GetMySayi("BransId");
                info.OgrenciId = dr.GetMySayi("OgrenciId");
                info.KurumKodu = dr.GetMySayi("KurumKodu");
                info.SoruNo = dr.GetMySayi("SoruNo");
                info.Puani = dr.GetMySayi("Puani");
                info.Secenek = dr.GetMyMetin("Secenek");
                info.KitapcikTuru = dr.GetMyMetin("KitapcikTuru");
            }
            dr.Close();
            return info;
        }

        public SonucOptikInfo KayitBilgiGetir(int sinavId, int bransId, int ogrId, int soruNo)
        {
            string cmdText = "select sonucoptik.Secenek from sonucoptik where SinavId=?SinavId and BransId=?BransId and OgrenciId=?OgrenciId and SoruNo=?SoruNo";
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
            SonucOptikInfo info = TabloAlanlar(dr);

            return info;
        }
        public void KayitSil(int sinavId)
        {
            const string sql = "delete from sonucoptik where SinavId=?SinavId";
            MySqlParameter p = new MySqlParameter("?SinavId", MySqlDbType.Int32) { Value = sinavId };
            helper.ExecuteNonQuery(sql, p);
        }
        public void KayitSil(int sinavId, int oturum,int ogrenciId)
        {
            const string sql = "delete from sonucoptik where SinavId=?SinavId and Oturum=?Oturum and OgrenciId=?OgrenciId";
            MySqlParameter[] param =
            {
                new MySqlParameter("?SinavId", MySqlDbType.Int32),
                new MySqlParameter("?Oturum", MySqlDbType.Int32),
                new MySqlParameter("?OgrenciId", MySqlDbType.Int32)
            };
            param[0].Value = sinavId;
            param[1].Value = oturum;
            param[2].Value = ogrenciId;
            helper.ExecuteNonQuery(sql, param);
        }
        public void KayitEkle(SonucOptikInfo info)
        {
            const string sql = @"insert into sonucoptik (SinavId,OgrenciId,SoruNo,Secenek,BransId,KurumKodu,Oturum,KitapcikTuru,Puani,Sinif,Sube) values (?SinavId,?OgrenciId,?SoruNo,?Secenek,?BransId,?KurumKodu,?Oturum,?KitapcikTuru,?Puani,?Sinif,?Sube)";
            MySqlParameter[] pars =
            {
                new MySqlParameter("?SinavId", MySqlDbType.Int32),
                new MySqlParameter("?OgrenciId", MySqlDbType.Int32),
                new MySqlParameter("?KurumKodu", MySqlDbType.Int32),
                new MySqlParameter("?SoruNo", MySqlDbType.Int32),
                new MySqlParameter("?Secenek", MySqlDbType.String),
                new MySqlParameter("?BransId", MySqlDbType.String),
                new MySqlParameter("?Oturum", MySqlDbType.Int32),
                new MySqlParameter("?KitapcikTuru", MySqlDbType.String),
                new MySqlParameter("?Puani", MySqlDbType.Int32),
                new MySqlParameter("?Sinif", MySqlDbType.Int32),
                new MySqlParameter("?Sube", MySqlDbType.String)
            };
            pars[0].Value = info.SinavId;
            pars[1].Value = info.OgrenciId;
            pars[2].Value = info.KurumKodu;
            pars[3].Value = info.SoruNo;
            pars[4].Value = info.Secenek;
            pars[5].Value = info.BransId;
            pars[6].Value = info.Oturum;
            pars[7].Value = info.KitapcikTuru;
            pars[8].Value = info.Puani;
            pars[9].Value = info.Sinif;
            pars[10].Value = info.Sube;
            helper.ExecuteNonQuery(sql, pars);
        }
       
        public void KayitGuncelle(int sinavId,int oturum,int ogrenciId,int soruNo,int brans,string secenek,int puani)
        {
            const string sql = @"update sonucoptik set Secenek=?Secenek,Puani=?Puani where SinavId=?SinavId and Oturum=?Oturum and OgrenciId=?OgrenciId and BransId=?BransId and SoruNo=?SoruNo";
            MySqlParameter[] pars =
            {
                new MySqlParameter("?SinavId", MySqlDbType.Int32),
                new MySqlParameter("?Oturum", MySqlDbType.Int32),
                new MySqlParameter("?OgrenciId", MySqlDbType.Int32),
                new MySqlParameter("?SoruNo", MySqlDbType.Int32),
                new MySqlParameter("?BransId", MySqlDbType.Int32),
                new MySqlParameter("?Secenek", MySqlDbType.String),
                new MySqlParameter("?Puani", MySqlDbType.Int32)
            };
            pars[0].Value = sinavId;
            pars[1].Value = oturum;
            pars[2].Value = ogrenciId;
            pars[3].Value = soruNo;
            pars[4].Value = brans;
            pars[5].Value = secenek;
            pars[6].Value = puani;
            helper.ExecuteNonQuery(sql, pars);
        }
        public List<SonucOptikInfo> KayitSayisi(int sinavId, int kurumKodu,int brans, int soruNo,string kitapcikTuru,int sinif,string sube)
        {
            //string sql = @"select
            //             (select Count(Puani) FROM sonucoptik where SinavId=?SinavId and KurumKodu=?KurumKodu and BransId=?BransId and SoruNo=?SoruNo and KitapcikTuru=?KitapcikTuru and Sinif=?Sinif and Sube=?Sube and Secenek<>'' and Puani<>0) as DogruSayisi,
            //             (select Count(Puani) FROM sonucoptik where SinavId=?SinavId and KurumKodu=?KurumKodu and BransId=?BransId and SoruNo=?SoruNo and KitapcikTuru=?KitapcikTuru and  Sinif=?Sinif and Sube=?Sube and Secenek<>'' and Puani=0) as YanlisSayisi ,
            //             (select Count(Secenek) FROM sonucoptik where SinavId=?SinavId and KurumKodu=?KurumKodu and BransId=?BransId and SoruNo=?SoruNo and KitapcikTuru=?KitapcikTuru and  Sinif=?Sinif and Sube=?Sube and Secenek='') as BosSayisi";
            //45 saat

            string sql = @"select
            sum(case when Puani = 1 then 1 else 0 end) as DogruSayisi,
            sum(case when Puani = 0 and Secenek <> '' then 1 else 0 end) as YanlisSayisi,
            sum(case when Secenek = '' then 1 else 0 end) as BosSayisi
            from sonucoptik where SinavId=?SinavId and KurumKodu=?KurumKodu and BransId=?BransId and SoruNo=?SoruNo and KitapcikTuru=?KitapcikTuru and  Sinif=?Sinif and Sube=?Sube";
            MySqlParameter[] pars =
            {
                new MySqlParameter("?SinavId", MySqlDbType.Int32),
                new MySqlParameter("?KurumKodu", MySqlDbType.Int32),
                new MySqlParameter("?BransId", MySqlDbType.Int32),
                new MySqlParameter("?SoruNo", MySqlDbType.Int32),
                new MySqlParameter("?KitapcikTuru",MySqlDbType.String),
                new MySqlParameter("?Sinif", MySqlDbType.Int32),
                new MySqlParameter("?Sube",MySqlDbType.String)
            };
            pars[0].Value = sinavId;
            pars[1].Value = kurumKodu;
            pars[2].Value = brans;
            pars[3].Value = soruNo;
            pars[4].Value = kitapcikTuru;
            pars[5].Value = sinif;
            pars[6].Value = sube;

            DataTable veriler = helper.ExecuteDataSet(sql, pars).Tables[0];
             return (from DataRow row in veriler.Rows select new SonucOptikInfo(Convert.ToInt32(row["DogruSayisi"]), Convert.ToInt32(row["YanlisSayisi"]), Convert.ToInt32(row["BosSayisi"]))).ToList();
            }
     }
}