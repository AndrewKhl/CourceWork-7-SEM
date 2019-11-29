using MyHotel.Commons;
using System.Data.Entity;

namespace MyHotel.Core
{
    public class DataBaseManager : DbContext
    {
        private static readonly MD5Helper _md5 = new MD5Helper();

        public DataBaseManager() : base("DbConnection")
        {
            //InitialCommit();

            var user = Staffs.FirstAsync().Result;
        }

        public DbSet<Staff> Staffs { get; set; }

        public void CloseConnection()
        {
            _md5.Dispose();
            Dispose();
        }

        private void InitialCommit()
        {
            Staffs.Add(new Staff()
            {
                Name = "Admin",
                Email = "admin@gmail.com",
                Password = _md5.GetMd5Hash("123456"),
                IsAdmin = true,
            });

            SaveChanges();
        }
    }
}
