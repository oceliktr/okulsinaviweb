using System;

public partial class OkulSinavi_CevrimiciSinavYonetim_OturumKayitRoot : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
           
            if (Request.QueryString["SinavId"] != null)
            {
                if (Request.QueryString["SinavId"].IsInteger())
                {
                    int sinavId = Request.QueryString["SinavId"].ToInt32();

                   
                    TestSinavlarDb sinavDb = new TestSinavlarDb();
                    TestSinavlarInfo info = sinavDb.KayitBilgiGetir(sinavId);
                    txtSinavAdi.Text = info.SinavAdi;
                    hfSinavId.Value = info.Id.ToString();

                    if (Request.QueryString["id"] != null)
                    {
                        if (Request.QueryString["id"].IsInteger())
                        {
                            int id = Request.QueryString["id"].ToInt32();

                            TestOturumlarDb oturumDb = new TestOturumlarDb();
                            TestOturumlarInfo oturumInfo = oturumDb.KayitBilgiGetir(id);
                            hfId.Value = oturumInfo.Id.ToString();
                            txtOturmAdi.Text = oturumInfo.OturumAdi;
                            txtSiraNo.Text = oturumInfo.SiraNo.ToString();
                            txtAciklama.Text = oturumInfo.Aciklama.Replace("<br>",Environment.NewLine);
                            txtSure.Text = oturumInfo.Sure.ToString();
                            txtTarih.Text = oturumInfo.BaslamaTarihi+ " - " + oturumInfo.BitisTarihi;

                            btnKaydet.Text = "Değiştir";
                        }
                    }
                }
            }
        }
    }

  
    protected void btnKaydet_OnClick(object sender, EventArgs e)
    {
        int sinavId = hfSinavId.Value.ToInt32();
        int siraNo = txtSiraNo.Text.ToInt32();
        int id = hfId.Value.ToInt32();
        string[] tarih = txtTarih.Text.Split('-');
        DateTime baslamaTarihi = tarih[0].Trim().ToDateTime();
        DateTime bitisTarihi = tarih[1].Trim().ToDateTime();
        TestOturumlarInfo info = new TestOturumlarInfo
        {
            OturumAdi = txtOturmAdi.Text.ToTemizMetin(),
            Aciklama = txtAciklama.Text.ToTemizMetin().Replace(Environment.NewLine, "<br>"),
            Sure = txtSure.Text.ToInt32(),
            SiraNo = siraNo,
            SinavId = sinavId,
            BaslamaTarihi = baslamaTarihi,
            BitisTarihi = bitisTarihi,
            Id = id
        };

        TestOturumlarDb veriDb = new TestOturumlarDb();
        var res = veriDb.KayitKontrol(sinavId, siraNo, id);
        if (res)
        {
            Master.UyariTuruncu("Oturum numarası daha önce kaydedilmiş.", phUyari);
        }
        else
        {
            if (hfId.Value == "0")
            {
                if (veriDb.KayitEkle(info) > 0)
                {
                    Master.UyariIslemTamam("Yeni bir oturum eklendi.", phUyari);
                    FormuTemizle();
                }
                else
                    Master.UyariTuruncu("Kayıt yapılamadı.", phUyari);
            }
            else
            {
                if (veriDb.KayitGuncelle(info) > 0)
                {
                    Master.UyariIslemTamam("Değişiklikler kaydildi.", phUyari);

                }
                else
                    Master.UyariTuruncu("Güncelleme yapılamadı.", phUyari);
            }

        }
    }

    private void FormuTemizle()
    {
        txtAciklama.Text = "";
        txtOturmAdi.Text = "";
        txtSure.Text = "";
    }
}