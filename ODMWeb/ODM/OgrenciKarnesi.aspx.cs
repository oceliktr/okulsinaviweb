using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DAL;

public partial class ODM_OgrenciKarnesi : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {

        if (!IsPostBack)
        {
            SinavlarDb veriDb = new SinavlarDb();
            ddlSinavId.DataSource = veriDb.KayitlariGetir();
            ddlSinavId.DataValueField = "Id";
            ddlSinavId.DataTextField = "SinavAdi";
            ddlSinavId.DataBind();
            ddlSinavId.Items.Insert(0, new ListItem("Sınav Seçiniz", ""));
            ddlOkul.Items.Insert(0, new ListItem("Önce Sınav Seçiniz", ""));
        }
    }

    protected void btnListele_OnClick(object sender, EventArgs e)
    {
        int sinavId = ddlSinavId.SelectedValue.ToInt32();
        int kurumKodu = ddlOkul.SelectedValue.ToInt32();

        int sinif = ddlSube.SelectedValue == "0" ? 0 : ddlSube.SelectedValue.Split('-')[0].ToInt32();
        //string sube = ddlSube.SelectedValue == "0" ? "0" : ddlSube.SelectedValue.Split('-')[1];

        Literal1.Text = ddlOkul.SelectedItem.Text + " - " + ddlBrans.SelectedItem.Text;

        OgrencilerDb ogrDb = new OgrencilerDb();
        List<OgrencilerInfo> ogrList = ogrDb.KayitlariDiziyeGetir(sinavId, kurumKodu, sinif);
        rptOgrenciler.DataSource = ogrList;
        rptOgrenciler.DataBind();
    }

    protected void ddlSinavId_OnSelectedIndexChanged(object sender, EventArgs e)
    {
        int sinavId = ddlSinavId.SelectedValue.ToInt32();
        KurumlarDb ilceDb = new KurumlarDb();
        ddlOkul.DataSource = ilceDb.SinavaGirenOkullariGetir(sinavId);
        ddlOkul.DataValueField = "KurumKodu";
        ddlOkul.DataTextField = "IlceOkul";
        ddlOkul.DataBind();
        ddlOkul.Items.Insert(0, new ListItem("Okul Seçiniz", ""));

        RubrikDb rbrDb = new RubrikDb();
        ddlBrans.DataSource = rbrDb.SinavdakiBranslar(sinavId);
        ddlBrans.DataValueField = "BransId";
        ddlBrans.DataTextField = "BransAdi";
        ddlBrans.DataBind();
        ddlBrans.Items.Insert(0, new ListItem("Ders Seçiniz", ""));
    }

    protected void rptKazanim_OnItemDataBound(object sender, RepeaterItemEventArgs e)
    {
        if (e.Item.ItemType == ListItemType.AlternatingItem || e.Item.ItemType == ListItemType.Item)
        {

            Literal ltrKazanimKarne = (Literal)e.Item.FindControl("ltrKazanimKarne");
            Literal ltrDY = (Literal)e.Item.FindControl("ltrDY");

            HiddenField hfOgrIdValue = (HiddenField)e.Item.FindControl("hfOgrIdValue");
            string kazanimlar = DataBinder.Eval(e.Item.DataItem, "Kazanimlar").ToString();
            int soruNo = DataBinder.Eval(e.Item.DataItem, "SoruNo").ToInt32();

            int sinavId = ddlSinavId.SelectedValue.ToInt32();
            int brans = ddlBrans.SelectedValue.ToInt32();
            int ogrenciId = hfOgrIdValue.Value.ToInt32();

        //    AbdusselamDB aslDb = new AbdusselamDB();
        //    AbdusselamInfo asInfo = aslDb.KayitBilgiGetir(sinavId, brans, ogrenciId, soruNo);

            //if (asInfo.Puani == 1)
            //{
            //    ltrDY.Text = "D";
            //}
            //else if (asInfo.Puani == 0 && asInfo.Secenek != " ")
            //{
            //    ltrDY.Text = "Y";
            //}
            //else if (asInfo.Puani == 0 && asInfo.Secenek == " ")
            //{
            //    ltrDY.Text = "B";
            //}


            KazanimlarDB kznDb = new KazanimlarDB();
            string[] konum = kazanimlar.Split('|');
            foreach (KazanimlarInfo kinfo in from t in konum where t.IsInteger() select kznDb.KayitBilgiGetir(t.ToInt32()))
            {
                ltrKazanimKarne.Text += kinfo.Karne;
            }

        }
    }
    public string RelativeToAbsoluteURLS(string text)
    {
        if (String.IsNullOrEmpty(text))
            return text;

        string absoluteUrl = Request.PhysicalApplicationPath;
        String value = Regex.Replace(text, "<(.*?)(src)=\"(?!http)(.*?)\"(.*?)>", "<$1$2=\"" + absoluteUrl + "$3\"$4>", RegexOptions.IgnoreCase | RegexOptions.Multiline);
        return value.Replace(absoluteUrl + "/", absoluteUrl);
    }
    protected void btnPdfOlustur_OnClick(object sender, EventArgs e)
    {

    }
    protected void ddlOkul_OnSelectedIndexChanged(object sender, EventArgs e)
    {

        int sinavId = ddlSinavId.SelectedValue.ToInt32();
        int kurumKodu = ddlOkul.SelectedValue.ToInt32();
        OgrencilerDb subeDb = new OgrencilerDb();
        ddlSube.DataSource = subeDb.SinavaGirenSubeler(sinavId, kurumKodu);
        ddlSube.DataValueField = "SinifSube";
        ddlSube.DataTextField = "SinifSube";
        ddlSube.DataBind();
        ddlSube.Items.Insert(0, new ListItem("Tümü", "0"));
    }

    protected void rptOgrenciler_OnItemDataBound(object sender, RepeaterItemEventArgs e)
    {
        if (e.Item.ItemType == ListItemType.AlternatingItem || e.Item.ItemType == ListItemType.Item)
        {
            Literal ltrSoyuSayisi = (Literal)e.Item.FindControl("ltrSoyuSayisi");
            Repeater rptSoruNo = (Repeater)e.Item.FindControl("rptSoruNo");
            Repeater rptSecenekA = (Repeater)e.Item.FindControl("rptSecenekA");
            Repeater rptSecenekB = (Repeater)e.Item.FindControl("rptSecenekB");
            Repeater rptCevaplar = (Repeater)e.Item.FindControl("rptCevaplar");
            Literal ltrDogruSoruSayisi = (Literal)e.Item.FindControl("ltrDogruSoruSayisi");
            Literal ltrYanlisSoruSayisi = (Literal)e.Item.FindControl("ltrYanlisSoruSayisi");
            Literal ltrBosSoruSayisi = (Literal)e.Item.FindControl("ltrBosSoruSayisi");
            Repeater rptKazanim = (Repeater)e.Item.FindControl("rptKazanim");
            Literal ltrBaslik = (Literal)e.Item.FindControl("ltrBaslik");
            Literal ltrOkulu = (Literal)e.Item.FindControl("ltrOkulu");
            Literal ltrIlcesi = (Literal)e.Item.FindControl("ltrIlcesi");
            Literal ltrKitapcikTuru = (Literal)e.Item.FindControl("ltrKitapcikTuru");

            int sinavId = ddlSinavId.SelectedValue.ToInt32();
            int brans = ddlBrans.SelectedValue.ToInt32();
            int sinif = DataBinder.Eval(e.Item.DataItem, "Sinifi").ToInt32();
            int ogrId = DataBinder.Eval(e.Item.DataItem, "OgrenciId").ToInt32();
            int kurumKodu = ddlOkul.SelectedValue.ToInt32();
            string ders = ddlBrans.SelectedItem.Text;

            KitapcikCevapDB ktpDb = new KitapcikCevapDB();
            List<KitapcikCevapInfo> sorular = ktpDb.SinavdakiSoruNolar(sinavId, sinif, brans);
            ltrSoyuSayisi.Text = sorular.Count.ToString();
     
            rptSoruNo.DataSource = sorular;
            rptSoruNo.DataBind();

            rptSecenekA.DataSource = sorular;
            rptSecenekA.DataBind();
            rptSecenekB.DataSource = sorular;
            rptSecenekB.DataBind();

            //AbdusselamDB veriDb = new AbdusselamDB();
           // AbdusselamInfo info = veriDb.KayitBilgiGetir(sinavId, brans, ogrId, 1);
          //  ltrKitapcikTuru.Text = info.KitapcikTuru;

          //  rptCevaplar.DataSource = veriDb.KayitlariGetir(sinavId, kurumKodu, brans, ogrId);
         //   rptCevaplar.DataBind();

            //ltrDogruSoruSayisi.Text = veriDb.DogruYanlisSayisi(sinavId, kurumKodu, brans, ogrId, 1).ToString();
            //ltrYanlisSoruSayisi.Text = veriDb.DogruYanlisSayisi(sinavId, kurumKodu, brans, ogrId, 0).ToString();
            //ltrBosSoruSayisi.Text = veriDb.BosCevapSayisi(sinavId, kurumKodu, brans, ogrId).ToString();

            //if (info.KitapcikTuru == "A")
            //{
            //    rptSecenekA.Visible = true;
            //    rptSecenekB.Visible = false;
            //}
            //else
            //{

            //    rptSecenekA.Visible = false;
            //    rptSecenekB.Visible = true;
            //}

            //RubrikDb rDb = new RubrikDb();
            //List<RubrikInfo> rinfo = rDb.KayitlariGetir(sinavId, sinif, brans, info.KitapcikTuru);

            //rptKazanim.DataSource = rinfo;
            //rptKazanim.DataBind();

            ltrBaslik.Text = brans == 3
                ? "09.05.2018 - İZLEME ARAŞTIRMASI " + ders.ToUpper(CultureInfo.CurrentCulture) + " DERSİ"
                : "08.05.2018 - İZLEME ARAŞTIRMASI " + ders.ToUpper(CultureInfo.CurrentCulture) + " DERSİ";

            KurumlarDb krmDb = new KurumlarDb();
            KurumlarInfo krmInfo = krmDb.KayitBilgiGetir(kurumKodu.ToString());
            ltrIlcesi.Text = krmInfo.IlceAdi;
            ltrOkulu.Text = krmInfo.KurumAdi;


        }
    }

}