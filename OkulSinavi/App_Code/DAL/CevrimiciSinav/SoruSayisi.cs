using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for SoruSayisi
/// </summary>
public class SoruSayisi
{
    public int SoruNoText { get; set; }
    public int SoruNoValue { get; set; }

    public bool Bos { get; set; }
    public bool BuSoru { get; set; }

    public SoruSayisi(int soruNoText, int soruNoValue, bool bos, bool buSoru)
    {
        SoruNoText = soruNoText;
        SoruNoValue = soruNoValue;
        Bos = bos;
        BuSoru = buSoru;
    }
}