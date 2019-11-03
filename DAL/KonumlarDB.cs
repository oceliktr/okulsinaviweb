using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using MySql.Data.MySqlClient;

namespace DAL
{
    public class KonumlarInfo
    {
        public int Id { get; set; }
        public int SinavId { get; set; }
        public int SoruNo { get; set; }
        public int BransId { get; set; }
        public int X1 { get; set; }
        public int Y1 { get; set; }
        public int W { get; set; }
        public int H { get; set; }
        public int Oturum { get; set; }
        public string Secenek { get; set; }
        public string Grup { get; set; }
        public KonumlarInfo()
        { }

        public KonumlarInfo(int id, int sinavId, int soruNo, int bransId, int x1, int y1, int w, int h, int oturum, string secenek, string grup)
        {
            Id = id;
            SinavId = sinavId;
            SoruNo = soruNo;
            BransId = bransId;
            X1 = x1;
            Y1 = y1;
            W = w;
            H = h;
            Oturum = oturum;
            Secenek = secenek;
            Grup = grup;
        }
        public KonumlarInfo(int bransId, int oturum)
        {
            BransId = bransId;
            Oturum = oturum;
        }
        public KonumlarInfo(int bransId)
        {
            BransId = bransId;
        }
    }

    public class KonumlarDB
    {
        readonly HelperDb helper = new HelperDb();
        public DataTable KayitlariGetir()
        {
            const string sql = "select * from konumlar order by Id asc";
            return helper.ExecuteDataSet(sql).Tables[0];
        }
        public DataTable KayitlariGetir(int sinavId,int oturum, string grup)
        {
            string sql = "select konumlar.SinavId,konumlar.Oturum,branslar.BransAdi,konumlar.SoruNo,konumlar.Id,konumlar.Secenek from konumlar,branslar where konumlar.BransId=branslar.Id and konumlar.SinavId=?SinavId and konumlar.Grup=?Grup and konumlar.Oturum=?Oturum Order by konumlar.SoruNo,konumlar.Secenek";
            MySqlParameter[] pars =
            {
                new MySqlParameter("?SinavId", MySqlDbType.Int32),
                new MySqlParameter("?Oturum", MySqlDbType.Int32),
                new MySqlParameter("?Grup", MySqlDbType.String)
            };
            pars[0].Value = sinavId;
            pars[1].Value = oturum;
            pars[2].Value = grup;
            return helper.ExecuteDataSet(sql, pars).Tables[0];
        }
        public List<KonumlarInfo> KayitlariDiziyeGetir(int sinavId)
        {
            string sql = "select * from konumlar where SinavId=?SinavId";
            MySqlParameter[] p =
            {
                new MySqlParameter("?SinavId", MySqlDbType.Int32)
            };
            p[0].Value = sinavId;

            DataTable veriler = helper.ExecuteDataSet(sql, p).Tables[0];
            return (from DataRow row in veriler.Rows select new KonumlarInfo(Convert.ToInt32(row["Id"]), Convert.ToInt32(row["SinavId"]), Convert.ToInt32(row["SoruNo"]), Convert.ToInt32(row["BransId"]), Convert.ToInt32(row["X1"]), Convert.ToInt32(row["Y1"]), Convert.ToInt32(row["W"]), Convert.ToInt32(row["H"]), Convert.ToInt32(row["Oturum"]), row["Secenek"].ToString(), row["Grup"].ToString())).ToList();
        }
        public List<KonumlarInfo> BranslariGetir(int sinavId)
        {
            string sql = "select DISTINCT(BransId) from konumlar where konumlar.SinavId=?SinavId and (Grup='au' or Grup='optik') order by konumlar.Oturum asc";
            MySqlParameter p = new MySqlParameter("?SinavId", MySqlDbType.Int32) { Value = sinavId };

            DataTable veriler = helper.ExecuteDataSet(sql, p).Tables[0];
            return (from DataRow row in veriler.Rows select new KonumlarInfo(Convert.ToInt32(row["BransId"]))).ToList();
        }
        public List<KonumlarInfo> BranslariGetir(int sinavId, int oturum)
        {
            string sql = "select DISTINCT(BransId) from konumlar where konumlar.SinavId=?SinavId and konumlar.Oturum=?Oturum and (Grup='au' or Grup='optik') order by konumlar.Oturum asc";
            MySqlParameter[] p =
            {
                new MySqlParameter("?SinavId", MySqlDbType.Int32),
                new MySqlParameter("?Oturum", MySqlDbType.Int32),
            };
            p[0].Value = sinavId;
            p[1].Value = oturum;
            DataTable veriler = helper.ExecuteDataSet(sql, p).Tables[0];
            return (from DataRow row in veriler.Rows select new KonumlarInfo(Convert.ToInt32(row["BransId"]))).ToList();
        }
        public List<KonumlarInfo> OturumlariGetir(int sinavId)
        {
            string sql = "select DISTINCT(Oturum),konumlar.BransId from konumlar where konumlar.SinavId=?SinavId  and (Grup='au' or Grup='optik') order by konumlar.Oturum asc";
            MySqlParameter p = new MySqlParameter("?SinavId", MySqlDbType.Int32) {Value = sinavId};

            DataTable veriler = helper.ExecuteDataSet(sql, p).Tables[0];
            return (from DataRow row in veriler.Rows select new KonumlarInfo(Convert.ToInt32(row["BransId"]),  Convert.ToInt32(row["Oturum"]))).ToList();
        }
        public List<KonumlarInfo> OptikFormdakiSoruSayisi(int sinavId, int oturum, int brans)
        {
            List<KonumlarInfo> knm = KayitlariDiziyeGetir(sinavId);
            //opitk formdaki sorular
            // knm nesnesinde sýnavId filitrelendiði için alttaki listede filitrelenmedi
            List<KonumlarInfo> sorular = (from x in knm where x.Oturum == oturum && x.BransId == brans && x.Grup == "optik" && x.Secenek == "A" orderby x.SoruNo select x).ToList();

            return sorular;
        }
        public KonumlarInfo KayitBilgiGetir(string cmdText, params MySqlParameter[] param)
        {
            MySqlDataReader dr = helper.ExecuteReader(cmdText, param);
            KonumlarInfo info = TabloAlanlar(dr);

            return info;
        }

