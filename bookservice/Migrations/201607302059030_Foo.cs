namespace BookService.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Foo : DbMigration
    {
        public override void Up()
        {
            DropPrimaryKey("dbo.Passwords");
            AddColumn("dbo.Passwords", "Id", c => c.String(nullable: false, maxLength: 128));
            AlterColumn("dbo.Passwords", "PublicKey", c => c.String());
            AlterColumn("dbo.Passwords", "TimeStamp", c => c.DateTime(nullable: false));
            AddPrimaryKey("dbo.Passwords", "Id");
        }
        
        public override void Down()
        {
            DropPrimaryKey("dbo.Passwords");
            AlterColumn("dbo.Passwords", "TimeStamp", c => c.String());
            AlterColumn("dbo.Passwords", "PublicKey", c => c.String(nullable: false, maxLength: 128));
            DropColumn("dbo.Passwords", "Id");
            AddPrimaryKey("dbo.Passwords", "PublicKey");
        }
    }
}
