using System;
using System.Collections.Generic;
using System.Drawing;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DAL;
using iTextSharp.text;
using iTextSharp.text.html.simpleparser;
using iTextSharp.text.pdf;
using ListItem = System.Web.UI.WebControls.ListItem;

public partial class ODM_KazanimKarneAdmin : Page
{
    public class DersveIlceler
    {
        public int Sinif { get; set; }
        public int Ilce { get; set; }
        public int BransId { get; set; }
        public int KurumKodu { get; set; }
        public string Sube { get; set; }
        public string Grup { get; set; }

        public DersveIlceler(int sinif, int ilce, int bransId, int kurumKodu, string sube, string grup)
        {
            Sinif = sinif;
            Ilce = ilce;
            BransId = bransId;
            KurumKodu = kurumKodu;
            Sube = sube;
            Grup = grup;
        }
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
    readonly List<string> eksikKazanimlar = new List<string>();
    protected void Page_Load(object sender, EventArgs e)
    {
        SinavlarDb sDb = new SinavlarDb();
        SinavlarInfo sinf = sDb.AktifSinavAdi();
        ltrDonemAdi.Text = sinf.SinavAdi;
        sinavId = sinf.SinavId;
        if (!IsPostBack)
        {
            if (!Master.Yetki().Contains("Admin"))
                Response.Redirect("Giris.aspx");


            RubrikDb rbrDb = new RubrikDb();
            ddlBrans.DataSource = rbrDb.SinavdakiBranslar(sinavId);
            ddlBrans.DataValueField = "BransId";
            ddlBrans.DataTextField = "BransAdi";
            ddlBrans.DataBind();
            ddlBrans.Items.Insert(0, new ListItem("Ders Seçiniz", ""));

            IlcelerDb ilceDb = new IlcelerDb();
            ddlIlce.DataSource = ilceDb.KayitlariGetir();
            ddlIlce.DataValueField = "Id";
            ddlIlce.DataTextField = "IlceAdi";
            ddlIlce.DataBind();
            ddlIlce.Items.Insert(0, new ListItem("İlçe Seçiniz", ""));
            ddlOkul.Items.Insert(0, new ListItem("İlçe Seçiniz", ""));
            ddlSube.Items.Insert(0, new ListItem("İlçe Seçiniz", ""));
            ddlIlce.Items.Insert(1, new ListItem("İl Raporu", "0"));
        }
    }

    protected void ddlIlce_OnSelectedIndexChanged(object sender, EventArgs e)
    {
        int ilceId = ddlIlce.SelectedValue.ToInt32();
        KurumlarDb ilceDb = new KurumlarDb();

        ddlOkul.DataSource = ilceDb.SinavaGirenOkullariGetir(sinavId, ilceId);
        ddlOkul.DataValueField = "KurumKodu";
        ddlOkul.DataTextField = "KurumAdi";
        ddlOkul.DataBind();
        ddlOkul.Items.Insert(0, new ListItem("Okul Seçiniz", "0"));

        OgrencilerDb subeDb = new OgrencilerDb();
        List<SinifSube> lst = new List<SinifSube>();
        if (ilceId == 0)
        {
            List<OgrencilerInfo> siniflar = subeDb.SinavaGirenSiniflar(sinavId);
            lst.AddRange(siniflar.Select(snf => new SinifSube(snf.Sinifi.ToString(), snf.Sinifi + ". sınıf il ort.")));
        }
        else
        {
            List<OgrencilerInfo> siniflar = subeDb.IlcedeSinavaGirenSiniflar(sinavId, ilceId);
            lst.AddRange(siniflar.Select(snf => new SinifSube(snf.Sinifi.ToString(), snf.Sinifi + ". sınıf ilçe ort.")));
        }
        ddlSube.DataSource = lst;
        ddlSube.DataValueField = "Sinif";
        ddlSube.DataTextField = "Sube";
        ddlSube.DataBind();
    }

