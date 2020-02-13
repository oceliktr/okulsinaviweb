using System;
using System.IO;
using System.Security.Cryptography;
using System.Text;

namespace ErzurumOdmMvc.Common.Library
{
    public static class SifrelemeIslemleri
    {
        public static string IdSifrele(this int id)
        {
            var s = id.ToString();
            s = s.Trim();
            s = s.Replace("1", "b");
            s = s.Replace("2", "i");
            s = s.Replace("3", "u");
            s = s.Replace("4", "d");
            s = s.Replace("5", "x");
            s = s.Replace("6", "a");
            s = s.Replace("7", "y");
            s = s.Replace("8", "s");
            s = s.Replace("9", "w");
            s = s.Replace("0", "j");

            return s;
        }

        /// <summary>
        /// İd adresleri için şifre çözme
        /// </summary>
        /// <param name="s">querysqtring değeri</param>
        /// <returns></returns>
        public static string IdSifreCoz(this string s)
        {
            if (string.IsNullOrEmpty(s)) return "";
            s = s.Trim();
            s = s.Replace("b", "1");
            s = s.Replace("i", "2");
            s = s.Replace("u", "3");
            s = s.Replace("d", "4");
            s = s.Replace("x", "5");
            s = s.Replace("a", "6");
            s = s.Replace("y", "7");
            s = s.Replace("s", "8");
            s = s.Replace("w", "9");
            s = s.Replace("j", "0");

            return s;
        }

        // herhangi birşey olabilir
        private const string passPhrase = "Besiktas";

        // herhangi birşey olabilir
        private const string saltValue = "Erzurumlu";

        // SHA1 ya da MD5
        private const string hashAlgorithm = "SHA1";

        // herhangi bir sayı olabilir
        private const int passwordIterations = 2;

        // 16 byte olmalı
        private const string initVector = "@1B2c3Dq@5F6x7H8";

        // kaç bit şifreleme?
        private const int keySize = 256;

        public static string Sifrele(this string plainText)
        {
            var initVectorBytes = Encoding.ASCII.GetBytes(initVector);
            var saltValueBytes = Encoding.ASCII.GetBytes(saltValue);

            var plainTextBytes = Encoding.UTF8.GetBytes(plainText);

            var password =
                new PasswordDeriveBytes(passPhrase, saltValueBytes, hashAlgorithm, passwordIterations);

            var keyBytes = password.GetBytes(keySize / 8);

            var symmetricKey = new RijndaelManaged { Mode = CipherMode.CBC };


            var encryptor =
                symmetricKey.CreateEncryptor(keyBytes, initVectorBytes);

            var memoryStream = new MemoryStream();

            var cryptoStream =
                new CryptoStream(memoryStream, encryptor, CryptoStreamMode.Write);
            cryptoStream.Write(plainTextBytes, 0, plainTextBytes.Length);

            cryptoStream.FlushFinalBlock();

            var cipherTextBytes = memoryStream.ToArray();

            memoryStream.Close();
            cryptoStream.Close();

            var cipherText = Convert.ToBase64String(cipherTextBytes);

            return cipherText;
        }


        public static string SifreCoz(this string cipherText)
        {
            try
            {
                var initVectorBytes = Encoding.ASCII.GetBytes(initVector);
                var saltValueBytes = Encoding.ASCII.GetBytes(saltValue);

                var cipherTextBytes = Convert.FromBase64String(cipherText);

                var password =
                    new PasswordDeriveBytes(passPhrase, saltValueBytes, hashAlgorithm, passwordIterations);

                var keyBytes = password.GetBytes(keySize / 8);

                var symmetricKey = new RijndaelManaged { Mode = CipherMode.CBC };


                var decryptor =
                    symmetricKey.CreateDecryptor(keyBytes, initVectorBytes);

                var memoryStream = new MemoryStream(cipherTextBytes);

                var cryptoStream =
                    new CryptoStream(memoryStream, decryptor, CryptoStreamMode.Read);

                var plainTextBytes = new byte[cipherTextBytes.Length];

                var decryptedByteCount =
                    cryptoStream.Read(plainTextBytes, 0, plainTextBytes.Length);

                memoryStream.Close();
                cryptoStream.Close();

                var plainText =
                    Encoding.UTF8.GetString(plainTextBytes, 0, decryptedByteCount);

                return plainText;
            }
            catch (Exception ex)
            {

                return ex.Message;
            }
        }


