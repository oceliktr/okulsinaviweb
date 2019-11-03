using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;

namespace DAL
{
    public class KitapcikCevapInfo
    {
        public int Id { get; set; }
        public int BransId { get; set; }
        public int SinavId { get; set; }
        public int Oturum { get; set; }
        public int SoruNo { get; set; }
        public int SoruPuani { get; set; }
        public string KitapcikA { get; set; }
        public string KitapcikB { get; set; }
        public string BransAdi { get; set; }

        public KitapcikCevapInfo()
        {
            
        }
        public KitapcikCevapInfo(int bransId, string bransAdi)
        {
            BransId = bransId;
            BransAdi = bransAdi;
        }
        public KitapcikCevapInfo(int bransId,int sinavId,int soruNo,string kitapcikA,string kitapcikB)
        {
            BransId = bransId;
            SinavId = sinavId;
            SoruNo = soruNo;
            KitapcikA = kitapcikA;
            KitapcikB = kitapcikB;
        }
    }

    public class KitapcikCevapDB
    {
        readonly HelperDb helper = new HelperDb();
        public DataTable KayitlariGetir()
        {
            const string sql = "select * from kitapcikcevap";
            return helper.ExecuteDataSet(sql).Tables[0];
        }
        private static KitapcikCevapInfo TabloAlanlar(MySqlDataReader dr)
        {
            KitapcikCevapInfo info = new KitapcikCevapInfo();
            while (dr.Read())
            {
                info.BransId = dr.GetMySayi("BransId");
                info.Id = dr.GetMySayi("Id");
                info.KitapcikA = dr.GetMyMetin("KitapcikA");
                info.Oturum = dr.GetMySayi("Oturum");
                info.KitapcikB = dr.GetMyMetin("KitapcikB");
                info.SinavId = dr.GetMySayi("SinavId");
                info.SoruNo = dr.GetMySayi("SoruNo");
                info.SoruPuani = dr.GetMySayi("SoruPuani");
            }
            dr.Close();

            return info;
        }
        public KitapcikCevapInfo KayitBilgiGetir(int sinavId, int oturum,int bransId, int soruNo)
        {
            const string cmdText = "select * from kitapcikcevap where SinavId=?SinavId and Oturum=?Oturum and BransId=?BransId and SoruNo=?SoruNo";
            MySqlParameter[] param =
            {
                new MySqlParameter("?SinavId", MySqlDbType.Int32),
                new MySqlParameter("?Oturum", MySqlDbType.Int32),
                new MySqlParameter("?BransId", MySqlDbType.Int32),
                new MySqlParameter("?SoruNo", MySqlDbType.Int32)
            };
            param[0].Value = sinavId;
            param[1].Value = oturum;
            param[2].Value = bransId;
            param[3].Value = soruNo;

            MySqlDataReader dr = helper.ExecuteReader(cmdText, param);

            return TabloAlanlar(dr);
        }
        public List<KitapcikCevapInfo> SinavdakiBranslar(int sinavId)
        {
            const string sql = @"Select DISTINCT kitapcikcevap.BransId,branslar.BransAdi from kitapcikcevap
                                INNER JOIN branslar ON kitapcikcevap.BransId = branslar.Id
                                 where kitapcikcevap.SinavId = ?SinavId order by kitapcikcevap.BransId asc";

            MySqlParameter param = new MySqlParameter("?SinavId", MySqlDbType.Int32) { Value = sinavId };

            DataTable veriler = helper.ExecuteDataSet(sql, param).Tables[0];

            return (from DataRow row in veriler.Rows select new KitapcikCevapInfo(Convert.ToInt32(row["BransId"]), row["BransAdi"].ToString())).ToList();

        }
        public List<KitapcikCevapInfo> SinavdakiSoruNolar(int sinavId, int sinif, int brans)
        {
            const string sql = @"Select kitapcikcevap.* from kitapcikcevap
                                    where kitapcikcevap.SinavId =?SinavId and kitapcikcevap.Sinif=?Sinif and kitapcikcevap.BransId=?BransId order by kitapcikcevap.SoruNo asc";

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
           
            return (from DataRow row in veriler.Rows select new KitapcikCevapInfo(Convert.ToInt32(row["BransId"]), Convert.ToInt32(row["SinavId"]), Convert.ToInt32(row["SoruNo"]), row["KitapcikA"].ToString(), row["KitapcikB"].ToString())).ToList();

        }
    }
}
