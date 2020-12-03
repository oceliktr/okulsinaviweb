using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for BrowserBilgisi
/// </summary>
public class BrowserBilgisi
{
    public string Getir()
    {
        var browser = System.Web.HttpContext.Current.Request.Browser;
        string browserBilgisi = "Browser UserAgent = " + HttpContext.Current.Request.UserAgent + "<br>"
                                + "Ip Adres = " + HttpContext.Current.Request.UserHostAddress + "<br>"
                                + "Browser = " + browser.Type + "<br>"
                                + "Version = " + browser.Version + "<br>"
                                + "Platform = " + browser.Platform + "<br>"
                                + "Supports Frames = " + browser.Frames + "<br>"
                                + "Supports Tables = " + browser.Tables + "<br>"
                                + "Supports Cookies = " + browser.Cookies + "<br>"
                                + "Supports EcmaScript Version = " + browser.EcmaScriptVersion + "<br>"
                                + "Supports Java Applets = " + browser.JavaApplets + "<br>"
                                + "Supports ActiveX Controls = " + browser.ActiveXControls + "<br>"
                                + "Supports JavaScript Version = " + browser["JavaScriptVersion"];
        return browserBilgisi;
    }
}