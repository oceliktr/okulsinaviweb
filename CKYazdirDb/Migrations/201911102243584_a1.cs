namespace ODM.CKYazdirDb.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class a1 : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.CevapTxt", "OpaqId", c => c.Long(nullable: false));
            AlterColumn("dbo.Kutuk", "OpaqId", c => c.Long(nullable: false));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Kutuk", "OpaqId", c => c.Int(nullable: false));
            AlterColumn("dbo.CevapTxt", "OpaqId", c => c.Int(nullable: false));
        }
    }
}
