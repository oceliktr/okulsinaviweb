using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using MySql.Data.MySqlClient;

namespace DAL
{
    public class OgrencilerInfo
    {
        public int Id { get; set; }
        public int OgrenciId { get; set; }
        public int SinavId { get; set; }
        public string TcKimlik { get; set; }
        public string Adi { get; set; }
        public string Soyadi { get; set; }
        public int KurumKodu { get; set; }
        public int OgrOkulNo { get; set; }
        public int Sinifi { get; set; }
        public int Girmedi { get; set; }
        public string Sube { get; set; }
        public string CKagitKontrol { get; set; }
        public string DosyaAdi { get; set; }
        public string IlceAdi { get; set; }
        public string KurumAdi { get; set; }
        public OgrencilerInfo()
        { }

        public OgrencilerInfo(int id,int ogrenciId, int sinavId, string adi, string soyadi, int kurumKodu, int okulNo, int sinifi, string sube,int girmedi)
        {
            Id = id;
            OgrenciId = ogrenciId;
            SinavId = sinavId;
            Adi = adi;
            Soyadi = soyadi;
            KurumKodu = kurumKodu;
            OgrOkulNo = okulNo;
            Sinifi = sinifi;
            Sube = sube;
            Girmedi = girmedi;
        }
        public OgrencilerInfo(int ogrenciId,int kurumKodu,string kurumAdi,string tcKimlik, string adi, string soyadi, int okulNo, int sinifi, string sube)
        {
            OgrenciId = ogrenciId;
            KurumAdi = kurumAdi;
            TcKimlik = tcKimlik;
            Adi = adi;
            Soyadi = soyadi;
            KurumKodu = kurumKodu;
            OgrOkulNo = okulNo;
            Sinifi = sinifi;
            Sube = sube;
        }
        public OgrencilerInfo(int id, int ogrenciId, int girmedi, string dosyaAdi)
        {
            Id = id;
            OgrenciId = ogrenciId;
            Girmedi = girmedi;
            DosyaAdi = dosyaAdi;
        }
        public OgrencilerInfo(int sinifi, string sube, int kurumKodu,string ilceAdi,string kurumAdi)
        {
            IlceAdi = ilceAdi;
            KurumKodu = kurumKodu;
            KurumAdi = kurumAdi;
            Sinifi = sinifi;
            Sube = sube;
        }

    }

    public class OgrencilerDb
    {
        readonly HelperDb helper = new HelperDb();

