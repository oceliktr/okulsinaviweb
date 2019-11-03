using System;
using System.Collections.Generic;
using System.Data;
using DAL;
using MySql.Data.MySqlClient;
public class CkKarneKutukInfo
{
    public int Id { get; set; }
    public int OpaqId { get; set; }
    public string IlAdi { get; set; }
    public string IlceAdi { get; set; }
    public int KurumKodu { get; set; }
    public string KurumAdi { get; set; }
    public int OgrenciNo { get; set; }
    public string Adi { get; set; }
    public string Soyadi { get; set; }
    public int Sinifi { get; set; }
    public string Sube { get; set; }
    public int SinavId { get; set; }
    public int DersKodu { get; set; }

    public CkKarneKutukInfo()
    {
        
    }

    public CkKarneKutukInfo(int id, int opaqId, string ilceAdi, int kurumKodu, string kurumAdi, int ogrenciNo, string adi, string soyadi, int sinifi, string sube, int sinavId)
    {
        Id = id;
        OpaqId = opaqId;
        IlceAdi = ilceAdi;
        KurumKodu = kurumKodu;
        KurumAdi = kurumAdi;
        OgrenciNo = ogrenciNo;
        Adi = adi;
        Soyadi = soyadi;
        Sinifi = sinifi;
        Sube = sube;
        SinavId = sinavId;
    }
}

public class CkKarneKutukDB
{
    readonly HelperDb helper = new HelperDb();
    public DataTable KayitlariGetir()
    {
        const string sql = "select * from ckkarnekutuk order by Id asc";
        return helper.ExecuteDataSet(sql).Tables[0];
    }
    public DataTable KayitlariGetir(int sinavId,string ilceAdi)
    {
        const string sql = "select DISTINCT KurumKodu,IlceAdi, KurumAdi, SinavId from ckkarnekutuk where SinavId=?SinavId and IlceAdi=?IlceAdi order by KurumAdi asc";
        MySqlParameter[] pars =
        {
            new MySqlParameter("?SinavId", MySqlDbType.Int32),
            new MySqlParameter("?IlceAdi", MySqlDbType.String)
        };
        pars[0].Value = sinavId;
        pars[1].Value = ilceAdi;

        var table =helper.ExecuteDataSet(sql,pars).Tables[0];
        return table;
    }
    public DataTable KayitlariGetir(int sinavId, int kurumKodu)
    {
        const string sql = "select  DISTINCT KurumKodu,IlceAdi, KurumAdi, SinavId from ckkarnekutuk where SinavId=?SinavId and KurumKodu=?KurumKodu order by KurumAdi asc";
        MySqlParameter[] pars =
        {
            new MySqlParameter("?SinavId", MySqlDbType.Int32),
            new MySqlParameter("?KurumKodu", MySqlDbType.Int32)
        };
        pars[0].Value = sinavId;
        pars[1].Value = kurumKodu;

        var table = helper.ExecuteDataSet(sql, pars).Tables[0];
        return table;
    }
    public List<CkKarneKutukInfo> KayitlariDizeGetir(int sinavId, int kurumKodu)
    {
        string sql = "select * from ckkarnekutuk where SinavId=?SinavId and KurumKodu=?KurumKodu";
        MySqlParameter[] p =
        {
            new MySqlParameter("?SinavId", MySqlDbType.Int32),
            new MySqlParameter("?KurumKodu", MySqlDbType.Int32)
        };
        p[0].Value = sinavId;
        p[1].Value =  kurumKodu;

        DataTable dt = helper.ExecuteDataSet(sql, p).Tables[0];
        List<CkKarneKutukInfo> karne = new List<CkKarneKutukInfo>();
        foreach (DataRow k in dt.Rows)
        {
            karne.Add(new CkKarneKutukInfo(Convert.ToInt32(k["Id"]), Convert.ToInt32(k["OpaqId"]), k["IlceAdi"].ToString(), 
                Convert.ToInt32(k["KurumKodu"]),k["KurumAdi"].ToString(), Convert.ToInt32(k["OgrenciNo"]), k["Adi"].ToString(), k["Soyadi"].ToString(), Convert.ToInt32(k["Sinifi"]),k["Sube"].ToString(), Convert.ToInt32(k["SinavId"])));
        }
        return karne;
    }
    public CkKarneKutukInfo KayitBilgiGetir(string cmdText, params MySqlParameter[] param)
    {
        MySqlDataReader dr = helper.ExecuteReader(cmdText, param);
        CkKarneKutukInfo info = new CkKarneKutukInfo();
        while (dr.Read())
        {
            info.Id = dr.GetMySayi("Id");
            info.OpaqId = dr.GetMySayi("OpaqId");
            info.IlAdi = dr.GetMyMetin("IlAdi");
            info.IlceAdi = dr.GetMyMetin("IlceAdi");
            info.KurumKodu = dr.GetMySayi("KurumKodu");
            info.KurumAdi = dr.GetMyMetin("KurumAdi");
            info.OgrenciNo = dr.GetMySayi("OgrenciNo");
            info.Adi = dr.GetMyMetin("Adi");
            info.Soyadi = dr.GetMyMetin("Soyadi");
            info.Sinifi = dr.GetMySayi("Sinifi");
            info.Sube = dr.GetMyMetin("Sube");
            info.SinavId = dr.GetMySayi("SinavId");
            info.DersKodu = dr.GetMySayi("DersKodu");
        }
        dr.Close();

        return info;
    }

