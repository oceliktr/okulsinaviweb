using MySql.Data.MySqlClient;
using System.Data;

public class TestDonemInfo
{
    public int Id { get; set; }
    public string Donem { get; set; }
    public int Aktif { get; set; }
    public int VeriGirisi { get; set; }
}
public class TestDonemDb
{

    private readonly HelperDb _helper = new HelperDb();

    public DataTable KayitlariGetir()
    {
        const string sql = "select * from testdonemler order by Id asc";
        return _helper.ExecuteDataSet(sql).Tables[0];
    }

    public TestDonemInfo KayitBilgiGetir(string cmdText, params MySqlParameter[] param)
    {
        MySqlDataReader dr = _helper.ExecuteReader(cmdText, param);
        return TabloAlanlar(dr);
    }

    public TestDonemInfo KayitBilgiGetir(int id)
    {
        string sql = "select * from testdonemler where Id=?Id";
        MySqlParameter p = new MySqlParameter("?Id", MySqlDbType.Int32) { Value = id };
        MySqlDataReader dr = _helper.ExecuteReader(sql, p);

        return TabloAlanlar(dr);
    }

    private static TestDonemInfo TabloAlanlar(MySqlDataReader dr)
    {
        TestDonemInfo info = new TestDonemInfo();
        while (dr.Read())
        {
            info.Id = dr.GetMySayi("Id");
            info.Donem = dr.GetMyMetin("Donem");
            info.Aktif = dr.GetMySayi("Aktif");
            info.VeriGirisi = dr.GetMySayi("VeriGirisi");
        }

        dr.Close();
        return info;
    }

    public TestDonemInfo AktifDonem()
    {
        string sql = "SELECT * from testdonemler WHERE Aktif=1";
        MySqlDataReader dr = _helper.ExecuteReader(sql);

        return TabloAlanlar(dr);
    }
  
    public void KayitSil(int id)
    {
        const string sql = "delete from testdonemler where Id=?Id";
        MySqlParameter p = new MySqlParameter("?Id", MySqlDbType.Int32) { Value = id };
        _helper.ExecuteNonQuery(sql, p);
    }

    public void KayitEkle(TestDonemInfo info)
    {
        const string sql = @"insert into testdonemler (Donem) values (?Donem)";
        MySqlParameter[] pars =
        {
            new MySqlParameter("?Donem", MySqlDbType.String)
        };
        pars[0].Value = info.Donem;
        _helper.ExecuteNonQuery(sql, pars);
    }
    public void KayitGuncelle(TestDonemInfo info)
    {
        const string sql = @"update testdonemler set Donem=?Donem where Id=?Id";
        MySqlParameter[] pars =
            {
             new MySqlParameter("?Donem", MySqlDbType.String),
             new MySqlParameter("?Id", MySqlDbType.Int32)
            };
        pars[0].Value = info.Donem;
        pars[1].Value = info.Id;
        _helper.ExecuteNonQuery(sql, pars);
    }

    public void AktifDonem(int id,int veriGirisi)
    {
        _helper.ExecuteNonQuery("update testdonemler set Aktif=0,VeriGirisi=0");

        const string sql = @"update testdonemler set Aktif=1,VeriGirisi=?VeriGirisi where Id=?Id";
        MySqlParameter[] pars =
        {
            new MySqlParameter("?VeriGirisi", MySqlDbType.Int32),
            new MySqlParameter("?Id", MySqlDbType.Int32)
        };
        pars[0].Value = veriGirisi;
        pars[1].Value = id;
        _helper.ExecuteNonQuery(sql, pars);
    }


}