        //public DataTable KayitlariGetir(int sinavId)
        //{
        //    const string sql = @"select ogrenciler.Id, ogrenciler.SinavId,ilceler.IlceAdi,kurumlar.KurumAdi,ogrenciler.OgrOkulNo,ogrenciler.Adi,ogrenciler.Soyadi,ogrenciler.Sinifi,ogrenciler.Sube from ogrenciler,kurumlar,ilceler 
        //                         where ogrenciler.SinavId=?SinavId and ogrenciler.OgrOkulNo<>0  and ogrenciler.KurumKodu=kurumlar.KurumKodu and kurumlar.IlceId=ilceler.Id 
        //                         order by ogrenciler.Sube,ogrenciler.Adi asc";
        //    MySqlParameter p = new MySqlParameter("?SinavId", MySqlDbType.Int32) { Value = sinavId };
        //    return helper.ExecuteDataSet(sql, p).Tables[0];
        //}
        public List<OgrencilerInfo> KayitlariDiziyeGetir(int sinavId)
        {
            string sql = "select * from ogrenciler where SinavId=?SinavId";
            MySqlParameter p = new MySqlParameter("?SinavId", MySqlDbType.Int32) { Value = sinavId };

            DataTable veriler = helper.ExecuteDataSet(sql, p).Tables[0];

            return (from DataRow row in veriler.Rows select new OgrencilerInfo(Convert.ToInt32(row["Id"]), Convert.ToInt32(row["OgrenciId"]), Convert.ToInt32(row["SinavId"]), row["Adi"].ToString(), row["Soyadi"].ToString(), Convert.ToInt32(row["KurumKodu"]), Convert.ToInt32(row["OgrOkulNo"]), Convert.ToInt32(row["Sinifi"]), row["Sube"].ToString(), Convert.ToInt32(row["Girmedi"]))).ToList();
        }
        public List<OgrencilerInfo> KayitlariDiziyeGetir(int sinavId,int kurumKodu,int sinif,string sube)
        {
            string sql = "select * from ogrenciler where SinavId=?SinavId and KurumKodu=?KurumKodu and Sinifi=?Sinifi and Sube=?Sube";
            MySqlParameter[] p =
            {
                new MySqlParameter("?SinavId", MySqlDbType.Int32),
                new MySqlParameter("?KurumKodu", MySqlDbType.Int32),
                new MySqlParameter("?Sinifi", MySqlDbType.Int32),
                new MySqlParameter("?Sube", MySqlDbType.String)
            };
            p[0].Value = sinavId;
            p[1].Value = kurumKodu;
            p[2].Value = sinif;
            p[3].Value = sube;


            DataTable veriler = helper.ExecuteDataSet(sql, p).Tables[0];

            return (from DataRow row in veriler.Rows select new OgrencilerInfo(Convert.ToInt32(row["Id"]), Convert.ToInt32(row["OgrenciId"]), Convert.ToInt32(row["SinavId"]), row["Adi"].ToString(), row["Soyadi"].ToString(), Convert.ToInt32(row["KurumKodu"]), Convert.ToInt32(row["OgrOkulNo"]), Convert.ToInt32(row["Sinifi"]), row["Sube"].ToString(), Convert.ToInt32(row["Girmedi"]))).ToList();
        }
        public List<OgrencilerInfo> SinavaGirenSubeler(int sinavId)
        {
            string sql = @"select DISTINCT(ogrenciler.Sube),ogrenciler.Sinifi,ogrenciler.KurumKodu,ilceler.IlceAdi,kurumlar.KurumAdi from ogrenciler 
                        INNER JOIN kurumlar on ogrenciler.KurumKodu = kurumlar.KurumKodu
                        Inner Join ilceler on kurumlar.IlceId = ilceler.Id
                        where ogrenciler.SinavId = ?SinavId and ogrenciler.Sube <> ''";
            MySqlParameter p = new MySqlParameter("?SinavId", MySqlDbType.Int32) { Value = sinavId };

            DataTable veriler = helper.ExecuteDataSet(sql, p).Tables[0];

            return (from DataRow row in veriler.Rows select new OgrencilerInfo(Convert.ToInt32(row["Sinifi"]),row["Sube"].ToString(), Convert.ToInt32(row["KurumKodu"]), row["IlceAdi"].ToString(), row["KurumAdi"].ToString())).ToList();

        }
        //public List<OgrencilerInfo> KayitlariDiziyeGetir(int sinavId, bool excel)
        //{
        //    string sql = "select * from ogrenciler where SinavId=?SinavId";
        //    MySqlParameter p = new MySqlParameter("?SinavId", MySqlDbType.Int32) { Value = sinavId };

        //    DataTable veriler = helper.ExecuteDataSet(sql, p).Tables[0];

