namespace ErzurumOdmMvc.Library
{
    public class FotografResult
    {
        public string Mesaj { get; set; }
        public string Alert { get; set; }
        public string Foto { get; set; }

        public FotografResult()
        {
        }

        public FotografResult(string mesaj, string alert, string foto)
        {
            Mesaj = mesaj;
            Alert = alert;
            Foto = foto;
        }
    }
}