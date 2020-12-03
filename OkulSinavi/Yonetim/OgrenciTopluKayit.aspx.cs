using System;
using System.Collections.Generic;
using System.Web.Services;
using System.Web.UI.WebControls;
using Newtonsoft.Json;

public partial class OkulSinavi_CevrimiciSinavYonetim_OgrenciTopluKayit : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            if (YetkiKontrol())
            {
                Response.Redirect("Default.aspx");
            }

            IlcelerDb ilcelerDb = new IlcelerDb();
            ddlIlce.DataSource = ilcelerDb.KayitlariGetir();
            ddlIlce.DataValueField = "IlceAdi";
            ddlIlce.DataTextField = "IlceAdi";
            ddlIlce.DataBind();
            ddlIlce.Items.Insert(0, new ListItem("İlçe Seçiniz", ""));
            ddlKurum.Items.Insert(0, new ListItem("İlçe Seçiniz", ""));

            

            ddlSinif.Attributes.Add("onchange", "sinifDegis()");
            if (Master.Yetki().Contains("OkulYetkilisi"))
            {
                TestDonemDb dnmDb = new TestDonemDb();
                var donem = dnmDb.AktifDonem();

                if (donem.VeriGirisi == 0)
                {
                    kayitForm.Visible = false;
                    Master.UyariTuruncu("Şuan için kayıt işlemleri kapatılmıştır.", phUyari);
                }
                phOkulIlce.Visible = false;
            }
        }
    }

    private static bool YetkiKontrol()
    {
        OturumIslemleri oturum = new OturumIslemleri();
        KullanicilarInfo kInfo = oturum.OturumKontrol();
        bool yetkili = !kInfo.Yetki.Contains("Root") && !kInfo.Yetki.Contains("Admin") && !kInfo.Yetki.Contains("OkulYetkilisi");
        return yetkili;
    }

    private int IlceBilgisi(string ilceAdi)
    {
        IlcelerDb ilcelerDb = new IlcelerDb();
        var ilce = ilcelerDb.KayitBilgiGetir(ilceAdi);
        return ilce.Id;
    }

    private void KurumlariGetir(int ilce)
    {
        KurumlarDb veriDb = new KurumlarDb();
        ddlKurum.DataSource = veriDb.OkullariGetir(ilce);
        ddlKurum.DataValueField = "KurumKodu";
        ddlKurum.DataTextField = "KurumAdi";
        ddlKurum.DataBind();
        ddlKurum.Items.Insert(0, new ListItem("Kurum Seçiniz", ""));
    }

    protected void ddlIlce_OnSelectedIndexChanged(object sender, EventArgs e)
    {
        string ilce = ddlIlce.SelectedValue;
        KurumlariGetir(IlceBilgisi(ilce));
    }

    [WebMethod]
    public static string KayitKontrol(string veri)
    {


        try
        {
            string[] stringSeparators = new string[] { "\n" };
            string[] veriler = veri.Split(stringSeparators, StringSplitOptions.None);
            List<OgrenciKayitModel> list = new List<OgrenciKayitModel>();

            foreach (var t in veriler)
            {
                if (string.IsNullOrEmpty(t))
                    continue;

                string sonuc = "";
                string tcKimlik = t.Split("	".ToCharArray())[0].Trim();
                string adi = t.Split("	".ToCharArray())[1].ToUpper().Trim();
                string soyadi = t.Split("	".ToCharArray())[2].ToUpper().Trim();
                string sube = t.Split("	".ToCharArray())[3].ToUpper().Trim();



                if (!tcKimlik.IsInteger())
                    sonuc += "Tc Kimlik numarasında numerik olmayan karakterler var. ";
                if (tcKimlik.Length != 11)
                    sonuc += "Tc Kimlik numarasında 11 karakter olmalı. ";
                if (adi.Length < 3)
                    sonuc += "Adı geçerli değil. ";
                if (soyadi.Length < 2)
                    sonuc += "Soyadı geçerli değil. ";
                if (sube.Length > 2 || sube == "")
                    sonuc += "Şube geçerli değil. ";
                
                string ss = "";
                foreach (var s in sube)
                {
                    if (s.ToString().IsInteger())
                    {
                        ss = "Şube alanında sayısal değer var. ";
                    }
                }
                if (ss != "")
                    sonuc += ss;


                list.Add(new OgrenciKayitModel()
                {
                    TcKimlik = tcKimlik,
                    Adi = adi,
                    Soyadi = soyadi,
                    Sube = sube,
                    Sonuc = sonuc
                });
            }

            return JsonConvert.SerializeObject(list);
        }
        catch (Exception)
        {

            return JsonConvert.SerializeObject(new { Mesaj = "Listeniz geçerli değil. Lütfen yönerge doğrultusunda tekrar deneyiniz.", Sonuc = "no" });
        }
    }
    [WebMethod]
    public static string TopluKayit(string sinif, string ilce, string kurumkodu, string veri)
    {
        TestDonemDb dnmDb = new TestDonemDb();
        int donem = dnmDb.AktifDonem().Id;

        string ilceAdi = ilce;
        int okulKurumKodu = kurumkodu.ToInt32();

        OturumIslemleri oturum = new OturumIslemleri();
        KullanicilarInfo kInfo = oturum.OturumKontrol();
        if (kInfo.Yetki.Contains("OkulYetkilisi"))
        {
            IlcelerDb ilcelerDb = new IlcelerDb();

            okulKurumKodu = kInfo.KurumKodu.ToInt32();
            ilceAdi = ilcelerDb.IlceAdi(kInfo.IlceId);
        }

        int eklenenKayitSayisi = 0;


        List<OgrenciKayitModel> list = new List<OgrenciKayitModel>();

        string[] stringSeparators = new string[] { "\n" };
        string[] veriler = veri.Split(stringSeparators, StringSplitOptions.None);

        foreach (var t in veriler)
        {
            if (string.IsNullOrEmpty(t))
                continue;

            string sonuc = "";
            string tcKimlik = t.Split("	".ToCharArray())[0].Trim();
            string adi = t.Split("	".ToCharArray())[1].ToUpper().Trim();
            string soyadi = t.Split("	".ToCharArray())[2].ToUpper().Trim();
            string sube = t.Split("	".ToCharArray())[3].ToUpper().Trim();


            list.Add(new OgrenciKayitModel()
            {
                TcKimlik = tcKimlik,
                Adi = adi,
                Soyadi = soyadi,
                Sube = sube,
                Sonuc = sonuc
            });
        }
        foreach (var t in list)
        {
            string opaqId = t.TcKimlik.Md5Sifrele();


            TestKutukDb veriDb = new TestKutukDb();
            var kontrol = veriDb.KayitKontrol(donem, opaqId, 0);
            if (kontrol)
            {
                return JsonConvert.SerializeObject(new { Mesaj = t.TcKimlik + " tc kimlikli öğrenci daha önce kaydedilmiş.\n Öğrenci listesi sayfasında görünmüyorsa sistem yöneticisi ile iletişime geçiniz.", Sonuc = "no" });
            }

            TestKutukInfo info = new TestKutukInfo
            {
                OpaqId = opaqId,
                Adi = t.Adi,
                Soyadi = t.Soyadi,
                IlceAdi = ilceAdi,
                KurumKodu = okulKurumKodu,
                Sinifi = sinif.ToInt32(),
                Sube = t.Sube,
                DonemId = donem
            };
            if (veriDb.KayitEkle(info) > 0)
            {
                eklenenKayitSayisi++;
            }
        }

        return JsonConvert.SerializeObject(new { Mesaj = eklenenKayitSayisi + " öğrenci kaydedildi.", Sonuc = "ok" });
    }
}