    public CkKarneKutukInfo KayitBilgiGetir(int id)
    {
        string cmdText = "select * from ckkarnekutuk where Id=?Id";
        MySqlParameter param = new MySqlParameter("?Id", MySqlDbType.Int32) { Value = id };
        MySqlDataReader dr = helper.ExecuteReader(cmdText, param);
        CkKarneKutukInfo info = new CkKarneKutukInfo();
        while (dr.Read())
        {
            info.Id = dr.GetMySayi("Id");
            info.OpaqId = dr.GetMySayi("OpaqId");
            info.IlAdi = dr.GetMyMetin("IlAdi");
            info.IlceAdi = dr.GetMyMetin("IlceAdi");
            info.KurumKodu = dr.GetMySayi("KurumKodu");
            info.KurumAdi = dr.GetMyMetin("KurumAdi");
            info.OgrenciNo = dr.GetMySayi("OgrenciNo");
            info.Adi = dr.GetMyMetin("Adi");
            info.Soyadi = dr.GetMyMetin("Soyadi");
            info.Sinifi = dr.GetMySayi("Sinifi");
            info.Sube = dr.GetMyMetin("Sube");
            info.SinavId = dr.GetMySayi("SinavId");
            info.DersKodu = dr.GetMySayi("DersKodu");
        }
        dr.Close();

        return info;
    }

    public void KayitSil(int id)
    {
        const string sql = "delete from ckkarnekutuk where Id=?Id";
        MySqlParameter p = new MySqlParameter("?Id", MySqlDbType.Int32) { Value = id };
        helper.ExecuteNonQuery(sql, p);
    }

    public void KayitEkle(CkKarneKutukInfo info)
    {
        const string sql = @"insert into ckkarnekutuk (OpaqId,IlAdi,IlceAdi,KurumKodu,KurumAdi,OgrenciNo,Adi,Soyadi,Sinifi,Sube,SinavId,DersKodu) values (?OpaqId,?IlAdi,?IlceAdi,?KurumKodu,?KurumAdi,?OgrenciNo,?Adi,?Soyadi,?Sinifi,?Sube,?SinavId,?DersKodu)";
        MySqlParameter[] pars =
        {
         new MySqlParameter("?OpaqId", MySqlDbType.Int32),
         new MySqlParameter("?IlAdi", MySqlDbType.String),
         new MySqlParameter("?IlceAdi", MySqlDbType.String),
         new MySqlParameter("?KurumKodu", MySqlDbType.Int32),
         new MySqlParameter("?KurumAdi", MySqlDbType.String),
         new MySqlParameter("?OgrenciNo", MySqlDbType.Int32),
         new MySqlParameter("?Adi", MySqlDbType.String),
         new MySqlParameter("?Soyadi", MySqlDbType.String),
         new MySqlParameter("?Sinifi", MySqlDbType.Int32),
         new MySqlParameter("?Sube", MySqlDbType.String),
         new MySqlParameter("?SinavId", MySqlDbType.Int32),
         new MySqlParameter("?DersKodu", MySqlDbType.Int32),
        };
        pars[0].Value = info.OpaqId;
        pars[1].Value = info.IlAdi;
        pars[2].Value = info.IlceAdi;
        pars[3].Value = info.KurumKodu;
        pars[4].Value = info.KurumAdi;
        pars[5].Value = info.OgrenciNo;
        pars[6].Value = info.Adi;
        pars[7].Value = info.Soyadi;
        pars[8].Value = info.Sinifi;
        pars[9].Value = info.Sube;
        pars[10].Value = info.SinavId;
        pars[11].Value = info.DersKodu;
        helper.ExecuteNonQuery(sql, pars);
    }

    public void KayitGuncelle(CkKarneKutukInfo info)
    {
        const string sql = @"update ckkarnekutuk set OpaqId=?OpaqId,IlAdi=?IlAdi,IlceAdi=?IlceAdi,KurumKodu=?KurumKodu,KurumAdi=?KurumAdi,OgrenciNo=?OgrenciNo,Adi=?Adi,Soyadi=?Soyadi,Sinifi=?Sinifi,Sube=?Sube,SinavId=?SinavId,DersKodu=?DersKodu where Id=?Id";
        MySqlParameter[] pars =
        {
 new MySqlParameter("?OpaqId", MySqlDbType.Int32),
 new MySqlParameter("?IlAdi", MySqlDbType.String),
 new MySqlParameter("?IlceAdi", MySqlDbType.String),
 new MySqlParameter("?KurumKodu", MySqlDbType.Int32),
 new MySqlParameter("?KurumAdi", MySqlDbType.String),
 new MySqlParameter("?OgrenciNo", MySqlDbType.Int32),
 new MySqlParameter("?Adi", MySqlDbType.String),
 new MySqlParameter("?Soyadi", MySqlDbType.String),
 new MySqlParameter("?Sinifi", MySqlDbType.Int32),
 new MySqlParameter("?Sube", MySqlDbType.String),
 new MySqlParameter("?SinavId", MySqlDbType.Int32),
 new MySqlParameter("?DersKodu", MySqlDbType.Int32),
 new MySqlParameter("?Id", MySqlDbType.Int32),
};
        pars[0].Value = info.OpaqId;
        pars[1].Value = info.IlAdi;
        pars[2].Value = info.IlceAdi;
        pars[3].Value = info.KurumKodu;
        pars[4].Value = info.KurumAdi;
        pars[5].Value = info.OgrenciNo;
        pars[6].Value = info.Adi;
        pars[7].Value = info.Soyadi;
        pars[8].Value = info.Sinifi;
        pars[9].Value = info.Sube;
        pars[10].Value = info.SinavId;
        pars[11].Value = info.DersKodu;
        pars[12].Value = info.Id;
        helper.ExecuteNonQuery(sql, pars);
    }
}

