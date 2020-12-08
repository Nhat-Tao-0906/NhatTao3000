namespace WebBanQuanAo.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class v10 : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Reviews",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        IDCus = c.Int(nullable: false),
                        IDProduct = c.Int(nullable: false),
                        DateReview = c.DateTime(nullable: false),
                        Ratio = c.Int(nullable: false),
                        Content = c.String(nullable: false),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.Customers", t => t.IDCus, cascadeDelete: true)
                .ForeignKey("dbo.Products", t => t.IDProduct, cascadeDelete: true)
                .Index(t => t.IDCus)
                .Index(t => t.IDProduct);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Reviews", "IDProduct", "dbo.Products");
            DropForeignKey("dbo.Reviews", "IDCus", "dbo.Customers");
            DropIndex("dbo.Reviews", new[] { "IDProduct" });
            DropIndex("dbo.Reviews", new[] { "IDCus" });
            DropTable("dbo.Reviews");
        }
    }
}
