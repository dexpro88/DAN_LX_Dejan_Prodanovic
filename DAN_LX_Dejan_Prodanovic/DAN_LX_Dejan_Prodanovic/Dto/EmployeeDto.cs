using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAN_LX_Dejan_Prodanovic.Dto
{
    class EmployeeDto
    {
        public int EmployeeID { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Gender { get; set; }
        public Nullable<System.DateTime> DateOfBirth { get; set; }
        public string JMBG { get; set; }
        public string IDNumber { get; set; }
        public string PhoneNumber { get; set; }
        public Nullable<int> ManagerId { get; set; }
        public Nullable<int> SectorID { get; set; }
        public Nullable<int> LocationID { get; set; }
        public string SectorName { get; set; }
        public string LocationName { get; set; }
        public string ManagerName { get; set; }
    }
}
