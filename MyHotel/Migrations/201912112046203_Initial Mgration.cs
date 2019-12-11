namespace MyHotel.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitialMgration : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Salaries",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Count = c.Int(nullable: false),
                        StaffId = c.Int(nullable: false),
                        PaymentDate = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.Salaries");
        }
    }
}
