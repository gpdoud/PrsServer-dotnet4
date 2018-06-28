namespace PrsServer.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addedvendor : DbMigration
    {
        public override void Up()
        {
            RenameIndex(table: "dbo.Users", name: "IX_Username", newName: "IDX_Username");
            CreateTable(
                "dbo.Vendors",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Code = c.String(nullable: false, maxLength: 20),
                        Name = c.String(nullable: false, maxLength: 50),
                        Address = c.String(nullable: false, maxLength: 50),
                        City = c.String(nullable: false, maxLength: 50),
                        State = c.String(nullable: false, maxLength: 2),
                        Zip = c.String(nullable: false, maxLength: 10),
                        Phone = c.String(nullable: false, maxLength: 12),
                        Email = c.String(nullable: false, maxLength: 250),
                        IsPreApproved = c.Boolean(nullable: false),
                        Active = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .Index(t => t.Code, unique: true, name: "IDX_VendorCode");
            
        }
        
        public override void Down()
        {
            DropIndex("dbo.Vendors", "IDX_VendorCode");
            DropTable("dbo.Vendors");
            RenameIndex(table: "dbo.Users", name: "IDX_Username", newName: "IX_Username");
        }
    }
}
