namespace BookService.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Password : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Passwords",
                c => new
                    {
                        Id = c.Guid(nullable:false),
                        PublicKey = c.String(nullable: false, maxLength: 512),
                        PrivateKey = c.String(),
                        UserInfo = c.String(),
                        TimeStamp = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
        }
        
        public override void Down()
        {
            DropTable("dbo.Passwords");
        }
    }
}
