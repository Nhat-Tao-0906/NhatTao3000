namespace WebBanQuanAo.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class v11 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Customers", "Image", c => c.String());
            AlterColumn("dbo.Bills", "OrderStatus", c => c.Int(nullable: false));
            AlterColumn("dbo.Bills", "PayMentStatus", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Bills", "PayMentStatus", c => c.String(nullable: false));
            AlterColumn("dbo.Bills", "OrderStatus", c => c.String(nullable: false));
            DropColumn("dbo.Customers", "Image");
        }
    }
}
