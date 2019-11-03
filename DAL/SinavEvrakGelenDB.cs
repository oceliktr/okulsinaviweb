using System.Data;
using MySql.Data.MySqlClient;

namespace DAL
{
    public class SinavEvrakGelenInfo
    {
        public int Id { get; set; }
        public int SinavId { get; set; }
        public string KurumKodu { get; set; }
        public string Dosya { get; set; }
    }

    public class SinavEvrakGelenDb
    {
        private readonly HelperDb _helper = new HelperDb();

        public DataTable KayitlariGetir()
        {
            const string sql = "select * from sinavevrakgelen order by Id asc";
            return _helper.ExecuteDataSet(sql).Tables[0];
        }
        public DataTable KayitlariGetir(int sinavId)
        {
            const string sql = "select ilceler.IlceAdi,kurumlar.KurumAdi,sinavevrakgelen.* from sinavevrakgelen,kurumlar,ilceler where  sinavevrakgelen.SinavId=?SinavId and kurumlar.IlceId=ilceler.Id and kurumlar.KurumKodu=sinavevrakgelen.KurumKodu order by Id asc";
            MySqlParameter p = new MySqlParameter("?SinavId", MySqlDbType.Int32) { Value = sinavId };
            return _helper.ExecuteDataSet(sql,p).Tables[0];
        }
        public SinavEvrakGelenInfo KayitBilgiGetir(string cmdText, params MySqlParameter[] param)
        {
            MySqlDataReader dr = _helper.ExecuteReader(cmdText, param);
            SinavEvrakGelenInfo info = new SinavEvrakGelenInfo();
            while (dr.Read())
            {
                info.Id = dr.GetMySayi("Id");
                info.SinavId = dr.GetMySayi("SinavId");
                info.KurumKodu = dr.GetMyMetin("KurumKodu");
                info.Dosya = dr.GetMyMetin("Dosya");
            }
            dr.Close();

            return info;
        }

        public SinavEvrakGelenInfo KayitBilgiGetir(int id)
        {
            string cmdText = "select * from sinavevrakgelen where Id=?Id";
            MySqlParameter param = new MySqlParameter("?Id", MySqlDbType.Int32) {Value = id};
            MySqlDataReader dr = _helper.ExecuteReader(cmdText, param);
            SinavEvrakGelenInfo info = new SinavEvrakGelenInfo();
            while (dr.Read())
            {
                info.Id = dr.GetMySayi("Id");
                info.SinavId = dr.GetMySayi("SinavId");
                info.KurumKodu = dr.GetMyMetin("KurumKodu");
                info.Dosya = dr.GetMyMetin("Dosya");
            }
            dr.Close();

            return info;
        }

        public void KayitSil(int id)
        {
            const string sql = "delete from sinavevrakgelen where Id=?Id";
            MySqlParameter p = new MySqlParameter("?Id", MySqlDbType.Int32) {Value = id};
            _helper.ExecuteNonQuery(sql, p);
        }

        public void KayitEkle(SinavEvrakGelenInfo info)
        {
            const string sql =
                @"insert into sinavevrakgelen (SinavId,KurumKodu,Dosya) values (?SinavId,?KurumKodu,?Dosya)";
            MySqlParameter[] pars =
            {
                new MySqlParameter("?SinavId", MySqlDbType.Int32),
                new MySqlParameter("?KurumKodu", MySqlDbType.String),
                new MySqlParameter("?Dosya", MySqlDbType.String),
            };
            pars[0].Value = info.SinavId;
            pars[1].Value = info.KurumKodu;
            pars[2].Value = info.Dosya;
            _helper.ExecuteNonQuery(sql, pars);
        }

        public void KayitGuncelle(SinavEvrakGelenInfo info)
        {
            const string sql =
                @"update sinavevrakgelen set SinavId=?SinavId,KurumKodu=?KurumKodu,Dosya=?Dosya where Id=?Id";
            MySqlParameter[] pars =
            {
                new MySqlParameter("?SinavId", MySqlDbType.Int32),
                new MySqlParameter("?KurumKodu", MySqlDbType.String),
                new MySqlParameter("?Dosya", MySqlDbType.String),
                new MySqlParameter("?Id", MySqlDbType.Int32),
            };
            pars[0].Value = info.SinavId;
            pars[1].Value = info.KurumKodu;
            pars[2].Value = info.Dosya;
            pars[3].Value = info.Id;
            _helper.ExecuteNonQuery(sql, pars);
        }
    }

}