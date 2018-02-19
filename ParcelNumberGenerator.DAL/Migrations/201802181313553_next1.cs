namespace ParcelNumberGenerator.DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class next1 : DbMigration
    {
        public override void Up()
        {
            RenameTable(name: "dbo.UsedNumbers", newName: "UsedNumber");
            DropTable("dbo.Categories");
            DropTable("dbo.Users");
        }
        
        public override void Down()
        {
            CreateTable(
                "dbo.Users",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Categories",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Tresc = c.String(maxLength: 500),
                    })
                .PrimaryKey(t => t.ID);
            
            RenameTable(name: "dbo.UsedNumber", newName: "UsedNumbers");
        }
    }
}
