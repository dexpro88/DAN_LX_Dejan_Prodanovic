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
                using (EmployeeDbEntities context = new EmployeeDbEntities())
                {
                    MessageBox.Show("tuj sam");
                    tblEmployee newEmployee = new tblEmployee();

                    newEmployee.FirstName = employee.FirstName;
                    newEmployee.LastName = employee.LastName;
                    newEmployee.JMBG = employee.JMBG;
                    newEmployee.IDNumber = employee.IDNumber;
                    newEmployee.PhoneNumber = employee.PhoneNumber;
                    newEmployee.LocationID = employee.LocationID;
                    newEmployee.DateOfBirth = employee.DateOfBirth;
                    newEmployee.ManagerId = employee.ManagerId;
                    newEmployee.Gender = employee.Gender;
                    //newEmployee.SectorID = employee.SectorID;

                    context.tblEmployees.Add(newEmployee);

                    context.SaveChanges();

                   

                    employee.EmployeeID = newEmployee.EmployeeID;
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
                using (EmployeeDbEntities context = new EmployeeDbEntities())
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
                using (EmployeeDbEntities context = new EmployeeDbEntities())
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
                using (EmployeeDbEntities context = new EmployeeDbEntities())
                {
                    tblEmployee emoloyee = (from e in context.tblEmployees where e.JMBG.Equals(JMBG) select e).First();


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
                using (EmployeeDbEntities context = new EmployeeDbEntities())
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
    }
}
