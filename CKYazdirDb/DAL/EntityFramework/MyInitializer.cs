using System.Data.Entity;


namespace ODM.CKYazdirDb.DAL
{
    public class MyInitializer : CreateDatabaseIfNotExists<DatabaseContext>
    {
        protected override void Seed(DatabaseContext context)
        {
           
            Ayarlar info = new Ayarlar
            {
                IlAdi = "Erzurum",
                CkSablon = "",
                Logo = "",
                SinifListesiSablon = ""
            };
            context.Ayarlar.Add(info);

            context.SaveChanges();
        }
    }
}
