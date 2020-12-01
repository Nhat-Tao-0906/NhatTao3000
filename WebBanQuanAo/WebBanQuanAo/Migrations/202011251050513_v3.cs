namespace WebBanQuanAo.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class v3 : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Size_Product",
                c => new
                    {
                        IDSize_Product = c.Int(nullable: false, identity: true),
                        IDProduct = c.Int(nullable: false),
                        IDSize = c.Int(nullable: false),
                        Quantity = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.IDSize_Product)
                .ForeignKey("dbo.Products", t => t.IDProduct, cascadeDelete: true)
                .ForeignKey("dbo.Sizes", t => t.IDSize, cascadeDelete: true)
                .Index(t => t.IDProduct)
                .Index(t => t.IDSize);
            
            CreateTable(
                "dbo.Sizes",
                c => new
                    {
                        IDSize = c.Int(nullable: false, identity: true),
                        _Size = c.String(nullable: false),
                    })
                .PrimaryKey(t => t.IDSize);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Size_Product", "IDSize", "dbo.Sizes");
            DropForeignKey("dbo.Size_Product", "IDProduct", "dbo.Products");
            DropIndex("dbo.Size_Product", new[] { "IDSize" });
            DropIndex("dbo.Size_Product", new[] { "IDProduct" });
            DropTable("dbo.Sizes");
            DropTable("dbo.Size_Product");
        }
    }
}
