using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using MySql.Data.MySqlClient;

namespace DAL
{
    public class CevaplarInfo
    {
        public int Id { get; set; }
        public int SinavId { get; set; }
        public int OgrenciId { get; set; }
        public int SoruNo { get; set; }
        public int BransId { get; set; }
        public int DegerlendiriciA { get; set; }
        public int DegerlendiriciB { get; set; }
        public int DegerlendirdiA { get; set; }
        public int DegerlendirdiB { get; set; }
        public int DegerlendiriciAPuani { get; set; }
        public int DegerlendiriciBPuani { get; set; }
        public int BosA { get; set; }
        public int BosB { get; set; }
        public int SinavNotu { get; set; }
        public int UstDegerlendirici { get; set; }
        public int UstDegerlendirildi { get; set; }
        public int UdPuani { get; set; }
        public string Dosya { get; set; }

        public CevaplarInfo() { }
        public CevaplarInfo(int id, int soruNo, int bransId, int degerlendiriciA, int degerlendiriciB)
        {
            Id = id;
            SoruNo = soruNo;
            BransId = bransId;
            DegerlendiriciA = degerlendiriciA;
            DegerlendiriciB = degerlendiriciB;
        }
        public CevaplarInfo(int bransId)
        {
            BransId = bransId;
        }
    }
    public class CevaplarDb
    {
        readonly HelperDb helper = new HelperDb();
        public DataTable KayitlariGetir(int sinavId)
        {
            const string sql = "select * from cevaplar where SinavId=?SinavId order by Id asc";
            MySqlParameter p = new MySqlParameter("SinavId",MySqlDbType.Int32){Value = sinavId};
            return helper.ExecuteDataSet(sql,p).Tables[0];
        }
        public DataTable OgrenciNotlariniGetir(int sinavId, int ogrenciId)
        {
            string cmdText = "select * from cevaplar where OgrenciId=?OgrenciId and SinavId=?SinavId order by SoruNo asc";
            MySqlParameter[] param =
            {
                new MySqlParameter("?SinavId", MySqlDbType.Int32),
                new MySqlParameter("?OgrenciId", MySqlDbType.Int32) 
            };
            param[0].Value = sinavId;
            param[1].Value = ogrenciId;
            return helper.ExecuteDataSet(cmdText, param).Tables[0];
        }
        public List<CevaplarInfo> KayitlariDiziyeGetir(int sinavId,int bransi)
        {
            string sql = "select * from cevaplar where SinavId=?SinavId and BransId=?BransId order by Soruno";
            MySqlParameter[] p =
            {
                new MySqlParameter("?SinavId", MySqlDbType.Int32),
                new MySqlParameter("?BransId", MySqlDbType.Int32)
            };
            p[0].Value = sinavId;
            p[1].Value = bransi;

            DataTable veriler = helper.ExecuteDataSet(sql, p).Tables[0];

            return (from DataRow row in veriler.Rows select new CevaplarInfo(Convert.ToInt32(row["Id"]), Convert.ToInt32(row["SoruNo"]), Convert.ToInt32(row["BransId"]), Convert.ToInt32(row["DegerlendiriciA"]), Convert.ToInt32(row["DegerlendiriciB"]))).ToList();
        }
        public CevaplarInfo KayitBilgiGetir(string cmdText, params MySqlParameter[] param)
        {
            MySqlDataReader dr = helper.ExecuteReader(cmdText, param);
            CevaplarInfo info = TabloAlanlar(dr);

            return info;
        }
        private static CevaplarInfo TabloAlanlar(MySqlDataReader dr)
        {
            CevaplarInfo info = new CevaplarInfo();
            while (dr.Read())
            {
                info.Id = dr.GetMySayi("Id");
                info.SinavId = dr.GetMySayi("SinavId");
                info.OgrenciId = dr.GetMySayi("OgrenciId");
                info.SoruNo = dr.GetMySayi("SoruNo");
                info.BransId = dr.GetMySayi("BransId");
                info.DegerlendiriciA = dr.GetMySayi("DegerlendiriciA");
                info.DegerlendiriciB = dr.GetMySayi("DegerlendiriciB");
                info.DegerlendirdiA = dr.GetMySayi("DegerlendirdiA");
                info.DegerlendirdiB = dr.GetMySayi("DegerlendirdiB");
                info.DegerlendiriciAPuani = dr.GetMySayi("DegerlendiriciAPuani");
                info.DegerlendiriciBPuani = dr.GetMySayi("DegerlendiriciBPuani");
                info.BosA = dr.GetMySayi("BosA");
                info.BosB = dr.GetMySayi("BosB");
                info.SinavNotu = dr.GetMySayi("SinavNotu");
                info.UstDegerlendirici = dr.GetMySayi("UstDegerlendirici");
                info.UstDegerlendirildi = dr.GetMySayi("UstDegerlendirildi");
                info.UdPuani = dr.GetMySayi("UDPuani");
                info.Dosya = dr.GetMyMetin("Dosya");
            }
            dr.Close();
            return info;
        }
        public CevaplarInfo KayitBilgiGetir(int id)
        {
            string cmdText = "select * from cevaplar where Id=?Id";
            MySqlParameter param = new MySqlParameter("?Id", MySqlDbType.Int32) { Value = id };
            MySqlDataReader dr = helper.ExecuteReader(cmdText, param);
            CevaplarInfo info = TabloAlanlar(dr);

            return info;
        }
        public CevaplarInfo KayitBilgiGetir(int sinavId, int soruId)
        {
            string cmdText = "select * from cevaplar where SoruNo=?SoruNo and SinavId=?SinavId";
            MySqlParameter[] param =
            {
                new MySqlParameter("?SinavId", MySqlDbType.Int32) ,
                new MySqlParameter("?SoruNo", MySqlDbType.Int32) 
            };
            param[0].Value = sinavId;
            param[1].Value = soruId;
            MySqlDataReader dr = helper.ExecuteReader(cmdText, param);
            CevaplarInfo info = TabloAlanlar(dr);

            return info;
        }
        public CevaplarInfo KayitBilgiGetirDegerlendirici(int sinavId, int uyeId,string grup)
        {
            string cmdText = string.Format("select * from cevaplar where Degerlendirici{0}=?Degerlendirici{0} and SinavId=?SinavId and Degerlendirdi{0}=0 order by SoruNo asc",grup);
            MySqlParameter[] param =
            {
                new MySqlParameter("?SinavId", MySqlDbType.Int32) ,
                new MySqlParameter("?DegerlendiriciA", MySqlDbType.Int32), 
                new MySqlParameter("?DegerlendiriciB", MySqlDbType.Int32) 
            };
            param[0].Value = sinavId;
            param[1].Value = uyeId;
            param[2].Value = uyeId;
            MySqlDataReader dr = helper.ExecuteReader(cmdText, param);
            CevaplarInfo info = TabloAlanlar(dr);

            return info;
        }
        public CevaplarInfo KayitBilgiGetirUstDegerlendirici(int sinavId, int bransId)
        {
            string cmdText = "select * from cevaplar where SinavId=?SinavId and BransId=?BransId and UstDegerlendirici=1 and UstDegerlendirildi=0 order by SoruNo asc";
            MySqlParameter[] param =
            {
                new MySqlParameter("?SinavId", MySqlDbType.Int32) ,
                new MySqlParameter("?BransId", MySqlDbType.Int32)
            };
            param[0].Value = sinavId;
            param[1].Value = bransId;
            MySqlDataReader dr = helper.ExecuteReader(cmdText, param);
            CevaplarInfo info = TabloAlanlar(dr);

            return info;
        }
       
        
        public void KayitSil(int id)
        {
            const string sql = "delete from cevaplar where Id=?Id";
            MySqlParameter p = new MySqlParameter("?Id", MySqlDbType.Int32) { Value = id };
            helper.ExecuteNonQuery(sql, p);
        }
        public void CevaplariSil(int sinavId)
        {
            const string sql = "delete from cevaplar where SinavId=?SinavId";
            MySqlParameter p = new MySqlParameter("?SinavId", MySqlDbType.Int32) { Value = sinavId };
            helper.ExecuteNonQuery(sql, p);
        }
        public void KayitEkle(CevaplarInfo info)
        {
            const string sql = @"insert into cevaplar (SinavId,OgrenciId,SoruNo,BransId,Dosya) values (?SinavId,?OgrenciId,?SoruNo,?BransId,?Dosya)";
            MySqlParameter[] pars = 
            {
             new MySqlParameter("?SinavId", MySqlDbType.Int32),
             new MySqlParameter("?OgrenciId", MySqlDbType.Int32),
             new MySqlParameter("?SoruNo", MySqlDbType.Int32),
             new MySqlParameter("?BransId", MySqlDbType.Int32),
             new MySqlParameter("?Dosya", MySqlDbType.String)
            };
            pars[0].Value = info.SinavId;
            pars[1].Value = info.OgrenciId;
            pars[2].Value = info.SoruNo;
            pars[3].Value = info.BransId;
            pars[4].Value = info.Dosya;
            helper.ExecuteNonQuery(sql, pars);
        }
        public void KayitGuncelle(int soruId, int degerlendiriciA)
        {
            const string sql = @"update cevaplar set DegerlendiriciA=?DegerlendiriciA where Id=?Id";
            MySqlParameter[] pars =
            {
             new MySqlParameter("?DegerlendiriciA", MySqlDbType.String),
             new MySqlParameter("?Id", MySqlDbType.Int32)
            };
            pars[0].Value = degerlendiriciA;
            pars[1].Value = soruId;
            helper.ExecuteNonQuery(sql, pars);
        }
        public void KayitGuncelle(int soruId, int degerlendiriciB,int deg2)
        {
            const string sql = @"update cevaplar set DegerlendiriciB=?DegerlendiriciB where Id=?Id";
            MySqlParameter[] pars =
            {
             new MySqlParameter("?DegerlendiriciB", MySqlDbType.String),
             new MySqlParameter("?Id", MySqlDbType.Int32)
            };
            pars[0].Value = degerlendiriciB;
            pars[1].Value = soruId;
            helper.ExecuteNonQuery(sql, pars);
        }
        public int CevaplanacakCkSayisi(int sinavId)
        {
            string cmdText = "select count(Id) from cevaplar where SinavId=?SinavId";
            MySqlParameter p = new MySqlParameter("?SinavId", MySqlDbType.Int32) {Value = sinavId};
            int sonuc = Convert.ToInt32(helper.ExecuteScalar(cmdText, p));
            return sonuc;
        }
        public int CevaplanacakCkSayisiA(int sinavId, int ogretmenId)
        {
            string cmdText = "select count(Id) from cevaplar where SinavId=?SinavId and (DegerlendiriciA=?OgretmenId)";
            MySqlParameter[] pars = 
            {
                new MySqlParameter("?SinavId", MySqlDbType.Int32),
                new MySqlParameter("?OgretmenId", MySqlDbType.Int32)
            };
            pars[0].Value = sinavId;
            pars[1].Value = ogretmenId;
            int sonuc = Convert.ToInt32(helper.ExecuteScalar(cmdText, pars));
            return sonuc;
        }
        public int CevaplanacakCkSayisiB(int sinavId, int ogretmenId)
        {
            string cmdText = "select count(Id) from cevaplar where SinavId=?SinavId and (DegerlendiriciB=?OgretmenId)";
            MySqlParameter[] pars = 
            {
                new MySqlParameter("?SinavId", MySqlDbType.Int32),
                new MySqlParameter("?OgretmenId", MySqlDbType.Int32)
            };
            pars[0].Value = sinavId;
            pars[1].Value = ogretmenId;
            int sonuc = Convert.ToInt32(helper.ExecuteScalar(cmdText, pars));
            return sonuc;
        }
        public int CevaplananCkSayisi(int sinavId,string grup)
        {
            string cmdText = grup=="A" ? "select count(Id) from cevaplar where SinavId=?SinavId and DegerlendirdiA=1" : "select count(Id) from cevaplar where SinavId=?SinavId and DegerlendirdiB=1";
            MySqlParameter p = new MySqlParameter("?SinavId", MySqlDbType.Int32) {Value = sinavId};
            int sonuc = Convert.ToInt32(helper.ExecuteScalar(cmdText, p));
            return sonuc;
        }
        
