using MyHotel.Commons;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;

namespace MyHotel.Core
{
    public class UserManager : DbContext, IModelManager
    {
        private static readonly MD5Helper _md5 = new MD5Helper();
        private readonly MailManager _mail;

        public UserManager() : base("DbConnection")
        {

        }

        public UserManager(MailManager mail) : this()
        {
            _mail = mail;
        }

        #region Staff

        public DbSet<Staff> Staffs { get; set; }

        public Staff AddStaff(Staff newStaff)
        {
            var user = TryGetUser(newStaff.Email);

            if (user != null)
                return null;

            if (newStaff.Password == null)
            {
                var rand = new Random(DateTime.Now.Millisecond);
                newStaff.Password = rand.Next().ToString();
            }
            newStaff.Password = _md5.Shifr(newStaff.Password);

            Staffs.Add(newStaff);
            SaveChanges();

            return newStaff;
        }

        public void ModifyStaff(Staff newStaff)
        {
            var old = Staffs.Where(u => u.Email == newStaff.Email).FirstOrDefault();

            if (old == null)
                return;

            old.Name = newStaff.Name;
            old.SecondName = newStaff.SecondName;
            old.BirthDay = newStaff.BirthDay;
            old.Salary = newStaff.Salary;
            old.EmploymentDate = newStaff.EmploymentDate;

            SaveChanges();
        }

        public void RemoveStaff(string email)
        {
            var staff = (Staff)TryFindStaff(email);

            if (staff == null)
                return;

            Staffs.Remove(staff);

            SaveChanges();
        }

        private Person TryFindStaff(string email, string password) => Staffs.AsEnumerable().Where(u => u.Email == email && _md5.VerifyString(password, u.Password)).FirstOrDefault();

        private Person TryFindStaff(string email) => Staffs.Where(u => u.Email == email).FirstOrDefault();

        public Staff TryGetStaff(int id) => Staffs.Where(u => u.Id == id).FirstOrDefault();
        #endregion


        #region Guests

        public DbSet<Guest> Guests { get; set; }

        public Guest AddGuest(Guest newGuest)
        {
            var user = TryGetUser(newGuest.Email);

            if (user != null)
                return null;

            if (newGuest.Password == null)
            {
                var rand = new Random(DateTime.Now.Millisecond);
                newGuest.Password = rand.Next().ToString();
            }

            _mail.SendRegistrarion(newGuest);

            newGuest.Password = _md5.Shifr(newGuest.Password);

            Guests.Add(newGuest);
            SaveChanges();

            return newGuest;
        }

        public Guest ModifyGuest(Guest newGuest)
        {
            var old = Guests.Where(u => u.Email == newGuest.Email).FirstOrDefault();

            if (old == null)
                return null;

            old.Name = newGuest.Name;
            old.SecondName = newGuest.SecondName;
            old.BirthDay = newGuest.BirthDay;

            SaveChanges();

            return old;
        }

        public void RemoveGuest(string email)
        {
            var guest = (Guest)TryFindGuests(email);

            if (guest == null)
                return;

            Guests.Remove(guest);

            SaveChanges();
        }

        public void AddReservation(int userId, int orderId)
        {
            var guset = Guests.Where(u => u.Id == userId).FirstOrDefault();

            if (guset == null)
                return;

            guset.AddReservation(orderId);

            SaveChanges();
        }

        public void RemoveReservation(int userId, int orderId)
        {
            var guset = Guests.Where(u => u.Id == userId).FirstOrDefault();

            if (guset == null)
                return;

            guset.RemoveReservation(orderId);

            SaveChanges();
        }

        public void AddOrder(int userId, int orderId)
        {
            var guset = Guests.Where(u => u.Id == userId).FirstOrDefault();

            if (guset == null)
                return;

            guset.AddOrders(orderId);

            SaveChanges();
        }

        public void RemoveOrder(int userId, int orderId)
        {
            var guset = Guests.Where(u => u.Id == userId).FirstOrDefault();

            if (guset == null)
                return;

            guset.RemoveOrders(orderId);

            SaveChanges();
        }

        public Guest TryFindGuests(string email, string password) => Guests.AsEnumerable().Where(u => u.Email == email && _md5.VerifyString(password, u.Password)).FirstOrDefault();
        
        public Guest TryFindGuests(string email) => Guests.AsEnumerable().Where(u => u.Email == email).FirstOrDefault();

        public Guest TryGetGuests(int id) => Guests.AsEnumerable().Where(u => u.Id == id).FirstOrDefault();

        #endregion


        #region Salary

        public DbSet<Salary> Salarys { get; set; }

        public void AddSalary(int count, int staffId)
        {
            var salary = new Salary()
            {
                StaffId = staffId,
                Count = count,
                PaymentDate = DateTime.Now.ToString(),
            };

            Salarys.Add(salary);

            SaveChanges();
        }

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
                EmploymentDate = DateTime.Now.ToString()
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
