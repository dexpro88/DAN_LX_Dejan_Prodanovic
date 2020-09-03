using DAN_LX_Dejan_Prodanovic.Command;
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
        public MainViewModel(MainWindow mainView)
        {
            view = mainView;

            employeeService = new EmployeeService();
            locationService = new LocationService();

            if (locationService.GetAllLocations().Count == 0)
            {
                LocationFileActions.AddLocationsToDatabase();
            }
            //FriendList = userService.GetFriends(User);

        }

        private tblEmployee employee;
        public tblEmployee Employee
        {
            get
            {
                return employee;
            }
            set
            {
                employee = value;
                OnPropertyChanged("Employee");
            }
        }

        

        private List<tblEmployee> employeeList;
        public List<tblEmployee> EmployeeList
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
                
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }
    }
}
