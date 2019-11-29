using System;

namespace MyHotel.Core
{
    public class Staff : Person
    {
        public bool IsAdmin { get; set; }

        public int Salary { get; set; }

        public string EmploymentDate { get; set; }

        
        public Staff()
        {
            EmploymentDate = DateTime.Now.ToString();
        }
    }
}
