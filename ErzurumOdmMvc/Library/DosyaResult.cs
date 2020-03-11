namespace ErzurumOdmMvc.Library
{
    public class DosyaResult
    {
        public string Mesaj { get; set; }
        public string Alert { get; set; }
        public string Dosya { get; set; }

        public DosyaResult()
        {
        }

        public DosyaResult(string mesaj, string alert, string dosya)
        {
            Mesaj = mesaj;
            Alert = alert;
            Dosya = dosya;
        }
    }
}