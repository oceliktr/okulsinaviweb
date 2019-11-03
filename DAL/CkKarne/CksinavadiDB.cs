using System.Data;
using DAL;
using MySql.Data.MySqlClient;
public class CkSinavAdiInfo
{
    public int Id { get; set; }
    public int SinavId { get; set; }
    public int Aktif { get; set; }
    public string SinavAdi { get; set; }
}

public class CkSinavAdiDB
{
    readonly HelperDb helper = new HelperDb();

    public DataTable KayitlariGetir()
    {
        const string sql = "select * from cksinavadi order by Id asc";
        return helper.ExecuteDataSet(sql).Tables[0];
    }
    public CkSinavAdiInfo AktifSinavAdi()
    {
        string sql ="select * from cksinavadi where Aktif=1";
        MySqlDataReader dr = helper.ExecuteReader(sql);
        CkSinavAdiInfo info = new CkSinavAdiInfo();
        while (dr.Read())
        {
            info.Id = dr.GetMySayi("Id");
            info.Aktif = dr.GetMySayi("Aktif");
            info.SinavId = dr.GetMySayi("SinavId");
            info.SinavAdi = dr.GetMyMetin("SinavAdi");
        }
        dr.Close();

        return info;
    }
    public CkSinavAdiInfo KayitBilgiGetir(string cmdText, params MySqlParameter[] param)
    {
        MySqlDataReader dr = helper.ExecuteReader(cmdText, param);
        CkSinavAdiInfo info = new CkSinavAdiInfo();
        while (dr.Read())
        {
            info.Id = dr.GetMySayi("Id");
            info.Aktif = dr.GetMySayi("Aktif");
            info.SinavId = dr.GetMySayi("SinavId");
            info.SinavAdi = dr.GetMyMetin("SinavAdi");
        }
        dr.Close();

        return info;
    }

    public CkSinavAdiInfo KayitBilgiGetir(int sinavId)
    {
        string cmdText = "select * from cksinavadi where SinavId=?SinavId";
        MySqlParameter param = new MySqlParameter("?SinavId", MySqlDbType.Int32) { Value = sinavId };
        MySqlDataReader dr = helper.ExecuteReader(cmdText, param);
        CkSinavAdiInfo info = new CkSinavAdiInfo();
        while (dr.Read())
        {
            info.Id = dr.GetMySayi("Id");
            info.Aktif = dr.GetMySayi("Aktif");
            info.SinavId = dr.GetMySayi("SinavId");
            info.SinavAdi = dr.GetMyMetin("SinavAdi");
        }
        dr.Close();

        return info;
    }

    public void KayitSil(int id)
    {
        const string sql = "delete from cksinavadi where Id=?Id";
        MySqlParameter p = new MySqlParameter("?Id", MySqlDbType.Int32) { Value = id };
        helper.ExecuteNonQuery(sql, p);
    }

    public void KayitEkle(CkSinavAdiInfo info)
    {
        const string sql = @"insert into cksinavadi (SinavId,SinavAdi) values (?SinavId,?SinavAdi)";
        MySqlParameter[] pars =
        {
         new MySqlParameter("?SinavId", MySqlDbType.Int32),
         new MySqlParameter("?SinavAdi", MySqlDbType.String),
        };
        pars[0].Value = info.SinavId;
        pars[1].Value = info.SinavAdi;
        helper.ExecuteNonQuery(sql, pars);
    }

    public void KayitGuncelle(CkSinavAdiInfo info)
    {
        const string sql = @"update cksinavadi set SinavId=?SinavId,SinavAdi=?SinavAdi where Id=?Id";
        MySqlParameter[] pars =
        {
         new MySqlParameter("?SinavId", MySqlDbType.Int32),
         new MySqlParameter("?SinavAdi", MySqlDbType.String),
         new MySqlParameter("?Id", MySqlDbType.Int32),
        };
        pars[0].Value = info.SinavId;
        pars[1].Value = info.SinavAdi;
        pars[2].Value = info.Id;
        helper.ExecuteNonQuery(sql, pars);
    }
}

