using ErzurumOdmMvc.Areas.ODM.Model;
using ErzurumOdmMvc.Business.CkKarne;
using ErzurumOdmMvc.CKKarneModel;
using ErzurumOdmMvc.Common.Library;
using ErzurumOdmMvc.Entities.CKKarne;
using ErzurumOdmMvc.Filter;
using ErzurumOdmMvc.Library;
using iTextSharp.text;
using iTextSharp.text.pdf;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ErzurumOdmMvc.Business;
using ErzurumOdmMvc.Common.Enums;

namespace ErzurumOdmMvc.Areas.ODM.Controllers
{
    [YetkiKontrol(Roles = "Admin,Root,LgsIlKomisyonu,IlceMEMYetkilisi,OkulYetkilisi")]
    public class KazanimKarneController : Controller
    {
        public ActionResult Index()
        {
            var ckKurumlar = new List<CkKarneKutuk>();
            CkSinavAdiManager sinavlar = new CkSinavAdiManager();

            KazanimKarneViewModel model = new KazanimKarneViewModel
            {
                CkKurumlar = ckKurumlar,
                CkSinavlar = sinavlar.List()
            };
            if (Session["ilceadi"] == null)
            {
                IlceManager ilceManager = new IlceManager();
                var ilce = ilceManager.Find(CurrentSession.Kullanici.IlceId);
                Session["ilceadi"] = ilce.IlceAdi;
            }

            return View(model);
        }

