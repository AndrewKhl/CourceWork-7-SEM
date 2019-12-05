﻿using System;
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

        public LivingRoom TryFindRoom(int id) => LivingRooms.Where(u => u.Id == id).FirstOrDefault();

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

            SaveChanges();
        }

        public void UpdateModel(LivingRoom room, int cost, string description, bool state)
        {
            room.Cost = cost;
            room.Descriptions = description;
            room.Status = state;

            SaveChangesAsync();
        }

        public bool AddPhoto(LivingRoom room, string path)
        {
            bool ok = room.AddPhoto(path);
            SaveChangesAsync();
            return ok;
        }

        public bool RemovePhoto(LivingRoom room, string path)
        {
            bool ok = room.RemovePhoto(path);
            SaveChangesAsync();
            return ok;
        }

        public void CloseConnection()
        {
            Dispose();
        }
    }
}
