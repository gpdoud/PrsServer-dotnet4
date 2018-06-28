namespace PrsServer.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class removedatecreatedfrompr : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.PurchaseRequests", "DateCreated");
        }
        
        public override void Down()
        {
            AddColumn("dbo.PurchaseRequests", "DateCreated", c => c.DateTime(nullable: false));
        }
    }
}
