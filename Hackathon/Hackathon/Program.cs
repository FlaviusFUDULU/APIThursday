using System;
using System.Data;
using System.Data.SqlClient;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PasswordEncryptionTool.Model;

namespace PasswordEncryptionTool
{
    class Program
    {
        static void Main(string[] args)
        {

            using (SqlConnection conn = new SqlConnection())
            {
                Console.Write("Server Name: ");
                string server = Console.ReadLine();
                Console.Write("Database: ");
                string database = Console.ReadLine();
                Console.Write("Username: ");
                string username = Console.ReadLine();
                Console.Write("Pssword: ");
                string pass = Console.ReadLine();

                Console.Clear();

                Console.WriteLine("Make sure data is correct");
                Console.WriteLine("-----------------------");
                Console.WriteLine("Server Name: " + server);
                Console.WriteLine("Database: " + database);
                Console.WriteLine("Username: " + username);
                Console.WriteLine("Pssword: " + pass);
                Console.WriteLine("-----------------------");
                Console.WriteLine("Press enter if data is correct, otherwise abort");
                Console.ReadLine();

                conn.ConnectionString = "Server=" + server + ";Database=" + database + ";Trusted_Connection=false;MultipleActiveResultSets=true;user id=" + username + ";password=" + pass;
                conn.Open();

                Console.Clear();

                List<DummyResponse> dbQueryResponse = RunDummySql(conn);

                Console.ReadLine();
                
            }
        }

        private static List<DummyResponse> RunDummySql(SqlConnection conn)
        {
            List<DummyResponse> response = new List<DummyResponse>();


            SqlCommand command = new SqlCommand("SELECT ObjectID, Description FROM Insights", conn);

            using (SqlDataReader reader = command.ExecuteReader())
            {
                Console.WriteLine("ObjectId \t Description");
                while (reader.Read())
                {
                    Console.WriteLine(String.Format("{0} \t | {1} \t |  ",
                        reader[0], reader[1]));
                    if (reader[1].ToString().Length < 1)
                    { 
                        DummyResponse dummy = new DummyResponse
                        {
                            ObjectID = Int32.Parse(reader[0].ToString()),
                            Description = reader[1].ToString()
                        };
                        response.Add(dummy);                       
                    }

                }
            }
            return response;
        }

    }
}
