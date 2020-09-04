using DAN_LX_Dejan_Prodanovic.Command;
using DAN_LX_Dejan_Prodanovic.Dto;
using DAN_LX_Dejan_Prodanovic.Model;
using DAN_LX_Dejan_Prodanovic.Service;
using DAN_LX_Dejan_Prodanovic.Validation;
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
    class EditEmployeeViewModel:ViewModelBase
    {
        EditEmployee editEmployee;
        ILocationService locationService;
        IEmployeeService employeeService;
       
        ISectorService sectorService;
        EventClass eventObject;
        List<tblLocation> locations = new List<tblLocation>();
        List<tblEmployee> potentialManagersInDb = new List<tblEmployee>();
        private EmployeeDto oldEmployee;

        #region Constructor
        public EditEmployeeViewModel(EditEmployee editEmployeeOpen, EmployeeDto employeeEdit)
        {
            eventObject = new EventClass();
            editEmployee = editEmployeeOpen;
             
            Employee = employeeEdit;

            selctedLocation = new LocationDto();
         
            StartDate = (DateTime)employee.DateOfBirth;
            Sector = employee.SectorName;

            locationService = new LocationService();
            employeeService = new EmployeeService();
           
            sectorService = new SectorService();

            potentialManagersInDb = employeeService.GetAllPotentialMenagers();
            PotentialMenagers = ConvertManagerListToDto(potentialManagersInDb);
            PotentialMenagers = PotentialMenagers.Where(x => x.ID != Employee.EmployeeID).ToList();

            locations = locationService.GetAllLocations().ToList();

            tblLocation locataionForPresent = locationService.GetLocationByID((int)Employee.LocationID);

            tblEmployee managerToPresent = null;
            if (Employee.ManagerId!=null)
            {
                managerToPresent = employeeService.GetEmployeeByID((int)Employee.ManagerId);
            }
          
            LocationList = ConvertLocationDtoList(locations);
           
            SelctedLocation = LocationList.Where(x=>x.ID==locataionForPresent.LocationID).FirstOrDefault();
            LocationList.OrderByDescending(x => x.Location);
            LocationList.Reverse();

            //if (managerToPresent!=null)
            //{
            //    SelectedMenager = PotentialMenagers.Where(x=>x.ID==Employee.ManagerId).FirstOrDefault();
            //}





            eventObject.ActionPerformed += ActionPerformed;

            oldEmployee = new EmployeeDto();
            oldEmployee.FirstName = employee.FirstName;
            oldEmployee.LastName = employee.LastName;
            oldEmployee.JMBG = employee.JMBG;
        }


        #endregion

        #region Properties
        private LocationDto selctedLocation;
        public LocationDto SelctedLocation
        {
            get
            {
                return selctedLocation;
            }
            set
            {
                selctedLocation = value;
                OnPropertyChanged("SelctedLocation");
            }
        }

        private EmployeeDto employee;
        public EmployeeDto Employee
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

      


        private ManagerDto selectedMenager;
        public ManagerDto SelectedMenager
        {
            get
            {
                return selectedMenager;
            }
            set
            {
                selectedMenager = value;
                OnPropertyChanged("SelectedMenager");
            }
        }

        private string sector;
        public string Sector
        {
            get
            {
                return sector;
            }
            set
            {
                sector = value;
                OnPropertyChanged("Sector");
            }
        }

        private string gender = "male";
        public string Gender
        {
            get { return gender; }
            set
            {
                gender = value;
                OnPropertyChanged("Gender");
            }
        }

        private List<LocationDto> locationList;
        public List<LocationDto> LocationList
        {
            get
            {
                return locationList;
            }
            set
            {
                locationList = value;
                OnPropertyChanged("LocationList");
            }
        }

        private List<ManagerDto> potentialMenagers;
        public List<ManagerDto> PotentialMenagers
        {
            get
            {
                return potentialMenagers;
            }
            set
            {
                potentialMenagers = value;
                OnPropertyChanged("PotentialMenagers");
            }
        }



        private DateTime _startDate = DateTime.Now;
        public DateTime StartDate
        {
            get { return _startDate; }
            set { _startDate = value; OnPropertyChanged("StartDate"); }
        }


        private bool isUpdateUser;
        public bool IsUpdateUser
        {
            get
            {
                return isUpdateUser;
            }
            set
            {
                isUpdateUser = value;
            }
        }
        #endregion

        #region Commands

        private ICommand save;
        public ICommand Save
        {
            get
            {
                if (save == null)
                {
                    save = new RelayCommand(param => SaveExecute(), param => CanSaveExecute());
                }
                return save;
            }
        }

        private void SaveExecute()
        {
            try
            {

                if (!ValidationClass.JMBGisValid(employee.JMBG))
                {
                    MessageBox.Show("JMBG  is not valid.");
                    return;
                }



                if (!ValidationClass.RegisterNumberIsValid(employee.IDNumber))
                {
                    MessageBox.Show("Registration number  is not valid");
                    return;
                }

                if (!ValidationClass.TelfonNumberValid(employee.PhoneNumber))
                {
                    MessageBox.Show("Telefon number  is not valid. It must have 9 numbers");
                    return;
                }


                employee.DateOfBirth = StartDate;
                Employee.LocationID = SelctedLocation.ID;

                if (SelectedMenager!=null)
                {
                    employee.ManagerId = SelectedMenager.ID;

                }
               

                if (Gender.Equals("male"))
                {
                    employee.Gender = "M";
                }
                else if (Gender.Equals("female"))
                {
                    employee.Gender = "F";
                }
                else
                {
                    employee.Gender = "O";
                }


                tblSector sectorDB = sectorService.GetSectorByName(Sector);
 

                employeeService.EditEmployee(Employee);

                if (sectorDB == null)
                {

                    sectorDB = new tblSector();
                    sectorDB.SectorName = Sector;
                    sectorDB = sectorService.AddSector(sectorDB);
                    
                    employeeService.EditSector(Employee.EmployeeID, sectorDB);

                }
                else
                {
                    //some comment
                    employeeService.EditSector(Employee.EmployeeID, sectorDB);
                }
                if (Employee.ManagerId!=null)
                {
                    if (SelectedMenager!=null)
                    {
                        employeeService.EditManager((int)Employee.EmployeeID, SelectedMenager.ID);
                    }
                    else
                    {
                        employeeService.EditManager((int)Employee.EmployeeID,0);
                    }
                   
                }



                string textForFile = String.Format("Updated user {0} {1} JMBG {2} to {3} {4} JMBG {5}", oldEmployee.FirstName,
                              oldEmployee.LastName, oldEmployee.JMBG, employee.FirstName, employee.LastName, employee.JMBG);
                eventObject.OnActionPerformed(textForFile);


               

                editEmployee.Close();

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        private bool CanSaveExecute()
        {

            if (String.IsNullOrEmpty(Employee.FirstName) || String.IsNullOrEmpty(Employee.FirstName) ||
                String.IsNullOrEmpty(Employee.JMBG) || String.IsNullOrEmpty(Employee.IDNumber) ||
                String.IsNullOrEmpty(Employee.PhoneNumber) || String.IsNullOrEmpty(SelctedLocation.Location) ||
                String.IsNullOrEmpty(Sector)
               )
            {
                return false;
            }
            else
            {
                return true;
            }
            //return true;
        }

        private ICommand close;
        public ICommand Close
        {
            get
            {
                if (close == null)
                {
                    close = new RelayCommand(param => CloseExecute(), param => CanCloseExecute());
                }
                return close;
            }
        }

        private void CloseExecute()
        {
            try
            {
                editEmployee.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        private bool CanCloseExecute()
        {
            return true;
        }


        private ICommand chooseGender;
        public ICommand ChooseGender
        {
            get
            {
                if (chooseGender == null)
                {
                    chooseGender = new RelayCommand(ChooseGenderExecute, CanChooseGenderExecute);
                }
                return chooseGender;
            }
        }

        private void ChooseGenderExecute(object parameter)
        {
            Gender = (string)parameter;
        }

        private bool CanChooseGenderExecute(object parameter)
        {
            if (parameter != null)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        #endregion

         
        List<LocationDto> ConvertLocationDtoList(List<tblLocation> locations)
        {
            List<LocationDto> locationDtos = new List<LocationDto>();
            foreach (var loc in locations)
            {
                locationDtos.Add(ConvertToLocationDto(loc));
            }

            return locationDtos;
        }
        LocationDto ConvertToLocationDto(tblLocation location)
        {

            LocationDto locationDto = new LocationDto();
            locationDto.Location = string.Format("{0} {1} {2} {3}", location.Street, location.Number,
                location.City, location.Country);
            locationDto.ID = location.LocationID;

            return locationDto;

        }

        List<ManagerDto> ConvertManagerListToDto(List<tblEmployee> managers)
        {
            List<ManagerDto> managerDtos = new List<ManagerDto>();
            foreach (var item in managers)
            {
                managerDtos.Add(ConvertToManagerDto(item));
            }
            return managerDtos;
        }

        ManagerDto ConvertToManagerDto(tblEmployee manager)
        {
            ManagerDto managerDto = new ManagerDto();
            managerDto.ManagerData = string.Format("{0} {1} {2}", manager.FirstName, manager.LastName,
                manager.JMBG);
            managerDto.ID = manager.EmployeeID;

            return managerDto;
        }

        void ActionPerformed(object source, TextToFileEventArgs args)
        {
            FileLogging.texToFile = args.TextForFile;
        }
    }
}
