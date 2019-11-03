using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using DAL;
using MySql.Data.MySqlClient;
public class SbSeceneklerInfo
{
    public int SoruId { get; set; }
    public string SAdi { get; set; }
    public int Dogru { get; set; }
    public string Secenek { get; set; }

    public SbSeceneklerInfo()
    {
        
    }
    public SbSeceneklerInfo(int soruId, string sAdi, int dogru, string secenek)
    {
        SoruId = soruId;
        SAdi = sAdi;
        Dogru = dogru;
        Secenek = secenek;
    }
}

public class SbSeceneklerDB
{
    readonly HelperDb helper = new HelperDb();

    public DataTable KayitlariGetir()
    {
        const string sql = "select * from sbsecenekler order by SoruId asc";
        return helper.ExecuteDataSet(sql).Tables[0];
    }
    public List<SbSeceneklerInfo> KayitlariGetir(int soruId)
    {
        string sql = "select * from sbsecenekler where SoruId=?SoruId";
        MySqlParameter param = new MySqlParameter("?SoruId", MySqlDbType.Int32) { Value = soruId };

        DataTable veriler = helper.ExecuteDataSet(sql, param).Tables[0];
        return (from DataRow row in veriler.Rows select new SbSeceneklerInfo(Convert.ToInt32(row["SoruId"]), row["SAdi"].ToString(), Convert.ToInt32(row["Dogru"]), row["Secenek"].ToString())).ToList();
    }
    public SbSeceneklerInfo KayitBilgiGetir(string cmdText, params MySqlParameter[] param)
    {
        MySqlDataReader dr = helper.ExecuteReader(cmdText, param);
        SbSeceneklerInfo info = new SbSeceneklerInfo();
        while (dr.Read())
        {
            info.SoruId = dr.GetMySayi("SoruId");
            info.SAdi = dr.GetMyMetin("SAdi");
            info.Dogru = dr.GetMySayi("Dogru");
            info.Secenek = dr.GetMyMetin("Secenek");
        }
        dr.Close();

        return info;
    }

    public SbSeceneklerInfo KayitBilgiGetir(int soruId)
    {
        string cmdText = "select * from sbsecenekler where SoruId=?SoruId";
        MySqlParameter param = new MySqlParameter("?SoruId", MySqlDbType.Int32) { Value = soruId };
        MySqlDataReader dr = helper.ExecuteReader(cmdText, param);
        SbSeceneklerInfo info = new SbSeceneklerInfo();
        while (dr.Read())
        {
            info.SoruId = dr.GetMySayi("SoruId");
            info.SAdi = dr.GetMyMetin("SAdi");
            info.Dogru = dr.GetMySayi("Dogru");
            info.Secenek = dr.GetMyMetin("Secenek");
        }
        dr.Close();

        return info;
    }

    public void KayitSil(int soruId)
    {
        const string sql = "delete from sbsecenekler where SoruId=?Id";
        MySqlParameter p = new MySqlParameter("?Id", MySqlDbType.Int32) { Value = soruId };
        helper.ExecuteNonQuery(sql, p);
    }

    public void KayitEkle(SbSeceneklerInfo info)
    {
        const string sql = @"insert into sbsecenekler (SoruId,SAdi,Dogru,Secenek) values (?SoruId,?SAdi,?Dogru,?Secenek)";
        MySqlParameter[] pars =
        {
         new MySqlParameter("?SoruId", MySqlDbType.Int32),
         new MySqlParameter("?SAdi", MySqlDbType.String),
         new MySqlParameter("?Dogru", MySqlDbType.Int32),
         new MySqlParameter("?Secenek", MySqlDbType.String),
        };
        pars[0].Value = info.SoruId;
        pars[1].Value = info.SAdi;
        pars[2].Value = info.Dogru;
        pars[3].Value = info.Secenek;
        helper.ExecuteNonQuery(sql, pars);
    }

    public void KayitGuncelle(SbSeceneklerInfo info)
    {
        const string sql = @"update sbsecenekler set Dogru=?Dogru,Secenek=?Secenek where SoruId=?SoruId and SAdi=?SAdi";
        MySqlParameter[] pars =
        {
         new MySqlParameter("?Dogru", MySqlDbType.Int32),
         new MySqlParameter("?Secenek", MySqlDbType.String),
         new MySqlParameter("?SoruId", MySqlDbType.Int32),
         new MySqlParameter("?SAdi", MySqlDbType.String)
        };
        pars[0].Value = info.Dogru;
        pars[1].Value = info.Secenek;
        pars[2].Value = info.SoruId;
        pars[3].Value = info.SAdi;
        helper.ExecuteNonQuery(sql, pars);
    }
}

