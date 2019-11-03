using System.Data;
using DAL;
using MySql.Data.MySqlClient;
public class OgrenciKarneInfo
{
    public int Id { get; set; }
    public int SinavId { get; set; }
    public int KurumKodu { get; set; }
    public int Sinif { get; set; }
    public string Sube { get; set; }
    public int OgrenciId { get; set; }
    public int BransId { get; set; }
    public int DogruSayisi { get; set; }
    public int YanlisSayisi { get; set; }
    public int Bos { get; set; }
    public string KitapcikTuru { get; set; }
}

public class OgrenciKarneDB
{
    readonly HelperDb helper = new HelperDb();

    public DataTable KayitlariGetir()
    {
        const string sql = "select * from ogrencikarne order by Id asc";
        return helper.ExecuteDataSet(sql).Tables[0];
    }
    public DataTable KayitlariGetir(int sinavId)
    {
        const string sql = "SELECT DISTINCT(OgrenciId) FROM ogrencikarne where SinavId=?SinavId";
        MySqlParameter p = new MySqlParameter("?SinavId", MySqlDbType.Int32) { Value = sinavId };
        return helper.ExecuteDataSet(sql,p).Tables[0];
    }
    public OgrenciKarneInfo KayitBilgiGetir(string cmdText, params MySqlParameter[] param)
    {
        MySqlDataReader dr = helper.ExecuteReader(cmdText, param);
        OgrenciKarneInfo info = new OgrenciKarneInfo();
        while (dr.Read())
        {
            info.Id = dr.GetMySayi("Id");
            info.SinavId = dr.GetMySayi("SinavId");
            info.KurumKodu = dr.GetMySayi("KurumKodu");
            info.Sinif = dr.GetMySayi("Sinif");
            info.Sube = dr.GetMyMetin("Sube");
            info.OgrenciId = dr.GetMySayi("OgrenciId");
            info.BransId = dr.GetMySayi("BransId");
            info.DogruSayisi = dr.GetMySayi("DogruSayisi");
            info.YanlisSayisi = dr.GetMySayi("YanlisSayisi");
            info.Bos = dr.GetMySayi("Bos");
            info.KitapcikTuru = dr.GetMyMetin("KitapcikTuru");
        }
        dr.Close();

        return info;
    }

    public OgrenciKarneInfo KayitBilgiGetir(int sinavId,int bransId,int ogrenciId)
    {
        string cmdText = "select * from ogrencikarne where SinavId=?SinavId and OgrenciId=?OgrenciId and BransId=?BransId";
        MySqlParameter[] pars =
        {
            new MySqlParameter("?SinavId", MySqlDbType.Int32),
            new MySqlParameter("?BransId", MySqlDbType.Int32),
            new MySqlParameter("?OgrenciId", MySqlDbType.Int32)
        };
        pars[0].Value = sinavId;
        pars[1].Value = bransId;
        pars[2].Value = ogrenciId;
        MySqlDataReader dr = helper.ExecuteReader(cmdText, pars);
        OgrenciKarneInfo info = new OgrenciKarneInfo();
        while (dr.Read())
        {
            info.Id = dr.GetMySayi("Id");
            info.SinavId = dr.GetMySayi("SinavId");
            info.KurumKodu = dr.GetMySayi("KurumKodu");
            info.Sinif = dr.GetMySayi("Sinif");
            info.Sube = dr.GetMyMetin("Sube");
            info.OgrenciId = dr.GetMySayi("OgrenciId");
            info.BransId = dr.GetMySayi("BransId");
            info.DogruSayisi = dr.GetMySayi("DogruSayisi");
            info.YanlisSayisi = dr.GetMySayi("YanlisSayisi");
            info.Bos = dr.GetMySayi("Bos");
            info.KitapcikTuru = dr.GetMyMetin("KitapcikTuru");
        }
        dr.Close();

        return info;
    }

