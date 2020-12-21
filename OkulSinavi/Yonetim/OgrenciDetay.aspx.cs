using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Services;
using System.Web.UI.WebControls;

public partial class Yonetim_OgrenciDetay : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            OturumIslemleri oturum = new OturumIslemleri();
            KullanicilarInfo kInfo = oturum.OturumKontrol();

            if (!kInfo.Yetki.Contains("Root") && !kInfo.Yetki.Contains("Admin") && !kInfo.Yetki.Contains("Ogretmen"))
            {
                Response.Redirect("Default.aspx");
            }

            if (Request.QueryString["OpaqId"] != null)
            {

                string opaqId = Request.QueryString["OpaqId"];

                TestKutukDb testKutukDb = new TestKutukDb();
                TestKutukInfo kutukInfo = testKutukDb.KayitBilgiGetir(opaqId);

                if (kInfo.KurumKodu.ToInt32() != kutukInfo.KurumKodu && !kInfo.Yetki.Contains("Root"))
                {
                    Response.Redirect("Default.aspx");
                }

                ltrOgrenciAdiSoyadi.Text = kutukInfo.Adi.IlkHarfleriBuyut() + " " + kutukInfo.Soyadi.IlkHarfleriBuyut();


                TestSinavlarDb veriDb = new TestSinavlarDb();
                ddlSinavlar.DataSource = veriDb.KayitlariGetir(kutukInfo.Sinifi, kutukInfo.KurumKodu.ToString());
                ddlSinavlar.DataTextField = "SinavAdi";
                ddlSinavlar.DataValueField = "Id";
                ddlSinavlar.DataBind();
                ddlSinavlar.Items.Insert(0, new ListItem("Sınav Seçiniz", ""));

                ddlSinavlar.Attributes.Add("onchange", "Sinav()");

            }
        }
    }


    [WebMethod]
    public static string Bitir(int OturumId,string opaqId)
    {
        OturumIslemleri oturum = new OturumIslemleri();
        KullanicilarInfo kInfo = oturum.OturumKontrol();


        TestOgrCevapDb oturumCevapDb = new TestOgrCevapDb();
        TestOgrCevapInfo sonuc = oturumCevapDb.KayitBilgiGetir(OturumId, opaqId);
        int sinavId = sonuc.SinavId;
        if (sonuc.Id == 0) //daha önce başlamamış ise oturuma başlaması için uyar
        {
            JsonSonuc soList = new JsonSonuc
            {
                Mesaj = "Öğrencinin cevaplarına ulaşılamadı.",
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
                puanlama.OgrenciCevaplariniIsle(OturumId, sonuc, opaqId);

                //##### LOG İŞLEMLERİ
                TestLogInfo logInfo = new TestLogInfo
                {
                    OpaqId = sonuc.OpaqId,
                    Grup = "Oturum Bitti  Oturum Id:" + OturumId,
                    Rapor = "Bitir metodu if (sonuc.Bitti == SinavDurum.DevamEdiyor.ToInt32()) OgrenciCevaplariniIsle metodu çalışıp sınav bitirildi. Bitiren: Yönetici" + kInfo.AdiSoyadi
                };
                TestLogDb logDb = new TestLogDb();
                logDb.KayitEkle(logInfo);
                //##### LOG İŞLEMLERİ

                //Oturumları listele ve bu öğrenci tüm oturumlara katılmış mı kontrol et

                //oturumları listele
                TestOturumlarDb oturumlarDb = new TestOturumlarDb();
                List<TestOturumlarInfo> oturumlar = oturumlarDb.Oturumlar(sinavId);

                TestOgrCevapDb ogrCevapDb = new TestOgrCevapDb();
                List<TestOgrCevapInfo> ogrCevaplari = ogrCevapDb.KayitlariDiziyeGetir(opaqId);
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
                        Mesaj = "Bu oturumu Tamamlandı",
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
                    TestKutukDb testKutukDb = new TestKutukDb();
                    TestKutukInfo ogrenci = testKutukDb.KayitBilgiGetir(opaqId);
                    TestOgrenci ogrenciTest = new TestOgrenci()
                    {
                        KurumKodu = ogrenci.KurumKodu,
                        OpaqId = ogrenci.OpaqId,
                        Sinifi = ogrenci.Sinifi,
                        Adi = ogrenci.Adi,
                        IlceAdi = ogrenci.IlceAdi,
                        Soyadi = ogrenci.Soyadi,
                        GirisKey = ""
                    };

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
                        puanlama.OgrPuanTablosunaKayitIslemleri(okulPuani, toplamBransPuan, toplamKatsayiPuani, ogrenciTest, sinavId);

                        puanlama.IlcePuanHesaplama(sinavId, ogrenciTest, katSayi, dogruYanlisOrani, okulPuani);
                    }


                    JsonSonuc soList = new JsonSonuc
                    {
                        Mesaj = "Değerlendirme bitti",
                        Sonuc = "ok",
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
                    Rapor = "Bitiren: Yönetici " + kInfo.AdiSoyadi + ". Bitir metodu if (sonuc.Bitti == SinavDurum.DevamEdiyor.ToInt32()) else metodu mesaj:" + mesaj
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
   

  
}