using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ErzurumOdmMvc.Common.Library
{
   public static class UrunFiyat
    {
        public static decimal SatisFiyati(this decimal fiyati, decimal indirimliFiyat)
        {
            decimal fiyat = 0;
            if (indirimliFiyat == 0 || indirimliFiyat > fiyati)
            {
                fiyat = fiyati;
            }
            else
            {
                fiyat = indirimliFiyat;

            }

            return fiyat;
        }
    }
}
