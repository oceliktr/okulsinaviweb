using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for OturumSinav
/// </summary>
public class OturumSinavJoinModel : TestOturumlarInfo
{
    public int Sinif { get; set; }
    public int Puanlama { get; set; }
    public string SinavAdi { get; set; }
    public int Aktif { get; set; }
}