using System;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using DAL;

namespace ODM
{
    public partial class OdmDegerlendirme : Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (IsPostBack) return;
            if (!Master.Yetki().Contains("Admin") && !Master.Yetki().Contains("Ogretmen"))
                Response.Redirect("Giris.aspx");

            HtmlGenericControl myBody = (HtmlGenericControl)Master.FindControl("myBody");
            myBody.Attributes.Add("class", "skin-purple sidebar-collapse"); 


            CevapGetir();
        }

        private void CevapGetir()
        {
            int uyeId = Master.UyeId();
            KullanicilarDb kDb = new KullanicilarDb();
            KullanicilarInfo ki = kDb.KayitBilgiGetir(uyeId);

            SinavlarDb snv = new SinavlarDb();
            SinavlarInfo snvInfo = snv.AktifSinavAdi();
            if (snvInfo.VeriGirisi == 0) Response.Redirect("Giris.aspx");

            SonucAuDB veriDb = new SonucAuDB();

            int atananCkSayisi = ki.Grup == "A" ? veriDb.CevaplanacakCkSayisiA(snvInfo.SinavId, uyeId) : veriDb.CevaplanacakCkSayisiB(snvInfo.SinavId, uyeId);
            int cevaplananCkSayisi = ki.Grup == "A" ? veriDb.CevaplananCkSayisiA(snvInfo.SinavId, uyeId) : veriDb.CevaplananCkSayisiB(snvInfo.SinavId, uyeId);

            if (atananCkSayisi == cevaplananCkSayisi)
            {
                divDegerlendirmeEkrani.Visible = false;
                Master.UyariIslemTamam("Değerlendirilecek cevap kalmadı. Teşekkürler.", phUyari);
                return;
            }
            int oran = (cevaplananCkSayisi * 100) / atananCkSayisi;

            divProgres.Attributes.Add("style", "width: " + oran + "%");
            ltrAtananCKSayisi.Text = atananCkSayisi.ToString();
            ltrKalanCKSayisi.Text = cevaplananCkSayisi.ToString();

            SonucAuInfo info = veriDb.KayitBilgiGetirDegerlendirici(snvInfo.SinavId, uyeId, ki.Grup);
            hfId.Value = info.Id.ToString();

            btnKaydet.Text = info.Id.ToString();

            imgDosya.ImageUrl = string.Format("Cevaplar/{0}/{1}", snvInfo.SinavId, info.Dosya);

            RubrikDb rbDb = new RubrikDb();
            RubrikInfo rinf = rbDb.KayitBilgiGetir(snvInfo.SinavId, info.SoruNo);

            divKismiCevap.Visible = divKismiPuan.Visible = tab_2.Visible = rbKismi.Visible = rinf.KismiPuan != 0;

            BranslarDb brnsDb = new BranslarDb();
            ltrSoruNo.Text = rinf.SoruNo.ToString();
            ltrKismiPuan.Text = rinf.KismiPuan.ToString();
            ltrTamPuan.Text = rinf.Tampuan.ToString();
            ltrBransAdi.Text = brnsDb.KayitBilgiGetir(rinf.BransId).BransAdi;
            ltrDogruCevap.Text = rinf.DogruCevap;
            ltrYanlisCevap.Text = rinf.YanlisCevap;
            ltrKismiCevap.Text = rinf.KismiCevap;
            ltrSoru.Text = rinf.Soru;
            if(hfSoruNo.Value!=rinf.SoruNo.ToString())
               Master.UyariTuruncu("Bu yeni bir soruya ait cevap!",phUyariYeniSoru);
            hfSoruNo.Value = rinf.SoruNo.ToString();
        }

        protected void btnKaydet_OnClick(object sender, EventArgs e)
        {
            if (rbKismi.Checked || rbTampuan.Checked || rbYanlis.Checked || rbBos.Checked)
            {

                int uyeId = Master.UyeId();
                int id = hfId.Value.ToInt32();

                KullanicilarDb kDb = new KullanicilarDb();
                KullanicilarInfo ki = kDb.KayitBilgiGetir(uyeId);

                SonucAuDB veriDb = new SonucAuDB();
                SonucAuInfo info = veriDb.KayitBilgiGetir(id);

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

                int nihaiPuan = 0;
                int ustDegerlendirici = 0;
                if (ki.Grup == "A")
                {
                    if (info.DegerlendirdiB == 1)
                    {
                        if (info.DegerlendiriciBPuani == puani)
                            nihaiPuan = puani;
                        else
                            ustDegerlendirici = 1;
                    }
                    veriDb.DegerlendirildiA(id, puani, bos, nihaiPuan, ustDegerlendirici);
                }

                if (ki.Grup == "B")
                {
                    if (info.DegerlendirdiA == 1)
                    {
                        if (info.DegerlendiriciAPuani == puani)
                            nihaiPuan = puani;
                        else
                            ustDegerlendirici = 1;
                    }
                    veriDb.DegerlendirildiB(id, puani, bos, nihaiPuan, ustDegerlendirici);
                }
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