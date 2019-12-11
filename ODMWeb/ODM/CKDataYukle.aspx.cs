using DAL;
using System;
using System.IO;
using System.Web;

namespace ODM
{
    public partial class CKDataYukle : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (!Master.Yetki().Contains("Root"))
                    Response.Redirect("Giris.aspx");

                MevcutSinavlar();
            }
        }

        private void MevcutSinavlar()
        {
            CkSinavAdiDB sinav = new CkSinavAdiDB();
            ddlSinavlar.DataSource = sinav.KayitlariGetir();
            ddlSinavlar.DataValueField = "SinavId";
            ddlSinavlar.DataTextField = "SinavAdi";
            ddlSinavlar.DataBind();
            ddlSinavlar.Items.Insert(0, new System.Web.UI.WebControls.ListItem("Sınav Seçiniz", ""));
        }

        protected void btnDosyaEkle_OnClick(object sender, EventArgs e)
        {
            if (fuFile.HasFile)
            {
                string dosyaAdi = Server.HtmlEncode(fuFile.FileName);
                string uzanti = Path.GetExtension(dosyaAdi);
                if (uzanti != null)
                {
                    //Dizin yoksa
                    if (!DizinIslemleri.DizinKontrol(Server.MapPath("/upload/ckveri/")))
                        Directory.CreateDirectory(Server.MapPath("/upload/ckveri/"));

                    uzanti = uzanti.ToLower();
                    string rastgeleMetin = GenelIslemler.RastgeleMetinUret(8);
                    if (uzanti == ".ck")
                    {
                        dosyaAdi = string.Format("{0}_{1}{2}", dosyaAdi.ToUrl(), rastgeleMetin, uzanti);
                        string dosyaYolu = string.Format(@"{0}upload\ckveri\{1}", HttpContext.Current.Server.MapPath("/"), dosyaAdi);
                        File.WriteAllBytes(dosyaYolu, fuFile.FileBytes);

                        string dosya = string.Format(@"/upload/ckveri/{0}", dosyaAdi);

                        string[] lines = File.ReadAllLines(Server.MapPath(dosya));
                     
                        int sinavId = ddlSinavlar.SelectedValue.ToInt32();

                        foreach (string line in lines)
                        {
                            //her satırı tek tek böl
                            string[] bol = line.Split('|');

                            if (bol[0] == "{SinavAdi}")
                            {
                                sinavId = bol[1].ToInt32(); //Sınav adı 

                                CkSinavAdiDB snvDb = new CkSinavAdiDB();
                                snvDb.AktifSinavlariPasifYap();

                                CkSinavAdiInfo snvInfo = new CkSinavAdiInfo()
                                {
                                    SinavId = bol[1].ToInt32(),
                                    Aktif = 1,
                                    SinavAdi = bol[2],
                                    DegerlendirmeTuru = bol[3].ToInt32(),
                                };
                                snvDb.KayitEkle(snvInfo);

                                MevcutSinavlar();
                            }

                            if (sinavId!=0)
                            {

                                if (bol[0] == "{DogruCevaplar}")
                                {
                                    //dogruCevap.SinavId + "|" + dogruCevap.BransId + "|" + dogruCevap.KitapcikTuru + "|" + dogruCevap.Cevaplar
                                    CkKarneDogruCevaplarDB dcDb = new CkKarneDogruCevaplarDB();
                                    CkKarneDogruCevaplarInfo dcInfo = new CkKarneDogruCevaplarInfo()
                                    {
                                        SinavId = sinavId,
                                        Sinif = bol[2].ToInt32(),
                                        BransId = bol[3].ToInt32(),
                                        KitapcikTuru = bol[4],
                                        Cevaplar = bol[5]
                                    };
                                    dcDb.KayitEkle(dcInfo);
                                }
                                if (bol[0] == "{IlceOrtalamasi}")
                                {
                                    CkIlIlceOrtalamasiDB ortDb = new CkIlIlceOrtalamasiDB();
                                    CkIlIlceOrtalamasiInfo ortInfo = new CkIlIlceOrtalamasiInfo()
                                    {
                                        SinavId = sinavId,
                                        Ilce = bol[1],
                                        BransId = bol[2].ToInt32(),
                                        Sinif = bol[3].ToInt32(),
                                        KazanimId = bol[7].ToInt32(),
                                        IlBasariYuzdesi = bol[8].ToInt32(),
                                        IlceBasariYuzdesi = bol[9].ToInt32()
                                    };
                                    ortDb.KayitEkle(ortInfo);
                                }
                                if (bol[0] == "{Branslar}")
                                {
                                    //brans.Id + "|" + brans.BransAdi
                                    CkKarneBranslarDB brnsDb = new CkKarneBranslarDB();
                                    CkKarneBranslarInfo brnsInfo = new CkKarneBranslarInfo()
                                    {
                                        SinavId = sinavId,
                                        BransId = bol[2].ToInt32(),
                                        BransAdi = bol[3]
                                    };
                                    brnsDb.KayitEkle(brnsInfo);
                                }
                                if (bol[0] == "{Kazanimlar}")
                                {
                                    //kazanim.Sinif + "|" + kazanim.BransId + "|" + kazanim.KazanimNo + "|" + kazanim.KazanimAdi + "|" + kazanim.KazanimAdiOgrenci + "|" + kazanim.Sorulari
                                    CkKarneKazanimlardB kznmtDb = new CkKarneKazanimlardB();
                                    CkKarneKazanimlarInfo kznmInfo = new CkKarneKazanimlarInfo()
                                    {
                                        KazanimId = bol[1].ToInt32(),
                                        SinavId = sinavId,
                                        Sinif = bol[3].ToInt32(),
                                        BransId = bol[4].ToInt32(),
                                        KazanimNo = bol[5],
                                        KazanimAdi = bol[6],
                                        KazanimAdiOgrenci = bol[7],
                                        Sorulari = bol[8]
                                    };
                                    kznmtDb.KayitEkle(kznmInfo);
                                }
                                if (bol[0] == "{Kutuk}")
                                {
                                    CkKarneKutukDB kutukDb = new CkKarneKutukDB();
                                    //OpaqId, IlceAdi, KurumKodu, KurumAdi, OgrenciNo, Adi, Soyadi, Sinifi, Sube, SinavId, KatilimDurumu, KitapcikTuru, Cevaplar
                                    CkKarneKutukInfo kutukInfo = new CkKarneKutukInfo()
                                    {
                                        OpaqId = bol[1].ToInt32(),
                                        IlceAdi = bol[2],
                                        KurumKodu = bol[3].ToInt32(),
                                        KurumAdi = bol[4],
                                        OgrenciNo = bol[5].ToInt32(),
                                        Adi = bol[6],
                                        Soyadi = bol[7],
                                        Sinifi = bol[8].ToInt32(),
                                        Sube = bol[9],
                                        SinavId = sinavId,
                                        KatilimDurumu = bol[11],
                                        KitapcikTuru = bol[12],
                                        Cevaplar = bol[13]
                                    };
                                    kutukDb.KayitEkle(kutukInfo);
                                }
                                if (bol[0] == "{KarneSonuclari}")
                                {
                                    // BransId Ilce KurumKodu Sinif Sube KitapcikTuru SoruNo Dogru Yanlis Bos
                                    CkKarneSonuclariDB cvpTxtDb = new CkKarneSonuclariDB();
                                    CkKarneSonuclariInfo ctInfo = new CkKarneSonuclariInfo()
                                    {
                                        SinavId = sinavId,
                                        BransId = bol[1].ToInt32(),
                                        Ilce = bol[2],
                                        KurumKodu = bol[3].ToInt32(),
                                        Sinif = bol[4].ToInt32(),
                                        Sube = bol[5],
                                        KitapcikTuru = bol[6],
                                        SoruNo = bol[7].ToInt32(),
                                        Dogru = bol[8].ToInt32(),
                                        Yanlis = bol[9].ToInt32(),
                                        Bos = bol[10].ToInt32()
                                    };
                                    cvpTxtDb.KayitEkle(ctInfo);
                                }

                            }
                            else
                            {
                                Master.UyariTuruncu("Sınav seçiniz veya önce sınav bilgisi olan dosya yükleyiniz.",phUyari);
                                return;
                            }
                        }


                        Master.UyariIslemTamam("Dosya başarıya yüklendi.", phUyari);
                    }
                    else
                    {
                        Master.UyariKirmizi("Lütfen CK Yazdır uygulamasıyla oluşturulan dosyayı yükleyiniz.", phUyari);
                    }
                }
            }

        }

        protected void lnkSil_OnClick(object sender, EventArgs e)
        {
            int sinavId = ddlSinavlar.SelectedValue.ToInt32();

            if (sinavId != 0)
            {
                CkIlIlceOrtalamasiDB ckIlIlceOrtalamasiDb= new CkIlIlceOrtalamasiDB();
                ckIlIlceOrtalamasiDb.SinaviSil(sinavId);

                CkKarneBranslarDB ckKarneBranslarDb= new CkKarneBranslarDB();
                ckKarneBranslarDb.SinaviSil(sinavId);

                CkKarneDogruCevaplarDB dogruCevaplarDb= new CkKarneDogruCevaplarDB();
                dogruCevaplarDb.SinaviSil(sinavId);

                CkKarneKazanimlardB karneKazanimlardB = new CkKarneKazanimlardB();
                karneKazanimlardB.SinaviSil(sinavId);

                CkKarneKutukDB kutukDb= new CkKarneKutukDB();
                kutukDb.SinaviSil(sinavId);

                CkKarneSonuclariDB karneSonuclariDb= new CkKarneSonuclariDB();
                karneSonuclariDb.SinaviSil(sinavId);

                CkSinavAdiDB sinavAdiDb= new CkSinavAdiDB();
                sinavAdiDb.SinaviSil(sinavId);

                MevcutSinavlar();
                Master.UyariBilgilendirme("Sınava ait tüm veriler silindi.",phUyari);

            }
            else
            {
                Master.UyariTuruncu("Herhangi bir sınav seçilmedi.",phUyari);
            }
        }
    }
}