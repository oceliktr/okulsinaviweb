namespace ODM.Kutuphanem
{
    public class KitapcikTuru
    {
        public string Kitapcik { get; set; }
        public byte R { get; set; }
        public byte G { get; set; }
        public byte B { get; set; }
        public int X { get; set; }
        public int Y { get; set; }
        public string Dosya { get; set; }

        public KitapcikTuru()
        { }

        public KitapcikTuru(string kitapcik, byte r, byte g, byte b, int x, int y, string dosya)
        {
            Kitapcik = kitapcik;
            R = r;
            G = g;
            B = b;
            X = x;
            Y = y;
            Dosya = dosya;
        }
    }
}
