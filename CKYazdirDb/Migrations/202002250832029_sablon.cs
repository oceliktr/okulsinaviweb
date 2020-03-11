namespace ODM.CKYazdirDb.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class sablon : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.OptikKonumlar", "Sablon", c => c.String(maxLength: 350));
        }
        
        public override void Down()
        {
            DropColumn("dbo.OptikKonumlar", "Sablon");
        }
    }
}
