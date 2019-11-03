using System.Data;
using DAL;
using MySql.Data.MySqlClient;
public class AnketInfo
{
    public int Id { get; set; }
    public string AD_SOYAD { get; set; }
    public string TCKN { get; set; }
    public string IL_ADI { get; set; }
    public string ILCE_ADI { get; set; }
    public string KURUM_KODU { get; set; }
    public string KURUM_ADI { get; set; }
    public string SINIF { get; set; }
    public string OGR_NO { get; set; }
    public string ANKETDURUMU { get; set; }
    public string YENIKURUMKODU { get; set; }
    public string YENIOKUL { get; set; }
    public string YENIILCE { get; set; }
    public string ANNEEGITIMDUZEY { get; set; }
    public string ANNEHAYATTA { get; set; }
    public string ANNEMESLEK { get; set; }
    public string BABAEGITIMDUZEY { get; set; }
    public string BABAHAYATTS { get; set; }
    public string BABAMESLEK { get; set; }
    public string AILEGELIR { get; set; }
}

public class AnketDB
{
    readonly HelperDb helper = new HelperDb();

    public DataTable KayitlariGetir()
    {
        const string sql = "select * from anket order by Id asc";
        return helper.ExecuteDataSet(sql).Tables[0];
    }
    public DataTable KayitlariGetir(string kurumKodu)
    {
        const string sql = "select * from anket where YENIKURUMKODU=?YENIKURUMKODU order by AD_SOYAD asc";
        MySqlParameter p = new MySqlParameter("?YENIKURUMKODU", MySqlDbType.String) { Value = kurumKodu };
        return helper.ExecuteDataSet(sql, p).Tables[0];
    }

    public DataTable TamamlamayanOkullar()
    {
        const string sql = "SELECT DISTINCT(`YENIKURUMKODU`),`YENIILCE`,`YENIOKUL` FROM `anket` WHERE `ANKETDURUMU`<>'Tamam'";
        return helper.ExecuteDataSet(sql).Tables[0];
    }
    public AnketInfo KayitBilgiGetir(string kurumKodu)
    {
        string cmdText = "select * from anket where YENIKURUMKODU=?YENIKURUMKODU";
        MySqlParameter p = new MySqlParameter("?YENIKURUMKODU", MySqlDbType.String) { Value = kurumKodu };
        MySqlDataReader dr = helper.ExecuteReader(cmdText, p);
        AnketInfo info = new AnketInfo();
        while (dr.Read())
        {
            info.Id = dr.GetMySayi("Id");
            info.AD_SOYAD = dr.GetMyMetin("AD_SOYAD");
            info.TCKN = dr.GetMyMetin("TCKN");
            info.IL_ADI = dr.GetMyMetin("IL_ADI");
            info.ILCE_ADI = dr.GetMyMetin("ILCE_ADI");
            info.KURUM_KODU = dr.GetMyMetin("KURUM_KODU");
            info.KURUM_ADI = dr.GetMyMetin("KURUM_ADI");
            info.SINIF = dr.GetMyMetin("SINIF");
            info.OGR_NO = dr.GetMyMetin("OGR_NO");
            info.ANKETDURUMU = dr.GetMyMetin("ANKETDURUMU");
            info.YENIKURUMKODU = dr.GetMyMetin("YENIKURUMKODU");
            info.YENIOKUL = dr.GetMyMetin("YENIOKUL");
            info.YENIILCE = dr.GetMyMetin("YENIILCE");
            info.ANNEEGITIMDUZEY = dr.GetMyMetin("ANNEEGITIMDUZEY");
            info.ANNEHAYATTA = dr.GetMyMetin("ANNEHAYATTA");
            info.ANNEMESLEK = dr.GetMyMetin("ANNEMESLEK");
            info.BABAEGITIMDUZEY = dr.GetMyMetin("BABAEGITIMDUZEY");
            info.BABAHAYATTS = dr.GetMyMetin("BABAHAYATTS");
            info.BABAMESLEK = dr.GetMyMetin("BABAMESLEK");
            info.AILEGELIR = dr.GetMyMetin("AILEGELIR");
        }
        dr.Close();

        return info;
    }

