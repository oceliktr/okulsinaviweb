using System;
using System.Collections.Generic;
using System.Data;
using MySql.Data.MySqlClient;


    public class CkKarneSonuclariInfo
    {
        public int Id { get; set; }
        public int SinavId { get; set; }
        public int BransId { get; set; }
        public string Ilce { get; set; }
        public int KurumKodu { get; set; }
        public int Sinif { get; set; }
        public string Sube { get; set; }
        public string KitapcikTuru { get; set; }
        public int SoruNo { get; set; }
        public int Dogru { get; set; }
        public int Yanlis { get; set; }
        public int Bos { get; set; }
        public string SinavAdi { get; set; }
        public string BransAdi { get; set; }
        public CkKarneSonuclariInfo()
        {
        
        }

        public CkKarneSonuclariInfo(int id, int sinavId, int bransId, string ilce, int kurumKodu, int sinif, string sube, string kitapcikTuru, int soruNo, int dogru, int yanlis, int bos)
        {
            Id = id;
            SinavId = sinavId;
            BransId = bransId;
            Ilce = ilce;
            KurumKodu = kurumKodu;
            Sinif = sinif;
            Sube = sube;
            KitapcikTuru = kitapcikTuru;
            SoruNo = soruNo;
            Dogru = dogru;
            Yanlis = yanlis;
            Bos = bos;
        }
        public CkKarneSonuclariInfo(string sinavAdi,string bransAdi, int sinavId, int bransId, string ilce, int kurumKodu, int sinif, string sube, string kitapcikTuru, int soruNo, int dogru, int yanlis, int bos)
        {
            SinavAdi = sinavAdi;
            BransAdi = bransAdi;
            SinavId = sinavId;
            BransId = bransId;
            Ilce = ilce;
            KurumKodu = kurumKodu;
            Sinif = sinif;
            Sube = sube;
            KitapcikTuru = kitapcikTuru;
            SoruNo = soruNo;
            Dogru = dogru;
            Yanlis = yanlis;
            Bos = bos;
        }
    }

    public class CkKarneSonuclariDB
    {
        readonly HelperDb helper = new HelperDb();

        public DataTable KayitlariGetir()
        {
            const string sql = "select * from ckkarnesonuclari order by Id asc";
            return helper.ExecuteDataSet(sql).Tables[0];
        }
        public List<CkKarneSonuclariInfo> KayitlariDizeGetir(int sinavId, string ilce, int kurumKodu,int sinif)
        {
            string sql = "select * from ckkarnesonuclari where SinavId=?SinavId and Ilce=?Ilce and KurumKodu=?KurumKodu and Sinif=?Sinif";
            MySqlParameter[] p =
            {
                new MySqlParameter("?SinavId", MySqlDbType.Int32),
                new MySqlParameter("?Ilce", MySqlDbType.String),
                new MySqlParameter("?KurumKodu", MySqlDbType.Int32),
                new MySqlParameter("?Sinif", MySqlDbType.Int32)
            };
            p[0].Value = sinavId;
            p[1].Value = ilce;
            p[2].Value = kurumKodu;
            p[3].Value = sinif;

            DataTable dt = helper.ExecuteDataSet(sql, p).Tables[0];
            List<CkKarneSonuclariInfo> karne = new List<CkKarneSonuclariInfo>();
            foreach (DataRow k in dt.Rows)
            {
                karne.Add(new CkKarneSonuclariInfo(Convert.ToInt32(k["Id"]), Convert.ToInt32(k["SinavId"]), Convert.ToInt32(k["BransId"]), k["Ilce"].ToString(),
                    Convert.ToInt32(k["KurumKodu"]), Convert.ToInt32(k["Sinif"]), k["Sube"].ToString(), k["KitapcikTuru"].ToString(), Convert.ToInt32(k["SoruNo"]), Convert.ToInt32(k["Dogru"]), Convert.ToInt32(k["Yanlis"]), Convert.ToInt32(k["Bos"])));
            }
            return karne;
        }
        public List<CkKarneSonuclariInfo> KayitlariDizeGetir(int sinavId, string ilce, int kurumKodu)
        {
            string sql = "select * from ckkarnesonuclari where SinavId=?SinavId and Ilce=?Ilce and KurumKodu=?KurumKodu";
            MySqlParameter[] p =
            {
                new MySqlParameter("?SinavId", MySqlDbType.Int32),
                new MySqlParameter("?Ilce", MySqlDbType.String),
                new MySqlParameter("?KurumKodu", MySqlDbType.Int32)
            };
            p[0].Value = sinavId;
            p[1].Value = ilce;
            p[2].Value = kurumKodu;

            DataTable dt = helper.ExecuteDataSet(sql, p).Tables[0];
            List<CkKarneSonuclariInfo> karne = new List<CkKarneSonuclariInfo>();
            foreach (DataRow k in dt.Rows)
            {
                karne.Add(new CkKarneSonuclariInfo(Convert.ToInt32(k["Id"]), Convert.ToInt32(k["SinavId"]), Convert.ToInt32(k["BransId"]), k["Ilce"].ToString(),
                    Convert.ToInt32(k["KurumKodu"]), Convert.ToInt32(k["Sinif"]), k["Sube"].ToString(), k["KitapcikTuru"].ToString(), Convert.ToInt32(k["SoruNo"]), Convert.ToInt32(k["Dogru"]), Convert.ToInt32(k["Yanlis"]), Convert.ToInt32(k["Bos"])));
            }
            return karne;
        }
        public List<CkKarneSonuclariInfo> KayitlariDizeGetir(int kurumKodu, int sinif)
        {
            string sql = @"SELECT sinav.SinavAdi,brans.BransAdi, ks.* FROM ckkarnesonuclari AS ks 
                        INNER JOIN cksinavadi AS sinav ON sinav.SinavId = ks.SinavId
                        INNER JOIN ckkarnebranslar AS brans ON brans.BransId = ks.BransId 
                        where ks.KurumKodu=?KurumKodu and ks.Sinif=?Sinif";
            MySqlParameter[] p =
            {
                new MySqlParameter("?KurumKodu", MySqlDbType.Int32),
                new MySqlParameter("?Sinif", MySqlDbType.Int32)
            };
            p[0].Value = kurumKodu;
            p[1].Value = sinif;

            DataTable dt = helper.ExecuteDataSet(sql, p).Tables[0];
            List<CkKarneSonuclariInfo> karne = new List<CkKarneSonuclariInfo>();
            foreach (DataRow k in dt.Rows)
            {
                karne.Add(new CkKarneSonuclariInfo(k["SinavAdi"].ToString(), k["BransAdi"].ToString(), Convert.ToInt32(k["SinavId"]), Convert.ToInt32(k["BransId"]), k["Ilce"].ToString(),Convert.ToInt32(k["KurumKodu"]), Convert.ToInt32(k["Sinif"]), k["Sube"].ToString(), k["KitapcikTuru"].ToString(), Convert.ToInt32(k["SoruNo"]), Convert.ToInt32(k["Dogru"]), Convert.ToInt32(k["Yanlis"]), Convert.ToInt32(k["Bos"])));
            }
            return karne;
        }
        public List<CkKarneSonuclariInfo> KayitlariDizeGetir(int sinavId, string ilce)
        {
            string sql = "select * from ckkarnesonuclari where SinavId=?SinavId and Ilce=?Ilce";
            MySqlParameter[] p =
            {
                new MySqlParameter("?SinavId", MySqlDbType.Int32),
                new MySqlParameter("?Ilce", MySqlDbType.String)
            };
            p[0].Value = sinavId;
            p[1].Value = ilce;

            DataTable dt = helper.ExecuteDataSet(sql, p).Tables[0];
            List<CkKarneSonuclariInfo> karne = new List<CkKarneSonuclariInfo>();
            foreach (DataRow k in dt.Rows)
            {
                karne.Add(new CkKarneSonuclariInfo(Convert.ToInt32(k["Id"]), Convert.ToInt32(k["SinavId"]), Convert.ToInt32(k["BransId"]), k["Ilce"].ToString(),
                    Convert.ToInt32(k["KurumKodu"]), Convert.ToInt32(k["Sinif"]), k["Sube"].ToString(), k["KitapcikTuru"].ToString(), Convert.ToInt32(k["SoruNo"]), Convert.ToInt32(k["Dogru"]), Convert.ToInt32(k["Yanlis"]), Convert.ToInt32(k["Bos"])));
            }
            return karne;
        }
        public List<CkKarneSonuclariInfo> KayitlariDizeGetir(int sinavId)
        {
            string sql = "select * from ckkarnesonuclari where SinavId=?SinavId";
            MySqlParameter[] p =
            {
                new MySqlParameter("?SinavId", MySqlDbType.Int32)
            };
            p[0].Value = sinavId;

            DataTable dt = helper.ExecuteDataSet(sql, p).Tables[0];
            List<CkKarneSonuclariInfo> karne = new List<CkKarneSonuclariInfo>();
            foreach (DataRow k in dt.Rows)
            {
                karne.Add(new CkKarneSonuclariInfo(Convert.ToInt32(k["Id"]), Convert.ToInt32(k["SinavId"]), Convert.ToInt32(k["BransId"]), k["Ilce"].ToString(),
                    Convert.ToInt32(k["KurumKodu"]), Convert.ToInt32(k["Sinif"]), k["Sube"].ToString(), k["KitapcikTuru"].ToString(), Convert.ToInt32(k["SoruNo"]), Convert.ToInt32(k["Dogru"]), Convert.ToInt32(k["Yanlis"]), Convert.ToInt32(k["Bos"])));
            }
            return karne;
        }
        public CkKarneSonuclariInfo KayitBilgiGetir(string cmdText, params MySqlParameter[] param)
        {
            MySqlDataReader dr = helper.ExecuteReader(cmdText, param);
            CkKarneSonuclariInfo info = new CkKarneSonuclariInfo();
            while (dr.Read())
            {
                info.Id = dr.GetMySayi("Id");
                info.SinavId = dr.GetMySayi("SinavId");
                info.BransId = dr.GetMySayi("BransId");
                info.Ilce = dr.GetMyMetin("Ilce");
                info.KurumKodu = dr.GetMySayi("KurumKodu");
                info.Sinif = dr.GetMySayi("Sinif");
                info.Sube = dr.GetMyMetin("Sube");
                info.KitapcikTuru = dr.GetMyMetin("KitapcikTuru");
                info.SoruNo = dr.GetMySayi("SoruNo");
                info.Dogru = dr.GetMySayi("Dogru");
                info.Yanlis = dr.GetMySayi("Yanlis");
                info.Bos = dr.GetMySayi("Bos");
            }
            dr.Close();

            return info;
        }

        public CkKarneSonuclariInfo KayitBilgiGetir(int id)
        {
            string cmdText = "select * from ckkarnesonuclari where Id=?Id";
            MySqlParameter param = new MySqlParameter("?Id", MySqlDbType.Int32) { Value = id };
            MySqlDataReader dr = helper.ExecuteReader(cmdText, param);
            CkKarneSonuclariInfo info = new CkKarneSonuclariInfo();
            while (dr.Read())
            {
                info.Id = dr.GetMySayi("Id");
                info.SinavId = dr.GetMySayi("SinavId");
                info.BransId = dr.GetMySayi("BransId");
                info.Ilce = dr.GetMyMetin("Ilce");
                info.KurumKodu = dr.GetMySayi("KurumKodu");
                info.Sinif = dr.GetMySayi("Sinif");
                info.Sube = dr.GetMyMetin("Sube");
                info.KitapcikTuru = dr.GetMyMetin("KitapcikTuru");
                info.SoruNo = dr.GetMySayi("SoruNo");
                info.Dogru = dr.GetMySayi("Dogru");
                info.Yanlis = dr.GetMySayi("Yanlis");
                info.Bos = dr.GetMySayi("Bos");
            }
            dr.Close();

            return info;
        }

        public void KayitSil(int id)
        {
            const string sql = "delete from ckkarnesonuclari where Id=?Id";
            MySqlParameter p = new MySqlParameter("?Id", MySqlDbType.Int32) { Value = id };
            helper.ExecuteNonQuery(sql, p);
        }
        public void SinaviSil(int sinavId)
        {
            const string sql = "delete from ckkarnesonuclari where SinavId=?SinavId";
            MySqlParameter p = new MySqlParameter("?SinavId", MySqlDbType.Int32) { Value = sinavId };
            helper.ExecuteNonQuery(sql, p);
        }
        public void KayitEkle(CkKarneSonuclariInfo info)
        {
            const string sql = @"insert into ckkarnesonuclari (SinavId,BransId,Ilce,KurumKodu,Sinif,Sube,KitapcikTuru,SoruNo,Dogru,Yanlis,Bos) values (?SinavId,?BransId,?Ilce,?KurumKodu,?Sinif,?Sube,?KitapcikTuru,?SoruNo,?Dogru,?Yanlis,?Bos)";
            MySqlParameter[] pars =
            {
                new MySqlParameter("?SinavId", MySqlDbType.Int32),
                new MySqlParameter("?BransId", MySqlDbType.Int32),
                new MySqlParameter("?Ilce", MySqlDbType.String),
                new MySqlParameter("?KurumKodu", MySqlDbType.Int32),
                new MySqlParameter("?Sinif", MySqlDbType.Int32),
                new MySqlParameter("?Sube", MySqlDbType.String),
                new MySqlParameter("?KitapcikTuru", MySqlDbType.String),
                new MySqlParameter("?SoruNo", MySqlDbType.Int32),
                new MySqlParameter("?Dogru", MySqlDbType.Int32),
                new MySqlParameter("?Yanlis", MySqlDbType.Int32),
                new MySqlParameter("?Bos", MySqlDbType.Int32),
            };
            pars[0].Value = info.SinavId;
            pars[1].Value = info.BransId;
            pars[2].Value = info.Ilce;
            pars[3].Value = info.KurumKodu;
            pars[4].Value = info.Sinif;
            pars[5].Value = info.Sube;
            pars[6].Value = info.KitapcikTuru;
            pars[7].Value = info.SoruNo;
            pars[8].Value = info.Dogru;
            pars[9].Value = info.Yanlis;
            pars[10].Value = info.Bos;
            helper.ExecuteNonQuery(sql, pars);
        }

        public void KayitGuncelle(CkKarneSonuclariInfo info)
        {
            const string sql = @"update ckkarnesonuclari set SinavId=?SinavId,BransId=?BransId,Ilce=?Ilce,KurumKodu=?KurumKodu,Sinif=?Sinif,Sube=?Sube,KitapcikTuru=?KitapcikTuru,SoruNo=?SoruNo,Dogru=?Dogru,Yanlis=?Yanlis,Bos=?Bos where Id=?Id";
            MySqlParameter[] pars =
            {
                new MySqlParameter("?SinavId", MySqlDbType.Int32),
                new MySqlParameter("?BransId", MySqlDbType.Int32),
                new MySqlParameter("?Ilce", MySqlDbType.String),
                new MySqlParameter("?KurumKodu", MySqlDbType.Int32),
                new MySqlParameter("?Sinif", MySqlDbType.Int32),
                new MySqlParameter("?Sube", MySqlDbType.String),
                new MySqlParameter("?KitapcikTuru", MySqlDbType.String),
                new MySqlParameter("?SoruNo", MySqlDbType.Int32),
                new MySqlParameter("?Dogru", MySqlDbType.Int32),
                new MySqlParameter("?Yanlis", MySqlDbType.Int32),
                new MySqlParameter("?Bos", MySqlDbType.Int32),
                new MySqlParameter("?Id", MySqlDbType.Int32),
            };
            pars[0].Value = info.SinavId;
            pars[1].Value = info.BransId;
            pars[2].Value = info.Ilce;
            pars[3].Value = info.KurumKodu;
            pars[4].Value = info.Sinif;
            pars[5].Value = info.Sube;
            pars[6].Value = info.KitapcikTuru;
            pars[7].Value = info.SoruNo;
            pars[8].Value = info.Dogru;
            pars[9].Value = info.Yanlis;
            pars[10].Value = info.Bos;
            pars[11].Value = info.Id;
            helper.ExecuteNonQuery(sql, pars);
        }
    }
