using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using DAN_LX_Dejan_Prodanovic.Model;

namespace DAN_LX_Dejan_Prodanovic.Service
{
    class EmployeeService : IEmployeeService
    {
        public tblEmployee AddEmployee(tblEmployee employee)
        {
            try
            {
                using (EmployeeDbEntities1 context = new EmployeeDbEntities1())
                {
                     
                    tblEmployee newEmployee = new tblEmployee();

                    newEmployee.FirstName = employee.FirstName;
                    newEmployee.LastName = employee.LastName;
                    newEmployee.JMBG = employee.JMBG;
                    newEmployee.IDNumber = employee.IDNumber;
                    newEmployee.PhoneNumber = employee.PhoneNumber;
                    newEmployee.LocationID = employee.LocationID;
                    newEmployee.DateOfBirth = employee.DateOfBirth;
                    //newEmployee.ManagerId = employee.ManagerId;
                    ////newEmployee.SectorID = employee.SectorID;
                    newEmployee.Gender = employee.Gender;


                    context.tblEmployees.Add(newEmployee);

                    context.SaveChanges();

                  
                    context.SaveChanges();
                    employee.EmployeeID = newEmployee.EmployeeID;
                }
                 
                return employee;
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine("Exception" + ex.Message.ToString());
                return null;
            }
        }

        public void DeleteEmployee(int employeeID)
        {
            try
            {
                using (EmployeeDbEntities1 context = new EmployeeDbEntities1())
                {

                    tblEmployee employeeToDelete = (from e in context.tblEmployees
                                                    where e.EmployeeID == employeeID select e).First();


                    context.tblEmployees.Remove(employeeToDelete);

                    context.SaveChanges();

                  
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine("Exception" + ex.Message.ToString());
            }
        }

        public tblEmployee EditManager(tblEmployee employee, int managerId)
        {
            try
            {
                using (EmployeeDbEntities1 context = new EmployeeDbEntities1())
                {

                    tblEmployee emoloyeeInDB = (from e in context.tblEmployees
                                                where e.EmployeeID == employee.EmployeeID
                                                select e).First();

                    


                    emoloyeeInDB.ManagerId = managerId;

                    context.SaveChanges();


                    return employee;


                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine("Exception" + ex.Message.ToString());
                return null;
            }
        }

        public tblEmployee EditSector(tblEmployee employee,tblSector sector)
        {
            try
            {
                using (EmployeeDbEntities1 context = new EmployeeDbEntities1())
                {
 
                    tblEmployee emoloyeeInDB = (from e in context.tblEmployees
                                            where e.EmployeeID == employee.EmployeeID
                                            select e).First();

                    tblSector sectorInDb = (from e in context.tblSectors
                                                where e.SectorID == sector.SectorID
                                                select e).First();

                   
                    emoloyeeInDB.SectorID = sector.SectorID;

                    context.SaveChanges();

                    
                    return employee;


                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine("Exception" + ex.Message.ToString());
                return null;
            }
        }

        

        public List<tblEmployee> GetAllPotentialMenagers()
        {
            try
            {
                using (EmployeeDbEntities1 context = new EmployeeDbEntities1())
                {
                    List<tblEmployee> list = new List<tblEmployee>();
                    list = (from x in context.tblEmployees
                            
                            select x).ToList();
                    return list;
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine("Exception" + ex.Message.ToString());
                return null;
            }
        }

        public List<tblEmployee> GetAllPotentialMenagersForEdit(tblEmployee employee)
        {
            try
            {
                using (EmployeeDbEntities1 context = new EmployeeDbEntities1())
                {
                    List<tblEmployee> list = new List<tblEmployee>();
                    list = (from x in context.tblEmployees where 
                             x.EmployeeID != employee.EmployeeID select x).ToList();
                    return list;
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine("Exception" + ex.Message.ToString());
                return null;
            }
        }

        public tblEmployee GetEmployeeByJMBG(string JMBG)
        {
            try
            {
                using (EmployeeDbEntities1 context = new EmployeeDbEntities1())
                {
                    tblEmployee emoloyee = (from e in context.tblEmployees
                                            where e.JMBG.Equals(JMBG) select e).First();


                    return emoloyee;

                    
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine("Exception" + ex.Message.ToString());
                return null;
            }
        }

        public tblEmployee GetEmployeeByRegnumber(string registrationNumber)
        {
            try
            {
                using (EmployeeDbEntities1 context = new EmployeeDbEntities1())
                {
                    tblEmployee emoloyee = (from e in context.tblEmployees where e.IDNumber.Equals(registrationNumber) select e).First();


                    return emoloyee;

                     
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine("Exception" + ex.Message.ToString());
                return null;
            }
        }

        public List<tblEmployee> GetEmployees()
        {
            try
            {
                using (EmployeeDbEntities1 context = new EmployeeDbEntities1())
                {
                    List<tblEmployee> list = new List<tblEmployee>();
                    list = (from x in context.tblEmployees select x).ToList();
                    return list;
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine("Exception" + ex.Message.ToString());
                return null;
            }
        }
    }
}
