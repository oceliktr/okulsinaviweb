using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using MySql.Data.MySqlClient;

namespace DAL
{
    public class SonucAuInfo
    {
        public int Id { get; set; }
        public int SinavId { get; set; }
        public int Oturum { get; set; }
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
        public int BosUD { get; set; }
        public int SinavNotu { get; set; }
        public int UstDegerlendirici { get; set; }
        public int UstDegerlendirildi { get; set; }
        public int UdPuani { get; set; }
        public string Dosya { get; set; }

        public SonucAuInfo() { }
        public SonucAuInfo(int id, int soruNo, int bransId, int degerlendiriciA, int degerlendiriciB)
        {
            Id = id;
            SoruNo = soruNo;
            BransId = bransId;
            DegerlendiriciA = degerlendiriciA;
            DegerlendiriciB = degerlendiriciB;
        }
        public SonucAuInfo(int bransId)
        {
            BransId = bransId;
        }
    }
    public class SonucAuDB
    {
        readonly HelperDb helper = new HelperDb();
        public DataTable KayitlariGetir(int sinavId)
        {
            const string sql = "select * from sonucau where SinavId=?SinavId order by Id asc";
            MySqlParameter p = new MySqlParameter("SinavId",MySqlDbType.Int32){Value = sinavId};
            return helper.ExecuteDataSet(sql,p).Tables[0];
        }
        public DataTable OgrenciNotlariniGetir(int sinavId, int ogrenciId)
        {
            string cmdText = "select * from sonucau where OgrenciId=?OgrenciId and SinavId=?SinavId order by SoruNo asc";
            MySqlParameter[] param =
            {
                new MySqlParameter("?SinavId", MySqlDbType.Int32),
                new MySqlParameter("?OgrenciId", MySqlDbType.Int32) 
            };
            param[0].Value = sinavId;
            param[1].Value = ogrenciId;
            return helper.ExecuteDataSet(cmdText, param).Tables[0];
        }
        public List<SonucAuInfo> KayitlariDiziyeGetir(int sinavId,int bransi)
        {
            string sql = "select * from sonucau where SinavId=?SinavId and BransId=?BransId order by Soruno";
            MySqlParameter[] p =
            {
                new MySqlParameter("?SinavId", MySqlDbType.Int32),
                new MySqlParameter("?BransId", MySqlDbType.Int32)
            };
            p[0].Value = sinavId;
            p[1].Value = bransi;

            DataTable veriler = helper.ExecuteDataSet(sql, p).Tables[0];

            return (from DataRow row in veriler.Rows select new SonucAuInfo(Convert.ToInt32(row["Id"]), Convert.ToInt32(row["SoruNo"]), Convert.ToInt32(row["BransId"]), Convert.ToInt32(row["DegerlendiriciA"]), Convert.ToInt32(row["DegerlendiriciB"]))).ToList();
        }
        public SonucAuInfo KayitBilgiGetir(string cmdText, params MySqlParameter[] param)
        {
            MySqlDataReader dr = helper.ExecuteReader(cmdText, param);
            SonucAuInfo info = TabloAlanlar(dr);

            return info;
        }
        private static SonucAuInfo TabloAlanlar(MySqlDataReader dr)
        {
            SonucAuInfo info = new SonucAuInfo();
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
                info.BosUD = dr.GetMySayi("BosUD");
                info.SinavNotu = dr.GetMySayi("SinavNotu");
                info.UstDegerlendirici = dr.GetMySayi("UstDegerlendirici");
                info.UstDegerlendirildi = dr.GetMySayi("UstDegerlendirildi");
                info.UdPuani = dr.GetMySayi("UDPuani");
                info.Dosya = dr.GetMyMetin("Dosya");
            }
            dr.Close();
            return info;
        }
        public SonucAuInfo KayitBilgiGetir(int id)
        {
            string cmdText = "select * from sonucau where Id=?Id";
            MySqlParameter param = new MySqlParameter("?Id", MySqlDbType.Int32) { Value = id };
            MySqlDataReader dr = helper.ExecuteReader(cmdText, param);
            SonucAuInfo info = TabloAlanlar(dr);

            return info;
        }
        public SonucAuInfo KayitBilgiGetir(int sinavId, int soruId)
        {
            string cmdText = "select * from sonucau where SoruNo=?SoruNo and SinavId=?SinavId";
            MySqlParameter[] param =
            {
                new MySqlParameter("?SinavId", MySqlDbType.Int32) ,
                new MySqlParameter("?SoruNo", MySqlDbType.Int32) 
            };
            param[0].Value = sinavId;
            param[1].Value = soruId;
            MySqlDataReader dr = helper.ExecuteReader(cmdText, param);
            SonucAuInfo info = TabloAlanlar(dr);

            return info;
        }
        public SonucAuInfo AuSinavNotu(int sinavId, int bransId, int ogrenciId, int soruNo)
        {
            const string sql = "SELECT * from sonucau where SinavId=?SinavId and OgrenciId=?OgrenciId and BransId=?BransId and SoruNo=?SoruNo";
            MySqlParameter[] pars =
            {
                new MySqlParameter("?SinavId", MySqlDbType.Int32),
                new MySqlParameter("?OgrenciId", MySqlDbType.Int32),
                new MySqlParameter("?BransId", MySqlDbType.Int32),
                new MySqlParameter("?SoruNo", MySqlDbType.Int32)
            };
            pars[0].Value = sinavId;
            pars[1].Value = ogrenciId;
            pars[2].Value = bransId;
            pars[3].Value = soruNo;
            MySqlDataReader dr = helper.ExecuteReader(sql, pars);
            SonucAuInfo info = TabloAlanlar(dr);
            return info;
        }
        public SonucAuInfo KayitBilgiGetirDegerlendirici(int sinavId, int uyeId,string grup)
        {
            string cmdText = string.Format("select * from sonucau where Degerlendirici{0}=?Degerlendirici{0} and SinavId=?SinavId and Degerlendirdi{0}=0 order by SoruNo asc",grup);
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
            SonucAuInfo info = TabloAlanlar(dr);

            return info;
        }
        public SonucAuInfo KayitBilgiGetirUstDegerlendirici(int sinavId, int bransId)
        {
            string cmdText = "select * from sonucau where SinavId=?SinavId and BransId=?BransId and UstDegerlendirici=1 and UstDegerlendirildi=0 order by SoruNo asc";
            MySqlParameter[] param =
            {
                new MySqlParameter("?SinavId", MySqlDbType.Int32) ,
                new MySqlParameter("?BransId", MySqlDbType.Int32)
            };
            param[0].Value = sinavId;
            param[1].Value = bransId;
            MySqlDataReader dr = helper.ExecuteReader(cmdText, param);
            SonucAuInfo info = TabloAlanlar(dr);

            return info;
        }
        public void KayitSil(int id)
        {
            const string sql = "delete from sonucau where Id=?Id";
            MySqlParameter p = new MySqlParameter("?Id", MySqlDbType.Int32) { Value = id };
            helper.ExecuteNonQuery(sql, p);
        }
        public void CevaplariSil(int sinavId)
        {
            const string sql = "delete from sonucau where SinavId=?SinavId";
            MySqlParameter p = new MySqlParameter("?SinavId", MySqlDbType.Int32) { Value = sinavId };
            helper.ExecuteNonQuery(sql, p);
        }
        public void CevaplariSil(int sinavId,int oturum,int ogrenciId)
        {
            const string sql = "delete from sonucau where SinavId=?SinavId and Oturum=?Oturum and OgrenciId=?OgrenciId";
            MySqlParameter[] pars =
            {
                new MySqlParameter("?SinavId", MySqlDbType.Int32),
                new MySqlParameter("?Oturum", MySqlDbType.Int32),
                new MySqlParameter("?OgrenciId", MySqlDbType.Int32)
            };
            pars[0].Value = sinavId;
            pars[1].Value = oturum;
            pars[2].Value = ogrenciId;
            helper.ExecuteNonQuery(sql, pars);
        }
        public void KayitEkle(SonucAuInfo info)
        {
            const string sql = @"insert into sonucau (SinavId,OgrenciId,SoruNo,BransId,Dosya,Oturum) values (?SinavId,?OgrenciId,?SoruNo,?BransId,?Dosya,?Oturum)";
            MySqlParameter[] pars = 
            {
                 new MySqlParameter("?SinavId", MySqlDbType.Int32),
                 new MySqlParameter("?OgrenciId", MySqlDbType.Int32),
                 new MySqlParameter("?SoruNo", MySqlDbType.Int32),
                 new MySqlParameter("?BransId", MySqlDbType.Int32),
                 new MySqlParameter("?Dosya", MySqlDbType.String),
                 new MySqlParameter("?BransId", MySqlDbType.Int32)
            };
            pars[0].Value = info.SinavId;
            pars[1].Value = info.OgrenciId;
            pars[2].Value = info.SoruNo;
            pars[3].Value = info.BransId;
            pars[4].Value = info.Dosya;
            pars[5].Value = info.Oturum;
            helper.ExecuteNonQuery(sql, pars);
        }
        public void KayitGuncelle(int soruId, int degerlendiriciA)
        {
            const string sql = @"update sonucau set DegerlendiriciA=?DegerlendiriciA where Id=?Id";
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
            const string sql = @"update sonucau set DegerlendiriciB=?DegerlendiriciB where Id=?Id";
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
            string cmdText = "select count(Id) from sonucau where SinavId=?SinavId";
            MySqlParameter p = new MySqlParameter("?SinavId", MySqlDbType.Int32) {Value = sinavId};
            int sonuc = Convert.ToInt32(helper.ExecuteScalar(cmdText, p));
            return sonuc;
        }
        public int CevaplanacakCkSayisiA(int sinavId, int ogretmenId)
        {
            string cmdText = "select count(Id) from sonucau where SinavId=?SinavId and (DegerlendiriciA=?OgretmenId)";
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
            string cmdText = "select count(Id) from sonucau where SinavId=?SinavId and (DegerlendiriciB=?OgretmenId)";
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
            string cmdText = grup=="A" ? "select count(Id) from sonucau where SinavId=?SinavId and DegerlendirdiA=1" : "select count(Id) from sonucau where SinavId=?SinavId and DegerlendirdiB=1";
            MySqlParameter p = new MySqlParameter("?SinavId", MySqlDbType.Int32) {Value = sinavId};
            int sonuc = Convert.ToInt32(helper.ExecuteScalar(cmdText, p));
            return sonuc;
        }     
        public int CevaplananCkSayisiA(int sinavId, int ogretmenId)
        {
            string cmdText = "select count(Id) from sonucau where SinavId=?SinavId and DegerlendiriciA=?OgretmenId and DegerlendirdiA=1";
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
            string cmdText = "select count(Id) from sonucau where SinavId=?SinavId and DegerlendiriciB=?OgretmenId and DegerlendirdiB=1";
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
            string cmdText = "select count(Id) from sonucau where SinavId=?SinavId and BransId=?BransId and  UstDegerlendirici=1";
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
            string cmdText = "select count(Id) from sonucau where SinavId=?SinavId and UstDegerlendirici=1";
            MySqlParameter p = new MySqlParameter("?SinavId", MySqlDbType.Int32) { Value = sinavId };
            int sonuc = Convert.ToInt32(helper.ExecuteScalar(cmdText, p));
            return sonuc;
        }
        public int UstPCevaplananCKSayisi(int sinavId, int bransId)
        {
            string cmdText = "select count(Id) from sonucau where SinavId=?SinavId and BransId=?BransId and UstDegerlendirildi=1";
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
            string cmdText = "select count(Id) from sonucau where SinavId=?SinavId and UstDegerlendirildi=1";
            MySqlParameter p = new MySqlParameter("?SinavId", MySqlDbType.Int32) { Value = sinavId };
            int sonuc = Convert.ToInt32(helper.ExecuteScalar(cmdText, p));
            return sonuc;
        }
        public int SoruAdet(int sinavId, int bransId)
        {
            string cmdText = "select count(DISTINCT(SoruNo)) from sonucau where SinavId=?SinavId and BransId=?BransId";
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
                string cmdText = "select sum(SinavNotu) as SinavNotu from sonucau where SinavId=?SinavId and OgrenciId=?OgrenciId";
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
                const string sql = "SELECT SUM(sonucau.SinavNotu) from sonucau where sonucau.SinavId=?SinavId and sonucau.OgrenciId=?OgrenciId and sonucau.BransId=?BransId";
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
        public List<SonucAuInfo> DersleriDiziyeGetir(int sinavId)
        {
            const string sql = "select DISTINCT(BransId) from sonucau where SinavId=?SinavId";
            MySqlParameter p = new MySqlParameter("?SinavId", MySqlDbType.Int32) {Value = sinavId};
           DataTable veriler = helper.ExecuteDataSet(sql,p).Tables[0];

            return (from DataRow row in veriler.Rows select new SonucAuInfo(Convert.ToInt32(row["BransId"]))).ToList();
        }
        public bool KayitKontrol(int sinavId)
        {
            string cmdText = "select count(Id) from sonucau where SinavId=?SinavId";
            MySqlParameter p = new MySqlParameter("?SinavId", MySqlDbType.Int32) {Value = sinavId};
            bool sonuc = Convert.ToInt32(helper.ExecuteScalar(cmdText, p)) > 0;
            return sonuc;
        }
        public void UdDegerlendirdi(int id,int puan, int bos)
        {
            const string sql = @"update sonucau set UstDegerlendirildi=?UstDegerlendirildi, UDPuani=?UDPuani,BosUD=?BosUD,SinavNotu=?SinavNotu where Id=?Id";
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
            const string sql = @"update sonucau set DegerlendirdiA=?DegerlendirdiA, DegerlendiriciAPuani=?DegerlendiriciAPuani,BosA=?BosA,SinavNotu=?SinavNotu,UstDegerlendirici=?UstDegerlendirici where Id=?Id";
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
            const string sql = @"update sonucau set DegerlendirdiB=?DegerlendirdiB, DegerlendiriciBPuani=?DegerlendiriciBPuani,BosB=?BosB,SinavNotu=?SinavNotu,UstDegerlendirici=?UstDegerlendirici where Id=?Id";
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
        public int Hesapla(int sinavId, int ogrId)
        {
            string cmdText = @"select Sum(sonucau.SinavNotu) from sonucau
                            where SinavId = ?SinavId and OgrenciId=?OgrenciId";
            MySqlParameter[] pars =
            {
                new MySqlParameter("?SinavId", MySqlDbType.Int32),
                new MySqlParameter("?OgrenciId", MySqlDbType.Int32)
            };
            pars[0].Value = sinavId;
            pars[1].Value = ogrId;
            int sonuc = 0;
            if (string.IsNullOrEmpty(helper.ExecuteScalar(cmdText, pars).ToString()))
                sonuc = 0;
                    else
             sonuc = Convert.ToInt32(helper.ExecuteScalar(cmdText, pars));
            return sonuc;
        }
        public int DogruYanlisCevaplar(int sinavId, int kurumKodu, int soruNo, bool dogru,int sinif, string sube)
        {
            string cmdText = @"select Count(sonucau.Id) from sonucau
                            Inner Join ogrenciler on ogrenciler.OgrenciId = sonucau.OgrenciId
                            where sonucau.SinavId = ?SinavId and ((sonucau.BosA+sonucau.BosB+sonucau.BosUD)<>2)
                            and sonucau.SoruNo = ?SoruNo and ogrenciler.KurumKodu = ?KurumKodu and ogrenciler.Sinifi=?Sinifi and ogrenciler.Sube=?Sube";
            cmdText += dogru ? " and sonucau.SinavNotu <> 0" : " and sonucau.SinavNotu = 0";
             MySqlParameter[] pars =
            {
                new MySqlParameter("?SinavId", MySqlDbType.Int32),
                new MySqlParameter("?KurumKodu", MySqlDbType.Int32),
                new MySqlParameter("?SoruNo", MySqlDbType.Int32),
                new MySqlParameter("?Sinifi", MySqlDbType.String),
                new MySqlParameter("?Sube", MySqlDbType.String)
            };
            pars[0].Value = sinavId;
            pars[1].Value = kurumKodu;
            pars[2].Value = soruNo;
            pars[3].Value = sinif;
            pars[4].Value = sube;
            int sonuc = Convert.ToInt32(helper.ExecuteScalar(cmdText, pars));
            return sonuc;
        }
        public int DogruYanlisCevaplar(int sinavId, int kurumKodu, int soruNo,bool dogru)
        {
            string cmdText = @"select Count(sonucau.Id) from sonucau
                            Inner Join ogrenciler on ogrenciler.OgrenciId = sonucau.OgrenciId
                            where sonucau.SinavId = ?SinavId and ((sonucau.BosA+sonucau.BosB+sonucau.BosUD)<>2)
                            and sonucau.SoruNo = ?SoruNo and ogrenciler.KurumKodu = ?KurumKodu";
            cmdText += dogru ? " and sonucau.SinavNotu <> 0" : " and sonucau.SinavNotu = 0";
             MySqlParameter[] pars =
            {
                new MySqlParameter("?SinavId", MySqlDbType.Int32),
                new MySqlParameter("?KurumKodu", MySqlDbType.Int32),
                new MySqlParameter("?SoruNo", MySqlDbType.Int32)
            };
            pars[0].Value = sinavId;
            pars[1].Value = kurumKodu;
            pars[2].Value = soruNo;
            int sonuc = Convert.ToInt32(helper.ExecuteScalar(cmdText, pars));
            return sonuc;
        }
        public int BosCevaplar(int sinavId, int kurumKodu,int soruNo)
        {
            string cmdText = @"select Count(sonucau.Id) from sonucau
                            Inner Join ogrenciler on ogrenciler.OgrenciId = sonucau.OgrenciId
                            where sonucau.SinavId = ?SinavId and ((sonucau.BosA+sonucau.BosB+sonucau.BosUD)=2)
                            and sonucau.SoruNo = ?SoruNo and ogrenciler.KurumKodu = ?KurumKodu";
             MySqlParameter[] pars =
            {
                new MySqlParameter("?SinavId", MySqlDbType.Int32),
                new MySqlParameter("?KurumKodu", MySqlDbType.Int32),
                new MySqlParameter("?SoruNo", MySqlDbType.Int32)
            };

            pars[0].Value = sinavId;
            pars[1].Value = kurumKodu;
            pars[2].Value = soruNo;
            int sonuc = Convert.ToInt32(helper.ExecuteScalar(cmdText, pars));
            return sonuc;
        }
        public int BosCevaplar(int sinavId, int kurumKodu, int soruNo,int sinif, string sube)
        {
            string cmdText = @"select Count(sonucau.Id) from sonucau
                            Inner Join ogrenciler on ogrenciler.OgrenciId = sonucau.OgrenciId
                            where sonucau.SinavId = ?SinavId and ((sonucau.BosA+sonucau.BosB+sonucau.BosUD)=2)
                            and sonucau.SoruNo = ?SoruNo and ogrenciler.KurumKodu = ?KurumKodu and ogrenciler.Sinifi=?Sinifi and ogrenciler.Sube=?Sube";
            MySqlParameter[] pars =
            {
                new MySqlParameter("?SinavId", MySqlDbType.Int32),
                new MySqlParameter("?KurumKodu", MySqlDbType.Int32),
                new MySqlParameter("?SoruNo", MySqlDbType.Int32),
                new MySqlParameter("?Sinifi", MySqlDbType.String),
                new MySqlParameter("?Sube", MySqlDbType.String)
            };

            pars[0].Value = sinavId;
            pars[1].Value = kurumKodu;
            pars[2].Value = soruNo;
            pars[3].Value = sinif;
            pars[4].Value = sube;
            int sonuc = Convert.ToInt32(helper.ExecuteScalar(cmdText, pars));
            return sonuc;
        }
    }
}