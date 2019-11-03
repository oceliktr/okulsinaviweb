using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for TurkiyeOrtalamasi
/// </summary>
public static class Turkiye
{
    
    public static int Ortalamasi(string kazanim)
    {
        int ortalama = 0;
        if (kazanim == "M.5.1.1.1") ortalama = 75;
        if (kazanim == "M.5.1.1.2") ortalama = 65;
        if (kazanim == "M.5.1.1.3") ortalama = 82;
        if (kazanim == "M.5.1.2.1") ortalama = 56;
        if (kazanim == "M.5.1.2.10") ortalama = 46;
        if (kazanim == "M.5.1.2.11") ortalama = 47;
        if (kazanim == "M.5.1.2.2") ortalama = 44;
        if (kazanim == "M.5.1.2.3") ortalama = 49;
        if (kazanim == "M.5.1.2.4") ortalama = 32;
        if (kazanim == "M.5.1.2.5") ortalama = 57;
        if (kazanim == "M.5.1.2.6") ortalama = 47;
        if (kazanim == "M.5.1.2.7") ortalama = 55;
        if (kazanim == "M.5.1.2.8") ortalama = 45;
        if (kazanim == "M.5.1.2.9") ortalama = 44;
        if (kazanim == "M.5.1.3.1") ortalama = 57;
        if (kazanim == "M.5.1.3.2") ortalama = 39;
        if (kazanim == "M.5.1.3.3") ortalama = 39;
        if (kazanim == "M.5.1.3.4") ortalama = 32;
        if (kazanim == "M.5.1.3.5") ortalama = 60;
        if (kazanim == "M.5.1.3.6") ortalama = 52;
        if (kazanim == "M.5.1.4.1") ortalama = 54;
        if (kazanim == "M.5.1.4.2") ortalama = 63;
        if (kazanim == "M.5.1.5.1") ortalama = 80;
        if (kazanim == "M.5.1.5.2") ortalama = 59;
        if (kazanim == "M.5.1.5.3") ortalama = 36;
        if (kazanim == "M.5.1.5.4") ortalama = 38;
        if (kazanim == "M.5.1.5.5") ortalama = 25;
        if (kazanim == "M.5.1.5.6") ortalama = 64;
        if (kazanim == "M.5.1.6.1") ortalama = 55;
        if (kazanim == "M.5.1.6.2") ortalama = 72;
        if (kazanim == "M.5.1.6.3") ortalama = 58;
        if (kazanim == "M.5.1.6.4") ortalama = 51;
        if (kazanim == "M.5.2.1.1") ortalama = 56;
        if (kazanim == "M.5.2.1.2") ortalama = 51;
        if (kazanim == "M.5.2.1.3") ortalama = 61;
        if (kazanim == "M.5.2.1.4") ortalama = 55;
        if (kazanim == "M.5.2.1.5") ortalama = 72;
        if (kazanim == "M.5.2.1.6") ortalama = 48;
        if (kazanim == "M.5.2.2.1") ortalama = 53;
        if (kazanim == "M.5.2.2.2") ortalama = 41;
        if (kazanim == "M.5.2.2.3") ortalama = 34;
        if (kazanim == "M.5.2.2.4") ortalama = 47;
        if (kazanim == "M.5.3.1.1") ortalama = 53;
        if (kazanim == "M.5.3.1.2") ortalama = 76;
        if (kazanim == "M.5.3.1.3") ortalama = 41;

        //if (kazanim == "T.5.3.5") ortalama = 60;
        //if (kazanim == "T.5.3.6") ortalama = 54;
        //if (kazanim == "T.5.3.7") ortalama = 39;
        //if (kazanim == "T.5.3.8") ortalama = 56;
        //if (kazanim == "T.5.3.9") ortalama = 51;
        //if (kazanim == "T.5.3.10") ortalama = 66;
        //if (kazanim == "T.5.3.12") ortalama = 61;
        //if (kazanim == "T.5.3.13") ortalama = 49;
        //if (kazanim == "T.5.3.14") ortalama = 66;
        //if (kazanim == "T.5.3.15") ortalama = 56;
        //if (kazanim == "T.5.3.16") ortalama = 23;
        //if (kazanim == "T.5.3.17") ortalama = 50;
        //if (kazanim == "T.5.3.19") ortalama = 56;
        //if (kazanim == "T.5.3.20") ortalama = 61;
        //if (kazanim == "T.5.3.21") ortalama = 28;
        //if (kazanim == "T.5.3.22") ortalama = 64;
        //if (kazanim == "T.5.3.24") ortalama = 53;
        //if (kazanim == "T.5.3.25") ortalama = 33;
        //if (kazanim == "T.5.3.26") ortalama = 68;
        //if (kazanim == "T.5.3.27") ortalama = 55;
        //if (kazanim == "T.5.3.29") ortalama = 33;
        //if (kazanim == "T.5.3.30") ortalama = 39;
        //if (kazanim == "T.5.3.31") ortalama = 43;
        //if (kazanim == "T.5.3.32") ortalama = 58;
        //if (kazanim == "T.5.3.33") ortalama = 66;
        //if (kazanim == "T.5.3.34") ortalama = 66;

        if (kazanim == "T.5.3.5") ortalama = 63;
        if (kazanim == "T.5.3.6") ortalama = 43;
        if (kazanim == "T.5.3.7") ortalama = 59;
        if (kazanim == "T.5.3.8") ortalama = 54;
        if (kazanim == "T.5.3.9") ortalama = 53;
        if (kazanim == "T.5.3.10") ortalama = 72;
        if (kazanim == "T.5.3.12") ortalama = 61;
        if (kazanim == "T.5.3.13") ortalama = 63;
        if (kazanim == "T.5.3.14") ortalama = 30;
        if (kazanim == "T.5.3.15") ortalama = 45;
        if (kazanim == "T.5.3.16") ortalama = 57;
        if (kazanim == "T.5.3.17") ortalama = 35;
        if (kazanim == "T.5.3.19") ortalama = 59;
        if (kazanim == "T.5.3.20") ortalama = 39;
        if (kazanim == "T.5.3.21") ortalama = 36;
        if (kazanim == "T.5.3.22") ortalama = 73;
        if (kazanim == "T.5.3.24") ortalama = 55;
        if (kazanim == "T.5.3.25") ortalama = 40;
        if (kazanim == "T.5.3.26") ortalama = 40;
        if (kazanim == "T.5.3.27") ortalama = 39;
        if (kazanim == "T.5.3.29") ortalama = 37;
        if (kazanim == "T.5.3.30") ortalama = 40;
        if (kazanim == "T.5.3.31") ortalama = 37;
        if (kazanim == "T.5.3.32") ortalama = 32;
        if (kazanim == "T.5.3.33") ortalama = 59;
        if (kazanim == "T.5.3.34") ortalama = 41;

        if (kazanim == "F.5.1.1.1") ortalama = 80;
        if (kazanim == "F.5.1.1.2") ortalama = 87;
        if (kazanim == "F.5.1.2.1") ortalama = 87;
        if (kazanim == "F.5.1.2.2") ortalama = 74;
        if (kazanim == "F.5.1.3.1") ortalama = 46;
        if (kazanim == "F.5.1.3.2") ortalama = 77;
        if (kazanim == "F.5.1.4.1") ortalama = 76;
        if (kazanim == "F.5.1.5.1") ortalama = 63;
        if (kazanim == "F.5.1.5.2") ortalama = 75;
        if (kazanim == "F.5.2.1.1") ortalama = 80;
        if (kazanim == "F.5.2.1.2") ortalama = 76;
        if (kazanim == "F.5.3.1.1") ortalama = 72;
        if (kazanim == "F.5.3.1.2") ortalama = 54;
        if (kazanim == "F.5.3.2.1") ortalama = 62;
        if (kazanim == "F.5.3.2.2") ortalama = 61;
        if (kazanim == "F.5.3.2.3") ortalama = 57;
        if (kazanim == "F.5.4.1.1") ortalama = 62;
        if (kazanim == "F.5.4.2.1") ortalama = 53;
        if (kazanim == "F.5.4.3.1") ortalama = 57;
        if (kazanim == "F.5.4.3.2") ortalama = 52;
        if (kazanim == "F.5.4.4.1") ortalama = 71;
        if (kazanim == "F.5.4.4.2") ortalama = 66;
        if (kazanim == "F.5.5.1.1") ortalama = 49;
        if (kazanim == "F.5.5.2.1") ortalama = 74;
        if (kazanim == "F.5.5.2.2") ortalama = 57;
        if (kazanim == "F.5.5.3.1") ortalama = 73;
        if (kazanim == "F.5.5.4.1") ortalama = 52;
        if (kazanim == "F.5.5.4.2") ortalama = 58;
        if (kazanim == "F.5.6.1.1") ortalama = 57;
        if (kazanim == "F.5.6.1.2") ortalama = 71;
        if (kazanim == "F.5.6.2.1") ortalama = 69;
        if (kazanim == "F.5.6.2.2") ortalama = 67;


        return ortalama;
    }

}