using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for SinavPuanlamaIslemleri
/// </summary>
public class SinavPuanlamaIslemleri
{
    public void OgrPuanTablosunaKayitIslemleri(List<TestOkulCevapInfo> okulPuani, decimal toplamBransPuan, int toplamKatsayiPuani,
        TestOgrenci ogrenci, int sinavId)
    {
        TestSinavlarInfo sinavInfo = CacheHelper.Sinavlar(ogrenci.KurumKodu.ToString()).FirstOrDefault(x => x.Id == sinavId);

        //(tr*4+mat*4+....)*100 / 270 270 =>toplamKatsayiPuani
        int toplamDogruSayisi = okulPuani.Sum(x => x.Dogru);
        int toplamYanlisSayisi = okulPuani.Sum(x => x.Yanlis);
        int toplamBosSayisi = okulPuani.Sum(x => x.Bos);
        decimal toplamPuan = (toplamBransPuan * sinavInfo.Puanlama) / toplamKatsayiPuani;
        TestOgrPuanDb ogrPuanDb = new TestOgrPuanDb();
        var kontrol = ogrPuanDb.KayitBilgiGetir(sinavId, ogrenci.KurumKodu, ogrenci.OpaqId);
        if (kontrol.Id == 0)//eğer kayıt varsa
        {
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
    }
    public void OgrenciCevaplariniIsle(int OturumId, TestOgrCevapInfo sonuc, string opaqId)
    {
        string ogrCevaplari = sonuc.Cevap; //öğrencinin cevapları ABCCDCDCBCABCCDCDCBC formatında geliyor

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
            OpaqId = opaqId,
            Dogru = dogru,
            Yanlis = yanlis,
            Bitti = 1
        };

        TestOgrCevapDb oturumCevapDb = new TestOgrCevapDb();
        oturumCevapDb.DogruYanlisGuncelle(cevapInfo);

    }
    public void IlcePuanHesaplama(int sinavId, TestOgrenci ogrenci, List<BranslarInfo> katSayi,
          int dogruYanlisOrani, List<TestOkulCevapInfo> okulPuani)
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
        TestSinavlarInfo sinavInfo = CacheHelper.Sinavlar(ogrenci.KurumKodu.ToString()).FirstOrDefault(x => x.Id == sinavId);

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

 }