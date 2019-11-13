using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DAL;
using iTextSharp.text;
using iTextSharp.text.pdf;
using ListItem = System.Web.UI.WebControls.ListItem;

public partial class ODM_LGSKazanimKarne : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Master.Yetki().Contains("Root") && !Master.Yetki().Contains("Admin") && !Master.Yetki().Contains("IlceMEMYetkilisi") && !Master.Yetki().Contains("OkulYetkilisi")&&!Master.Yetki().Contains("LgsIlKomisyonu"))
            Response.Redirect("Giris.aspx");


        if (!IsPostBack)
        {
            ilceTr.Visible = false;
            CkSinavAdiDB sinav = new CkSinavAdiDB();
            ddlSinavlar.DataSource = sinav.KayitlariGetir();
            ddlSinavlar.DataValueField = "SinavId";
            ddlSinavlar.DataTextField = "SinavAdi";
            ddlSinavlar.DataBind();
            ddlSinavlar.Items.Insert(0, new ListItem("Sınav Seçiniz", ""));

            CkKarneBranslarDB brans = new CkKarneBranslarDB();
            ddlBrans.DataSource = brans.KayitlariGetir();
            ddlBrans.DataValueField = "BransId";
            ddlBrans.DataTextField = "BransAdi";
            ddlBrans.DataBind();
            ddlBrans.Items.Insert(0, new ListItem("Ders Seçiniz", ""));

            if (Master.Yetki().Contains("IlceMEMYetkilisi"))
            {

                ilceTr.Visible = true;
                KullanicilarDb kDb = new KullanicilarDb();
                KullanicilarInfo info = kDb.KayitBilgiGetir(Master.UyeId(), true);

                ddlIlce.SelectedValue = info.IlceAdi.BuyukHarfeCevir();
                ddlIlce.Enabled = false;

                CkSinavAdiInfo aktifSinav = sinav.AktifSinavAdi();
                ddlSinavlar.SelectedValue = aktifSinav.SinavId.ToString();

                CkKarneKutukDB kutukDb = new CkKarneKutukDB();
                rptKurumlar.DataSource = kutukDb.KayitlariGetir(aktifSinav.SinavId, info.IlceAdi);
                rptKurumlar.DataBind();


            }

            if (Master.Yetki().Contains("OkulYetkilisi"))
            {
                KullanicilarDb kDb = new KullanicilarDb();
                KullanicilarInfo info = kDb.KayitBilgiGetir(Master.UyeId(), true);

                ddlIlce.SelectedValue = info.IlceAdi.BuyukHarfeCevir();
                ddlIlce.Enabled = false;

                CkSinavAdiInfo aktifSinav = sinav.AktifSinavAdi();
                ddlSinavlar.SelectedValue = aktifSinav.SinavId.ToString();

                CkKarneKutukDB kutukDb = new CkKarneKutukDB();
                rptKurumlar.DataSource = kutukDb.KayitlariGetir(aktifSinav.SinavId, info.KurumKodu.ToInt32());
                rptKurumlar.DataBind();


            }
        }
    }

    protected void btnListele_OnClick(object sender, EventArgs e)
    {
        int sinavId = ddlSinavlar.SelectedValue.ToInt32();
        string ilceAdi = ddlIlce.SelectedValue;

        CkKarneKutukDB kutukDb = new CkKarneKutukDB();
        rptKurumlar.DataSource = kutukDb.KayitlariGetir(sinavId, ilceAdi);
        rptKurumlar.DataBind();

        if (!Master.Yetki().Contains("OkulYetkilisi")) //okul dışndaki yetkililere göster Root,Admin,IlceMEMYetkilisi
        {
            if(!string.IsNullOrEmpty(ilceAdi)&& sinavId!=0)
                ilceTr.Visible = true;
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
        int sinavId = value[0].ToInt32();
        int kurumKodu = value[1].ToInt32();
        string ilceAdi = value[2];
        string okulAdi = value[3];
    //    int sinif = ddlSinif.SelectedValue.ToInt32();
     //   int brans = ddlBrans.SelectedValue.ToInt32();

        string path = string.Format("{0} - {1} Kazanım Karnesi.pdf", ilceAdi, okulAdi);
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

        CkSinavAdiDB sinav = new CkSinavAdiDB();
        CkSinavAdiInfo sAdi = sinav.KayitBilgiGetir(sinavId);
        string sinavAdi = sAdi.SinavAdi;

        CkKarneCevapTxtDB cvpTxtDb = new CkKarneCevapTxtDB();
        List<CkKarneCevapTxtInfo> cvpTxtList = cvpTxtDb.KayitlariDizeGetir(sinavId);

        CkKarneDogruCevaplarDB dogruCevapDb = new CkKarneDogruCevaplarDB();
        List<CkKarneDogruCevaplarInfo> dogruCevapList = dogruCevapDb.KayitlariDizeGetir(sinavId);

        CkKarneSonuclariDB ksDb = new CkKarneSonuclariDB();
        CkKarneBranslarDB brnsDb = new CkKarneBranslarDB();
        CkKarneKutukDB kutukDb = new CkKarneKutukDB();
        CkKarneKazanimlardB kazanimDb = new CkKarneKazanimlardB();
        List<CkKarneKazanimlarInfo> kazanimList = kazanimDb.KayitlariDizeGetir(sinavId);

        List<CkKarneSonuclariInfo> karneSonucList = ksDb.KayitlariDizeGetir(sinavId, ilceAdi, kurumKodu);

        List<CkKarneSonuclariInfo> branslar = karneSonucList.GroupBy(x => x.BransId).Select(x => x.First()).OrderBy(x => x.BransId).ToList();

        var siniflar = karneSonucList.Where(x => x.KurumKodu == kurumKodu && x.SinavId == sinavId).GroupBy(x => x.Sinif).Select(x => x.First()).OrderBy(x => x.Sinif).ToList();
        foreach (CkKarneSonuclariInfo sinif in siniflar)
        {
            foreach (var brans in branslar)
            {
                string bransAdi = DersAdi(sinavId, brans.BransId).BransAdi;

                string sonSatirBaslik = string.Format("{0} {1}. SINIF KARNESİ", okulAdi.BuyukHarfeCevir(), sinif.Sinif);
                KarneModel karneModel = KarneModeliOlustur(ilceAdi, kurumKodu, okulAdi, sinif.Sinif, brans.BransId, sinavId, sinavAdi, bransAdi);
                //OKUL KARNESİ
                PdfOlustur(doc, sinavAdi, sinif, bransAdi, KolonSayisi.Okul.ToInt32(), sonSatirBaslik, karneModel);

                doc.NewPage();

            }
        }
        foreach (CkKarneSonuclariInfo sinif in siniflar)
        {
            foreach (var brans in branslar)
            {
                string bransAdi = DersAdi(sinavId, brans.BransId).BransAdi;

                List<CkKarneSonuclariInfo> subeler = karneSonucList.Where(x => x.KurumKodu == kurumKodu && x.Sinif == sinif.Sinif)
                .GroupBy(x => x.Sube).Select(x => x.First()).OrderBy(x => x.Sube).ToList();

                foreach (var sube in subeler)
                {
                    //ŞUBE KARNESİ

                    string sonSatirBaslik = string.Format("{0} {1}-{2} SINIFI ŞUBE KARNESİ", okulAdi.BuyukHarfeCevir(), sinif.Sinif, sube.Sube);
                    var karneModel = KarneModeliOlustur(ilceAdi, kurumKodu, okulAdi, sinif.Sinif, sube.Sube, brans.BransId, sinavId, sinavAdi, bransAdi, okulAdi);

                    PdfOlustur(doc, sinavAdi, sinif, bransAdi, KolonSayisi.Sube.ToInt32(), sonSatirBaslik, karneModel);
                    doc.NewPage();
                }
            }
        }

        foreach (CkKarneSonuclariInfo sinif in siniflar)
        {
            foreach (var brans in branslar)
            {
                string bransAdi = DersAdi(sinavId, brans.BransId).BransAdi;

                List<CkKarneSonuclariInfo> subeler = karneSonucList.Where(x => x.KurumKodu == kurumKodu && x.Sinif == sinif.Sinif)
                .GroupBy(x => x.Sube).Select(x => x.First()).OrderBy(x => x.Sube).ToList();

                foreach (var sube in subeler)
                {

                    //ÖĞRENCİ KARNESİ

                    List<CkKarneKazanimlarInfo> kazanimlar = kazanimList.Where(x => x.BransId == brans.BransId && x.Sinif == sinif.Sinif).ToList();
                    List<CkKarneKutukInfo> ogrenciListesi = kutukDb.KayitlariDizeGetir(sinavId, kurumKodu).Where(x => x.Sinifi == sinif.Sinif && x.Sube == sube.Sube).ToList();

                    foreach (var kutuk in ogrenciListesi)
                    {
                        CkKarneCevapTxtInfo ogrCevap = cvpTxtList.FirstOrDefault(x => x.BransId == brans.BransId && x.OpaqId == kutuk.OpaqId);
                        if (ogrCevap != null)
                        {
                            CkKarneDogruCevaplarInfo dogruCvplar = dogruCevapList.FirstOrDefault(x =>x.Sinif==sinif.Sinif&& x.BransId == brans.BransId && x.KitapcikTuru == ogrCevap.KitapcikTuru);
                            PdfOlustur(doc, sinavAdi, bransAdi, kutuk, ogrCevap, dogruCvplar, kazanimlar);
                            doc.NewPage();
                        }
                    }
                }

            }
        }

        doc.Close();

        //Dosyanın içeriğini Response içerisine aktarıyoruz.
        Response.Write(doc);

        //Son aşama da işlemleri bitirip, ekran çıktısına ulaşıyoruz.
        Response.End();

    }


    enum KolonSayisi
    {
        Il = 4,
        Ilce = 6,
        Okul = 7,
        Sube = 8
    }
    /// <summary>
    /// Öğrenci karnesi için pdf oluşturur
    /// </summary>
    /// <param name="path"></param>
    /// <param name="ayar"></param>
    /// <param name="dersAdi"></param>
    /// <param name="kutuk"></param>
    /// <param name="ogrCevap"></param>
    /// <param name="dogruCvplar"></param>
    /// <param name="kazanimlar"></param>
    private void PdfOlustur(Document doc, string sinavAdi, string dersAdi, CkKarneKutukInfo kutuk, CkKarneCevapTxtInfo ogrCevap, CkKarneDogruCevaplarInfo dogruCvplar, List<CkKarneKazanimlarInfo> kazanimlar)
    {
        int soruSayisi = dogruCvplar.Cevaplar.Length;
        string ogrenciCevabi = ogrCevap.Cevaplar;
        string dogruCevaplar = dogruCvplar.Cevaplar;

        int dogruSayisi = 0;
        int yanlisSayisi = 0;
        int bosSayisi = 0;

        for (int i = 0; i < soruSayisi; i++)
        {
            if (ogrenciCevabi.Substring(i, 1) == " ")
                bosSayisi++;
            else if (ogrenciCevabi.Substring(i, 1) == dogruCevaplar.Substring(i, 1))
                dogruSayisi++;
            else
                yanlisSayisi++;
        }


        PdfIslemleri pdf = new PdfIslemleri();

        #region Başlık

        pdf.addParagraph(doc, "ERZURUM  İL MİLLİ EĞİTİM MÜDÜRLÜĞÜ", hizalama: Element.ALIGN_CENTER);
        pdf.addParagraph(doc, "Ölçme Değerlendirme Merkezi", hizalama: Element.ALIGN_CENTER);
        pdf.addParagraph(doc, string.Format("{0} {1} DERSİ ÖĞRENCİ KARNESİ", sinavAdi.BuyukHarfeCevir(), dersAdi.BuyukHarfeCevir()), hizalama: Element.ALIGN_CENTER);

        #endregion

        #region Öğrenci Bilgileri Tablosu

        PdfPTable tableOgrenciBilgileri = new PdfPTable(7)
        {
            TotalWidth = 522f, // puan cinsinden tablonun gerçek genişliği
            LockedWidth = true, // tablonun mutlak genişliğini sabitle
            SpacingBefore = 20f, //öncesinde boşluk miktarı
            SpacingAfter = 0f, //sonrasında boşluk miktarı
            HorizontalAlignment = Element.ALIGN_LEFT
        };

        float[] widthsOB = { 75f, 352f, 20f, 20f, 20f, 20f, 20f };
        tableOgrenciBilgileri.SetWidths(widthsOB);

        var cvp = ogrCevap.Cevaplar;

        pdf.addCell(tableOgrenciBilgileri, "ÖĞRENCİ BİLGİLERİ", colspan: 2, fontSize: 6, bgColor: PdfIslemleri.Renkler.Gri, hizalama: Element.ALIGN_CENTER, fontStyle: iTextSharp.text.Font.BOLD);
        pdf.addCell(tableOgrenciBilgileri, "Uygulama Sonucu", colspan: 5, fontSize: 6, bgColor: PdfIslemleri.Renkler.Gri, hizalama: Element.ALIGN_CENTER, fontStyle: iTextSharp.text.Font.BOLD);

        pdf.addCell(tableOgrenciBilgileri, "ADI SOYADI :", hizalama: Element.ALIGN_RIGHT);
        pdf.addCell(tableOgrenciBilgileri, string.Format("{0} {1} {2}", kutuk.Adi, kutuk.Soyadi, (ogrCevap.KatilimDurumu == 0 ? "(Katılmadı))" : "")));
        pdf.addCell(tableOgrenciBilgileri, "SS", hizalama: Element.ALIGN_CENTER);
        pdf.addCell(tableOgrenciBilgileri, "D", hizalama: Element.ALIGN_CENTER);
        pdf.addCell(tableOgrenciBilgileri, "Y", hizalama: Element.ALIGN_CENTER);
        pdf.addCell(tableOgrenciBilgileri, "B", hizalama: Element.ALIGN_CENTER);
        pdf.addCell(tableOgrenciBilgileri, "KT", hizalama: Element.ALIGN_CENTER);

        pdf.addCell(tableOgrenciBilgileri, "İLÇESİ / OKULU :", hizalama: Element.ALIGN_RIGHT);
        pdf.addCell(tableOgrenciBilgileri, string.Format("{0} / {1}", kutuk.IlceAdi, kutuk.KurumAdi));
        pdf.addCell(tableOgrenciBilgileri, soruSayisi.ToString(), 2, hizalama: Element.ALIGN_CENTER);//soru sayısı
        pdf.addCell(tableOgrenciBilgileri, dogruSayisi.ToString(), 2, hizalama: Element.ALIGN_CENTER);//doğru
        pdf.addCell(tableOgrenciBilgileri, yanlisSayisi.ToString(), 2, hizalama: Element.ALIGN_CENTER);//yanlış
        pdf.addCell(tableOgrenciBilgileri, bosSayisi == 0 ? "" : bosSayisi.ToString(), 2, hizalama: Element.ALIGN_CENTER);//boş
        pdf.addCell(tableOgrenciBilgileri, ogrCevap.KitapcikTuru, 2, hizalama: Element.ALIGN_CENTER);//Kitapçık türü

        pdf.addCell(tableOgrenciBilgileri, "SINIFI / OKUL :", hizalama: Element.ALIGN_RIGHT);
        pdf.addCell(tableOgrenciBilgileri, string.Format("{0} / {1}", kutuk.Sinifi, kutuk.Sube));
        pdf.addCell(tableOgrenciBilgileri, "");

        doc.Add(tableOgrenciBilgileri);

        #endregion

        #region Cevap Anahtarı

        int kolonSayisi = soruSayisi + 1;
        PdfPTable tableOgrenciCevaplari = new PdfPTable(kolonSayisi)
        {
            TotalWidth = 522f, // puan cinsinden tablonun gerçek genişliği
            LockedWidth = true, // tablonun mutlak genişliğini sabitle
            SpacingBefore = 20f, //öncesinde boşluk miktarı
            SpacingAfter = 0f, //sonrasında boşluk miktarı
            HorizontalAlignment = Element.ALIGN_LEFT
        };

        float[] widthsOC = new float[kolonSayisi];
        widthsOC[0] = 60f;
        float kolonW = 467f / kolonSayisi;

        for (int i = 1; i < kolonSayisi; i++)
        {
            widthsOC[i] = kolonW;
        }
        tableOgrenciCevaplari.SetWidths(widthsOC);

        //boş hücre
        pdf.addCell(tableOgrenciCevaplari, "", bgColor: PdfIslemleri.Renkler.Gri);

        //Soru Numaraları
        for (int i = 1; i < kolonSayisi; i++)
        {
            pdf.addCell(tableOgrenciCevaplari, i.ToString(), fontSize: 6, bgColor: PdfIslemleri.Renkler.Gri, hizalama: Element.ALIGN_CENTER, fontStyle: iTextSharp.text.Font.BOLD);
        }
        pdf.addCell(tableOgrenciCevaplari, "Cevap Anahtarı", hizalama: Element.ALIGN_LEFT, fontSize: 6, bgColor: PdfIslemleri.Renkler.Gri, fontStyle: iTextSharp.text.Font.BOLD);
        //Cevap Anahtarı
        for (int i = 1; i < kolonSayisi; i++)
        {
            pdf.addCell(tableOgrenciCevaplari, dogruCevaplar.Substring(i - 1, 1), fontSize: 6, hizalama: Element.ALIGN_CENTER);
        }
        pdf.addCell(tableOgrenciCevaplari, "Öğrenci Cevabı", hizalama: Element.ALIGN_LEFT, fontSize: 6, bgColor: PdfIslemleri.Renkler.Gri, fontStyle: iTextSharp.text.Font.BOLD);
        //Öğrencinin Cevapları
        if (ogrCevap.KatilimDurumu == 1) //Sınava katılmış ise 
        {
            for (int i = 1; i < kolonSayisi; i++)
            {
                pdf.addCell(tableOgrenciCevaplari, ogrenciCevabi.Substring(i - 1, 1), fontSize: 6, hizalama: Element.ALIGN_CENTER);
            }
        }
        doc.Add(tableOgrenciCevaplari);

        #endregion

        #region Kazamılar

        PdfPTable tableKazanimlar = new PdfPTable(3)
        {
            TotalWidth = 522f, // puan cinsinden tablonun gerçek genişliği
            LockedWidth = true, // tablonun mutlak genişliğini sabitle
            SpacingBefore = 20f, //öncesinde boşluk miktarı
            SpacingAfter = 0f, //sonrasında boşluk miktarı
            HorizontalAlignment = Element.ALIGN_LEFT
        };

        float[] widthsKazanim = { 50f, 50f, 427f };

        tableKazanimlar.SetWidths(widthsKazanim);


        //Kazanım Karnesi 
        pdf.addCell(tableKazanimlar, "S. No", bgColor: PdfIslemleri.Renkler.Gri, hizalama: Element.ALIGN_CENTER, fontStyle: iTextSharp.text.Font.BOLD);
        pdf.addCell(tableKazanimlar, "Sonuç", bgColor: PdfIslemleri.Renkler.Gri, hizalama: Element.ALIGN_CENTER, fontStyle: iTextSharp.text.Font.BOLD);
        pdf.addCell(tableKazanimlar, "Kazanım", bgColor: PdfIslemleri.Renkler.Gri, hizalama: Element.ALIGN_CENTER, fontStyle: iTextSharp.text.Font.BOLD);

        for (int i = 1; i <= soruSayisi; i++)
        {
            string sonuc = "";
            string kazanimAdi = "";
            if (ogrCevap.KatilimDurumu == 1) //Sınava katılmış ise 
            {
                if (ogrenciCevabi.Substring(i - 1, 1) == " ")
                    sonuc = "B";
                else if (ogrenciCevabi.Substring(i - 1, 1) == dogruCevaplar.Substring(i - 1, 1))
                    sonuc = "D";
                else
                    sonuc = "Y";
                string soruSql = ogrCevap.KitapcikTuru + i + ",";
                CkKarneKazanimlarInfo kazanim = kazanimlar.Find(x => x.Sorulari.Contains(soruSql));

                if (kazanim != null)
                    kazanimAdi = kazanim.KazanimAdiOgrenci;
            }

            pdf.addCell(tableKazanimlar, i.ToString(), fontSize: 6, hizalama: Element.ALIGN_CENTER);
            pdf.addCell(tableKazanimlar, sonuc, fontSize: 6, hizalama: Element.ALIGN_CENTER);
            pdf.addCell(tableKazanimlar, kazanimAdi, fontSize: 6);
        }
        doc.Add(tableKazanimlar);

        #endregion

        #region Öğretmen Görüşü

        PdfPTable tableOG = new PdfPTable(3)
        {
            TotalWidth = 522f, // puan cinsinden tablonun gerçek genişliği
            LockedWidth = true, // tablonun mutlak genişliğini sabitle
            SpacingBefore = 20f, //öncesinde boşluk miktarı
            SpacingAfter = 0f, //sonrasında boşluk miktarı
            HorizontalAlignment = Element.ALIGN_LEFT
        };

        float[] widthsOG = { 175f, 175f, 175f };

        tableOG.SetWidths(widthsOG);


        pdf.addCell(tableOG, "ÖĞRETMEN GÖRÜŞÜ", colspan: 3, bgColor: PdfIslemleri.Renkler.Gri, hizalama: Element.ALIGN_CENTER, fontStyle: iTextSharp.text.Font.BOLD);

        pdf.addCell(tableOG, "Olumlu Yönler", bgColor: PdfIslemleri.Renkler.Gri, hizalama: Element.ALIGN_CENTER, fontStyle: iTextSharp.text.Font.BOLD);
        pdf.addCell(tableOG, "Geliştirilmesi Gereken Yönler", bgColor: PdfIslemleri.Renkler.Gri, hizalama: Element.ALIGN_CENTER, fontStyle: iTextSharp.text.Font.BOLD);
        pdf.addCell(tableOG, "Yapması Gerekenler", bgColor: PdfIslemleri.Renkler.Gri, hizalama: Element.ALIGN_CENTER, fontStyle: iTextSharp.text.Font.BOLD);
        //Kazanım Karnesi 
        pdf.addCell(tableOG, " ", hizalama: Element.ALIGN_CENTER, yukseklik: 100);
        pdf.addCell(tableOG, " ", hizalama: Element.ALIGN_CENTER, yukseklik: 100);
        pdf.addCell(tableOG, " ", hizalama: Element.ALIGN_CENTER, yukseklik: 100);

        pdf.addCell(tableOG, "Ders Öğretmeni (Adı Soyadı İmza)", fontSize: 5, colspan: 3, hizalama: Element.ALIGN_RIGHT, yukseklik: 30, vertical: Element.ALIGN_TOP);

        doc.Add(tableOG);

        #endregion

        //-------------SON SATIR -----------------
        pdf.addParagraph(doc, "\n\n");
        pdf.addParagraph(doc, "Erzurum Ölçme ve Değerlendirme Merkezi - Adres: Lalapaşa Mahallesi, Yukarı Mumcu Caddesi, Atatürk Evi Sokak, No1, Kat 5/6, Yakutiye / ERZURUM\n", 7, Element.ALIGN_CENTER, iTextSharp.text.Font.ITALIC);
        pdf.addParagraph(doc, "Web: http://erzurumodm.meb.gov.tr E-Mail: odm25@meb.gov.tr", 7, Element.ALIGN_CENTER, iTextSharp.text.Font.ITALIC);

    }

    /// <summary>
    /// İl İlçe Okul Sınıf ve Şube Karneleri için pdf oluşturur
    /// </summary>
    private void PdfOlustur(Document doc, string sinavAdi, CkKarneSonuclariInfo sinif, string dersAdi, int column, string sonSatirBaslik, KarneModel karneModel)
    {
        if (karneModel.TableModeller.Count > 0)
        {

            PdfIslemleri pdf = new PdfIslemleri();

            #region Başlık

            pdf.addParagraph(doc, "ERZURUM  İL MİLLİ EĞİTİM MÜDÜRLÜĞÜ", hizalama: Element.ALIGN_CENTER);
            pdf.addParagraph(doc, "Ölçme Değerlendirme Merkezi", hizalama: Element.ALIGN_CENTER);
            pdf.addParagraph(doc, string.Format("{0} {1}. SINIF {2} DERSİ", sinavAdi.BuyukHarfeCevir(), sinif.Sinif, dersAdi.BuyukHarfeCevir()), hizalama: Element.ALIGN_CENTER);
            pdf.addParagraph(doc, sonSatirBaslik, hizalama: Element.ALIGN_CENTER);

            #endregion

            float[] widths4 = { 25f, 40f, 392f, 70f }; //il  
            float[] widths6 = { 25f, 40f, 285f, 30f, 30f, 117f }; //il - ilçe 
            float[] widths7 = { 25f, 40f, 255f, 30f, 30f, 30f, 117f }; //il - ilçe - okul
            float[] widths8 = { 25f, 40f, 225f, 30f, 30f, 30f, 30f, 117f }; //il - ilçe - okul - şube

            PdfPTable table = new PdfPTable(column)
            {
                TotalWidth = 522f, // puan cinsinden tablonun gerçek genişliği
                LockedWidth = true, // tablonun mutlak genişliğini sabitle
                SpacingBefore = 20f, //öncesinde boşluk miktarı
                SpacingAfter = 30f, //sonrasında boşluk miktarı
            };


            if (column == KolonSayisi.Il.ToInt32())
                table.SetWidths(widths4);
            else if (column == KolonSayisi.Ilce.ToInt32())
                table.SetWidths(widths6);
            else if (column == KolonSayisi.Okul.ToInt32())
                table.SetWidths(widths7);
            else
                table.SetWidths(widths8);


            //--------------Üst Başlık
            pdf.addCell(table, "", colspan: 3, fontSize: 6, bgColor: PdfIslemleri.Renkler.Gri);
            pdf.addCell(table, "Edinilme Oranı %", hizalama: Element.ALIGN_CENTER, colspan: column - 4, bgColor: PdfIslemleri.Renkler.Gri);
            if (column > 4) pdf.addCell(table, "", bgColor: PdfIslemleri.Renkler.Gri);
            //Üst Başlık İkinci Satır
            pdf.addCell(table, "No", hizalama: Element.ALIGN_CENTER, bgColor: PdfIslemleri.Renkler.Gri);
            pdf.addCell(table, "Kazanım No", hizalama: Element.ALIGN_CENTER, bgColor: PdfIslemleri.Renkler.Gri);
            pdf.addCell(table, "Kazanım", bgColor: PdfIslemleri.Renkler.Gri);
            pdf.addCell(table, "İl", hizalama: Element.ALIGN_CENTER, bgColor: PdfIslemleri.Renkler.Gri);

            if (column > 5) pdf.addCell(table, "İlçe", hizalama: Element.ALIGN_CENTER, bgColor: PdfIslemleri.Renkler.Gri);
            if (column > 6) pdf.addCell(table, "Okul", hizalama: Element.ALIGN_CENTER, bgColor: PdfIslemleri.Renkler.Gri);
            if (column > 7) pdf.addCell(table, "Şube", hizalama: Element.ALIGN_CENTER, bgColor: PdfIslemleri.Renkler.Gri);
            if (column > 4) pdf.addCell(table, "", bgColor: PdfIslemleri.Renkler.Gri);
            //--------------Üst Başlık Son

            //--------------satırlar
            foreach (var t in karneModel.TableModeller)
            {
                pdf.addCell(table, t.No, hizalama: Element.ALIGN_CENTER); //Satır numarası
                pdf.addCell(table, t.KazanimNo, hizalama: Element.ALIGN_CENTER);
                pdf.addCell(table, t.Kazanim);
                pdf.addCell(table, t.IlPuani, hizalama: Element.ALIGN_CENTER);
                if (column > 5) pdf.addCell(table, t.IlcePuani, hizalama: Element.ALIGN_CENTER); //ilçe
                if (column > 6) pdf.addCell(table, t.OkulPuani, hizalama: Element.ALIGN_CENTER); //okul
                if (column > 7) pdf.addCell(table, t.SubePuani, hizalama: Element.ALIGN_CENTER); //şube
                if (column > 4) pdf.addCell(table, t.Aciklama, hizalama: Element.ALIGN_CENTER); //açıklama
            }

            doc.Add(table);

            pdf.addParagraph(doc, karneModel.Raporu, 7);
            pdf.addParagraph(doc, "Çalışmalarınızda kolaylıklar diler, katkılarınız için teşekkür ederiz.", 7, Element.ALIGN_RIGHT, iTextSharp.text.Font.ITALIC);
            pdf.addParagraph(doc, "\n\n");
            pdf.addParagraph(doc, "Erzurum Ölçme ve Değerlendirme Merkezi - Adres: Lalapaşa Mahallesi, Yukarı Mumcu Caddesi, Atatürk Evi Sokak, No1, Kat 5/6, Yakutiye / ERZURUM\n", 7, Element.ALIGN_CENTER, iTextSharp.text.Font.ITALIC);
            pdf.addParagraph(doc, "Web: http://erzurumodm.meb.gov.tr E-Mail: odm25@meb.gov.tr", 7, Element.ALIGN_CENTER, iTextSharp.text.Font.ITALIC);

        }
    }

    #region MyRegion
    //private KarneModel KarneModeliOlustur(int sinif, int brans, Ayarlar ayar)
    //{
    //    int destekKazanimSayisi = 0;
    //    int telafiKazanimSayisi = 0;
    //    List<TableModel> model = new List<TableModel>();
    //    List<string> eksikKazanimlar = new List<string>();


    //    int i = 1;
    //    karneSonucList karneSonucList = new karneSonucList();
    //    KazanimManager kazanimManager = new KazanimManager();

    //    foreach (var kazanim in kazanimManager.List(x => x.BransId == brans && x.Sinif == sinif))
    //    {
    //        int ilDogruSayisi = 0;
    //        int ilYanlisSayisi = 0;
    //        int ilBosSayisi = 0;


    //        //Kazanım soru numaralarının tutulduğu alanın sonundaki virgülü silmem gerekiyor. (B1,B10,BA12,A13, gibi olan verileri)
    //        //Çünkü dizide hata veriyor.

    //        string kazanimSorulariSonKarakteri = kazanim.Sorulari.Substring(kazanim.Sorulari.Length - 1, 1);
    //        //eğer son karakter değeri virgül ise onu kaldır
    //        string ks = kazanimSorulariSonKarakteri == ","
    //            ? kazanim.Sorulari.Substring(0, kazanim.Sorulari.Length - 1)
    //            : kazanim.Sorulari;

    //        string[] kazanimSorulari = ks.Split(','); //B1,B10,BA12,A13 gibi olan verileri
    //        foreach (var s in kazanimSorulari)
    //        {
    //            string kitapcikTuru = s.Substring(0, 1);
    //            int soruNo = s.Substring(1, s.Length - 1).ToInt32();

    //            ilDogruSayisi += karneSonucList.Where(x => x.BransId == brans && x.Sinif == sinif && x.KitapcikTuru == kitapcikTuru && x.SoruNo == soruNo).Sum(x => x.Dogru);
    //            ilYanlisSayisi += karneSonucList.Where(x => x.BransId == brans && x.Sinif == sinif && x.KitapcikTuru == kitapcikTuru && x.SoruNo == soruNo).Sum(x => x.Yanlis);
    //            ilBosSayisi += karneSonucList.Where(x => x.BransId == brans && x.Sinif == sinif && x.KitapcikTuru == kitapcikTuru && x.SoruNo == soruNo).Sum(x => x.Bos);

    //        }

    //        int ilOgrenciSayisi = ilDogruSayisi + ilYanlisSayisi + ilBosSayisi;

    //        int ilBasariDurumu = (((double)ilDogruSayisi * 100) / ilOgrenciSayisi).ToInt32();

    //        string aciklama = "";
    //        if (ilBasariDurumu >= 0 && ilBasariDurumu < 31)
    //        {
    //            aciklama = "Destekleme kursları yapılmalı";
    //            eksikKazanimlar.Add(string.Format("'{0} {1}'", kazanim.KazanimNo, kazanim.KazanimAdi));
    //            destekKazanimSayisi++;
    //        }
    //        else if (ilBasariDurumu >= 31 && ilBasariDurumu <= 49)
    //        {
    //            aciklama = "Sınıf içi telafi eğitimi yapılmalı";
    //            telafiKazanimSayisi++;
    //        }

    //        model.Add(new TableModel(i.ToString(), kazanim.KazanimNo, kazanim.KazanimAdi, ilBasariDurumu.ToString("0"), "", "", "", aciklama));


    //        i++;
    //    }

    //    string raporStr = String.Format("{0} {1}. sınıf {2} testi kazanımlarının ilimizde edinilme oranları,\n", sinavAdi, sinif, bransAdi);

    //    if (eksikKazanimlar.Count > 0)
    //    {
    //        raporStr += "Özellikle;\n";
    //        foreach (var k in eksikKazanimlar)
    //        {
    //            raporStr += string.Format("- {0}\n", k);
    //        }

    //        raporStr += "kazanımlarının yapılma yüzdeleri çok düşüktür.\n";
    //    }

    //    string desteklemeStr = "";
    //    string telafiStr = "";
    //    if (destekKazanimSayisi > 0)
    //    {
    //        desteklemeStr = "% 31’in altında kalan " + destekKazanimSayisi +
    //                        " kazanım için destekleme kurslarının planlanıp uygulanması ";
    //        desteklemeStr += telafiKazanimSayisi > 0 ? "; " : " ";
    //    }

    //    if (telafiKazanimSayisi > 0)
    //    {
    //        telafiStr = "% 31’den % 49’a kadar olan " + telafiKazanimSayisi +
    //                    " kazanımın öğrenme eksikliklerinin sınıf içi etkinliklerle giderilmesi ";
    //    }

    //    if (destekKazanimSayisi != 0 || telafiKazanimSayisi != 0)
    //    {
    //        raporStr += "Edinilme düzeyi " + desteklemeStr + telafiStr +
    //                       " tavsiye edilmektedir.Tabloda bu kazanımların karşılarına gerekli açıklamalar yapılmıştır.\n";
    //    }
    //    else
    //    {
    //        raporStr +=
    //            string.Format(@"Edinilme düzeyi % 31’in altında kalan {0} kazanım için destekleme kurslarının planlanıp uygulanması, % 31’den % 49’a kadar olan {1} kazanımın öğrenme eksikliklerinin sınıf içi etkinliklerle giderilmesi tavsiye edilmektedir.Tabloda bu kazanımların karşılarına gerekli açıklamalar yapılmıştır.
    //                                                Kazanım karnesinin incelenmesi, öğrenme eksikliklerinin tespit edilmesi ve gerekli telafi eğitimlerin yapılması, projenin asıl amacı olan eğitimde niteliğinin artırılması ve öğrenme eksikliklerinin giderilmesi hedefine ulaşılmasını sağlayacaktır.", destekKazanimSayisi, telafiKazanimSayisi);
    //    }

    //    KarneModel karneModel = new KarneModel
    //    {
    //        EksikKazanimlar = eksikKazanimlar,
    //        TableModeller = model,
    //        Raporu = raporStr
    //    };


    //    return karneModel;
    //}
    private KarneModel KarneModeliOlustur(string sinavAdi, string ilce, int sinif, string bransAdi, int brans, List<CkKarneKazanimlarInfo> kazanimList, List<CkKarneSonuclariInfo> karneSonucList)
    {
        int destekKazanimSayisi = 0;
        int telafiKazanimSayisi = 0;
        List<TableModel> model = new List<TableModel>();
        List<string> eksikKazanimlar = new List<string>();

        int i = 1;

        foreach (var kazanim in kazanimList.Where(x => x.BransId == brans && x.Sinif == sinif))
        {
            int ilDogruSayisi = 0;
            int ilYanlisSayisi = 0;
            int ilBosSayisi = 0;
            int ilceDogruSayisi = 0;
            int ilceYanlisSayisi = 0;
            int ilceBosSayisi = 0;

            //Kazanım soru numaralarının tutulduğu alanın sonundaki virgülü silmem gerekiyor. (B1,B10,BA12,A13, gibi olan verileri)
            //Çünkü dizide hata veriyor.

            string kazanimSorulariSonKarakteri = kazanim.Sorulari.Substring(kazanim.Sorulari.Length - 1, 1);
            //eğer son karakter değeri virgül ise onu kaldır
            string ks = kazanimSorulariSonKarakteri == ","
                ? kazanim.Sorulari.Substring(0, kazanim.Sorulari.Length - 1)
                : kazanim.Sorulari;

            string[] kazanimSorulari = ks.Split(','); //B1,B10,BA12,A13 gibi olan verileri
            foreach (var s in kazanimSorulari)
            {
                string kitapcikTuru = s.Substring(0, 1);
                int soruNo = s.Substring(1, s.Length - 1).ToInt32();

                ilDogruSayisi += karneSonucList.Where(x => x.BransId == brans && x.Sinif == sinif && x.KitapcikTuru == kitapcikTuru && x.SoruNo == soruNo).Sum(x => x.Dogru);
                ilYanlisSayisi += karneSonucList.Where(x => x.BransId == brans && x.Sinif == sinif && x.KitapcikTuru == kitapcikTuru && x.SoruNo == soruNo).Sum(x => x.Yanlis);
                ilBosSayisi += karneSonucList.Where(x => x.BransId == brans && x.Sinif == sinif && x.KitapcikTuru == kitapcikTuru && x.SoruNo == soruNo).Sum(x => x.Bos);

                ilceDogruSayisi += karneSonucList.Where(x => x.BransId == brans && x.Ilce == ilce && x.Sinif == sinif && x.KitapcikTuru == kitapcikTuru && x.SoruNo == soruNo).Sum(x => x.Dogru);
                ilceYanlisSayisi += karneSonucList.Where(x => x.BransId == brans && x.Ilce == ilce && x.Sinif == sinif && x.KitapcikTuru == kitapcikTuru && x.SoruNo == soruNo).Sum(x => x.Yanlis);
                ilceBosSayisi += karneSonucList.Where(x => x.BransId == brans && x.Ilce == ilce && x.Sinif == sinif && x.KitapcikTuru == kitapcikTuru && x.SoruNo == soruNo).Sum(x => x.Bos);

            }

            int ilOgrenciSayisi = ilDogruSayisi + ilYanlisSayisi + ilBosSayisi;
            int ilceOgrenciSayisi = ilceDogruSayisi + ilceYanlisSayisi + ilceBosSayisi;

            int ilBasariDurumu = (((double)ilDogruSayisi * 100) / ilOgrenciSayisi).ToInt32();
            int ilceBasariDurumu = (((double)ilceDogruSayisi * 100) / ilceOgrenciSayisi).ToInt32();

            string aciklama = "";
            if (ilceBasariDurumu >= 0 && ilceBasariDurumu < 31)
            {
                aciklama = "Destekleme kursları yapılmalı";
                eksikKazanimlar.Add(string.Format("'{0} {1}'", kazanim.KazanimNo, kazanim.KazanimAdi));
                destekKazanimSayisi++;
            }
            else if (ilceBasariDurumu >= 31 && ilceBasariDurumu <= 49)
            {
                aciklama = "Sınıf içi telafi eğitimi yapılmalı";
                telafiKazanimSayisi++;
            }

            model.Add(new TableModel(i.ToString(), kazanim.KazanimNo, kazanim.KazanimAdi, ilBasariDurumu.ToString("0"), ilceBasariDurumu.ToString("0"),
                "", "", aciklama));


            i++;
        }

        string raporStr = string.Format("{0} {1}. sınıf {2} testi kazanımlarının {3} ilçesinde edinilme oranları,\n", sinavAdi, sinif, bransAdi, ilce.IlkHarfleriBuyut());

        if (eksikKazanimlar.Count > 0)
        {
            raporStr += "Özellikle;\n";
            foreach (var k in eksikKazanimlar)
            {
                raporStr += string.Format("- {0}\n", k);
            }

            raporStr += "kazanımlarının yapılma yüzdeleri çok düşüktür.\n";
        }

        string desteklemeStr = "";
        string telafiStr = "";
        if (destekKazanimSayisi > 0)
        {
            desteklemeStr = "% 31’in altında kalan " + destekKazanimSayisi +
                            " kazanım için destekleme kurslarının planlanıp uygulanması ";
            desteklemeStr += telafiKazanimSayisi > 0 ? "; " : " ";
        }

        if (telafiKazanimSayisi > 0)
        {
            telafiStr = "% 31’den % 49’a kadar olan " + telafiKazanimSayisi +
                        " kazanımın öğrenme eksikliklerinin sınıf içi etkinliklerle giderilmesi ";
        }

        if (destekKazanimSayisi != 0 || telafiKazanimSayisi != 0)
        {
            raporStr += "Edinilme düzeyi " + desteklemeStr + telafiStr +
                           " tavsiye edilmektedir.Tabloda bu kazanımların karşılarına gerekli açıklamalar yapılmıştır.\n";
        }
        else
        {
            raporStr += string.Format(@"Edinilme düzeyi % 31’in altında kalan {0} kazanım için destekleme kurslarının planlanıp uygulanması, % 31’den % 49’a kadar olan {1} kazanımın öğrenme eksikliklerinin sınıf içi etkinliklerle giderilmesi tavsiye edilmektedir.Tabloda bu kazanımların karşılarına gerekli açıklamalar yapılmıştır.
                                                    Kazanım karnesinin incelenmesi, öğrenme eksikliklerinin tespit edilmesi ve gerekli telafi eğitimlerin yapılması, projenin asıl amacı olan eğitimde niteliğinin artırılması ve öğrenme eksikliklerinin giderilmesi hedefine ulaşılmasını sağlayacaktır.", destekKazanimSayisi, telafiKazanimSayisi);
        }

        KarneModel karneModel = new KarneModel
        {
            EksikKazanimlar = eksikKazanimlar,
            TableModeller = model,
            Raporu = raporStr
        };


        return karneModel;
    }

    #endregion
    private KarneModel KarneModeliOlustur(string ilce, int kurumkodu, string kurumAdi, int sinif, int brans, int sinavId, string sinavAdi, string bransAdi)
    {
        int destekKazanimSayisi = 0;
        int telafiKazanimSayisi = 0;
        List<TableModel> model = new List<TableModel>();
        List<string> eksikKazanimlar = new List<string>();


        int i = 1;


        CkKarneSonuclariDB ksDb = new CkKarneSonuclariDB();
        CkKarneKazanimlardB kazanimDb = new CkKarneKazanimlardB();
        List<CkKarneKazanimlarInfo> kazanimList = kazanimDb.KayitlariDizeGetir(sinavId);

        List<CkKarneSonuclariInfo> karneSonucList = ksDb.KayitlariDizeGetir(sinavId);

        foreach (var kazanim in kazanimList.Where(x => x.BransId == brans && x.Sinif == sinif))
        {
            int ilDogruSayisi = 0;
            int ilYanlisSayisi = 0;
            int ilBosSayisi = 0;
            int ilceDogruSayisi = 0;
            int ilceYanlisSayisi = 0;
            int ilceBosSayisi = 0;
            int okulDogruSayisi = 0;
            int okulYanlisSayisi = 0;
            int okulBosSayisi = 0;

            //Kazanım soru numaralarının tutulduğu alanın sonundaki virgülü silmem gerekiyor. (B1,B10,BA12,A13, gibi olan verileri)
            //Çünkü dizide hata veriyor.

            string kazanimSorulariSonKarakteri = kazanim.Sorulari.Substring(kazanim.Sorulari.Length - 1, 1);
            //eğer son karakter değeri virgül ise onu kaldır
            string ks = kazanimSorulariSonKarakteri == ","
                ? kazanim.Sorulari.Substring(0, kazanim.Sorulari.Length - 1)
                : kazanim.Sorulari;

            string[] kazanimSorulari = ks.Split(','); //B1,B10,BA12,A13 gibi olan verileri
            foreach (var s in kazanimSorulari)
            {
                string kitapcikTuru = s.Substring(0, 1);
                int soruNo = s.Substring(1, s.Length - 1).ToInt32();

                ilDogruSayisi += karneSonucList.Where(x => x.BransId == brans && x.Sinif == sinif && x.KitapcikTuru == kitapcikTuru && x.SoruNo == soruNo).Sum(x => x.Dogru);
                ilYanlisSayisi += karneSonucList.Where(x => x.BransId == brans && x.Sinif == sinif && x.KitapcikTuru == kitapcikTuru && x.SoruNo == soruNo).Sum(x => x.Yanlis);
                ilBosSayisi += karneSonucList.Where(x => x.BransId == brans && x.Sinif == sinif && x.KitapcikTuru == kitapcikTuru && x.SoruNo == soruNo).Sum(x => x.Bos);

                ilceDogruSayisi += karneSonucList.Where(x => x.BransId == brans && x.Ilce == ilce && x.Sinif == sinif && x.KitapcikTuru == kitapcikTuru && x.SoruNo == soruNo).Sum(x => x.Dogru);
                ilceYanlisSayisi += karneSonucList.Where(x => x.BransId == brans && x.Ilce == ilce && x.Sinif == sinif && x.KitapcikTuru == kitapcikTuru && x.SoruNo == soruNo).Sum(x => x.Yanlis);
                ilceBosSayisi += karneSonucList.Where(x => x.BransId == brans && x.Ilce == ilce && x.Sinif == sinif && x.KitapcikTuru == kitapcikTuru && x.SoruNo == soruNo).Sum(x => x.Bos);

                okulDogruSayisi += karneSonucList.Where(x => x.BransId == brans && x.KurumKodu == kurumkodu && x.Sinif == sinif && x.KitapcikTuru == kitapcikTuru && x.SoruNo == soruNo).Sum(x => x.Dogru);
                okulYanlisSayisi += karneSonucList.Where(x => x.BransId == brans && x.KurumKodu == kurumkodu && x.Sinif == sinif && x.KitapcikTuru == kitapcikTuru && x.SoruNo == soruNo).Sum(x => x.Yanlis);
                okulBosSayisi += karneSonucList.Where(x => x.BransId == brans && x.KurumKodu == kurumkodu && x.Sinif == sinif && x.KitapcikTuru == kitapcikTuru && x.SoruNo == soruNo).Sum(x => x.Bos);

            }

            int ilOgrenciSayisi = ilDogruSayisi + ilYanlisSayisi + ilBosSayisi;
            int ilceOgrenciSayisi = ilceDogruSayisi + ilceYanlisSayisi + ilceBosSayisi;
            int okulOgrenciSayisi = okulDogruSayisi + okulYanlisSayisi + okulBosSayisi;

            int ilBasariDurumu = (((double)ilDogruSayisi * 100) / ilOgrenciSayisi).ToInt32();
            int ilceBasariDurumu = (((double)ilceDogruSayisi * 100) / ilceOgrenciSayisi).ToInt32();
            int okulBasariDurumu = (((double)okulDogruSayisi * 100) / okulOgrenciSayisi).ToInt32();

            string aciklama = "";
            if (okulBasariDurumu >= 0 && okulBasariDurumu < 31)
            {
                aciklama = "Destekleme kursları yapılmalı";
                eksikKazanimlar.Add(string.Format("'{0} {1}'", kazanim.KazanimNo, kazanim.KazanimAdi));
                destekKazanimSayisi++;
            }
            else if (okulBasariDurumu >= 31 && okulBasariDurumu <= 49)
            {
                aciklama = "Sınıf içi telafi eğitimi yapılmalı";
                telafiKazanimSayisi++;
            }

            model.Add(new TableModel(i.ToString(), kazanim.KazanimNo, kazanim.KazanimAdi, ilBasariDurumu.ToString("0"), ilceBasariDurumu.ToString("0"),
                okulBasariDurumu.ToString("0"), "", aciklama));


            i++;
        }

        string raporStr = string.Format("{0} {1} {2}. sınıf {3} testi kazanımlarının edinilme oranları,\n", sinavAdi, kurumAdi, sinif, bransAdi);

        if (eksikKazanimlar.Count > 0)
        {
            raporStr += "Özellikle;\n";
            foreach (var k in eksikKazanimlar)
            {
                raporStr += string.Format("- {0}\n", k);
            }

            raporStr += "kazanımlarının yapılma yüzdeleri çok düşüktür.\n";
        }

        string desteklemeStr = "";
        string telafiStr = "";
        if (destekKazanimSayisi > 0)
        {
            desteklemeStr = "% 31’in altında kalan " + destekKazanimSayisi +
                            " kazanım için destekleme kurslarının planlanıp uygulanması ";
            desteklemeStr += telafiKazanimSayisi > 0 ? "; " : " ";
        }

        if (telafiKazanimSayisi > 0)
        {
            telafiStr = "% 31’den % 49’a kadar olan " + telafiKazanimSayisi +
                        " kazanımın öğrenme eksikliklerinin sınıf içi etkinliklerle giderilmesi ";
        }

        if (destekKazanimSayisi != 0 || telafiKazanimSayisi != 0)
        {
            raporStr += "Edinilme düzeyi " + desteklemeStr + telafiStr +
                           " tavsiye edilmektedir.Tabloda bu kazanımların karşılarına gerekli açıklamalar yapılmıştır.\n";
        }
        else
        {
            raporStr +=
                string.Format("Edinilme düzeyi % 31’in altında kalan {0} kazanım için destekleme kurslarının planlanıp uygulanması, % 31’den % 49’a kadar olan {1} kazanımın öğrenme eksikliklerinin sınıf içi etkinliklerle giderilmesi tavsiye edilmektedir.Tabloda bu kazanımların karşılarına gerekli açıklamalar yapılmıştır. Kazanım karnesinin incelenmesi, öğrenme eksikliklerinin tespit edilmesi ve gerekli telafi eğitimlerin yapılması, projenin asıl amacı olan eğitimde niteliğinin artırılması ve öğrenme eksikliklerinin giderilmesi hedefine ulaşılmasını sağlayacaktır.", destekKazanimSayisi, telafiKazanimSayisi);
        }

        KarneModel karneModel = new KarneModel
        {
            EksikKazanimlar = eksikKazanimlar,
            TableModeller = model,
            Raporu = raporStr
        };


        return karneModel;
    }
    private KarneModel KarneModeliOlustur(string ilce, int kurumkodu, string kurumAdi, int sinif, string sube, int brans, int sinavId, string sinavAdi, string bransAdi, string okulAdi)
    {
        int destekKazanimSayisi = 0;
        int telafiKazanimSayisi = 0;
        List<TableModel> model = new List<TableModel>();
        List<string> eksikKazanimlar = new List<string>();


        int i = 1;
        CkKarneSonuclariDB ksDb = new CkKarneSonuclariDB();
        CkKarneKazanimlardB kazanimDb = new CkKarneKazanimlardB();
        List<CkKarneKazanimlarInfo> kazanimList = kazanimDb.KayitlariDizeGetir(sinavId);

        List<CkKarneSonuclariInfo> karneSonucList = ksDb.KayitlariDizeGetir(sinavId);



        foreach (var kazanim in kazanimList.Where(x => x.BransId == brans && x.Sinif == sinif))
        {
            int ilDogruSayisi = 0;
            int ilYanlisSayisi = 0;
            int ilBosSayisi = 0;
            int ilceDogruSayisi = 0;
            int ilceYanlisSayisi = 0;
            int ilceBosSayisi = 0;
            int okulDogruSayisi = 0;
            int okulYanlisSayisi = 0;
            int okulBosSayisi = 0;
            int subeDogruSayisi = 0;
            int subeYanlisSayisi = 0;
            int subeBosSayisi = 0;


            //Kazanım soru numaralarının tutulduğu alanın sonundaki virgülü silmem gerekiyor. (B1,B10,BA12,A13, gibi olan verileri)
            //Çünkü dizide hata veriyor.

            string kazanimSorulariSonKarakteri = kazanim.Sorulari.Substring(kazanim.Sorulari.Length - 1, 1);
            //eğer son karakter değeri virgül ise onu kaldır
            string ks = kazanimSorulariSonKarakteri == ","
                ? kazanim.Sorulari.Substring(0, kazanim.Sorulari.Length - 1)
                : kazanim.Sorulari;

            string[] kazanimSorulari = ks.Split(','); //B1,B10,BA12,A13 gibi olan verileri
            foreach (var s in kazanimSorulari)
            {
                string kitapcikTuru = s.Substring(0, 1);
                int soruNo = s.Substring(1, s.Length - 1).ToInt32();

                ilDogruSayisi += karneSonucList.Where(x => x.BransId == brans && x.Sinif == sinif && x.KitapcikTuru == kitapcikTuru && x.SoruNo == soruNo).Sum(x => x.Dogru);
                ilYanlisSayisi += karneSonucList.Where(x => x.BransId == brans && x.Sinif == sinif && x.KitapcikTuru == kitapcikTuru && x.SoruNo == soruNo).Sum(x => x.Yanlis);
                ilBosSayisi += karneSonucList.Where(x => x.BransId == brans && x.Sinif == sinif && x.KitapcikTuru == kitapcikTuru && x.SoruNo == soruNo).Sum(x => x.Bos);

                ilceDogruSayisi += karneSonucList.Where(x => x.BransId == brans && x.Ilce == ilce && x.Sinif == sinif && x.KitapcikTuru == kitapcikTuru && x.SoruNo == soruNo).Sum(x => x.Dogru);
                ilceYanlisSayisi += karneSonucList.Where(x => x.BransId == brans && x.Ilce == ilce && x.Sinif == sinif && x.KitapcikTuru == kitapcikTuru && x.SoruNo == soruNo).Sum(x => x.Yanlis);
                ilceBosSayisi += karneSonucList.Where(x => x.BransId == brans && x.Ilce == ilce && x.Sinif == sinif && x.KitapcikTuru == kitapcikTuru && x.SoruNo == soruNo).Sum(x => x.Bos);

                okulDogruSayisi += karneSonucList.Where(x => x.BransId == brans && x.KurumKodu == kurumkodu && x.Sinif == sinif && x.KitapcikTuru == kitapcikTuru && x.SoruNo == soruNo).Sum(x => x.Dogru);
                okulYanlisSayisi += karneSonucList.Where(x => x.BransId == brans && x.KurumKodu == kurumkodu && x.Sinif == sinif && x.KitapcikTuru == kitapcikTuru && x.SoruNo == soruNo).Sum(x => x.Yanlis);
                okulBosSayisi += karneSonucList.Where(x => x.BransId == brans && x.KurumKodu == kurumkodu && x.Sinif == sinif && x.KitapcikTuru == kitapcikTuru && x.SoruNo == soruNo).Sum(x => x.Bos);

                subeDogruSayisi += karneSonucList.Where(x => x.BransId == brans && x.KurumKodu == kurumkodu && x.Sinif == sinif && x.Sube == sube && x.KitapcikTuru == kitapcikTuru && x.SoruNo == soruNo).Sum(x => x.Dogru);
                subeYanlisSayisi += karneSonucList.Where(x => x.BransId == brans && x.KurumKodu == kurumkodu && x.Sinif == sinif && x.Sube == sube && x.KitapcikTuru == kitapcikTuru && x.SoruNo == soruNo).Sum(x => x.Yanlis);
                subeBosSayisi += karneSonucList.Where(x => x.BransId == brans && x.KurumKodu == kurumkodu && x.Sinif == sinif && x.Sube == sube && x.KitapcikTuru == kitapcikTuru && x.SoruNo == soruNo).Sum(x => x.Bos);
            }

            int ilOgrenciSayisi = ilDogruSayisi + ilYanlisSayisi + ilBosSayisi;
            int ilceOgrenciSayisi = ilceDogruSayisi + ilceYanlisSayisi + ilceBosSayisi;
            int okulOgrenciSayisi = okulDogruSayisi + okulYanlisSayisi + okulBosSayisi;
            int subeOgrenciSayisi = subeDogruSayisi + subeYanlisSayisi + subeBosSayisi;

            int ilBasariDurumu = (((double)ilDogruSayisi * 100) / ilOgrenciSayisi).ToInt32();
            int ilceBasariDurumu = (((double)ilceDogruSayisi * 100) / ilceOgrenciSayisi).ToInt32();
            int okulBasariDurumu = (((double)okulDogruSayisi * 100) / okulOgrenciSayisi).ToInt32();
            int subeBasariDurumu = (((double)subeDogruSayisi * 100) / subeOgrenciSayisi).ToInt32();

            string aciklama = "";
            if (subeBasariDurumu >= 0 && subeBasariDurumu < 31)
            {
                aciklama = "Destekleme kursları yapılmalı";
                eksikKazanimlar.Add(string.Format("'{0} {1}'", kazanim.KazanimNo, kazanim.KazanimAdi));
                destekKazanimSayisi++;
            }
            else if (subeBasariDurumu >= 31 && subeBasariDurumu <= 49)
            {
                aciklama = "Sınıf içi telafi eğitimi yapılmalı";
                telafiKazanimSayisi++;
            }

            model.Add(new TableModel(i.ToString(), kazanim.KazanimNo, kazanim.KazanimAdi, ilBasariDurumu.ToString("0"), ilceBasariDurumu.ToString("0"),
                okulBasariDurumu.ToString("0"), subeBasariDurumu.ToString("0"), aciklama));


            i++;
        }

        string raporStr =
            string.Format("{0} {1} testi kazanımlarının {2} {3}-{4} şubesinde edinilme oranları,\n", sinavAdi, bransAdi, okulAdi, sinif, sube);

        if (eksikKazanimlar.Count > 0)
        {
            raporStr += "Özellikle;\n";
            foreach (var k in eksikKazanimlar)
            {
                raporStr += string.Format("- {0}\n", k);
            }

            raporStr += "kazanımlarının yapılma yüzdeleri çok düşüktür.\n";
        }

        string desteklemeStr = "";
        string telafiStr = "";
        if (destekKazanimSayisi > 0)
        {
            desteklemeStr = "% 31’in altında kalan " + destekKazanimSayisi +
                            " kazanım için destekleme kurslarının planlanıp uygulanması ";
            desteklemeStr += telafiKazanimSayisi > 0 ? "; " : " ";
        }

        if (telafiKazanimSayisi > 0)
        {
            telafiStr = "% 31’den % 49’a kadar olan " + telafiKazanimSayisi +
                        " kazanımın öğrenme eksikliklerinin sınıf içi etkinliklerle giderilmesi ";
        }

        if (destekKazanimSayisi != 0 || telafiKazanimSayisi != 0)
        {
            raporStr += "Edinilme düzeyi " + desteklemeStr + telafiStr +
                           " tavsiye edilmektedir.Tabloda bu kazanımların karşılarına gerekli açıklamalar yapılmıştır.\n";
        }
        else
        {
            raporStr +=
                string.Format(@"Edinilme düzeyi % 31’in altında kalan {0} kazanım için destekleme kurslarının planlanıp uygulanması, % 31’den % 49’a kadar olan {1} kazanımın öğrenme eksikliklerinin sınıf içi etkinliklerle giderilmesi tavsiye edilmektedir.Tabloda bu kazanımların karşılarına gerekli açıklamalar yapılmıştır.
                                                    Kazanım karnesinin incelenmesi, öğrenme eksikliklerinin tespit edilmesi ve gerekli telafi eğitimlerin yapılması, projenin asıl amacı olan eğitimde niteliğinin artırılması ve öğrenme eksikliklerinin giderilmesi hedefine ulaşılmasını sağlayacaktır.", destekKazanimSayisi, telafiKazanimSayisi);
        }

        KarneModel karneModel = new KarneModel
        {
            EksikKazanimlar = eksikKazanimlar,
            TableModeller = model,
            Raporu = raporStr
        };


        return karneModel;
    }
    protected void lnkIlceKarnesi_OnClick(object sender, EventArgs e)
    {
        int sinavId = ddlSinavlar.SelectedValue.ToInt32();
        string ilceAdi = ddlIlce.SelectedValue;
        ltrIlceAdi.Text = ilceAdi;


        CkKarneSonuclariDB ksDb = new CkKarneSonuclariDB();
        CkKarneKazanimlardB kazanimDb = new CkKarneKazanimlardB();

        List<CkKarneKazanimlarInfo> kazanimList = kazanimDb.KayitlariDizeGetir(sinavId);
        List<CkKarneSonuclariInfo> karneSonucList = ksDb.KayitlariDizeGetir(sinavId);

        if (karneSonucList.Count > 0)
        {

            string path = string.Format("{0} - İlçe Kazanım Karnesi.pdf", ilceAdi.IlkHarfleriBuyut());
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

            CkSinavAdiDB sinav = new CkSinavAdiDB();
            CkSinavAdiInfo sAdi = sinav.KayitBilgiGetir(sinavId);
            string sinavAdi = sAdi.SinavAdi;

            List<CkKarneSonuclariInfo> branslar = karneSonucList.GroupBy(x => x.BransId).Select(x => x.First()).OrderBy(x => x.BransId).ToList();

            var siniflar = karneSonucList.Where(x => x.SinavId == sinavId).GroupBy(x => x.Sinif).Select(x => x.First()).OrderBy(x => x.Sinif).ToList();
            foreach (CkKarneSonuclariInfo sinif in siniflar)
            {
                foreach (var brans in branslar)
                {
                    string bransAdi = DersAdi(sinavId, brans.BransId).BransAdi;

                    string sonSatirBaslik = string.Format("{0} İLÇE KARNESİ", ilceAdi.BuyukHarfeCevir());
                    KarneModel karneModel = KarneModeliOlustur(sinavAdi, ilceAdi, sinif.Sinif, bransAdi, brans.BransId, kazanimList, karneSonucList);

                    PdfOlustur(doc, sinavAdi, sinif, bransAdi, KolonSayisi.Ilce.ToInt32(), sonSatirBaslik, karneModel);

                    doc.NewPage();
                }
            }
            doc.Close();

            //Dosyanın içeriğini Response içerisine aktarıyoruz.
            Response.Write(doc);

            //Son aşama da işlemleri bitirip, ekran çıktısına ulaşıyoruz.
            Response.End();

        }
        else
        {
            Master.UyariTuruncu("İlçeye ait karne bulunamadı", phUyari);
        }
    }
}