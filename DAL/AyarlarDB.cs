using System.Data;
using MySql.Data.MySqlClient;

namespace DAL
{


    public class AyarlarInfo
    {
        public int Id { get; set; }
        public string SiteAdi { get; set; }
        public string Description { get; set; }
        public string Keywords { get; set; }
        public string SiteAdres { get; set; }
        public string SiteTelefon { get; set; }
        public string SiteFax { get; set; }
        public string EpostaServer { get; set; }
        public string EpostaGonderenAdres { get; set; }
        public string EpostaReply { get; set; }
        public string EpostaPass { get; set; }
        public string EpostaGonderenIsmi { get; set; }
        public int EpostaSsl { get; set; }
        public int EpostaPort { get; set; }
        public string EpostaSiteAdres { get; set; }
        public string EpostaAliciAdres { get; set; }
        public int SinavId { get; set; }
        public int VeriGirisi { get; set; }
        public AyarlarInfo()
        {
        }
                                                                                                                                                                                                                                                                                        
        public AyarlarInfo(int id, string siteAdi, string description, string keywords, string siteAdres, string siteTelefon,  string siteFax, string epostaServer, string epostaGonderenAdres, string epostaReply, string epostaPass, string epostaGonderenIsmi, int epostaSsl, int epostaPort, string epostaSiteAdres, string epostaAliciAdres)
        {
            Id = id;
            SiteAdi = siteAdi;
            Description = description;
            Keywords = keywords;
            SiteAdres = siteAdres;
            SiteTelefon = siteTelefon;
            SiteFax = siteFax;
            EpostaServer = epostaServer;
            EpostaGonderenAdres = epostaGonderenAdres;
            EpostaReply = epostaReply;
            EpostaPass = epostaPass;
            EpostaGonderenIsmi = epostaGonderenIsmi;
            EpostaSsl = epostaSsl;
            EpostaPort = epostaPort;
            EpostaSiteAdres = epostaSiteAdres;
            EpostaAliciAdres = epostaAliciAdres;
        }
    }
    public class AyarlarDb
    {
        readonly HelperDb _helper = new HelperDb();

        public DataTable KayitlariGetir()
        {
            const string sql = "select * from ayarlar order by Id asc";
            return _helper.ExecuteDataSet(sql).Tables[0];
        }
        public AyarlarInfo KayitBilgiGetir(string cmdText, params MySqlParameter[] param)
        {
            MySqlDataReader dr = _helper.ExecuteReader(cmdText, param);
            AyarlarInfo info = new AyarlarInfo();
            while (dr.Read())
            {
                info.Id = dr.GetMySayi("Id");
                info.SiteAdi = dr.GetMyMetin("SiteAdi");
                info.Description = dr.GetMyMetin("Description");
                info.Keywords = dr.GetMyMetin("Keywords");
                info.SiteAdres = dr.GetMyMetin("SiteAdres");
                info.SiteTelefon = dr.GetMyMetin("SiteTelefon");
                info.SiteFax = dr.GetMyMetin("SiteFax");
                info.EpostaServer = dr.GetMyMetin("EpostaServer");
                info.EpostaGonderenAdres = dr.GetMyMetin("EpostaGonderenAdres");
                info.EpostaReply = dr.GetMyMetin("EpostaReply");
                info.EpostaPass = dr.GetMyMetin("EpostaPass");
                info.EpostaGonderenIsmi = dr.GetMyMetin("EpostaGonderenIsmi");
                info.EpostaSsl = dr.GetMySayi("EpostaSSL");
                info.EpostaPort = dr.GetMySayi("EpostaPort");
                info.EpostaSiteAdres = dr.GetMyMetin("EpostaSiteAdres");
                info.EpostaAliciAdres = dr.GetMyMetin("EpostaAliciAdres");
                info.SinavId = dr.GetMySayi("SinavId");
                info.VeriGirisi = dr.GetMySayi("VeriGirisi");
            }
            dr.Close();

            return info;
        }
        public AyarlarInfo KayitBilgiGetir(int id)
        {
            string sql = "select * from Ayarlar where Id=?Id";
            MySqlParameter p = new MySqlParameter("?Id", MySqlDbType.Int32) { Value = id };
            MySqlDataReader dr = _helper.ExecuteReader(sql, p);
            AyarlarInfo info = new AyarlarInfo();
            while (dr.Read())
            {
                info.Id = dr.GetMySayi("Id");
                info.SiteAdi = dr.GetMyMetin("SiteAdi");
                info.Description = dr.GetMyMetin("Description");
                info.Keywords = dr.GetMyMetin("Keywords");
                info.SiteAdres = dr.GetMyMetin("SiteAdres");
                info.SiteTelefon = dr.GetMyMetin("SiteTelefon");
                info.SiteFax = dr.GetMyMetin("SiteFax");
                info.EpostaServer = dr.GetMyMetin("EpostaServer");
                info.EpostaGonderenAdres = dr.GetMyMetin("EpostaGonderenAdres");
                info.EpostaReply = dr.GetMyMetin("EpostaReply");
                info.EpostaPass = dr.GetMyMetin("EpostaPass");
                info.EpostaGonderenIsmi = dr.GetMyMetin("EpostaGonderenIsmi");
                info.EpostaSsl = dr.GetMySayi("EpostaSSL");
                info.EpostaPort = dr.GetMySayi("EpostaPort");
                info.EpostaSiteAdres = dr.GetMyMetin("EpostaSiteAdres");
                info.EpostaAliciAdres = dr.GetMyMetin("EpostaAliciAdres");
                info.SinavId = dr.GetMySayi("SinavId");
                info.VeriGirisi = dr.GetMySayi("VeriGirisi");
            }
            dr.Close();

            return info;
        }

