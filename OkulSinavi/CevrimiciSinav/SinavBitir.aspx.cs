using System;
using System.Collections.Generic;
using iTextSharp.text;

public partial class Sinav_SinavBitir : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["Ogrenci"] == null)
        {
            Server.Transfer("Default.aspx");
        }

        if (!IsPostBack)
        {
            int testId = 0;
            if (Request.QueryString["t"] != "")
                if (Request.QueryString["t"].IsInteger())
                    testId = Request.QueryString["t"].ToInt32();

            if (testId != 0)
            {
                TestOgrenci ogrenci = (TestOgrenci)Session["Ogrenci"];
                //mükerrer oturumu kontrol için
                if (CacheHelper.KullaniciGirisKontrol(ogrenci.OpaqId) != ogrenci.GirisKey)
                {
                    Response.Redirect("Default.aspx");
                }
                List<SoruSayisi> lstBosSorular = new List<SoruSayisi>();

                TestOgrCevapDb testCevapDb = new TestOgrCevapDb();
                var sonuc = testCevapDb.KayitBilgiGetir(testId, ogrenci.OpaqId);
                if (sonuc.Id != 0)
                {
                    string cevaplar = sonuc.Cevap;
                    for (int i = 0; i < cevaplar.Length; i++)
                    {
                        if (cevaplar.Substring(i, 1) == " ")
                        {
                            lstBosSorular.Add(new SoruSayisi(i+1,i+1,true,true));
                        }
                    }

                    if (lstBosSorular.Count>0)
                    {
                        rptBosSorular.DataSource = lstBosSorular;
                        rptBosSorular.DataBind();
                        phBosSorular.Visible = true;
                    }

                    string mesaj = lstBosSorular.Count == 0
                        ? "Tebrikler tüm soruları cevapladınız. Sınavı bitirebilirsiniz."
                        : lstBosSorular.Count +
                          " soruyu boş bıraktınız. Sorulara dönmek isterseniz soru numarasına tıklayınız.";
                    ltrMesaj.Text = mesaj;

                    //##### LOG İŞLEMLERİ
                    TestLogInfo logInfo = new TestLogInfo
                    {
                        OpaqId = ogrenci.OpaqId,
                        Grup = "Sınav Bitir - Oturum Id:" + testId,
                        Rapor = "Sınav Bitir butonuna tıklandı SinavBitir.aspx sayfası Page_Load çalıştı. Mesaj:" + mesaj
                    };
                    TestLogDb logDb = new TestLogDb();
                    logDb.KayitEkle(logInfo);
                    //##### LOG İŞLEMLERİ
                }
                else
                {
                    Response.Redirect("Sinavlar.aspx");
                }
            }
        }
    }
}