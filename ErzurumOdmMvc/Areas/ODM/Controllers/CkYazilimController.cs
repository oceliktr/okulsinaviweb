using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ErzurumOdmMvc.Business.CkKarne;
using ErzurumOdmMvc.Common.Library;
using ErzurumOdmMvc.Entities.CKKarne;
using ErzurumOdmMvc.Filter;

namespace ErzurumOdmMvc.Areas.ODM.Controllers
{
    [YetkiKontrol(Roles = "Root")]
    public class CkYazilimController : Controller
    {
        // GET: ODM/CkYazilim
        public ActionResult Index()
        {
            CkSinavAdiManager sinavAdiManager = new CkSinavAdiManager();
            IEnumerable<CkSinavAdi> model = sinavAdiManager.List();
            return View(model);
        }

        [HttpPost]
        public ActionResult Index(FormCollection frm, HttpPostedFileBase updateFile)
        {
            CkSinavAdiManager sinavAdiManager = new CkSinavAdiManager();
            IEnumerable<CkSinavAdi> model = sinavAdiManager.List();

            if (updateFile.ContentLength > 0)
            {
                string yuklemeAdresi = "/upload/ckveri/";
                string dosyaAdi = Server.HtmlEncode(updateFile.FileName);
                string uzanti = Path.GetExtension(dosyaAdi);
                string rastgeleMetin = StringIslemleri.RastgeleMetinUret(8);
                if (uzanti == ".ck")
                {
                    if (!DosyaDizinIslemleri.DizinKontrol(Server.MapPath(yuklemeAdresi)))
                        DosyaDizinIslemleri.DizinOlustur(Server.MapPath(yuklemeAdresi));

                    string yeniDosyaAdi = Path.Combine(Server.MapPath("~" + yuklemeAdresi), Path.GetFileName($"{rastgeleMetin}{uzanti}"));
                    updateFile.SaveAs(yeniDosyaAdi);


                    string[] lines = System.IO.File.ReadAllLines(Server.MapPath($"{yuklemeAdresi}{rastgeleMetin}{uzanti}"));

                    int sinavId = frm["sinav"].ToInt32();

                    foreach (string line in lines)
                    {
                        //her satırı tek tek böl
                        string[] bol = line.Split('|');

                        if (bol[0] == "{SinavAdi}")
                        {
                            sinavId = bol[1].ToInt32(); //Sınav adı 

                            CkSinavAdiManager ckSinavAdiManager = new CkSinavAdiManager();
                            ckSinavAdiManager.AktifSinavlariPasifYap();

                            CkSinavAdi snv = new CkSinavAdi
                            {
                                SinavId = sinavId,
                                Aktif = 1,
                                SinavAdi = bol[2],
                                DegerlendirmeTuru = bol[3].ToInt32(),
                            };
                            ckSinavAdiManager.Insert(snv);

                            model = sinavAdiManager.List();
                        }

                        if (sinavId != 0)
                        {

                            if (bol[0] == "{DogruCevaplar}")
                            {
                                //dogruCevap.SinavId + "|" + dogruCevap.BransId + "|" + dogruCevap.KitapcikTuru + "|" + dogruCevap.Cevaplar
                                CkKarneDogruCevaplarManager dcDb = new CkKarneDogruCevaplarManager();
                                CkKarneDogruCevaplar dc = new CkKarneDogruCevaplar
                                {
                                    SinavId = sinavId,
                                    Sinif = bol[1].ToInt32(),
                                    BransId = bol[2].ToInt32(),
                                    KitapcikTuru = bol[3],
                                    Cevaplar = bol[4]
                                };
                                dcDb.Insert(dc);
                            }
                            if (bol[0] == "{IlceOrtalamasi}")
                            {
                                CkIlIlceOrtalamasiManager ortDb = new CkIlIlceOrtalamasiManager();
                                CkIlIlceOrtalamasi ort = new CkIlIlceOrtalamasi
                                {
                                    SinavId = sinavId,
                                    Ilce = bol[1],
                                    BransId = bol[2].ToInt32(),
                                    Sinif = bol[3].ToInt32(),
                                    KazanimId = bol[7].ToInt32(),
                                    IlBasariYuzdesi = bol[8].ToInt32(),
                                    IlceBasariYuzdesi = bol[9].ToInt32()
                                };
                                ortDb.Insert(ort);
                            }
                            if (bol[0] == "{Branslar}")
                            {
                                //brans.Id + "|" + brans.BransAdi
                                CkKarneBranslarManager brnsDb = new CkKarneBranslarManager();
                                CkKarneBranslar brns = new CkKarneBranslar
                                {
                                    SinavId = sinavId,
                                    BransId = bol[1].ToInt32(),
                                    BransAdi = bol[2]
                                };
                                brnsDb.Insert(brns);
                            }
                            if (bol[0] == "{Kazanimlar}")
                            {
                                //kazanim.Sinif + "|" + kazanim.BransId + "|" + kazanim.KazanimNo + "|" + kazanim.KazanimAdi + "|" + kazanim.KazanimAdiOgrenci + "|" + kazanim.Sorulari
                                CkKarneKazanimlarManager kznmtDb = new CkKarneKazanimlarManager();
                                CkKarneKazanimlar kznm = new CkKarneKazanimlar()
                                {
                                    KazanimId = bol[1].ToInt32(),
                                    SinavId = sinavId,
                                    Sinif = bol[2].ToInt32(),
                                    BransId = bol[3].ToInt32(),
                                    KazanimNo = bol[4],
                                    KazanimAdi = bol[5],
                                    KazanimAdiOgrenci = bol[6],
                                    Sorulari = bol[7]
                                };
                                kznmtDb.Insert(kznm);
                            }
                            if (bol[0] == "{Kutuk}")
                            {
                                CkKarneKutukManager kutukDb = new CkKarneKutukManager();
                                //OpaqId, IlceAdi, KurumKodu, KurumAdi, OgrenciNo, Adi, Soyadi, Sinifi, Sube, SinavId, KatilimDurumu, KitapcikTuru, Cevaplar
                                CkKarneKutuk kutuk = new CkKarneKutuk()
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
                                    KatilimDurumu = bol[10],
                                    Cevaplar = bol[11]
                                };
                                kutukDb.Insert(kutuk);
                            }
                            if (bol[0] == "{KarneSonuclari}")
                            {
                                // BransId Ilce KurumKodu Sinif Sube KitapcikTuru SoruNo Dogru Yanlis Bos
                                CkKarneSonuclariManager cvpTxtDb = new CkKarneSonuclariManager();
                                CkKarneSonuclari ct = new CkKarneSonuclari()
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
                                cvpTxtDb.Insert(ct);
                            }

                        }
                        else
                        {
                            ModelState.AddModelError("", "Sınav seçiniz veya önce sınav bilgisi olan dosya yükleyiniz.");
                            ViewBag.Uyari = "uyari";
                            return View(model);
                        }
                    }

                    ModelState.AddModelError("", updateFile.FileName + " isimli dosya başarıya yüklendi.");
                    ViewBag.Uyari = "islemTamam";
                    return View(model);
                }
                else
                {
                    ModelState.AddModelError("", "Lütfen CK Yazdır uygulamasıyla oluşturulan dosyayı yükleyiniz.");
                    ViewBag.Uyari = "uyari";
                    return View(model);
                }
            }

            return View(model);
        }

        [HttpPost]
        public JsonResult Sil(int id) //id=SinavId
        {
            CkSinavAdiManager ckSinavAdiManager = new CkSinavAdiManager();
            var sinavAdi = ckSinavAdiManager.SinavBilgisi(id);
            if (sinavAdi == null)
            {
                return Json(new { Sonuc = false, Mesaj = "Sınav bilgisi bulunamadı." });
            }

            int kayitSayisi = ckSinavAdiManager.TumunuSil(id);

            if (kayitSayisi > 0)
            {
                return Json(new { Sonuc = true, Mesaj = kayitSayisi+" kayıt silindi." });
            }
            return Json(new { Sonuc = false, Mesaj = "Kayıt silinemedi." });
        }

    }
}