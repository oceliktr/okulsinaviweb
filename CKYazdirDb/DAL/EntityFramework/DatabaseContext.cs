using System.Data.Entity;
using ODM.CKYazdirDb.Entities;
using ODM.CKYazdirDb.Model;


namespace ODM.CKYazdirDb.DAL
{
    public class DatabaseContext:DbContext
    {
        public DbSet<Ayarlar> Ayarlar { get; set; }
        public DbSet<DogruCevap> Cevaplar { get; set; }
        public DbSet<Kutuk> Kutuk { get; set; }
        public DbSet<Kazanim> Kazanimlar { get; set; }
        public DbSet<Brans> Branslar { get; set; }
        public DbSet<KarneSonuc> KarneSonuclari { get; set; }
        public DbSet<OgrenciCevabi> OgrenciCevaplari { get; set; }
        public DbSet<CkSablon> Sablonlar { get; set; }
        public DbSet<OptikKonum> OptikKonumlar { get; set; }
        public DatabaseContext()
        {
           // Database.SetInitializer(new MyInitializer());
        }
    }
}
