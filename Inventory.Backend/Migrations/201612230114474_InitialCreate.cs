namespace Inventory.Backend.Migrations {
    using System;
    using System.Data.Entity.Migrations;

    public partial class InitialCreate : DbMigration {
        public override void Up() {
            CreateTable(
                "dbo.Article",
                c => new {
                    ArticleID = c.Int(nullable: false, identity: true),
                    StoreID = c.Int(nullable: false),
                    Name = c.String(),
                    Description = c.String(),
                    Price = c.Decimal(nullable: false, precision: 18, scale: 2),
                    TotalInShelf = c.Decimal(nullable: false, precision: 18, scale: 2),
                    TotalInVault = c.Decimal(nullable: false, precision: 18, scale: 2),
                })
                .PrimaryKey(t => t.ArticleID)
                .ForeignKey("dbo.Store", t => t.StoreID, cascadeDelete: true)
                .Index(t => t.StoreID);

            CreateTable(
                "dbo.Store",
                c => new {
                    StoreID = c.Int(nullable: false, identity: true),
                    Name = c.String(),
                    Address = c.String(),
                })
                .PrimaryKey(t => t.StoreID);

        }

        public override void Down() {
            DropForeignKey("dbo.Article", "StoreID", "dbo.Store");
            DropIndex("dbo.Article", new[] { "StoreID" });
            DropTable("dbo.Store");
            DropTable("dbo.Article");
        }
    }
}
