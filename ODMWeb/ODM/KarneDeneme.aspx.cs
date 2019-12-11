using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DAL.CkKarne;
using iTextSharp.text;
using iTextSharp.text.pdf;

public partial class ODM_KarneDeneme : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        int sinavId = 530;
        CkKarneOgrenciCevaplariDB veridB= new CkKarneOgrenciCevaplariDB();
        var ogrCvpList = veridB.KayitlariDizeGetir(sinavId, 713789);
        CkKarneDogruCevaplarDB dogruCevaplarDb= new CkKarneDogruCevaplarDB();
        var dogruCevapList = dogruCevaplarDb.KayitlariDizeGetir(sinavId);

        foreach (var cvp in ogrCvpList)
        {
            string[] cevap = cvp.Cevaplar.Split('#');
            for (int i = 0; i < cevap.Length-1; i+=2)
            {
                int bransId = cevap[i].ToInt32();
                string bransCevap = cevap[i+1];

                var dcvp = dogruCevapList.FirstOrDefault(x => x.BransId == bransId && x.KitapcikTuru == cvp.KitapcikTuru);
                for (int j = 0; j < bransCevap.Length; j++)
                {
                    
                }
                Response.Write(cvp.OpaqId+" "+bransId+" - "+bransCevap+"<br>");
            }
        }

    }

    protected void Button1_OnClick(object sender, EventArgs e)
    {
        string path = string.Format("{0} - {1} Kazanım Karnesi.pdf", "AŞKALE", "");
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

        string sinavAdi = "ÜDS SINAVI";
        int sinavId = 530;
        int kurumKodu = 713789;
        int brans = 1;
        string bransAdi = "Türkçe";
        CkKarneKutukDB kutukDb = new CkKarneKutukDB();

        CkKarneCevapTxtDB cvpTxtDb = new CkKarneCevapTxtDB();
        List<CkKarneCevapTxtInfo> cvpTxtList = cvpTxtDb.KayitlariDizeGetir(sinavId);

        CkKarneDogruCevaplarDB dogruCevapDb = new CkKarneDogruCevaplarDB();
        List<CkKarneDogruCevaplarInfo> dogruCevapList = dogruCevapDb.KayitlariDizeGetir(sinavId);
        CkKarneKazanimlardB kazanimDb = new CkKarneKazanimlardB();
        List<CkKarneKazanimlarInfo> kazanimList = kazanimDb.KayitlariDizeGetir(sinavId);
        CkKarneOgrenciCevaplariDB ckKarneOgrenciDb= new CkKarneOgrenciCevaplariDB();

        List<CkKarneOgrenciCevaplariInfo> ogrenciListesi = ckKarneOgrenciDb.KayitlariDizeGetir(sinavId, kurumKodu).Where(x => x.Sinif == 7 && x.Sube == "A").ToList();

        /*
         * ckogrencicevaplari tablosunda kurumkoduna ait öğrencileri döndür.
         * sonra branşın id değerini bul cevaplarını al
         * 
         *
         */
        foreach (var ocvp in ogrenciListesi)
        {
            List<CkKarneKazanimlarInfo> kazanimlar = kazanimList.Where(x => x.BransId == brans && x.Sinif == 7).ToList();
            CkKarneCevapTxtInfo ogrCevap = cvpTxtList.FirstOrDefault(x => x.BransId == brans && x.OpaqId == ocvp.OpaqId);
            if (ogrCevap != null)
            {
               // CkKarneDogruCevaplarInfo dogruCvplar = dogruCevapList.FirstOrDefault(x => x.BransId == brans && x.KitapcikTuru == ogrCevap.KitapcikTuru);
                string sonSatirBaslik = string.Format("{0} {1}-{2} SINIFI ŞUBE KARNESİ", "okul adı", 7, "A");
                var karneModel = KarneModeliOlustur("AŞKALE", kurumKodu, "OKUL ADI", 7, "A", brans, sinavId, sinavAdi, bransAdi);

                PdfOlustur(doc, sinavAdi, 7, bransAdi, KolonSayisi.Sube.ToInt32(), sonSatirBaslik, karneModel);
                doc.NewPage();
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
    /// İl İlçe Okul Sınıf ve Şube Karneleri için pdf oluşturur
    /// </summary>
    private void PdfOlustur(Document doc, string sinavAdi, int sinif, string dersAdi, int column, string sonSatirBaslik, KarneModel karneModel)
    {

        PdfIslemleri pdf = new PdfIslemleri();

        #region Başlık

        pdf.addParagraph(doc, "ERZURUM  İL MİLLİ EĞİTİM MÜDÜRLÜĞÜ", hizalama: Element.ALIGN_CENTER);
        pdf.addParagraph(doc, "Ölçme Değerlendirme Merkezi", hizalama: Element.ALIGN_CENTER);
        pdf.addParagraph(doc, string.Format("{0} {1}. SINIF {2} DERSİ", sinavAdi.BuyukHarfeCevir(), sinif, dersAdi.BuyukHarfeCevir()), hizalama: Element.ALIGN_CENTER);
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
    private KarneModel KarneModeliOlustur(string ilce, int kurumkodu, string kurumAdi, int sinif, string sube, int brans, int sinavId, string sinavAdi, string bransAdi)
    {
        int destekKazanimSayisi = 0;
        int telafiKazanimSayisi = 0;
        List<TableModel> model = new List<TableModel>();
        List<string> eksikKazanimlar = new List<string>();


        int i = 1;
        CkKarneSonuclariDB ksDb = new CkKarneSonuclariDB();
        CkKarneKazanimlardB kazanimDb = new CkKarneKazanimlardB();
        List<CkKarneKazanimlarInfo> kazanimList = kazanimDb.KayitlariDizeGetir(sinavId);

        List<CkKarneSonuclariInfo> karneSonucList = ksDb.KayitlariDizeGetir(sinavId, ilce, kurumkodu);



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

            model.Add(new TableModel(i.ToString(), kazanim.Sorulari,kazanim.KazanimNo, kazanim.KazanimAdi, ilBasariDurumu.ToString("0"), ilceBasariDurumu.ToString("0"),
                okulBasariDurumu.ToString("0"), subeBasariDurumu.ToString("0"), aciklama));


            i++;
        }

        string raporStr =
            string.Format("{0} {1} testi kazanımlarının {2} {3}-{4} şubesinde edinilme oranları,\n", sinavAdi, bransAdi, kurumAdi, sinif, sube);

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

}