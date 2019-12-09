namespace MyHotel.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitialCreate : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.HousingOrders",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        InTime = c.String(),
                        OutTime = c.String(),
                        UserId = c.Int(nullable: false),
                        RoomId = c.Int(nullable: false),
                        Comment = c.String(),
                        CreateTime = c.String(),
                        IsPaid = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Payments",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        IsHousingOrder = c.Boolean(nullable: false),
                        OrderId = c.Int(nullable: false),
                        Cost = c.Int(nullable: false),
                        CreateTime = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.ServiceOrders",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        ServiceId = c.Int(nullable: false),
                        StartTime = c.String(),
                        UserId = c.Int(nullable: false),
                        RoomId = c.Int(nullable: false),
                        Comment = c.String(),
                        CreateTime = c.String(),
                        IsPaid = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.ServiceOrders");
            DropTable("dbo.Payments");
            DropTable("dbo.HousingOrders");
        }
    }
}
