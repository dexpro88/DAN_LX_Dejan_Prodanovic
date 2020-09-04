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
    class AddEmployeeViewModel : ViewModelBase
    {
        AddEmployee addEmployee;
        ILocationService locationService;
        IEmployeeService employeeService;

        ISectorService sectorService;
        EventClass eventObject;
        List<tblLocation> locationsInDb;
        List<tblEmployee> managersInDb;

        #region Constructor
        public AddEmployeeViewModel(AddEmployee addEmployeeOpen)
        {
            eventObject = new EventClass();

            SelectedMenager = new ManagerDto();
            

            employee = new tblEmployee();
            addEmployee = addEmployeeOpen;

            locationService = new LocationService();
            employeeService = new EmployeeService();

            sectorService = new SectorService();

            locationsInDb = locationService.GetAllLocations().ToList();

            LocationList = ConvertLocationDtoList(locationsInDb);
            selctedLocation = LocationList.FirstOrDefault();
            LocationList.OrderByDescending(x => x.Location);
            LocationList.Reverse();



            managersInDb = employeeService.GetAllPotentialMenagers();

            PotentialMenagers = ConvertManagerListToDto(managersInDb);


            eventObject.ActionPerformed += ActionPerformed;
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

                if (!ValidationClass.JMBGIsUnique(employee.JMBG))
                {
                    MessageBox.Show("JMBG  already exists in database");
                    return;
                }

                if (!ValidationClass.RegisterNumberIsValid(employee.IDNumber))
                {
                    MessageBox.Show("Registration number  is not valid. It must have 9 numbers");
                    return;
                }
                if (!ValidationClass.RegNumberIsUnique(employee.IDNumber))
                {
                    MessageBox.Show("Registration number  already exists in database");
                    return;
                }
                if (!ValidationClass.TelfonNumberValid(employee.PhoneNumber))
                {
                    MessageBox.Show("Telephone number  is not valid. It must have 9 numbers");
                    return;
                }

                employee.LocationID = SelctedLocation.ID;
                employee.DateOfBirth = StartDate;

                 
                employee.ManagerId = SelectedMenager.ID;
              



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

                string textForFile = String.Format("Added user {0} {1} JMBG {2}", employee.FirstName,
                              employee.LastName, employee.JMBG);
                eventObject.OnActionPerformed(textForFile);



                tblEmployee emplInDb = employeeService.AddEmployee(employee);

                if (sectorDB == null)
                {

                    sectorDB = new tblSector();
                    sectorDB.SectorName = Sector;
                    sectorDB = sectorService.AddSector(sectorDB);
                    //employee.SectorID = sectorDB.SectorID;
                    employeeService.EditSector(emplInDb.EmployeeID, sectorDB);

                }
                else
                {
                    
                    employeeService.EditSector(emplInDb.EmployeeID, sectorDB);
                }
                employeeService.EditManager(emplInDb.EmployeeID, SelectedMenager.ID);

                addEmployee.Close();

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
                String.IsNullOrEmpty(Employee.PhoneNumber) || String.IsNullOrEmpty(SelctedLocation.Location) 
                ||
                String.IsNullOrEmpty(Sector)
               )
            {
                return false;
            }
            else
            {
                return true;
            }
          
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
                addEmployee.Close();
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

        List<LocationDto> ConvertLocationDtoList(List<tblLocation>locations)
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
            locationDto.Location = string.Format("{0} {1} {2} {3}",location.Street,location.Number,
                location.City,location.Country);
            locationDto.ID = location.LocationID;

            return locationDto;
      
        }

        List<ManagerDto> ConvertManagerListToDto(List<tblEmployee>managers)
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
            managerDto.ManagerData = string.Format("{0} {1} {2}",manager.FirstName,manager.LastName,
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
 