using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for OturumIslemleri
/// </summary>
public  class OturumIslemleri
{
    public  KullanicilarInfo OturumKontrol()
    {
        if (HttpContext.Current.Request.Cookies["uyeCookie"] == null) return null;
        int uyeId = HttpContext.Current.Request.Cookies["uyeCookie"]["UyeId"].ToInt32();

        KullanicilarInfo kInfo;
        if (HttpContext.Current.Session["Kullanici"] == null)
        {
            KullanicilarDb kDb = new KullanicilarDb();
            kInfo = kDb.KayitBilgiGetir(uyeId);
            HttpContext.Current.Session["Kullanici"] = kInfo;//tekrar sessiona yükle
        }
        else
        {
            kInfo = (KullanicilarInfo)HttpContext.Current.Session["Kullanici"];
            HttpContext.Current.Session["Kullanici"] = kInfo;//tekrar sessiona yükle session süresi uzasın
        }

        return kInfo;
    }
    public KullanicilarInfo OturumKontrol(HttpContext context)
    {
        if (context.Request.Cookies["uyeCookie"] == null) return null;
        int uyeId = context.Request.Cookies["uyeCookie"]["UyeId"].ToInt32();

        KullanicilarInfo kInfo;
        if (context.Session["Kullanici"] == null)
        {
            KullanicilarDb kDb = new KullanicilarDb();
            kInfo = kDb.KayitBilgiGetir(uyeId);
        }
        else
        {
            kInfo = (KullanicilarInfo)context.Session["Kullanici"];
            context.Session["Kullanici"] = kInfo;
        }

        return kInfo;
    }
    public OturumIslemleri()
    {
        //
        // TODO: Add constructor logic here
        //
    }
}