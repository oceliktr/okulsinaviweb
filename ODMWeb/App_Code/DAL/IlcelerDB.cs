using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using MySql.Data.MySqlClient;


    public class IlcelerInfo
    {
        public int Id { get; set; }
        public string IlceAdi { get; set; }

        public IlcelerInfo()
        {
            
        }

        public IlcelerInfo(int id, string ilceAdi)
        {
            Id = id;
            IlceAdi = ilceAdi;
        }
    }

    public class IlcelerDb
    {
        readonly HelperDb _helper = new HelperDb();

        public List<IlcelerInfo> KayitlariDiziyeGetir()
        {
            string sql = "select * from ilceler order by IlceAdi asc";
            DataTable veriler = _helper.ExecuteDataSet(sql).Tables[0];
            return(from DataRow row in veriler.Rows select new IlcelerInfo(Convert.ToInt32(row["Id"]), row["IlceAdi"].ToString())).ToList();
        }
        public DataTable KayitlariGetir()
        {
            const string sql = "select * from ilceler order by IlceAdi asc";
            return _helper.ExecuteDataSet(sql).Tables[0];
        }
        public string IlceAdi(int ilceId)
        {
            string cmdText = "select * from ilceler where Id=?Id";
            MySqlParameter param = new MySqlParameter("?Id", MySqlDbType.Int32) { Value = ilceId };
            MySqlDataReader dr = _helper.ExecuteReader(cmdText, param);
            IlcelerInfo info = new IlcelerInfo();
            while (dr.Read())
            {
                info.Id = dr.GetMySayi("Id");
                info.IlceAdi = dr.GetMyMetin("IlceAdi");
            }
            dr.Close();

            return info.IlceAdi;
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
        public IlcelerInfo KayitBilgiGetir(string ilceAdi)
        {
            string cmdText = "select * from ilceler where IlceAdi=?IlceAdi";
            MySqlParameter param = new MySqlParameter("?IlceAdi", MySqlDbType.VarChar) { Value = ilceAdi };
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
        public int KayitKontrol(int ilceId)
        {
            const string cmdText = "select count(Id) from kurumlar where IlceId=?IlceId";
            const string cmdText2 = "select count(Id) from kullanicilar where IlceId=?IlceId";
            MySqlParameter pars = new MySqlParameter("?IlceId", MySqlDbType.Int32) { Value = ilceId };
            int sonuc = Convert.ToInt32(_helper.ExecuteScalar(cmdText, pars));
            sonuc += Convert.ToInt32(_helper.ExecuteScalar(cmdText2, pars));
            return sonuc;
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

        public List<IlcelerInfo> Ilceler()
        {
            const string sql = @"Select * from ilceler";
            
            DataTable veriler = _helper.ExecuteDataSet(sql).Tables[0];

            return (from DataRow row in veriler.Rows select new IlcelerInfo(Convert.ToInt32(row["Id"]), row["IlceAdi"].ToString())).ToList();

        }

    }
