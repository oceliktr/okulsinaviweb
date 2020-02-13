using DAL;
using iTextSharp.text;
using iTextSharp.text.pdf;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class ODM_LGSKazanimKarne : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Master.Yetki().Contains("Root") && !Master.Yetki().Contains("Admin") && !Master.Yetki().Contains("IlceMEMYetkilisi") && !Master.Yetki().Contains("OkulYetkilisi") && !Master.Yetki().Contains("LgsIlKomisyonu"))
            Response.Redirect("Giris.aspx");

        if (!IsPostBack)
        {
            ilceTr.Visible = false;
            CkSinavAdiDB sinav = new CkSinavAdiDB();
            ddlSinavlar.DataSource = sinav.KayitlariGetir();
            ddlSinavlar.DataValueField = "SinavId";
            ddlSinavlar.DataTextField = "SinavAdi";
            ddlSinavlar.DataBind();
            ddlSinavlar.Items.Insert(0, new System.Web.UI.WebControls.ListItem("Sınav Seçiniz", ""));

            if (Master.Yetki().Contains("IlceMEMYetkilisi"))
            {

                ilceTr.Visible = true;
                KullanicilarDb kDb = new KullanicilarDb();
                KullanicilarInfo info = kDb.KayitBilgiGetir(Master.UyeId(), true);

                ddlIlce.SelectedValue = info.IlceAdi.BuyukHarfeCevir();
                ddlIlce.Enabled = false;

                CkSinavAdiInfo aktifSinav = sinav.AktifSinavAdi();
                ddlSinavlar.SelectedValue = aktifSinav.SinavId.ToString();

                lgIstatistik.Visible = false;
                btnIlKarnesi.Visible = false;
            }

            if (Master.Yetki().Contains("OkulYetkilisi"))
            {
                KullanicilarDb kDb = new KullanicilarDb();
                KullanicilarInfo info = kDb.KayitBilgiGetir(Master.UyeId(), true);

                ddlIlce.SelectedValue = info.IlceAdi.BuyukHarfeCevir();
                ddlIlce.Enabled = false;

                CkSinavAdiInfo aktifSinav = sinav.AktifSinavAdi();
                ddlSinavlar.SelectedValue = aktifSinav.SinavId.ToString();

                lgIstatistik.Visible = false;
                btnIlKarnesi.Visible = false;

            }
        }
    }

    protected void btnListele_OnClick(object sender, EventArgs e)
    {
        int sinavId = ddlSinavlar.SelectedValue.ToInt32();
        string ilceAdi = ddlIlce.SelectedValue;
        int sinif = ddlSinif.SelectedValue.ToInt32();

        if (Master.Yetki().Contains("OkulYetkilisi")) //okul dışndaki yetkililere göster Root,Admin,IlceMEMYetkilisi
        {

            CkSinavAdiDB sinav = new CkSinavAdiDB();
            CkSinavAdiInfo aktifSinav = sinav.AktifSinavAdi();
            KullanicilarDb kDb = new KullanicilarDb();
            KullanicilarInfo info = kDb.KayitBilgiGetir(Master.UyeId(), true);


            CkKarneKutukDB kutukDb = new CkKarneKutukDB();
            rptKurumlar.DataSource = kutukDb.KayitlariGetir(sinavId, info.KurumKodu.ToInt32(),sinif);
            rptKurumlar.DataBind();

        }
        else
        {


            CkKarneKutukDB kutukDb = new CkKarneKutukDB();
            rptKurumlar.DataSource = kutukDb.KayitlariGetir(sinavId, ilceAdi,sinif);
            rptKurumlar.DataBind();

          
            if (!string.IsNullOrEmpty(ilceAdi) && sinavId != 0 && rptKurumlar.Items.Count>0)
                ilceTr.Visible = true;
            else
            {
                ilceTr.Visible = false;
            }
        }
    }

    public CkKarneBranslarInfo DersAdi(int sinavId, int bransId)
    {
        CkKarneBranslarDB brnsDb = new CkKarneBranslarDB();
        CkKarneBranslarInfo brans = brnsDb.KayitlariDizeGetir(sinavId).Find(x => x.BransId == bransId);
        return brans;
    }
    protected void rptKurumlar_OnItemCommand(object source, RepeaterCommandEventArgs e)
    {

        string[] value = e.CommandArgument.ToString().Split(',');

        OkulicinBerlesikDersliKarneler(value, e.CommandName);
        // OkulicinTekSayfaliKarneler(value);
    }
    /// <summary>
    /// Bir sayfada briden fazla ders olan karneler
    /// </summary>
    /// <param name="value"></param>
    private void OkulicinBerlesikDersliKarneler(string[] value, string karne)
    {
        TekSayfaliKarne tekSayfaliKarne = new TekSayfaliKarne();

        int sinavId = value[0].ToInt32();
        int kurumKodu = value[1].ToInt32();
        string ilceAdi = value[2];
        string okulAdi = value[3];
        int sinif = ddlSinif.SelectedValue.ToInt32();

        string path = karne == "okul" ? string.Format("{0} - {1} Karnesi.pdf", ilceAdi, okulAdi) : string.Format("{0} - {1} Öğrenci Karnesi.pdf", ilceAdi, okulAdi);
        Document doc = new Document();
        //Dosya tipini PDF olarak belirtiyoruz.
        Response.ContentType = "application/pdf";
        // PDF Dosya ismini belirtiyoruz.
        Response.AddHeader("content-disposition", "attachment;filename=" + path);

        //Sayfamızın cache'lenmesini kapatıyoruz
        Response.Cache.SetCacheability(HttpCacheability.NoCache);

        //PdfWriter PDF dosyamız ile stream'i eşitleyen class'ımız.
        PdfWriter.GetInstance(doc, Response.OutputStream);

        //   PdfWriter.GetInstance(doc, new FileStream(path, FileMode.Create));
        doc.Open();

        CkSinavAdiDB sinavDb = new CkSinavAdiDB();
        CkSinavAdiInfo sinav = sinavDb.KayitBilgiGetir(sinavId);
     


        CkKarneKazanimlardB kazanimDb = new CkKarneKazanimlardB();
        List<CkKarneKazanimlarInfo> kazanimList = kazanimDb.KayitlariDizeGetir(sinavId);

        CkKarneSonuclariDB ksDb = new CkKarneSonuclariDB();
        List<CkKarneSonuclariInfo> karneSonucList = ksDb.KayitlariDizeGetir(sinavId, ilceAdi, kurumKodu);

        CkKarneBranslarDB bransDb = new CkKarneBranslarDB();
        List<CkKarneBranslarInfo> branslar = bransDb.KayitlariDizeGetir(sinavId).OrderBy(x => x.Sira).ToList();

        CkIlIlceOrtalamasiDB basariDb = new CkIlIlceOrtalamasiDB();
        List<CkIlIlceOrtalamasiInfo> basariYuzdesi = basariDb.KayitlariDizeGetir(sinavId);
        if (karne == "okul") //okul ve şube karneleri
        {
            //   var siniflar = karneSonucList.Where(x => x.KurumKodu == kurumKodu && x.SinavId == sinavId).GroupBy(x => x.Sinif).Select(x => x.First()).OrderBy(x => x.Sinif).ToList();
            //foreach (CkKarneSonuclariInfo sinif in siniflar)
            //{
            foreach (var brans in branslar)
            {
                // string bransAdi = DersAdi(sinavId, brans.BransId).BransAdi;
                string sonSatirBaslik = string.Format("{0} {1}. SINIF OKUL KARNESİ", okulAdi.BuyukHarfeCevir(), sinif);
                KarneModel karneModel =
                    tekSayfaliKarne.KarneModeliOlusturOkul(ilceAdi, okulAdi, sinif, brans.BransId, sinav, brans.BransAdi, kazanimList, karneSonucList, basariYuzdesi);
                //OKUL KARNESİ
                tekSayfaliKarne.PdfOlustur(doc, sinav, sinif, brans.BransAdi, TekSayfaliKarne.KolonSayisi.Okul.ToInt32(), sonSatirBaslik, karneModel);

                doc.NewPage();


                List<CkKarneSonuclariInfo> subeler = karneSonucList.Where(x => x.Sinif == sinif).GroupBy(x => x.Sube).Select(x => x.First()).OrderBy(x => x.Sube).ToList();

                if (subeler.Count > 1)
                {
                    foreach (var sube in subeler)
                    {
                        //ŞUBE KARNESİ

                        string sonSatirBaslikSube = string.Format("{0} {1}-{2} SINIFI ŞUBE KARNESİ", okulAdi.BuyukHarfeCevir(),
                            sinif, sube.Sube);
                        var karneModelSube = tekSayfaliKarne.KarneModeliOlusturSube(ilceAdi, kurumKodu, okulAdi, sinif, sube.Sube, brans.BransId, sinav, brans.BransAdi, okulAdi, kazanimList, karneSonucList, basariYuzdesi);

                        tekSayfaliKarne.PdfOlustur(doc, sinav, sinif, brans.BransAdi, TekSayfaliKarne.KolonSayisi.Sube.ToInt32(), sonSatirBaslikSube, karneModelSube);

                        doc.NewPage();
                    }
                }
            }
            //}
        }

        if (karne == "ogrenci") //öğrenci karneleri
        {

            List<CkKarneSonuclariInfo> subelerOgr = karneSonucList.Where(x => x.KurumKodu == kurumKodu && x.Sinif == sinif)
                .GroupBy(x => x.Sube).Select(x => x.First()).OrderBy(x => x.Sube).ToList();

            foreach (var sube in subelerOgr)
            {
                //ÖĞRENCİ KARNESİ
                CkKarneKutukDB kutukDb = new CkKarneKutukDB();
                List<CkKarneKutukInfo> ogrenciListesi = kutukDb.KayitlariDizeGetir(sinavId, kurumKodu).Where(x => x.Sinifi == sinif && x.Sube == sube.Sube).ToList();
                
                CkKarneDogruCevaplarDB dogruCevapDb = new CkKarneDogruCevaplarDB();
                List<CkKarneDogruCevaplarInfo> dogruCevapList = dogruCevapDb.KayitlariDizeGetir(sinavId);


                PdfIslemleri pdf = new PdfIslemleri();

      

                foreach (var kutuk in ogrenciListesi)
                {

                    if (kutuk.KitapcikTuru != "")
                    {
                        if (kutuk.KatilimDurumu != "0")
                        {
                            #region Başlık

                            pdf.addParagraph(doc, "ERZURUM  İL MİLLİ EĞİTİM MÜDÜRLÜĞÜ", hizalama: Element.ALIGN_CENTER);
                            pdf.addParagraph(doc, "Ölçme Değerlendirme Merkezi", hizalama: Element.ALIGN_CENTER);
                            pdf.addParagraph(doc, string.Format("{0} ÖĞRENCİ KARNESİ", sinav.SinavAdi.BuyukHarfeCevir()), hizalama: Element.ALIGN_CENTER);

                            #endregion

                            #region Öğrenci Bilgileri Tablosu

                            PdfPTable tableOgrenciBilgileri = new PdfPTable(2)
                            {
                                TotalWidth = 522f, // puan cinsinden tablonun gerçek genişliği
                                LockedWidth = true, // tablonun mutlak genişliğini sabitle
                                SpacingBefore = 20f, //öncesinde boşluk miktarı
                                SpacingAfter = 0f, //sonrasında boşluk miktarı
                                HorizontalAlignment = Element.ALIGN_LEFT
                            };

                            //   float[] widthsOB = { 75f, 352f, 20f, 20f, 20f, 20f, 20f };
                            float[] widthsOB = { 75f, 452f };
                            tableOgrenciBilgileri.SetWidths(widthsOB);


                            pdf.addCell(tableOgrenciBilgileri, "ÖĞRENCİ BİLGİLERİ", colspan: 2, fontSize: 6,
                                bgColor: PdfIslemleri.Renkler.Gri, hizalama: Element.ALIGN_CENTER,
                                fontStyle: Font.BOLD);

                            pdf.addCell(tableOgrenciBilgileri, "ADI SOYADI :", hizalama: Element.ALIGN_RIGHT);
                            pdf.addCell(tableOgrenciBilgileri, string.Format("{0} {1} ({2} Kitapçığı)", kutuk.Adi, kutuk.Soyadi, kutuk.KitapcikTuru));

                            pdf.addCell(tableOgrenciBilgileri, "İLÇESİ / OKULU :", hizalama: Element.ALIGN_RIGHT);
                            pdf.addCell(tableOgrenciBilgileri,
                                string.Format("{0} / {1}", kutuk.IlceAdi, kutuk.KurumAdi));

                            pdf.addCell(tableOgrenciBilgileri, "SINIFI / ŞUBE :", hizalama: Element.ALIGN_RIGHT);
                            pdf.addCell(tableOgrenciBilgileri, string.Format("{0} / {1}", kutuk.Sinifi, kutuk.Sube));
                            //  pdf.addCell(tableOgrenciBilgileri, "");

                            doc.Add(tableOgrenciBilgileri);

                            //pdf.addParagraph(doc, "SS: Soru Sayısı    D: Doğru Cevap Sayısı   Y: Yanlış Cevap Sayısı   B: Boş Sayısı   KT: Kitapçık Türü", fontSize: 6, fontStil: Font.ITALIC, hizalama: Element.ALIGN_RIGHT);

                            #endregion

                            foreach (var brans in branslar)
                            {
                                CkKarneDogruCevaplarInfo dogruCvplar = dogruCevapList.FirstOrDefault(x => x.Sinif == sinif && x.BransId == brans.BransId && x.KitapcikTuru == kutuk.KitapcikTuru);

                                if (dogruCvplar != null)  //Sınavda sorulmayan dersleri ayırmak için suni kontrol
                                {
                                    List<CkKarneKazanimlarInfo> kazanimlar = kazanimList.Where(x => x.BransId == brans.BransId && x.Sinif == sinif).ToList();


                                    string ogrenciCevabi = "";
                                    string[] cevapBilgisi = kutuk.Cevaplar.Split('#');
                                    for (int t = 0; t < cevapBilgisi.Length - 1; t += 2)
                                    {
                                        int dersKodu = cevapBilgisi[t].ToInt32();
                                        if (dersKodu == brans.BransId)
                                            ogrenciCevabi = cevapBilgisi[t + 1];
                                    }

                                    int soruSayisi = dogruCvplar.Cevaplar.Length;

                                    #region Kazamılar

                                    PdfPTable tableKazanimlar = new PdfPTable(4)
                                    {
                                        TotalWidth = 522f, // puan cinsinden tablonun gerçek genişliği
                                        LockedWidth = true, // tablonun mutlak genişliğini sabitle
                                        SpacingBefore = 10f, //öncesinde boşluk miktarı
                                        SpacingAfter = 0f, //sonrasında boşluk miktarı
                                        HorizontalAlignment = Element.ALIGN_LEFT
                                    };

                                    float[] widthsKazanim = { 40f, 40f, 40f, 407f };

                                    tableKazanimlar.SetWidths(widthsKazanim);

                                   

                                    //Kazanım Karnesi 
                                    pdf.addCell(tableKazanimlar, "S. No", bgColor: PdfIslemleri.Renkler.Gri,
                                        hizalama: Element.ALIGN_CENTER, fontStyle: Font.BOLD);
                                    pdf.addCell(tableKazanimlar, "C. Anahtarı", bgColor: PdfIslemleri.Renkler.Gri,
                                        hizalama: Element.ALIGN_CENTER, fontStyle: Font.BOLD);
                                    pdf.addCell(tableKazanimlar, "Ö. Cevabı", bgColor: PdfIslemleri.Renkler.Gri,
                                        hizalama: Element.ALIGN_CENTER, fontStyle: Font.BOLD);
                                    if (sinav.DegerlendirmeTuru == DegerlendirmeTurleri.DegerlendirmeTuru.KazanimBazli.ToInt32())
                                    {
                                        pdf.addCell(tableKazanimlar, DersAdi(sinavId, brans.BransId).BransAdi + " Dersi Kazanımları", bgColor: PdfIslemleri.Renkler.Gri, hizalama: Element.ALIGN_CENTER,
                                            fontStyle: Font.BOLD);
                                    }
                                    else
                                    {
                                        pdf.addCell(tableKazanimlar, DersAdi(sinavId, brans.BransId).BransAdi + " Dersi Konuları", bgColor: PdfIslemleri.Renkler.Gri, hizalama: Element.ALIGN_CENTER,
                                            fontStyle: Font.BOLD);
                                    }
                                   

                                    for (int i = 1; i <= soruSayisi; i++)
                                    {
                                        string kazanimAdi = "";


                                        string soruSql = kutuk.KitapcikTuru + i + ",";
                                        CkKarneKazanimlarInfo kazanim = kazanimlar.Find(x => x.Sorulari.Contains(soruSql));

                                        if (kazanim != null)
                                        {
                                            kazanimAdi = kazanim.KazanimAdiOgrenci==""? kazanim.KazanimAdi: kazanim.KazanimAdiOgrenci;
                                        }


                                        pdf.addCell(tableKazanimlar, i.ToString(), fontSize: 6,
                                            hizalama: Element.ALIGN_CENTER);
                                        pdf.addCell(tableKazanimlar, dogruCvplar.Cevaplar.Substring(i - 1, 1), fontSize: 6,
                                            hizalama: Element.ALIGN_CENTER);
                                        pdf.addCell(tableKazanimlar, ogrenciCevabi.Substring(i - 1, 1), fontSize: 6,
                                            hizalama: Element.ALIGN_CENTER);
                                        pdf.addCell(tableKazanimlar, kazanimAdi, fontSize: 6);
                                    }

                                    doc.Add(tableKazanimlar);

                                    #endregion


                                }

                            }

                            //-------------SON SATIR -----------------
                            pdf.addParagraph(doc, "\n\n");
                            pdf.addParagraph(doc,
                                "Erzurum Ölçme ve Değerlendirme Merkezi - Adres: Lalapaşa Mahallesi, Yukarı Mumcu Caddesi, Atatürk Evi Sokak, No1, Kat 5/6, Yakutiye / ERZURUM\n",
                                7, Element.ALIGN_CENTER, Font.ITALIC);
                            pdf.addParagraph(doc, "Web: http://erzurumodm.meb.gov.tr E-Mail: odm25@meb.gov.tr", 7,
                                Element.ALIGN_CENTER, Font.ITALIC);
                            doc.NewPage();
                        }
                    }
                }
            }
        }
       
        doc.Close();

        //Dosyanın içeriğini Response içerisine aktarıyoruz.
        Response.Write(doc);


        try
        {
            KullanicilarDb userDb = new KullanicilarDb();
            var user = userDb.KayitBilgiGetir(Master.UyeId());

            CkKarneLogDB logDb = new CkKarneLogDB();
            var kontrol = logDb.KayitBilgiGetir(Master.UyeId(), kurumKodu, sinif, 0);

            CkKarneLogInfo infoLog = new CkKarneLogInfo();
            infoLog.KullaniciId = Master.UyeId();
            infoLog.KurumKodu = kurumKodu;
            infoLog.Tarih = DateTime.Now;
            infoLog.Sinif = sinif;
            infoLog.Brans = 0;
            infoLog.Aciklama = karne + " Karnesi" + user.AdiSoyadi + " - " + user.TcKimlik + " - " + user.Yetki;

            if (kontrol.Id == 0)
            {
                infoLog.Say = 1;

                logDb.KayitEkle(infoLog);
            }
            else
            {
                infoLog.Say = kontrol.Say + 1;
                infoLog.Id = kontrol.Id;
                logDb.KayitGuncelle(infoLog);
            }
        }
        catch (Exception)
        {
            //
        }

        //Son aşama da işlemleri bitirip, ekran çıktısına ulaşıyoruz.
        Response.End();
    }

    /// <summary>
    /// Her ders için bir sayfa ayarlanmış karneler.
    /// </summary>
    /// <param name="value"></param>
    private void OkulicinTekSayfaliKarneler(string[] value)
    {
        TekSayfaliKarne tekSayfaliKarne = new TekSayfaliKarne();

        int sinavId = value[0].ToInt32();
        int kurumKodu = value[1].ToInt32();
        string ilceAdi = value[2];
        string okulAdi = value[3];
        int sinif = ddlSinif.SelectedValue.ToInt32();
        int brans = 0;// ddlBrans.SelectedValue.ToInt32();
        string bransAdi = DersAdi(sinavId, brans).BransAdi;

        string path = string.Format("{0} - {1} {2} Dersi Kazanım Karnesi.pdf", ilceAdi, okulAdi, bransAdi);
        Document doc = new Document();
        //Dosya tipini PDF olarak belirtiyoruz.
        Response.ContentType = "application/pdf";
        // PDF Dosya ismini belirtiyoruz.
        Response.AddHeader("content-disposition", "attachment;filename=" + path);

        //Sayfamızın cache'lenmesini kapatıyoruz
        Response.Cache.SetCacheability(HttpCacheability.NoCache);

        //PdfWriter PDF dosyamız ile stream'i eşitleyen class'ımız.
        PdfWriter.GetInstance(doc, Response.OutputStream);

        //   PdfWriter.GetInstance(doc, new FileStream(path, FileMode.Create));
        doc.Open();

        CkSinavAdiDB sinavDb = new CkSinavAdiDB();
        CkSinavAdiInfo sinav = sinavDb.KayitBilgiGetir(sinavId);
        
        CkKarneDogruCevaplarDB dogruCevapDb = new CkKarneDogruCevaplarDB();
        List<CkKarneDogruCevaplarInfo> dogruCevapList = dogruCevapDb.KayitlariDizeGetir(sinavId);

        CkKarneSonuclariDB ksDb = new CkKarneSonuclariDB();
        CkKarneKutukDB kutukDb = new CkKarneKutukDB();
        CkKarneKazanimlardB kazanimDb = new CkKarneKazanimlardB();
        List<CkKarneKazanimlarInfo> kazanimList = kazanimDb.KayitlariDizeGetir(sinavId);

        List<CkKarneSonuclariInfo> karneSonucList = ksDb.KayitlariDizeGetir(sinavId, ilceAdi, kurumKodu);


        CkIlIlceOrtalamasiDB basariDb = new CkIlIlceOrtalamasiDB();
        List<CkIlIlceOrtalamasiInfo> basariYuzdesi = basariDb.KayitlariDizeGetir(sinavId);


        string sonSatirBaslik = string.Format("{0} {1}. SINIF OKUL KARNESİ", okulAdi.BuyukHarfeCevir(), sinif);
        KarneModel karneModel =
            tekSayfaliKarne.KarneModeliOlusturOkul(ilceAdi, okulAdi, sinif, brans, sinav, bransAdi, kazanimList, karneSonucList, basariYuzdesi);
        //OKUL KARNESİ
        tekSayfaliKarne.PdfOlustur(doc, sinav, sinif, bransAdi, TekSayfaliKarne.KolonSayisi.Okul.ToInt32(),
            sonSatirBaslik, karneModel);

        doc.NewPage();


        List<CkKarneSonuclariInfo> subeler = karneSonucList.Where(x => x.KurumKodu == kurumKodu && x.Sinif == sinif)
            .GroupBy(x => x.Sube).Select(x => x.First()).OrderBy(x => x.Sube).ToList();

        if (subeler.Count > 1)
        {
            foreach (var sube in subeler)
            {
                //ŞUBE KARNESİ

                string sonSatirBaslikSube = string.Format("{0} {1}-{2} SINIFI ŞUBE KARNESİ", okulAdi.BuyukHarfeCevir(),
                    sinif, sube.Sube);
                var karneModelSube = tekSayfaliKarne.KarneModeliOlusturSube(ilceAdi, kurumKodu, okulAdi, sinif, sube.Sube,
                    brans, sinav, bransAdi, okulAdi, kazanimList, karneSonucList, basariYuzdesi);

                tekSayfaliKarne.PdfOlustur(doc, sinav, sinif, bransAdi, TekSayfaliKarne.KolonSayisi.Sube.ToInt32(),
                    sonSatirBaslikSube, karneModelSube);
                doc.NewPage();
            }
        }

        List<CkKarneSonuclariInfo> subelerOgr = karneSonucList.Where(x => x.KurumKodu == kurumKodu && x.Sinif == sinif)
            .GroupBy(x => x.Sube).Select(x => x.First()).OrderBy(x => x.Sube).ToList();

        foreach (var sube in subelerOgr)
        {
            //ÖĞRENCİ KARNESİ

            List<CkKarneKazanimlarInfo>
                kazanimlar = kazanimList.Where(x => x.BransId == brans && x.Sinif == sinif).ToList();
            List<CkKarneKutukInfo> ogrenciListesi = kutukDb.KayitlariDizeGetir(sinavId, kurumKodu)
                .Where(x => x.Sinifi == sinif && x.Sube == sube.Sube).ToList();

            foreach (var kutuk in ogrenciListesi)
            {
                if (kutuk.KitapcikTuru != "")
                {
                    string ogrenciCevabi = "";
                    string[] cevapBilgisi = kutuk.Cevaplar.Split('#');
                    for (int t = 0; t < cevapBilgisi.Length - 1; t += 2)
                    {
                        int dersKodu = cevapBilgisi[t].ToInt32();
                        if (dersKodu == brans)
                            ogrenciCevabi = cevapBilgisi[t + 1];
                    }

                    CkKarneDogruCevaplarInfo dogruCvplar = dogruCevapList.FirstOrDefault(x =>
                        x.Sinif == sinif && x.BransId == brans && x.KitapcikTuru == kutuk.KitapcikTuru);
                    tekSayfaliKarne.PdfOlustur(doc, sinav.SinavAdi, bransAdi, kutuk, ogrenciCevabi, dogruCvplar.Cevaplar,
                        kazanimlar, kutuk.KatilimDurumu, kutuk.KitapcikTuru);
                    doc.NewPage();
                }
            }
        }

        //    }
        //}

        doc.Close();

        //Dosyanın içeriğini Response içerisine aktarıyoruz.
        Response.Write(doc);


        try
        {
            KullanicilarDb userDb = new KullanicilarDb();
            var user = userDb.KayitBilgiGetir(Master.UyeId());

            CkKarneLogDB logDb = new CkKarneLogDB();
            var kontrol = logDb.KayitBilgiGetir(Master.UyeId(), kurumKodu, sinif, brans);

            CkKarneLogInfo infoLog = new CkKarneLogInfo();
            infoLog.KullaniciId = Master.UyeId();
            infoLog.KurumKodu = kurumKodu;
            infoLog.Tarih = DateTime.Now;
            infoLog.Sinif = sinif;
            infoLog.Brans = brans;
            infoLog.Aciklama = user.AdiSoyadi + " - " + user.TcKimlik + " - " + user.Yetki;

            if (kontrol.Id == 0)
            {
                infoLog.Say = 1;

                logDb.KayitEkle(infoLog);
            }
            else
            {
                infoLog.Say = kontrol.Say + 1;
                infoLog.Id = kontrol.Id;
                logDb.KayitGuncelle(infoLog);
            }
        }
        catch (Exception)
        {
            //
        }

        //Son aşama da işlemleri bitirip, ekran çıktısına ulaşıyoruz.
        Response.End();
    }


    protected void lnkIlceKarnesi_OnClick(object sender, EventArgs e)
    {
        TekSayfaliKarne tekSayfaliKarne = new TekSayfaliKarne();
        int sinavId = ddlSinavlar.SelectedValue.ToInt32();
        string ilceAdi = ddlIlce.SelectedValue;
        ltrIlceAdi.Text = ilceAdi;


        //  CkKarneSonuclariDB ksDb = new CkKarneSonuclariDB();
        CkKarneKazanimlardB kazanimDb = new CkKarneKazanimlardB();

        List<CkKarneKazanimlarInfo> kazanimList = kazanimDb.KayitlariDizeGetir(sinavId);
        //   List<CkKarneSonuclariInfo> karneSonucList = ksDb.KayitlariDizeGetir(sinavId);

        CkIlIlceOrtalamasiDB basariDb = new CkIlIlceOrtalamasiDB();
        List<CkIlIlceOrtalamasiInfo> basariYuzdesi = basariDb.KayitlariDizeGetir(sinavId);

        if (basariYuzdesi.Count > 0)
        {

            string path = string.Format("{0} - İlçe Karnesi.pdf", ilceAdi.IlkHarfleriBuyut());
            Document doc = new Document();
            //Dosya tipini PDF olarak belirtiyoruz.
            Response.ContentType = "application/pdf";
            // PDF Dosya ismini belirtiyoruz.
            Response.AddHeader("content-disposition", "attachment;filename=" + path);

            //Sayfamızın cache'lenmesini kapatıyoruz
            Response.Cache.SetCacheability(HttpCacheability.NoCache);

            //PdfWriter PDF dosyamız ile stream'i eşitleyen class'ımız.
            PdfWriter.GetInstance(doc, Response.OutputStream);

            //   PdfWriter.GetInstance(doc, new FileStream(path, FileMode.Create));
            doc.Open();


            CkSinavAdiDB sinavDb = new CkSinavAdiDB();
            CkSinavAdiInfo sinav = sinavDb.KayitBilgiGetir(sinavId);

            List<CkIlIlceOrtalamasiInfo> branslar = basariYuzdesi.GroupBy(x => x.BransId).Select(x => x.First()).OrderBy(x => x.BransId).ToList();

            var siniflar = basariYuzdesi.Where(x => x.SinavId == sinavId).GroupBy(x => x.Sinif).Select(x => x.First()).OrderBy(x => x.Sinif).ToList();
            foreach (CkIlIlceOrtalamasiInfo sinif in siniflar)
            {
                foreach (var brans in branslar)
                {
                    string bransAdi = DersAdi(sinavId, brans.BransId).BransAdi;

                    string sonSatirBaslik = string.Format("{0} İLÇE KARNESİ", ilceAdi.BuyukHarfeCevir());
                    KarneModel karneModel = tekSayfaliKarne.KarneModeliOlusturIlce(sinav, ilceAdi, sinif.Sinif, bransAdi, brans.BransId, kazanimList, basariYuzdesi);

                    tekSayfaliKarne.PdfOlustur(doc, sinav, sinif.Sinif, bransAdi, TekSayfaliKarne.KolonSayisi.Ilce.ToInt32(), sonSatirBaslik, karneModel);

                    doc.NewPage();
                }
            }
            doc.Close();

            //Dosyanın içeriğini Response içerisine aktarıyoruz.
            Response.Write(doc);


            try
            {
                KullanicilarDb userDb = new KullanicilarDb();
                var user = userDb.KayitBilgiGetir(Master.UyeId());

                CkKarneLogDB logDb = new CkKarneLogDB();
                var kontrol = logDb.KayitBilgiGetir(Master.UyeId(), user.KurumKodu.ToInt32());

                CkKarneLogInfo infoLog = new CkKarneLogInfo();
                infoLog.KullaniciId = Master.UyeId();
                infoLog.KurumKodu = user.KurumKodu.ToInt32();
                infoLog.Tarih = DateTime.Now;
                infoLog.Sinif = 0;
                infoLog.Brans = 0;
                infoLog.Aciklama = "İlçe Karnesi " + user.AdiSoyadi + " - " + user.TcKimlik + " - " + user.Yetki;
                if (kontrol.Id == 0)
                {
                    infoLog.Say = 1;

                    logDb.KayitEkle(infoLog);
                }
                else
                {
                    infoLog.Say = kontrol.Say + 1;
                    infoLog.Id = kontrol.Id;
                    logDb.KayitGuncelle(infoLog);

                }
            }
            catch (Exception)
            {
                //
            }
            //Son aşama da işlemleri bitirip, ekran çıktısına ulaşıyoruz.
            Response.End();

        }
        else
        {
            Master.UyariTuruncu("İlçeye ait karne bulunamadı", phUyari);
        }
    }

    protected void btnIlKarnesi_OnClick(object sender, EventArgs e)
    {
        TekSayfaliKarne tekSayfaliKarne = new TekSayfaliKarne();
        int sinavId = ddlSinavlar.SelectedValue.ToInt32();

        CkSinavAdiDB sinavDb = new CkSinavAdiDB();
        CkSinavAdiInfo sinav = sinavDb.KayitBilgiGetir(sinavId); 


        CkKarneKazanimlardB kazanimDb = new CkKarneKazanimlardB();

        List<CkKarneKazanimlarInfo> kazanimList = kazanimDb.KayitlariDizeGetir(sinavId);
        
        CkIlIlceOrtalamasiDB basariDb = new CkIlIlceOrtalamasiDB();
        List<CkIlIlceOrtalamasiInfo> basariYuzdesi = basariDb.KayitlariDizeGetir(sinavId);

        if (basariYuzdesi.Count > 0)
        {

            string path = string.Format("{0} İl Kazanım Karnesi.pdf", sinav.SinavAdi.IlkHarfleriBuyut());
            Document doc = new Document();
            //Dosya tipini PDF olarak belirtiyoruz.
            Response.ContentType = "application/pdf";
            // PDF Dosya ismini belirtiyoruz.
            Response.AddHeader("content-disposition", "attachment;filename=" + path);

            //Sayfamızın cache'lenmesini kapatıyoruz
            Response.Cache.SetCacheability(HttpCacheability.NoCache);

            //PdfWriter PDF dosyamız ile stream'i eşitleyen class'ımız.
            PdfWriter.GetInstance(doc, Response.OutputStream);

            //   PdfWriter.GetInstance(doc, new FileStream(path, FileMode.Create));
            doc.Open();


            List<CkIlIlceOrtalamasiInfo> branslar = basariYuzdesi.GroupBy(x => x.BransId).Select(x => x.First()).OrderBy(x => x.BransId).ToList();

            var siniflar = basariYuzdesi.Where(x => x.SinavId == sinavId).GroupBy(x => x.Sinif).Select(x => x.First()).OrderBy(x => x.Sinif).ToList();
            foreach (var sinif in siniflar)
            {
                foreach (var brans in branslar)
                {
                    string bransAdi = DersAdi(sinavId, brans.BransId).BransAdi;

                    string sonSatirBaslik = "İL KARNESİ";
                    KarneModel karneModel = tekSayfaliKarne.IlKarneModeliOlustur3(sinav, sinif.Sinif, bransAdi, brans.BransId, kazanimList, basariYuzdesi);
                    //Document doc, string sinavAdi, CkKarneSonuclariInfo sinif, string dersAdi, int column, string sonSatirBaslik, KarneModel karneModel
                    tekSayfaliKarne.PdfOlustur(doc, sinav, sinif.Sinif, bransAdi, TekSayfaliKarne.KolonSayisi.Il.ToInt32(), sonSatirBaslik, karneModel);

                    doc.NewPage();
                }
            }
            doc.Close();

            //Dosyanın içeriğini Response içerisine aktarıyoruz.
            Response.Write(doc);


            try
            {
                KullanicilarDb userDb = new KullanicilarDb();
                var user = userDb.KayitBilgiGetir(Master.UyeId());

                CkKarneLogDB logDb = new CkKarneLogDB();
                var kontrol = logDb.KayitBilgiGetir(Master.UyeId(), user.KurumKodu.ToInt32());

                CkKarneLogInfo infoLog = new CkKarneLogInfo();
                infoLog.KullaniciId = Master.UyeId();
                infoLog.KurumKodu = user.KurumKodu.ToInt32();
                infoLog.Tarih = DateTime.Now;
                infoLog.Sinif = 0;
                infoLog.Brans = 0;
                infoLog.Aciklama = "İl Karnesi" + user.AdiSoyadi + " - " + user.TcKimlik + " - " + user.Yetki;
                if (kontrol.Id == 0)
                {
                    infoLog.Say = 1;

                    logDb.KayitEkle(infoLog);
                }
                else
                {
                    infoLog.Say = kontrol.Say + 1;
                    infoLog.Id = kontrol.Id;
                    logDb.KayitGuncelle(infoLog);

                }
            }
            catch (Exception)
            {
                //
            }
            //Son aşama da işlemleri bitirip, ekran çıktısına ulaşıyoruz.
            Response.End();

        }
        else
        {
            Master.UyariTuruncu("İle ait karne bulunamadı. Yada sınav seçmediniz.", phUyari);
        }
    }

    private int a;
    protected void rptKurumlar_OnItemDataBound(object sender, RepeaterItemEventArgs e)
    {
        a++;
        if (e.Item.ItemType == ListItemType.Footer)
        {
            Literal ltr = (Literal) e.Item.FindControl("ltrBilgi");
            ltr.Text = a==1 ? "<p class=\"text-danger\">Kayıt bulunamadı</p>" : "<p class=\"text-info\"><strong>"+(a-1)+" kurum</strong> listelendi</p>";
        }
    }
}