namespace ODM.Model
{
    public class Koordinat
    {
        public int KrdX { get; set; }
        public int KrdY { get; set; }
        public Koordinat()
        { }

        public Koordinat(int krdX, int krdY)
        {
            KrdX = krdX;
            KrdY = krdY;
        }
    }
}
