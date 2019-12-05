using MyHotel.Commons;
using System.Data.Entity;
using System.Linq;

namespace MyHotel.Core
{
    public class UserManager : DbContext, IModelManager
    {
        private static readonly MD5Helper _md5 = new MD5Helper();

        public UserManager() : base("DbConnection")
        {}

        #region Staff

        public DbSet<Staff> Staffs { get; set; }

        public bool AddStaff(Staff newStaff)
        {
            var user = TryGetUser(newStaff.Email);

            if (user != null)
                return false;

            newStaff.Password = _md5.Shifr(newStaff.Password);

            Staffs.Add(newStaff);
            SaveChangesAsync();

            return true;
        }

        public void ModifyStaff(Staff newStaff)
        {
            var old = Staffs.Where(u => u.Email == newStaff.Email).FirstOrDefault();

            if (old == null)
                return;

            old = newStaff;

            SaveChangesAsync();
        }

        private Person TryFindStaff(string email, string password) => Staffs.AsEnumerable().Where(u => u.Email == email && _md5.VerifyString(password, u.Password)).FirstOrDefault();

        private Person TryFindStaff(string email) => Staffs.Where(u => u.Email == email).FirstOrDefault();

        #endregion


        #region Guests

        public DbSet<Guest> Guests { get; set; }

        public Guest AddGuest(Guest newGuest)
        {
            var user = TryGetUser(newGuest.Email);

            if (user != null)
                return null;

            newGuest.Password = _md5.Shifr(newGuest.Password);

            Guests.Add(newGuest);
            SaveChangesAsync();

            return newGuest;
        }

        public Guest TryFindGuests(string email, string password) => Guests.AsEnumerable().Where(u => u.Email == email && _md5.VerifyString(password, u.Password)).FirstOrDefault();
        
        public Guest TryFindGuests(string email) => Guests.AsEnumerable().Where(u => u.Email == email).FirstOrDefault();

        #endregion

        public Person TryGetUser(string email, string password) => TryFindStaff(email, password) ?? TryFindGuests(email, password);

        public Person TryGetUser(string email) => TryFindStaff(email) ?? TryFindGuests(email);

        public void InitialCommit()
        {
            Staffs.Add(new Staff()
            {
                Name = "Admin",
                Email = "admin@gmail.com",
                Password = _md5.Shifr("123456"),
                IsAdmin = true,
            });

            SaveChanges();
        }

        public void CloseConnection()
        {
            _md5.Dispose();
            Dispose();
        }
    }
}
