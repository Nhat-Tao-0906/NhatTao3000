namespace WebBanQuanAo.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class v6 : DbMigration
    {
        public override void Up()
        {
            CreateIndex("dbo.Size_Product", "IDPro");
            AddForeignKey("dbo.Size_Product", "IDPro", "dbo.Products", "IDProduct", cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Size_Product", "IDPro", "dbo.Products");
            DropIndex("dbo.Size_Product", new[] { "IDPro" });
        }
    }
}
