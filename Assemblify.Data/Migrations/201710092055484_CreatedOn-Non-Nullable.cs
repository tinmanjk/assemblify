namespace Assemblify.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class CreatedOnNonNullable : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Posts", "CreatedOn", c => c.DateTime(nullable: false));
            AlterColumn("dbo.AspNetUsers", "CreatedOn", c => c.DateTime(nullable: false));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.AspNetUsers", "CreatedOn", c => c.DateTime());
            AlterColumn("dbo.Posts", "CreatedOn", c => c.DateTime());
        }
    }
}
