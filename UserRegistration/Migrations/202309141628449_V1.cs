namespace UserRegistration.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class V1 : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Users",
                c => new
                    {
                        UserID = c.Int(nullable: false, identity: true),
                        FullName = c.String(nullable: false, maxLength: 50),
                        Email = c.String(nullable: false, maxLength: 50),
                        MobileNumber = c.String(nullable: false, maxLength: 11),
                        Address = c.String(nullable: false, maxLength: 200),
                        UserName = c.String(nullable: false, maxLength: 50),
                        Password = c.String(nullable: false, maxLength: 200),
                    })
                .PrimaryKey(t => t.UserID)
                .Index(t => t.UserName, unique: true, name: "Url");
            
        }
        
        public override void Down()
        {
            DropIndex("dbo.Users", "Url");
            DropTable("dbo.Users");
        }
    }
}
