using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DAL;

public partial class ODM_Kazanimlar : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            if (!Master.Yetki().Contains("Admin"))
                Response.Redirect("Giris.aspx");

            BranslarDb brnsDb = new BranslarDb();
            ddlBrans.DataSource = brnsDb.KayitlariGetir();
            ddlBrans.DataValueField = "Id";
            ddlBrans.DataTextField = "BransAdi";
            ddlBrans.DataBind();
            ddlBrans.Items.Insert(0, new ListItem("Branş Seçiniz", ""));

            ddlKazanimOgrenmeAlani.Items.Insert(0, new ListItem("Sınıf ve Branş Seçiniz", ""));
            ddlKazanimAltOgrenmeAlani.Items.Insert(0, new ListItem("Sınıf ve Branş Seçiniz", ""));

            ddlAnaKat.Items.Insert(0, new ListItem("Sınıf ve Branş Seçiniz", ""));
        }
    }

    private void KazanimlariGetir()
    {
        int sinif = ddlSinif.SelectedValue.ToInt32();
        int brans = ddlBrans.SelectedValue.ToInt32();
        KazanimlarDB kznDb = new KazanimlarDB();
        rptKazanimlar.DataSource = kznDb.KayitlariGetir(brans, sinif);
        rptKazanimlar.DataBind();
    }
    private void UstOgrenmeAlanlari()
    {
        int sinif = ddlSinif.SelectedValue.ToInt32();
        int brans = ddlBrans.SelectedValue.ToInt32();

        KOgrenmeAlanlariDB uoaDb = new KOgrenmeAlanlariDB();
        ddlKazanimOgrenmeAlani.DataSource = ddlAnaKat.DataSource = uoaDb.KayitlariGetir(0, brans, sinif);
        ddlKazanimOgrenmeAlani.DataValueField = ddlAnaKat.DataValueField = "AlanNo";
        ddlKazanimOgrenmeAlani.DataTextField = ddlAnaKat.DataTextField = "OgrenmeAlani";
        ddlAnaKat.DataBind();
        ddlKazanimOgrenmeAlani.DataBind();
        ddlAnaKat.Items.Insert(0, new ListItem("Seçiniz", ""));
        ddlAnaKat.Items.Insert(1, new ListItem("Üst Öğrenme Alanı", "0"));

        ddlKazanimOgrenmeAlani.Items.Insert(0, new ListItem("Seçiniz", ""));
        ddlKazanimAltOgrenmeAlani.Items.Clear();
        ddlKazanimAltOgrenmeAlani.Items.Insert(0, new ListItem("Öğrenme Alanı Seçiniz", ""));

    }

    protected void btnOgrenmeAlaniKaydet_OnClick(object sender, EventArgs e)
    {
        int id = hfOgrenmeAlaniId.Value.ToInt32();

        string ogrenmeAlani = txtOgrenmeAlani.Text;
        int ustOgrenmealani = ddlAnaKat.SelectedValue.ToInt32();
        int ogrenmealaniSinif = ddlSinif.SelectedValue.ToInt32();
        int ogrenmealaniBrans = ddlBrans.SelectedValue.ToInt32();
        int ogrenmeAlaniNo = txtOgrenmeAlaniNo.Text.ToInt32();
        int anaKat = ddlAnaKat.SelectedValue.ToInt32();

        KOgrenmeAlanlariDB veriDb = new KOgrenmeAlanlariDB();

        KOgrenmeAlanlariInfo info = new KOgrenmeAlanlariInfo
        {
            AnaKat = ustOgrenmealani,
            BransId = ogrenmealaniBrans,
            OgrenmeAlani = ogrenmeAlani,
            Sinif = ogrenmealaniSinif,
            AlanNo = ogrenmeAlaniNo
        };
        if (id == 0)
        {
            if (veriDb.KayitKontrol(ogrenmealaniBrans, ogrenmealaniSinif, ogrenmeAlaniNo, anaKat))
            {
                Master.UyariTuruncu("Bu öğrenme alan numarası daha önceden kaydedilmiştir.", phUyari);
            }
            else
            {
                veriDb.KayitEkle(info);
                Master.UyariIslemTamam("Öğrenme alanı kaydedildi", phUyari);
                
                UstOgrenmeAlanlari();
                txtOgrenmeAlani.Text = "";
                txtOgrenmeAlaniNo.Text = "";
                ddlAnaKat.SelectedValue = "";
                hfOgrenmeAlaniId.Value = "0";
            }
        }
        else
        {
            if (veriDb.KayitKontrol(ogrenmealaniBrans, ogrenmealaniSinif, ogrenmeAlaniNo, id))
            {
                Master.UyariTuruncu("Bu öğrenme alan numarası daha önceden kaydedilmiştir.", phUyari);
            }
            else
            {
                info.Id = id;

                veriDb.KayitGuncelle(info);
                Master.UyariIslemTamam("Değişiklikler kaydedildi", phUyari);
                
                UstOgrenmeAlanlari();
                txtOgrenmeAlani.Text = "";
                txtOgrenmeAlaniNo.Text = "";
                hfOgrenmeAlaniId.Value = "0";
                ddlAnaKat.SelectedValue = "";
                btnOgrenmeAlaniKaydet.Text = "Kaydet";
            }
        }
        TablariKapat();
        tabliOgrenmeAlanlari.Attributes.Add("class", "active");
        OgrenmeAlanlari.Attributes.Add("class", "tab-pane active");
        
        rptUstOgrenmeAlanlari.DataSource = veriDb.KayitlariGetir(ogrenmealaniBrans, ogrenmealaniSinif);
        rptUstOgrenmeAlanlari.DataBind();
    }

    protected void rptUstOgrenmeAlanlari_OnItemDataBound(object sender, RepeaterItemEventArgs e)
    {
        if (e.Item.ItemType == ListItemType.AlternatingItem || e.Item.ItemType == ListItemType.Item)
        {
            GenelIslemler.SiraNumarasiForRepeater(e, "lblSira");

            Literal ltrBransHarfi = (Literal)e.Item.FindControl("ltrBransHarfi");
            Literal ltrAnaKat = (Literal)e.Item.FindControl("ltrAnaKat");
            Literal ltrOgrenmeAlani = (Literal)e.Item.FindControl("ltrOgrenmeAlani");

            int bransId = DataBinder.Eval(e.Item.DataItem, "BransId").ToInt32();
            int anaKat = DataBinder.Eval(e.Item.DataItem, "AnaKat").ToInt32();
            string ogrenmeAlani = DataBinder.Eval(e.Item.DataItem, "OgrenmeAlani").ToString();
            if (anaKat != 0)
            {
                ltrOgrenmeAlani.Text =ogrenmeAlani ;
                ltrAnaKat.Text = "." + anaKat;
            }
            else
            {
                ltrOgrenmeAlani.Text = string.Concat("<strong>",ogrenmeAlani,"</strong>");
            }
            BranslarDb brnsDb = new BranslarDb();
            BranslarInfo info = brnsDb.KayitBilgiGetir(bransId);
            ltrBransHarfi.Text = info.BransAdi.Substring(0, 1);
        }
    }
    protected void ddlSinif_OnSelectedIndexChanged(object sender, EventArgs e)
    {
        int sinif = ddlSinif.SelectedValue.ToInt32();
        int brans = ddlBrans.SelectedValue.ToInt32();
        if (sinif != 0 && brans != 0)
        {
            KOgrenmeAlanlariDB uoaDb = new KOgrenmeAlanlariDB();
            rptUstOgrenmeAlanlari.DataSource = uoaDb.KayitlariGetir(brans, sinif);
            rptUstOgrenmeAlanlari.DataBind();

            UstOgrenmeAlanlari();

            KazanimlariGetir();
        }
    }
    protected void ddlBrans_OnSelectedIndexChanged(object sender, EventArgs e)
    {
        int sinif = ddlSinif.SelectedValue.ToInt32();
        int brans = ddlBrans.SelectedValue.ToInt32();
        if (sinif != 0 && brans != 0)
        {
            KOgrenmeAlanlariDB uoaDb = new KOgrenmeAlanlariDB();
            rptUstOgrenmeAlanlari.DataSource = uoaDb.KayitlariGetir(brans, sinif);
            rptUstOgrenmeAlanlari.DataBind();

            UstOgrenmeAlanlari();
            KazanimlariGetir();
        }
    }
    protected void rptUstOgrenmeAlanlari_OnItemCommand(object source, RepeaterCommandEventArgs e)
    {
        int id = e.CommandArgument.ToInt32();
        hfOgrenmeAlaniId.Value = id.ToString();
        KOgrenmeAlanlariDB veriDb = new KOgrenmeAlanlariDB();
        KOgrenmeAlanlariInfo info = veriDb.KayitBilgiGetir(id);
        int alanNo, altAlanNo;
        //Eğer anakat=0 ise id değeri üst öğrenme alanı
        //Eğer anakat!=0 ise id alt öğrenme alanı anakat üst öğrenme alanı
        if (info.AnaKat == 0)
        {
            alanNo = id;
            altAlanNo = 0;
        }
        else
        {
            alanNo = info.AnaKat;
            altAlanNo = id;
        }
        if (e.CommandName.Equals("Sil"))
        {
            KazanimlarDB kznmDb = new KazanimlarDB();
            if (kznmDb.KayitKontrol(info.BransId, info.Sinif, alanNo, altAlanNo))
            {
                Master.UyariTuruncu("Bu öğrenim alanına ait kazanımlar bulunduğu için silinemedi.", phUyari);
            }
            else
            {
                veriDb.KayitSil(id);
                Master.UyariIslemTamam("Öğrenim alanı başarıyla silindi.", phUyari);
                UstOgrenmeAlanlari();

                int sinif = ddlSinif.SelectedValue.ToInt32();
                int brans = ddlBrans.SelectedValue.ToInt32();
                ddlBrans.SelectedValue = brans.ToString();
                ddlSinif.SelectedValue = sinif.ToString();
                
                rptUstOgrenmeAlanlari.DataSource = veriDb.KayitlariGetir(brans, sinif);
                rptUstOgrenmeAlanlari.DataBind();
            }
        }
        if (e.CommandName.Equals("Duzenle"))
        {
            ddlSinif.SelectedValue = info.Sinif.ToString();
            ddlBrans.SelectedValue = info.BransId.ToString();
            txtOgrenmeAlani.Text = info.OgrenmeAlani;
            txtOgrenmeAlaniNo.Text = info.AlanNo.ToString();
            ddlAnaKat.SelectedValue = info.AnaKat.ToString();
            hfOgrenmeAlaniId.Value = info.Id.ToString();
            
            btnOgrenmeAlaniKaydet.Text = "Değişiklikleri Kaydet";
        }
        TablariKapat();
        tabliOgrenmeAlanlari.Attributes.Add("class", "active");
        OgrenmeAlanlari.Attributes.Add("class", "tab-pane active");
    }
    private void TablariKapat()
    {

        tabliKazanimlar.Attributes.Add("class", "");
        Kazanimlar.Attributes.Add("class", "tab-pane");
        tabliOgrenmeAlanlari.Attributes.Add("class", "");
        OgrenmeAlanlari.Attributes.Add("class", "tab-pane ");
    }
    protected void btnOgrenmeAlaniVazgec_OnClick(object sender, EventArgs e)
    {
        txtOgrenmeAlani.Text = "";
        txtOgrenmeAlaniNo.Text = "";
        ddlAnaKat.SelectedValue = "";
        hfOgrenmeAlaniId.Value = "0";
        btnOgrenmeAlaniKaydet.Text = "Kaydet";
    }
    protected void ddlKazanimOgrenmeAlani_OnSelectedIndexChanged(object sender, EventArgs e)
    {
        int ogrenmeAlani = ddlKazanimOgrenmeAlani.SelectedValue.ToInt32();
        KazanimaAltOgrenmealanlariniGetir(ogrenmeAlani);
    }

    private void KazanimaAltOgrenmealanlariniGetir(int ogrenmeAlani)
    {
        int sinif = ddlSinif.SelectedValue.ToInt32();
        int brans = ddlBrans.SelectedValue.ToInt32();
        KOgrenmeAlanlariDB uoaDb = new KOgrenmeAlanlariDB();
        ddlKazanimAltOgrenmeAlani.DataSource = uoaDb.KayitlariGetir(ogrenmeAlani, brans, sinif);
        ddlKazanimAltOgrenmeAlani.DataValueField = ddlAnaKat.DataValueField = "AlanNo";
        ddlKazanimAltOgrenmeAlani.DataTextField = ddlAnaKat.DataTextField = "OgrenmeAlani";
        ddlKazanimAltOgrenmeAlani.DataBind();
        ddlKazanimAltOgrenmeAlani.Items.Insert(0, new ListItem("Seçiniz", ""));
    }

    protected void btnKazanimKaydet_OnClick(object sender, EventArgs e)
    {
        int id = hfKazanimId.Value.ToInt32();
        string kazanim = txtKazanim.Text;
        int kazanimNo = txtKazanimNo.Text.ToInt32();
        int ogrenmealani = ddlKazanimOgrenmeAlani.SelectedValue.ToInt32();
        int altOgrenmeAlani = ddlKazanimAltOgrenmeAlani.SelectedValue.ToInt32();
        int ogrenmealaniSinif = ddlSinif.SelectedValue.ToInt32();
        int ogrenmealaniBrans = ddlBrans.SelectedValue.ToInt32();
        
        KazanimlarDB veriDb = new KazanimlarDB();
        KazanimlarInfo info = new KazanimlarInfo
        {
            AltOgrenmeAlani = altOgrenmeAlani,
            OgrenmeAlani = ogrenmealani,
            BransId = ogrenmealaniBrans,
            Sinif = ogrenmealaniSinif,
            Kazanim = kazanim,
            KazanimNo = kazanimNo
        };
        if (id==0)
        {
            if (veriDb.KayitKontrol(ogrenmealaniBrans, ogrenmealaniSinif, ogrenmealani, altOgrenmeAlani,kazanimNo))
            {
                Master.UyariTuruncu("Bu kazanım daha önceden kaydedilmiştir.", phUyari);
            }
            else
            {
                veriDb.KayitEkle(info);
                Master.UyariIslemTamam("Kazanım kaydedildi", phUyari);

            }
        }
        else
        {
            if (veriDb.KayitKontrol(ogrenmealaniBrans, ogrenmealaniSinif, ogrenmealani, altOgrenmeAlani, kazanimNo,id))
            {
                Master.UyariTuruncu("Bu kazanım daha önceden kaydedilmiştir.", phUyari);
            }
            else
            {
                info.Id = id;
                veriDb.KayitGuncelle(info);
                Master.UyariIslemTamam("Değişiklikler kaydedildi", phUyari);

            }
        }
        KazanimlariGetir();
        txtKazanim.Text = "";
        hfKazanimId.Value = "0";
        txtKazanimNo.Text = "";
        TablariKapat();
        tabliKazanimlar.Attributes.Add("class", "active");
        Kazanimlar.Attributes.Add("class", "tab-pane active");
    }
    protected void btnKazanimVazgec_OnClick(object sender, EventArgs e)
    {
        txtKazanim.Text = "";
        txtKazanimNo.Text = "";
        btnKazanimKaydet.Text = "Kaydet";
        hfKazanimId.Value = "0";
    }
    protected void rptKazanimlar_OnItemDataBound(object sender, RepeaterItemEventArgs e)
    {
        if (e.Item.ItemType == ListItemType.AlternatingItem || e.Item.ItemType == ListItemType.Item)
        {
            GenelIslemler.SiraNumarasiForRepeater(e, "lblSira");
            Literal ltrBransHarfi = (Literal)e.Item.FindControl("ltrBransHarfi");
            int bransId = DataBinder.Eval(e.Item.DataItem, "BransId").ToInt32();
            BranslarDb brnsDb = new BranslarDb();
            BranslarInfo info = brnsDb.KayitBilgiGetir(bransId);
            ltrBransHarfi.Text = info.BransAdi.Substring(0, 1);
        }
    }
    protected void rptKazanimlar_OnItemCommand(object source, RepeaterCommandEventArgs e)
    {
        int id = e.CommandArgument.ToInt32();
        hfOgrenmeAlaniId.Value = id.ToString();
        KazanimlarDB veriDb = new KazanimlarDB();
        KazanimlarInfo info = veriDb.KayitBilgiGetir(id);
        
        if (e.CommandName.Equals("Sil"))
        {
           
                veriDb.KayitSil(id);
                Master.UyariIslemTamam("Kazanım başarıyla silindi.", phUyari);
                UstOgrenmeAlanlari();

                KazanimlariGetir();
        }
        if (e.CommandName.Equals("Duzenle"))
        {
            ddlSinif.SelectedValue = info.Sinif.ToString();
            ddlBrans.SelectedValue = info.BransId.ToString();
            txtKazanimNo.Text = info.KazanimNo.ToString();
            txtKazanim.Text = info.Kazanim;
            ddlKazanimOgrenmeAlani.SelectedValue = info.OgrenmeAlani.ToString();

            KazanimaAltOgrenmealanlariniGetir(info.OgrenmeAlani);
            ddlKazanimAltOgrenmeAlani.SelectedValue = info.AltOgrenmeAlani.ToString();
            hfKazanimId.Value = info.Id.ToString();
            
            btnKazanimKaydet.Text = "Değişiklikleri Kaydet";
        }
        TablariKapat();
        tabliKazanimlar.Attributes.Add("class", "active");
        Kazanimlar.Attributes.Add("class", "tab-pane active");
    }
}