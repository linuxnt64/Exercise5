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
        public Models.Task task = new();
        public TaskService taskService = new();
        public List<Models.Task> taskList = new();

        public MainWindow()
        {
            InitializeComponent();
            ShowCustomerList();
            ShowTaskList();
        }

        private void ShowCustomerList()
        {
            customerList = customerService.GetCustomerToListFromDB();
            lvCustomerList.Items.Clear();
            foreach (var c in customerList)
            {
                lvCustomerList.Items.Add(c);
            }
        }

        private void ShowTaskList()
        {
            taskList = taskService.GetTaskToListFromDB();
            lvTaskList.Items.Clear();
            foreach (var t in taskList)
            {
                lvTaskList.Items.Add(t);
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
            ShowCustomerList();
        }

        private void lvCustomerList_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }
        // (Id, Description, Category, Status, CustomerID)

        private void btnAddTask_Click(object sender, RoutedEventArgs e)
        {
            WriteTaskToDB();
            taskService.CreateTask(task);
            ShowTaskList();
            ClearTaskForm();
        }


        private void btnUpdateTask_Click(object sender, RoutedEventArgs e)
        {
            WriteTaskToDB();
            taskService.UpdateTask(task);
            ShowTaskList();
            ClearTaskForm();
        }

        private void ClearTaskForm()
        {
            tbDescription.Clear();
            tbCategory.Clear();
            tbStatus.Clear();
            tbCustomerID.Clear();
        }

        private void WriteTaskToDB()
        {
            task.Description = tbDescription.Text;
            task.Category = tbCategory.Text;
            task.Status = tbStatus.Text;
            task.CustomerID = int.Parse(tbCustomerID.Text);
        }

        private void lvTaskList_MouseUp(object sender, MouseButtonEventArgs e)
        {
            var selectedTask = lvTaskList.SelectedIndex;
            task.Id = taskList[selectedTask].Id;                        // Behövs för att update DB (btnUpdateTask_Click) ska fungera
            tbDescription.Text = taskList[selectedTask].Description;
            tbCategory.Text = taskList[selectedTask].Category;
            tbStatus.Text = taskList[selectedTask].Status;
            tbCustomerID.Text = taskList[selectedTask].CustomerID.ToString();
            lvCustomerList.SelectedIndex = taskList[selectedTask].CustomerID-1;
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