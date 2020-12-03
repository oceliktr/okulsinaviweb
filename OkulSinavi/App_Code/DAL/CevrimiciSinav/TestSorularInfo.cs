/// <summary>
/// Summary description for TestlerInfo
/// </summary>
public class TestSorularInfo
{
    public int Id { get; set; }
    public int OturumId { get; set; }
    public int BransId { get; set; }
    public int SoruNo { get; set; }
    public string Soru { get; set; }
    public string Cevap { get; set; }

    public TestSorularInfo(int id, int testId, int bransId, int soruNo, string soru, string cevap)
    {
        Id = id;
        OturumId = testId;
        BransId = bransId;
        SoruNo = soruNo;
        Soru = soru;
        Cevap = cevap;
    }

    public TestSorularInfo()
    {
    }
}