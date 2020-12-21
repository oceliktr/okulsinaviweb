using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for DevamEdenOkullarModel
/// </summary>
public class DevamEdenOkullarModel
{

    public string KurumAdi { get; set; }
    public string IlceAdi { get; set; }
    public int OgrenciSayisi { get; set; }

    public DevamEdenOkullarModel(string kurumAdi, string ilceAdi, int ogrenciSayisi)
    {
        KurumAdi = kurumAdi;
        IlceAdi = ilceAdi;
        OgrenciSayisi = ogrenciSayisi;
    }

    public DevamEdenOkullarModel()
    {
    }
}