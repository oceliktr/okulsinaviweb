using ODM.CKYazdirDb.Entities;
using System.Data.Entity;

namespace ODM.CKYazdirDb.DAL.EntityFramework
{
    public class MyInitializer : CreateDatabaseIfNotExists<DatabaseContext>
    {
        protected override void Seed(DatabaseContext context)
        {
           
            Ayarlar info = new Ayarlar
            {
                IlAdi = "Erzurum",
                Logo = "",
                SinifListesiSablon = ""
            };
            context.Ayarlar.Add(info);

            OptikKonum ok= new OptikKonum
            {
                BubleArtim = 0,
                BubleH = 0,
                BubleW = 0,
                BubleX = 0,
                BubleY = 0,
                OgrBilgiH = 0,
                OgrBilgiX = 0,
                OgrBilgiY = 0
            };
            context.OptikKonumlar.Add(ok);
        context.SaveChanges();
        }
    }
}
