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
        public string Uyrugu { get; set; }
        public string Adi { get; set; }
        public string Soyadi { get; set; }
        public string AdiSoyadi { get; set; }
        public int KurumKodu { get; set; }
        public int OgrOkulNo { get; set; }
        public int Sinifi { get; set; }
        public string Sube { get; set; }
        public int IlceId { get; set; }
        public string IlceAdi { get; set; }
        public string KurumAdi { get; set; }
        public string SinifSube { get; set; }
        public string SinifAdi { get; set; } //5. sýnýf gibi
        public OgrencilerInfo()
        { }

        public OgrencilerInfo(string tcKimlik, string uyrugu, int ogrenciId, int sinavId, string adi, string soyadi, int kurumKodu, int okulNo, int sinifi, string sube)
        {
            TcKimlik = tcKimlik;
            Uyrugu = uyrugu;
            OgrenciId = ogrenciId;
            SinavId = sinavId;
            Adi = adi;
            Soyadi = soyadi;
            KurumKodu = kurumKodu;
            OgrOkulNo = okulNo;
            Sinifi = sinifi;
            Sube = sube;
        }
        public OgrencilerInfo(string tcKimlik, string uyrugu, int ogrenciId, int sinavId, string adi, string soyadi, int kurumKodu, int okulNo, int sinifi, string sube, string ilceAdi, string kurumAdi)
        {
            TcKimlik = tcKimlik;
            Uyrugu = uyrugu;
            OgrenciId = ogrenciId;
            SinavId = sinavId;
            Adi = adi;
            Soyadi = soyadi;
            KurumKodu = kurumKodu;
            OgrOkulNo = okulNo;
            Sinifi = sinifi;
            Sube = sube;
            IlceAdi = ilceAdi;
            KurumAdi = kurumAdi;
        }
        public OgrencilerInfo(int ogrenciId, int kurumKodu, string kurumAdi, string tcKimlik, string adi, string soyadi, int okulNo, int sinifi, string sube)
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
        public OgrencilerInfo(int id, int ogrenciId)
        {
            Id = id;
            OgrenciId = ogrenciId;
        }
        public OgrencilerInfo(int sinifi, string sube, int kurumKodu, string ilceAdi, int ilceId, string kurumAdi)
        {
            IlceAdi = ilceAdi;
            KurumKodu = kurumKodu;
            KurumAdi = kurumAdi;
            Sinifi = sinifi;
            Sube = sube;
            IlceId = ilceId;
        }
        public OgrencilerInfo(string sinifSube)
        {
            SinifSube = sinifSube;
        }
        public OgrencilerInfo(int sinifi)
        {
            Sinifi = sinifi;
        }
        public OgrencilerInfo(int sinifi, string sinifAdi)
        {
            Sinifi = sinifi;
            SinifAdi = sinifAdi;
        }
        public OgrencilerInfo(int ogrenciId, string adiSoyadi, int x)
        {
            OgrenciId = ogrenciId;
            AdiSoyadi = adiSoyadi;
        }

        public OgrencilerInfo(int sinifi, string sube, string sinifSube)
        {

            Sinifi = sinifi;
            Sube = sube;
            SinifSube = sinifSube;
        }
    }

    public class OgrencilerDb
    {
        readonly HelperDb helper = new HelperDb();
        public List<OgrencilerInfo> KayitlariDiziyeGetir(int sinavId, int ilceId, int kurumKodu, int sinif, string sube)
        {
            HelperDb _helper = new HelperDb();
            string sql = string.Concat(@"SELECT ogrenciler.OgrenciId,CONCAT(ogrenciler.Adi,' ',ogrenciler.Soyadi) as AdiSoyadi FROM ogrenciler
                             INNER JOIN Kurumlar ON ogrenciler.KurumKodu = kurumlar.KurumKodu and kurumlar.IlceId=", ilceId,
                " WHERE ogrenciler.SinavId =", sinavId);
            if (sinif != 0)
                sql += " and ogrenciler.Sinifi=" + sinif;
            if (kurumKodu != 1 && kurumKodu != 0)
                sql += " and ogrenciler.KurumKodu=" + kurumKodu;
            if (sube != "Tümü" && sube != "Seçiniz")
                sql += " and ogrenciler.Sube='" + sube + "'";
            sql += " order by ogrenciler.KurumKodu,ogrenciler.Sinifi,ogrenciler.Sube,ogrenciler.Adi asc";

            DataTable veriler = _helper.ExecuteDataSet(sql).Tables[0];
            List<OgrencilerInfo> sonuc = (from DataRow row in veriler.Rows
                                          select new OgrencilerInfo(Convert.ToInt32(row["OgrenciId"]), row["AdiSoyadi"].ToString(), 0)).ToList();
            return sonuc;
        }
        public List<OgrencilerInfo> KayitlariDiziyeGetir(int sinavId, int ilceId, int kurumKodu, int sinif, string sube, int ogrenciId)
        {
            HelperDb _helper = new HelperDb();
            string sql = string.Concat(@"SELECT ogrenciler.*,kurumlar.KurumAdi,ilceler.IlceAdi FROM ogrenciler
                             INNER JOIN Kurumlar ON ogrenciler.KurumKodu = kurumlar.KurumKodu and kurumlar.IlceId=", ilceId,
                             " INNER JOIN ilceler on kurumlar.IlceId = ilceler.Id WHERE ogrenciler.SinavId =", sinavId);
            if (sinif != 0)
                sql += " and ogrenciler.Sinifi=" + sinif;
            if (ogrenciId != 0 && ogrenciId != 1)
                sql += " and ogrenciler.OgrenciId=" + ogrenciId;
            if (kurumKodu != 1 && kurumKodu != 0)
                sql += " and ogrenciler.KurumKodu=" + kurumKodu;
            if (sube != "Tümü" && sube != "Seçiniz")
                sql += " and ogrenciler.Sube='" + sube + "'";
            sql += " order by ogrenciler.KurumKodu,ogrenciler.Sinifi,ogrenciler.Sube,ogrenciler.Adi asc";

            DataTable veriler = _helper.ExecuteDataSet(sql).Tables[0];
            List<OgrencilerInfo> sonuc = (from DataRow row in veriler.Rows select new OgrencilerInfo(row["TcKimlik"].ToString(), row["Uyrugu"].ToString(), Convert.ToInt32(row["OgrenciId"]), Convert.ToInt32(row["SinavId"]), row["Adi"].ToString(), row["Soyadi"].ToString(), Convert.ToInt32(row["KurumKodu"]), Convert.ToInt32(row["OgrOkulNo"]), Convert.ToInt32(row["Sinifi"]), row["Sube"].ToString(), row["KurumAdi"].ToString(), row["IlceAdi"].ToString())).ToList();
            return sonuc;
        }
        public List<OgrencilerInfo> KayitlariDiziyeGetir(int sinavId)
        {
            string sql = "select * from ogrenciler where SinavId=?SinavId";
            MySqlParameter p = new MySqlParameter("?SinavId", MySqlDbType.Int32) { Value = sinavId };

            DataTable veriler = helper.ExecuteDataSet(sql, p).Tables[0];

            return (from DataRow row in veriler.Rows select new OgrencilerInfo(row["TcKimlik"].ToString(), row["Uyrugu"].ToString(), Convert.ToInt32(row["OgrenciId"]), Convert.ToInt32(row["SinavId"]), row["Adi"].ToString(), row["Soyadi"].ToString(), Convert.ToInt32(row["KurumKodu"]), Convert.ToInt32(row["OgrOkulNo"]), Convert.ToInt32(row["Sinifi"]), row["Sube"].ToString())).ToList();
        }
        public List<OgrencilerInfo> KayitlariDiziyeGetir(int sinavId, int kurumKodu)
        {
            string sql = "select * from ogrenciler where SinavId=?SinavId and KurumKodu=?KurumKodu";
            MySqlParameter[] p =
            {
                new MySqlParameter("?SinavId", MySqlDbType.Int32),
                new MySqlParameter("?KurumKodu", MySqlDbType.Int32),
            };
            p[0].Value = sinavId;
            p[1].Value = kurumKodu;
            DataTable veriler = helper.ExecuteDataSet(sql, p).Tables[0];

            return (from DataRow row in veriler.Rows select new OgrencilerInfo(row["TcKimlik"].ToString(), row["Uyrugu"].ToString(), Convert.ToInt32(row["OgrenciId"]), Convert.ToInt32(row["SinavId"]), row["Adi"].ToString(), row["Soyadi"].ToString(), Convert.ToInt32(row["KurumKodu"]), Convert.ToInt32(row["OgrOkulNo"]), Convert.ToInt32(row["Sinifi"]), row["Sube"].ToString())).ToList();
        }
        public List<OgrencilerInfo> KayitlariDiziyeGetir(int sinavId, int kurumKodu, int sinif)
        {
            string sql = "select * from ogrenciler where SinavId=?SinavId and KurumKodu=?KurumKodu";
            sql += " and Sinifi=?Sinifi";
            sql += " order by Sinifi,Sube,Adi";
            MySqlParameter[] p =
            {
                new MySqlParameter("?SinavId", MySqlDbType.Int32),
                new MySqlParameter("?KurumKodu", MySqlDbType.Int32),
                new MySqlParameter("?Sinifi", MySqlDbType.Int32)
            };
            p[0].Value = sinavId;
            p[1].Value = kurumKodu;
            p[2].Value = sinif;
            DataTable veriler = helper.ExecuteDataSet(sql, p).Tables[0];

            return (from DataRow row in veriler.Rows select new OgrencilerInfo(row["TcKimlik"].ToString(), row["Uyrugu"].ToString(), Convert.ToInt32(row["OgrenciId"]), Convert.ToInt32(row["SinavId"]), row["Adi"].ToString(), row["Soyadi"].ToString(), Convert.ToInt32(row["KurumKodu"]), Convert.ToInt32(row["OgrOkulNo"]), Convert.ToInt32(row["Sinifi"]), row["Sube"].ToString())).ToList();
        }
        public List<OgrencilerInfo> KayitlariDiziyeGetir(int sinavId, int kurumKodu, int sinif, string sube)
        {
            string sql = "select * from ogrenciler where SinavId=?SinavId and KurumKodu=?KurumKodu";
            if (sube != "0")
                sql += " and Sinifi=?Sinifi and Sube=?Sube";
            sql += " order by Sinifi,Sube,Adi";
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

            return (from DataRow row in veriler.Rows select new OgrencilerInfo(row["TcKimlik"].ToString(), row["Uyrugu"].ToString(), Convert.ToInt32(row["OgrenciId"]), Convert.ToInt32(row["SinavId"]), row["Adi"].ToString(), row["Soyadi"].ToString(), Convert.ToInt32(row["KurumKodu"]), Convert.ToInt32(row["OgrOkulNo"]), Convert.ToInt32(row["Sinifi"]), row["Sube"].ToString())).ToList();
        }
        public List<OgrencilerInfo> SinavaGirenSubeler(int sinavId)
        {
            string sql = @"select DISTINCT(ogrenciler.Sube),ogrenciler.Sinifi,ogrenciler.KurumKodu,ilceler.Id as IlceId,ilceler.IlceAdi,kurumlar.KurumAdi from ogrenciler 
                        INNER JOIN kurumlar on ogrenciler.KurumKodu = kurumlar.KurumKodu
                        Inner Join ilceler on kurumlar.IlceId = ilceler.Id
                        where ogrenciler.SinavId = ?SinavId and ogrenciler.Sube <> '' order by ilceler.IlceAdi,kurumlar.KurumAdi,ogrenciler.Sube asc";
            MySqlParameter p = new MySqlParameter("?SinavId", MySqlDbType.Int32) { Value = sinavId };

            DataTable veriler = helper.ExecuteDataSet(sql, p).Tables[0];

            return (from DataRow row in veriler.Rows select new OgrencilerInfo(Convert.ToInt32(row["Sinifi"]), row["Sube"].ToString(), Convert.ToInt32(row["KurumKodu"]), row["IlceAdi"].ToString(), Convert.ToInt32(row["IlceId"]), row["KurumAdi"].ToString())).ToList();

        }
        public List<OgrencilerInfo> SinavaGirenSubeler(int sinavId, int kurumKodu)
        {
            string sql = @"select DISTINCT(o.Sube),o.Sinifi, CONCAT(o.Sinifi,'-',o.Sube) as SinifSube from ogrenciler as o
                        where o.SinavId = ?SinavId and o.KurumKodu =?KurumKodu and o.Sube <> '' 
                        order by o.Sinifi, o.Sube asc";
            MySqlParameter[] p =
            {
                new MySqlParameter("?SinavId", MySqlDbType.Int32),
                new MySqlParameter("?KurumKodu", MySqlDbType.Int32)
            };
            p[0].Value = sinavId;
            p[1].Value = kurumKodu;
            DataTable veriler = helper.ExecuteDataSet(sql, p).Tables[0];

            return (from DataRow row in veriler.Rows select new OgrencilerInfo(row["SinifSube"].ToString())).ToList();

        }
        public List<OgrencilerInfo> SinavaGirenSubeler(int sinavId, int kurumKodu, int sinifi)
        {
            string sql = @"select DISTINCT(o.Sube) as Sube,o.Sinifi, CONCAT(o.Sinifi,'-',o.Sube) as SinifSube from ogrenciler as o
                        where o.SinavId = ?SinavId and o.KurumKodu =?KurumKodu and o.Sube <> '' and Sinifi=?Sinifi 
                        order by o.Sinifi, o.Sube asc";
            MySqlParameter[] p =
            {
                new MySqlParameter("?SinavId", MySqlDbType.Int32),
                new MySqlParameter("?KurumKodu", MySqlDbType.Int32),
                new MySqlParameter("?Sinifi", MySqlDbType.Int32)
            };
            p[0].Value = sinavId;
            p[1].Value = kurumKodu;
            p[2].Value = sinifi;
            DataTable veriler = helper.ExecuteDataSet(sql, p).Tables[0];

            return (from DataRow row in veriler.Rows select new OgrencilerInfo(Convert.ToInt32(row["Sinifi"]), row["Sube"].ToString(), row["SinifSube"].ToString())).ToList();

        }
        public List<OgrencilerInfo> SinavaGirenSubeler2(int sinavId, int kurumKodu, int sinifi)
        {
            string sql = @"select DISTINCT(o.Sube) as SinifSube from ogrenciler as o
                        where o.SinavId = ?SinavId and o.KurumKodu =?KurumKodu and Sinifi=?Sinifi 
                        order by o.Sinifi, o.Sube asc";
            MySqlParameter[] p =
            {
                new MySqlParameter("?SinavId", MySqlDbType.Int32),
                new MySqlParameter("?KurumKodu", MySqlDbType.Int32),
                new MySqlParameter("?Sinifi", MySqlDbType.Int32)
            };
            p[0].Value = sinavId;
            p[1].Value = kurumKodu;
            p[2].Value = sinifi;
            DataTable veriler = helper.ExecuteDataSet(sql, p).Tables[0];

            return (from DataRow row in veriler.Rows select new OgrencilerInfo(row["SinifSube"].ToString())).ToList();

        }
        public List<OgrencilerInfo> SinavaGirenSiniflar(int sinavId)
        {
            string sql = @"select DISTINCT(o.Sinifi),CONCAT(Sinifi,'. sýnýf') as SinifAdi from ogrenciler as o where o.SinavId = ?SinavId order by o.Sinifi asc";
            MySqlParameter p = new MySqlParameter("?SinavId", MySqlDbType.Int32) { Value = sinavId };
            DataTable veriler = helper.ExecuteDataSet(sql, p).Tables[0];

            return (from DataRow row in veriler.Rows select new OgrencilerInfo(Convert.ToInt32(row["Sinifi"]), row["SinifAdi"].ToString())).ToList();

        }
        public List<OgrencilerInfo> SinavaGirenSiniflar(int sinavId, int kurumKodu)
        {
            string sql = @"select DISTINCT(o.Sinifi) from ogrenciler as o
                        where o.SinavId = ?SinavId and o.KurumKodu =?KurumKodu
                        order by o.Sinifi asc";
            MySqlParameter[] p =
            {
                new MySqlParameter("?SinavId", MySqlDbType.Int32),
                new MySqlParameter("?KurumKodu", MySqlDbType.Int32)
            };
            p[0].Value = sinavId;
            p[1].Value = kurumKodu;
            DataTable veriler = helper.ExecuteDataSet(sql, p).Tables[0];

            return (from DataRow row in veriler.Rows select new OgrencilerInfo(Convert.ToInt32(row["Sinifi"]))).ToList();

        }
        public List<OgrencilerInfo> IlcedeSinavaGirenSiniflar(int sinavId, int ilceId)
        {
            string sql = @"select DISTINCT(ogrenciler.Sinifi) from ogrenciler 
                         inner join kurumlar on ogrenciler.KurumKodu=kurumlar.KurumKodu and kurumlar.IlceId=?IlceId
                         where ogrenciler.SinavId =?SinavId order by ogrenciler.Sinifi asc";
            MySqlParameter[] p =
            {
                new MySqlParameter("?SinavId", MySqlDbType.Int32),
                new MySqlParameter("?IlceId", MySqlDbType.Int32)
            };
            p[0].Value = sinavId;
            p[1].Value = ilceId;
            DataTable veriler = helper.ExecuteDataSet(sql, p).Tables[0];

            return (from DataRow row in veriler.Rows select new OgrencilerInfo(Convert.ToInt32(row["Sinifi"]))).ToList();

        }
        public List<OgrencilerInfo> IlcedeSinavaGirenSubeler(int sinavId, int ilceId)
        {
            string sql = @"select DISTINCT(ogrenciler.Sube) as SinifSube from ogrenciler 
                         inner join kurumlar on ogrenciler.KurumKodu=kurumlar.KurumKodu and kurumlar.IlceId=?IlceId
                         where ogrenciler.SinavId =?SinavId order by ogrenciler.Sube asc";
            MySqlParameter[] p =
            {
                new MySqlParameter("?SinavId", MySqlDbType.Int32),
                new MySqlParameter("?IlceId", MySqlDbType.Int32)
            };
            p[0].Value = sinavId;
            p[1].Value = ilceId;
            DataTable veriler = helper.ExecuteDataSet(sql, p).Tables[0];

            return (from DataRow row in veriler.Rows select new OgrencilerInfo(row["SinifSube"].ToString())).ToList();

        }
        /// <summary>
        /// Cevap kaðýdý eksik olan öðrenciler
        /// </summary>
        /// <param name="sinavId">Sýnav No</param>
        /// <param name="sqlTxt"></param>
        /// <returns></returns>
        public DataTable EksikCKlariGetir(int sinavId, string sqlTxt)
        {
            string sql = string.Format("select ckdosyalar.DosyaAdi,ogrenciler.* from ogrenciler,ckdosyalar where ckdosyalar.OgrenciId=ogrenciler.Id and ogrenciler.SinavId=?SinavId and (ogrenciler.Id=0 {0})", sqlTxt);
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
                info.TcKimlik = dr.GetMyMetin("TcKimlik");
                info.Uyrugu = dr.GetMyMetin("Uyrugu");
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
        public OgrencilerInfo KayitBilgiGetir(int ogrenciId, int sinavId)
        {
            string cmdText = "select * from ogrenciler where OgrenciId=?OgrenciId and SinavId=?SinavId";
            MySqlParameter[] pars =
            {
                new MySqlParameter("?OgrenciId", MySqlDbType.Int32),
                new MySqlParameter("?SinavId", MySqlDbType.Int32)
            };
            pars[0].Value = ogrenciId;
            pars[1].Value = sinavId;
            MySqlDataReader dr = helper.ExecuteReader(cmdText, pars);
            return TabloAlanlar(dr);
        }
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
        public void KitapcikTuru(int ogrenciId, string kitapcikTuru)
        {
            string sql = "update ogrenciler set KitapcikTuru=?KitapcikTuru where OgrenciId=?OgrenciId";
            MySqlParameter[] pars =
            {
                new MySqlParameter("?OgrenciId", MySqlDbType.Int32),
                new MySqlParameter("?KitapcikTuru", MySqlDbType.String)
            };
            pars[0].Value = ogrenciId;
            pars[1].Value = kitapcikTuru;
            helper.ExecuteNonQuery(sql, pars);
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
        public int OgrenciSayisi(int sinavId)
        {
            string sql = "SELECT COUNT(Id) FROM ogrenciler where SinavId=?SinavId";
            MySqlParameter pars = new MySqlParameter("?SinavId", MySqlDbType.Int32) { Value = sinavId };
            int ogrenciSayisi = Convert.ToInt32(helper.ExecuteScalar(sql, pars));
            return ogrenciSayisi;
        }
        public int SubeOgrenciSayisi(int sinavId,string kurumKodu, string sube)
        {
            string sql = "select Count(Id) from ogrenciler where  SinavId=?SinavId and KurumKodu =?KurumKodu and Sube=?Sube";
            MySqlParameter[] pars =
            {
                new MySqlParameter("?SinavId", MySqlDbType.Int32),
                new MySqlParameter("?KurumKodu", MySqlDbType.VarChar),
                new MySqlParameter("?Sube", MySqlDbType.VarChar)
            };
            pars[0].Value = sinavId;
            pars[1].Value = kurumKodu;
            pars[2].Value = sube;
            int mesajSayisi = Convert.ToInt32(helper.ExecuteScalar(sql, pars));
            return mesajSayisi;

        }

    }
}