using System.Data;
using MySql.Data.MySqlClient;

namespace DAL
{

    public class OgrenciKarneInfo
    {
        public int Id { get; set; }
        public int OgrenciId { get; set; }
        public int KurumKodu { get; set; }
        public int BransId { get; set; }
        public int SinavId { get; set; }
        public int AuNotu { get; set; }
        public int OptikNotu { get; set; }
        public int Toplam { get; set; }
    }

    public class OgrenciKarneDB
    {
        private readonly HelperDb helper = new HelperDb();

        public DataTable KayitlariGetir()
        {
            const string sql = "select * from ogrencikarne order by Id asc";
            return helper.ExecuteDataSet(sql).Tables[0];
        }
        public DataTable KayitlariGetir(int sinavId,int kurumKodu)
        {
            const string sql = @"select ogrencikarne.*,branslar.BransAdi,ogrenciler.Girmedi,ogrenciler.Adi,ogrenciler.Soyadi,ogrenciler.Sinifi,ogrenciler.Sube,ogrenciler.OgrOkulNo from ogrencikarne
                                INNER JOIN ogrenciler on ogrencikarne.OgrenciId = ogrenciler.OgrenciId
                                INNER JOIN branslar On ogrencikarne.BransId = branslar.Id
                                where ogrencikarne.SinavId = ?SinavId and ogrencikarne.KurumKodu=?KurumKodu";
            MySqlParameter[] pars =
           {
                new MySqlParameter("?SinavId", MySqlDbType.Int32),
                new MySqlParameter("?KurumKodu", MySqlDbType.Int32)
            };
            pars[0].Value = sinavId;
            pars[1].Value =kurumKodu;
            return helper.ExecuteDataSet(sql,pars).Tables[0];
        }
        public OgrenciKarneInfo KayitBilgiGetir(string cmdText, params MySqlParameter[] param)
        {
            MySqlDataReader dr = helper.ExecuteReader(cmdText, param);
            return TabloAlanlar(dr);
        }

        private static OgrenciKarneInfo TabloAlanlar(MySqlDataReader dr)
        {
            OgrenciKarneInfo info = new OgrenciKarneInfo();
            while (dr.Read())
            {
                info.Id = dr.GetMySayi("Id");
                info.OgrenciId = dr.GetMySayi("OgrenciId");
                info.KurumKodu = dr.GetMySayi("KurumKodu");
                info.BransId = dr.GetMySayi("BransId");
                info.SinavId = dr.GetMySayi("SinavId");
                info.AuNotu = dr.GetMySayi("AuNotu");
                info.OptikNotu = dr.GetMySayi("OptikNotu");
                info.Toplam = dr.GetMySayi("Toplam");
            }
            dr.Close();

            return info;
        }

        public OgrenciKarneInfo KayitBilgiGetir(int id)
        {
            string cmdText = "select * from ogrencikarne where Id=?Id";
            MySqlParameter param = new MySqlParameter("?Id", MySqlDbType.Int32) {Value = id};
            MySqlDataReader dr = helper.ExecuteReader(cmdText, param);
            return TabloAlanlar(dr);
        }

        public void KayitSil(int id)
        {
            const string sql = "delete from ogrencikarne where Id=?Id";
            MySqlParameter p = new MySqlParameter("?Id", MySqlDbType.Int32) {Value = id};
            helper.ExecuteNonQuery(sql, p);
        }
        public void KayitSil(int sinavId,bool tr)
        {
            const string sql = "delete from ogrencikarne where SinavId=?SinavId";
            MySqlParameter p = new MySqlParameter("?SinavId", MySqlDbType.Int32) { Value = sinavId };
            helper.ExecuteNonQuery(sql, p);
        }
        public void KayitEkle(OgrenciKarneInfo info)
        {
            const string sql =
                @"insert into ogrencikarne (OgrenciId,BransId,SinavId,AuNotu,OptikNotu,Toplam,KurumKodu) values (?OgrenciId,?BransId,?SinavId,?AuNotu,?OptikNotu,?Toplam,?KurumKodu)";
            MySqlParameter[] pars =
            {
                new MySqlParameter("?OgrenciId", MySqlDbType.Int32),
                new MySqlParameter("?BransId", MySqlDbType.Int32),
                new MySqlParameter("?SinavId", MySqlDbType.Int32),
                new MySqlParameter("?AuNotu", MySqlDbType.Int32),
                new MySqlParameter("?OptikNotu", MySqlDbType.Int32),
                new MySqlParameter("?Toplam", MySqlDbType.Int32),
                new MySqlParameter("?KurumKodu", MySqlDbType.Int32)
            };
            pars[0].Value = info.OgrenciId;
            pars[1].Value = info.BransId;
            pars[2].Value = info.SinavId;
            pars[3].Value = info.AuNotu;
            pars[4].Value = info.OptikNotu;
            pars[5].Value = info.Toplam;
            pars[6].Value = info.KurumKodu;
            helper.ExecuteNonQuery(sql, pars);
        }

        public void KayitGuncelle(OgrenciKarneInfo info)
        {
            const string sql =
                @"update ogrencikarne set OgrenciId=?OgrenciId,BransId=?BransId,SinavId=?SinavId,AuNotu=?AuNotu,OptikNotu=?OptikNotu,Toplam=?Toplam where Id=?Id";
            MySqlParameter[] pars =
            {
                new MySqlParameter("?OgrenciId", MySqlDbType.Int32),
                new MySqlParameter("?BransId", MySqlDbType.Int32),
                new MySqlParameter("?SinavId", MySqlDbType.Int32),
                new MySqlParameter("?AuNotu", MySqlDbType.Int32),
                new MySqlParameter("?OptikNotu", MySqlDbType.Int32),
                new MySqlParameter("?Toplam", MySqlDbType.Int32),
                new MySqlParameter("?Id", MySqlDbType.Int32),
            };
            pars[0].Value = info.OgrenciId;
            pars[1].Value = info.BransId;
            pars[2].Value = info.SinavId;
            pars[3].Value = info.AuNotu;
            pars[4].Value = info.OptikNotu;
            pars[5].Value = info.Toplam;
            pars[6].Value = info.Id;
            helper.ExecuteNonQuery(sql, pars);
        }
    }
}
