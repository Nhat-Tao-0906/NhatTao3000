namespace WebBanQuanAo.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class v1 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Bills", "ShippingCost", c => c.Double(nullable: false));
            AddColumn("dbo.Bills", "BillTotal", c => c.Double(nullable: false));
            AddColumn("dbo.Bills", "Status", c => c.String(nullable: false));
            AddColumn("dbo.Bills", "DateCreated", c => c.DateTime(nullable: false));
            DropColumn("dbo.Bills", "PayDay");
            DropColumn("dbo.BillDetails", "ShippingCost");
            DropColumn("dbo.BillDetails", "Total");
        }
        
        public override void Down()
        {
            AddColumn("dbo.BillDetails", "Total", c => c.Double(nullable: false));
            AddColumn("dbo.BillDetails", "ShippingCost", c => c.Double(nullable: false));
            AddColumn("dbo.Bills", "PayDay", c => c.DateTime(nullable: false));
            DropColumn("dbo.Bills", "DateCreated");
            DropColumn("dbo.Bills", "Status");
            DropColumn("dbo.Bills", "BillTotal");
            DropColumn("dbo.Bills", "ShippingCost");
        }
    }
}
