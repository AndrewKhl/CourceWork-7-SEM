namespace MyHotel.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class NewCostpropertyinOrdermodel : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.HousingOrders", "Cost", c => c.Int(nullable: false));
            AddColumn("dbo.ServiceOrders", "Cost", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.ServiceOrders", "Cost");
            DropColumn("dbo.HousingOrders", "Cost");
        }
    }
}
