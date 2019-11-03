using DAL;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class ODM_KazanimKarne : Page
{
    public string Yetki()
    {
        string yetkiId = "";
        if (Request.Cookies["uyeCookie"] == null) return yetkiId;
        string uyeAdiCookies = Request.Cookies["uyeCookie"]["Oturum"];

        if (uyeAdiCookies == "Acik")
            yetkiId = Request.Cookies["uyeCookie"]["Yetki"];
        return yetkiId;
    }
    public int UyeId()
    {
        int uyeId = 0;
        if (Request.Cookies["uyeCookie"] == null) return uyeId;
        string uyeAdiCookies = Request.Cookies["uyeCookie"]["Oturum"];

        if (uyeAdiCookies == "Acik")
            uyeId = Request.Cookies["uyeCookie"]["UyeId"].ToInt32();
        return uyeId;
    }
    public class SinifSube
    {
        public string Sinif { get; set; }
        public string Sube { get; set; }
        public SinifSube()
        { }
        public SinifSube(string sinif, string sube)
        {
            Sinif = sinif;
            Sube = sube;
        }
    }
    private int trOrtalamaUstu;
    private int trOrtalamaAlti;
    private int trOrtalamaEsit;
    private int ilOrtalamaUstu;
    private int ilOrtalamaAlti;
    private int ilOrtalamaEsit;
    private int ilceOrtalamaUstu;
    private int ilceOrtalamaAlti;
    private int ilceOrtalamaEsit;
    private int okulOrtalamaUstu;
    private int okulOrtalamaAlti;
    private int okulOrtalamaEsit;
    private int destekleme;
    private int telafi;

     private int sinavId;
    private int ilceId;
    private string ilceAdi;
    private int kurumKodu;
    private string kurumAdi;


    readonly List<string> eksikKazanimlar = new List<string>();

    protected void Page_Load(object sender, EventArgs e)
    {
        if(UyeId()==0)
            Response.Redirect("Giris.aspx");

       
        KullanicilarDb veriDb = new KullanicilarDb();
        KullanicilarInfo info = veriDb.KayitBilgiGetir(UyeId());
        
        KurumlarDb krmDb = new KurumlarDb();
        KurumlarInfo kinfo = krmDb.KayitBilgiGetir(info.KurumKodu);
        ////İL RAPORU


        if (Yetki().Contains("Admin"))
        {
            ilceId = 0;
            kurumKodu =0;
            ilceAdi = "";
            kurumAdi = "";

            ltrOkulIlceAdi.Text = "İl Milli Eğitim Müdürlüğü";
        }
        if (Yetki().Contains("OkulYetkilisi"))
        {
            ilceId = info.IlceId;
            kurumKodu = info.KurumKodu.ToInt32();
            ilceAdi = kinfo.IlceAdi;
            kurumAdi = kinfo.KurumAdi;

            ltrOkulIlceAdi.Text = kinfo.KurumAdi;
        }
        if (Yetki().Contains("IlceMEMYetkilisi"))
        {
            IlcelerDb ilceDb = new IlcelerDb();
            IlcelerInfo ilceInfo = ilceDb.KayitBilgiGetir(info.IlceId);

            ilceId = info.IlceId;
            kurumKodu = 0;
            ilceAdi = ilceInfo.IlceAdi;
            kurumAdi = "";
            ltrOkulIlceAdi.Text = ilceInfo.IlceAdi+" İlçe Milli Eğitim Müdürlüğü Yetkilisi";
        }
       

        string grup = Grup(""); //il olup olmadığını anlamak için
        
        SinavlarDb snvDb = new SinavlarDb();
        SinavlarInfo sinfo = snvDb.AktifSinavAdi();
        sinavId = sinfo.SinavId;
        if (!IsPostBack)
        {

            RubrikDb rbrDb = new RubrikDb();
            ddlBrans.DataSource = rbrDb.SinavdakiBranslar(sinavId);
            ddlBrans.DataValueField = "BransId";
            ddlBrans.DataTextField = "BransAdi";
            ddlBrans.DataBind();
            ddlBrans.Items.Insert(0, new ListItem("Ders Seçiniz", ""));

            OgrencilerDb subeDb = new OgrencilerDb();

            List<SinifSube> lst = new List<SinifSube>();


            List<OgrencilerInfo> subeler = subeDb.SinavaGirenSubeler(sinavId, kurumKodu);
            List<OgrencilerInfo> siniflar = null;
            siniflar = kurumKodu == 0 ? subeDb.IlcedeSinavaGirenSiniflar(sinavId, ilceId) : subeDb.SinavaGirenSiniflar(sinavId, kurumKodu);

            if (grup == "il")
            {
                siniflar = subeDb.SinavaGirenSiniflar(sinavId);
                lst.AddRange(siniflar.Select(snf => new SinifSube(snf.Sinifi.ToString(), snf.Sinifi + ". sınıf il ort.")));
            }
            else
            {
                foreach (var snf in siniflar)
                {
                    lst.Add(kurumKodu == 0
                        ? new SinifSube(snf.Sinifi.ToString(), snf.Sinifi + ". sınıf ilçe ort.")
                        : new SinifSube(snf.Sinifi.ToString(), snf.Sinifi + ". sınıf okul ort."));
                }
                if (subeler.Count > 1)
                {
                    lst.AddRange(subeler.Select(sb => new SinifSube(sb.SinifSube, sb.SinifSube)));
                }
            }

            ddlSube.DataSource = lst;
            ddlSube.DataValueField = "Sinif";
            ddlSube.DataTextField = "Sube";
            ddlSube.DataBind();
        }
    }

    private string Grup(string sube)
    {
        string grup = "";
        if (ilceId == 0)
            grup = "il";
        else if (ilceId != 0 && kurumKodu == 0)
            grup = "ilce";
        else if (kurumKodu != 0 && sube == "")
            grup = "okul";
        else if (kurumKodu != 0 && sube != "")
            grup = "sube";

        return grup;
    }

    protected void btnListele_OnClick(object sender, EventArgs e)
    {
        phRapor.Visible = true;

        SinavlarDb snvDb = new SinavlarDb();
        SinavlarInfo sinfo = snvDb.AktifSinavAdi();

        yazdir.Visible = true;
        string okulIlceSubeAdi = "";
        int brans = ddlBrans.SelectedValue.ToInt32();
        string ders = ddlBrans.SelectedItem.ToString();
        int sinif = ddlSube.SelectedValue.Split('-')[0].ToInt32();
        string sube = "";
        if (ddlSube.SelectedValue.Length > 1)
            sube = ddlSube.SelectedValue.Split('-')[1];

        string grup = Grup(sube);

        ltrBaslik.Text = sinfo.DonemAdi.ToUpper()+ " - İZLEME ARAŞTIRMASI ";
        if (grup == "il")
        {
            okulIlceSubeAdi = "<strong>Erzurum</strong> ilinde";

            ltrSinif.Text = sinif + ". SINIF " + ders.ToUpper(CultureInfo.CurrentCulture).Replace("I","İ") + " DERSİ";
            ltrIlceOkulSube.Text = "ERZURUM İL KARNESİ";
      
            phIlRapor.Visible = true;
            phIlceRapor.Visible = false;
            phOkulRapor.Visible = false;
            phSubeRapor.Visible = false;

            KazanimKarneDB rbrDb = new KazanimKarneDB();
            List<KazanimKarneInfo> karne = rbrDb.IlKazanimlariGetir(sinavId, sinif, brans);

            rptKazanimKarneIl.DataSource = karne;
            rptKazanimKarneIl.DataBind();

        }
        if (grup == "ilce")
        {
            ltrSinif.Text = sinif + ". SINIF " + ders.ToUpper(CultureInfo.CurrentCulture) + " DERSİ";
            ltrIlceOkulSube.Text = ilceAdi.ToUpper(CultureInfo.CurrentCulture) + " İLÇE KARNESİ";

            okulIlceSubeAdi = "<strong>" + ilceAdi + "</strong> ilçesinde";

            phIlRapor.Visible = false;
            phIlceRapor.Visible = true;
            phOkulRapor.Visible = false;
            phSubeRapor.Visible = false;

            KurumlarDb krmDb = new KurumlarDb();
            List<KurumlarInfo> sorular = krmDb.SinavaGirenOkullariDiziyeGetir(sinavId, ilceId);
            string sqlText = "";
            foreach (var s in sorular)
            {
                sqlText += " KurumKodu=" + s.KurumKodu + " or";
            }

            KazanimKarneDB rbrDb = new KazanimKarneDB();
            List<KazanimKarneInfo> karne = rbrDb.IlceKazanimlariGetir(sinavId, sinif, brans, sqlText);

            rptKazanimKarneIlce.DataSource = karne;
            rptKazanimKarneIlce.DataBind();
        }
        else if (grup == "okul") //okul raporu
        {
            phIlRapor.Visible = false;
            phIlceRapor.Visible = false;
            phOkulRapor.Visible = true;
            phSubeRapor.Visible = false;

            okulIlceSubeAdi = "<strong>" + kurumAdi + "nda </strong>";

            ltrSinif.Text = sinif + ". SINIF " + ders.ToUpper(CultureInfo.CurrentCulture) + " DERSİ";
            ltrIlceOkulSube.Text = kurumAdi.ToUpper(CultureInfo.CurrentCulture) + " KARNESİ";
            this.Title = sinif + " sınıf " + ders + " Dersi " + kurumAdi+" Karnesi";
            KazanimKarneDB rbrDb = new KazanimKarneDB();
            List<KazanimKarneInfo> karne = rbrDb.OkulKazanimlariGetir(sinavId, sinif, brans, kurumKodu);

            rptKazanimKarneOkul.DataSource = karne;
            rptKazanimKarneOkul.DataBind();
        }
        else if (grup == "sube")
        {
            ltrSinif.Text = "";
            phIlRapor.Visible = false;
            phIlceRapor.Visible = false;
            phOkulRapor.Visible = false;
            phSubeRapor.Visible = true;

            this.Title = ders + " Dersi " + kurumAdi + " " + sinif + "-" + sube + " Şube Karnesi";

            okulIlceSubeAdi = "<strong>" + kurumAdi + " " + sinif + "-" + sube + " şubesinde </strong>";

            ltrSinif.Text = ders.ToUpper(CultureInfo.CurrentCulture) + " DERSİ";
            ltrIlceOkulSube.Text = kurumAdi.ToUpper(CultureInfo.CurrentCulture) + " " + sinif + "-" + sube + " ŞUBE KARNESİ";


            KazanimKarneDB rbrDb = new KazanimKarneDB();
            rptKazanimKarneSube.DataSource = rbrDb.KayitlariGetir(sinavId, brans, kurumKodu, sinif, sube);
            rptKazanimKarneSube.DataBind();

        }

            string ekkazanimStr = "";

            if (eksikKazanimlar.Count > 0)
            {
                ekkazanimStr = "<ul><li>Özellikle;<br> ";
                for (var i = 0; i < eksikKazanimlar.Count; i++)
                {
                    ekkazanimStr += "- " + eksikKazanimlar[i] + "<br>";
                }
                if (eksikKazanimlar.Count > 1)
                    ekkazanimStr += " kazanımlarının yapılma yüzdeleri çok düşüktür.</li></ul>";
                else
                    ekkazanimStr += " kazanımının yapılma yüzdesi çok düşüktür.</li></ul>";
            }
            string desteklemeStr = "";
            string telafiStr = "";
            if (destekleme > 0)
            {
                desteklemeStr = "% 31’in altında kalan " + this.destekleme + " kazanım için destekleme kurslarının planlanıp uygulanması";
                desteklemeStr += telafi > 0 ? "; " : " ";
            }

            if (telafi > 0)
            {
                telafiStr = "% 31’den % 49’a kadar olan " + this.telafi + " kazanımın öğrenme eksikliklerinin sınıf içi etkinliklerle giderilmesi ";
            }

            string trOrtalamaAltiStr = "";
            string trOrtalamaUstuStr = "";
            string trOrtalamaEsitStr = "";
            if (trOrtalamaUstu > 0)
            {
                trOrtalamaUstuStr = trOrtalamaUstu + " kazanımda Türkiye ortalamasının üstüne çıkıldığı";
                trOrtalamaUstuStr += trOrtalamaAlti > 0 || trOrtalamaEsit > 0 ? ", " : " ";
            }
            if (trOrtalamaAlti != 0)
            {
                trOrtalamaAltiStr = trOrtalamaAlti + " kazanımda Türkiye ortalamasının altında kalındığı, ";

            }
            if (trOrtalamaEsit != 0)
            {
                trOrtalamaEsitStr = trOrtalamaEsit + " kazanımın ise Türkiye ortalamasıyla eşit olduğu";
                trOrtalamaEsitStr += grup != "il" ? ", " : " ";
            }

            string ilOrtalamaAltiStr = "";
            string ilOrtalamaUstuStr = "";
            string ilOrtalamaEsitStr = "";
            if (ilOrtalamaUstu != 0)
            {
                ilOrtalamaUstuStr = ilOrtalamaUstu + " kazanımda Erzurum ortalamasının üstüne çıkıldığı";
                ilOrtalamaUstuStr += ilOrtalamaAlti > 0 || ilOrtalamaEsit > 0 ? ", " : " ";
            }
            if (ilOrtalamaAlti != 0)
            {
                ilOrtalamaAltiStr = ilOrtalamaAlti + " kazanımda Erzurum ortalamasının altında kalındığı";
                ilOrtalamaAltiStr += ilOrtalamaEsit > 0 ? ", " : " ";
            }
            if (ilOrtalamaEsit != 0)
            {
                ilOrtalamaEsitStr = ilOrtalamaEsit + " kazanımın ise Erzurum ortalamasıyla eşit olduğu ";
            }

            string subeYokNot = "";
            if (kurumKodu != 0)
            {
                OgrencilerDb subeDb = new OgrencilerDb();
                List<OgrencilerInfo> subeler = subeDb.SinavaGirenSubeler(sinavId, kurumKodu);
                if (subeler.Count < 2)
                {
                    subeYokNot = @"<p>________________________________<br/><i><b> Not:</b> Okulunuzda " + sinif + ".sınıf tek şube olduğu için ayrıca şube raporu düzenlenmemiştir.</i></p>";
                }
            }
            string desteklemeTelafiStr = "";
            if (destekleme != 0 || telafi != 0)
            {
                desteklemeTelafiStr = "<li> Edinilme düzeyi " + desteklemeStr + telafiStr + " tavsiye edilmektedir.Tabloda bu kazanımların karşılarına gerekli açıklamalar yapılmıştır.</li>";
            }
            string ilOrtalamasiStr = grup != "il" ? "<li> Erzurum oranları ile karşılaştırıldığında; " + ilOrtalamaUstuStr + ilOrtalamaAltiStr + ilOrtalamaEsitStr + " görülmektedir.</li>" : "görülmektedir.</li>";
            ltrRapor.Text =
                string.Concat(@"<p>",sinfo.DonemAdi," ayı İzleme Araştırması ", ders, " testi kazanımlarının ", okulIlceSubeAdi, "  edinilme oranları," +
                              "<ul><li>Türkiye geneli oranları ile karşılaştırıldığında; ", trOrtalamaUstuStr, trOrtalamaAltiStr, trOrtalamaEsitStr +
                              ilOrtalamasiStr + ekkazanimStr + desteklemeTelafiStr +
                              "<li> Kazanım karnesinin incelenmesi, öğrenme eksikliklerinin tespit edilmesi ve gerekli telafi eğitimlerin yapılması, projenin asıl amacı olan eğitimde niteliğinin artırılması ve öğrenme eksikliklerinin giderilmesi hedefine ulaşılmasını sağlayacaktır.</li>" +
                              "</ul><p style=text-align:right><em>Çalışmalarınızda kolaylıklar diler, katkılarınız için teşekkür ederiz</em></p></p>", subeYokNot);


        KarneRaporDB krnDb = new KarneRaporDB();
        KarneRaporInfo krnInfo = krnDb.KayitBilgiGetir(sinavId, ilceId, brans, kurumKodu, sinif, sube, grup);
        if (krnInfo.Sayac == 0)
        {

            krnDb.KayitEkle(sinavId, ilceId, brans, kurumKodu, sinif, sube, grup, 1);

        }
        else
        {
            int say = krnInfo.Sayac + 1;
            krnDb.KayitGuncelle(sinavId, ilceId, brans, kurumKodu, sinif, sube, grup, say);
        }
    }

    protected void rptKazanimKarneIl_OnItemDataBound(object sender, RepeaterItemEventArgs e)
    {
        GenelIslemler.SiraNumarasiForRepeater(e, "lblSira", 0, 1000);
        if (e.Item.ItemType == ListItemType.AlternatingItem || e.Item.ItemType == ListItemType.Item)
        {
            Literal ltrIlBasariOrani = (Literal)e.Item.FindControl("ltrIlBasariOrani");
            Literal ltrTrBasariOrani = (Literal)e.Item.FindControl("ltrTrBasariOrani");

            int sinif = ddlSube.SelectedValue.Split('-')[0].ToInt32();
            int brans = ddlBrans.SelectedValue.ToInt32();

            string kazanimNo = DataBinder.Eval(e.Item.DataItem, "KazanimNo").ToString();
            string kazanim = DataBinder.Eval(e.Item.DataItem, "Kazanim").ToString();

            KazanimKarneDB karneDb = new KazanimKarneDB();
            List<KazanimKarneInfo> ilKarne = karneDb.IlKazanimlariHesapla(sinavId, sinif, brans, kazanimNo).ToList();

            int ilDogruSayisi = ilKarne[0].DogruSayisi;

            int ilOgrenciSayisi = karneDb.IlOgrenciSayisi(sinavId, sinif, brans, kazanimNo);

            int ilBasariDurumu = (((double)ilDogruSayisi * 100) / ilOgrenciSayisi).ToInt32();
            int trOrtalamasi = Turkiye.Ortalamasi(kazanimNo);
            ltrIlBasariOrani.Text = ilBasariDurumu.ToString("0");
            ltrTrBasariOrani.Text = trOrtalamasi.ToString();

            if (ilBasariDurumu <= 30)
                eksikKazanimlar.Add(string.Format("'{0} {1}'", kazanimNo, kazanim));


            if (ilBasariDurumu > trOrtalamasi) trOrtalamaUstu++; else if (ilBasariDurumu < trOrtalamasi) trOrtalamaAlti++;
            if (ilBasariDurumu == trOrtalamasi) trOrtalamaEsit++;

            if (ilBasariDurumu >= 0 && ilBasariDurumu < 31)
            {
                destekleme++;
            }
            else if (ilBasariDurumu >= 31 && ilBasariDurumu <= 49)
            {
                telafi++;
            }

        }
    }
    protected void rptKazanimKarneIlce_OnItemDataBound(object sender, RepeaterItemEventArgs e)
    {

        GenelIslemler.SiraNumarasiForRepeater(e, "lblSira", 0, 1000);
        if (e.Item.ItemType == ListItemType.AlternatingItem || e.Item.ItemType == ListItemType.Item)
        {
            Literal ltrBasariOrani = (Literal)e.Item.FindControl("ltrBasariOrani");
            Literal ltrIlBasariOrani = (Literal)e.Item.FindControl("ltrIlBasariOrani");
            Literal ltrTrBasariOrani = (Literal)e.Item.FindControl("ltrTrBasariOrani");

            int sinif = ddlSube.SelectedValue.Split('-')[0].ToInt32();
            int brans = ddlBrans.SelectedValue.ToInt32();

            string kazanimNo = DataBinder.Eval(e.Item.DataItem, "KazanimNo").ToString();
            string kazanim = DataBinder.Eval(e.Item.DataItem, "Kazanim").ToString();

            KurumlarDb krmDb = new KurumlarDb();
            List<KurumlarInfo> sorular = krmDb.SinavaGirenOkullariDiziyeGetir(sinavId, ilceId);
            string sqlText = "";
            foreach (var s in sorular)
            {
                sqlText += " KurumKodu=" + s.KurumKodu + " or";
            }

            KazanimKarneDB karneDb = new KazanimKarneDB();
            List<KazanimKarneInfo> karne = karneDb.IlceKazanimlariHesapla(sinavId, sinif, brans, sqlText, kazanimNo).ToList();
            List<KazanimKarneInfo> ilKarne = karneDb.IlKazanimlariHesapla(sinavId, sinif, brans, kazanimNo).ToList();

            int dogruSayisi = karne[0].DogruSayisi;
            int ilDogruSayisi = ilKarne[0].DogruSayisi;

            int ogrenciSayisi = karneDb.IlceOgrenciSayisi(sinavId, sinif, brans, sqlText, kazanimNo);
            int ilOgrenciSayisi = karneDb.IlOgrenciSayisi(sinavId, sinif, brans, kazanimNo);

            int basariDurumu = (((double)dogruSayisi * 100) / ogrenciSayisi).ToInt32();
            int ilBasariDurumu = (((double)ilDogruSayisi * 100) / ilOgrenciSayisi).ToInt32();
            int trOrtalamasi = Turkiye.Ortalamasi(kazanimNo);
            ltrBasariOrani.Text = basariDurumu.ToString("0");
            ltrIlBasariOrani.Text = ilBasariDurumu.ToString("0");
            ltrTrBasariOrani.Text = trOrtalamasi.ToString();

            if (basariDurumu <= 30)
                eksikKazanimlar.Add(string.Format("'{0} {1}'", kazanimNo, kazanim));

            if (basariDurumu > trOrtalamasi) trOrtalamaUstu++; else if (basariDurumu < trOrtalamasi) trOrtalamaAlti++;
            if (basariDurumu > ilBasariDurumu) ilOrtalamaUstu++; else if (basariDurumu < ilBasariDurumu) ilOrtalamaAlti++;
            if (basariDurumu == trOrtalamasi) trOrtalamaEsit++;
            if (basariDurumu == ilBasariDurumu) ilOrtalamaEsit++;

            Literal ltrSonuc = (Literal)e.Item.FindControl("ltrSonuc");

            if (basariDurumu >= 0 && basariDurumu < 31)
            {
                ltrSonuc.Text = "Destekleme kursları yapılmalı";
                destekleme++;
            }
            else if (basariDurumu >= 31 && basariDurumu <= 49)
            {
                ltrSonuc.Text = "Sınıf içi telafi eğitimi yapılmalı";
                telafi++;
            }

        }
    }
    protected void rptKazanimKarneOkul_OnItemDataBound(object sender, RepeaterItemEventArgs e)
    {
        GenelIslemler.SiraNumarasiForRepeater(e, "lblSira", 0, 1000);
        if (e.Item.ItemType == ListItemType.AlternatingItem || e.Item.ItemType == ListItemType.Item)
        {
            Literal ltrOkulBasariOrani = (Literal)e.Item.FindControl("ltrOkulBasariOrani");
            Literal ltrIlceBasariOrani = (Literal)e.Item.FindControl("ltrIlceBasariOrani");
            Literal ltrIlBasariOrani = (Literal)e.Item.FindControl("ltrIlBasariOrani");
            Literal ltrTrBasariOrani = (Literal)e.Item.FindControl("ltrTrBasariOrani");

            int sinif = ddlSube.SelectedValue.Split('-')[0].ToInt32();
            int brans = ddlBrans.SelectedValue.ToInt32();

            string kazanimNo = DataBinder.Eval(e.Item.DataItem, "KazanimNo").ToString();
            string kazanim = DataBinder.Eval(e.Item.DataItem, "Kazanim").ToString();

            KurumlarDb krmDb = new KurumlarDb();
            List<KurumlarInfo> sorular = krmDb.SinavaGirenOkullariDiziyeGetir(sinavId, ilceId);
            string sqlText = "";
            foreach (var s in sorular)
            {
                sqlText += " KurumKodu=" + s.KurumKodu + " or";
            }

            KazanimKarneDB karneDb = new KazanimKarneDB();
            List<KazanimKarneInfo> okulKarne = karneDb.OkulKazanimlariHesapla(sinavId, sinif, brans, kurumKodu, kazanimNo).ToList();
            List<KazanimKarneInfo> ilceKarne = karneDb.IlceKazanimlariHesapla(sinavId, sinif, brans, sqlText, kazanimNo).ToList();
            List<KazanimKarneInfo> ilKarne = karneDb.IlKazanimlariHesapla(sinavId, sinif, brans, kazanimNo).ToList();

            int okulDogruSayisi = okulKarne[0].DogruSayisi;
            int ilceDogruSayisi = ilceKarne[0].DogruSayisi;
            int ilDogruSayisi = ilKarne[0].DogruSayisi;

            int okulOgrenciSayisi = karneDb.OkulOgrenciSayisi(sinavId, sinif, brans, kurumKodu, kazanimNo);
            int ilceOgrenciSayisi = karneDb.IlceOgrenciSayisi(sinavId, sinif, brans, sqlText, kazanimNo);
            int ilOgrenciSayisi = karneDb.IlOgrenciSayisi(sinavId, sinif, brans, kazanimNo);

            int okulBasariDurumu = (((double)okulDogruSayisi * 100) / okulOgrenciSayisi).ToInt32();
            int ilceBasariDurumu = (((double)ilceDogruSayisi * 100) / ilceOgrenciSayisi).ToInt32();
            int ilBasariDurumu = (((double)ilDogruSayisi * 100) / ilOgrenciSayisi).ToInt32();
            int trOrtalamasi = Turkiye.Ortalamasi(kazanimNo);

            ltrOkulBasariOrani.Text = okulBasariDurumu.ToString("0");
            ltrIlceBasariOrani.Text = ilceBasariDurumu.ToString("0");
            ltrIlBasariOrani.Text = ilBasariDurumu.ToString("0");
            ltrTrBasariOrani.Text = trOrtalamasi.ToString();

            if (okulBasariDurumu <= 30)
                eksikKazanimlar.Add(string.Format("'{0} {1}'", kazanimNo, kazanim));

            if (okulBasariDurumu > trOrtalamasi) trOrtalamaUstu++; else if (okulBasariDurumu < trOrtalamasi) trOrtalamaAlti++;
            if (okulBasariDurumu > ilBasariDurumu) ilOrtalamaUstu++; else if (okulBasariDurumu < ilBasariDurumu) ilOrtalamaAlti++;
            if (okulBasariDurumu > ilceBasariDurumu) ilceOrtalamaUstu++; else if (okulBasariDurumu < ilceBasariDurumu) ilceOrtalamaAlti++;
            if (okulBasariDurumu == trOrtalamasi) trOrtalamaEsit++;
            if (okulBasariDurumu == ilBasariDurumu) ilOrtalamaEsit++;
            if (okulBasariDurumu == ilceBasariDurumu) ilceOrtalamaEsit++;

            Literal ltrSonuc = (Literal)e.Item.FindControl("ltrSonuc");

            if (okulBasariDurumu >= 0 && okulBasariDurumu < 31)
            {
                ltrSonuc.Text = "Destekleme kursları yapılmalı";
                destekleme++;
            }
            else if (okulBasariDurumu >= 31 && okulBasariDurumu <= 49)
            {
                ltrSonuc.Text = "Sınıf içi telafi eğitimi yapılmalı";
                telafi++;
            }

        }
    }
    protected void rptKazanimKarneSube_OnItemDataBound(object sender, RepeaterItemEventArgs e)
    {
        GenelIslemler.SiraNumarasiForRepeater(e, "lblSira", 0, 1000);
        if (e.Item.ItemType == ListItemType.AlternatingItem || e.Item.ItemType == ListItemType.Item)
        {
            Literal ltrSubeBasariOrani = (Literal)e.Item.FindControl("ltrSubeBasariOrani");
            Literal ltrOkulBasariOrani = (Literal)e.Item.FindControl("ltrOkulBasariOrani");
            Literal ltrIlceBasariOrani = (Literal)e.Item.FindControl("ltrIlceBasariOrani");
            Literal ltrIlBasariOrani = (Literal)e.Item.FindControl("ltrIlBasariOrani");
            Literal ltrTrBasariOrani = (Literal)e.Item.FindControl("ltrTrBasariOrani");

            int sinif = ddlSube.SelectedValue.Split('-')[0].ToInt32();
            int brans = ddlBrans.SelectedValue.ToInt32();

            string kazanimNo = DataBinder.Eval(e.Item.DataItem, "KazanimNo").ToString();
            string kazanim = DataBinder.Eval(e.Item.DataItem, "Kazanim").ToString();

            KurumlarDb krmDb = new KurumlarDb();
            List<KurumlarInfo> sorular = krmDb.SinavaGirenOkullariDiziyeGetir(sinavId, ilceId);
            string sqlText = "";
            foreach (var s in sorular)
            {
                sqlText += " KurumKodu=" + s.KurumKodu + " or";
            }

            KazanimKarneDB karneDb = new KazanimKarneDB();
            List<KazanimKarneInfo> okulKarne = karneDb.OkulKazanimlariHesapla(sinavId, sinif, brans, kurumKodu, kazanimNo).ToList();
            List<KazanimKarneInfo> ilceKarne = karneDb.IlceKazanimlariHesapla(sinavId, sinif, brans, sqlText, kazanimNo).ToList();
            List<KazanimKarneInfo> ilKarne = karneDb.IlKazanimlariHesapla(sinavId, sinif, brans, kazanimNo).ToList();

            int subeDogruSayisi = DataBinder.Eval(e.Item.DataItem, "DogruSayisi").ToInt32();
            int okulDogruSayisi = okulKarne[0].DogruSayisi;
            int ilceDogruSayisi = ilceKarne[0].DogruSayisi;
            int ilDogruSayisi = ilKarne[0].DogruSayisi;

            int subeOgrenciSayisi = DataBinder.Eval(e.Item.DataItem, "OgrenciSayisi").ToInt32();
            int okulOgrenciSayisi = karneDb.OkulOgrenciSayisi(sinavId, sinif, brans, kurumKodu, kazanimNo);
            int ilceOgrenciSayisi = karneDb.IlceOgrenciSayisi(sinavId, sinif, brans, sqlText, kazanimNo);
            int ilOgrenciSayisi = karneDb.IlOgrenciSayisi(sinavId, sinif, brans, kazanimNo);

            int subeBasariDurumu = (((double)subeDogruSayisi * 100) / subeOgrenciSayisi).ToInt32();
            int okulBasariDurumu = (((double)okulDogruSayisi * 100) / okulOgrenciSayisi).ToInt32();
            int ilceBasariDurumu = (((double)ilceDogruSayisi * 100) / ilceOgrenciSayisi).ToInt32();
            int ilBasariDurumu = (((double)ilDogruSayisi * 100) / ilOgrenciSayisi).ToInt32();
            int trOrtalamasi = Turkiye.Ortalamasi(kazanimNo);

            ltrSubeBasariOrani.Text = subeBasariDurumu.ToString("0");
            ltrOkulBasariOrani.Text = okulBasariDurumu.ToString("0");
            ltrIlceBasariOrani.Text = ilceBasariDurumu.ToString("0");
            ltrIlBasariOrani.Text = ilBasariDurumu.ToString("0");
            ltrTrBasariOrani.Text = trOrtalamasi.ToString();

            if (subeBasariDurumu <= 30)
                eksikKazanimlar.Add(string.Format("'{0} {1}'", kazanimNo, kazanim));

            if (subeBasariDurumu > trOrtalamasi) trOrtalamaUstu++; else if (subeBasariDurumu < trOrtalamasi) trOrtalamaAlti++;
            if (subeBasariDurumu > ilBasariDurumu) ilOrtalamaUstu++; else if (subeBasariDurumu < ilBasariDurumu) ilOrtalamaAlti++;
            if (subeBasariDurumu > ilceBasariDurumu) ilceOrtalamaUstu++; else if (subeBasariDurumu < ilceBasariDurumu) ilceOrtalamaAlti++;
            if (subeBasariDurumu > okulBasariDurumu) okulOrtalamaUstu++; else if (subeBasariDurumu < okulBasariDurumu) okulOrtalamaAlti++;
            if (subeBasariDurumu == trOrtalamasi) trOrtalamaEsit++;
            if (subeBasariDurumu == ilBasariDurumu) ilOrtalamaEsit++;
            if (subeBasariDurumu == ilceBasariDurumu) ilceOrtalamaEsit++;
            if (subeBasariDurumu == okulBasariDurumu) okulOrtalamaEsit++;


            Literal ltrSonuc = (Literal)e.Item.FindControl("ltrSonuc");

            if (subeBasariDurumu >= 0 && subeBasariDurumu < 31)
            {
                ltrSonuc.Text = "Destekleme kursları yapılmalı";
                destekleme++;
            }
            else if (subeBasariDurumu >= 31 && subeBasariDurumu <= 49)
            {
                ltrSonuc.Text = "Sınıf içi telafi eğitimi yapılmalı";
                telafi++;
            }

        }
    }
}