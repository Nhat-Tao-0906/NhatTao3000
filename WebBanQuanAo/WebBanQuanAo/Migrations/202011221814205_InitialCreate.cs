namespace WebBanQuanAo.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitialCreate : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Accounts",
                c => new
                    {
                        IDCus = c.Int(nullable: false),
                        UserName = c.String(nullable: false),
                        PassWord = c.String(nullable: false),
                    })
                .PrimaryKey(t => t.IDCus)
                .ForeignKey("dbo.Customers", t => t.IDCus)
                .Index(t => t.IDCus);
            
            CreateTable(
                "dbo.Customers",
                c => new
                    {
                        IDCus = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false),
                        Email = c.String(nullable: false),
                        Phone = c.String(nullable: false),
                        Address = c.String(nullable: false),
                    })
                .PrimaryKey(t => t.IDCus);
            
            CreateTable(
                "dbo.Bills",
                c => new
                    {
                        IDBill = c.Int(nullable: false, identity: true),
                        IDCus = c.Int(nullable: false),
                        PayDay = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.IDBill)
                .ForeignKey("dbo.Customers", t => t.IDCus, cascadeDelete: true)
                .Index(t => t.IDCus);
            
            CreateTable(
                "dbo.BillDetails",
                c => new
                    {
                        IDBillDetails = c.Int(nullable: false, identity: true),
                        IDProduct = c.Int(nullable: false),
                        IDBill = c.Int(nullable: false),
                        Quantity = c.Int(nullable: false),
                        UnitPrice = c.Double(nullable: false),
                        ShippingCost = c.Double(nullable: false),
                        Total = c.Double(nullable: false),
                    })
                .PrimaryKey(t => t.IDBillDetails)
                .ForeignKey("dbo.Bills", t => t.IDBill, cascadeDelete: true)
                .ForeignKey("dbo.Products", t => t.IDProduct, cascadeDelete: true)
                .Index(t => t.IDProduct)
                .Index(t => t.IDBill);
            
            CreateTable(
                "dbo.Products",
                c => new
                    {
                        IDProduct = c.Int(nullable: false, identity: true),
                        IDCategory = c.Int(nullable: false),
                        IDChatLieu = c.Int(nullable: false),
                        IDThuongHieu = c.Int(nullable: false),
                        NameProduct = c.String(nullable: false),
                        Image1 = c.String(),
                        Image2 = c.String(),
                        Image3 = c.String(),
                        Image4 = c.String(),
                        Image5 = c.String(),
                        Price = c.Double(nullable: false),
                        Size = c.String(nullable: false),
                        Quantity = c.Int(nullable: false),
                        QuantityRemaining = c.Int(nullable: false),
                        Description = c.String(nullable: false),
                        ImportDate = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.IDProduct)
                .ForeignKey("dbo.Categories", t => t.IDCategory, cascadeDelete: true)
                .ForeignKey("dbo.ChatLieux", t => t.IDChatLieu, cascadeDelete: true)
                .ForeignKey("dbo.ThuongHieux", t => t.IDThuongHieu, cascadeDelete: true)
                .Index(t => t.IDCategory)
                .Index(t => t.IDChatLieu)
                .Index(t => t.IDThuongHieu);
            
            CreateTable(
                "dbo.Categories",
                c => new
                    {
                        IDCategory = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false),
                        Description = c.String(nullable: false),
                    })
                .PrimaryKey(t => t.IDCategory);
            
            CreateTable(
                "dbo.ChatLieux",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Ten = c.String(),
                    })
                .PrimaryKey(t => t.ID);
            
            CreateTable(
                "dbo.ThuongHieux",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                    })
                .PrimaryKey(t => t.ID);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Accounts", "IDCus", "dbo.Customers");
            DropForeignKey("dbo.Bills", "IDCus", "dbo.Customers");
            DropForeignKey("dbo.BillDetails", "IDProduct", "dbo.Products");
            DropForeignKey("dbo.Products", "IDThuongHieu", "dbo.ThuongHieux");
            DropForeignKey("dbo.Products", "IDChatLieu", "dbo.ChatLieux");
            DropForeignKey("dbo.Products", "IDCategory", "dbo.Categories");
            DropForeignKey("dbo.BillDetails", "IDBill", "dbo.Bills");
            DropIndex("dbo.Products", new[] { "IDThuongHieu" });
            DropIndex("dbo.Products", new[] { "IDChatLieu" });
            DropIndex("dbo.Products", new[] { "IDCategory" });
            DropIndex("dbo.BillDetails", new[] { "IDBill" });
            DropIndex("dbo.BillDetails", new[] { "IDProduct" });
            DropIndex("dbo.Bills", new[] { "IDCus" });
            DropIndex("dbo.Accounts", new[] { "IDCus" });
            DropTable("dbo.ThuongHieux");
            DropTable("dbo.ChatLieux");
            DropTable("dbo.Categories");
            DropTable("dbo.Products");
            DropTable("dbo.BillDetails");
            DropTable("dbo.Bills");
            DropTable("dbo.Customers");
            DropTable("dbo.Accounts");
        }
    }
}
