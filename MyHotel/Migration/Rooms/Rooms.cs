namespace MyHotel.Migration.Rooms
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Rooms : DbMigrationsConfiguration<MyHotel.Core.RoomManager>
    {
        public Rooms()
        {
            AutomaticMigrationsEnabled = true;
            MigrationsDirectory = @"Migration\Rooms";
            ContextKey = "MyHotel.Core.RoomManager";
        }

        protected override void Seed(MyHotel.Core.RoomManager context)
        {
            //  This method will be called after migrating to the latest version.

            //  You can use the DbSet<T>.AddOrUpdate() helper extension method
            //  to avoid creating duplicate seed data.
        }
    }
}
