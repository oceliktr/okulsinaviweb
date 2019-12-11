using iTextSharp.text;
using iTextSharp.text.pdf;
using System;
using System.Collections.Generic;
using System.Linq;

/// <summary>
/// Summary description for TekSayfaliKarne
/// </summary>
public class TekSayfaliKarne
{
    public enum KolonSayisi
    {
        Il = 6,
        Ilce = 7,
        Okul = 8,
        Sube = 9
    }


    /// <summary>
    /// Her sayfada birden fazla dersli Öğrenci karnesi için pdf oluşturur
    /// </summary>
    /// <param name="path"></param>
    /// <param name="ayar"></param>
    /// <param name="dersAdi"></param>
    /// <param name="kutuk"></param>
    /// <param name="ogrCevap"></param>
    /// <param name="dogruCvplar"></param>
    /// <param name="kazanimlar"></param>
    public void BirdenFazlaDersliPdfOlustur(Document doc, string sinavAdi, string dersAdi, CkKarneKutukInfo kutuk, string ogrenciCevabi, string dogruCvplar, List<CkKarneKazanimlarInfo> kazanimlar, string katilimDurumu, string kitapcikTuru)
    {
        int soruSayisi = dogruCvplar.Length;

        int dogruSayisi = 0;
        int yanlisSayisi = 0;
        int bosSayisi = 0;

        for (int i = 0; i < soruSayisi; i++)
        {
            if (ogrenciCevabi.Substring(i, 1) == " ")
                bosSayisi++;
            else if (ogrenciCevabi.Substring(i, 1) == dogruCvplar.Substring(i, 1))
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


        pdf.addCell(tableOgrenciBilgileri, "ÖĞRENCİ BİLGİLERİ", colspan: 2, fontSize: 6, bgColor: PdfIslemleri.Renkler.Gri, hizalama: Element.ALIGN_CENTER, fontStyle: Font.BOLD);
        pdf.addCell(tableOgrenciBilgileri, "Uygulama Sonucu", colspan: 5, fontSize: 6, bgColor: PdfIslemleri.Renkler.Gri, hizalama: Element.ALIGN_CENTER, fontStyle: Font.BOLD);

        pdf.addCell(tableOgrenciBilgileri, "ADI SOYADI :", hizalama: Element.ALIGN_RIGHT);
        pdf.addCell(tableOgrenciBilgileri, string.Format("{0} {1} {2}", kutuk.Adi, kutuk.Soyadi, (katilimDurumu == "0" ? "(Katılmadı))" : "")));
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
        pdf.addCell(tableOgrenciBilgileri, kitapcikTuru, 2, hizalama: Element.ALIGN_CENTER);//Kitapçık türü

        pdf.addCell(tableOgrenciBilgileri, "SINIFI / OKUL :", hizalama: Element.ALIGN_RIGHT);
        pdf.addCell(tableOgrenciBilgileri, string.Format("{0} / {1}", kutuk.Sinifi, kutuk.Sube));
        pdf.addCell(tableOgrenciBilgileri, "");

        doc.Add(tableOgrenciBilgileri);

        pdf.addParagraph(doc, "SS: Soru Sayısı    D: Doğru Cevap Sayısı   Y: Yanlış Cevap Sayısı   B: Boş Sayısı   KT: Kitapçık Türü", fontSize: 6, fontStil: Font.ITALIC, hizalama: Element.ALIGN_RIGHT);

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
            pdf.addCell(tableOgrenciCevaplari, i.ToString(), fontSize: 6, bgColor: PdfIslemleri.Renkler.Gri, hizalama: Element.ALIGN_CENTER, fontStyle: Font.BOLD);
        }
        pdf.addCell(tableOgrenciCevaplari, "Cevap Anahtarı", hizalama: Element.ALIGN_LEFT, fontSize: 6, bgColor: PdfIslemleri.Renkler.Gri, fontStyle: Font.BOLD);
        //Cevap Anahtarı
        for (int i = 1; i < kolonSayisi; i++)
        {
            pdf.addCell(tableOgrenciCevaplari, dogruCvplar.Substring(i - 1, 1), fontSize: 6, hizalama: Element.ALIGN_CENTER);
        }
        pdf.addCell(tableOgrenciCevaplari, "Öğrenci Cevabı", hizalama: Element.ALIGN_LEFT, fontSize: 6, bgColor: PdfIslemleri.Renkler.Gri, fontStyle: Font.BOLD);
        //Öğrencinin Cevapları
        if (katilimDurumu != "0") //Sınava katılmış ise 
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
        pdf.addCell(tableKazanimlar, "S. No", bgColor: PdfIslemleri.Renkler.Gri, hizalama: Element.ALIGN_CENTER, fontStyle: Font.BOLD);
        pdf.addCell(tableKazanimlar, "Sonuç", bgColor: PdfIslemleri.Renkler.Gri, hizalama: Element.ALIGN_CENTER, fontStyle: Font.BOLD);
        pdf.addCell(tableKazanimlar, "Kazanım", bgColor: PdfIslemleri.Renkler.Gri, hizalama: Element.ALIGN_CENTER, fontStyle: Font.BOLD);

        for (int i = 1; i <= soruSayisi; i++)
        {
            string sonuc = "";
            string kazanimAdi = "";
            if (katilimDurumu != "0") //Sınava katılmış ise 
            {
                if (ogrenciCevabi.Substring(i - 1, 1) == " ")
                    sonuc = "B";
                else if (ogrenciCevabi.Substring(i - 1, 1) == dogruCvplar.Substring(i - 1, 1))
                    sonuc = "D";
                else
                    sonuc = "Y";
                string soruSql = kitapcikTuru + i + ",";
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

        //-------------SON SATIR -----------------
        pdf.addParagraph(doc, "\n\n");
        pdf.addParagraph(doc, "Erzurum Ölçme ve Değerlendirme Merkezi - Adres: Lalapaşa Mahallesi, Yukarı Mumcu Caddesi, Atatürk Evi Sokak, No1, Kat 5/6, Yakutiye / ERZURUM\n", 7, Element.ALIGN_CENTER, Font.ITALIC);
        pdf.addParagraph(doc, "Web: http://erzurumodm.meb.gov.tr E-Mail: odm25@meb.gov.tr", 7, Element.ALIGN_CENTER, Font.ITALIC);

    }


    /// <summary>
    /// Her sayfada bir ders olan Öğrenci karnesi için pdf oluşturur
    /// </summary>
    /// <param name="path"></param>
    /// <param name="ayar"></param>
    /// <param name="dersAdi"></param>
    /// <param name="kutuk"></param>
    /// <param name="ogrCevap"></param>
    /// <param name="dogruCvplar"></param>
    /// <param name="kazanimlar"></param>
    public void PdfOlustur(Document doc, string sinavAdi, string dersAdi, CkKarneKutukInfo kutuk, string ogrenciCevabi, string dogruCvplar, List<CkKarneKazanimlarInfo> kazanimlar, string katilimDurumu, string kitapcikTuru)
    {
        int soruSayisi = dogruCvplar.Length;

        int dogruSayisi = 0;
        int yanlisSayisi = 0;
        int bosSayisi = 0;

        for (int i = 0; i < soruSayisi; i++)
        {
            if (ogrenciCevabi.Substring(i, 1) == " ")
                bosSayisi++;
            else if (ogrenciCevabi.Substring(i, 1) == dogruCvplar.Substring(i, 1))
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


        pdf.addCell(tableOgrenciBilgileri, "ÖĞRENCİ BİLGİLERİ", colspan: 2, fontSize: 6, bgColor: PdfIslemleri.Renkler.Gri, hizalama: Element.ALIGN_CENTER, fontStyle: Font.BOLD);
        pdf.addCell(tableOgrenciBilgileri, "Uygulama Sonucu", colspan: 5, fontSize: 6, bgColor: PdfIslemleri.Renkler.Gri, hizalama: Element.ALIGN_CENTER, fontStyle: Font.BOLD);

        pdf.addCell(tableOgrenciBilgileri, "ADI SOYADI :", hizalama: Element.ALIGN_RIGHT);
        pdf.addCell(tableOgrenciBilgileri, string.Format("{0} {1} {2}", kutuk.Adi, kutuk.Soyadi, (katilimDurumu == "0" ? "(Katılmadı))" : "")));
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
        pdf.addCell(tableOgrenciBilgileri, kitapcikTuru, 2, hizalama: Element.ALIGN_CENTER);//Kitapçık türü

        pdf.addCell(tableOgrenciBilgileri, "SINIFI / OKUL :", hizalama: Element.ALIGN_RIGHT);
        pdf.addCell(tableOgrenciBilgileri, string.Format("{0} / {1}", kutuk.Sinifi, kutuk.Sube));
        pdf.addCell(tableOgrenciBilgileri, "");

        doc.Add(tableOgrenciBilgileri);

        pdf.addParagraph(doc, "SS: Soru Sayısı    D: Doğru Cevap Sayısı   Y: Yanlış Cevap Sayısı   B: Boş Sayısı   KT: Kitapçık Türü", fontSize: 6, fontStil: Font.ITALIC, hizalama: Element.ALIGN_RIGHT);

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
            pdf.addCell(tableOgrenciCevaplari, i.ToString(), fontSize: 6, bgColor: PdfIslemleri.Renkler.Gri, hizalama: Element.ALIGN_CENTER, fontStyle: Font.BOLD);
        }
        pdf.addCell(tableOgrenciCevaplari, "Cevap Anahtarı", hizalama: Element.ALIGN_LEFT, fontSize: 6, bgColor: PdfIslemleri.Renkler.Gri, fontStyle: Font.BOLD);
        //Cevap Anahtarı
        for (int i = 1; i < kolonSayisi; i++)
        {
            pdf.addCell(tableOgrenciCevaplari, dogruCvplar.Substring(i - 1, 1), fontSize: 6, hizalama: Element.ALIGN_CENTER);
        }
        pdf.addCell(tableOgrenciCevaplari, "Öğrenci Cevabı", hizalama: Element.ALIGN_LEFT, fontSize: 6, bgColor: PdfIslemleri.Renkler.Gri, fontStyle: Font.BOLD);
        //Öğrencinin Cevapları
        if (katilimDurumu != "0") //Sınava katılmış ise 
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
        pdf.addCell(tableKazanimlar, "S. No", bgColor: PdfIslemleri.Renkler.Gri, hizalama: Element.ALIGN_CENTER, fontStyle: Font.BOLD);
        pdf.addCell(tableKazanimlar, "Sonuç", bgColor: PdfIslemleri.Renkler.Gri, hizalama: Element.ALIGN_CENTER, fontStyle: Font.BOLD);
        pdf.addCell(tableKazanimlar, "Kazanım", bgColor: PdfIslemleri.Renkler.Gri, hizalama: Element.ALIGN_CENTER, fontStyle: Font.BOLD);

        for (int i = 1; i <= soruSayisi; i++)
        {
            string sonuc = "";
            string kazanimAdi = "";
            if (katilimDurumu != "0") //Sınava katılmış ise 
            {
                if (ogrenciCevabi.Substring(i - 1, 1) == " ")
                    sonuc = "B";
                else if (ogrenciCevabi.Substring(i - 1, 1) == dogruCvplar.Substring(i - 1, 1))
                    sonuc = "D";
                else
                    sonuc = "Y";
                string soruSql = kitapcikTuru + i + ",";
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


        pdf.addCell(tableOG, "ÖĞRETMEN GÖRÜŞÜ", colspan: 3, bgColor: PdfIslemleri.Renkler.Gri, hizalama: Element.ALIGN_CENTER, fontStyle: Font.BOLD);

        pdf.addCell(tableOG, "Olumlu Yönler", bgColor: PdfIslemleri.Renkler.Gri, hizalama: Element.ALIGN_CENTER, fontStyle: Font.BOLD);
        pdf.addCell(tableOG, "Geliştirilmesi Gereken Yönler", bgColor: PdfIslemleri.Renkler.Gri, hizalama: Element.ALIGN_CENTER, fontStyle: Font.BOLD);
        pdf.addCell(tableOG, "Yapması Gerekenler", bgColor: PdfIslemleri.Renkler.Gri, hizalama: Element.ALIGN_CENTER, fontStyle: Font.BOLD);
        //Kazanım Karnesi 
        pdf.addCell(tableOG, " ", hizalama: Element.ALIGN_CENTER, yukseklik: 100);
        pdf.addCell(tableOG, " ", hizalama: Element.ALIGN_CENTER, yukseklik: 100);
        pdf.addCell(tableOG, " ", hizalama: Element.ALIGN_CENTER, yukseklik: 100);

        pdf.addCell(tableOG, "Ders Öğretmeni (Adı Soyadı İmza)", fontSize: 5, colspan: 3, hizalama: Element.ALIGN_RIGHT, yukseklik: 30, vertical: Element.ALIGN_TOP);

        doc.Add(tableOG);

        #endregion

        //-------------SON SATIR -----------------
        pdf.addParagraph(doc, "\n\n");
        pdf.addParagraph(doc, "Erzurum Ölçme ve Değerlendirme Merkezi - Adres: Lalapaşa Mahallesi, Yukarı Mumcu Caddesi, Atatürk Evi Sokak, No1, Kat 5/6, Yakutiye / ERZURUM\n", 7, Element.ALIGN_CENTER, Font.ITALIC);
        pdf.addParagraph(doc, "Web: http://erzurumodm.meb.gov.tr E-Mail: odm25@meb.gov.tr", 7, Element.ALIGN_CENTER, Font.ITALIC);

    }

    /// <summary>
    /// Okul Sınıf ve Şube Karneleri için pdf oluşturur
    /// </summary>
    public void PdfOlustur(Document doc, CkSinavAdiInfo sinav, int sinif, string dersAdi, int column, string sonSatirBaslik, KarneModel karneModel)
    {
        if (karneModel.TableModeller.Count > 0)
        {

            PdfIslemleri pdf = new PdfIslemleri();

            #region Başlık

            pdf.addParagraph(doc, "ERZURUM İL MİLLİ EĞİTİM MÜDÜRLÜĞÜ", hizalama: Element.ALIGN_CENTER);
            pdf.addParagraph(doc, "Ölçme Değerlendirme Merkezi", hizalama: Element.ALIGN_CENTER);
            pdf.addParagraph(doc, string.Format("{0} {1}. SINIF {2} DERSİ", sinav.SinavAdi.BuyukHarfeCevir(), sinif, dersAdi.BuyukHarfeCevir()), hizalama: Element.ALIGN_CENTER);
            pdf.addParagraph(doc, sonSatirBaslik, hizalama: Element.ALIGN_CENTER);

            #endregion
            float[] widths4 = { 25f, 41f, 40f, 352f, 30f, 70f }; //il  
            float[] widths6 = { 25f, 41f, 40f, 245f, 30f, 30f, 117f }; //il - ilçe 
            float[] widths7 = { 25f, 41f, 40f, 215f, 30f, 30f, 30f, 117f }; //il - ilçe - okul
            float[] widths8 = { 25f, 41f, 40f, 215f, 30f, 30f, 30f, 30f, 117f }; //il - ilçe - okul - şube

            if (sinav.DegerlendirmeTuru == DegerlendirmeTurleri.DegerlendirmeTuru.KonuBazli.ToInt32())
            {
                widths4 = widths4.Select(s => s != 41f ? s : 80f).ToArray(); //Sorular sutununu kazanım no kadar genişlet.
                widths4 = widths4.Where((val, idx) => idx != Array.IndexOf(widths4, 40f)).ToArray(); //kazanım no stununu kaldır

                widths6 = widths6.Select(s => s != 41f ? s : 80f).ToArray(); //Sorular sutununu kazanım no kadar genişlet.
                widths6 = widths6.Where((val, idx) => idx != Array.IndexOf(widths6, 40f)).ToArray(); //kazanım no stununu kaldır

                widths7 = widths7.Select(s => s != 41f ? s : 80f).ToArray(); //Sorular sutununu kazanım no kadar genişlet.
                widths7 = widths7.Where((val, idx) => idx != Array.IndexOf(widths7, 40f)).ToArray(); //kazanım no stununu kaldır

                widths8 = widths8.Select(s => s != 41f ? s : 80f).ToArray(); //Sorular sutununu kazanım no kadar genişlet.
                widths8 = widths8.Where((val, idx) => idx != Array.IndexOf(widths8, 40f)).ToArray(); //kazanım no stununu kaldır
            }


            int columnYeni = column;
            if (sinav.DegerlendirmeTuru == DegerlendirmeTurleri.DegerlendirmeTuru.KonuBazli.ToInt32())
                columnYeni--; //konu değerlendirmede kazanım numarası tablosu kaldırıldığı için bir eksilttik.

            PdfPTable table = new PdfPTable(columnYeni)
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

            int birlestirilenHucre = 4;
            if (sinav.DegerlendirmeTuru == DegerlendirmeTurleri.DegerlendirmeTuru.KonuBazli.ToInt32())
                birlestirilenHucre = 3;

            //--------------Üst Başlık
            if (column > 6) //ilde görünmesin
            {
                string edinilmeBaslik =
                    sinav.DegerlendirmeTuru == DegerlendirmeTurleri.DegerlendirmeTuru.KazanimBazli.ToInt32()
                        ? "Edinilme Oranı %"
                        : "Yapılma Oranı %";


                pdf.addCell(table, "", colspan: birlestirilenHucre, fontSize: 6, bgColor: PdfIslemleri.Renkler.Gri);
                pdf.addCell(table, edinilmeBaslik, hizalama: Element.ALIGN_CENTER, colspan: column - 5, bgColor: PdfIslemleri.Renkler.Gri);
                if (column > 4) pdf.addCell(table, "", bgColor: PdfIslemleri.Renkler.Gri);
            }

            //Üst Başlık İkinci Satır
            pdf.addCell(table, "No", hizalama: Element.ALIGN_CENTER, bgColor: PdfIslemleri.Renkler.Gri);
            pdf.addCell(table, "Soru No", hizalama: Element.ALIGN_CENTER, bgColor: PdfIslemleri.Renkler.Gri);
            if (sinav.DegerlendirmeTuru == DegerlendirmeTurleri.DegerlendirmeTuru.KazanimBazli.ToInt32())
            {
                pdf.addCell(table, "Kazanım No", hizalama: Element.ALIGN_CENTER, bgColor: PdfIslemleri.Renkler.Gri);
                pdf.addCell(table, "Kazanım", bgColor: PdfIslemleri.Renkler.Gri);
            }
            else
            {
                pdf.addCell(table, "Konu", bgColor: PdfIslemleri.Renkler.Gri);
            }
            pdf.addCell(table, "İl", hizalama: Element.ALIGN_CENTER, bgColor: PdfIslemleri.Renkler.Gri);

            if (column > 6) pdf.addCell(table, "İlçe", hizalama: Element.ALIGN_CENTER, bgColor: PdfIslemleri.Renkler.Gri);
            if (column > 7) pdf.addCell(table, "Okul", hizalama: Element.ALIGN_CENTER, bgColor: PdfIslemleri.Renkler.Gri);
            if (column > 8) pdf.addCell(table, "Şube", hizalama: Element.ALIGN_CENTER, bgColor: PdfIslemleri.Renkler.Gri);
            if (column > 4) pdf.addCell(table, "", bgColor: PdfIslemleri.Renkler.Gri);
            //--------------Üst Başlık Son

            //--------------satırlar
            foreach (var t in karneModel.TableModeller)
            {
                pdf.addCell(table, t.No, hizalama: Element.ALIGN_CENTER); //Satır numarası
                pdf.addCell(table, t.SoruNo, hizalama: Element.ALIGN_CENTER); //Soru numarası
                if (sinav.DegerlendirmeTuru == DegerlendirmeTurleri.DegerlendirmeTuru.KazanimBazli.ToInt32())
                    pdf.addCell(table, t.KazanimNo, hizalama: Element.ALIGN_CENTER);
                pdf.addCell(table, t.Kazanim);
                pdf.addCell(table, t.IlPuani, hizalama: Element.ALIGN_CENTER);
                if (column > 6) pdf.addCell(table, t.IlcePuani, hizalama: Element.ALIGN_CENTER); //ilçe
                if (column > 7) pdf.addCell(table, t.OkulPuani, hizalama: Element.ALIGN_CENTER); //okul
                if (column > 8) pdf.addCell(table, t.SubePuani, hizalama: Element.ALIGN_CENTER); //şube
                if (column > 4) pdf.addCell(table, t.Aciklama, hizalama: Element.ALIGN_CENTER); //açıklama
            }

            doc.Add(table);

            pdf.addParagraph(doc, karneModel.Raporu, 7);
            pdf.addParagraph(doc, "Çalışmalarınızda kolaylıklar diler, katkılarınız için teşekkür ederiz.", 7, Element.ALIGN_RIGHT, Font.ITALIC);
            pdf.addParagraph(doc, "\n");
            pdf.addParagraph(doc, "Not: Soru Numarası sütununda A1, B5 gibi ifadeler, ilgili kazanımın A kitapçığı 1. soru B kitapçığı 5. soruyla ölçüldüğünü ifade eder.", 6, fontStil: Font.ITALIC);
            pdf.addParagraph(doc, "\n\n");
            pdf.addParagraph(doc, "Erzurum Ölçme ve Değerlendirme Merkezi - Adres: Lalapaşa Mahallesi, Yukarı Mumcu Caddesi, Atatürk Evi Sokak, No1, Kat 5/6, Yakutiye / ERZURUM\n", 7, Element.ALIGN_CENTER, Font.ITALIC);
            pdf.addParagraph(doc, "Web: http://erzurumodm.meb.gov.tr E-Mail: odm25@meb.gov.tr", 7, Element.ALIGN_CENTER, Font.ITALIC);

        }
    }

    #region MyRegion

    public KarneModel KarneModeliOlusturIlce(CkSinavAdiInfo sinav, string ilce, int sinif, string bransAdi, int brans, List<CkKarneKazanimlarInfo> kazanimList, List<CkIlIlceOrtalamasiInfo> basariYuzdesi)
    {
        int destekKazanimSayisi = 0;
        int telafiKazanimSayisi = 0;
        List<TableModel> model = new List<TableModel>();
        List<string> eksikKazanimlar = new List<string>();

        int i = 1;

        foreach (var kazanim in kazanimList.Where(x => x.BransId == brans && x.Sinif == sinif))
        {

            CkIlIlceOrtalamasiInfo ililceOrtalamasi = basariYuzdesi.FirstOrDefault(x => x.BransId == brans && x.Sinif == sinif && x.KazanimId == kazanim.KazanimId && x.Ilce == ilce);
            int ilBasariDurumu = ililceOrtalamasi.IlBasariYuzdesi;
            int ilceBasariDurumu = ililceOrtalamasi.IlceBasariYuzdesi;

            string aciklama = "";
            if (ilceBasariDurumu >= 0 && ilceBasariDurumu < 31)
            {
                aciklama = "Çok düşük";
                eksikKazanimlar.Add(
                    sinav.DegerlendirmeTuru == DegerlendirmeTurleri.DegerlendirmeTuru.KazanimBazli.ToInt32()
                        ? string.Format("'{0} {1}'", kazanim.KazanimNo, kazanim.KazanimAdi)
                        : kazanim.KazanimAdi);
                destekKazanimSayisi++;
            }
            else if (ilceBasariDurumu >= 31 && ilceBasariDurumu <= 49)
            {
                aciklama = "Düşük";
                telafiKazanimSayisi++;
            }

            model.Add(new TableModel(i.ToString(), kazanim.Sorulari.Replace(",", ", "), kazanim.KazanimNo, kazanim.KazanimAdi, ilBasariDurumu.ToString("0"), ilceBasariDurumu.ToString("0"),
                "", "", aciklama));

            i++;
        }

        string konularKazanimlar = sinav.DegerlendirmeTuru == DegerlendirmeTurleri.DegerlendirmeTuru.KazanimBazli.ToInt32() ? "kazanımlarının" : "sorularının";

      
        string raporStr = string.Format("{0} {1}. sınıf {2} testi {3} {4} ilçesinde edinilme oranları,\n", sinav.SinavAdi, sinif, bransAdi, konularKazanimlar, ilce.IlkHarfleriBuyut());

        if (eksikKazanimlar.Count > 0)
        {
            raporStr += "Özellikle;\n";
            foreach (var k in eksikKazanimlar)
            {
                raporStr += string.Format("- {0}\n", k);
            }
            raporStr += konularKazanimlar + " yapılma yüzdeleri çok düşüktür.\n";
        }

        string desteklemeStr = "";
        string telafiStr = "";
        if (destekKazanimSayisi > 0)
        {
            desteklemeStr = "% 31’in altında kalan " + destekKazanimSayisi + " " + (sinav.DegerlendirmeTuru == DegerlendirmeTurleri.DegerlendirmeTuru.KazanimBazli.ToInt32() ? "kazanım" : "konu") + " için destekleme kurslarının planlanıp uygulanması ";
            desteklemeStr += telafiKazanimSayisi > 0 ? "; " : " ";
        }

        if (telafiKazanimSayisi > 0)
        {
            telafiStr = "% 31’den % 49’a kadar olan " + telafiKazanimSayisi +
                        " " + (sinav.DegerlendirmeTuru == DegerlendirmeTurleri.DegerlendirmeTuru.KazanimBazli.ToInt32() ? "kazanımın" : "konunun") + " öğrenme eksikliklerinin sınıf içi etkinliklerle giderilmesi ";
        }

        if (destekKazanimSayisi != 0 || telafiKazanimSayisi != 0)
        {
            raporStr += "Edinilme düzeyi " + desteklemeStr + telafiStr +
                           " tavsiye edilmektedir.Tabloda bu " + (sinav.DegerlendirmeTuru == DegerlendirmeTurleri.DegerlendirmeTuru.KazanimBazli.ToInt32() ? "kazanımların" : "konu başlıklarının") + " karşılarına gerekli açıklamalar yapılmıştır.\n";
        }
        else
        {
            if (sinav.DegerlendirmeTuru == DegerlendirmeTurleri.DegerlendirmeTuru.KazanimBazli.ToInt32())
            {

                raporStr += string.Format(@"Edinilme düzeyi % 31’in altında kalan {0} kazanım için destekleme kurslarının planlanıp uygulanması, % 31’den % 49’a kadar olan {1} kazanımın öğrenme eksikliklerinin sınıf içi etkinliklerle giderilmesi tavsiye edilmektedir.Tabloda bu kazanımların karşılarına gerekli açıklamalar yapılmıştır.
                                                    Kazanım karnesinin incelenmesi, öğrenme eksikliklerinin tespit edilmesi ve gerekli telafi eğitimlerin yapılması, projenin asıl amacı olan eğitimde niteliğinin artırılması ve öğrenme eksikliklerinin giderilmesi hedefine ulaşılmasını sağlayacaktır.", destekKazanimSayisi, telafiKazanimSayisi);
            }
            else
            {
                raporStr += string.Format(@"Edinilme düzeyi % 31’in altında kalan {0} konular için destekleme kurslarının planlanıp uygulanması, % 31’den % 49’a kadar olan {1} konunun öğrenme eksikliklerinin sınıf içi etkinliklerle giderilmesi tavsiye edilmektedir.Tabloda bu konu başlıklarının karşılarına gerekli açıklamalar yapılmıştır.
                                                    Karnenin incelenmesi, öğrenme eksikliklerinin tespit edilmesi ve gerekli telafi eğitimlerin yapılması, projenin asıl amacı olan eğitimde niteliğinin artırılması ve öğrenme eksikliklerinin giderilmesi hedefine ulaşılmasını sağlayacaktır.", destekKazanimSayisi, telafiKazanimSayisi);

            }
        }

        KarneModel karneModel = new KarneModel
        {
            EksikKazanimlar = eksikKazanimlar,
            TableModeller = model,
            Raporu = raporStr
        };

        return karneModel;
    }
    public KarneModel IlKarneModeliOlustur3(CkSinavAdiInfo sinav, int sinif, string bransAdi, int brans, List<CkKarneKazanimlarInfo> kazanimList, List<CkIlIlceOrtalamasiInfo> basariYuzdesi)
    {
        int destekKazanimSayisi = 0;
        int telafiKazanimSayisi = 0;
        List<TableModel> model = new List<TableModel>();
        List<string> eksikKazanimlar = new List<string>();

        int i = 1;

        foreach (var kazanim in kazanimList.Where(x => x.BransId == brans && x.Sinif == sinif))
        {

            CkIlIlceOrtalamasiInfo ililceOrtalamasi = basariYuzdesi.FirstOrDefault(x => x.BransId == brans && x.Sinif == sinif && x.KazanimId == kazanim.KazanimId);
            int ilBasariDurumu = ililceOrtalamasi.IlBasariYuzdesi;

            string aciklama = "";
            if (ilBasariDurumu >= 0 && ilBasariDurumu < 31)
            {
                aciklama = "Çok düşük";
                eksikKazanimlar.Add(
                    sinav.DegerlendirmeTuru == DegerlendirmeTurleri.DegerlendirmeTuru.KazanimBazli.ToInt32()
                        ? string.Format("'{0} {1}'", kazanim.KazanimNo, kazanim.KazanimAdi)
                        : kazanim.KazanimAdi);
                destekKazanimSayisi++;
            }
            else if (ilBasariDurumu >= 31 && ilBasariDurumu <= 49)
            {
                aciklama = "Düşük";
                telafiKazanimSayisi++;
            }

            model.Add(new TableModel(i.ToString(), kazanim.Sorulari.Replace(",", ", "), kazanim.KazanimNo, kazanim.KazanimAdi, ilBasariDurumu.ToString("0"), ilBasariDurumu.ToString("0"),
                "", "", aciklama));


            i++;
        }

        string raporStr = string.Format("{0} {1}. sınıf {2} testi " + (sinav.DegerlendirmeTuru == DegerlendirmeTurleri.DegerlendirmeTuru.KazanimBazli.ToInt32() ? "kazanımlarının" : "sorularının") + " Erzurum ilinde edinilme oranları,\n", sinav.SinavAdi, sinif, bransAdi);

        if (eksikKazanimlar.Count > 0)
        {
            raporStr += "Özellikle;\n";
            foreach (var k in eksikKazanimlar)
            {
                raporStr += string.Format("- {0}\n", k);
            }

            raporStr += (sinav.DegerlendirmeTuru == DegerlendirmeTurleri.DegerlendirmeTuru.KazanimBazli.ToInt32() ? "kazanımlarının" : "sorularının") + " yapılma yüzdeleri çok düşüktür.\n";
        }

        string desteklemeStr = "";
        string telafiStr = "";
        if (destekKazanimSayisi > 0)
        {
            desteklemeStr = "% 31’in altında kalan " + destekKazanimSayisi +
                            " " + (sinav.DegerlendirmeTuru == DegerlendirmeTurleri.DegerlendirmeTuru.KazanimBazli.ToInt32() ? "kazanım" : "konu") + " için destekleme kurslarının planlanıp uygulanması ";
            desteklemeStr += telafiKazanimSayisi > 0 ? "; " : " ";
        }

        if (telafiKazanimSayisi > 0)
        {
            telafiStr = "% 31’den % 49’a kadar olan " + telafiKazanimSayisi +
                        " " + (sinav.DegerlendirmeTuru == DegerlendirmeTurleri.DegerlendirmeTuru.KazanimBazli.ToInt32() ? "kazanımın" : "konunun") + " öğrenme eksikliklerinin sınıf içi etkinliklerle giderilmesi ";
        }

        if (destekKazanimSayisi != 0 || telafiKazanimSayisi != 0)
        {
            raporStr += "Edinilme düzeyi " + desteklemeStr + telafiStr +
                           " tavsiye edilmektedir.Tabloda bu " + (sinav.DegerlendirmeTuru == DegerlendirmeTurleri.DegerlendirmeTuru.KazanimBazli.ToInt32() ? "kazanımların" : "soruların") + " karşılarına gerekli açıklamalar yapılmıştır.\n";
        }
        else
        {
            if (sinav.DegerlendirmeTuru == DegerlendirmeTurleri.DegerlendirmeTuru.KazanimBazli.ToInt32())
            {
                raporStr += string.Format(@"Edinilme düzeyi % 31’in altında kalan {0} kazanım için destekleme kurslarının planlanıp uygulanması, % 31’den % 49’a kadar olan {1} kazanımın öğrenme eksikliklerinin sınıf içi etkinliklerle giderilmesi tavsiye edilmektedir.Tabloda bu kazanımların karşılarına gerekli açıklamalar yapılmıştır. Kazanım karnesinin incelenmesi, öğrenme eksikliklerinin tespit edilmesi ve gerekli telafi eğitimlerin yapılması, projenin asıl amacı olan eğitimde niteliğinin artırılması ve öğrenme eksikliklerinin giderilmesi hedefine ulaşılmasını sağlayacaktır.", destekKazanimSayisi, telafiKazanimSayisi);
            }
            else
            {
                raporStr += string.Format(@"Edinilme düzeyi % 31’in altında kalan {0} konu için destekleme kurslarının planlanıp uygulanması, % 31’den % 49’a kadar olan {1} konunun öğrenme eksikliklerinin sınıf içi etkinliklerle giderilmesi tavsiye edilmektedir.Tabloda bu konu başlıklarının karşılarına gerekli açıklamalar yapılmıştır. Karnenin incelenmesi, öğrenme eksikliklerinin tespit edilmesi ve gerekli telafi eğitimlerin yapılması, projenin asıl amacı olan eğitimde niteliğinin artırılması ve öğrenme eksikliklerinin giderilmesi hedefine ulaşılmasını sağlayacaktır.", destekKazanimSayisi, telafiKazanimSayisi);
            }

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
    /// <summary>
    /// Okul Karnesi için
    /// </summary>
    /// <param name="ilce"></param>
    /// <param name="kurumkodu"></param>
    /// <param name="kurumAdi"></param>
    /// <param name="sinif"></param>
    /// <param name="brans"></param>
    /// <param name="sinavId"></param>
    /// <param name="sinavAdi"></param>
    /// <param name="bransAdi"></param>
    /// <param name="kazanimList"></param>
    /// <param name="karneSonucList">Kurum Kodu düzeyinde karne sonuç listesi</param>
    /// <param name="basariYuzdesi"></param>
    /// <returns></returns>
    public KarneModel KarneModeliOlusturOkul(string ilce, string kurumAdi, int sinif, int brans, CkSinavAdiInfo sinav, string bransAdi, List<CkKarneKazanimlarInfo> kazanimList, List<CkKarneSonuclariInfo> karneSonucList, List<CkIlIlceOrtalamasiInfo> basariYuzdesi)
    {
        int destekKazanimSayisi = 0;
        int telafiKazanimSayisi = 0;
        List<TableModel> model = new List<TableModel>();
        List<string> eksikKazanimlar = new List<string>();


        int i = 1;

        foreach (var kazanim in kazanimList.Where(x => x.BransId == brans && x.Sinif == sinif))
        {
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

                okulDogruSayisi += karneSonucList.Where(x => x.BransId == brans && x.Sinif == sinif && x.KitapcikTuru == kitapcikTuru && x.SoruNo == soruNo).Sum(x => x.Dogru);
                okulYanlisSayisi += karneSonucList.Where(x => x.BransId == brans && x.Sinif == sinif && x.KitapcikTuru == kitapcikTuru && x.SoruNo == soruNo).Sum(x => x.Yanlis);
                okulBosSayisi += karneSonucList.Where(x => x.BransId == brans && x.Sinif == sinif && x.KitapcikTuru == kitapcikTuru && x.SoruNo == soruNo).Sum(x => x.Bos);

            }

            int okulOgrenciSayisi = okulDogruSayisi + okulYanlisSayisi + okulBosSayisi;

            CkIlIlceOrtalamasiInfo ililceOrtalamasi = basariYuzdesi.FirstOrDefault(x => x.BransId == brans && x.Sinif == sinif && x.KazanimId == kazanim.KazanimId && x.Ilce == ilce);
            int ilBasariDurumu = ililceOrtalamasi.IlBasariYuzdesi;
            int ilceBasariDurumu = ililceOrtalamasi.IlceBasariYuzdesi;

            int okulBasariDurumu = (((double)okulDogruSayisi * 100) / okulOgrenciSayisi).ToInt32();

            string aciklama = "";
            if (okulBasariDurumu >= 0 && okulBasariDurumu < 31)
            {
                aciklama = "Destekleme kursları yapılmalı";
                eksikKazanimlar.Add(
                    sinav.DegerlendirmeTuru == DegerlendirmeTurleri.DegerlendirmeTuru.KazanimBazli.ToInt32()
                        ? string.Format("'{0} {1}'", kazanim.KazanimNo, kazanim.KazanimAdi)
                        : kazanim.KazanimAdi);
                destekKazanimSayisi++;
            }
            else if (okulBasariDurumu >= 31 && okulBasariDurumu <= 49)
            {
                aciklama = "Sınıf içi telafi eğitimi yapılmalı";
                telafiKazanimSayisi++;
            }

            model.Add(new TableModel(i.ToString(), kazanim.Sorulari.Replace(",", ", "), kazanim.KazanimNo, kazanim.KazanimAdi, ilBasariDurumu.ToString("0"), ilceBasariDurumu.ToString("0"),
                okulBasariDurumu.ToString("0"), "", aciklama));


            i++;
        }


        var raporStr = RaporStr(kurumAdi, sinif, sinav, bransAdi, eksikKazanimlar, destekKazanimSayisi, telafiKazanimSayisi);

        KarneModel karneModel = new KarneModel
        {
            EksikKazanimlar = eksikKazanimlar,
            TableModeller = model,
            Raporu = raporStr
        };


        return karneModel;
    }

    private static string RaporStr(string kurumAdi, int sinif, CkSinavAdiInfo sinav, string bransAdi, List<string> eksikKazanimlar,
        int destekKazanimSayisi, int telafiKazanimSayisi)
    {
        string kazanimKonuStr =
            sinav.DegerlendirmeTuru == DegerlendirmeTurleri.DegerlendirmeTuru.KazanimBazli.ToInt32()
                ? "kazanımlarının"
                : "sorularının";

        string raporStr = string.Format("{0} {1} {2}. sınıf {3} testi {4} edinilme oranları,\n", sinav.SinavAdi, kurumAdi,
            sinif, bransAdi, kazanimKonuStr);

        if (eksikKazanimlar.Count > 0)
        {
            raporStr += "Özellikle;\n";
            foreach (var k in eksikKazanimlar)
            {
                raporStr += string.Format("- {0}\n", k);
            }

            raporStr += kazanimKonuStr + " yapılma yüzdeleri çok düşüktür.\n";
        }

        string desteklemeStr = "";
        string telafiStr = "";
        if (destekKazanimSayisi > 0)
        {
            desteklemeStr = "% 31’in altında kalan " + destekKazanimSayisi + " " +
                            (sinav.DegerlendirmeTuru == DegerlendirmeTurleri.DegerlendirmeTuru.KazanimBazli.ToInt32()
                                ? "kazanım"
                                : "konu") + " için destekleme kurslarının planlanıp uygulanması ";
            desteklemeStr += telafiKazanimSayisi > 0 ? "; " : " ";
        }

        if (telafiKazanimSayisi > 0)
        {
            telafiStr = "% 31’den % 49’a kadar olan " + telafiKazanimSayisi +
                        " " + (sinav.DegerlendirmeTuru == DegerlendirmeTurleri.DegerlendirmeTuru.KazanimBazli.ToInt32()
                            ? "kazanımın"
                            : "konunun") + " öğrenme eksikliklerinin sınıf içi etkinliklerle giderilmesi ";
        }

        if (destekKazanimSayisi != 0 || telafiKazanimSayisi != 0)
        {
            raporStr += "Edinilme düzeyi " + desteklemeStr + telafiStr +
                        " tavsiye edilmektedir.Tabloda bu " +
                        (sinav.DegerlendirmeTuru == DegerlendirmeTurleri.DegerlendirmeTuru.KazanimBazli.ToInt32()
                            ? "kazanımların"
                            : "konu başlıklarının") + " karşılarına gerekli açıklamalar yapılmıştır.\n";
        }
        else
        {
            raporStr +=
                string.Format(
                    "Edinilme düzeyi % 31’in altında kalan {0} " +
                    (sinav.DegerlendirmeTuru == DegerlendirmeTurleri.DegerlendirmeTuru.KazanimBazli.ToInt32()
                        ? "kazanım"
                        : "konu") + " için destekleme kurslarının planlanıp uygulanması, % 31’den % 49’a kadar olan {1} " +
                    (sinav.DegerlendirmeTuru == DegerlendirmeTurleri.DegerlendirmeTuru.KazanimBazli.ToInt32()
                        ? "kazanımın"
                        : "konunun") +
                    " öğrenme eksikliklerinin sınıf içi etkinliklerle giderilmesi tavsiye edilmektedir.Tabloda bu " +
                    (sinav.DegerlendirmeTuru == DegerlendirmeTurleri.DegerlendirmeTuru.KazanimBazli.ToInt32()
                        ? "kazanımların"
                        : "konu başlıklarının") +
                    " karşılarına gerekli açıklamalar yapılmıştır. Karnenin incelenmesi, öğrenme eksikliklerinin tespit edilmesi ve gerekli telafi eğitimlerin yapılması, projenin asıl amacı olan eğitimde niteliğinin artırılması ve öğrenme eksikliklerinin giderilmesi hedefine ulaşılmasını sağlayacaktır.",
                    destekKazanimSayisi, telafiKazanimSayisi);
        }

        return raporStr;
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="ilce"></param>
    /// <param name="kurumkodu"></param>
    /// <param name="kurumAdi"></param>
    /// <param name="sinif"></param>
    /// <param name="sube"></param>
    /// <param name="brans"></param>
    /// <param name="sinavId"></param>
    /// <param name="sinavAdi"></param>
    /// <param name="bransAdi"></param>
    /// <param name="okulAdi"></param>
    /// <param name="kazanimList"></param>
    /// <param name="karneSonucList">Kurum kodu düzeyinde karne sonuç listesi</param>
    /// <param name="basariYuzdesi"></param>
    /// <returns></returns>
    public KarneModel KarneModeliOlusturSube(string ilce, int kurumkodu, string kurumAdi, int sinif, string sube, int brans, CkSinavAdiInfo sinav, string bransAdi, string okulAdi, List<CkKarneKazanimlarInfo> kazanimList, List<CkKarneSonuclariInfo> karneSonucList, List<CkIlIlceOrtalamasiInfo> basariYuzdesi)
    {
        int destekKazanimSayisi = 0;
        int telafiKazanimSayisi = 0;
        List<TableModel> model = new List<TableModel>();
        List<string> eksikKazanimlar = new List<string>();


        int i = 1;

        foreach (var kazanim in kazanimList.Where(x => x.BransId == brans && x.Sinif == sinif))
        {
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


                okulDogruSayisi += karneSonucList.Where(x => x.BransId == brans && x.Sinif == sinif && x.KitapcikTuru == kitapcikTuru && x.SoruNo == soruNo).Sum(x => x.Dogru);
                okulYanlisSayisi += karneSonucList.Where(x => x.BransId == brans && x.Sinif == sinif && x.KitapcikTuru == kitapcikTuru && x.SoruNo == soruNo).Sum(x => x.Yanlis);
                okulBosSayisi += karneSonucList.Where(x => x.BransId == brans && x.Sinif == sinif && x.KitapcikTuru == kitapcikTuru && x.SoruNo == soruNo).Sum(x => x.Bos);

                subeDogruSayisi += karneSonucList.Where(x => x.BransId == brans && x.Sinif == sinif && x.Sube == sube && x.KitapcikTuru == kitapcikTuru && x.SoruNo == soruNo).Sum(x => x.Dogru);
                subeYanlisSayisi += karneSonucList.Where(x => x.BransId == brans && x.Sinif == sinif && x.Sube == sube && x.KitapcikTuru == kitapcikTuru && x.SoruNo == soruNo).Sum(x => x.Yanlis);
                subeBosSayisi += karneSonucList.Where(x => x.BransId == brans && x.Sinif == sinif && x.Sube == sube && x.KitapcikTuru == kitapcikTuru && x.SoruNo == soruNo).Sum(x => x.Bos);
            }

            int okulOgrenciSayisi = okulDogruSayisi + okulYanlisSayisi + okulBosSayisi;
            int subeOgrenciSayisi = subeDogruSayisi + subeYanlisSayisi + subeBosSayisi;


            CkIlIlceOrtalamasiInfo ililceOrtalamasi = basariYuzdesi.FirstOrDefault(x => x.BransId == brans && x.Sinif == sinif && x.KazanimId == kazanim.KazanimId && x.Ilce == ilce);
            int ilBasariDurumu = ililceOrtalamasi.IlBasariYuzdesi;
            int ilceBasariDurumu = ililceOrtalamasi.IlceBasariYuzdesi;


            int okulBasariDurumu = (((double)okulDogruSayisi * 100) / okulOgrenciSayisi).ToInt32();
            int subeBasariDurumu = (((double)subeDogruSayisi * 100) / subeOgrenciSayisi).ToInt32();

            string aciklama = "";
            if (subeBasariDurumu >= 0 && subeBasariDurumu < 31)
            {
                aciklama = "Destekleme kursları yapılmalı";
                eksikKazanimlar.Add(
                    sinav.DegerlendirmeTuru == DegerlendirmeTurleri.DegerlendirmeTuru.KazanimBazli.ToInt32()
                        ? string.Format("'{0} {1}'", kazanim.KazanimNo, kazanim.KazanimAdi)
                        : kazanim.KazanimAdi);
                destekKazanimSayisi++;
            }
            else if (subeBasariDurumu >= 31 && subeBasariDurumu <= 49)
            {
                aciklama = "Sınıf içi telafi eğitimi yapılmalı";
                telafiKazanimSayisi++;
            }

            model.Add(new TableModel(i.ToString(), kazanim.Sorulari.Replace(",", ", "), kazanim.KazanimNo, kazanim.KazanimAdi, ilBasariDurumu.ToString(), ilceBasariDurumu.ToString(),
                okulBasariDurumu.ToString("0"), subeBasariDurumu.ToString("0"), aciklama));


            i++;
        }
        var raporStr = RaporStr(kurumAdi, sinif, sinav, bransAdi, eksikKazanimlar, destekKazanimSayisi, telafiKazanimSayisi);

        KarneModel karneModel = new KarneModel
        {
            EksikKazanimlar = eksikKazanimlar,
            TableModeller = model,
            Raporu = raporStr
        };


        return karneModel;
    }

}
