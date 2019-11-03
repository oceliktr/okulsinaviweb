using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.UI.WebControls;
using DAL;
using SoruBankasi;

public partial class SoruBank_SoruEkle : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            MaddeTurleri maddeTurleri = new MaddeTurleri();
            List<MaddeTurleriInfo> lst = maddeTurleri.MaddeTurleriniGetir();
            ddlMaddeTuru.DataSource = lst;
            ddlMaddeTuru.DataValueField = "Id";
            ddlMaddeTuru.DataTextField = "MaddeTuru";
            ddlMaddeTuru.DataBind();
            ddlMaddeTuru.Items.Insert(0, new ListItem("--- Seçiniz ---", ""));

            BranslarDb brnsDb = new BranslarDb();
            ddlBrans.DataSource = brnsDb.KayitlariGetir();
            ddlBrans.DataTextField = "BransAdi";
            ddlBrans.DataValueField = "Id";
            ddlBrans.DataBind();
            ddlBrans.Items.Insert(0, new ListItem("--- Seçiniz ---", ""));

            KullanicilarDb kDb = new KullanicilarDb();
            KullanicilarInfo kInfo = kDb.KayitBilgiGetir(Master.UyeId());

            ddlBrans.SelectedValue = kInfo.Bransi.ToString();
            if (!kInfo.Yetki.Contains("Admin") && (kInfo.Yetki.Contains("Ogretmen|") || kInfo.Yetki.Contains("UstDegerlendirici|")))
            {
                ddlBrans.Enabled = false;
                divDurum.Visible = false;
            }

            if (Request.QueryString["Id"] != null)
            {
                int soruId = Request.QueryString["Id"].ToInt32();
                hfSoruId.Value = soruId.ToString();

                if (soruId != 0)
                {
                    SbMaddeKokleriDB sbMKDb = new SbMaddeKokleriDB();
                    SbMaddeKokleriInfo sbMkInfo = sbMKDb.KayitBilgiGetir(soruId);
                    if (sbMkInfo.Id != 0)
                    {
                        ddlZorlukDerecesi.SelectedValue = sbMkInfo.ZorlukOgretmen;
                        txtMaddeBilgi.Text = sbMkInfo.Bilgi;
                        ddlDurum.SelectedValue = sbMkInfo.Durum.ToString();

                        txtMaddeKoku.Text = sbMkInfo.SoruKoku;

                        SbSeceneklerDB scnkDb = new SbSeceneklerDB();
                        List<SbSeceneklerInfo> scnkInfo = scnkDb.KayitlariGetir(soruId);
                        foreach (var scnk in scnkInfo)
                        {
                            if (scnk.SAdi == "A")
                            {
                                if (scnk.Dogru == 1)
                                    rbSecenekA.Checked = true;

                                txtSecenekA.Text = scnk.Secenek;
                            };
                            if (scnk.SAdi == "B")
                            {
                                if (scnk.Dogru == 1)
                                    rbSecenekB.Checked = true;
                                txtSecenekB.Text = scnk.Secenek;
                            };
                            if (scnk.SAdi == "C")
                            {
                                if (scnk.Dogru == 1)
                                    rbSecenekC.Checked = true;
                                txtSecenekC.Text = scnk.Secenek;
                            };
                            if (scnk.SAdi == "D")
                            {
                                if (scnk.Dogru == 1)
                                    rbSecenekD.Checked = true;
                                txtSecenekD.Text = scnk.Secenek;
                            };
                            if (scnk.SAdi == "E")
                            {
                                if (scnk.Dogru == 1)
                                    rbSecenekE.Checked = true;
                                txtSecenekE.Text = scnk.Secenek;
                            };
                        }

                        divAsama1.Visible = false;
                        divAsama2.Visible = false;
                        MaddeEkleAsamasi(sbMkInfo.Sinif);
                    }
                }
            }

        }
    }
    private void UstOgrenmeAlanlari()
    {
        int sinif = ddlSinif.SelectedValue.ToInt32();
        int brans = ddlBrans.SelectedValue.ToInt32();

        KOgrenmeAlanlariDB uoaDb = new KOgrenmeAlanlariDB();
        ddlOgrenmeAlani.DataSource = uoaDb.KayitlariGetir(0, brans, sinif);
        ddlOgrenmeAlani.DataValueField = "AlanNo";
        ddlOgrenmeAlani.DataTextField = "NoOgrenmeAlani";
        ddlOgrenmeAlani.DataBind();

        ddlOgrenmeAlani.Items.Insert(0, new ListItem("Seçiniz", ""));
        ddlAltOgrenmeAlani.Items.Clear();
        ddlAltOgrenmeAlani.Items.Insert(0, new ListItem("Öğrenme Alanı Seçiniz", ""));
    }
    private void AltOgrenmealanlariniGetir(int ogrenmeAlani)
    {
        int sinif = ddlSinif.SelectedValue.ToInt32();
        int brans = ddlBrans.SelectedValue.ToInt32();
        KOgrenmeAlanlariDB uoaDb = new KOgrenmeAlanlariDB();
        ddlAltOgrenmeAlani.DataSource = uoaDb.KayitlariGetir(ogrenmeAlani, brans, sinif);
        ddlAltOgrenmeAlani.DataValueField = "AlanNo";
        ddlAltOgrenmeAlani.DataTextField = "NoOgrenmeAlani";
        ddlAltOgrenmeAlani.DataBind();
        ddlAltOgrenmeAlani.Items.Insert(0, new ListItem("Seçiniz", ""));
    }
    protected void btnAsama1_OnClick(object sender, EventArgs e)
    {
        if (ddlMaddeTuru.SelectedValue.ToInt32() != MaddeTurleriEnum.CoktanSecmeli.ToInt32())
        {
            Master.UyariTuruncu(ddlMaddeTuru.SelectedItem.Text + " için alt yapımız henüz hazır değildir.", phUyari);
        }
        else
        {
            divAsama1.Visible = false;
            divAsama2.Visible = true;
            lbKazanimlar.Items.Clear();
            UstOgrenmeAlanlari();
        }
    }
    protected void ddlOgrenmeAlani_OnSelectedIndexChanged(object sender, EventArgs e)
    {
        int sinif = ddlSinif.SelectedValue.ToInt32();
        int brans = ddlBrans.SelectedValue.ToInt32();
        int ogrenmeAlani = ddlOgrenmeAlani.SelectedValue.ToInt32();
        AltOgrenmealanlariniGetir(ogrenmeAlani);


        ListboxWithTooltip kazanimlar = new ListboxWithTooltip();
        kazanimlar.Kazanimlar(lbKazanimlar, sinif, brans, ogrenmeAlani, 0);
    }
    protected void btnAsama1eDon_OnClick(object sender, EventArgs e)
    {
        divAsama1.Visible = true;
        divAsama2.Visible = false;
        divAsama3MaddeEkle.Visible = false;
        divAsama4BilgiEkle.Visible = false;
    }
    protected void ddlAltOgrenmeAlani_OnSelectedIndexChanged(object sender, EventArgs e)
    {
        int sinif = ddlSinif.SelectedValue.ToInt32();
        int brans = ddlBrans.SelectedValue.ToInt32();
        int ogrenmeAlani = ddlOgrenmeAlani.SelectedValue.ToInt32();
        int altOgrenmeAlani = ddlAltOgrenmeAlani.SelectedValue.ToInt32();


        ListboxWithTooltip kazanimlar = new ListboxWithTooltip();
        kazanimlar.Kazanimlar(lbKazanimlar, sinif, brans, ogrenmeAlani, altOgrenmeAlani);
    }
    protected void btnKazanimlar_OnClick(object sender, EventArgs e)
    {
        int sinif = ddlSinif.SelectedValue.ToInt32();
        MaddeEkleAsamasi(sinif);
    }
    private void MaddeEkleAsamasi(int sinif)
    {
        divSecenekD.Visible = sinif > 3;
        divSecenekE.Visible = sinif > 8;


        divAsama2.Visible = false;
        divAsama3MaddeEkle.Visible = true;


        phMaddeBilgi.Visible = false;
        phMeddeveCevaplar.Visible = true;
    }


    protected void btnAsama3Devam_Click(object sender, EventArgs e)
    {
        divAsama3MaddeEkle.Visible = false;
        divAsama4BilgiEkle.Visible = true;
        
        phMaddeBilgi.Visible = true;
        phMeddeveCevaplar.Visible = false;
    }
    protected void btnTamamla_OnClick(object sender, EventArgs e)
    {
        int soruId = hfSoruId.Value.ToInt32();
        
        divAsama4BilgiEkle.Visible = false;
        divAsama1.Visible = true;

        string kazanimlar = "|";
        foreach (ListItem li in lbKazanimlar.Items)
        {
            if (li.Selected)
                kazanimlar += li.Value + "|";
        }

        if (soruId == 0)
        {
            SbMaddeKokleriDB veriDb = new SbMaddeKokleriDB();
            SbMaddeKokleriInfo info = new SbMaddeKokleriInfo
            {
                Bilgi = txtMaddeBilgi.Text,
                Brans = ddlBrans.SelectedValue.ToInt32(),
                Kazanim = kazanimlar,
                MaddeTuru = ddlMaddeTuru.SelectedValue.ToInt32(),
                Sinif = ddlSinif.SelectedValue.ToInt32(),
                SoruKoku = txtMaddeKoku.Text,
                ZorlukOgretmen = ddlZorlukDerecesi.SelectedValue,
                UyeId = Master.UyeId(),
                Durum = ddlDurum.SelectedValue.ToInt32()
            };
            veriDb.KayitEkle(info);


            int sonId = veriDb.SonKayitId();

            SbSeceneklerDB scnkDb = new SbSeceneklerDB();
            SbSeceneklerInfo scnkInfo = new SbSeceneklerInfo
            {
                Dogru = rbSecenekA.Checked.ToInt32(),
                SAdi = "A",
                Secenek = txtSecenekA.Text,
                SoruId = sonId
            };
            scnkDb.KayitEkle(scnkInfo);

            scnkInfo = new SbSeceneklerInfo
            {
                Dogru = rbSecenekB.Checked.ToInt32(),
                SAdi = "B",
                Secenek = txtSecenekB.Text,
                SoruId = sonId
            };
            scnkDb.KayitEkle(scnkInfo);

            scnkInfo = new SbSeceneklerInfo
            {
                Dogru = rbSecenekC.Checked.ToInt32(),
                SAdi = "C",
                Secenek = txtSecenekC.Text,
                SoruId = sonId
            };
            scnkDb.KayitEkle(scnkInfo);
            if (ddlSinif.SelectedValue.ToInt32() > 3)
            {
                scnkInfo = new SbSeceneklerInfo
                {
                    Dogru = rbSecenekD.Checked.ToInt32(),
                    SAdi = "D",
                    Secenek = txtSecenekD.Text,
                    SoruId = sonId
                };
                scnkDb.KayitEkle(scnkInfo);
            }

            if (ddlSinif.SelectedValue.ToInt32() > 8)
            {
                scnkInfo = new SbSeceneklerInfo
                {
                    Dogru = rbSecenekE.Checked.ToInt32(),
                    SAdi = "E",
                    Secenek = txtSecenekE.Text,
                    SoruId = sonId
                };
                scnkDb.KayitEkle(scnkInfo);
            }

            Master.UyariIslemTamam("Soru başarıyla kaydedildi. Sorularım bölümünden sorunun durumunu takip edebilirsiniz.", phUyari);
        }
        else
        {
            SbMaddeKokleriDB veriDb = new SbMaddeKokleriDB();
            SbMaddeKokleriInfo info = new SbMaddeKokleriInfo
            {
                Bilgi = txtMaddeBilgi.Text,
                SoruKoku = txtMaddeKoku.Text,
                ZorlukOgretmen = ddlZorlukDerecesi.SelectedValue,
                Durum = ddlDurum.SelectedValue.ToInt32(),
                Id = hfSoruId.Value.ToInt32()
            };
            veriDb.KayitGuncelle(info);

            SbSeceneklerDB scnkDb = new SbSeceneklerDB();
            SbSeceneklerInfo scnkInfo = new SbSeceneklerInfo
            {
                Dogru = rbSecenekA.Checked.ToInt32(),
                SAdi = "A",
                Secenek = txtSecenekA.Text,
                SoruId = soruId
            };
            scnkDb.KayitGuncelle(scnkInfo);

            scnkInfo = new SbSeceneklerInfo
            {
                Dogru = rbSecenekB.Checked.ToInt32(),
                SAdi = "B",
                Secenek = txtSecenekB.Text,
                SoruId = soruId
            };
            scnkDb.KayitGuncelle(scnkInfo);

            scnkInfo = new SbSeceneklerInfo
            {
                Dogru = rbSecenekC.Checked.ToInt32(),
                SAdi = "C",
                Secenek = txtSecenekC.Text,
                SoruId = soruId
            };
            scnkDb.KayitGuncelle(scnkInfo);


            scnkInfo = new SbSeceneklerInfo
            {
                Dogru = rbSecenekD.Checked.ToInt32(),
                SAdi = "D",
                Secenek = txtSecenekD.Text,
                SoruId = soruId
            };
            scnkDb.KayitGuncelle(scnkInfo);

            scnkInfo = new SbSeceneklerInfo
            {
                Dogru = rbSecenekE.Checked.ToInt32(),
                SAdi = "E",
                Secenek = txtSecenekE.Text,
                SoruId = soruId
            };
            scnkDb.KayitGuncelle(scnkInfo);

            Response.Redirect("Sorularim.aspx");
        }

        txtMaddeBilgi.Text = "";
        txtMaddeKoku.Text = "";
        txtSecenekA.Text = "";
        txtSecenekB.Text = "";
        txtSecenekC.Text = "";
        txtSecenekD.Text = "";
        txtSecenekE.Text = "";
        hfSoruId.Value = "0";
    }

}