        public static byte[] ByteDonustur(string deger)
        {

            var ByteConverter = new UnicodeEncoding();
            return ByteConverter.GetBytes(deger);

        }

        public static byte[] Byte8(string deger)
        {
            var arrayChar = deger.ToCharArray();
            var arrayByte = new byte[arrayChar.Length];
            for (var i = 0; i < arrayByte.Length; i++)
            {
                arrayByte[i] = Convert.ToByte(arrayChar[i]);
            }

            return arrayByte;
        }

        public static string MD5Sifrele(this string str)
        {
            if (string.IsNullOrEmpty(str))
            {
                throw new ArgumentNullException("Şifrelenecek veri yok");
            }
            else
            {
                var password = new MD5CryptoServiceProvider();
                var aryPassword = ByteDonustur(str);
                var aryHash = password.ComputeHash(aryPassword);
                return BitConverter.ToString(aryHash).Replace("-", "");
            }
        }

        public static string SHA1Sifrele(this string strGiris)
        {
            if (string.IsNullOrEmpty(strGiris))
            {
                throw new ArgumentNullException("Şifrelenecek veri yok.");
            }
            else
            {
                var sifre = new SHA1CryptoServiceProvider();
                var arySifre = ByteDonustur(strGiris);
                var aryHash = sifre.ComputeHash(arySifre);
                return BitConverter.ToString(aryHash).Replace("-", "");
            }
        }

        public static string SHA256Sifrele(this string strGiris)
        {
            if (string.IsNullOrEmpty(strGiris))
            {
                throw new ArgumentNullException("Şifrelenecek Veri Yok");
            }
            else
            {
                var sifre = new SHA256Managed();
                var arySifre = ByteDonustur(strGiris);
                var aryHash = sifre.ComputeHash(arySifre);
                return BitConverter.ToString(aryHash).Replace("-", "");
            }
        }

        public static string SHA384Sifrele(this string strGiris)
        {
            if (string.IsNullOrEmpty(strGiris))
            {
                throw new ArgumentNullException("Şifrelenecek veri bulunamadı.");
            }
            else
            {
                var sifre = new SHA384Managed();
                var arySifre = ByteDonustur(strGiris);
                var aryHash = sifre.ComputeHash(arySifre);
                return BitConverter.ToString(aryHash).Replace("-", "");
            }
        }

        public static string SHA512Sifrele(this string strGiris)
        {
            if (string.IsNullOrEmpty(strGiris))
            {
                throw new ArgumentNullException("Şifrelenecek veri yok.");
            }
            else
            {
                var sifre = new SHA512Managed();
                var arySifre = ByteDonustur(strGiris);
                var aryHash = sifre.ComputeHash(arySifre);
                return BitConverter.ToString(aryHash).Replace("-", "");
            }
        }

        public static string BenzersizKodUret()
        {
            DateTime tarih = DateTime.Now;

            string yil = tarih.AddYears(25).Year.ToString().Substring(2, 2);
            string aygun = (tarih.Month + 25) + (tarih.Day + 25).ToString();
            string saat = tarih.Hour.ToString();
            string dakika = tarih.Minute.ToString();
            //  string saniye = tarih.Second.ToString();
            string salise = tarih.Millisecond.ToString();
            string key = RastgeleMetinUret(1);
            string key2 = RastgeleMetinUret(1);

            string result = $"{key}{yil}{aygun}{saat}{key2}{dakika}{salise}";

            return result;
        }
        static readonly Random random = new Random();
        public static string RastgeleMetinUret(int adet)
        {
            string s = "";
            for (int i = 0; i < adet; i++)
            {
                char c = Convert.ToChar(65 + random.Next(26));
                s = string.Concat(s, Convert.ToString(c));
            }
            return s;
        }

    }
}