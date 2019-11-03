using System.Data.Entity;
using ODM.CKYazdirDb.Model;


namespace ODM.CKYazdirDb.DAL
{
    public class DatabaseContext:DbContext
    {
        public DbSet<Ayarlar> Ayarlar { get; set; }
        public DbSet<DogruCevap> Cevaplar { get; set; }
        public DbSet<CevapTxt> CevapTxt { get; set; }
        public DbSet<Kutuk> Kutuk { get; set; }
        public DbSet<Kazanim> Kazanimlar { get; set; }
        public DbSet<Brans> Branslar { get; set; }
        public DbSet<KarneSonuc> KarneSonuclari { get; set; }

        public DatabaseContext()
        {
           // Database.SetInitializer(new MyInitializer());
        }
    }
}
