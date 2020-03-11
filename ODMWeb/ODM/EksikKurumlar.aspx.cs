using System;
using System.Collections.Generic;
using System.Linq;

public partial class ODM_EksikKurumlar : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {

    }

    protected void btnSifreOlustur_OnClick(object sender, EventArgs e)
    {
        KurumlarDb kurumDb = new KurumlarDb();
        KullanicilarDb kullaniciDb = new KullanicilarDb();

        //Kurumları diziye al.
        List<KurumlarInfo> kurumInfo = kurumDb.SinavaGirenOkullariDiziyeGetir(5);
        List<string> diziKurum = new List<string>();
        foreach (var krm in kurumInfo)
        {
           diziKurum.Add(krm.KurumKodu); 
        }

        //Kullanıcıları diziye al.
        List<KullanicilarInfo> kullanicilar = kullaniciDb.KayitlariDiziyeGetir("OkulYetkilisi");
        List<string> diziKullanici = new List<string>();
        foreach (var klnc in kullanicilar)
        {
            diziKullanici.Add(klnc.KurumKodu);
        }

        string[] kullaniciOlmayanKurumlar = diziKurum.Except(diziKullanici).ToArray();

        foreach (var kullanici in kullaniciOlmayanKurumlar)
        {
            KurumlarInfo kurumInf = kurumDb.KayitBilgiGetir(kullanici);
            KullanicilarInfo infoK = new KullanicilarInfo
            {
                Email = kullanici + "@meb.k12.tr",
                Grup = "",
                Yetki = "OkulYetkilisi|",
                IlceId = kurumInf.IlceId,
                KurumKodu = kullanici,
                TcKimlik = kullanici.Md5Sifrele(),
                Sifre = kullanici.Md5Sifrele(),
                AdiSoyadi=kurumInf.KurumAdi,
                Bransi=0
            };
            kullaniciDb.KayitEkle(infoK);
        }
        Response.Write("Eksik kullanıcılar oluşturuldu. Eklenen kullanıcı sayısı: "+kullaniciOlmayanKurumlar.Count());
    }
}