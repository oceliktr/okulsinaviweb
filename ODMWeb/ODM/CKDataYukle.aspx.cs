using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Data;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI.WebControls;
using DAL;

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
            }
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

                        foreach (string line in lines)
                        {
                            //her satırı tek tek böl
                            string[] bol = line.Split('|');
                            if (bol[0] == "{Kutuk}")
                            {
                                CkKarneKutukDB kutukDb = new CkKarneKutukDB();
                                //OpaqId +  IlAdi +  IlceAdi +  KurumKodu + KurumAdi +  OgrenciNo +  Adi +  Soyadi +  Sinifi +  Sube +  SinavId +  DersKodu
                                CkKarneKutukInfo kutukInfo = new CkKarneKutukInfo()
                                {
                                    OpaqId = bol[1].ToInt32(),
                                    IlAdi = bol[2],
                                    IlceAdi = bol[3],
                                    KurumKodu = bol[4].ToInt32(),
                                    KurumAdi = bol[5],
                                    OgrenciNo = bol[6].ToInt32(),
                                    Adi = bol[7],
                                    Soyadi = bol[8],
                                    Sinifi = bol[9].ToInt32(),
                                    Sube = bol[10],
                                    SinavId = bol[11].ToInt32(),
                                    DersKodu = bol[12].ToInt32()
                                };
                                kutukDb.KayitEkle(kutukInfo);
                            }
                            if (bol[0] == "{SinavAdi}")
                            { 
                                CkSinavAdiDB snvDb = new CkSinavAdiDB();
                                CkSinavAdiInfo snvInfo = new CkSinavAdiInfo()
                                {
                                    SinavId = bol[1].ToInt32(),
                                    SinavAdi = bol[2]
                                };
                                snvDb.KayitEkle(snvInfo);
                            }
                            if (bol[0] == "{DogruCevaplar}")
                            {
                                //dogruCevap.SinavId + "|" + dogruCevap.BransId + "|" + dogruCevap.KitapcikTuru + "|" + dogruCevap.Cevaplar
                                CkKarneDogruCevaplarDB dcDb = new CkKarneDogruCevaplarDB();
                                CkKarneDogruCevaplarInfo dcInfo = new CkKarneDogruCevaplarInfo()
                                {
                                    SinavId = bol[1].ToInt32(),
                                    BransId = bol[2].ToInt32(),
                                    KitapcikTuru = bol[3],
                                    Cevaplar = bol[4]
                                };
                                dcDb.KayitEkle(dcInfo);
                            }
                            if (bol[0] == "{OgrenciCevaplari}")
                            {
                                //ogrCvp.OpaqId + "|" + ogrCvp.SinavId + "|" + ogrCvp.KitapcikTuru + "|" + ogrCvp.CevapTipi + "|" + ogrCvp.KatilimDurumu + "|" + ogrCvp.Cevaplar + "|" + ogrCvp.BransId
                                CkKarneCevapTxtDB cvpTxtDb= new CkKarneCevapTxtDB();
                                CkKarneCevapTxtInfo ctInfo = new CkKarneCevapTxtInfo()
                                {
                                    OpaqId = bol[1].ToInt32(),
                                    SinavId = bol[2].ToInt32(),
                                    KitapcikTuru = bol[3],
                                    CevapTipi = bol[4].ToInt32(),
                                    KatilimDurumu = bol[5].ToInt32(),
                                    Cevaplar = bol[6],
                                    BransId = bol[7].ToInt32()
                                };
                                cvpTxtDb.KayitEkle(ctInfo);
                            }
                            if (bol[0] == "{KarneSonuclari}")
                            {
                                //sonuc.SinavId + "|" + sonuc.BransId + "|" + sonuc.Ilce + "|" + sonuc.KurumKodu + "|" + sonuc.Sinif + "|" + sonuc.Sube + "|" + sonuc.KitapcikTuru + "|" + sonuc.SoruNo + "|" + sonuc.Dogru + "|" + sonuc.Yanlis + "|" + sonuc.Bos
                                CkKarneSonuclariDB cvpTxtDb = new CkKarneSonuclariDB();
                                CkKarneSonuclariInfo ctInfo = new CkKarneSonuclariInfo()
                                {
                                    SinavId = bol[1].ToInt32(),
                                    BransId = bol[2].ToInt32(),
                                    Ilce = bol[3],
                                    KurumKodu = bol[4].ToInt32(),
                                    Sinif = bol[5].ToInt32(),
                                    Sube = bol[6],
                                    KitapcikTuru = bol[7],
                                    SoruNo = bol[8].ToInt32(),
                                    Dogru = bol[9].ToInt32(),
                                    Yanlis = bol[10].ToInt32(),
                                    Bos = bol[11].ToInt32()
                                };
                                cvpTxtDb.KayitEkle(ctInfo);
                            }
                            if (bol[0] == "{Branslar}")
                            {
                                //brans.Id + "|" + brans.BransAdi
                                CkKarneBranslarDB brnsDb = new CkKarneBranslarDB();
                                CkKarneBranslarInfo brnsInfo = new CkKarneBranslarInfo()
                                {
                                    SinavId = bol[1].ToInt32(),
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
                                    SinavId = bol[1].ToInt32(),
                                    Sinif = bol[2].ToInt32(),
                                    BransId = bol[3].ToInt32(),
                                    KazanimNo = bol[4],
                                    KazanimAdi = bol[5],
                                    KazanimAdiOgrenci = bol[6],
                                    Sorulari = bol[7]
                                };
                                kznmtDb.KayitEkle(kznmInfo);
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

            //    lblBilgi.Text = Server.MapPath(dosyaAdi);
            //    StreamReader sr = File.OpenText(Server.MapPath(dosyaAdi));
            //    string strContents = sr.ReadToEnd();
            //    To display normal raw contents
            //Response.Write(strContents);

            //    To handle Carriage returns
            //Response.Write(strContents.Replace("\n", "<br>"));

            //    sr.Close();

        }
    }
}