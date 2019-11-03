using System;
using System.Web;
using System.Text;
using System.Security.Cryptography;
using System.IO;

/// <summary>
/// Summary description for Sifreleme
/// </summary>
public static class Sifreleme
{
    public static string IdSifrele(this int id)
    {
        //int adet = 0;
        //string m = "";
        string s = id.ToString();
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

        //if (s.Length <= 4)
        //    adet = 4 - s.Length;
        //else
        //    adet = 4 - s.Length;

        //char[] cr = "ceghfklmnoqprtvz".ToCharArray();

        //Random r = new Random();
        //for (int i = 0; i < adet; i++)
        //{
        //    m += cr[r.Next(0, cr.Length - 1)].ToString();
        //}
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


        //char[] cr = "ceghfklmnoqprtvz".ToCharArray();


        //for (int i = 0; i < cr.Length; i++)
        //{
        //    s = s.Replace(cr[i].ToString(), "");
        //}
        return s;

    }
    // herhangi birşey olabilir
    private const string PassPhrase = "Besiktas";

    // herhangi birşey olabilir
    private const string SaltValue = "Erzurumlu";

    // SHA1 ya da MD5
    private const string HashAlgorithm = "SHA1";

    // herhangi bir sayı olabilir
    private const int PasswordIterations = 2;

    // 16 byte olmalı
    private const string InitVector = "@1B2c3Dq@5F6x7H8";

    // kaç bit şifreleme?
    private const int KeySize = 256;

    public static string Sifrele(this string plainText)
    {
        var initVectorBytes = Encoding.ASCII.GetBytes(InitVector);
        var saltValueBytes = Encoding.ASCII.GetBytes(SaltValue);

        var plainTextBytes = Encoding.UTF8.GetBytes(plainText);

        var password =
            new PasswordDeriveBytes(PassPhrase, saltValueBytes, HashAlgorithm, PasswordIterations);

        var keyBytes = password.GetBytes(KeySize / 8);

        var symmetricKey = new RijndaelManaged();

        symmetricKey.Mode = CipherMode.CBC;

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

    public static string Sifrele(this string plainText, bool urlEncode)
    {
        if (urlEncode)
            return HttpContext.Current.Server.UrlEncode(plainText.Sifrele());
        else
            return plainText.Sifrele();
    }

    public static string SifreCoz(this string cipherText)
    {
        try
        {
            var initVectorBytes = Encoding.ASCII.GetBytes(InitVector);
            var saltValueBytes = Encoding.ASCII.GetBytes(SaltValue);

            var cipherTextBytes = Convert.FromBase64String(cipherText);

            var password =
                new PasswordDeriveBytes(PassPhrase, saltValueBytes, HashAlgorithm, PasswordIterations);

            var keyBytes = password.GetBytes(KeySize / 8);

            var symmetricKey = new RijndaelManaged();

            symmetricKey.Mode = CipherMode.CBC;

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

    public static string SifreCoz(this string cipherText, bool urlDecode)
    {
        if (urlDecode)
            return HttpContext.Current.Server.UrlDecode(cipherText).SifreCoz();
        else
            return cipherText.SifreCoz();
    }

    public static byte[] ByteDonustur(string deger)
    {

        var byteConverter = new UnicodeEncoding();
        return byteConverter.GetBytes(deger);

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

    public static string Md5Sifrele(this string strGiris)
    {
        if (string.IsNullOrEmpty(strGiris))
        {
            throw new ArgumentNullException(@"Şifrelenecek veri yok");
        }
        var sifre = new MD5CryptoServiceProvider();
        var arySifre = ByteDonustur(strGiris);
        var aryHash = sifre.ComputeHash(arySifre);
        return BitConverter.ToString(aryHash).Replace("-", "");
    }
    public static string Sha1Sifrele(this string strGiris)
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
    public static string Sha256Sifrele(this string strGiris)
    {
        if (string.IsNullOrEmpty(strGiris))
        {
            throw new ArgumentNullException("Şifrelenecek Veri Yok");
        }
        var sifre = new SHA256Managed();
        var arySifre = ByteDonustur(strGiris);
        var aryHash = sifre.ComputeHash(arySifre);
        return BitConverter.ToString(aryHash).Replace("-", "");
    }
    public static string Sha384Sifrele(this string strGiris)
    {
        if (string.IsNullOrEmpty(strGiris))
        {
            throw new ArgumentNullException("Şifrelenecek veri bulunamadı.");
        }
        var sifre = new SHA384Managed();
        var arySifre = ByteDonustur(strGiris);
        var aryHash = sifre.ComputeHash(arySifre);
        return BitConverter.ToString(aryHash).Replace("-", "");
    }
    public static string Sha512Sifrele(this string strGiris)
    {
        if (string.IsNullOrEmpty(strGiris))
        {
            throw new ArgumentNullException("Şifrelenecek veri yok.");
        }
        var sifre = new SHA512Managed();
        var arySifre = ByteDonustur(strGiris);
        var aryHash = sifre.ComputeHash(arySifre);
        return BitConverter.ToString(aryHash).Replace("-","");
    }

}