        public int CevaplananCkSayisiA(int sinavId, int ogretmenId)
        {
            string cmdText = "select count(Id) from cevaplar where SinavId=?SinavId and DegerlendiriciA=?OgretmenId and DegerlendirdiA=1";
            MySqlParameter[] pars = 
            {
                new MySqlParameter("?SinavId", MySqlDbType.Int32),
                new MySqlParameter("?OgretmenId", MySqlDbType.Int32)
            };
            pars[0].Value = sinavId;
            pars[1].Value = ogretmenId;
            int sonuc = Convert.ToInt32(helper.ExecuteScalar(cmdText, pars));
            return sonuc;
        }
        public int CevaplananCkSayisiB(int sinavId, int ogretmenId)
        {
            string cmdText = "select count(Id) from cevaplar where SinavId=?SinavId and DegerlendiriciB=?OgretmenId and DegerlendirdiB=1";
            MySqlParameter[] pars = 
            {
                new MySqlParameter("?SinavId", MySqlDbType.Int32),
                new MySqlParameter("?OgretmenId", MySqlDbType.Int32)
            };
            pars[0].Value = sinavId;
            pars[1].Value = ogretmenId;
            int sonuc = Convert.ToInt32(helper.ExecuteScalar(cmdText, pars));
            return sonuc;
        }
        public int UstPCKSayisi(int sinavId, int bransId)
        {
            string cmdText = "select count(Id) from cevaplar where SinavId=?SinavId and BransId=?BransId and  UstDegerlendirici=1";
            MySqlParameter[] pars = 
            {
                new MySqlParameter("?SinavId", MySqlDbType.Int32),
                new MySqlParameter("?BransId", MySqlDbType.Int32)
            };
            pars[0].Value = sinavId;
            pars[1].Value = bransId;
            int sonuc = Convert.ToInt32(helper.ExecuteScalar(cmdText, pars));
            return sonuc;
        }
        public int UstPCKSayisi(int sinavId)
        {
            string cmdText = "select count(Id) from cevaplar where SinavId=?SinavId and UstDegerlendirici=1";
            MySqlParameter p = new MySqlParameter("?SinavId", MySqlDbType.Int32) { Value = sinavId };
            int sonuc = Convert.ToInt32(helper.ExecuteScalar(cmdText, p));
            return sonuc;
        }
        public int UstPCevaplananCKSayisi(int sinavId, int bransId)
        {
            string cmdText = "select count(Id) from cevaplar where SinavId=?SinavId and BransId=?BransId and UstDegerlendirildi=1";
            MySqlParameter[] pars = 
            {
                new MySqlParameter("?SinavId", MySqlDbType.Int32),
                new MySqlParameter("?BransId", MySqlDbType.Int32)
            };
            pars[0].Value = sinavId;
            pars[1].Value = bransId;
            int sonuc = Convert.ToInt32(helper.ExecuteScalar(cmdText, pars));
            return sonuc;
        }
        public int UstPCevaplananCKSayisi(int sinavId)
        {
            string cmdText = "select count(Id) from cevaplar where SinavId=?SinavId and UstDegerlendirildi=1";
            MySqlParameter p = new MySqlParameter("?SinavId", MySqlDbType.Int32) { Value = sinavId };
            int sonuc = Convert.ToInt32(helper.ExecuteScalar(cmdText, p));
            return sonuc;
        }
        public int SoruAdet(int sinavId, int bransId)
        {
            string cmdText = "select count(DISTINCT(SoruNo)) from cevaplar where SinavId=?SinavId and BransId=?BransId";
            MySqlParameter[] pars = 
            {
                new MySqlParameter("?SinavId", MySqlDbType.Int32),
                new MySqlParameter("?BransId", MySqlDbType.Int32)
            };
            pars[0].Value = sinavId;
            pars[1].Value = bransId;
            int sonuc = Convert.ToInt32(helper.ExecuteScalar(cmdText, pars));
            return sonuc;
        }
        public int SinavNotu(int sinavId,int ogrenciId)
        {
            try
            {
                string cmdText = "select sum(SinavNotu) as SinavNotu from cevaplar where SinavId=?SinavId and OgrenciId=?OgrenciId";
                MySqlParameter[] p =
                {
                    new MySqlParameter("?SinavId", MySqlDbType.Int32),
                    new MySqlParameter("?OgrenciId", MySqlDbType.Int32) 
                };
                p[0].Value = sinavId;
                p[1].Value = ogrenciId;
                int sonuc = Convert.ToInt32(helper.ExecuteScalar(cmdText, p));
                return sonuc;
            }
            catch (Exception)
            {
                return 0;
            }
        }
        public int AuSinavNotu(int sinavId, int bransId, int ogrenciId)
        {
            try
            {
                const string sql = "SELECT SUM(cevaplar.SinavNotu) from cevaplar where cevaplar.SinavId=?SinavId and cevaplar.OgrenciId=?OgrenciId and cevaplar.BransId=?BransId";
                MySqlParameter[] pars =
                {
                new MySqlParameter("?SinavId", MySqlDbType.Int32),
                new MySqlParameter("?OgrenciId", MySqlDbType.Int32),
                new MySqlParameter("?BransId", MySqlDbType.Int32)
            };
                pars[0].Value = sinavId;
                pars[1].Value = ogrenciId;
                pars[2].Value = bransId;
                return Convert.ToInt32(helper.ExecuteScalar(sql, pars));
            }
            catch (Exception)
            {

                return 0;
            }
        }
        public List<CevaplarInfo> DersleriDiziyeGetir(int sinavId)
        {
            const string sql = "select DISTINCT(BransId) from cevaplar where SinavId=?SinavId";
            MySqlParameter p = new MySqlParameter("?SinavId", MySqlDbType.Int32) {Value = sinavId};
           DataTable veriler = helper.ExecuteDataSet(sql,p).Tables[0];

            return (from DataRow row in veriler.Rows select new CevaplarInfo(Convert.ToInt32(row["BransId"]))).ToList();
        }