        [HttpPost]
        public ActionResult Index(FormCollection frm)
        {
            int sinavId = frm["sinavlar"].ToInt32();
            string ilceAdi = frm["ilceler"];
            int sinif = frm["siniflar"].ToInt32();
            CkKarneKutukManager kutukManager = new CkKarneKutukManager();
            var ckKurumlar = kutukManager.SinavKurumlari(sinavId, ilceAdi, sinif).Result;

            CkSinavAdiManager sinavlar = new CkSinavAdiManager();

            KazanimKarneViewModel model = new KazanimKarneViewModel
            {
                CkKurumlar = ckKurumlar,
                CkSinavlar = sinavlar.List()
            };

            return View(model);
        }
        [YetkiKontrol(Roles = "Admin,Root,LgsIlKomisyonu")]
        public void IlKarnesi(int sinavId)
        {
            string yetki = CurrentSession.Kullanici.Yetki;
            bool goster;

            if (yetki.Contains(KullaniciSeviye.Root.ToString()) || yetki.Contains(KullaniciSeviye.Admin.ToString()) || yetki.Contains(KullaniciSeviye.LgsIlKomisyonu.ToString()))
                goster = true;
            else
               goster = false;
            
            if (goster)
            {
                // List<CkKarneBranslar> branslar = CacheHelper.CkBranslarFromChache(sinavId).ToList();
                List<CkIlIlceOrtalamasi> basariYuzdesi = CacheHelper.CkIlIlceOrtalamalariFromChache(sinavId);

                if (basariYuzdesi.Count > 0)
                {
                    TekSayfaliKarne tekSayfaliKarne = new TekSayfaliKarne();

                    List<CkKarneKazanimlar> kazanimList = CacheHelper.CkKazanimlarFromChache(sinavId);
                    CkSinavAdi sinav = CacheHelper.CkSinavlarFromChache().FirstOrDefault(x => x.SinavId == sinavId);

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


                    List<CkIlIlceOrtalamasi> branslar = basariYuzdesi.GroupBy(x => x.BransId).Select(x => x.First()).OrderBy(x => x.BransId).ToList();

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
                        var user = CurrentSession.Kullanici;

                        CkKarneLogManager logDb = new CkKarneLogManager();
                        CkKarneLog kontrol = logDb.KullaniciLog(user.Id, sinavId, user.KurumKodu).Result;

                        CkKarneLog infoLog = new CkKarneLog
                        {
                            SinavId = sinavId,
                            KullaniciId = user.Id,
                            KurumKodu = user.KurumKodu,
                            Tarih = DateTime.Now,
                            Sinif = 0,
                            Brans = 0,
                            Aciklama = "İl Karnesi " + user.AdiSoyadi + " - " + user.TcKimlik + " - " + user.Yetki
                        };

                        if (kontrol.Id == 0)
                        {
                            infoLog.Say = 1;

                            logDb.InsertAsync(infoLog);
                        }
                        else
                        {
                            infoLog.Say = kontrol.Say + 1;
                            infoLog.Id = kontrol.Id;
                            logDb.UpdateAsync(infoLog);
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
                    Response.Write("Kayıt bulunamadı.");
                }
            }
            else
            {
                Response.Redirect("/ODM/Yonetim/Yetkisiz");
            }
        }

        [YetkiKontrol(Roles = "Admin,Root,IlceMEMYetkilisi")]
        public void IlceKarnesi(int sinavId, string ilceAdi)
        {
            string yetki = CurrentSession.Kullanici.Yetki;
            bool goster = true;

            //Kendi ilçesi değil ise gösterme kontrolü
            if (yetki.Contains(KullaniciSeviye.IlceMEMYetkilisi.ToString()))
            {
                if (ilceAdi != Session["ilceadi"].ToString())
                {
                    goster = false;
                }
            }
            //Kendi ilçesi değil  fakat Admin veya Root ise göster
            if (yetki.Contains(KullaniciSeviye.Root.ToString())|| yetki.Contains(KullaniciSeviye.Admin.ToString()))
            {
                goster = true;
            }
            if (goster)
            {
                // List<CkKarneBranslar> branslar = CacheHelper.CkBranslarFromChache(sinavId).ToList();
                List<CkIlIlceOrtalamasi> basariYuzdesi = CacheHelper.CkIlIlceOrtalamalariFromChache(sinavId);

                if (basariYuzdesi.Count > 0)
                {
                    TekSayfaliKarne tekSayfaliKarne = new TekSayfaliKarne();

                    List<CkKarneKazanimlar> kazanimList = CacheHelper.CkKazanimlarFromChache(sinavId);
                    CkSinavAdi sinav = CacheHelper.CkSinavlarFromChache().FirstOrDefault(x => x.SinavId == sinavId);

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


                    List<CkIlIlceOrtalamasi> branslar = basariYuzdesi.GroupBy(x => x.BransId).Select(x => x.First()).OrderBy(x => x.BransId).ToList();

                    var siniflar = basariYuzdesi.Where(x => x.SinavId == sinavId).GroupBy(x => x.Sinif).Select(x => x.First()).OrderBy(x => x.Sinif).ToList();
                    foreach (CkIlIlceOrtalamasi sinif in siniflar)
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
                        var user = CurrentSession.Kullanici;

                        CkKarneLogManager logDb = new CkKarneLogManager();
                        CkKarneLog kontrol = logDb.KullaniciLog(user.Id, sinavId, user.KurumKodu).Result;

                        CkKarneLog infoLog = new CkKarneLog
                        {
                            SinavId = sinavId,
                            KullaniciId = user.Id,
                            KurumKodu = user.KurumKodu,
                            Tarih = DateTime.Now,
                            Sinif = 0,
                            Brans = 0,
                            Aciklama = "İlçe Karnesi " + user.AdiSoyadi + " - " + user.TcKimlik + " - " + user.Yetki
                        };

                        if (kontrol.Id == 0)
                        {
                            infoLog.Say = 1;

                            logDb.InsertAsync(infoLog);
                        }
                        else
                        {
                            infoLog.Say = kontrol.Say + 1;
                            infoLog.Id = kontrol.Id;
                            logDb.UpdateAsync(infoLog);
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
                    Response.Write("Kayıt bulunamadı.");
                }
            }
            else
            {
                Response.Redirect("/ODM/Yonetim/Yetkisiz");
            }
        }
        public void OkulKarnesi(int sinavId, int kurumKodu, int sinifi)
        {
            OkulicinBerlesikDersliKarneler(sinavId, kurumKodu, sinifi, "okul");
        }

        public void OgrenciKarnesi(int sinavId, int kurumKodu, int sinifi)
        {
            OkulicinBerlesikDersliKarneler(sinavId, kurumKodu, sinifi, "ogrenci");
        }
        private CkKarneBranslar DersAdi(int sinavId, int bransId)
        {
            CkKarneBranslarManager brnsDb = new CkKarneBranslarManager();
            CkKarneBranslar brans = brnsDb.BransBilgisi(sinavId, bransId);
            return brans;
        }
        /// <summary>
        /// Bir sayfada briden fazla ders olan karneler
        /// </summary>
        /// <param name="sinavId"></param>
        /// <param name="kurumKodu"></param>
        /// <param name="sinif"></param>
        /// <param name="karne"></param>
        private void OkulicinBerlesikDersliKarneler(int sinavId, int kurumKodu, int sinif, string karne)
        {
            int s = 0;
            CkKarneKutukManager kutukDb = new CkKarneKutukManager();
            List<CkKarneKutuk> ogrenciListesi = kutukDb.SinavOkulListesi(sinavId, kurumKodu, sinif).Result.ToList();

            TekSayfaliKarne tekSayfaliKarne = new TekSayfaliKarne();

            string ilceAdi = ogrenciListesi[0].IlceAdi;
            string okulAdi = ogrenciListesi[0].KurumAdi;

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

            CkSinavAdi sinav = CacheHelper.CkSinavlarFromChache().FirstOrDefault(x => x.SinavId == sinavId);


            CkKarneSonuclariManager ksDb = new CkKarneSonuclariManager();
            List<CkKarneSonuclari> karneSonucList = ksDb.KarneSonuclari(sinavId, kurumKodu).Result.ToList();

            List<CkKarneKazanimlar> kazanimList = CacheHelper.CkKazanimlarFromChache(sinavId);
            List<CkKarneBranslar> branslar = CacheHelper.CkBranslarFromChache(sinavId).ToList();
            List<CkIlIlceOrtalamasi> basariYuzdesi = CacheHelper.CkIlIlceOrtalamalariFromChache(sinavId);

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

                    s++;
                    doc.NewPage();


                    List<CkKarneSonuclari> subeler = karneSonucList.Where(x => x.Sinif == sinif).GroupBy(x => x.Sube).Select(x => x.First()).OrderBy(x => x.Sube).ToList();

                    if (subeler.Count > 1)
                    {
                        foreach (var sube in subeler)
                        {
                            //ŞUBE KARNESİ

                            string sonSatirBaslikSube = string.Format("{0} {1}-{2} SINIFI ŞUBE KARNESİ", okulAdi.BuyukHarfeCevir(),
                                sinif, sube.Sube);
                            var karneModelSube = tekSayfaliKarne.KarneModeliOlusturSube(ilceAdi, kurumKodu, okulAdi, sinif, sube.Sube, brans.BransId, sinav, brans.BransAdi, okulAdi, kazanimList, karneSonucList, basariYuzdesi);

                            tekSayfaliKarne.PdfOlustur(doc, sinav, sinif, brans.BransAdi, TekSayfaliKarne.KolonSayisi.Sube.ToInt32(), sonSatirBaslikSube, karneModelSube);

                            s++;
                            doc.NewPage();
                        }
                    }
                }
                //}
            }

            if (karne == "ogrenci") //öğrenci karneleri
            {

                List<CkKarneSonuclari> subelerOgr = karneSonucList.Where(x => x.Sinif == sinif)
                    .GroupBy(x => x.Sube).Select(x => x.First()).OrderBy(x => x.Sube).ToList();

                foreach (var sube in subelerOgr)
                {
                    //ÖĞRENCİ KARNESİ
                    ogrenciListesi = ogrenciListesi.Where(x => x.Sinifi == sinif && x.Sube == sube.Sube).ToList();


                    List<CkKarneDogruCevaplar> dogruCevapList = CacheHelper.DogruCevaplarFromChache(sinavId);

                    PdfIslemleri pdf = new PdfIslemleri();

                    foreach (var kutuk in ogrenciListesi)
                    {
                        string[] kTuruList = kutuk.Cevaplar.Split('#'); //cevaplarda ikinci bölüm kitapçık türünü ifade eder
                        List<CevaplarModel> ogrCevaplarList = new List<CevaplarModel>();
                        for (int i = 0; i < kTuruList.Length; i += 3)
                        {
                            //if (i == 2 || i % 3 == 2) //her ikinci bölüm kitapçık türünü ifade eder.
                            //{
                            //    if (!kTuru.Contains(kTuruList[i]))
                            //        kTuru = kTuruList[i] + " ";
                            //}
                            ogrCevaplarList.Add(new CevaplarModel(kTuruList[i].ToInt32(), kTuruList[i + 1], kTuruList[i + 2]));

                        }
                        if (ogrCevaplarList.DistinctBy(x => x.KitapcikTuru).Count() > 0)
                        {
                            string kTuru = "";
                            foreach (var kt in ogrCevaplarList.DistinctBy(x => x.KitapcikTuru))
                            {
                                kTuru += kt.KitapcikTuru + " ";
                            }
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
                                pdf.addCell(tableOgrenciBilgileri, string.Format("{0} {1} (Kitapçık Türü: {2})", kutuk.Adi, kutuk.Soyadi, kTuru));

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
                                    var kitapcikTuru = ogrCevaplarList.FirstOrDefault(x => x.BransId == brans.BransId);
                                    CkKarneDogruCevaplar dogruCvplar = dogruCevapList.FirstOrDefault(x => x.Sinif == sinif && x.BransId == brans.BransId && x.KitapcikTuru == kitapcikTuru.KitapcikTuru);

                                    if (dogruCvplar != null)  //Sınavda sorulmayan dersleri ayırmak için suni kontrol
                                    {
                                        List<CkKarneKazanimlar> kazanimlar = kazanimList.Where(x => x.BransId == brans.BransId && x.Sinif == sinif).ToList();


                                        string ogrenciCevabi = "";
                                        string[] cevapBilgisi = kutuk.Cevaplar.Split('#');
                                        for (int t = 0; t < cevapBilgisi.Length - 1; t += 3)
                                        {
                                            int dersKodu = cevapBilgisi[t].ToInt32();
                                            if (dersKodu == brans.BransId)
                                                ogrenciCevabi = cevapBilgisi[t + 2];
                                        }

                                        
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
                                            pdf.addCell(tableKazanimlar, DersAdi(sinavId, brans.BransId).BransAdi + " Dersi Kazanımları (" + kitapcikTuru.KitapcikTuru + " kitapçığı)", bgColor: PdfIslemleri.Renkler.Gri, hizalama: Element.ALIGN_CENTER,
                                                fontStyle: Font.BOLD);
                                        }
                                        else
                                        {
                                            pdf.addCell(tableKazanimlar, DersAdi(sinavId, brans.BransId).BransAdi + " Dersi Konuları (" + kitapcikTuru.KitapcikTuru + " kitapçığı)", bgColor: PdfIslemleri.Renkler.Gri, hizalama: Element.ALIGN_CENTER,
                                                fontStyle: Font.BOLD);
                                        }


                                        int soruSayisi = dogruCvplar.Cevaplar.Length;

                                        for (int i = 1; i <= soruSayisi; i++)
                                        {
                                            string kazanimAdi = "";


                                            string soruSql = kitapcikTuru.KitapcikTuru + i + ",";
                                            CkKarneKazanimlar kazanim = kazanimlar.Find(x => x.Sorulari.Contains(soruSql));

                                            if (kazanim != null)
                                            {
                                                kazanimAdi = kazanim.KazanimAdiOgrenci == "" ? kazanim.KazanimAdi : kazanim.KazanimAdiOgrenci;
                                            }


                                            pdf.addCell(tableKazanimlar, i.ToString(), fontSize: 6, hizalama: Element.ALIGN_CENTER);
                                            pdf.addCell(tableKazanimlar, dogruCvplar.Cevaplar.Substring(i - 1, 1), fontSize: 6, hizalama: Element.ALIGN_CENTER);
                                            pdf.addCell(tableKazanimlar, ogrenciCevabi.Substring(i - 1, 1), fontSize: 6, hizalama: Element.ALIGN_CENTER);
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

                                s++;
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
                var user = CurrentSession.Kullanici;

                CkKarneLogManager logDb = new CkKarneLogManager();
                CkKarneLog kontrol = logDb.KullaniciLog(user.Id, sinavId, kurumKodu, sinif, 0).Result;

                CkKarneLog infoLog = new CkKarneLog
                {
                    SinavId = sinavId,
                    KullaniciId = user.Id,
                    KurumKodu = kurumKodu,
                    Tarih = DateTime.Now,
                    Sinif = sinif,
                    Brans = 0,
                    Aciklama = karne + " Karnesi" + user.AdiSoyadi + " - " + user.TcKimlik + " - " + user.Yetki
                };

                if (kontrol.Id == 0)
                {
                    infoLog.Say = 1;

                    logDb.InsertAsync(infoLog);
                }
                else
                {
                    infoLog.Say = kontrol.Say + 1;
                    infoLog.Id = kontrol.Id;
                    logDb.UpdateAsync(infoLog);
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
        ///  Her ders için bir sayfa ayarlanmış karneler.
        /// </summary>
        /// <param name="sinavId"></param>
        /// <param name="kurumKodu"></param>
        /// <param name="sinif"></param>
        private void OkulicinTekSayfaliKarneler(int sinavId, int kurumKodu, int sinif)
        {
            CkKarneKutukManager kutukDb = new CkKarneKutukManager();
            List<CkKarneKutuk> ogrenciListesi = kutukDb.SinavOkulListesi(sinavId, kurumKodu, sinif).Result.ToList();

            TekSayfaliKarne tekSayfaliKarne = new TekSayfaliKarne();

            string ilceAdi = ogrenciListesi[0].IlceAdi;
            string okulAdi = ogrenciListesi[0].KurumAdi;

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


            CkKarneSonuclariManager ksDb = new CkKarneSonuclariManager();
            List<CkKarneSonuclari> karneSonucList = ksDb.KarneSonuclari(sinavId, kurumKodu).Result.ToList();

            List<CkKarneKazanimlar> kazanimList = CacheHelper.CkKazanimlarFromChache(sinavId);
            List<CkIlIlceOrtalamasi> basariYuzdesi = CacheHelper.CkIlIlceOrtalamalariFromChache(sinavId);

            CkSinavAdi sinav = CacheHelper.CkSinavlarFromChache().FirstOrDefault(x => x.SinavId == sinavId);

            List<CkKarneDogruCevaplar> dogruCevapList = CacheHelper.DogruCevaplarFromChache(sinavId);



            string sonSatirBaslik = string.Format("{0} {1}. SINIF OKUL KARNESİ", okulAdi.BuyukHarfeCevir(), sinif);
            KarneModel karneModel =
                tekSayfaliKarne.KarneModeliOlusturOkul(ilceAdi, okulAdi, sinif, brans, sinav, bransAdi, kazanimList, karneSonucList, basariYuzdesi);
            //OKUL KARNESİ
            tekSayfaliKarne.PdfOlustur(doc, sinav, sinif, bransAdi, TekSayfaliKarne.KolonSayisi.Okul.ToInt32(),
                sonSatirBaslik, karneModel);

            doc.NewPage();


            List<CkKarneSonuclari> subeler = karneSonucList.Where(x => x.KurumKodu == kurumKodu && x.Sinif == sinif)
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

            List<CkKarneSonuclari> subelerOgr = karneSonucList.Where(x => x.KurumKodu == kurumKodu && x.Sinif == sinif)
                .GroupBy(x => x.Sube).Select(x => x.First()).OrderBy(x => x.Sube).ToList();

            foreach (var sube in subelerOgr)
            {
                //ÖĞRENCİ KARNESİ

                List<CkKarneKazanimlar>
                    kazanimlar = kazanimList.Where(x => x.BransId == brans && x.Sinif == sinif).ToList();

                ogrenciListesi = ogrenciListesi.Where(x => x.Sube == sube.Sube).ToList();

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

                        CkKarneDogruCevaplar dogruCvplar = dogruCevapList.FirstOrDefault(x =>
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
                var user = CurrentSession.Kullanici;

                CkKarneLogManager logDb = new CkKarneLogManager();
                CkKarneLog kontrol = logDb.KullaniciLog(user.Id, sinavId, kurumKodu, sinif, brans).Result;


                CkKarneLog infoLog = new CkKarneLog
                {
                    SinavId = sinavId,
                    KullaniciId = user.Id,
                    KurumKodu = kurumKodu,
                    Tarih = DateTime.Now,
                    Sinif = sinif,
                    Brans = brans,
                    Aciklama = user.AdiSoyadi + " - " + user.TcKimlik + " - " + user.Yetki
                };

                if (kontrol.Id == 0)
                {
                    infoLog.Say = 1;

                    logDb.InsertAsync(infoLog);
                }
                else
                {
                    infoLog.Say = kontrol.Say + 1;
                    infoLog.Id = kontrol.Id;
                    logDb.UpdateAsync(infoLog);
                }
            }
            catch (Exception)
            {
                //
            }

            //Son aşama da işlemleri bitirip, ekran çıktısına ulaşıyoruz.
            Response.End();
        }
    }
}