namespace Infrastructure.DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class DatetimeFieldsModification : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Orders", "StartTransaction", c => c.DateTime());
            AlterColumn("dbo.Orders", "EndTransaction", c => c.DateTime());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Orders", "EndTransaction", c => c.DateTime(nullable: false));
            AlterColumn("dbo.Orders", "StartTransaction", c => c.DateTime(nullable: false));
        }
    }
}
