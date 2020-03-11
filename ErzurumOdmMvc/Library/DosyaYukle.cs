using ErzurumOdmMvc.Common.Library;
using System;
using System.IO;
using System.Web;

namespace ErzurumOdmMvc.Library
{
    public static class DosyaYukle
    {
        public static DosyaResult Yukle(HttpPostedFileBase updateFile, string yuklemeAdresi,bool resimMi)
        {
            DosyaResult fResult = new DosyaResult();

            if (updateFile != null)
            {
                if (!DosyaDizinIslemleri.DizinKontrol(HttpContext.Current.Server.MapPath(yuklemeAdresi)))
                    DosyaDizinIslemleri.DizinOlustur(HttpContext.Current.Server.MapPath(yuklemeAdresi));

                string dosyaAdi = HttpContext.Current.Server.HtmlEncode(updateFile.FileName);
                string uzanti = Path.GetExtension(dosyaAdi);
                if (uzanti != null)
                {

                    uzanti = uzanti.ToLower();

                   // bool dosyaResim =resimYukle? GenelIslemler.yuklenecekResimler.Contains(uzanti): GenelIslemler.yuklenecekDosyalar.Contains(uzanti);

                    string rastgeleMetin = StringIslemleri.YeniGuid();
                    if (resimMi ? GenelIslemler.yuklenecekResimler.Contains(uzanti) : GenelIslemler.yuklenecekDosyalar.Contains(uzanti))
                    {
                        dosyaAdi = Path.Combine(HttpContext.Current.Server.MapPath("~" + yuklemeAdresi),
                            Path.GetFileName($"{rastgeleMetin}{uzanti}"));
                        updateFile.SaveAs(dosyaAdi);
                        fResult.Dosya = $"{yuklemeAdresi}{rastgeleMetin}{uzanti}";
                    }
                    else
                    {
                        fResult.Mesaj = "Yalnızca " + (resimMi ? GenelIslemler.yuklenecekResimler : GenelIslemler.yuklenecekDosyalar) + " uzantılı dosyalar yüklenir.";
                        fResult.Alert = "uyari";
                    }
                }
            }

            return fResult;
        }
    }
}