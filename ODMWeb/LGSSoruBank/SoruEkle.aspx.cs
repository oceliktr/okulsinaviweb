using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DAL;

public partial class LGSSoruBank_SoruEkle : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            if (Master.UyeId() == 0) Response.Redirect("~/ODM/Cikis.aspx");
            SinavlarDb sDb = new SinavlarDb();
            SinavlarInfo sinf = sDb.AktifSinavAdi();
            ltrDonemAdiBreadCrumb.Text = ltrDonemAdi.Text = sinf.SinavAdi;
            if (sinf.VeriGirisi == 0)
            {
                Master.UyariTuruncu(string.Format("<b>{0}</b> için veri girişleri kapatıldı.", sinf.SinavAdi), phUyari);
                divAsama1.Visible = false;
            }
            BranslarDb brnsDb = new BranslarDb();
            ddlBrans.DataSource = brnsDb.KayitlariGetir();
            ddlBrans.DataTextField = "BransAdi";
            ddlBrans.DataValueField = "Id";
            ddlBrans.DataBind();
            ddlBrans.Items.Insert(0, new ListItem("Branş Seçiniz", ""));


            ddlKazanim.Items.Insert(0, new ListItem("Kazanım Seçiniz", ""));

            KullanicilarDb kDb = new KullanicilarDb();
            KullanicilarInfo kInfo = kDb.KayitBilgiGetir(Master.UyeId());

            ddlBrans.SelectedValue = kInfo.Bransi.ToString();


            if (Request.QueryString["Id"] != null)
            {
                int soruId = Request.QueryString["Id"].ToInt32();

                if (soruId != 0)
                {
                    LgsSorularDB sbMKDb = new LgsSorularDB();
                    LgsSorularInfo sbMkInfo = sbMKDb.KayitBilgiGetir(soruId, Master.UyeId());
                    if (sbMkInfo.Id != 0)
                    {
                        hfSoruId.Value = soruId.ToString();
                        ddlSinif.SelectedValue = sbMkInfo.Sinif.ToString();

                        KazanimlariListele();

                        if (sbMkInfo.KazanimId != 0)
                            ddlKazanim.Text = sbMkInfo.KazanimId.ToString();
                    }
                }
            }
        }
    }
    protected void KazanimlariListele()
    {
        int bransId = ddlBrans.SelectedValue.ToInt32();
        int sinif = ddlSinif.SelectedValue.ToInt32();


        LgsKazanimlarDB kznmDb = new LgsKazanimlarDB();
        ddlKazanim.DataSource = kznmDb.KazanimNoKazanimBirlestir(bransId, sinif);
        ddlKazanim.DataValueField = "Id";
        ddlKazanim.DataTextField = "KazanimNoKazanim";
        ddlKazanim.DataBind();
        ddlKazanim.Items.Insert(0, new ListItem("Kazanım Seçiniz", ""));
    }

    protected void btnYukle_OnClick(object sender, EventArgs e)
    {
        int id = hfSoruId.Value.ToInt32();

        string yuklenecekDosyalar = ".doc,.docx";

        int sinif = ddlSinif.SelectedValue.ToInt32();

        SinavlarDb sDb = new SinavlarDb();
        SinavlarInfo sinav = sDb.AktifSinavAdi();
        int sinavId = sinav.SinavId;

        KullanicilarDb kDb = new KullanicilarDb();
        KullanicilarInfo kInfo = kDb.KayitBilgiGetir(Master.UyeId(), true);

        int kullaniciId = Master.UyeId();
        string kullanidiAdiSoyadi = kInfo.AdiSoyadi;
        int bransId = kInfo.Bransi;
        string bransAdi = kInfo.BransAdi;
        string dosya = "";
        if (fuDosya.HasFile)
        {
            string dosyaAdi = Server.HtmlEncode(fuDosya.FileName);
            string uzanti = Path.GetExtension(dosyaAdi);
            if (uzanti != null)
            {
                //Dizin yoksa
                if (!DizinIslemleri.DizinKontrol(Server.MapPath("/upload/lgs/" + sinavId + "/")))
                    Directory.CreateDirectory(Server.MapPath("/upload/lgs/" + sinavId + "/"));

                uzanti = uzanti.ToLower();
                string rastgeleMetin = GenelIslemler.RastgeleMetinUret(5);
                if (yuklenecekDosyalar.Contains(uzanti))
                {
                    dosyaAdi = string.Format("{0}_{1}_{2}_{3}{4}", sinif, bransAdi.ToUrl(), kullanidiAdiSoyadi.ToUrl(), rastgeleMetin, uzanti);
                    string dosyaYolu = string.Format(@"{0}upload\lgs\{1}\{2}", HttpContext.Current.Server.MapPath("/"), sinavId, dosyaAdi);
                    File.WriteAllBytes(dosyaYolu, fuDosya.FileBytes);

                    dosya = string.Format(@"/upload/lgs/{0}/{1}", sinavId, dosyaAdi);

                    if (id == 0)
                    {
                        KayitEkle(sinavId, bransId, kullaniciId, sinif, dosya);
                    }
                    else
                    {

                        LgsSorularDB sbMKDb = new LgsSorularDB();
                        LgsSorularInfo sbMkInfo = sbMKDb.KayitBilgiGetir(id, Master.UyeId());
                        //Güncelleme ile yeni dosya olacağı için önceki dosyayı sil
                        DizinIslemleri.DosyaSil(Server.MapPath(sbMkInfo.SoruUrl));

                        KayitGuncelle(id, kullaniciId, dosya);
                    }
                    //formu temizle
                }
                else
                {
                    Master.UyariKirmizi("Yalnızca " + yuklenecekDosyalar + " uzantılı dosyalar yüklenir.", phUyari);
                }
            }
        }
        else
        {
            if (id == 0)
            {
                Master.UyariTuruncu("Herhangi bir dosya seçmediniz.", phUyari);
            }
            else
            {
                LgsSorularDB sbMKDb = new LgsSorularDB();
                LgsSorularInfo sbMkInfo = sbMKDb.KayitBilgiGetir(id, Master.UyeId());
                dosya = sbMkInfo.SoruUrl;

                KayitGuncelle(id, kullaniciId, dosya);
            }
        }
    }

    private void KayitGuncelle(int id, int kullaniciId, string dosya)
    {
        LgsSorularDB sgDb = new LgsSorularDB();
        LgsSorularInfo sgInfo = sgDb.KayitBilgiGetir(id, kullaniciId);

        sgInfo.KazanimId = ddlKazanim.SelectedValue.ToInt32();
        sgInfo.SoruUrl = dosya;
        sgDb.KayitGuncelle(sgInfo);

        Master.UyariIslemTamam("Değişiklikler kaydedildi.", phUyari);
    }

    private void KayitEkle(int sinavId, int bransId, int kullaniciId, int sinif, string dosya)
    {
        LgsSorularDB sgDb = new LgsSorularDB();
        LgsSorularInfo sgInfo = new LgsSorularInfo
        {
            SinavId = sinavId,
            BransId = bransId,
            KazanimId = ddlKazanim.SelectedValue.ToInt32(),
            KullaniciId = kullaniciId,
            Onay = 0,
            Sinif = sinif,
            SoruUrl = dosya,
            Tarih = DateTime.Now
        };
        sgDb.KayitEkle(sgInfo);
        Master.UyariIslemTamam("Dosya başarıya yüklendi. Teşekkür ederiz.", phUyari);
    }

    protected void ddlSinif_OnSelectedIndexChanged(object sender, EventArgs e)
    {
        KazanimlariListele();
    }
}