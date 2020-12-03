using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for TestSeciliDonem
/// </summary>
public static class TestSeciliDonem
{
    public static TestDonemInfo SeciliDonem()
        {
            TestDonemDb dnmDb = new TestDonemDb();
            TestDonemInfo donemInfo = new TestDonemInfo();

            //seçili dönem yoksa aktif dönemi cookie yazalım
            if (HttpContext.Current.Request.Cookies["csDonem"] == null)
            {
                donemInfo = dnmDb.AktifDonem();

                HttpCookie uyeCookie = new HttpCookie("csDonem");
                uyeCookie["SeciliDonem"] = donemInfo.Id.ToString();
                uyeCookie.Expires = GenelIslemler.YerelTarih().AddDays(1);
                HttpContext.Current.Response.Cookies.Add(uyeCookie);

            }
            else
            {
                //seçili dönem bilgisini gönderelim
                string donemCookies = HttpContext.Current.Request.Cookies["csDonem"]["SeciliDonem"];
                donemInfo = dnmDb.KayitBilgiGetir(donemCookies.ToInt32());

            }

            return donemInfo;
        }
    
}