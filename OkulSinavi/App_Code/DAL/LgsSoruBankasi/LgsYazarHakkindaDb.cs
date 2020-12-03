using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using MySql.Data.MySqlClient;

public class LgsYazarHakkindaInfo
{
    public int KullaniciId { get; set; }
    public string Aciklama { get; set; }
}
public class LgsYazarHakkindaDb
{
    private readonly HelperDb helper = new HelperDb();

    public LgsYazarHakkindaInfo KayitBilgiGetir(int kullaniciId)
    {
        const string sql = "select * from lgsyazar_bilgi where KullaniciId=?KullaniciId";
        MySqlParameter param = new MySqlParameter("?KullaniciId", MySqlDbType.Int32) { Value = kullaniciId };
        LgsYazarHakkindaInfo info = new LgsYazarHakkindaInfo();
        MySqlDataReader dr = helper.ExecuteReader(sql, param);
        while (dr.Read())
        {
            info.KullaniciId = dr.GetMySayi("KullaniciId");
            info.Aciklama = dr.GetMyMetin("Aciklama");
        }
        dr.Close();

        return info; 
    }
    public void KayitEkle(LgsYazarHakkindaInfo info)
    {
        const string sql = @"insert into lgsyazar_bilgi (KullaniciId,Aciklama) values (?KullaniciId,?Aciklama)";
        MySqlParameter[] pars =
        {
                new MySqlParameter("?KullaniciId", MySqlDbType.Int32),
                new MySqlParameter("?Aciklama", MySqlDbType.String)
            };
        pars[0].Value = info.KullaniciId;
        pars[1].Value = info.Aciklama;
        helper.ExecuteNonQuery(sql, pars);
    }
    public void KayitGuncelle(LgsYazarHakkindaInfo info)
    {
        const string sql = @"update lgsyazar_bilgi set Aciklama=?Aciklama where KullaniciId=?KullaniciId";
        MySqlParameter[] pars =
        {
                new MySqlParameter("?Aciklama", MySqlDbType.String),
                new MySqlParameter("?KullaniciId", MySqlDbType.Int32)
            };
        pars[0].Value = info.Aciklama;
        pars[1].Value = info.KullaniciId;
        helper.ExecuteNonQuery(sql, pars);
    }

}