    public void KayitSil(int sinavId)
    {
        const string sql = "delete from ogrencikarne where SinavId=?SinavId";
        MySqlParameter p = new MySqlParameter("?SinavId", MySqlDbType.Int32) { Value = sinavId };
        helper.ExecuteNonQuery(sql, p);
    }

    public void KayitEkle(OgrenciKarneInfo info)
    {
        const string sql = @"insert into ogrencikarne (SinavId,KurumKodu,Sinif,Sube,OgrenciId,BransId,DogruSayisi,YanlisSayisi,Bos,KitapcikTuru) values (?SinavId,?KurumKodu,?Sinif,?Sube,?OgrenciId,?BransId,?DogruSayisi,?YanlisSayisi,?Bos,?KitapcikTuru)";
        MySqlParameter[] pars =
        {
         new MySqlParameter("?SinavId", MySqlDbType.Int32),
         new MySqlParameter("?KurumKodu", MySqlDbType.Int32),
         new MySqlParameter("?Sinif", MySqlDbType.Int32),
         new MySqlParameter("?Sube", MySqlDbType.String),
         new MySqlParameter("?OgrenciId", MySqlDbType.Int32),
         new MySqlParameter("?BransId", MySqlDbType.Int32),
         new MySqlParameter("?DogruSayisi", MySqlDbType.Int32),
         new MySqlParameter("?YanlisSayisi", MySqlDbType.Int32),
         new MySqlParameter("?Bos", MySqlDbType.Int32),
         new MySqlParameter("?KitapcikTuru", MySqlDbType.String),
        };
        pars[0].Value = info.SinavId;
        pars[1].Value = info.KurumKodu;
        pars[2].Value = info.Sinif;
        pars[3].Value = info.Sube;
        pars[4].Value = info.OgrenciId;
        pars[5].Value = info.BransId;
        pars[6].Value = info.DogruSayisi;
        pars[7].Value = info.YanlisSayisi;
        pars[8].Value = info.Bos;
        pars[9].Value = info.KitapcikTuru;
        helper.ExecuteNonQuery(sql, pars);
    }

    public void KayitGuncelle(OgrenciKarneInfo info)
    {
        const string sql = @"update ogrencikarne set SinavId=?SinavId,KurumKodu=?KurumKodu,Sinif=?Sinif,Sube=?Sube,OgrenciId=?OgrenciId,BransId=?BransId,DogruSayisi=?DogruSayisi,YanlisSayisi=?YanlisSayisi,Bos=?Bos,KitapcikTuru=?KitapcikTuru where Id=?Id";
        MySqlParameter[] pars =
        {
 new MySqlParameter("?SinavId", MySqlDbType.Int32),
 new MySqlParameter("?KurumKodu", MySqlDbType.Int32),
 new MySqlParameter("?Sinif", MySqlDbType.Int32),
 new MySqlParameter("?Sube", MySqlDbType.String),
 new MySqlParameter("?OgrenciId", MySqlDbType.Int32),
 new MySqlParameter("?BransId", MySqlDbType.Int32),
 new MySqlParameter("?DogruSayisi", MySqlDbType.Int32),
 new MySqlParameter("?YanlisSayisi", MySqlDbType.Int32),
 new MySqlParameter("?Bos", MySqlDbType.Int32),
 new MySqlParameter("?KitapcikTuru", MySqlDbType.String),
 new MySqlParameter("?Id", MySqlDbType.Int32),
};
        pars[0].Value = info.SinavId;
        pars[1].Value = info.KurumKodu;
        pars[2].Value = info.Sinif;
        pars[3].Value = info.Sube;
        pars[4].Value = info.OgrenciId;
        pars[5].Value = info.BransId;
        pars[6].Value = info.DogruSayisi;
        pars[7].Value = info.YanlisSayisi;
        pars[8].Value = info.Bos;
        pars[9].Value = info.KitapcikTuru;
        pars[10].Value = info.Id;
        helper.ExecuteNonQuery(sql, pars);
    }
}

