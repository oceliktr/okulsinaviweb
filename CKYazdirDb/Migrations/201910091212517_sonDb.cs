namespace ODM.CKYazdirDb.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class sonDb : DbMigration
    {
        public override void Up()
        {
            RenameTable(name: "dbo.CevapTxts", newName: "CevapTxt");
            CreateTable(
                "dbo.Branslar",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        BransAdi = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.DogruCevaplar",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        SinavId = c.Int(nullable: false),
                        BransId = c.Int(nullable: false),
                        KitapcikTuru = c.String(maxLength: 3),
                        Cevaplar = c.String(maxLength: 250),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.KarneSonuclari",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        SinavId = c.Int(nullable: false),
                        BransId = c.Int(nullable: false),
                        Ilce = c.String(),
                        KurumKodu = c.Int(nullable: false),
                        Sinif = c.Int(nullable: false),
                        Sube = c.String(),
                        KitapcikTuru = c.String(),
                        SoruNo = c.Int(nullable: false),
                        Dogru = c.Int(nullable: false),
                        Yanlis = c.Int(nullable: false),
                        Bos = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Kazanimlar",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Sinif = c.Int(nullable: false),
                        BransId = c.Int(nullable: false),
                        KazanimNo = c.String(),
                        KazanimAdi = c.String(),
                        KazanimAdiOgrenci = c.String(),
                        Sorulari = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            AddColumn("dbo.CevapTxt", "BransId", c => c.Int(nullable: false));
            DropColumn("dbo.CevapTxt", "KurumKodu");
            DropColumn("dbo.CevapTxt", "OgrenciNo");
            DropColumn("dbo.CevapTxt", "Adi");
            DropColumn("dbo.CevapTxt", "Soyadi");
            DropColumn("dbo.CevapTxt", "Sinifi");
            DropColumn("dbo.CevapTxt", "Sube");
            DropColumn("dbo.CevapTxt", "DersKodu");
            DropTable("dbo.Cevaplar");
        }
        
        public override void Down()
        {
            CreateTable(
                "dbo.Cevaplar",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        SinavId = c.Int(nullable: false),
                        DersId = c.Int(nullable: false),
                        KitapcikTuru = c.String(maxLength: 3),
                        DogruCevaplar = c.String(maxLength: 250),
                    })
                .PrimaryKey(t => t.Id);
            
            AddColumn("dbo.CevapTxt", "DersKodu", c => c.Int(nullable: false));
            AddColumn("dbo.CevapTxt", "Sube", c => c.String(maxLength: 3));
            AddColumn("dbo.CevapTxt", "Sinifi", c => c.Int(nullable: false));
            AddColumn("dbo.CevapTxt", "Soyadi", c => c.String(maxLength: 50));
            AddColumn("dbo.CevapTxt", "Adi", c => c.String(maxLength: 50));
            AddColumn("dbo.CevapTxt", "OgrenciNo", c => c.Int(nullable: false));
            AddColumn("dbo.CevapTxt", "KurumKodu", c => c.Int(nullable: false));
            DropColumn("dbo.CevapTxt", "BransId");
            DropTable("dbo.Kazanimlar");
            DropTable("dbo.KarneSonuclari");
            DropTable("dbo.DogruCevaplar");
            DropTable("dbo.Branslar");
            RenameTable(name: "dbo.CevapTxt", newName: "CevapTxts");
        }
    }
}
