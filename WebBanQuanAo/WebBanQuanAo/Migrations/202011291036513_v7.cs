namespace WebBanQuanAo.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class v7 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Bills", "OrderStatus", c => c.String(nullable: false));
            AddColumn("dbo.Bills", "PayMentStatus", c => c.String(nullable: false));
            AddColumn("dbo.Products", "QuantitySold", c => c.Int());
            DropColumn("dbo.Bills", "Status");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Bills", "Status", c => c.String(nullable: false));
            DropColumn("dbo.Products", "QuantitySold");
            DropColumn("dbo.Bills", "PayMentStatus");
            DropColumn("dbo.Bills", "OrderStatus");
        }
    }
}
