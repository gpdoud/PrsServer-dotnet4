namespace PrsServer.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class removereqfromphoneandemail : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Vendors", "Phone", c => c.String(maxLength: 12));
            AlterColumn("dbo.Vendors", "Email", c => c.String(maxLength: 250));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Vendors", "Email", c => c.String(nullable: false, maxLength: 250));
            AlterColumn("dbo.Vendors", "Phone", c => c.String(nullable: false, maxLength: 12));
        }
    }
}
