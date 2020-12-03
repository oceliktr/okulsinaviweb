﻿using System;
using System.Web.UI.WebControls;

public partial class OkulSinavi_CevrimiciSinavYonetim_OgrenciKayit : System.Web.UI.Page
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

            TestDonemDb dnmDb = new TestDonemDb();
            var donem = dnmDb.AktifDonem();
            txtDonem.Text = donem.Donem;

            
            OturumIslemleri oturum = new OturumIslemleri();
            KullanicilarInfo kInfo = oturum.OturumKontrol();
            if (kInfo.Yetki.Contains("OkulYetkilisi"))
            {
                if (donem.VeriGirisi == 0)
                {
                    kayitForm.Visible = false;
                    Master.UyariTuruncu("Şuan için kayıt işlemleri kapatılmıştır.", phUyari);
                }

                phOklIlce.Visible = false;
            }
            //düzeltme işlemleri
            if (Request.QueryString["Id"] != null)
            {
                if (Request.QueryString["Id"].IsInteger())
                {
                    int id = Request.QueryString["Id"].ToInt32();

                    TestKutukDb sinavDb = new TestKutukDb();
                    TestKutukInfo info = sinavDb.KayitBilgiGetir(id);

                    hfId.Value = info.Id.ToString();
                    txtAdi.Text = info.Adi;
                    txtSoyadi.Text = info.Soyadi;
                    ddlSinif.SelectedValue = info.Sinifi.ToString();
                    ddlIlce.SelectedValue = info.IlceAdi;

                    KurumlariGetir(IlceBilgisi(info.IlceAdi));

                    ddlKurum.SelectedValue = info.KurumKodu.ToString();
                    txtSube.Text = info.Sube;

                    btnKaydet.Text = "Değiştir";

                    var ogrDonemi = dnmDb.KayitBilgiGetir(info.DonemId);
                    txtDonem.Text = ogrDonemi.Donem;
                }
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

    protected void btnKaydet_OnClick(object sender, EventArgs e)
    {
        int id = hfId.Value.ToInt32();

        string ilce = ddlIlce.SelectedValue;
        int kurumkodu = ddlKurum.SelectedValue.ToInt32();

        OturumIslemleri oturum = new OturumIslemleri();
        KullanicilarInfo kInfo = oturum.OturumKontrol();
        if (kInfo.Yetki.Contains("OkulYetkilisi"))
        {
            IlcelerDb ilcelerDb = new IlcelerDb();
            var ilceInfo = ilcelerDb.KayitBilgiGetir(kInfo.IlceId);

            ilce = ilceInfo.IlceAdi;
            kurumkodu = kInfo.KurumKodu.ToInt32();
        }

        TestDonemDb dnmDb = new TestDonemDb();
        int donem = dnmDb.AktifDonem().Id;

        TestKutukDb veriDb = new TestKutukDb();

        if (hfId.Value == "0")
        {
            if (txtTcKimlik.Text.Length !=11)
            {
                Master.UyariKirmizi("Tc Kimlik numarası geçerli değil.", phUyari);
                return;
            }
            
            string opaqId = txtTcKimlik.Text.Md5Sifrele();
            var kontrol = veriDb.KayitKontrol(donem, opaqId, 0);
            if (kontrol)
            {
                Master.UyariKirmizi("Bu Tc kimlik numarası daha önce kaydedilmiş. Öğrenci listesini kontrol ediniz. Listede göremiyorsanız sistem yöneticisi ile iletişime geçiniz.", phUyari);
                return;
            }

            TestKutukInfo info = new TestKutukInfo
            {
                OpaqId = opaqId,
                Adi = txtAdi.Text.ToUpper(),
                Soyadi = txtSoyadi.Text.ToUpper(),
                IlceAdi = ilce,
                KurumKodu = kurumkodu,
                Sinifi = ddlSinif.SelectedValue.ToInt32(),
                Sube = txtSube.Text.ToUpper(),
                DonemId = donem
            };

            if (veriDb.KayitEkle(info) > 0)
            {
                Master.UyariIslemTamam("Yeni bir öğrenci eklendi.", phUyari);

                txtAdi.Text = "";
                txtSoyadi.Text = "";
                txtTcKimlik.Text = "";
                hfId.Value = "0";
            }
            else
                Master.UyariTuruncu("Kayıt yapılamadı.", phUyari);

        }
        else
        {
            string opaqId = "";
            if (txtTcKimlik.Text == "")
            {
                TestKutukInfo kullaniciInfo = veriDb.KayitBilgiGetir(id);
                opaqId = kullaniciInfo.OpaqId;
              
            }
            else
            {
                if (txtTcKimlik.Text.Length != 11)
                {
                    Master.UyariKirmizi("Tc Kimlik numarası geçerli değil.", phUyari);
                    return;
                }

                opaqId = txtTcKimlik.Text.Md5Sifrele();
            }
            var kontrol = veriDb.KayitKontrol(donem, opaqId, id);
            if (kontrol)
            {
                Master.UyariKirmizi("Bu Tc kimlik numarası daha önce kaydedilmiş. Öğrenci listesini kontrol ediniz. Listede göremiyorsanız sistem yöneticisi ile iletişime geçiniz.", phUyari);
                return;
            }
            TestKutukInfo info = new TestKutukInfo
            {
                OpaqId = opaqId,
                Adi = txtAdi.Text.ToUpper(),
                Soyadi = txtSoyadi.Text.ToUpper(),
                IlceAdi = ilce,
                KurumKodu = kurumkodu,
                Sinifi = ddlSinif.SelectedValue.ToInt32(),
                Sube = txtSube.Text.ToUpper(),
                Id = id
            };


            if (veriDb.KayitGuncelle(info) > 0)
            {
                Master.UyariIslemTamam("Değişiklikler kaydildi.", phUyari);

                txtAdi.Text = "";
                txtSoyadi.Text = "";
                txtTcKimlik.Text = "";
                txtSube.Text = "";
                hfId.Value = "0";
            }
            else
                Master.UyariTuruncu("Güncelleme yapılamadı.", phUyari);
        }

    }

    protected void ddlIlce_OnSelectedIndexChanged(object sender, EventArgs e)
    {
        string ilce = ddlIlce.SelectedValue;
        KurumlariGetir(IlceBilgisi(ilce));
    }



}
