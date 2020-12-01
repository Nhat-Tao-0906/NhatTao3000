namespace WebBanQuanAo.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class v8 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.BillDetails", "Size", c => c.String(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.BillDetails", "Size");
        }
    }
}
