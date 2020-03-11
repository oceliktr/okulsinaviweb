using System;
using System.Collections.Generic;
using System.Data;
using MySql.Data.MySqlClient;


    public class CkIlIlceOrtalamasiInfo
    {
        public int Id { get; set; }
        public int SinavId { get; set; }
        public string Ilce { get; set; }
        public int BransId { get; set; }
        public int Sinif { get; set; }
        public int KazanimId { get; set; }
        public int IlBasariYuzdesi { get; set; }
        public int IlceBasariYuzdesi { get; set; }

        public CkIlIlceOrtalamasiInfo()
        {
        
        }

        public CkIlIlceOrtalamasiInfo(int sinavId,string ilce, int bransId, int sinif, int kazanimId, int ilBasariYuzdesi, int ilceBasariYuzdesi)
        {
            SinavId = sinavId;
            Ilce = ilce;
            BransId = bransId;
            Sinif = sinif;
            KazanimId = kazanimId;
            IlBasariYuzdesi = ilBasariYuzdesi;
            IlceBasariYuzdesi = ilceBasariYuzdesi;
        }
    }

    public class CkIlIlceOrtalamasiDB
    {
        readonly HelperDb helper = new HelperDb();

        public DataTable KayitlariGetir()
        {
            const string sql = "select * from ckililceortalamasi order by Id asc";
            return helper.ExecuteDataSet(sql).Tables[0];
        }
        public List<CkIlIlceOrtalamasiInfo> KayitlariDizeGetir(int sinavId)
        {
            string sql = "select * from ckililceortalamasi where SinavId=?SinavId";
            MySqlParameter[] p =
            {
                new MySqlParameter("?SinavId", MySqlDbType.Int32)
            };
            p[0].Value = sinavId;

            DataTable dt = helper.ExecuteDataSet(sql, p).Tables[0];
            List<CkIlIlceOrtalamasiInfo> karne = new List<CkIlIlceOrtalamasiInfo>();
            foreach (DataRow k in dt.Rows)
            {
                karne.Add(new CkIlIlceOrtalamasiInfo(Convert.ToInt32(k["SinavId"]), k["Ilce"].ToString(),Convert.ToInt32(k["BransId"]), Convert.ToInt32(k["Sinif"]), Convert.ToInt32(k["KazanimId"]), Convert.ToInt32(k["IlBasariYuzdesi"]), Convert.ToInt32(k["IlceBasariYuzdesi"])));
            }
            return karne;
        }
        public CkIlIlceOrtalamasiInfo KayitBilgiGetir(string cmdText, params MySqlParameter[] param)
        {
            MySqlDataReader dr = helper.ExecuteReader(cmdText, param);
            CkIlIlceOrtalamasiInfo info = new CkIlIlceOrtalamasiInfo();
            while (dr.Read())
            {
                info.Id = dr.GetMySayi("Id");
                info.SinavId = dr.GetMySayi("SinavId");
                info.Ilce = dr.GetMyMetin("Ilce");
                info.BransId = dr.GetMySayi("BransId");
                info.Sinif = dr.GetMySayi("Sinif");
                info.KazanimId = dr.GetMySayi("KazanimId");
                info.IlBasariYuzdesi = dr.GetMySayi("IlBasariYuzdesi");
                info.IlceBasariYuzdesi = dr.GetMySayi("IlceBasariYuzdesi");
            }
            dr.Close();

            return info;
        }

        public CkIlIlceOrtalamasiInfo KayitBilgiGetir(int id)
        {
            string cmdText = "select * from ckililceortalamasi where Id=?Id";
            MySqlParameter param = new MySqlParameter("?Id", MySqlDbType.Int32) { Value = id };
            MySqlDataReader dr = helper.ExecuteReader(cmdText, param);
            CkIlIlceOrtalamasiInfo info = new CkIlIlceOrtalamasiInfo();
            while (dr.Read())
            {
                info.Id = dr.GetMySayi("Id");
                info.SinavId = dr.GetMySayi("SinavId");
                info.Ilce = dr.GetMyMetin("Ilce");
                info.BransId = dr.GetMySayi("BransId");
                info.Sinif = dr.GetMySayi("Sinif");
                info.KazanimId = dr.GetMySayi("KazanimId");
                info.IlBasariYuzdesi = dr.GetMySayi("IlBasariYuzdesi");
                info.IlceBasariYuzdesi = dr.GetMySayi("IlceBasariYuzdesi");
            }
            dr.Close();

            return info;
        }

        public void KayitSil(int id)
        {
            const string sql = "delete from ckililceortalamasi where Id=?Id";
            MySqlParameter p = new MySqlParameter("?Id", MySqlDbType.Int32) { Value = id };
            helper.ExecuteNonQuery(sql, p);
        }
        public void SinaviSil(int sinavId)
        {
            const string sql = "delete from ckililceortalamasi where SinavId=?SinavId";
            MySqlParameter p = new MySqlParameter("?SinavId", MySqlDbType.Int32) { Value = sinavId };
            helper.ExecuteNonQuery(sql, p);
        }
        public void KayitEkle(CkIlIlceOrtalamasiInfo info)
        {
            const string sql = @"insert into ckililceortalamasi (SinavId,Ilce,BransId,Sinif,KazanimId,IlBasariYuzdesi,IlceBasariYuzdesi) values (?SinavId,?Ilce,?BransId,?Sinif,?KazanimId,?IlBasariYuzdesi,?IlceBasariYuzdesi)";
            MySqlParameter[] pars =
            {
                new MySqlParameter("?SinavId", MySqlDbType.Int32),
                new MySqlParameter("?Ilce", MySqlDbType.String),
                new MySqlParameter("?BransId", MySqlDbType.Int32),
                new MySqlParameter("?Sinif", MySqlDbType.Int32),
                new MySqlParameter("?KazanimId", MySqlDbType.Int32),
                new MySqlParameter("?IlBasariYuzdesi", MySqlDbType.Int32),
                new MySqlParameter("?IlceBasariYuzdesi", MySqlDbType.Int32),
            };
            pars[0].Value = info.SinavId;
            pars[1].Value = info.Ilce;
            pars[2].Value = info.BransId;
            pars[3].Value = info.Sinif;
            pars[4].Value = info.KazanimId;
            pars[5].Value = info.IlBasariYuzdesi;
            pars[6].Value = info.IlceBasariYuzdesi;
            helper.ExecuteNonQuery(sql, pars);
        }

        public void KayitGuncelle(CkIlIlceOrtalamasiInfo info)
        {
            const string sql = @"update ckililceortalamasi set SinavId=?SinavId,Ilce=?Ilce,BransId=?BransId,Sinif=?Sinif,KazanimId=?KazanimId,IlBasariYuzdesi=?IlBasariYuzdesi,IlceBasariYuzdesi=?IlceBasariYuzdesi where Id=?Id";
            MySqlParameter[] pars =
            {
                new MySqlParameter("?SinavId", MySqlDbType.Int32),
                new MySqlParameter("?Ilce", MySqlDbType.String),
                new MySqlParameter("?BransId", MySqlDbType.Int32),
                new MySqlParameter("?Sinif", MySqlDbType.Int32),
                new MySqlParameter("?KazanimId", MySqlDbType.Int32),
                new MySqlParameter("?IlBasariYuzdesi", MySqlDbType.Int32),
                new MySqlParameter("?IlceBasariYuzdesi", MySqlDbType.Int32),
                new MySqlParameter("?Id", MySqlDbType.Int32),
            };
            pars[0].Value = info.SinavId;
            pars[1].Value = info.Ilce;
            pars[2].Value = info.BransId;
            pars[3].Value = info.Sinif;
            pars[4].Value = info.KazanimId;
            pars[5].Value = info.IlBasariYuzdesi;
            pars[6].Value = info.IlceBasariYuzdesi;
            pars[7].Value = info.Id;
            helper.ExecuteNonQuery(sql, pars);
        }
    }
