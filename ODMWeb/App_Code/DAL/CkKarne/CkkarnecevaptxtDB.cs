using System;
using System.Collections.Generic;
using System.Data;
using MySql.Data.MySqlClient;


    public class CkKarneCevapTxtInfo
    {
        public int Id { get; set; }
        public int OpaqId { get; set; }
        public int SinavId { get; set; }
        public string KitapcikTuru { get; set; }
        public int CevapTipi { get; set; }
        public int KatilimDurumu { get; set; }
        public string Cevaplar { get; set; }
        public int BransId { get; set; }

        public CkKarneCevapTxtInfo()
        {
        
        }
        public CkKarneCevapTxtInfo(int id, int opaqId, int sinavId, string kitapcikTuru, int cevapTipi, int katilimDurumu, string cevaplar, int bransId)
        {
            Id = id;
            OpaqId = opaqId;
            SinavId = sinavId;
            KitapcikTuru = kitapcikTuru;
            CevapTipi = cevapTipi;
            KatilimDurumu = katilimDurumu;
            Cevaplar = cevaplar;
            BransId = bransId;
        }
    }

    public class CkKarneCevapTxtDB
    {
        readonly HelperDb helper = new HelperDb();
        public DataTable KayitlariGetir()
        {
            const string sql = "select * from ckkarnecevaptxt order by Id asc";
            return helper.ExecuteDataSet(sql).Tables[0];
        }
        public List<CkKarneCevapTxtInfo> KayitlariDizeGetir(int sinavId)
        {
            string sql = "select * from ckkarnecevaptxt where SinavId=?SinavId";
            MySqlParameter[] p =
            {
                new MySqlParameter("?SinavId", MySqlDbType.Int32)
            };
            p[0].Value = sinavId;

            DataTable dt = helper.ExecuteDataSet(sql, p).Tables[0];
            List<CkKarneCevapTxtInfo> karne = new List<CkKarneCevapTxtInfo>();
            foreach (DataRow k in dt.Rows)
            {
                karne.Add(new CkKarneCevapTxtInfo(Convert.ToInt32(k["Id"]), Convert.ToInt32(k["OpaqId"]),Convert.ToInt32(k["SinavId"]), k["KitapcikTuru"].ToString(), Convert.ToInt32(k["CevapTipi"]), Convert.ToInt32(k["KatilimDurumu"]), 
                    k["Cevaplar"].ToString(), Convert.ToInt32(k["BransId"])));
            }
            return karne;
        }
        public CkKarneCevapTxtInfo KayitBilgiGetir(string cmdText, params MySqlParameter[] param)
        {
            MySqlDataReader dr = helper.ExecuteReader(cmdText, param);
            CkKarneCevapTxtInfo info = new CkKarneCevapTxtInfo();
            while (dr.Read())
            {
                info.Id = dr.GetMySayi("Id");
                info.OpaqId = dr.GetMySayi("OpaqId");
                info.SinavId = dr.GetMySayi("SinavId");
                info.KitapcikTuru = dr.GetMyMetin("KitapcikTuru");
                info.CevapTipi = dr.GetMySayi("CevapTipi");
                info.KatilimDurumu = dr.GetMySayi("KatilimDurumu");
                info.Cevaplar = dr.GetMyMetin("Cevaplar");
                info.BransId = dr.GetMySayi("BransId");
            }
            dr.Close();

            return info;
        }

        public CkKarneCevapTxtInfo KayitBilgiGetir(int id)
        {
            string cmdText = "select * from ckkarnecevaptxt where Id=?Id";
            MySqlParameter param = new MySqlParameter("?Id", MySqlDbType.Int32) { Value = id };
            MySqlDataReader dr = helper.ExecuteReader(cmdText, param);
            CkKarneCevapTxtInfo info = new CkKarneCevapTxtInfo();
            while (dr.Read())
            {
                info.Id = dr.GetMySayi("Id");
                info.OpaqId = dr.GetMySayi("OpaqId");
                info.SinavId = dr.GetMySayi("SinavId");
                info.KitapcikTuru = dr.GetMyMetin("KitapcikTuru");
                info.CevapTipi = dr.GetMySayi("CevapTipi");
                info.KatilimDurumu = dr.GetMySayi("KatilimDurumu");
                info.Cevaplar = dr.GetMyMetin("Cevaplar");
                info.BransId = dr.GetMySayi("BransId");
            }
            dr.Close();

            return info;
        }

        public void KayitSil(int id)
        {
            const string sql = "delete from ckkarnecevaptxt where Id=?Id";
            MySqlParameter p = new MySqlParameter("?Id", MySqlDbType.Int32) { Value = id };
            helper.ExecuteNonQuery(sql, p);
        }

        public void KayitEkle(CkKarneCevapTxtInfo info)
        {
            const string sql = @"insert into ckkarnecevaptxt (OpaqId,SinavId,KitapcikTuru,CevapTipi,KatilimDurumu,Cevaplar,BransId) values (?OpaqId,?SinavId,?KitapcikTuru,?CevapTipi,?KatilimDurumu,?Cevaplar,?BransId)";
            MySqlParameter[] pars =
            {
                new MySqlParameter("?OpaqId", MySqlDbType.Int32),
                new MySqlParameter("?SinavId", MySqlDbType.Int32),
                new MySqlParameter("?KitapcikTuru", MySqlDbType.String),
                new MySqlParameter("?CevapTipi", MySqlDbType.Int32),
                new MySqlParameter("?KatilimDurumu", MySqlDbType.Int32),
                new MySqlParameter("?Cevaplar", MySqlDbType.String),
                new MySqlParameter("?BransId", MySqlDbType.Int32),
            };
            pars[0].Value = info.OpaqId;
            pars[1].Value = info.SinavId;
            pars[2].Value = info.KitapcikTuru;
            pars[3].Value = info.CevapTipi;
            pars[4].Value = info.KatilimDurumu;
            pars[5].Value = info.Cevaplar;
            pars[6].Value = info.BransId;
            helper.ExecuteNonQuery(sql, pars);
        }

        public void KayitGuncelle(CkKarneCevapTxtInfo info)
        {
            const string sql = @"update ckkarnecevaptxt set OpaqId=?OpaqId,SinavId=?SinavId,KitapcikTuru=?KitapcikTuru,CevapTipi=?CevapTipi,KatilimDurumu=?KatilimDurumu,Cevaplar=?Cevaplar,BransId=?BransId where Id=?Id";
            MySqlParameter[] pars =
            {
                new MySqlParameter("?OpaqId", MySqlDbType.Int32),
                new MySqlParameter("?SinavId", MySqlDbType.Int32),
                new MySqlParameter("?KitapcikTuru", MySqlDbType.String),
                new MySqlParameter("?CevapTipi", MySqlDbType.Int32),
                new MySqlParameter("?KatilimDurumu", MySqlDbType.Int32),
                new MySqlParameter("?Cevaplar", MySqlDbType.String),
                new MySqlParameter("?BransId", MySqlDbType.Int32),
                new MySqlParameter("?Id", MySqlDbType.Int32),
            };
            pars[0].Value = info.OpaqId;
            pars[1].Value = info.SinavId;
            pars[2].Value = info.KitapcikTuru;
            pars[3].Value = info.CevapTipi;
            pars[4].Value = info.KatilimDurumu;
            pars[5].Value = info.Cevaplar;
            pars[6].Value = info.BransId;
            pars[7].Value = info.Id;
            helper.ExecuteNonQuery(sql, pars);
        }
    }
