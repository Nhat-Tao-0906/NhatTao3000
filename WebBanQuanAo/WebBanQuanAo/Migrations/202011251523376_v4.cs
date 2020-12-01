namespace WebBanQuanAo.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class v4 : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Size_Product", "IDProduct", "dbo.Products");
            DropForeignKey("dbo.Size_Product", "IDSize", "dbo.Sizes");
            DropIndex("dbo.Size_Product", new[] { "IDProduct" });
            DropIndex("dbo.Size_Product", new[] { "IDSize" });
            RenameColumn(table: "dbo.Size_Product", name: "IDSize", newName: "size_IDSize");
            AddColumn("dbo.Size_Product", "IDPro", c => c.Int(nullable: false));
            AlterColumn("dbo.Products", "QuantityRemaining", c => c.Int());
            AlterColumn("dbo.Size_Product", "size_IDSize", c => c.Int());
            CreateIndex("dbo.Size_Product", "size_IDSize");
            AddForeignKey("dbo.Size_Product", "size_IDSize", "dbo.Sizes", "IDSize");
            DropColumn("dbo.Products", "Quantity");
            DropColumn("dbo.Size_Product", "IDProduct");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Size_Product", "IDProduct", c => c.Int(nullable: false));
            AddColumn("dbo.Products", "Quantity", c => c.Int(nullable: false));
            DropForeignKey("dbo.Size_Product", "size_IDSize", "dbo.Sizes");
            DropIndex("dbo.Size_Product", new[] { "size_IDSize" });
            AlterColumn("dbo.Size_Product", "size_IDSize", c => c.Int(nullable: false));
            AlterColumn("dbo.Products", "QuantityRemaining", c => c.Int(nullable: false));
            DropColumn("dbo.Size_Product", "IDPro");
            RenameColumn(table: "dbo.Size_Product", name: "size_IDSize", newName: "IDSize");
            CreateIndex("dbo.Size_Product", "IDSize");
            CreateIndex("dbo.Size_Product", "IDProduct");
            AddForeignKey("dbo.Size_Product", "IDSize", "dbo.Sizes", "IDSize", cascadeDelete: true);
            AddForeignKey("dbo.Size_Product", "IDProduct", "dbo.Products", "IDProduct", cascadeDelete: true);
        }
    }
}
