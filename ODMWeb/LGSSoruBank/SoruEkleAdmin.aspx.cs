using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class LGSSoruBank_SoruEkleAdmin : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            if (!Master.Yetki().Contains("Root") && !Master.Yetki().Contains("Admin"))
                Response.Redirect("/ODM/Cikis.aspx");

            SinavlarDb sDb = new SinavlarDb();
            SinavlarInfo sinf = sDb.AktifSinavAdi();

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

           
            ddlSoruYazarlari.Items.Insert(0, new ListItem("Önce Branş Seçiniz", ""));
            ddlKazanim.Items.Insert(0, new ListItem("Kazanım Seçiniz", ""));


            SinavlarDb sinav = new SinavlarDb();
            ddlSinavlar.DataSource = sinav.KayitlariGetir();
            ddlSinavlar.DataValueField = "Id";
            ddlSinavlar.DataTextField = "SinavAdi";
            ddlSinavlar.DataBind();
            ddlSinavlar.Items.Insert(0, new ListItem("Sınav Seçiniz", ""));
            ddlSinavlar.SelectedValue = sinf.SinavId.ToString();


            if (Request.QueryString["Id"] != null)
            {
                int soruId = Request.QueryString["Id"].ToInt32();

                if (soruId != 0)
                {
                    LgsSorularDB sbMKDb = new LgsSorularDB();
                    LgsSorularInfo sbMkInfo = sbMKDb.KayitBilgiGetir(soruId);
                    if (sbMkInfo.Id != 0)
                    {
                        hfSoruId.Value = soruId.ToString();
                        ddlSinif.SelectedValue = sbMkInfo.Sinif.ToString();
                        txtKazanim.Text = sbMkInfo.Kazanim;
                        ddlBrans.SelectedValue = sbMkInfo.BransId.ToString();


                        KazanimlariListele();

                        KullanicilarDb kDb = new KullanicilarDb();
                        ddlSoruYazarlari.DataSource = kDb.OgretmenleriGetir("LgsYazari", sbMkInfo.BransId);
                        ddlSoruYazarlari.DataValueField = "Id";
                        ddlSoruYazarlari.DataTextField = "AdiSoyadi";
                        ddlSoruYazarlari.DataBind();
                        ddlSoruYazarlari.Items.Insert(0, new ListItem("Soru Yazarı Seçiniz", ""));

                       
                        ddlSoruYazarlari.SelectedValue = sbMkInfo.KullaniciId.ToString();

                        cbOnay.Checked = sbMkInfo.Onay.ToBoolean();
                    }
                }
            }
        }
    }

    protected void btnYukle_OnClick(object sender, EventArgs e)
    {
        string yuklenecekDosyalar = ".doc,.docx";

        int id = hfSoruId.Value.ToInt32();
         int sinavId = ddlSinavlar.SelectedValue.ToInt32();
        int kullaniciId =ddlSoruYazarlari.SelectedValue.ToInt32();
        int kazanimId =ddlKazanim.SelectedValue.ToInt32();
        int sinif = ddlSinif.SelectedValue.ToInt32();
        int onay = cbOnay.Checked?1:0;


        KullanicilarDb kDb = new KullanicilarDb();
        KullanicilarInfo kInfo = kDb.KayitBilgiGetir(kullaniciId, true);

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
                        KayitEkle(sinavId, bransId, kullaniciId, sinif, dosya,onay, kazanimId);
                    }
                    else
                    {

                        LgsSorularDB sbMKDb = new LgsSorularDB();
                        LgsSorularInfo sbMkInfo = sbMKDb.KayitBilgiGetir(id);
                        //Güncelleme ile yeni dosya olacağı için önceki dosyayı sil
                        DizinIslemleri.DosyaSil(Server.MapPath(sbMkInfo.SoruUrl));

                        LgsSorularInfo lgsInfo = new LgsSorularInfo
                        {
                            BransId = bransId,
                            SoruUrl = dosya,
                            SinavId = sinavId,
                            Sinif = sinif,
                            KazanimId = kazanimId,
                            KullaniciId = kullaniciId,
                            Onay = onay,
                            Id = id
                        };
                        sbMKDb.KayitGuncelleAdmin(lgsInfo);

                        Master.UyariIslemTamam("Değişiklikler kaydedildi.", phUyari);

                    }
                    //formu temizle
                    txtKazanim.Text = "";
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
                LgsSorularInfo sbMkInfo = sbMKDb.KayitBilgiGetir(id);
                dosya = sbMkInfo.SoruUrl;

                LgsSorularInfo lgsInfo = new LgsSorularInfo
                {
                    BransId = bransId,
                    SoruUrl = dosya,
                    SinavId = sinavId,
                    Sinif = sinif,
                    KazanimId = kazanimId,
                    KullaniciId = kullaniciId,
                    Onay = onay,
                    Id = id
                };
                sbMKDb.KayitGuncelleAdmin(lgsInfo);
                Master.UyariIslemTamam("Değişiklikler kaydedildi.", phUyari);
            }
        }
    }

    private void KayitEkle(int sinavId, int bransId, int kullaniciId, int sinif, string dosya,int onay,int kazanimId)
    {
        LgsSorularDB sgDb = new LgsSorularDB();
        LgsSorularInfo sgInfo = new LgsSorularInfo
        {
            BransId = bransId,
            SoruUrl = dosya,
            SinavId = sinavId,
            Sinif = sinif,
            KazanimId =kazanimId, 
            KullaniciId = kullaniciId,
            Onay = onay,
            Tarih = DateTime.Now
        };
        sgDb.KayitEkle(sgInfo);
        Master.UyariIslemTamam("Dosya başarıya yüklendi. Teşekkür ederiz.", phUyari);
    }

    protected void ddlBrans_OnSelectedIndexChanged(object sender, EventArgs e)
    {
        int bransId = ddlBrans.SelectedValue.ToInt32();
        KullanicilarDb kDb = new KullanicilarDb();

        ddlSoruYazarlari.DataSource = kDb.OgretmenleriGetir("LgsYazari",bransId);
        ddlSoruYazarlari.DataValueField = "Id";
        ddlSoruYazarlari.DataTextField = "AdiSoyadi";
        ddlSoruYazarlari.DataBind();
        ddlSoruYazarlari.Items.Insert(0, new ListItem("Soru Yazarı Seçiniz", ""));

        KazanimlariListele();
        
    }

    protected void KazanimlariListele()
    {
        int bransId = ddlBrans.SelectedValue.ToInt32();
        int sinif = ddlSinif.SelectedValue.ToInt32();
       

        LgsKazanimlarDb kznmDb = new LgsKazanimlarDb();
        ddlKazanim.DataSource = kznmDb.KazanimNoKazanimBirlestir(bransId, sinif);
        ddlKazanim.DataValueField = "Id";
        ddlKazanim.DataTextField = "KazanimNoKazanim";
        ddlKazanim.DataBind();
        ddlKazanim.Items.Insert(0, new ListItem("Kazanım Seçiniz", ""));
    }

    protected void ddlSinif_OnSelectedIndexChanged(object sender, EventArgs e)
    {
        KazanimlariListele();
    }
}