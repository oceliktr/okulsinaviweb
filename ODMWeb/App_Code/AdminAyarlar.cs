public class AdminAyarlar
{
    // root : id 1 olan y�netici
    // edit�r : di�er y�neticler
    public static AdminAyarInfo Ayarlar()
    {
        AdminAyarInfo ayar = new AdminAyarInfo
        {
            KategoriMenu = false,
            SliderMenu = true,
            Maillistesi = false,
            Popup = false
        };
        //Root d���ndakine edit�re de g�stermek i�in true

        return ayar;
    }
}