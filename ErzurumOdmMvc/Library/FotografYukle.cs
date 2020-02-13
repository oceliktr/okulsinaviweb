using ErzurumOdmMvc.Common.Library;
using System;
using System.IO;
using System.Web;

namespace ErzurumOdmMvc.Library
{
    public static class FotografYukle
    {
        public static FotografResult Yukle(HttpPostedFileBase updateFile, string yuklemeAdresi)
        {
            FotografResult fResult = new FotografResult();

            if (updateFile != null)
            {
                if (!DosyaDizinIslemleri.DizinKontrol(HttpContext.Current.Server.MapPath(yuklemeAdresi)))
                    DosyaDizinIslemleri.DizinOlustur(HttpContext.Current.Server.MapPath(yuklemeAdresi));

                string dosyaAdi = HttpContext.Current.Server.HtmlEncode(updateFile.FileName);
                string uzanti = Path.GetExtension(dosyaAdi);
                if (uzanti != null)
                {
                    uzanti = uzanti.ToLower();
                    string rastgeleMetin = ConvertIslemleri.YeniGuid();
                    if (GenelIslemler.yuklenecekResimler.Contains(uzanti))
                    {
                        dosyaAdi = Path.Combine(HttpContext.Current.Server.MapPath("~" + yuklemeAdresi),
                            Path.GetFileName($"{rastgeleMetin}{uzanti}"));
                        updateFile.SaveAs(dosyaAdi);
                        fResult.Foto = $"{yuklemeAdresi}{rastgeleMetin}{uzanti}";
                    }
                    else
                    {
                        fResult.Mesaj = "Yalnızca " + GenelIslemler.yuklenecekResimler + " uzantılı dosyalar yüklenir.";
                        fResult.Alert = "uyari";
                    }
                }
            }

            return fResult;
        }
    }
}