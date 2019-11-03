using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;
using DAL;

/// <summary>
/// Summary description for AktifSinav
/// </summary>
[WebService(Namespace = "http://tempuri.org/")]
[WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
// To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
// [System.Web.Script.Services.ScriptService]
public class AktifSinav : System.Web.Services.WebService
{

    public AktifSinav()
    {
        
    }
    [WebMethod]
    public void GuncelSinav(int sinavId,int veriGirisi,string access)
    {
        if(access== "B7BC3B9344001FF88AA061BDB901BE29")
        {
            AyarlarDb ayrDb = new AyarlarDb();
            ayrDb.KayitGuncelle(sinavId, veriGirisi);
        }
    }
    [WebMethod]
    public string HelloWorld()
    {
        return "Hello World";
    }

}
