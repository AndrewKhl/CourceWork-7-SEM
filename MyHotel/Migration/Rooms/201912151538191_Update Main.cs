namespace MyHotel.Migration.Rooms
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UpdateMain : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Maintenances", "Date", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Maintenances", "Date");
        }
    }
}
