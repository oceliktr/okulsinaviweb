using System;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using DAL;

namespace ODM
{
    public partial class OdmDegerlendirmeUst : Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (IsPostBack) return;
            if (!Master.Yetki().Contains("Admin") && !Master.Yetki().Contains("UstDegerlendirici"))
                Response.Redirect("Giris.aspx");

            HtmlGenericControl myBody = (HtmlGenericControl)Master.FindControl("myBody");
            myBody.Attributes.Add("class", "skin-purple sidebar-collapse"); 

            CevapGetir();
        }

        private void CevapGetir()
        {
            int uyeId = Master.UyeId();
            SinavlarDb snv = new SinavlarDb();
            SinavlarInfo snvInfo = snv.AktifSinavAdi();
            if (snvInfo.VeriGirisi == 0) Response.Redirect("Giris.aspx");

            KullanicilarDb kDb = new KullanicilarDb();
            KullanicilarInfo ki = kDb.KayitBilgiGetir(uyeId);

            CevaplarDb veriDb = new CevaplarDb();
            int atananCkSayisiUp = veriDb.UstPCKSayisi(snvInfo.SinavId, ki.Bransi);
            int cevaplananCkSayisiUp = veriDb.UstPCevaplananCKSayisi(snvInfo.SinavId, ki.Bransi);

            if (atananCkSayisiUp == cevaplananCkSayisiUp)
            {
                divDegerlendirmeEkrani.Visible = false;
                Master.UyariIslemTamam("Değerlendirilecek cevap kalmadı. Teşekkürler.", phUyari);
                return;
            }
            int oran = ((cevaplananCkSayisiUp+1) * 100) / atananCkSayisiUp;

            divProgres.Attributes.Add("style", "width: " + oran + "%");
            ltrAtananCKSayisi.Text = atananCkSayisiUp.ToString();
            ltrKalanCKSayisi.Text = (cevaplananCkSayisiUp+1).ToString();


            CevaplarInfo info = veriDb.KayitBilgiGetirUstDegerlendirici(snvInfo.SinavId, ki.Bransi);
            hfId.Value = info.Id.ToString();
        
            imgDosya.ImageUrl = string.Format("Cevaplar/{0}/{1}", snvInfo.SinavId, info.Dosya);

            RubrikDb rbDb = new RubrikDb();
            RubrikInfo rinf = rbDb.KayitBilgiGetir(snvInfo.SinavId, info.SoruNo);

            divKismiCevap.Visible = divKismiPuan.Visible = tab_2.Visible = rbKismi.Visible = rinf.KismiPuan != 0;

            BranslarDb brnsDb = new BranslarDb();
            BranslarInfo brsInfo = brnsDb.KayitBilgiGetir(rinf.BransId);
            ltrSoruNo.Text = rinf.SoruNo.ToString();
            ltrKismiPuan.Text = rinf.KismiPuan.ToString();
            ltrTamPuan.Text = rinf.Tampuan.ToString();
            ltrBransAdi.Text = brsInfo.BransAdi;
            ltrDogruCevap.Text = rinf.DogruCevap;
            ltrYanlisCevap.Text = rinf.YanlisCevap;
            ltrKismiCevap.Text = rinf.KismiCevap;
            ltrSoru.Text = rinf.Soru;

            if (hfSoruNo.Value != rinf.SoruNo.ToString())
                Master.UyariTuruncu("Bu yeni bir soruya ait cevap!", phUyariYeniSoru);
            hfSoruNo.Value = rinf.SoruNo.ToString();

            string cevapA = "";
            if (info.DegerlendiriciAPuani == rinf.KismiPuan && rinf.KismiPuan != 0)
                cevapA = "Kısmi Puan";
            else if (info.DegerlendiriciAPuani == rinf.Tampuan)
                cevapA = "Tam Puan";
            else if (info.DegerlendiriciAPuani == 0)
            {
                cevapA = info.BosA == 1 ? "Boş" : "Yanlış";
            }
            ltrDegerlendiriciA.Text = string.Format("A Grubundaki Değerlendirici Puanı : <strong>{0}</strong>", cevapA);

            string cevapB = "";
            if (info.DegerlendiriciBPuani == rinf.KismiPuan && rinf.KismiPuan!=0)
                cevapB = "Kısmi Puan";
            else if (info.DegerlendiriciBPuani == rinf.Tampuan)
                cevapB = "Tam Puan";
            else if (info.DegerlendiriciBPuani == 0)
            {
                cevapB = info.BosB == 1 ? "Boş" : "Yanlış";
            }
            ltrDegerlendiriciB.Text = string.Format("B Grubundaki Değerlendirici Puanı : <strong>{0}</strong>", cevapB);
        }

        protected void btnKaydet_OnClick(object sender, EventArgs e)
        {
            if (rbKismi.Checked || rbTampuan.Checked || rbYanlis.Checked || rbBos.Checked)
            {
                int id = hfId.Value.ToInt32();

                CevaplarDb veriDb = new CevaplarDb();
                CevaplarInfo info = veriDb.KayitBilgiGetir(id);


                RubrikDb rbDb = new RubrikDb();
                RubrikInfo rinf = rbDb.KayitBilgiGetir(info.SinavId, info.SoruNo);

                int bos = rbBos.Checked ? 1 : 0;
                int puani = 0;
                if (rbKismi.Checked)
                    puani = rinf.KismiPuan;
                else if (rbTampuan.Checked)
                    puani = rinf.Tampuan;
                else if (rbYanlis.Checked)
                    puani = 0;
                else if (rbBos.Checked)
                    puani = 0;

                veriDb.UdDegerlendirdi(id, puani, bos);
            
                rbBos.Checked = false;
                rbYanlis.Checked = false;
                rbKismi.Checked = false;
                rbTampuan.Checked = false;

                CevapGetir();
            }
            else
            {
                Master.UyariKirmizi("Puanlama seçeneklerinden birini işaretlemediniz.", phUyari);
            }
        }
    }
}