        private static KonumlarInfo TabloAlanlar(MySqlDataReader dr)
        {
            KonumlarInfo info = new KonumlarInfo();
            while (dr.Read())
            {
                info.Id = dr.GetMySayi("Id");
                info.SinavId = dr.GetMySayi("SinavId");
                info.SoruNo = dr.GetMySayi("SoruNo");
                info.BransId = dr.GetMySayi("BransId");
                info.X1 = dr.GetMySayi("X1");
                info.Y1 = dr.GetMySayi("Y1");
                info.W = dr.GetMySayi("W");
                info.H = dr.GetMySayi("H");
                info.Oturum = dr.GetMySayi("Oturum");
                info.Secenek = dr.GetMyMetin("Secenek");
                info.Grup = dr.GetMyMetin("Grup");
            }
            dr.Close();
            return info;
        }

        public KonumlarInfo KayitBilgiGetir(int id)
        {
            string cmdText = "select * from konumlar where Id=?Id";
            MySqlParameter param = new MySqlParameter("?Id", MySqlDbType.Int32) { Value = id };
            MySqlDataReader dr = helper.ExecuteReader(cmdText, param);
            KonumlarInfo info = TabloAlanlar(dr);

            return info;
        }

        public void KayitSil(int id)
        {
            const string sql = "delete from konumlar where Id=?Id";
            MySqlParameter p = new MySqlParameter("?Id", MySqlDbType.Int32) { Value = id };
            helper.ExecuteNonQuery(sql, p);
        }
        public void KayitEkle(KonumlarInfo info)
        {
            const string sql = @"insert into konumlar (SinavId,SoruNo,BransId,X1,Y1,W,H,Oturum,Grup,Secenek) values (?SinavId,?SoruNo,?BransId,?X1,?Y1,?W,?H,?Oturum,?Grup,?Secenek)";
            MySqlParameter[] pars =
            {
                new MySqlParameter("?SinavId", MySqlDbType.Int32),
                new MySqlParameter("?SoruNo", MySqlDbType.Int32),
                new MySqlParameter("?BransId", MySqlDbType.Int32),
                new MySqlParameter("?X1", MySqlDbType.Int32),
                new MySqlParameter("?Y1", MySqlDbType.Int32),
                new MySqlParameter("?W", MySqlDbType.Int32),
                new MySqlParameter("?H", MySqlDbType.Int32),
                new MySqlParameter("?Oturum", MySqlDbType.Int32),
                new MySqlParameter("?Grup", MySqlDbType.String),
                new MySqlParameter("?Secenek", MySqlDbType.String)
            };
            pars[0].Value = info.SinavId;
            pars[1].Value = info.SoruNo;
            pars[2].Value = info.BransId;
            pars[3].Value = info.X1;
            pars[4].Value = info.Y1;
            pars[5].Value = info.W;
            pars[6].Value = info.H;
            pars[7].Value = info.Oturum;
            pars[8].Value = info.Grup;
            pars[9].Value = info.Secenek;
            helper.ExecuteNonQuery(sql, pars);
        }
        public void KayitGuncelle(KonumlarInfo info)
        {
            const string sql = @"update konumlar set X1=?X1,Y1=?Y1,W=?W,H=?H where SinavId=?SinavId and SoruNo=?SoruNo and BransId=?BransId and Oturum=?Oturum and Grup=?Grup and Secenek=?Secenek";
            MySqlParameter[] pars =
            {
                new MySqlParameter("?SinavId", MySqlDbType.Int32),
                new MySqlParameter("?SoruNo", MySqlDbType.Int32),
                new MySqlParameter("?BransId", MySqlDbType.Int32),
                new MySqlParameter("?X1", MySqlDbType.Int32),
                new MySqlParameter("?Y1", MySqlDbType.Int32),
                new MySqlParameter("?W", MySqlDbType.Int32),
                new MySqlParameter("?H", MySqlDbType.Int32),
                new MySqlParameter("?Oturum", MySqlDbType.Int32),
                new MySqlParameter("?Grup", MySqlDbType.String),
                new MySqlParameter("?Secenek", MySqlDbType.String)
            };
            pars[0].Value = info.SinavId;
            pars[1].Value = info.SoruNo;
            pars[2].Value = info.BransId;
            pars[3].Value = info.X1;
            pars[4].Value = info.Y1;
            pars[5].Value = info.W;
            pars[6].Value = info.H;
            pars[7].Value = info.Oturum;
            pars[8].Value = info.Grup;
            pars[9].Value = info.Secenek;
            helper.ExecuteNonQuery(sql, pars);
        }
        public bool KayitKontrol(int sinavId, int soruNo, int bransId, int oturum, string secenek, string grup)
        {
            string cmdText = "select count(Id) from konumlar where SinavId=?SinavId and SoruNo=?SoruNo and BransId=?BransId and Oturum=?Oturum and Secenek=?Secenek and Grup=?Grup";
            MySqlParameter[] pars =
            {
                new MySqlParameter("?SinavId", MySqlDbType.Int32),
                new MySqlParameter("?SoruNo", MySqlDbType.Int32),
                new MySqlParameter("?BransId", MySqlDbType.Int32),
                new MySqlParameter("?Oturum", MySqlDbType.Int32),
                new MySqlParameter("?Grup", MySqlDbType.String),
                new MySqlParameter("?Secenek", MySqlDbType.String)
            };
            pars[0].Value = sinavId;
            pars[1].Value = soruNo;
            pars[2].Value = bransId;
            pars[3].Value = oturum;
            pars[4].Value = grup;
            pars[5].Value = secenek;
            bool sonuc = Convert.ToInt32(helper.ExecuteScalar(cmdText, pars)) > 0;
            return sonuc;
        }
        //TODO:BU METOD YENÝDEN DÜZENLENECEK
        public bool DogruCevapSayiKontrol(int sinavId, int soruNo, int bransId, int oturum, string secenek)
        {
            string cmdText = "select count(Id) from konumlar where SinavId=?SinavId and SoruNo=?SoruNo and BransId=?BransId and Oturum=?Oturum and Grup='optik' and Secenek<>?Secenek";
            MySqlParameter[] pars =
            {
                new MySqlParameter("?SinavId", MySqlDbType.Int32),
                new MySqlParameter("?SoruNo", MySqlDbType.Int32),
                new MySqlParameter("?BransId", MySqlDbType.Int32),
                new MySqlParameter("?Oturum", MySqlDbType.Int32),
                new MySqlParameter("?Secenek", MySqlDbType.String)
            };
            pars[0].Value = sinavId;
            pars[1].Value = soruNo;
            pars[2].Value = bransId;
            pars[3].Value = oturum;
            pars[4].Value = secenek;
            bool sonuc = Convert.ToInt32(helper.ExecuteScalar(cmdText, pars)) > 0;
            return sonuc;
        }
    }
}