        //    return (from DataRow row in veriler.Rows select new OgrencilerInfo(Convert.ToInt32(row["KurumKodu"]), row["Adi"].ToString(), row["Soyadi"].ToString(), Convert.ToInt32(row["OgrOkulNo"]), Convert.ToInt32(row["Sinifi"]), row["Soyadi"].ToString())).ToList();
        //}
        public List<OgrencilerInfo> SinavaGirmeyenleriDiziyeGetir(int sinavId)
        {
            string sql = "select * from ogrenciler where SinavId=?SinavId and Girmedi=1";
            MySqlParameter p = new MySqlParameter("?SinavId", MySqlDbType.Int32) { Value = sinavId };

            DataTable veriler = helper.ExecuteDataSet(sql, p).Tables[0];

            return (from DataRow row in veriler.Rows select new OgrencilerInfo(Convert.ToInt32(row["Id"]), Convert.ToInt32(row["OgrenciId"]), Convert.ToInt32(row["Girmedi"]), row["DosyaAdi"].ToString())).ToList();
        }
        /// <summary>
        /// Cevap kaðýdý eksik olan öðrenciler
        /// </summary>
        /// <param name="sinavId">Sýnav No</param>
        /// <param name="sqlTxt"></param>
        /// <returns></returns>
        public DataTable EksikCKlariGetir(int sinavId, string sqlTxt)
        {
            string sql = string.Format("select ckkontrol.DosyaAdi,ogrenciler.* from ogrenciler,ckkontrol where ckkontrol.OgrId=ogrenciler.Id and ogrenciler.SinavId=?SinavId and (ogrenciler.Id=0 {0})", sqlTxt);
            MySqlParameter p = new MySqlParameter("?SinavId", MySqlDbType.Int32) { Value = sinavId };
            return helper.ExecuteDataSet(sql, p).Tables[0];
        }
         private static OgrencilerInfo TabloAlanlar(MySqlDataReader dr)
        {
            OgrencilerInfo info = new OgrencilerInfo();
            while (dr.Read())
            {
                info.Id = dr.GetMySayi("Id");
                info.OgrenciId = dr.GetMySayi("OgrenciId");
                info.SinavId = dr.GetMySayi("SinavId");
                info.Adi = dr.GetMyMetin("Adi");
                info.Soyadi = dr.GetMyMetin("Soyadi");
                info.KurumKodu = dr.GetMySayi("KurumKodu");
                info.OgrOkulNo = dr.GetMySayi("OgrOkulNo");
                info.Sinifi = dr.GetMySayi("Sinifi");
                info.Sube = dr.GetMyMetin("Sube");
                info.Girmedi = dr.GetMySayi("Girmedi");
                info.CKagitKontrol = dr.GetMyMetin("CKagitKontrol");
                info.DosyaAdi = dr.GetMyMetin("DosyaAdi");
            }
            dr.Close();

            return info;
        }
        public OgrencilerInfo KayitBilgiGetir(int id)
        {
            string cmdText = "select * from ogrenciler where Id=?Id";
            MySqlParameter param = new MySqlParameter("?Id", MySqlDbType.Int32) { Value = id };
            MySqlDataReader dr = helper.ExecuteReader(cmdText, param);
            return TabloAlanlar(dr);
        }
        //public void KayitSil(int id)
        //{
        //    const string sql = "delete from ogrenciler where Id=?Id";
        //    MySqlParameter p = new MySqlParameter("?Id", MySqlDbType.Int32) { Value = id };
        //    helper.ExecuteNonQuery(sql, p);
        //}
        //public void KayitSil(int snavId, bool tr)
        //{
        //    const string sql = "delete from ogrenciler where SinavId=?SinavId";
        //    MySqlParameter p = new MySqlParameter("?SinavId", MySqlDbType.Int32) { Value = snavId };
        //    helper.ExecuteNonQuery(sql, p);
        //}
        public void CkKontroTemizle(int sinavId)
        {
            const string sql = "update ogrenciler set CKagitKontrol=null where SinavId=?SinavId";
            MySqlParameter p = new MySqlParameter("?SinavId", MySqlDbType.Int32) { Value = sinavId };
            helper.ExecuteNonQuery(sql, p);
        }
        public void KayitEkle(OgrencilerInfo info)
        {
            const string sql = @"insert into ogrenciler (SinavId,OgrenciId,TcKimlik,Adi,Soyadi,KurumKodu,OgrOkulNo,Sinifi,Sube) values (?SinavId,?OgrenciId,?TcKimlik,?Adi,?Soyadi,?KurumKodu,?OgrOkulNo,?Sinifi,?Sube)";
            MySqlParameter[] pars =
            {
             new MySqlParameter("?SinavId", MySqlDbType.Int32),
             new MySqlParameter("?OgrenciId", MySqlDbType.Int32),
             new MySqlParameter("?TcKimlik", MySqlDbType.String),
             new MySqlParameter("?Adi", MySqlDbType.String),
             new MySqlParameter("?Soyadi", MySqlDbType.String),
             new MySqlParameter("?KurumKodu", MySqlDbType.Int32),
             new MySqlParameter("?OgrOkulNo", MySqlDbType.Int32),
             new MySqlParameter("?Sinifi", MySqlDbType.Int32),
             new MySqlParameter("?Sube", MySqlDbType.String)
            };
            pars[0].Value = info.SinavId;
            pars[1].Value = info.OgrenciId;
            pars[2].Value = info.TcKimlik;
            pars[3].Value = info.Adi;
            pars[4].Value = info.Soyadi;
            pars[5].Value = info.KurumKodu;
            pars[6].Value = info.OgrOkulNo;
            pars[7].Value = info.Sinifi;
            pars[8].Value = info.Sube;
            helper.ExecuteNonQuery(sql, pars);
        }
        public void KayitGuncelle(OgrencilerInfo info)
        {
            const string sql = @"update ogrenciler set SinavId=?SinavId,Adi=?Adi,Soyadi=?Soyadi,KurumKodu=?KurumKodu,OgrOkulNo=?OgrOkulNo,Sinifi=?Sinifi,Sube=?Sube where OgrenciId=?OgrenciId";
            MySqlParameter[] pars =
            {
             new MySqlParameter("?SinavId", MySqlDbType.Int32),
             new MySqlParameter("?Adi", MySqlDbType.String),
             new MySqlParameter("?Soyadi", MySqlDbType.String),
             new MySqlParameter("?KurumKodu", MySqlDbType.Int32),
             new MySqlParameter("?OgrOkulNo", MySqlDbType.Int32),
             new MySqlParameter("?Sinifi", MySqlDbType.Int32),
             new MySqlParameter("?Sube", MySqlDbType.String),
             new MySqlParameter("?OgrenciId", MySqlDbType.Int32)
            };
            pars[0].Value = info.SinavId;
            pars[1].Value = info.Adi;
            pars[2].Value = info.Soyadi;
            pars[3].Value = info.KurumKodu;
            pars[4].Value = info.OgrOkulNo;
            pars[5].Value = info.Sinifi;
            pars[6].Value = info.Sube;
            pars[7].Value = info.OgrenciId;
            helper.ExecuteNonQuery(sql, pars);
        }
        public void CKagitKontrol(int id, string sayfaYuzu)
        {
            string sql = string.Format("update ogrenciler set CKagitKontrol='{0}' where OgrenciId={1} and SinavId", sayfaYuzu, id);
            helper.ExecuteNonQuery(sql);
        }
        public void SinavaGirmedi(int ogrenciId, string dosyaAdi)
        {
            string sql = "update ogrenciler set Girmedi=1, DosyaAdi=?DosyaAdi where Id=?Id";
            MySqlParameter[] pars =
             {
             new MySqlParameter("?DosyaAdi", MySqlDbType.String),
             new MySqlParameter("?Id", MySqlDbType.Int32)
            };
            pars[0].Value = dosyaAdi;
            pars[1].Value = ogrenciId;
            helper.ExecuteNonQuery(sql,pars);
        }
        public bool KayitKontrol(int sinavId, int kurumKodu, int ogrOkulNo)
        {
            string cmdText = "select count(Id) from ogrenciler where SinavId=?SinavId and KurumKodu=?KurumKodu and OgrOkulNo=?OgrOkulNo";
            MySqlParameter[] pars =
            {
                new MySqlParameter("?SinavId", MySqlDbType.Int32),
                new MySqlParameter("?KurumKodu", MySqlDbType.Int32),
                new MySqlParameter("?OgrOkulNo", MySqlDbType.Int32)
            };
            pars[0].Value = sinavId;
            pars[1].Value = kurumKodu;
            pars[2].Value = ogrOkulNo;
            bool sonuc = Convert.ToInt32(helper.ExecuteScalar(cmdText, pars)) > 0;
            return sonuc;
        }
        public bool KayitKontrol(int sinavId)
        {
            string cmdText = "select count(Id) from ogrenciler where SinavId=?SinavId";
            MySqlParameter pars = new MySqlParameter("?SinavId", MySqlDbType.Int32) { Value = sinavId };
            bool sonuc = Convert.ToInt32(helper.ExecuteScalar(cmdText, pars)) > 0;
            return sonuc;
        }
    }

}