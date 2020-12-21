using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;

public partial class Sinav_Sinav : System.Web.UI.Page
{

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            //CacheHelper.SorulariGetirKaldir(15);
            //TestKutukDb okullarDb = new TestKutukDb();
            //var sonuc = okullarDb.OgrenciBilgiGetir(749788, 11864728);
            //TestOgrenci ogr = new TestOgrenci
            //{
            //    OpaqId = sonuc.OpaqId,
            //    KurumKodu = sonuc.KurumKodu,
            //    Adi = sonuc.Adi,
            //    Soyadi = sonuc.Soyadi,
            //    Sinifi = sonuc.Sinifi
            //};
            //Session["Ogrenci"] = ogr;
            //yukarısı silinecek

            if (Session["Ogrenci"] == null)
            {
                Server.Transfer("Default.aspx");
            }

            TestOgrenci ogrenci = (TestOgrenci)Session["Ogrenci"];

            if (Request.QueryString["t"] != "")
            {
                if (Request.QueryString["t"].IsInteger())
                {
                    //mükerrer oturumu kontrol için
                    if (CacheHelper.KullaniciGirisKontrol(ogrenci.OpaqId) != ogrenci.GirisKey)
                    {
                        Response.Redirect("Default.aspx");
                    }

                    int id = Request.QueryString["t"].ToInt32();
                    TestOturumlarDb oturumlar = new TestOturumlarDb();
                    var oturum = oturumlar.KayitBilgiGetir(id);


                    if (oturum.BaslamaTarihi > GenelIslemler.YerelTarih() || oturum.BitisTarihi.AddMinutes(oturum.Sure) < GenelIslemler.YerelTarih())
                    {
                        Response.Redirect("Sinavlar.aspx");
                    }

                    ltrTestAdi.Text = oturum.BaslamaTarihi.TarihYaz() + " - " + oturum.BitisTarihi.TarihYaz();
                    if (oturum.Sinif == ogrenci.Sinifi && oturum.Aktif == 1)
                    {
                        int soruSayisi = TestSoruSayisi(id);
                        HttpContext.Current.Session["SoruSayisi"] = soruSayisi;
                        ltrTestAdi.Text = oturum.SinavAdi;
                        ltrSoruSayisi.Text = soruSayisi.ToString();
                        ltrSure.Text = oturum.Sure.ToString();
                    }
                    else
                    {
                        Response.Redirect("Sinavlar.aspx");
                    }

                }
                else
                {
                    Response.Redirect("Sinavlar.aspx");
                }
            }
            else
            {
                Response.Redirect("Sinavlar.aspx");
            }
        }
    }

    [WebMethod]
    public static string SinavaBasla(int OturumId)
    {
        JsonSonuc soList;
        if (HttpContext.Current.Session["Ogrenci"] == null)
        {
            soList = new JsonSonuc
            {
                Sonuc = "no",
                Mesaj = "Oturum süreniz dolmuş. Sisteme tekrar giriş yaparak tekrar deneyiniz.",
                KalanDakika = 0,
                KalanSaniye = 0,
                KalanSaat = 0
            };
            return JsonConvert.SerializeObject(soList);
        }

        TestOgrenci ogrenci = (TestOgrenci)HttpContext.Current.Session["Ogrenci"];

        //mükerrer oturumu kontrol için
        if (CacheHelper.KullaniciGirisKontrol(ogrenci.OpaqId) != ogrenci.GirisKey)
        {
            HttpContext.Current.Response.Redirect("Default.aspx");
        }

        int oturumSuresi = TestSuresi(OturumId, true);

        TestOgrCevapDb oturumCevapDb = new TestOgrCevapDb();
        var ogrCevap = oturumCevapDb.KayitBilgiGetir(OturumId, ogrenci.OpaqId);
        if (ogrCevap.Id == 0) //daha önce başlamamış
        {
            int soruSayisi = TestSoruSayisi(OturumId);

            string cevaplar = ""; //soru sayısı kadar boşluk oluştur.
            for (int i = 0; i < soruSayisi; i++)
            {
                cevaplar += " ";
            }

            TestOturumlarDb oturumDb = new TestOturumlarDb();
            TestOturumlarInfo sId = oturumDb.SinavIdGetir(OturumId);


            TestOgrCevapInfo cevapInfo = new TestOgrCevapInfo()
            {
                SinavId = sId.SinavId,
                OturumId = OturumId,
                OpaqId = ogrenci.OpaqId,
                Cevap = cevaplar,
                Dogru = 0,
                Yanlis = 0
            }; //başlama süresi kayıtekle metodu içerisindedir.

            oturumCevapDb.KayitEkle(cevapInfo);

            //##### LOG İŞLEMLERİ
            TestLogInfo logInfo = new TestLogInfo
            {
                OpaqId = ogrenci.OpaqId,
                Grup = "Oturum Başla Butonu - Oturum Id:" + OturumId,
                Rapor = "SinavaBasla metodu. İlk defa başlama gerçekleştirdiğinde çalışan metod"
            };
            TestLogDb logDb = new TestLogDb();
            logDb.KayitEkle(logInfo);
            //##### LOG İŞLEMLERİ

            //ilk başlangıç süresi
            TimeSpan kalanSure = GenelIslemler.YerelTarih().AddMinutes(oturumSuresi).KalanSure();

            soList = new JsonSonuc
            {
                Mesaj = "Başarılı",
                Sonuc = "ok",
                KalanDakika = kalanSure.Minutes,
                KalanSaniye = kalanSure.Seconds,
                KalanSaat = kalanSure.Hours
            };

            return JsonConvert.SerializeObject(soList);
        }
        else //oturumu devam ediyor. kalan süreyi hesapla
        {
            if (ogrCevap.Bitti == SinavDurum.Bitti.ToInt32())
            {
                string mesaj = ogrCevap.Bitis != null
                    ? "Bu oturumu şu tarih-saatte bitirdiniz: " + ogrCevap.Bitis.Value.TarihYaz()
                    : "Bu oturumu bitirdiniz.";
                //##### LOG İŞLEMLERİ
                TestLogInfo logInfo = new TestLogInfo
                {
                    OpaqId = ogrCevap.OpaqId,
                    Grup = "Oturum Başla Butonu Oturum Id:" + OturumId,
                    Rapor = "Oturum daha önceden tamamlanmış SinavDurum.Bitti olarak görünüyor. Uyarı mesajı : " + mesaj
                };
                TestLogDb logDb = new TestLogDb();
                logDb.KayitEkle(logInfo);
                //##### LOG İŞLEMLERİ

                HttpContext.Current.Session["Sure"] = 0;

                soList = new JsonSonuc
                {
                    Mesaj = mesaj,
                    Sonuc = "no",
                    KalanDakika = 0,
                    KalanSaniye = 0,
                    KalanSaat = 0
                };
                return JsonConvert.SerializeObject(soList);
            }
            else
            {
                DateTime baslama = ogrCevap.Baslangic; //ilk başlangıç saati


                if (baslama.AddMinutes(oturumSuresi) < GenelIslemler.YerelTarih())
                {
                    string mesaj = "Bu oturum için süreniz bitmiştir. Başlama :" + baslama.TarihYaz() + " Bitiş : " +
                                   baslama.AddMinutes(oturumSuresi).TarihYaz();
                    //##### LOG İŞLEMLERİ
                    TestLogInfo logInfo = new TestLogInfo
                    {
                        OpaqId = ogrCevap.OpaqId,
                        Grup = "Oturum Başla Butonu Oturum Id:" + OturumId,
                        Rapor = "Oturum süresinin bittiği if(" + baslama.AddMinutes(oturumSuresi) + "<" + GenelIslemler.YerelTarih() + ") kontrolü. Mesaj:" + mesaj
                    };
                    TestLogDb logDb = new TestLogDb();
                    logDb.KayitEkle(logInfo);
                    //##### LOG İŞLEMLERİ

                    HttpContext.Current.Session["Sure"] = 0;

                    soList = new JsonSonuc
                    {
                        Mesaj = mesaj,
                        Sonuc = "no",
                        KalanDakika = 0,
                        KalanSaniye = 0,
                        KalanSaat = 0
                    };
                    return JsonConvert.SerializeObject(soList);
                }
                else
                {
                    //##### LOG İŞLEMLERİ
                    TestLogInfo logInfo = new TestLogInfo
                    {
                        OpaqId = ogrCevap.OpaqId,
                        Grup = "Oturum Başla Butonu Devam Oturum Id:" + OturumId, //tekrar geldi.
                        Rapor = "Öğrenci sınava tekrar geldi.if(" + baslama.AddMinutes(oturumSuresi) + "<" + GenelIslemler.YerelTarih() + ") kontrolü geçerli sınava devam ediyor"
                    };
                    TestLogDb logDb = new TestLogDb();
                    logDb.KayitEkle(logInfo);
                    //##### LOG İŞLEMLERİ

                    HttpContext.Current.Session["Sure"] = oturumSuresi;

                    TimeSpan kalanSure = baslama.AddMinutes(oturumSuresi).KalanSure();

                    soList = new JsonSonuc
                    {
                        Mesaj = "Devam",
                        Sonuc = "ok",
                        KalanDakika = kalanSure.Minutes,
                        KalanSaniye = kalanSure.Seconds,
                        KalanSaat = kalanSure.Hours
                    };
                    return JsonConvert.SerializeObject(soList);
                }

            }
        }
    }

    [WebMethod]
    public static string Cevap(int OturumId, string Cevap, int SoruNo)
    {
        if (HttpContext.Current.Session["Ogrenci"] == null)
        {
            JsonSonuc soList = new JsonSonuc
            {
                Mesaj = "Oturum süreniz dolmuş. Sisteme tekrar giriş yaparak tekrar deneyiniz.",
                Sonuc = "no",
                KalanDakika = 0,
                KalanSaniye = 0,
                KalanSaat = 0
            };
            return JsonConvert.SerializeObject(soList);
        }

        TestOgrenci ogrenci = (TestOgrenci)HttpContext.Current.Session["Ogrenci"];
        //mükerrer oturumu kontrol için
        if (CacheHelper.KullaniciGirisKontrol(ogrenci.OpaqId) != ogrenci.GirisKey)
        {
            HttpContext.Current.Response.Redirect("Default.aspx");
        }

        TestOgrCevapDb oturumCevapDb = new TestOgrCevapDb();
        var sonuc = oturumCevapDb.KayitBilgiGetir(OturumId, ogrenci.OpaqId);
        if (sonuc.Id == 0) //daha önce başlamamış ise
        {
            JsonSonuc soList = new JsonSonuc
            {
                Mesaj = "Bu sınav için henüz başlangıç yapılmamış. Lütfen sınavlar menüsünden yeniden başlayınız.",
                Sonuc = "no",
                KalanDakika = 0,
                KalanSaniye = 0,
                KalanSaat = 0
            };
            return JsonConvert.SerializeObject(soList);
        }
        else
        {
            int oturumSuresi = TestSuresi(OturumId, false);
            DateTime baslama = sonuc.Baslangic; //ilk başlangıç saati
            TimeSpan kalanSure = baslama.AddMinutes(oturumSuresi).KalanSure();

            if (baslama.AddMinutes(oturumSuresi) < GenelIslemler.YerelTarih())
            {
                HttpContext.Current.Session["Sure"] = 0;
                string mesaj = "Cevabınız değerlendirilemedi. Sınav süreniz bitmiştir. Başlama: " + baslama.TarihYaz() +
                               " Bitiş: " + baslama.AddMinutes(oturumSuresi).TarihYaz();
                //##### LOG İŞLEMLERİ
                TestLogInfo logInfo = new TestLogInfo
                {
                    OpaqId = ogrenci.OpaqId,
                    Grup = "Cevap Metodu - Oturum Id:" + OturumId,
                    Rapor = "Süre bitti uyarısı if(" + baslama.AddMinutes(oturumSuresi) + "<" + GenelIslemler.YerelTarih() + ") kontrolü Mesaj:" + mesaj
                };
                TestLogDb logDb = new TestLogDb();
                logDb.KayitEkle(logInfo);
                //##### LOG İŞLEMLERİ

                JsonSonuc soList = new JsonSonuc
                {
                    Mesaj = mesaj,
                    Sonuc = "no",
                    KalanDakika = 0,
                    KalanSaniye = 0,
                    KalanSaat = 0
                };
                return JsonConvert.SerializeObject(soList);
            }
            else
            {
                if (sonuc.Bitti == SinavDurum.DevamEdiyor.ToInt32()) //oturumhiç bitirilmemiş (değerlenmemiş) ise değiştirebilsin
                {
                    string yeniCevap = CevabiIsle(sonuc.Cevap, Cevap, SoruNo);

                    TestOgrCevapInfo cevapInfo = new TestOgrCevapInfo
                    {
                        Cevap = yeniCevap,
                        OturumId = OturumId,
                        OpaqId = ogrenci.OpaqId
                    };
                    oturumCevapDb.CevapGuncelle(cevapInfo);
                    JsonSonuc soList = new JsonSonuc
                    {
                        Mesaj = "Başarılı",
                        Sonuc = "ok",
                        KalanDakika = kalanSure.Minutes,
                        KalanSaniye = kalanSure.Seconds,
                        KalanSaat = kalanSure.Hours
                    };
                    return JsonConvert.SerializeObject(soList);
                }
                else
                {
                    //Bu bölüm değerlendirilmiş bir oturumu tekrar başlattığında olası değişiklikleri engellemek için
                    //Cevap değişmiş mi kontrol edelim.

                    string eskiCevap = sonuc.Cevap.Substring(SoruNo - 1, 1);
                    if (eskiCevap != Cevap) //cevap değiştirilmiş ise
                    {
                        JsonSonuc soList = new JsonSonuc
                        {
                            Mesaj = "Değerlendirilmeyecek.",
                            Sonuc = "es",
                            KalanDakika = kalanSure.Minutes,
                            KalanSaniye = kalanSure.Seconds,
                            KalanSaat = kalanSure.Hours
                        };
                        return JsonConvert.SerializeObject(soList);
                    }
                    else
                    {
                        JsonSonuc soList = new JsonSonuc
                        {
                            Mesaj = "Başarılı",
                            Sonuc = "ok",
                            KalanDakika = kalanSure.Minutes,
                            KalanSaniye = kalanSure.Seconds,
                            KalanSaat = kalanSure.Hours
                        };
                        return JsonConvert.SerializeObject(soList);
                    }
                }
            }
        }
    }

    [WebMethod]
    public static string Bitir(int OturumId, string bitiren)
    {
        if (HttpContext.Current.Session["Ogrenci"] == null)
        {
            JsonSonuc soList = new JsonSonuc
            {
                Mesaj = "Oturum süreniz dolmuş. Sisteme tekrar giriş yaparak deneyiniz.",
                Sonuc = "no",
                KalanDakika = 0,
                KalanSaniye = 0,
                KalanSaat = 0
            };
            return JsonConvert.SerializeObject(soList);
        }
        TestOgrenci ogrenci = (TestOgrenci)HttpContext.Current.Session["Ogrenci"];
        //mükerrer oturumu kontrol için
        if (CacheHelper.KullaniciGirisKontrol(ogrenci.OpaqId) != ogrenci.GirisKey)
        {
            HttpContext.Current.Response.Redirect("Default.aspx");
        }
        TestOgrCevapDb oturumCevapDb = new TestOgrCevapDb();
        TestOgrCevapInfo sonuc = oturumCevapDb.KayitBilgiGetir(OturumId, ogrenci.OpaqId);
        int sinavId = sonuc.SinavId;
        if (sonuc.Id == 0) //daha önce başlamamış ise oturuma başlaması için uyar
        {
            JsonSonuc soList = new JsonSonuc
            {
                Mesaj = "Cevaplarınıza ulaşılamadı. Teste başlayınız.",
                Sonuc = "no",
                KalanDakika = 0,
                KalanSaniye = 0,
                KalanSaat = 0
            };
            return JsonConvert.SerializeObject(soList);
        }
        else
        {
            if (sonuc.Bitti == SinavDurum.DevamEdiyor.ToInt32())//devam ediyor ise bitirme işlemlerini başlat
            {
                //önce sınavın doğru yanlış sayılarını hesapla testogrcevaplar tablosuna işle. diğer hesaplamalar ise tüm oturumları bitmiş ise işlenecek
                //Oturumları listele ve bu öğrenci tüm oturumlara katılmış mı kontrol et
                //oturumları tamamlamış ise öğrenci okul ve ilçe için hesaplama yap ve sonuç ekranına yönlendir.
                //yoksa oturumları listeleyecek sayfaya yönlendir.

                //önce bu oturumdaki öğrencinin testogrcevaplar tablosundaki doğru yanlış sayılarını hesapla
                SinavPuanlamaIslemleri puanlama = new SinavPuanlamaIslemleri();
                puanlama.OgrenciCevaplariniIsle(OturumId, sonuc, ogrenci.OpaqId);

                //##### LOG İŞLEMLERİ
                TestLogInfo logInfo = new TestLogInfo
                {
                    OpaqId = sonuc.OpaqId,
                    Grup = "Oturum Bitti  Oturum Id:" + OturumId,
                    Rapor = "Bitir metodu if (sonuc.Bitti == SinavDurum.DevamEdiyor.ToInt32()) OgrenciCevaplariniIsle metodu çalışıp sınav bitirildi. Bitiren:" + bitiren
                };
                TestLogDb logDb = new TestLogDb();
                logDb.KayitEkle(logInfo);
                //##### LOG İŞLEMLERİ

                //Oturumları listele ve bu öğrenci tüm oturumlara katılmış mı kontrol et

                //oturumları listele
                TestOturumlarDb oturumlarDb = new TestOturumlarDb();
                List<TestOturumlarInfo> oturumlar = oturumlarDb.Oturumlar(sinavId);

                TestOgrCevapDb ogrCevapDb = new TestOgrCevapDb();
                List<TestOgrCevapInfo> ogrCevaplari = ogrCevapDb.KayitlariDiziyeGetir(ogrenci.OpaqId);
                int kalanOturumSayisi = 0; //girmediği oturumları kontrol için gerekli

                foreach (TestOturumlarInfo o in oturumlar)
                {
                    var res = ogrCevaplari.Count(x => x.OturumId == o.Id && (x.Bitti == SinavDurum.Bitti.ToInt32() || x.Baslangic.AddMinutes(o.Sure) <= GenelIslemler.YerelTarih()));//bitiş süresi şuandan küçük ise
                    //sınavı tamamlanmış
                    if (res == 0)
                    {
                        kalanOturumSayisi++;//sınava girmediği oturumu var.
                    }
                }

                //kalanOturumSayisi otrum sayısı 0dan büyükse oturumu vardır
                if (kalanOturumSayisi > 0)
                {
                    JsonSonuc soList = new JsonSonuc
                    {
                        Mesaj = "Bu oturumu başarıyla tamamladınız. Bu sınava ait varsa diğer oturumları da tamamlayınız.",
                        Sonuc = "ok",
                        KalanDakika = 0,
                        KalanSaniye = 0,
                        KalanSaat = 0
                    };
                    return JsonConvert.SerializeObject(soList);
                }
                else
                {
                    //oturumları tamamlamış. öğrenci okul ve ilçe için hesaplama yap ve sonuç ekranına yönlendir.

                    //sonuçları okul puanı dizisine al.
                    List<TestOkulCevapInfo> okulPuani = new List<TestOkulCevapInfo>();

                    foreach (TestOturumlarInfo otrm in oturumlar)
                    {
                        var res = ogrCevaplari.FirstOrDefault(x => x.OturumId == otrm.Id);
                        if (res != null)
                        {
                            var ogrCevabi = ogrCevaplari.FirstOrDefault(x => x.OturumId == otrm.Id).Cevap;
                            var cevaplar = CacheHelper.SorulariGetir(otrm.Id);

                            var buSinavdakiBranslar = cevaplar.GroupBy(x => x.BransId).Select(x => x.First())
                                .OrderBy(x => x.BransId).ToList();

                            //  int toplamKatsayiPuani = 0;
                            foreach (var b in buSinavdakiBranslar)
                            {
                                //okul puanını hesaplamak için öncelikle branşı dizieye ekleyelim.
                                okulPuani.Add(new TestOkulCevapInfo(b.BransId, 0, 0, 0, 0));
                                var yeniPuan = okulPuani.Find(x => x.BransId == b.BransId);

                                foreach (var c in cevaplar.Where(x => x.BransId == b.BransId))
                                {
                                    if (ogrCevabi.Substring(c.SoruNo - 1, 1) == " ")
                                    {
                                        yeniPuan.Bos += 1;
                                    }
                                    else if (c.Cevap == ogrCevabi.Substring(c.SoruNo - 1, 1))
                                    {
                                        yeniPuan.Dogru += 1;
                                    }
                                    else
                                    {
                                        yeniPuan.Yanlis += 1;
                                    }
                                }
                            }

                        }
                    }

                    BranslarDb branslarDb = new BranslarDb();
                    var katSayi = branslarDb.KayitlariDiziyeGetir();

                    int toplamKatsayiPuani = 0;//öğrencinin puanı için bu kat sayı gerekli
                    decimal toplamBransPuan = 0;
                    //Bu döngüde branşlar kadar döüşüm olacaktır. Çünkü dizeye branşa göre doğru yanlış sayıları işlendi.

                    TestOkulPuanDb okulPuanDb = new TestOkulPuanDb();
                    var okulPuaniList = okulPuanDb.KayitlariDiziyeGetir(sinavId, ogrenci.KurumKodu);

                    int dogruYanlisOrani = ogrenci.Sinifi >= 9 ? 4 : 3;

                    foreach (var pInfo in okulPuani)
                    {
                        var bransKatSayi = katSayi.FirstOrDefault(x => x.Id == pInfo.BransId).KatSayi;

                        decimal net = (pInfo.Dogru - ((decimal)pInfo.Yanlis / dogruYanlisOrani));
                        toplamBransPuan += net * bransKatSayi;
                        toplamKatsayiPuani += (pInfo.Bos + pInfo.Dogru + pInfo.Yanlis) * bransKatSayi;

                        int okulDogruSayisi = pInfo.Dogru;
                        int okulYanlisSayisi = pInfo.Yanlis;
                        int okulBosSayisi = pInfo.Bos;

                        var okulPuaniBrans = okulPuaniList.FirstOrDefault(x => x.BransId == pInfo.BransId);

                        if (okulPuaniBrans != null) //kayıt varsa güncelle
                        {
                            okulDogruSayisi += okulPuaniBrans.Dogru;
                            okulYanlisSayisi += okulPuaniBrans.Yanlis;
                            okulBosSayisi += okulPuaniBrans.Bos;

                            TestOkulPuanInfo pinfo = new TestOkulPuanInfo()
                            {
                                BransId = pInfo.BransId,
                                KurumKodu = ogrenci.KurumKodu,
                                SinavId = sinavId,
                                Dogru = okulDogruSayisi,
                                Yanlis = okulYanlisSayisi,
                                Bos = okulBosSayisi
                            };
                            okulPuanDb.DYBGuncelle(pinfo);
                        }
                        else //kayıt yoksa ekle
                        {
                            TestOkulPuanInfo pinfo = new TestOkulPuanInfo()
                            {
                                BransId = pInfo.BransId,
                                KurumKodu = ogrenci.KurumKodu,
                                SinavId = sinavId,
                                Dogru = okulDogruSayisi,
                                Yanlis = okulYanlisSayisi,
                                Bos = okulBosSayisi
                            };
                            okulPuanDb.KayitEkle(pinfo);
                        }
                    }

                    if (okulPuani.Count > 0)
                    {
                        puanlama.OgrPuanTablosunaKayitIslemleri(okulPuani, toplamBransPuan, toplamKatsayiPuani, ogrenci, sinavId);

                        puanlama.IlcePuanHesaplama(sinavId, ogrenci, katSayi, dogruYanlisOrani, okulPuani);
                    }


                    JsonSonuc soList = new JsonSonuc
                    {
                        Mesaj = "Değerlendirme bitti",
                        Sonuc = "ok2",
                        KalanDakika = 0,
                        KalanSaniye = 0,
                        KalanSaat = 0
                    };
                    return JsonConvert.SerializeObject(soList);
                }
            }
            else //sınavı bitti alanı işaretli ise
            {
                string mesaj = "Bu sınav için daha önce bir değerlendirme yapılmış. Yeniden değerlendirilemez.";
                //##### LOG İŞLEMLERİ
                TestLogInfo logInfo = new TestLogInfo
                {
                    OpaqId = sonuc.OpaqId,
                    Grup = "Oturum Bitti  Oturum Id:" + OturumId,
                    Rapor = "Bitiren:" + bitiren + ". Bitir metodu if (sonuc.Bitti == SinavDurum.DevamEdiyor.ToInt32()) else metodu mesaj:" + mesaj
                };
                TestLogDb logDb = new TestLogDb();
                logDb.KayitEkle(logInfo);
                //##### LOG İŞLEMLERİ

                JsonSonuc soList = new JsonSonuc
                {
                    Mesaj = mesaj,
                    Sonuc = "no",
                    KalanDakika = 0,
                    KalanSaniye = 0,
                    KalanSaat = 0
                };
                return JsonConvert.SerializeObject(soList);
            }
        }
    }

  
    [WebMethod]
    public static string BransIlkSorusu(int OturumId, int BransId)
    {
        if (HttpContext.Current.Session["Ogrenci"] == null)
        {
            JsonSonuc soList = new JsonSonuc
            {
                Mesaj = "Oturum süreniz dolmuş. Sisteme tekrar giriş yaparak tekrar deneyiniz.",
                Sonuc = "no",
                KalanDakika = 0,
                KalanSaniye = 0,
                KalanSaat = 0
            };
            return JsonConvert.SerializeObject(soList);
        }

        TestSorularDb sorularDb = new TestSorularDb();
        var sonuc = sorularDb.IlkSoruyuGetir(OturumId, BransId);
        if (sonuc == 0) //daha önce başlamamış ise oturume başlaması için uyar
        {
            JsonSonuc soList = new JsonSonuc
            {
                Mesaj = "Dersin sorularına ulaşılamadı.",
                Sonuc = "no",
            };
            return JsonConvert.SerializeObject(soList);
        }
        else
        {
            JsonSonuc soList = new JsonSonuc
            {
                Mesaj = sonuc.ToString(),
                Sonuc = "ok",
            };
            return JsonConvert.SerializeObject(soList);
        }
    }

    private static int TestSuresi(int oturumId, bool noSession)
    {
        if (noSession)
        {
            HttpContext.Current.Session["Sure"] = null;
        }
        int oturumSuresi;
        if (HttpContext.Current.Session["Sure"] == null)
        {
            TestOturumlarDb oturumDb = new TestOturumlarDb();
            var oturumInfo = oturumDb.KayitBilgiGetir(oturumId);
            oturumSuresi = oturumInfo.Sure;
        }
        else
        {
            oturumSuresi = HttpContext.Current.Session["Sure"].ToInt32();
        }

        return oturumSuresi;
    }

    private static string CevabiIsle(string metin, string cevap, int soruNo)
    {
        string yeniMetin = "";
        for (int i = 1; i <= metin.Length; i++)
        {
            if (i == soruNo)
            {
                yeniMetin += cevap;
            }
            else
            {
                yeniMetin += metin.Substring(i - 1, 1);
            }
        }

        return yeniMetin;
    }

    private static int TestSoruSayisi(int oturumId)
    {
        TestSorularDb sorularDb = new TestSorularDb();
        int soruSayisi = sorularDb.SoruSayisi(oturumId);
        HttpContext.Current.Session["SoruSayisi"] = soruSayisi;

        return soruSayisi;
    }
}
