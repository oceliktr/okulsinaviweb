namespace ODM.CKYazdirDb.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class os : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.OgrenciCevaplari",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        OpaqId = c.Long(nullable: false),
                        Ilce = c.String(),
                        KurumKodu = c.Int(nullable: false),
                        Sinif = c.Int(nullable: false),
                        Sube = c.String(),
                        KatilimDurumu = c.String(maxLength: 3),
                        KitapcikTuru = c.String(maxLength: 3),
                        Cevaplar = c.String(maxLength: 350),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.OgrenciCevaplari");
        }
    }
}
