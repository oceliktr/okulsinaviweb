using System.Data;
using MySql.Data.MySqlClient;

namespace DAL
{
    public class IlcelerInfo
    {
        public int Id { get; set; }
        public string IlceAdi { get; set; }
    }

    public class IlcelerDb
    {
        readonly HelperDb _helper = new HelperDb();

        public DataTable KayitlariGetir()
        {
            const string sql = "select * from ilceler order by IlceAdi asc";
            return _helper.ExecuteDataSet(sql).Tables[0];
        }

        public IlcelerInfo KayitBilgiGetir(string cmdText, params MySqlParameter[] param)
        {
            MySqlDataReader dr = _helper.ExecuteReader(cmdText, param);
            IlcelerInfo info = new IlcelerInfo();
            while (dr.Read())
            {
                info.Id = dr.GetMySayi("Id");
                info.IlceAdi = dr.GetMyMetin("IlceAdi");
            }
            dr.Close();

            return info;
        }

        public IlcelerInfo KayitBilgiGetir(int id)
        {
            string cmdText = "select * from ilceler where Id=?Id";
            MySqlParameter param = new MySqlParameter("?Id", MySqlDbType.Int32) { Value = id };
            MySqlDataReader dr = _helper.ExecuteReader(cmdText, param);
            IlcelerInfo info = new IlcelerInfo();
            while (dr.Read())
            {
                info.Id = dr.GetMySayi("Id");
                info.IlceAdi = dr.GetMyMetin("IlceAdi");
            }
            dr.Close();

            return info;
        }

        public void KayitSil(int id)
        {
            const string sql = "delete from ilceler where Id=?Id";
            MySqlParameter p = new MySqlParameter("?Id", MySqlDbType.Int32) { Value = id };
            _helper.ExecuteNonQuery(sql, p);
        }

        public void KayitEkle(IlcelerInfo info)
        {
            const string sql = @"insert into ilceler (IlceAdi) values (?IlceAdi)";
            MySqlParameter[] pars = 
{
 new MySqlParameter("?IlceAdi", MySqlDbType.String),
};
            pars[0].Value = info.IlceAdi;
            _helper.ExecuteNonQuery(sql, pars);
        }

        public void KayitGuncelle(IlcelerInfo info)
        {
            const string sql = @"update ilceler set IlceAdi=?IlceAdi where Id=?Id";
            MySqlParameter[] pars = 
{
 new MySqlParameter("?IlceAdi", MySqlDbType.String),
 new MySqlParameter("?Id", MySqlDbType.Int32),
};
            pars[0].Value = info.IlceAdi;
            pars[1].Value = info.Id;
            _helper.ExecuteNonQuery(sql, pars);
        }
    }
}

