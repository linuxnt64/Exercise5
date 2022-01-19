using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskCollector.Models;

namespace TaskCollector.Services
{
    public class CustomerService
    {
        private readonly string _connectionstring = @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\LEXICON\Grupparbete\Exercise5-main\TaskManager\TaskCollector\Data\TaskSQLDB.mdf;Integrated Security=True";

        public void CreateCustomer(Customer customer)
        {
            using (SqlConnection conn = new SqlConnection(_connectionstring))
            {
                conn.Open();
                using (SqlCommand cmd = new SqlCommand("IF NOT EXISTS (SELECT Id FROM CUSTOMER WHERE Name = @CustomerName) INSERT INTO CUSTOMER (Name, Phone, Mail, Company, Address) VALUES  (@CustomerName , @CustomerPhone, @CustomerMail , @CustomerCompany , @CustomerAddress )", conn))
                {
                    cmd.Parameters.AddWithValue("@CustomerName", customer.Name);
                    cmd.Parameters.AddWithValue("@CustomerPhone", customer.Phone);
                    cmd.Parameters.AddWithValue("@CustomerMail", customer.Mail);
                    cmd.Parameters.AddWithValue("@CustomerCompany", customer.Company);
                    cmd.Parameters.AddWithValue("@CustomerAddress", customer.Address);
                    cmd.ExecuteNonQuery();
                }
            }
        }

        public List<Customer> GetCustomerToListFromDB()
        {
            List<Customer> customerList = new List<Customer>();
            using (SqlConnection conn = new SqlConnection(_connectionstring))
            {
                conn.Open();
                using (SqlCommand cmd = new SqlCommand("SELECT * FROM CUSTOMER", conn))
                {
                    var result = cmd.ExecuteReader();
                    while (result.Read())
                    {
                        customerList.Add(new Customer
                        {
                            //(Name, Phone, Mail, Company, Address)
                            Id = int.Parse(result.GetValue(0).ToString()),
                            Name = result.GetValue(1).ToString(),
                            Phone = result.GetValue(2).ToString(),
                            Mail = result.GetValue(3).ToString(),
                            Company = result.GetValue(4).ToString(),
                            Address = result.GetValue(5).ToString()
                        });
                    }
                }
            }
            return customerList;
        }
    }
}
