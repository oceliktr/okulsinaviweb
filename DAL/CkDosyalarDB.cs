using System;
using System.Collections.Generic;
using System.Linq;
using System.Data;
using MySql.Data.MySqlClient;

namespace DAL
{
    public class CkDosyalarInfo
    {
        public string DizinAdresi { get; set; }
        public string DosyaAdi { get; set; }
        public int Boyutlandir { get; set; }
        public int OgrenciId { get; set; }
        public int SinavId { get; set; }
        public int Oturum { get; set; }
        public int Girmedi { get; set; }
        public string KitapcikTuru { get; set; }
        public int CkKontrol { get; set; }
        public CkDosyalarInfo()
        { }

        public CkDosyalarInfo(string dosyaAdresi,string dosyaAdi,int boyutlandir,int ogrenciId,int sinavId,int oturum,int girmedi,string kitapcikTuru,int ckKontrol)
        {
            DizinAdresi = dosyaAdresi;
            DosyaAdi = dosyaAdi;
            Boyutlandir = boyutlandir;
            OgrenciId = ogrenciId;
            Oturum = oturum;
            SinavId = sinavId;
            Girmedi = girmedi;
            KitapcikTuru = kitapcikTuru;
            CkKontrol = ckKontrol;
        }
    }

    public class CkDosyalarDB
    {
        readonly HelperDb helper = new HelperDb();

        public DataTable KayitlariGetir()
        {
            const string sql = "select * from ckdosyalar order by DizinAdresi asc";
            return helper.ExecuteDataSet(sql).Tables[0];
        }
        public List<CkDosyalarInfo> KayitlariDiziyeGetir(int sinavId)
        {
            string sql = "select * from ckdosyalar where SinavId=?SinavId order by Oturum";
            MySqlParameter p = new MySqlParameter("?SinavId", MySqlDbType.Int32) { Value = sinavId };

            DataTable veriler = helper.ExecuteDataSet(sql,p).Tables[0];

            return (from DataRow row in veriler.Rows select new CkDosyalarInfo(row["DizinAdresi"].ToString(), row["DosyaAdi"].ToString(), Convert.ToInt32(row["Boyutlandir"]), Convert.ToInt32(row["OgrenciId"]), Convert.ToInt32(row["SinavId"]), Convert.ToInt32(row["Oturum"]), Convert.ToInt32(row["Girmedi"]), row["KitapcikTuru"].ToString(), Convert.ToInt32(row["CkKontrol"]))).ToList();
        }
        /// <summary>
        /// Karekod veya ocr dosyasý okunamayan dosyalar
        /// </summary>
        /// <param name="sinavId"></param>
        /// <returns></returns>
        public List<CkDosyalarInfo> SorunluKayitlariDiziyeGetir(int sinavId)
        {
            string sql = "select * from ckdosyalar where SinavId=?SinavId and ((OgrenciId=0 or Oturum=0) or (Girmedi=0 and KitapcikTuru='') or CkKontrol=0)";
            MySqlParameter p = new MySqlParameter("?SinavId", MySqlDbType.Int32) { Value = sinavId };

            DataTable veriler = helper.ExecuteDataSet(sql, p).Tables[0];

            return (from DataRow row in veriler.Rows select new CkDosyalarInfo(row["DizinAdresi"].ToString(), row["DosyaAdi"].ToString(), Convert.ToInt32(row["Boyutlandir"]), Convert.ToInt32(row["OgrenciId"]), Convert.ToInt32(row["SinavId"]), Convert.ToInt32(row["Oturum"]), Convert.ToInt32(row["Girmedi"]), row["KitapcikTuru"].ToString(), Convert.ToInt32(row["CkKontrol"]))).ToList();
        }
        public CkDosyalarInfo KayitBilgiGetir(string cmdText, params MySqlParameter[] param)
        {
            MySqlDataReader dr = helper.ExecuteReader(cmdText, param);
            return TabloAlanlar(dr);
        }

        private static CkDosyalarInfo TabloAlanlar(MySqlDataReader dr)
        {
            CkDosyalarInfo info = new CkDosyalarInfo();
            while (dr.Read())
            {
                info.DizinAdresi = dr.GetMyMetin("DizinAdresi");
                info.DosyaAdi = dr.GetMyMetin("DosyaAdi");
                info.Boyutlandir = dr.GetMySayi("Boyutlandir");
                info.SinavId = dr.GetMySayi("SinavId");
                info.Oturum = dr.GetMySayi("Oturum");
                info.OgrenciId = dr.GetMySayi("OgrenciId");
                info.KitapcikTuru = dr.GetMyMetin("KitapcikTuru");
            }
            dr.Close();

            return info;
        }

        public CkDosyalarInfo KayitBilgiGetir(int sinavId,int ogrenciId,int oturum)
        {
            const string cmdText = "select * from ckdosyalar where OgrenciId=?OgrenciId and Oturum=?Oturum and SinavId=?SinavId";
            MySqlParameter[] pars =
            {
                new MySqlParameter("?OgrenciId", MySqlDbType.Int32),
                new MySqlParameter("?Oturum", MySqlDbType.Int32),
                new MySqlParameter("?SinavId", MySqlDbType.Int32)
            };
            pars[0].Value = ogrenciId;
            pars[1].Value = oturum;
            pars[2].Value = sinavId;
            MySqlDataReader dr = helper.ExecuteReader(cmdText, pars);

            return TabloAlanlar(dr);
        }

        public CkDosyalarInfo KayitBilgiGetir(string dosyaAdresi)
        {
            const string cmdText = "select * from ckdosyalar where DizinAdresi=?DizinAdresi";
            MySqlParameter param = new MySqlParameter("?DizinAdresi", MySqlDbType.String) {Value = dosyaAdresi};
            MySqlDataReader dr = helper.ExecuteReader(cmdText, param);

            return TabloAlanlar(dr);
        }

