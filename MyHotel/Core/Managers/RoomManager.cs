using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyHotel.Core
{
    public class RoomManager : DbContext, IModelManager
    {
        public RoomManager() : base("DbConnection")
        {

        }

        public DbSet<LivingRoom> LivingRooms { get; set; }

        public void AddService(int roomId)
        {
            LivingRoom room = TryFindRoom(roomId);

            if (room == null)
                return;


        }

        public Room TryFindRoom(int id) => LivingRooms.Where(u => u.Id == id).FirstOrDefault();

        public void InitialCommit()
        {
            LivingRooms.Add(new LivingRoom()
            {
                Cost = 1000,
                Status = false,
                Floor = 1,
            });

            LivingRooms.Add(new LivingRoom()
            {
                Cost = 1500,
                Status = false,
                Floor = 2,
            });

            LivingRooms.Add(new LivingRoom()
            {
                Cost = 2000,
                Status = false,
                Floor = 2,
            });

            SaveChangesAsync();
        }

        public void CloseConnection()
        {
            Dispose();
        }
    }
}
