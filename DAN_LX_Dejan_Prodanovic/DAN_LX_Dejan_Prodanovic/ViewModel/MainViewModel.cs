using DAN_LX_Dejan_Prodanovic.Command;
using DAN_LX_Dejan_Prodanovic.Dto;
using DAN_LX_Dejan_Prodanovic.Model;
using DAN_LX_Dejan_Prodanovic.Service;
using DAN_LX_Dejan_Prodanovic.Utility;
using DAN_LX_Dejan_Prodanovic.View;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace DAN_LX_Dejan_Prodanovic.ViewModel
{
    class MainViewModel: ViewModelBase
    {
        MainWindow view;
        IEmployeeService employeeService;
        ILocationService locationService;
        ISectorService sectorService;
        List<tblEmployee> employees;

        public MainViewModel(MainWindow mainView)
        {
            view = mainView;

            employeeService = new EmployeeService();
            locationService = new LocationService();
            sectorService = new SectorService();

            if (locationService.GetAllLocations().Count == 0)
            {
                LocationFileActions.AddLocationsToDatabase();
            }

            employees = employeeService.GetEmployees();

            EmployeeList = ConvertToListEmployeeDto(employees);
            //FriendList = userService.GetFriends(User);

        }

        private EmployeeDto selectedEmployee;
        public EmployeeDto SelectedEmployee
        {
            get
            {
                return selectedEmployee;
            }
            set
            {
                selectedEmployee = value;
                OnPropertyChanged("SelectedEmployee");
            }
        }

        

        private List<EmployeeDto> employeeList;
        public List<EmployeeDto> EmployeeList
        {
            get
            {
                return employeeList;
            }
            set
            {
                employeeList = value;
                OnPropertyChanged("EmployeeList");
            }
        }

        private ICommand close;
        public ICommand Close
        {
            get
            {
                if (close == null)
                {
                    close = new RelayCommand(param => CloseExecute());
                }
                return close;
            }
        }

        private void CloseExecute()
        {
            try
            {
                view.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        private ICommand deleteEmployee;
        public ICommand DeleteEmployee
        {
            get
            {
                if (deleteEmployee == null)
                {
                    deleteEmployee = new RelayCommand(param => DeleteEmployeeExecute(), param => CanDeleteEmployeeExecute());
                }
                return deleteEmployee;
            }
        }

        private void DeleteEmployeeExecute()
        {
            try
            {
                if (SelectedEmployee != null)
                {

                    MessageBoxResult result = MessageBox.Show("Are you sure that you want to delete this employee?",
                       "My App",
                        MessageBoxButton.YesNo, MessageBoxImage.Question, MessageBoxResult.No);
                    int employeeID = SelectedEmployee.EmployeeID;

                    switch (result)
                    {
                        case MessageBoxResult.Yes:
                            if (ChechIfIsManager(SelectedEmployee))
                            {
                                MessageBox.Show("This user is menager to other users. You have to delete them first.");
                                return;
                            }
                            string textForFile = String.Format("Deleted user {0} {1} JMBG {2}", SelectedEmployee.FirstName,
                                SelectedEmployee.LastName, SelectedEmployee.JMBG);
                            //eventObject.OnActionPerformed(textForFile);

                            employeeService.DeleteEmployee(employeeID);
                            employees = employeeService.GetEmployees();

                            EmployeeList = ConvertToListEmployeeDto(employees);

                            break;
                    }

                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }
        private bool CanDeleteEmployeeExecute()
        {
            if (SelectedEmployee == null)
            {
                return false;
            }
            else
            {
                return true;
            }
        }
        private ICommand addEmployee;
        public ICommand AddEmployee
        {
            get
            {
                if (addEmployee == null)
                {
                    addEmployee = new RelayCommand(param => AddEmployeeExecute());
                }
                return addEmployee;
            }
        }

        private void AddEmployeeExecute()
        {
            try
            {
                AddEmployee addEmployee = new AddEmployee();
                addEmployee.ShowDialog();

                employees = employeeService.GetEmployees();

                EmployeeList = ConvertToListEmployeeDto(employees);

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        List<EmployeeDto>ConvertToListEmployeeDto(List<tblEmployee>employees)
        {
            List<EmployeeDto> employeeDtos = new List<EmployeeDto>();

            foreach (var item in employees)
            {
                employeeDtos.Add(ConvertToEmployeeDto(item));
            }
            return employeeDtos;
        }

        EmployeeDto ConvertToEmployeeDto(tblEmployee employee)
        {
            EmployeeDto employeeDto = new EmployeeDto();
            employeeDto.EmployeeID = employee.EmployeeID;
            employeeDto.FirstName = employee.FirstName;
            employeeDto.LastName = employee.LastName;
            if (employee.Gender.Equals("M"))
            {
                employeeDto.Gender = "male";
            }
            else if (employee.Gender.Equals("F"))
            {
                employeeDto.Gender = "female";
            }
            else
            {
                employeeDto.Gender = "other";

            }

            employeeDto.DateOfBirth = employee.DateOfBirth;
            employeeDto.JMBG = employee.JMBG;
            employeeDto.IDNumber = employee.IDNumber;
            employeeDto.PhoneNumber = employee.PhoneNumber;
            employeeDto.ManagerId = employee.ManagerId;
            employeeDto.SectorID = employee.SectorID;
            employeeDto.LocationID = employee.LocationID;

            if (employee.SectorID!=null)
            {
                tblSector sector = sectorService.GetSectorByID((int)employee.SectorID);
                employeeDto.SectorName = sector.SectorName;
            }
            if (employee.LocationID!=null)
            {
                tblLocation location = locationService.GetLocationByID((int)employee.LocationID);
                employeeDto.LocationName = string.Format("{0} {1} {2}",location.Street,location.Number,
                    location.City);
            }



             
            tblEmployee manager = employeeService.GetEmployeeByJMBG(employee.JMBG);

            employeeDto.ManagerName = string.Format("{0} {1} {2}",manager.FirstName,manager.LastName,
                manager.JMBG);

            return employeeDto;
             
    }

        bool ChechIfIsManager(EmployeeDto employee)
        {
            var managerIds = employees.Select(item => item.ManagerId).ToList();
            if (managerIds.Contains(employee.EmployeeID))
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}