    public AnketInfo KayitBilgiGetir(int id)
    {
        string cmdText = "select * from anket where Id=?Id";
        MySqlParameter param = new MySqlParameter("?Id", MySqlDbType.Int32) { Value = id };
        MySqlDataReader dr = helper.ExecuteReader(cmdText, param);
        AnketInfo info = new AnketInfo();
        while (dr.Read())
        {
            info.Id = dr.GetMySayi("Id");
            info.AD_SOYAD = dr.GetMyMetin("AD_SOYAD");
            info.TCKN = dr.GetMyMetin("TCKN");
            info.IL_ADI = dr.GetMyMetin("IL_ADI");
            info.ILCE_ADI = dr.GetMyMetin("ILCE_ADI");
            info.KURUM_KODU = dr.GetMyMetin("KURUM_KODU");
            info.KURUM_ADI = dr.GetMyMetin("KURUM_ADI");
            info.SINIF = dr.GetMyMetin("SINIF");
            info.OGR_NO = dr.GetMyMetin("OGR_NO");
            info.ANKETDURUMU = dr.GetMyMetin("ANKETDURUMU");
            info.YENIKURUMKODU = dr.GetMyMetin("YENIKURUMKODU");
            info.YENIOKUL = dr.GetMyMetin("YENIOKUL");
            info.YENIILCE = dr.GetMyMetin("YENIILCE");
            info.ANNEEGITIMDUZEY = dr.GetMyMetin("ANNEEGITIMDUZEY");
            info.ANNEHAYATTA = dr.GetMyMetin("ANNEHAYATTA");
            info.ANNEMESLEK = dr.GetMyMetin("ANNEMESLEK");
            info.BABAEGITIMDUZEY = dr.GetMyMetin("BABAEGITIMDUZEY");
            info.BABAHAYATTS = dr.GetMyMetin("BABAHAYATTS");
            info.BABAMESLEK = dr.GetMyMetin("BABAMESLEK");
            info.AILEGELIR = dr.GetMyMetin("AILEGELIR");
        }
        dr.Close();

        return info;
    }

    public void KayitSil(int id)
    {
        const string sql = "delete from anket where Id=?Id";
        MySqlParameter p = new MySqlParameter("?Id", MySqlDbType.Int32) { Value = id };
        helper.ExecuteNonQuery(sql, p);
    }

