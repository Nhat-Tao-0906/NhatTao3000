namespace WebBanQuanAo.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class v5 : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Size_Product", "size_IDSize", "dbo.Sizes");
            DropIndex("dbo.Size_Product", new[] { "size_IDSize" });
            AddColumn("dbo.Size_Product", "size", c => c.String());
            DropColumn("dbo.Size_Product", "size_IDSize");
            DropTable("dbo.Sizes");
        }
        
        public override void Down()
        {
            CreateTable(
                "dbo.Sizes",
                c => new
                    {
                        IDSize = c.Int(nullable: false, identity: true),
                        _Size = c.String(nullable: false),
                    })
                .PrimaryKey(t => t.IDSize);
            
            AddColumn("dbo.Size_Product", "size_IDSize", c => c.Int());
            DropColumn("dbo.Size_Product", "size");
            CreateIndex("dbo.Size_Product", "size_IDSize");
            AddForeignKey("dbo.Size_Product", "size_IDSize", "dbo.Sizes", "IDSize");
        }
    }
}