        public void KayitSil(int id)
        {
            const string sql = "delete from Ayarlar where Id=?Id";
            MySqlParameter p = new MySqlParameter("?Id", MySqlDbType.Int32) { Value = id };
            _helper.ExecuteNonQuery(sql, p);
        }

 
        public void KayitGuncelle(AyarlarInfo info)
        {
            const string sql = @"update Ayarlar set SiteAdi=?SiteAdi,Description=?Description,Keywords=?Keywords,SiteAdres=?SiteAdres,SiteTelefon=?SiteTelefon,SiteFax=?SiteFax,EpostaServer=?EpostaServer,EpostaGonderenAdres=?EpostaGonderenAdres,EpostaReply=?EpostaReply,EpostaPass=?EpostaPass,EpostaGonderenIsmi=?EpostaGonderenIsmi,EpostaSSL=?EpostaSSL,EpostaPort=?EpostaPort,EpostaSiteAdres=?EpostaSiteAdres,EpostaAliciAdres=?EpostaAliciAdres where Id=?Id";
            MySqlParameter[] pars = 
            {
             new MySqlParameter("?SiteAdi", MySqlDbType.String),
             new MySqlParameter("?Description", MySqlDbType.String),
             new MySqlParameter("?Keywords", MySqlDbType.String),
             new MySqlParameter("?SiteAdres", MySqlDbType.String),
             new MySqlParameter("?SiteTelefon", MySqlDbType.String),
             new MySqlParameter("?SiteFax", MySqlDbType.String),
             new MySqlParameter("?EpostaServer", MySqlDbType.String),
             new MySqlParameter("?EpostaGonderenAdres", MySqlDbType.String),
             new MySqlParameter("?EpostaReply", MySqlDbType.String),
             new MySqlParameter("?EpostaPass", MySqlDbType.String),
             new MySqlParameter("?EpostaGonderenIsmi", MySqlDbType.String),
             new MySqlParameter("?EpostaSSL", MySqlDbType.Int32),
             new MySqlParameter("?EpostaPort", MySqlDbType.Int32),
             new MySqlParameter("?EpostaSiteAdres", MySqlDbType.String),
             new MySqlParameter("?EpostaAliciAdres", MySqlDbType.String),
             new MySqlParameter("?Id", MySqlDbType.Int32)
            };
            pars[0].Value = info.SiteAdi;
            pars[1].Value = info.Description;
            pars[2].Value = info.Keywords;
            pars[3].Value = info.SiteAdres;
            pars[4].Value = info.SiteTelefon;
            pars[5].Value = info.SiteFax;
            pars[6].Value = info.EpostaServer;
            pars[7].Value = info.EpostaGonderenAdres;
            pars[8].Value = info.EpostaReply;
            pars[9].Value = info.EpostaPass;
            pars[10].Value = info.EpostaGonderenIsmi;
            pars[11].Value = info.EpostaSsl;
            pars[12].Value = info.EpostaPort;
            pars[13].Value = info.EpostaSiteAdres;
            pars[14].Value = info.EpostaAliciAdres;
            pars[15].Value = info.Id;
            _helper.ExecuteNonQuery(sql, pars);
        }
        public void KayitGuncelle(int aktifDonem,int veriGirisi)
        {
            const string sql = @"update ayarlar set SinavId=?SinavId,VeriGirisi=?VeriGirisi where Id=1";
            MySqlParameter[] pars =
            {
                new MySqlParameter("?SinavId", MySqlDbType.Int32) ,
                new MySqlParameter("?VeriGirisi", MySqlDbType.Int32) 
            };
            pars[0].Value = aktifDonem;
            pars[1].Value = veriGirisi;
            _helper.ExecuteNonQuery(sql, pars);
        }
    }

}

