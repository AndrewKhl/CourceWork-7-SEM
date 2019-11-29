using MyHotel.Commons;
using System.Data.Entity;
using System.Linq;

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

        public DbSet<Guest> Guests { get; set;}



        public Person GetUser(string email, string password) => TryFindStaff(email, password) ?? TryFindGuests(email, password);

        private Person TryFindStaff(string email, string password) => Staffs.AsEnumerable().Where(u => u.Email == email && _md5.VerifyMd5Hash(password, u.Password)).FirstOrDefault();

        private Person TryFindGuests(string email, string password) => Guests.AsEnumerable().Where(u => u.Email == email && _md5.VerifyMd5Hash(password, u.Password)).FirstOrDefault();

        private void InitialCommit()
        {
            Staffs.Add(new Staff()
            {
                Name = "Admin",
                Email = "admin@gmail.com",
                Password = _md5.GetMd5Hash("123456"),
                IsAdmin = true,
            });

            SaveChangesAsync();
        }

        public void CloseConnection()
        {
            _md5.Dispose();
            Dispose();
        }
    }
}