        public bool KayitKontrol(int sinavId)
        {
            string cmdText = "select count(Id) from cevaplar where SinavId=?SinavId";
            MySqlParameter p = new MySqlParameter("?SinavId", MySqlDbType.Int32) {Value = sinavId};
            bool sonuc = Convert.ToInt32(helper.ExecuteScalar(cmdText, p)) > 0;
            return sonuc;
        }
        public void UdDegerlendirdi(int id,int puan, int bos)
        {
            const string sql = @"update cevaplar set UstDegerlendirildi=?UstDegerlendirildi, UDPuani=?UDPuani,BosUD=?BosUD,SinavNotu=?SinavNotu where Id=?Id";
            MySqlParameter[] pars =
            {
             new MySqlParameter("?UstDegerlendirildi", MySqlDbType.String),
             new MySqlParameter("?UDPuani", MySqlDbType.String),
             new MySqlParameter("?BosUD", MySqlDbType.String),
             new MySqlParameter("?SinavNotu", MySqlDbType.Int32),
             new MySqlParameter("?Id", MySqlDbType.Int32)
            };
            pars[0].Value = 1;
            pars[1].Value = puan;
            pars[2].Value = bos;
            pars[3].Value = puan;
            pars[4].Value = id;
            helper.ExecuteNonQuery(sql, pars);
        }
        
