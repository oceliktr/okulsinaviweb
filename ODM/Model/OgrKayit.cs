namespace ODM.Kutuphanem
{
    public class OgrenciKayit
    {
        public int OgrenciNo { get; set; }
        public int Oturum { get; set; }
        public string DizinAdresi { get; set; }
        public string DosyaAdi { get; set; }
        public OgrenciKayit()
        { }

        public OgrenciKayit(int ogrNo, int oturum,string dizinAdresi,string dosyaAdi)
        {
            Oturum = oturum;
            OgrenciNo = ogrNo;
            DosyaAdi = dosyaAdi;
            DizinAdresi = dizinAdresi;
        }
    }
}