    protected void ddlOkul_OnSelectedIndexChanged(object sender, EventArgs e)
    {
        List<SinifSube> lst = new List<SinifSube>();

        int ilceId = ddlIlce.SelectedValue.ToInt32();
        int kurumKodu = ddlOkul.SelectedValue.ToInt32();
        OgrencilerDb subeDb = new OgrencilerDb();

        List<OgrencilerInfo> subeler = subeDb.SinavaGirenSubeler(sinavId, kurumKodu);
        List<OgrencilerInfo> siniflar = null;
        siniflar = kurumKodu == 0 ? subeDb.IlcedeSinavaGirenSiniflar(sinavId, ilceId) : subeDb.SinavaGirenSiniflar(sinavId, kurumKodu);

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

        ddlSube.DataSource = lst;
        ddlSube.DataValueField = "Sinif";
        ddlSube.DataTextField = "Sube";
        ddlSube.DataBind();

    }

    protected void btnListele_OnClick(object sender, EventArgs e)
    {
        SinavlarDb snvDb = new SinavlarDb();
        SinavlarInfo sinfo = snvDb.AktifSinavAdi();

        yazdir.Visible = true;
        phRaporAlmayanlar.Visible = false;

        string okulIlceSubeAdi = "";
        int brans = ddlBrans.SelectedValue.ToInt32();
        string ders = ddlBrans.SelectedItem.ToString();
        int sinif = ddlSube.SelectedValue.Split('-')[0].ToInt32();
        string sube = "";
        if (ddlSube.SelectedValue.Length > 1)
            sube = ddlSube.SelectedValue.Split('-')[1];
        int ilceId = ddlIlce.SelectedValue.ToInt32();
        int kurumKodu = ddlOkul.SelectedValue.ToInt32();
        string ilceAdi = ddlIlce.SelectedItem.Text;
        string kurumAdi = ddlOkul.SelectedItem.Text;

        string grup = "";
        if (ilceId == 0)
            grup = "il";
        else if (ilceId != 0 && kurumKodu == 0)
            grup = "ilce";
        else if (kurumKodu != 0 && sube == "")
            grup = "okul";
        else if (kurumKodu != 0 && sube != "")
            grup = "sube";


        ltrBaslik.Text = sinfo.DonemAdi.ToUpper() + " - İZLEME ARAŞTIRMASI ";
        if (grup == "il")
        {
            okulIlceSubeAdi = "<strong>Erzurum</strong> ilinde";

            ltrSinif.Text = sinif + ". SINIF " + ders.ToUpper(CultureInfo.CurrentCulture) + " DERSİ";
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
            this.Title = sinif + " sınıf " + ders + " Dersi " + kurumAdi + " Karnesi";
            KazanimKarneDB rbrDb = new KazanimKarneDB();
            List<KazanimKarneInfo> karne = rbrDb.OkulKazanimlariGetir(sinavId, sinif, brans, kurumKodu);

            rptKazanimKarneOkul.DataSource = karne;
            rptKazanimKarneOkul.DataBind();

            // krnInfo = krnDb.KayitBilgiGetir(sinavId, ilceId, brans, kurumKodu, sinif);

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


    }

    private int trOrtalamasiGenel = 0;
    private int trOrtalamasiGenelIl = 0;
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

            trOrtalamasiGenel += trOrtalamasi;
            ltrTrOrtalama.Text = (trOrtalamasiGenel / (e.Item.ItemIndex + 1)).ToString();
            trOrtalamasiGenelIl += ilBasariDurumu;
            ltrIlOrtalama.Text = (trOrtalamasiGenelIl / (e.Item.ItemIndex + 1)).ToString();
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

            int ilceId = ddlIlce.SelectedValue.ToInt32();
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

            int ilceId = ddlIlce.SelectedValue.ToInt32();
            int kurumKodu = ddlOkul.SelectedValue.ToInt32();
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

            int ilceId = ddlIlce.SelectedValue.ToInt32();
            int kurumKodu = ddlOkul.SelectedValue.ToInt32();
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

    protected void btnRapor_OnClick(object sender, EventArgs e)
    {
        yazdir.Visible = false;
        phRaporAlmayanlar.Visible = true;

        BranslarDb brnsDb = new BranslarDb();
        List<DersveIlceler> mevcutLst = new List<DersveIlceler>();
        OgrencilerDb ogrDb = new OgrencilerDb();
        List<OgrencilerInfo> ogr = ogrDb.SinavaGirenSiniflar(sinavId);
        List<OgrencilerInfo> ogrSb = ogrDb.SinavaGirenSubeler(sinavId);
        RubrikDb rbrDb = new RubrikDb();
        List<RubrikInfo> brn = rbrDb.SinavdakiBranslar(sinavId);
        IlcelerDb ilceDb = new IlcelerDb();
        List<IlcelerInfo> ilce = ilceDb.Ilceler();
        KurumlarDb krmDb = new KurumlarDb();
        List<KurumlarInfo> okul = krmDb.SinavaGirenOkullariDiziyeGetir(sinavId);

        KarneRaporDB krnDb = new KarneRaporDB();
        List<KarneRaporInfo> karneRapor = krnDb.KayitlariDizeGetir(sinavId);

        foreach (var s in ogr)
        {
            foreach (var k in brn)
            {
                foreach (var i in ilce)
                {
                    if (i.Id != 21)
                        mevcutLst.Add(new DersveIlceler(s.Sinifi, i.Id, k.BransId, 0, "", "ilce"));
                }
                foreach (var okl in okul)
                {
                    mevcutLst.Add(new DersveIlceler(s.Sinifi, okl.IlceId, k.BransId, okl.KurumKodu.ToInt32(), "", "okul"));
                }
                foreach (var sb in ogrSb)
                {
                    var subee = ogrSb.Count(x => x.KurumKodu == sb.KurumKodu);
                    if (subee > 1)
                    {
                        mevcutLst.Add(new DersveIlceler(s.Sinifi, sb.IlceId, k.BransId, sb.KurumKodu, sb.Sube, "sube"));
                    }
                }
            }
        }

        string ilceMem = "İlçe Milli Eğitim Müdürlüğü";
        ltrRaporAlamayanlar.Text = "";
        int a = 0;
        foreach (var mvc in mevcutLst)
        {
           string kurum= mvc.KurumKodu == 0 ? ilceDb.IlceAdi(mvc.Ilce) + " "+ ilceMem : krmDb.KurumAdi(mvc.KurumKodu);
            var dbKontrol = karneRapor.Count(x => x.Sinif == mvc.Sinif && x.IlceId == mvc.Ilce && x.BransId == mvc.BransId && x.KurumKodu == mvc.KurumKodu && x.Sube == mvc.Sube && x.Grup == mvc.Grup);
            if (dbKontrol == 0)
            {
            a++;
                ltrRaporAlamayanlar.Text += "<tr role = \"row\" class=\"odd\"><td class=\"sorting_1\">"+a + "</td><td>" + ilceDb.IlceAdi(mvc.Ilce) + "</td><td>"+kurum+ "</td><td>" + brnsDb.BransAdi(mvc.BransId) + "</td><td>" + mvc.Sinif +"</td><td>" + mvc.Sube + "</td><td>" + GrupAdi(mvc.Grup) + "</td>";
            }
        }
        

    }

    private string GrupAdi(string grup)
    {
        string donus = "";
        if (grup == "ilce")
            donus = "İlçe";
        else if (grup == "okul")
            donus = "Okul";
        else if (grup == "sube")
            donus = "Şube";
        return donus;
    }

    protected void btnExport_OnClick(object sender, EventArgs e)
    {
        Response.ContentType = "application/pdf";
        Response.AddHeader("content-disposition", "attachment;filename=Panel.pdf");
        Response.Cache.SetCacheability(HttpCacheability.NoCache);
        StringWriter sw = new StringWriter();
        HtmlTextWriter hw = new HtmlTextWriter(sw);
        yazdir.RenderControl(hw);
        StringReader sr = new StringReader(sw.ToString());
        Document pdfDoc = new Document(PageSize.A4, 10f, 10f, 100f, 0f);

        StyleSheet css = new StyleSheet();
        css.LoadTagStyle("div", "color", "red");
        css.LoadTagStyle("table", "color", "red");
        HTMLWorker htmlparser = new HTMLWorker(pdfDoc,null,css);
        PdfWriter.GetInstance(pdfDoc, Response.OutputStream);
        pdfDoc.Open();

       
        htmlparser.Parse(sr);
        pdfDoc.Close();
        Response.Write(pdfDoc);
        Response.End();


        
    }
}