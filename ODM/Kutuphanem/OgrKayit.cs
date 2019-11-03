namespace ODM.Kutuphanem
{
    public class OgrenciKayit
    {
        public int OgrenciNo { get; set; }
        public string SayfaYuzu { get; set; }
        public string DosyaAdi { get; set; }
        public OgrenciKayit()
        { }

        public OgrenciKayit(int ogrNo, string abcd,string dosyaAdi)
        {
            SayfaYuzu = abcd;
            OgrenciNo = ogrNo;
            DosyaAdi = dosyaAdi;
        }
    }
}
