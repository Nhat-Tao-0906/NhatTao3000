namespace WebBanQuanAo.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class v9 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Accounts", "Status", c => c.Boolean(nullable: false));
            AddColumn("dbo.Customers", "Status", c => c.Boolean(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Customers", "Status");
            DropColumn("dbo.Accounts", "Status");
        }
    }
}
