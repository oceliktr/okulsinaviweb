using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for JsonMesaj
/// </summary>
public class JsonMesaj
{
    public string Mesaj { get; set; }
    public string Sonuc { get; set; }
}

public class JsonSonuc
{
    public string Sonuc { get; set; }
    public string Mesaj { get; set; }
    public int KalanSaat { get; set; }
    public int KalanDakika { get; set; }
    public int KalanSaniye { get; set; }
}