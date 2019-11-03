using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using MySql.Data.MySqlClient;


namespace DAL
{
    public class RubrikInfo
    {
        public int Id { get; set; }
        public string Kazanimlar { get; set; }
        public int SinavId { get; set; }
        public int Sinif { get; set; }
        public int SoruNo { get; set; }
        public int KismiPuan { get; set; }
        public int Tampuan { get; set; }
        public int BransId { get; set; }
        public string Soru { get; set; }
        public string DogruCevap { get; set; }
        public string YanlisCevap { get; set; }
        public string KismiCevap { get; set; }
        public string BransAdi { get; set; }
        public string KitapcikTuru { get; set; }
        public RubrikInfo()
        { }

        public RubrikInfo(int bransId, string bransAdi)
        {
            BransId = bransId;
            BransAdi = bransAdi;
        }
        public RubrikInfo(int bransId, string kazanimlar, int sinavId,int sinif,int soruNo,string kitapcikTuru)
        {
            BransId = bransId;
            Kazanimlar = kazanimlar;
            SinavId = sinavId;
            Sinif = sinif;
            SoruNo = soruNo;
            KitapcikTuru = kitapcikTuru;
        }
        public RubrikInfo(int soruNo)
        {
            SoruNo = soruNo;
        }
        public RubrikInfo(int soruNo, string kitapcikTuru,int bransId)
        {
            SoruNo = soruNo;
            KitapcikTuru = kitapcikTuru;
            BransId = bransId;
        }
        public RubrikInfo(string kazanimlar)
        {
            Kazanimlar = kazanimlar;
        }
    }
    public class RubrikDb
    {
        private readonly HelperDb helper = new HelperDb();
        public DataTable KayitlariGetir()
        {
            const string sql = "select * from rubrik order by Id asc";
            return helper.ExecuteDataSet(sql).Tables[0];
        }
        public RubrikInfo KayitBilgiGetir(string cmdText, params MySqlParameter[] param)
        {
            MySqlDataReader dr = helper.ExecuteReader(cmdText, param);
            return TabloAlanlar(dr); 
        }
        public DataTable KayitlariGetir(int sinavId)
        {
            const string sql = "select * from rubrik where SinavId=?SinavId order by SoruNo asc";
            MySqlParameter[] param =
            {
                new MySqlParameter("?SinavId", MySqlDbType.Int32)
            };
            param[0].Value = sinavId;

            return helper.ExecuteDataSet(sql,param).Tables[0];
        }
        public DataTable KayitlariGetir(int sinavId, string kitapcikTuru)
        {
            const string sql = "select * from rubrik where SinavId=?SinavId and KitapcikTuru=?KitapcikTuru order by BransId,SoruNo asc";
            MySqlParameter[] param =
            {
                new MySqlParameter("?SinavId", MySqlDbType.Int32),
                new MySqlParameter("?KitapcikTuru", MySqlDbType.String)
            };
            param[0].Value = sinavId;
            param[1].Value = kitapcikTuru;

            return helper.ExecuteDataSet(sql, param).Tables[0];
        }
        public List<RubrikInfo> KayitlariGetir(int sinavId, int sinif, int brans,string kitapcikTuru)
        {
            const string sql = @"Select * from rubrik
            where rubrik.SinavId =?SinavId and rubrik.Sinif=?Sinif and rubrik.BransId=?BransId and rubrik.KitapcikTuru=?KitapcikTuru order by rubrik.SoruNo asc";

            MySqlParameter[] param =
            {
                new MySqlParameter("?SinavId", MySqlDbType.Int32),
                new MySqlParameter("?Sinif", MySqlDbType.Int32),
                new MySqlParameter("?BransId", MySqlDbType.Int32),
                new MySqlParameter("?KitapcikTuru", MySqlDbType.String)
            };
            param[0].Value = sinavId;
            param[1].Value = sinif;
            param[2].Value = brans;
            param[3].Value = kitapcikTuru;
            DataTable veriler = helper.ExecuteDataSet(sql, param).Tables[0];

            return (from DataRow row in veriler.Rows select new RubrikInfo(Convert.ToInt32(row["BransId"]), row["Kazanimlar"].ToString(), Convert.ToInt32(row["SinavId"]), Convert.ToInt32(row["Sinif"]), Convert.ToInt32(row["SoruNo"]), row["KitapcikTuru"].ToString())).ToList();

        }
        public List<RubrikInfo> SinavdakiBranslar(int sinavId)
        {
            const string sql = @"Select DISTINCT rubrik.BransId,branslar.BransAdi from rubrik
                                INNER JOIN branslar ON rubrik.BransId = branslar.Id
                                 where rubrik.SinavId = ?SinavId order by rubrik.BransId asc";

            MySqlParameter param = new MySqlParameter("?SinavId", MySqlDbType.Int32) { Value = sinavId };
            
            DataTable veriler = helper.ExecuteDataSet(sql, param).Tables[0];

            return (from DataRow row in veriler.Rows select new RubrikInfo(Convert.ToInt32(row["BransId"]), row["BransAdi"].ToString())).ToList();

        }
        public List<RubrikInfo> SinavdakiKazanimlar(int sinavId,int brans)
        {
            const string sql = @"Select DISTINCT(rubrik.Kazanimlar) from rubrik
                                where rubrik.SinavId =?SinavId and rubrik.BransId=?BransId order by rubrik.Kazanimlar asc";
            MySqlParameter[] param =
            {
                new MySqlParameter("?SinavId", MySqlDbType.Int32),
                new MySqlParameter("?BransId", MySqlDbType.Int32)
            };
            param[0].Value = sinavId;
            param[1].Value = brans;
            DataTable veriler = helper.ExecuteDataSet(sql, param).Tables[0];

            return (from DataRow row in veriler.Rows select new RubrikInfo(row["Kazanimlar"].ToString())).ToList();
        }
        public List<RubrikInfo> SinavdakiSoruNolar(int sinavId,int sinif,int brans)
        {
            const string sql = @"Select rubrik.SoruNo from rubrik
            where rubrik.SinavId =?SinavId and rubrik.Sinif=?Sinif and rubrik.BransId=?BransId order by rubrik.SoruNo asc";

            MySqlParameter[] param =
            {
                new MySqlParameter("?SinavId", MySqlDbType.Int32),
                new MySqlParameter("?Sinif", MySqlDbType.Int32),
                new MySqlParameter("?BransId", MySqlDbType.Int32)
            };
            param[0].Value = sinavId;
            param[1].Value = sinif;
            param[2].Value = brans;
            DataTable veriler = helper.ExecuteDataSet(sql, param).Tables[0];

            return (from DataRow row in veriler.Rows select new RubrikInfo(Convert.ToInt32(row["SoruNo"]))).ToList();

        }
        public List<RubrikInfo> KazanimdakiSoruNolar(string kazanimNo,int sinavId, int sinif, int brans)
        {
            const string sql = @"Select SoruNo,KitapcikTuru from rubrik
            where Kazanimlar=?Kazanimlar and SinavId=?SinavId and Sinif=?Sinif and BransId=?BransId order by SoruNo asc";

            MySqlParameter[] param =
            {
                new MySqlParameter("?SinavId", MySqlDbType.Int32),
                new MySqlParameter("?Sinif", MySqlDbType.Int32),
                new MySqlParameter("?BransId", MySqlDbType.Int32),
                new MySqlParameter("?Kazanimlar", MySqlDbType.String)
            };
            param[0].Value = sinavId;
            param[1].Value = sinif;
            param[2].Value = brans;
            param[3].Value = kazanimNo;
            DataTable veriler = helper.ExecuteDataSet(sql, param).Tables[0];

            return (from DataRow row in veriler.Rows select new RubrikInfo(Convert.ToInt32(row["SoruNo"]), row["KitapcikTuru"].ToString(),0)).ToList();

        }
        public RubrikInfo KayitBilgiGetir(int id)
        {
            string cmdText = "select * from rubrik where Id=?Id";
            MySqlParameter param = new MySqlParameter("?Id", MySqlDbType.Int32) { Value = id };
            MySqlDataReader dr = helper.ExecuteReader(cmdText, param);
            return TabloAlanlar(dr); 
        }
        private static RubrikInfo TabloAlanlar(MySqlDataReader dr)
        {
            RubrikInfo info = new RubrikInfo();
            while (dr.Read())
            {
                info.Id = dr.GetMySayi("Id");
                info.Kazanimlar = dr.GetMyMetin("Kazanimlar");
                info.SinavId = dr.GetMySayi("SinavId");
                info.Sinif = dr.GetMySayi("Sinif");
                info.SoruNo = dr.GetMySayi("SoruNo");
                info.KismiPuan = dr.GetMySayi("KismiPuan");
                info.Tampuan = dr.GetMySayi("Tampuan");
                info.BransId = dr.GetMySayi("BransId");
                info.Soru = dr.GetMyMetin("Soru");
                info.DogruCevap = dr.GetMyMetin("DogruCevap");
                info.YanlisCevap = dr.GetMyMetin("YanlisCevap");
                info.KismiCevap = dr.GetMyMetin("KismiCevap");
            }
            dr.Close();
            return info;
        }

