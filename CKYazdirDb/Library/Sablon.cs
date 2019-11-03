using System.Collections.Generic;

namespace ODM.CKYazdirDb.Library
{
    public  class Sablon
    {
        public string Id { get; set; }
        public string SablonAdi { get; set; }
        public Sablon()
        {

        }

        public Sablon(string id,string sablonAdi)
        {
            Id = id;
            SablonAdi = sablonAdi;
        }
        public List<Sablon> Sablonlar()
        {
            List<Sablon> sablons = new List<Sablon>
            {
                new Sablon("1", "Tek Dersli - A4"),
                new Sablon("2", "Tek Dersli - A5"),
                new Sablon("3", "Üç Dersli - A4")
            };

            return sablons;
        }
    }
}
