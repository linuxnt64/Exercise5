using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskCollector.Models;



namespace TaskCollector.Services
{
    public class TaskService
    {
        private readonly string _connectionstring = @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=S:\TaskManager\TaskCollector\Data\TaskSQLDB.mdf;Integrated Security=True;Integrated Security=True";

        public void CreateTask(Models.Task task)
        {
            using (SqlConnection conn = new SqlConnection(_connectionstring))
            {
                conn.Open();
                //               using (SqlCommand cmd = new SqlCommand("IF NOT EXISTS (SELECT Category FROM TASK WHERE Category = @TaskCategory) INSERT INTO TASK (Description, Category, Status, CustomerId) VALUES (@TaskDescription , @TaskCategory , @TaskStatus , @TaskCustomerId)", conn))
                using (SqlCommand cmd = new SqlCommand("INSERT INTO TASK (Description, Category, Status, CustomerId) VALUES (@TaskDescription , @TaskCategory , @TaskStatus , @TaskCustomerId)", conn))
                {
                    cmd.Parameters.AddWithValue("@TaskDescription", task.Description);
                    cmd.Parameters.AddWithValue("@TaskCategory", task.Category);
                    cmd.Parameters.AddWithValue("@TaskStatus", task.Status);
                    cmd.Parameters.AddWithValue("@TaskCustomerID", task.CustomerID);
                    cmd.ExecuteNonQuery();
                }
            }

        }
        internal void UpdateTask(Models.Task task)
        {
            List<Models.Task> taskList = new List<Models.Task>();
            using (SqlConnection conn = new SqlConnection(_connectionstring))
            {
                conn.Open();
                using (SqlCommand cmd = new SqlCommand("UPDATE TASK SET Status = @TaskStatus WHERE Id = @Id", conn))
                {
                    cmd.Parameters.AddWithValue("@TaskStatus", task.Status);
                    cmd.Parameters.AddWithValue("@Id", task.Id);
                    cmd.ExecuteNonQuery();
                }
            }
        }
        public List<Models.Task> GetTaskToListFromDB()
        {
            List<Models.Task> taskList = new List<Models.Task>();
            using (SqlConnection conn = new SqlConnection(_connectionstring))
            {
                conn.Open();
                using (SqlCommand cmd = new SqlCommand("SELECT * FROM TASK", conn))
                {
                    var result = cmd.ExecuteReader();
                    while (result.Read())
                    {
                        taskList.Add(new Models.Task
                        {
                            // (Id, Description, Category, Status, CustomerID)
                            Id = int.Parse(result.GetValue(0).ToString()),
                            Description = result.GetValue(1).ToString(),
                            Category = result.GetValue(2).ToString(),
                            Status = result.GetValue(3).ToString(),
                            CustomerID = int.Parse(result.GetValue(4).ToString())
                        });
                    }
                }
            }
            return taskList;
        }
    }
}