        public RubrikInfo KayitBilgiGetir(int sinavId, int soruNo)
        {
            string cmdText = "select * from rubrik where SinavId=?SinavId and SoruNo=?SoruNo";
            MySqlParameter[] pars = 
            {
                new MySqlParameter("?SinavId", MySqlDbType.Int32),
                new MySqlParameter("?SoruNo", MySqlDbType.Int32)
            };
            pars[0].Value = sinavId;
            pars[1].Value = soruNo;
            MySqlDataReader dr = helper.ExecuteReader(cmdText, pars);
            return TabloAlanlar(dr); 
        }
        public void KayitSil(int id)
        {
            const string sql = "delete from rubrik where Id=?Id";
            MySqlParameter p = new MySqlParameter("?Id", MySqlDbType.Int32) { Value = id };
            helper.ExecuteNonQuery(sql, p);
        }
        public void KayitEkle(RubrikInfo info)
        {
            const string sql =
                @"insert into rubrik (SinavId,SoruNo,KismiPuan,Tampuan,BransId,DogruCevap,YanlisCevap,KismiCevap,Soru,Sinif) values (?SinavId,?SoruNo,?KismiPuan,?Tampuan,?BransId,?DogruCevap,?YanlisCevap,?KismiCevap,?Soru,?Sinif)";
            MySqlParameter[] pars =
            {
                new MySqlParameter("?SinavId", MySqlDbType.Int32),
                new MySqlParameter("?SoruNo", MySqlDbType.Int32),
                new MySqlParameter("?KismiPuan", MySqlDbType.Int32),
                new MySqlParameter("?Tampuan", MySqlDbType.Int32),
                new MySqlParameter("?BransId", MySqlDbType.Int32),
                new MySqlParameter("?DogruCevap", MySqlDbType.String),
                new MySqlParameter("?YanlisCevap", MySqlDbType.String),
                new MySqlParameter("?KismiCevap", MySqlDbType.String),
                new MySqlParameter("?Soru", MySqlDbType.String),
                new MySqlParameter("?Sinif", MySqlDbType.Int32)
            };
            pars[0].Value = info.SinavId;
            pars[1].Value = info.SoruNo;
            pars[2].Value = info.KismiPuan;
            pars[3].Value = info.Tampuan;
            pars[4].Value = info.BransId;
            pars[5].Value = info.DogruCevap;
            pars[6].Value = info.YanlisCevap;
            pars[7].Value = info.KismiCevap;
            pars[8].Value = info.Soru;
            pars[9].Value = info.Sinif;
            helper.ExecuteNonQuery(sql, pars);
        }
        public void KayitGuncelle(int id,string kazanimlar)
        {
            const string sql ="update rubrik set Kazanimlar=?Kazanimlar where Id=?Id";
            MySqlParameter[] pars =
            {
                new MySqlParameter("?Kazanimlar", MySqlDbType.String),
                new MySqlParameter("?Id", MySqlDbType.Int32)
            };
            pars[0].Value = kazanimlar;
            pars[1].Value = id;
            helper.ExecuteNonQuery(sql, pars);
        }
        public void KayitGuncelle(RubrikInfo info)
        {
            const string sql =
                @"update rubrik set SinavId=?SinavId,SoruNo=?SoruNo,KismiPuan=?KismiPuan,Tampuan=?Tampuan,BransId=?BransId,DogruCevap=?DogruCevap,YanlisCevap=?YanlisCevap,KismiCevap=?KismiCevap,Soru=?Soru,Sinif=?Sinif where Id=?Id";
            MySqlParameter[] pars =
            {
                new MySqlParameter("?SinavId", MySqlDbType.Int32),
                new MySqlParameter("?SoruNo", MySqlDbType.Int32),
                new MySqlParameter("?KismiPuan", MySqlDbType.Int32),
                new MySqlParameter("?Tampuan", MySqlDbType.Int32),
                new MySqlParameter("?BransId", MySqlDbType.Int32),
                new MySqlParameter("?DogruCevap", MySqlDbType.String),
                new MySqlParameter("?YanlisCevap", MySqlDbType.String),
                new MySqlParameter("?KismiCevap", MySqlDbType.String),
                new MySqlParameter("?Soru", MySqlDbType.String),
                new MySqlParameter("?Sinif", MySqlDbType.Int32),
                new MySqlParameter("?Id", MySqlDbType.Int32)
            };
            pars[0].Value = info.SinavId;
            pars[1].Value = info.SoruNo;
            pars[2].Value = info.KismiPuan;
            pars[3].Value = info.Tampuan;
            pars[4].Value = info.BransId;
            pars[5].Value = info.DogruCevap;
            pars[6].Value = info.YanlisCevap;
            pars[7].Value = info.KismiCevap;
            pars[8].Value = info.Soru;
            pars[9].Value = info.Sinif;
            pars[10].Value = info.Id;
            helper.ExecuteNonQuery(sql, pars);
        }
        public bool KayitKontrol(int sinavId, int sinif, int brans, int soruNo)
        {
            string cmdText = "select count(Id) from rubrik where SinavId=?SinavId and SoruNo=?SoruNo and Sinif=?Sinif and BransId=?BransId";
            MySqlParameter[] pars = 
            {
                new MySqlParameter("?SinavId", MySqlDbType.Int32),
                new MySqlParameter("?SoruNo", MySqlDbType.Int32),
                new MySqlParameter("?Sinif", MySqlDbType.Int32),
                new MySqlParameter("?BransId", MySqlDbType.Int32)
            };
            pars[0].Value = sinavId;
            pars[1].Value = soruNo;
            pars[2].Value = sinif;
            pars[3].Value = brans;
            bool sonuc = Convert.ToInt32(helper.ExecuteScalar(cmdText, pars)) > 0;
            return sonuc;
        }
    }
       
}