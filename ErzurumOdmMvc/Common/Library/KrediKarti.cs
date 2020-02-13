using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ErzurumOdmMvc.Common.Library
{
   public class KrediKarti
    {
        private bool Dogrulama(string pKartNo)
        {
            int toplam = 0;
            for (int i = 0; i < 16; i++)
            {
                int sayi = Convert.ToInt32(pKartNo[i].ToString());
                if (i % 2 == 0)
                {
                    sayi = sayi * 2;
                    if (sayi.ToString().Length == 2)
                        sayi = Convert.ToInt32(sayi.ToString().Substring(0, 1)) + Convert.ToInt32(sayi.ToString().Substring(1, 1));
                }
                toplam += sayi;
            }
            if (toplam % 10 == 0)
                return true;
            else
                return false;
        }
    }
}
