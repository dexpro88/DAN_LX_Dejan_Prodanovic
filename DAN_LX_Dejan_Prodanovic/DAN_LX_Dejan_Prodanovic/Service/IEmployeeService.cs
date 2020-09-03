using DAN_LX_Dejan_Prodanovic.Dto;
using DAN_LX_Dejan_Prodanovic.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAN_LX_Dejan_Prodanovic.Service
{
    interface IEmployeeService
    {
        List<tblEmployee> GetEmployees();
        tblEmployee GetEmployeeByJMBG(string JMBG);
        tblEmployee GetEmployeeByID(int ID);
        tblEmployee GetEmployeeByRegnumber(string registrationNumber);
        List<tblEmployee> GetAllPotentialMenagersForEdit(tblEmployee employee);
        List<tblEmployee> GetAllPotentialMenagers();
        tblEmployee AddEmployee(tblEmployee employee);
        tblEmployee EditSector(int employeeID, tblSector sector);
        tblEmployee EditManager(int employeeID, int managerId);
        void DeleteEmployee(int employeeID);
        void EditEmployee(EmployeeDto employee);

    }
}
