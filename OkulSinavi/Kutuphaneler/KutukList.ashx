<%@ WebHandler Language="C#" Class="KutukList" %>

using System;
using System.Web;
using System.Web.SessionState;

public class KutukList : IHttpHandler,IRequiresSessionState  {

    public void ProcessRequest (HttpContext context)
    {

        string api = "";
        if (!string.IsNullOrEmpty(context.Request.QueryString["api"]))
        {
            api = context.Request.QueryString["api"];
        }

        if (YetkiKontrol(context)&&api!="652136B973556CB86B07F3A2E4A5AEB5")
        {
            context.Response.Write("Hata");
            context.Response.End();
        }

        if (string.IsNullOrEmpty(context.Request.QueryString["donem"]))
        {
            context.Response.Write("Hata");
            context.Response.End();
        }
        if (string.IsNullOrEmpty(context.Request.QueryString["sinif"]))
        {
            context.Response.Write("Hata");
            context.Response.End();
        }
        int donem = context.Request.QueryString["donem"].ToInt32();
        int sinif = context.Request.QueryString["sinif"].ToInt32();
        TestKutukDb veriDb = new TestKutukDb();

        context.Response.ContentType = "text/json";
        context.Response.Charset = "utf-8";

        context.Response.Write(veriDb.Kutuk(donem,sinif));
    }

    public bool IsReusable {
        get {
            return false;
        }
    }
    private static bool YetkiKontrol(HttpContext context)
    {
        OturumIslemleri oturum = new OturumIslemleri();
        KullanicilarInfo kInfo = oturum.OturumKontrol(context);
        if (kInfo == null)
            return true;
        bool yetkili = !kInfo.Yetki.Contains("Root") && !kInfo.Yetki.Contains("Admin");
        return yetkili;
    }
}