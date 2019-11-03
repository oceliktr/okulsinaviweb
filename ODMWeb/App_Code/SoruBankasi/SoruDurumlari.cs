using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for SoruDurumlari
/// </summary>
public static class SoruDurumlari
{
    public enum Durum
    {
        Inceleniyor = 0,
        RedakteEdiliyor = 1,
        RedakteEdildi = 2,
        GeriGonderildi = 3,
        Kullanildi=4
    }
    public static string DurumBul(this int d)
    {
        string s="";
        switch (d)
        {
            case 0:
                s = "<span class=\"label label-info pull-right\">Henüz İncelenmedi</span>";
                break;
            case 1:
                s = "<span class=\"label label-warning pull-right\">Redakte ediliyor</span>";
                break;
            case 2:
                s = "<span class=\"label label-success pull-right\">Redakte edildi</span>";
                break;
            case 3:
                s = "<span class=\"label label-danger pull-right\">Geri Gönderildi</span>";
                break;
            case 4:
                s = "<span class=\"label label-primary pull-right\">Kullanıldı/Bir sınav için ayrıldı</span>";
                break;
        }
        
        return s;
    }

    public enum LgsDurum
    {
        Yeni = 0,
        Incelendi = 1
    }
    public static string LgsDurumBul(this int d)
    {
        string s = "";
        switch (d)
        {
            case 0:
                s = "<span class=\"label label-info pull-right\">Henüz İncelenmedi</span>";
                break;
            case 1:
                s = "<span class=\"label label-success pull-right\">İncelendi</span>";
                break;
        }

        return s;
    }

}