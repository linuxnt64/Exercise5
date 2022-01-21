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
using System.Windows.Navigation;
using System.Windows.Shapes;
using TaskCollector.Models;
using TaskCollector.Services;

namespace TaskCollector
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public Customer customer = new Customer();
        public CustomerService customerService = new CustomerService();
        public List<Customer> customerList = new List<Customer>();

        public MainWindow()
        {
            InitializeComponent();
            customerList = customerService.GetCustomerToListFromDB();
            foreach (var c in customerList)
            {
                lvCustomerList.Items.Add(c);
            }
        }

        private void btnAddCustomer_Click(object sender, RoutedEventArgs e)
        {
            customer.Name = tbName.Text;                    
            customer.Phone = tbPhone.Text;                             
            customer.Mail = tbMail.Text;
            customer.Company = tbCompany.Text;
            customer.Address = tbAddress.Text;

            customerService.CreateCustomer(customer);
            lvCustomerList.Items.Add(customer);
        }

        private void lvCustomerList_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            
        }

        private void btnAddTask_Click(object sender, RoutedEventArgs e)
        {

        }

        private void btnUpdateTask_Click(object sender, RoutedEventArgs e)
        {
           
        }
    }
}

/*

"Billy Badboll";
"0123456768";
"BBad@domain.com"
"BolagetS"
"Glada gatan 1"
 
 */