        public void DegerlendirildiA(int id, int degerlendiriciAPuani, int bos, int nihaiPuan, int ustDegerlendirici)
        {
            const string sql = @"update cevaplar set DegerlendirdiA=?DegerlendirdiA, DegerlendiriciAPuani=?DegerlendiriciAPuani,BosA=?BosA,SinavNotu=?SinavNotu,UstDegerlendirici=?UstDegerlendirici where Id=?Id";
            MySqlParameter[] pars =
            {
             new MySqlParameter("?DegerlendirdiA", MySqlDbType.String),
             new MySqlParameter("?DegerlendiriciAPuani", MySqlDbType.String),
             new MySqlParameter("?BosA", MySqlDbType.String),
             new MySqlParameter("?SinavNotu", MySqlDbType.Int32),
             new MySqlParameter("?UstDegerlendirici", MySqlDbType.Int32),
             new MySqlParameter("?Id", MySqlDbType.Int32)
            };
            pars[0].Value = 1;
            pars[1].Value = degerlendiriciAPuani;
            pars[2].Value = bos;
            pars[3].Value = nihaiPuan;
            pars[4].Value = ustDegerlendirici;
            pars[5].Value = id;
            helper.ExecuteNonQuery(sql, pars);
        }
        public void DegerlendirildiB(int id, int degerlendiriciBPuani, int bos, int nihaiPuan, int ustDegerlendirici)
        {
            const string sql = @"update cevaplar set DegerlendirdiB=?DegerlendirdiB, DegerlendiriciBPuani=?DegerlendiriciBPuani,BosB=?BosB,SinavNotu=?SinavNotu,UstDegerlendirici=?UstDegerlendirici where Id=?Id";
            MySqlParameter[] pars =
            {
             new MySqlParameter("?DegerlendirdiB", MySqlDbType.String),
             new MySqlParameter("?DegerlendiriciBPuani", MySqlDbType.String),
             new MySqlParameter("?BosB", MySqlDbType.String),
             new MySqlParameter("?SinavNotu", MySqlDbType.Int32),
             new MySqlParameter("?UstDegerlendirici", MySqlDbType.Int32),
             new MySqlParameter("?Id", MySqlDbType.Int32)
            };
            pars[0].Value = 1;
            pars[1].Value = degerlendiriciBPuani;
            pars[2].Value = bos;
            pars[3].Value = nihaiPuan;
            pars[4].Value = ustDegerlendirici;
            pars[5].Value = id;
            helper.ExecuteNonQuery(sql, pars);
        }  
    }

}