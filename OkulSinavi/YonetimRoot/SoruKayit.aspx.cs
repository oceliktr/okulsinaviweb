using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class OkulSinavi_CevrimiciSinavYonetim_SoruKayitRoot : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
           
            BranslarDb brnsDb = new BranslarDb();
            ddlBrans.DataSource = brnsDb.KayitlariGetir();
            ddlBrans.DataTextField = "BransAdi";
            ddlBrans.DataValueField = "Id";
            ddlBrans.DataBind();
            ddlBrans.Items.Insert(0, new ListItem("Branş Seçiniz", ""));

            if (Request.QueryString["OturumId"] != null)
            {
                if (Request.QueryString["OturumId"].IsInteger())
                {
                    int oturumId = Request.QueryString["OturumId"].ToInt32();

                    TestOturumlarDb veriDb = new TestOturumlarDb();
                    TestOturumlarInfo info = veriDb.KayitBilgiGetir(oturumId);
                    txtOturmAdi.Text = info.OturumAdi;
                    hfOturum.Value = info.Id.ToString();

                    hlOturum.NavigateUrl = "OturumYonetim.aspx?SinavId=" + info.SinavId;

                    if (Request.QueryString["id"] != null)
                    {
                        if (Request.QueryString["id"].IsInteger())
                        {
                            int id = Request.QueryString["id"].ToInt32();

                            TestSorularDb soruDb = new TestSorularDb();
                            TestSorularInfo soruInfo = soruDb.KayitBilgiGetir(id);
                            hfId.Value = soruInfo.Id.ToString();
                            ddlBrans.SelectedValue = soruInfo.BransId.ToString();
                            txtSoruNo.Text = soruInfo.SoruNo.ToString();
                            ddlCevap.SelectedValue = soruInfo.Cevap;
                            if (soruInfo.Soru.Contains("<"))
                                txtSoru.Text = soruInfo.Soru;
                            else
                                txtUrl.Text = soruInfo.Soru;

                            btnKaydet.Text = "Değiştir";
                        }
                    }
                }
            }
        }
    }


    protected void btnKaydet_OnClick(object sender, EventArgs e)
    {
        string dosya = "";
        if (fuResim.HasFile)
        {
            string dosyaAdi = Server.HtmlEncode(fuResim.FileName);
            string uzanti = Path.GetExtension(dosyaAdi);
            if (uzanti != null)
            {
                //Dizin yoksa
                if (!DizinIslemleri.DizinKontrol(Server.MapPath("/upload/test/" + hfOturum.Value)))
                    Directory.CreateDirectory(Server.MapPath("/upload/test/" + hfOturum.Value));

                uzanti = uzanti.ToLower();
                string rastgeleMetin = GenelIslemler.RastgeleMetinUret(8);
                if (GenelIslemler.YuklenecekResimler.Contains(uzanti))
                {
                    dosyaAdi = string.Format("{0}{1}", rastgeleMetin, uzanti);
                    string dosyaYolu = string.Format(@"{0}upload\test\{1}\{2}", HttpContext.Current.Server.MapPath("/"), hfOturum.Value, dosyaAdi);
                    File.WriteAllBytes(dosyaYolu, fuResim.FileBytes);

                    dosya = string.Format(@"/upload/test/{0}/{1}", hfOturum.Value, dosyaAdi);
                }
                else
                {
                    Master.UyariKirmizi("Yalnızca " + GenelIslemler.YuklenecekResimler + " uzantılı dosyalar yüklenir.", phUyari);
                }
            }
        }

        int id = hfId.Value.ToInt32();
        int oturumId = hfOturum.Value.ToInt32();
        int soruNo = txtSoruNo.Text.ToInt32();
        int bransId = ddlBrans.SelectedValue.ToInt32();

        string soru;
        if (dosya == "" && txtUrl.Text == "")
        {
            soru = txtSoru.Text;
        }
        else
        {
            soru = dosya == "" ? txtUrl.Text : dosya;
        }
        TestSorularInfo info = new TestSorularInfo
        {
            BransId = bransId,
            SoruNo = soruNo,
            Cevap = ddlCevap.SelectedValue,
            Soru = soru,
            OturumId = oturumId,
            Id = id
        };
        if (soru == "" || bransId == 0 || soruNo == 0)
        {
            Master.UyariKirmizi("İlgili tüm alanları giriniz.", phUyari);
            return;
        }

        TestSorularDb veriDb = new TestSorularDb();
        var kontrol = veriDb.KayitKontrol(oturumId, soruNo, id);
        if (kontrol)
        {
            Master.UyariKirmizi("Bu soru daha önce kaydedilmiş. Bir oturumda soru numarası bir defa kullanılabilir.", phUyari);
        }
        else
        {
            if (hfId.Value == "0")
            {
                if (veriDb.KayitEkle(info) > 0)
                {
                    Master.UyariIslemTamam("Yeni bir soru eklendi.", phUyari);
                    FormuTemizle();
                    txtSoruNo.Text = (soruNo + 1).ToString();
                }
                else
                    Master.UyariTuruncu("Kayıt yapılamadı.", phUyari);
            }
            else
            {
                if (veriDb.KayitGuncelle(info) > 0)
                    Master.UyariIslemTamam("Değişiklikler kaydildi.", phUyari);
                else
                    Master.UyariTuruncu("Güncelleme yapılamadı.", phUyari);
            }

            CacheHelper.BranslarKaldir();
            CacheHelper.SinavdakiBranslarKaldir(oturumId);
            CacheHelper.SorulariGetirKaldir(oturumId);

        }
    }

    private void FormuTemizle()
    {
        txtUrl.Text = "";
        ddlCevap.SelectedValue = "";
        ddlCevap.SelectedValue = "";
    }

    protected void btnYukle_OnClick(object sender, EventArgs e)
    {
        string dosya = "";
        if (fuResim.HasFile)
        {
            string dosyaAdi = Server.HtmlEncode(fuResim.FileName);
            string uzanti = Path.GetExtension(dosyaAdi);
            if (uzanti != null)
            {
                //Dizin yoksa
                if (!DizinIslemleri.DizinKontrol(Server.MapPath("/upload/test/" + hfOturum.Value)))
                    Directory.CreateDirectory(Server.MapPath("/upload/test/" + hfOturum.Value));

                uzanti = uzanti.ToLower();
                string rastgeleMetin = GenelIslemler.RastgeleMetinUret(8);
                if (GenelIslemler.YuklenecekResimler.Contains(uzanti))
                {
                    dosyaAdi = string.Format("{0}{1}", rastgeleMetin, uzanti);
                    string dosyaYolu = string.Format(@"{0}upload\test\{1}\{2}", HttpContext.Current.Server.MapPath("/"), hfOturum.Value, dosyaAdi);
                    File.WriteAllBytes(dosyaYolu, fuResim.FileBytes);

                    dosya = string.Format(@"/upload/test/{0}/{1}", hfOturum.Value, dosyaAdi);
                }
                else
                {
                    Master.UyariKirmizi("Yalnızca " + GenelIslemler.YuklenecekResimler + " uzantılı dosyalar yüklenir.", phUyari);
                }
            }
        }

        txtUrl.Text = dosya;
    }
}