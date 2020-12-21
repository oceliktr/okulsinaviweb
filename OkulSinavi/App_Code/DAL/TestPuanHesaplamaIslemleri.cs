using System.Collections.Generic;
using System.Linq;

/// <summary>
/// Summary description for TestPuanHesaplamaIslemleri
/// </summary>
public class TestPuanHesaplamaIslemleri
{
    public static void PuanlamaHesapla(string opaqId, int sinavId)
    {

        TestKutukDb kutukDb = new TestKutukDb();
        var ogrenciBilgi = kutukDb.KayitBilgiGetir(opaqId);
        TestOgrenci ogrenci = new TestOgrenci
        {
            OpaqId = opaqId,
            KurumKodu = ogrenciBilgi.KurumKodu,
            IlceAdi = ogrenciBilgi.IlceAdi,
            Sinifi = ogrenciBilgi.Sinifi
        };

        //Oturumları listele ve bu öğrenci tüm oturumlara katılmış mı kontrol et

        //oturumları listele
        TestOturumlarDb oturumlarDb = new TestOturumlarDb();
        List<TestOturumlarInfo> oturumlar = oturumlarDb.Oturumlar(sinavId);

        TestOgrCevapDb oturumCevapDb = new TestOgrCevapDb();

        foreach (TestOturumlarInfo o in oturumlar)
        {
            //önce bu oturumdaki öğrencinin testogrcevaplar tablosundaki doğru yanlış sayılarını hesapla
            TestOgrCevapInfo sonuc = oturumCevapDb.KayitBilgiGetir(o.Id, opaqId);
            if (sonuc.Id != 0)
            {
                OgrenciCevaplariniIsle(o.Id, sonuc, ogrenci);
            }

        }
        TestOgrCevapDb ogrCevapDb = new TestOgrCevapDb();
        List<TestOgrCevapInfo> ogrCevaplari = ogrCevapDb.KayitlariDiziyeGetir(opaqId);

        int kalanOturumSayisi = 0; //girmediği oturumları kontrol için gerekli

        foreach (TestOturumlarInfo o in oturumlar)
        {
            var res = ogrCevaplari.Count(x => x.OturumId == o.Id && (x.Dogru != 0 || x.Yanlis != 0));
            //sınavı tamamlanmış
            if (res == 0)
            {
                kalanOturumSayisi++;//sınava girmediği oturumu var.
            }
        }

       // if (kalanOturumSayisi == 0)
       // {
            //oturumları tamamlamış. öğrenci okul ve ilçe için hesaplama yap ve sonuç ekranına yönlendir.
            TestSinavlarDb sinavlarDb = new TestSinavlarDb();
            TestSinavlarInfo sinavInfo = sinavlarDb.KayitBilgiGetir(sinavId, ogrenciBilgi.KurumKodu.ToString());

            //sonuçları okul puanı dizisine al.
            List<TestOkulCevapInfo> okulPuani = new List<TestOkulCevapInfo>();


            foreach (TestOturumlarInfo otrm in oturumlar)
            {
                var res = ogrCevaplari.FirstOrDefault(x => x.OturumId == otrm.Id);
                if (res != null)
                {
                    if (res.Dogru != 0 || res.Yanlis != 0)
                    {
                        var ogrCevabi = res.Cevap;
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
            }

            BranslarDb branslarDb = new BranslarDb();
            var katSayi = branslarDb.KayitlariDiziyeGetir();

            int toplamKatsayiPuani = 0; //öğrencinin puanı için bu kat sayı gerekli
            decimal toplamBransPuan = 0;
            //Bu döngüde branşlar kadar döüşüm olacaktır. Çünkü dizeye branşa göre doğru yanlış sayıları işlendi.

            TestOkulPuanDb okulPuanDb = new TestOkulPuanDb();
            var okulPuaniList = okulPuanDb.KayitlariDiziyeGetir(sinavId, ogrenci.KurumKodu);

            int dogruYanlisOrani = ogrenci.Sinifi >= 9 ? 4 : 3;

            foreach (var pInfo in okulPuani)
            {
                var bransKatSayi = katSayi.FirstOrDefault(x => x.Id == pInfo.BransId).KatSayi;

                decimal net = (pInfo.Dogru - ((decimal)pInfo.Yanlis / dogruYanlisOrani));
                decimal b = net * bransKatSayi;
                toplamBransPuan += b;
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
                OgrPuanTablosunaKayitIslemleri(okulPuani, toplamBransPuan, toplamKatsayiPuani, ogrenci, sinavInfo);

                IlcePuanHesaplama(sinavId, ogrenci, katSayi, dogruYanlisOrani, sinavInfo, okulPuani);
            }
       // }

    }


    private static void IlcePuanHesaplama(int sinavId, TestOgrenci ogrenci, List<BranslarInfo> katSayi,
    int dogruYanlisOrani, TestSinavlarInfo sinavInfo, List<TestOkulCevapInfo> okulPuani)
    {
        TestIlcePuanDb ilcePuanDb = new TestIlcePuanDb(); //ilçe puan tablosu içinde sadece bu öğrencinin okul bilgileri güncellenir.
        var ilcePuaniList = ilcePuanDb.KayitBilgiGetir(sinavId, ogrenci.KurumKodu);

        TestOgrPuanDb ogrPuanDb = new TestOgrPuanDb();
        // toplamKatsayiPuani öğrenci sayısıyla çarpılmalı
        var ogrenciSayisi = ogrPuanDb.PuaniHesaplananOgrenciSayisi(sinavId, ogrenci.KurumKodu); //+1 bu öğrenci

        decimal toplamIlceBransPuan = 0;
        int toplamIlceKatsayiPuani = 0;
        TestOkulPuanDb okulPuanDb = new TestOkulPuanDb();

        List<TestOkulPuanInfo> okulPuaniList = okulPuanDb.KayitlariDiziyeGetir(sinavId, ogrenci.KurumKodu);

        foreach (var opBrans in okulPuaniList)
        {
            var bransKatSayi = katSayi.FirstOrDefault(x => x.Id == opBrans.BransId).KatSayi;

            decimal net = (opBrans.Dogru - ((decimal)opBrans.Yanlis / dogruYanlisOrani)) / ogrenciSayisi;
            toplamIlceBransPuan += net * bransKatSayi;
            toplamIlceKatsayiPuani += (opBrans.Bos + opBrans.Dogru + opBrans.Yanlis) * bransKatSayi;
        }

        decimal toplamIlcePuan =
            (toplamIlceBransPuan * sinavInfo.Puanlama) / ((decimal)toplamIlceKatsayiPuani / ogrenciSayisi);

        if (ilcePuaniList.Id == 0) //kayıt yok ise
        {
            int toplamDogruSayisi = okulPuani.Sum(x => x.Dogru);
            int toplamYanlisSayisi = okulPuani.Sum(x => x.Yanlis);
            int toplamBosSayisi = okulPuani.Sum(x => x.Bos);

            TestIlcePuanInfo ilcePuanInfo = new TestIlcePuanInfo()
            {
                Bos = toplamBosSayisi,
                Dogru = toplamDogruSayisi,
                Yanlis = toplamYanlisSayisi,
                KurumKodu = ogrenci.KurumKodu,
                SinavId = sinavId,
                IlceAdi = ogrenci.IlceAdi,
                Puan = toplamIlcePuan,
                OgrSayisi = ogrenciSayisi
            };
            ilcePuanDb.KayitEkle(ilcePuanInfo);
        }
        else
        {
            int toplamDogruSayisi = okulPuani.Sum(x => x.Dogru) + ilcePuaniList.Dogru;
            int toplamYanlisSayisi = okulPuani.Sum(x => x.Yanlis) + ilcePuaniList.Yanlis;
            int toplamBosSayisi = okulPuani.Sum(x => x.Bos) + ilcePuaniList.Bos;

            TestIlcePuanInfo ilcePuanInfo = new TestIlcePuanInfo()
            {
                Bos = toplamBosSayisi,
                Dogru = toplamDogruSayisi,
                Yanlis = toplamYanlisSayisi,
                Puan = toplamIlcePuan,
                OgrSayisi = ogrenciSayisi,
                Id = ilcePuaniList.Id
            };
            ilcePuanDb.KayitGuncelle(ilcePuanInfo);
        }
    }

    private static void OgrPuanTablosunaKayitIslemleri(List<TestOkulCevapInfo> okulPuani, decimal toplamBransPuan, int toplamKatsayiPuani,
        TestOgrenci ogrenci, TestSinavlarInfo sinavInfo)
    {

        //(tr*4+mat*4+....)*100 / 270 270 =>toplamKatsayiPuani
        int toplamDogruSayisi = okulPuani.Sum(x => x.Dogru);
        int toplamYanlisSayisi = okulPuani.Sum(x => x.Yanlis);
        int toplamBosSayisi = okulPuani.Sum(x => x.Bos);
        decimal toplamPuan = (toplamBransPuan * sinavInfo.Puanlama) / toplamKatsayiPuani;
        TestOgrPuanDb ogrPuanDb = new TestOgrPuanDb();
        TestOgrPuanInfo op = new TestOgrPuanInfo()
        {
            Dogru = toplamDogruSayisi,
            Yanlis = toplamYanlisSayisi,
            Bos = toplamBosSayisi,
            OpaqId = ogrenci.OpaqId,
            Puan = toplamPuan,
            SinavId = sinavInfo.Id,
            KurumKodu = ogrenci.KurumKodu
        };
        ogrPuanDb.KayitEkle(op);
    }

    private static void OgrenciCevaplariniIsle(int OturumId, TestOgrCevapInfo sonuc, TestOgrenci ogrenci)
    {
        TestOgrCevapDb oturumCevapDb = new TestOgrCevapDb();

        string ogrCevaplari = sonuc.Cevap; //öğrencinin cevapları ABCCDCDCBCABCCDCDCBC formatında geliyor
        CacheHelper.SorulariGetirKaldir(OturumId);
        List<TestSorularInfo> cevaplar = CacheHelper.SorulariGetir(OturumId);

        int dogru = 0;
        int yanlis = 0;

        for (int i = 0; i < ogrCevaplari.Length; i++)
        {
            var c = cevaplar.FirstOrDefault(x => x.SoruNo == i + 1);
            if (ogrCevaplari.Substring(i, 1) == " ")
            {
            }
            else if (c.Cevap == ogrCevaplari.Substring(c.SoruNo - 1, 1))
            {
                dogru++;
            }
            else
            {
                yanlis++;
            }
        }

        TestOgrCevapInfo cevapInfo = new TestOgrCevapInfo
        {
            OturumId = OturumId,
            OpaqId = ogrenci.OpaqId,
            Dogru = dogru,
            Yanlis = yanlis,
            Bitti = 1
        };

        oturumCevapDb.DogruYanlisGuncelle(cevapInfo);
    }

}