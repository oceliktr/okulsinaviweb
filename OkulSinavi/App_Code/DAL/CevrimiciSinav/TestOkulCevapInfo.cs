using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for TestOkulCevapInfo
/// </summary>
public class TestOkulCevapInfo
{
    public int Id { get; set; }
    public int OturumId { get; set; }
    public int BransId { get; set; }
    public int KurumKodu { get; set; }
    public int Dogru { get; set; }
    public int Yanlis { get; set; }
    public int Bos { get; set; }
    public decimal Puan { get; set; }

    public TestOkulCevapInfo(int bransId,int dogru, int yanlis, int bos,decimal puan)
    {
        BransId = bransId;
        Dogru = dogru;
        Yanlis = yanlis;
        Bos = bos;
        Puan = puan;
    }

    public TestOkulCevapInfo()
    {
    }
}