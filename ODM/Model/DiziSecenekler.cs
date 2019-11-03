namespace ODM.Kutuphanem
{
    public class DiziSecenekler
    {
        public DiziSecenekler()
        { }

        public DiziSecenekler(int oturum, int bransId, int ogrenciId, int soruNo, string secenek, byte r, byte g, byte b, int x, int y, string dosya)
        {
            OgrenciId = ogrenciId;
            SoruNo = soruNo;
            Secenek = secenek;
            R = r;
            G = g;
            B = b;
            X = x;
            Y = y;
            Dosya = dosya;
            Oturum = oturum;
            BransId = bransId;
        }

        public int OgrenciId { get; set; }
        public int Oturum { get; set; }
        public int BransId { get; set; }
        public int SoruNo { get; set; }
        public string Secenek { get; set; }
        public byte R { get; set; }
        public byte G { get; set; }
        public byte B { get; set; }
        public int X { get; set; }
        public int Y { get; set; }
        public string Dosya { get; set; }
    }

}
