using System;

namespace ErzurumOdmMvc.Common.Library
{
    public static class ConvertIslemleri
    {
        public static string YeniGuid()
        {
            return Guid.NewGuid().ToString().Replace("-", "").ToUpper();
        }
        public static bool IsInteger(this object obj)
        {
            try
            {
                if (obj == null) throw new Exception();
                Convert.ToInt32(obj);
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public static int ToInt32(this object obj)
        {
            try
            {
                if (obj == null) throw new Exception();
                int x = Convert.ToInt32(obj);
                return x;
            }
            catch (Exception)
            {
                return 0;
            }
        }

        public static long ToInt64(this object obj)
        {
            try
            {
                if (obj == null) throw new Exception();
                long x = Convert.ToInt64(obj);
                return x;
            }
            catch (Exception)
            {
                return 0;
            }
        }

        public static double ToDouble(this object obj)
        {
            try
            {
                if (obj == null) throw new Exception();
                double x = Convert.ToDouble(obj);
                return x;
            }
            catch (Exception)
            {
                return 1;
            }
        }

        public static decimal ToDecimal(this object obj)
        {
            try
            {
                if (obj == null) throw new Exception();
                decimal x = Convert.ToDecimal(obj);
                return x;
            }
            catch (Exception)
            {
                return 1;
            }
        }

        public static DateTime ToDateTime(this object obj)
        {
            try
            {
                if (obj == null) throw new Exception();
                string g = obj.ToString();
                DateTime x = Convert.ToDateTime(g);
                return x;
            }
            catch (Exception)
            {
                return DateTime.Now;
            }
        }

        public static bool ToBoolean(this object s)
        {
            try
            {
                return Convert.ToBoolean(s);
            }
            catch (Exception)
            {
                return false;
            }
        }
        public static string SayiKisalt(this int sayi)
        {
            string sonuc;
            if (sayi > 1000000 && sayi < 999999999) //milyonlar
            {
                decimal sonucx = sayi / (decimal)1000000;
                sonuc = sonucx.ToString("##.## 'M'");
            }
            else if (sayi > 1000000000) //milyonlardan büyük sayılar
            {
                decimal sonucx = sayi / (decimal)100000000;
                sonuc = sonucx.ToString("##.## '~'");
            }
            else
            {
                sonuc = sayi.ToString();
            }

            return sonuc;
        }
    }
}
