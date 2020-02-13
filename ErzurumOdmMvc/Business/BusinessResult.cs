using ErzurumOdmMvc.Common.Mesajlar;
using System.Collections.Generic;

namespace ErzurumOdmMvc.Business
{
    public class BusinessResult<T> where T : class
    {
        public List<UyariMesaji> Uyarilar { get; set; }
        public T Result { get; set; }

        public BusinessResult()
        {
            Uyarilar = new List<UyariMesaji>();
        }

        public void Ekle(string mesaj)
        {
            Uyarilar.Add(new UyariMesaji { Mesaj = mesaj });
        }
    }
}
