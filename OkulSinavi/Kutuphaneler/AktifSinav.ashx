<%@ WebHandler Language="C#" Class="AktifSinav" %>

using System;
using System.Web;

public class AktifSinav : IHttpHandler {
    
    public void ProcessRequest (HttpContext context) {
        SinavlarDb veriDb = new SinavlarDb();

        context.Response.ContentType = "text/json";
        context.Response.Charset = "utf-8";

        context.Response.Write(veriDb.AktifDonemJson());
    }
 
    public bool IsReusable {
        get {
            return false;
        }
    }

}