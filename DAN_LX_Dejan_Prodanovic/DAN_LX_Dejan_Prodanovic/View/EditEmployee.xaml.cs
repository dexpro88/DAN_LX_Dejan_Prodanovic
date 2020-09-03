using DAN_LX_Dejan_Prodanovic.Dto;
using DAN_LX_Dejan_Prodanovic.Model;
using DAN_LX_Dejan_Prodanovic.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace DAN_LX_Dejan_Prodanovic.View
{
    /// <summary>
    /// Interaction logic for EditEmployee.xaml
    /// </summary>
    public partial class EditEmployee : Window
    {
        public EditEmployee()
        {
            InitializeComponent();
        }
        public EditEmployee(EmployeeDto employee)
        {
            InitializeComponent();
            DataContext = new EditEmployeeViewModel(this, employee);
        }
    }
}
