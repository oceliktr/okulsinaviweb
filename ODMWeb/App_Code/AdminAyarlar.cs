public class AdminAyarlar
{
    // root : id 1 olan yönetici
    // editör : diðer yöneticler
    public static AdminAyarInfo Ayarlar()
    {
        AdminAyarInfo ayar = new AdminAyarInfo
        {
            KategoriMenu = false,
            SliderMenu = true,
            Maillistesi = false,
            Popup = false
        };
        //Root dýþýndakine editöre de göstermek için true

        return ayar;
    }
}