        public void KayitSil(int sinavId)
        {
            const string sql = "delete from ckdosyalar where SinavId=?SinavId";
            MySqlParameter p = new MySqlParameter("?SinavId", MySqlDbType.Int32) { Value = sinavId };
            helper.ExecuteNonQuery(sql,p);
        }
      
        public void KayitEkle(string dizinAdresi,string dosyaAdi,int sinavId)
        {
            const string sql = @"insert into ckdosyalar (DizinAdresi,DosyaAdi,SinavId) values (?DizinAdresi,?DosyaAdi,?SinavId)";
            MySqlParameter[] pars =
                {
                    new MySqlParameter("?DizinAdresi", MySqlDbType.String),
                    new MySqlParameter("?DosyaAdi", MySqlDbType.String),
                    new MySqlParameter("?SinavId", MySqlDbType.Int32)
                };
            pars[0].Value = dizinAdresi;
            pars[1].Value = dosyaAdi;
            pars[2].Value = sinavId;
            helper.ExecuteNonQuery(sql, pars);
        }
        public void KayitGuncelle(string dizinAdresi, string dosyaAdi,int oturum,int ogrenciId)
        {
            const string sql = @"update ckdosyalar set Oturum=?Oturum,OgrenciId=?OgrenciId where DizinAdresi=?DizinAdresi and DosyaAdi=?DosyaAdi";
            MySqlParameter[] pars =
            {
                new MySqlParameter("?Oturum", MySqlDbType.Int32),
                new MySqlParameter("?OgrenciId", MySqlDbType.Int32),
                new MySqlParameter("?DizinAdresi", MySqlDbType.String),
                new MySqlParameter("?DosyaAdi", MySqlDbType.String)
            };
            pars[0].Value = oturum;
            pars[1].Value = ogrenciId;
            pars[2].Value = dizinAdresi;
            pars[3].Value = dosyaAdi;
            helper.ExecuteNonQuery(sql, pars);
        }
        public void SinavaGirmedi(int ogrenciId, int oturum)
        {
            string sql = "update ckdosyalar set Girmedi=1 where OgrenciId=?OgrenciId and Oturum=?Oturum";
            MySqlParameter[] pars =
            {
                new MySqlParameter("?Oturum", MySqlDbType.Int32),
                new MySqlParameter("?OgrenciId", MySqlDbType.Int32)
            };
            pars[0].Value = oturum;
            pars[1].Value = ogrenciId;
            helper.ExecuteNonQuery(sql, pars);
        }
        public void KitapcikTuru(string dosyaAdi, string kitapcikTuru)
        {
            string sql = "update ckdosyalar set KitapcikTuru=?KitapcikTuru where DosyaAdi=?DosyaAdi";
            MySqlParameter[] pars =
            {
                new MySqlParameter("?DosyaAdi", MySqlDbType.String),
                new MySqlParameter("?KitapcikTuru", MySqlDbType.String)
            };
            pars[0].Value = dosyaAdi;
            pars[1].Value = kitapcikTuru;
            helper.ExecuteNonQuery(sql, pars);
        }
        public void BoyutlandirmaIslemiGordu(string dizinAdresi, string dosyaAdi)
        {
            const string sql = @"update ckdosyalar set Boyutlandir=1 where DizinAdresi=?DizinAdresi and DosyaAdi=?DosyaAdi";
            MySqlParameter[] pars = 
            {
                new MySqlParameter("?DizinAdresi", MySqlDbType.String),
                new MySqlParameter("?DosyaAdi", MySqlDbType.String)
            };
            pars[0].Value = dizinAdresi;
            pars[1].Value = dosyaAdi;
            helper.ExecuteNonQuery(sql, pars);
        }
        public void CKIslemiGordu(string dizinAdresi, int islem)
        {
            const string sql = @"update ckdosyalar set Boyutlandir=?Boyutlandir, CkKontrol=?CkKontrol where DizinAdresi=?DizinAdresi";
            MySqlParameter[] pars =
            {
                new MySqlParameter("?DizinAdresi", MySqlDbType.String),
                new MySqlParameter("?Boyutlandir", MySqlDbType.Int32),
                new MySqlParameter("?CkKontrol", MySqlDbType.Int32)
            };
            pars[0].Value = dizinAdresi;
            pars[1].Value = islem;
            pars[2].Value = islem;
            helper.ExecuteNonQuery(sql, pars);
        }
        public void CkKontrolIslemiGordu(string dizinAdresi, string dosyaAdi)
        {
            const string sql = @"update ckdosyalar set CkKontrol=1 where DizinAdresi=?DizinAdresi and DosyaAdi=?DosyaAdi";
            MySqlParameter[] pars =
            {
                new MySqlParameter("?DizinAdresi", MySqlDbType.String),
                new MySqlParameter("?DosyaAdi", MySqlDbType.String)
            };
            pars[0].Value = dizinAdresi;
            pars[1].Value = dosyaAdi;
            helper.ExecuteNonQuery(sql, pars);
        }
        public void CkKontrolIslemiTemizle(int sinavId)
        {
            const string sql = @"update ckdosyalar set CkKontrol=0,OgrenciId=0,Oturum=0,Girmedi=0,KitapcikTuru='' where SinavId=?SinavId";
            MySqlParameter[] pars =
            {
                new MySqlParameter("?SinavId", MySqlDbType.Int32)
            };
            pars[0].Value = sinavId;
            helper.ExecuteNonQuery(sql, pars);
        }
    }

}

