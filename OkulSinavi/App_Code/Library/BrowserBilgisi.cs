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
                                + "Browser = " + browser.Type + " - "
                                + "Version = " + browser.Version + " - "
                                + "Platform = " + browser.Platform + "<br>"
                                 + "Supports Cookies = " + browser.Cookies + " -"
                                + "Supports EcmaScript Version = " + browser.EcmaScriptVersion + " - "
                                + "Supports Java Applets = " + browser.JavaApplets + " - "
                                + "Supports ActiveX Controls = " + browser.ActiveXControls + " - "
                                + "Supports JavaScript Version = " + browser["JavaScriptVersion"];
        return browserBilgisi;
    }
}