    public void KayitEkle(AnketInfo info)
    {
        const string sql = @"insert into anket (AD_SOYAD,TCKN,IL_ADI,ILCE_ADI,KURUM_KODU,KURUM_ADI,SINIF,OGR_NO,ANKETDURUMU,YENIKURUMKODU,YENIOKUL,YENIILCE,ANNEEGITIMDUZEY,ANNEHAYATTA,ANNEMESLEK,BABAEGITIMDUZEY,BABAHAYATTS,BABAMESLEK,AILEGELIR) values (?AD_SOYAD,?TCKN,?IL_ADI,?ILCE_ADI,?KURUM_KODU,?KURUM_ADI,?SINIF,?OGR_NO,?ANKETDURUMU,?YENIKURUMKODU,?YENIOKUL,?YENIILCE,?ANNEEGITIMDUZEY,?ANNEHAYATTA,?ANNEMESLEK,?BABAEGITIMDUZEY,?BABAHAYATTS,?BABAMESLEK,?AILEGELIR)";
        MySqlParameter[] pars =
        {
 new MySqlParameter("?AD_SOYAD", MySqlDbType.String),
 new MySqlParameter("?TCKN", MySqlDbType.String),
 new MySqlParameter("?IL_ADI", MySqlDbType.String),
 new MySqlParameter("?ILCE_ADI", MySqlDbType.String),
 new MySqlParameter("?KURUM_KODU", MySqlDbType.String),
 new MySqlParameter("?KURUM_ADI", MySqlDbType.String),
 new MySqlParameter("?SINIF", MySqlDbType.String),
 new MySqlParameter("?OGR_NO", MySqlDbType.String),
 new MySqlParameter("?ANKETDURUMU", MySqlDbType.String),
 new MySqlParameter("?YENIKURUMKODU", MySqlDbType.String),
 new MySqlParameter("?YENIOKUL", MySqlDbType.String),
 new MySqlParameter("?YENIILCE", MySqlDbType.String),
 new MySqlParameter("?ANNEEGITIMDUZEY", MySqlDbType.String),
 new MySqlParameter("?ANNEHAYATTA", MySqlDbType.String),
 new MySqlParameter("?ANNEMESLEK", MySqlDbType.String),
 new MySqlParameter("?BABAEGITIMDUZEY", MySqlDbType.String),
 new MySqlParameter("?BABAHAYATTS", MySqlDbType.String),
 new MySqlParameter("?BABAMESLEK", MySqlDbType.String),
 new MySqlParameter("?AILEGELIR", MySqlDbType.String),
};
        pars[0].Value = info.AD_SOYAD;
        pars[1].Value = info.TCKN;
        pars[2].Value = info.IL_ADI;
        pars[3].Value = info.ILCE_ADI;
        pars[4].Value = info.KURUM_KODU;
        pars[5].Value = info.KURUM_ADI;
        pars[6].Value = info.SINIF;
        pars[7].Value = info.OGR_NO;
        pars[8].Value = info.ANKETDURUMU;
        pars[9].Value = info.YENIKURUMKODU;
        pars[10].Value = info.YENIOKUL;
        pars[11].Value = info.YENIILCE;
        pars[12].Value = info.ANNEEGITIMDUZEY;
        pars[13].Value = info.ANNEHAYATTA;
        pars[14].Value = info.ANNEMESLEK;
        pars[15].Value = info.BABAEGITIMDUZEY;
        pars[16].Value = info.BABAHAYATTS;
        pars[17].Value = info.BABAMESLEK;
        pars[18].Value = info.AILEGELIR;
        helper.ExecuteNonQuery(sql, pars);
    }

    public void KayitGuncelle(AnketInfo info)
    {
        const string sql = @"update anket set ANKETDURUMU=?ANKETDURUMU,ANNEEGITIMDUZEY=?ANNEEGITIMDUZEY,ANNEHAYATTA=?ANNEHAYATTA,ANNEMESLEK=?ANNEMESLEK,BABAEGITIMDUZEY=?BABAEGITIMDUZEY,BABAHAYATTS=?BABAHAYATTS,BABAMESLEK=?BABAMESLEK,AILEGELIR=?AILEGELIR where Id=?Id";
        MySqlParameter[] pars =
        {
             new MySqlParameter("?ANKETDURUMU", MySqlDbType.String),
             new MySqlParameter("?ANNEEGITIMDUZEY", MySqlDbType.String),
             new MySqlParameter("?ANNEHAYATTA", MySqlDbType.String),
             new MySqlParameter("?ANNEMESLEK", MySqlDbType.String),
             new MySqlParameter("?BABAEGITIMDUZEY", MySqlDbType.String),
             new MySqlParameter("?BABAHAYATTS", MySqlDbType.String),
             new MySqlParameter("?BABAMESLEK", MySqlDbType.String),
             new MySqlParameter("?AILEGELIR", MySqlDbType.String),
             new MySqlParameter("?Id", MySqlDbType.Int32)
        };
        pars[0].Value = info.ANKETDURUMU;
        pars[1].Value = info.ANNEEGITIMDUZEY;
        pars[2].Value = info.ANNEHAYATTA;
        pars[3].Value = info.ANNEMESLEK;
        pars[4].Value = info.BABAEGITIMDUZEY;
        pars[5].Value = info.BABAHAYATTS;
        pars[6].Value = info.BABAMESLEK;
        pars[7].Value = info.AILEGELIR;
        pars[8].Value = info.Id;
        helper.ExecuteNonQuery(sql, pars);
    }
}

