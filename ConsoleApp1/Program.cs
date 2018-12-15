using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;

using MySql.Data;
using MySql.Data.MySqlClient;
namespace ConsoleApp1
{
    class Program
    {
        public static void Main()
        {
            string connStr = "server=mysql32.mydevil.net;port=3306;database=m11808_baz;user=m11808_wed;password=Xamarin1@#";
            MySqlConnection conn = new MySqlConnection(connStr);
            try
            {
                Console.WriteLine("Connecting to MySQL...");
                conn.Open();
                MySqlCommand cmd = new MySqlCommand("insert into User(numerkarty,haslo) values(1,12)", conn);
                cmd.ExecuteNonQuery();
                // Perform database operations
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
            conn.Close();
            Console.WriteLine("Done.");
            Console.ReadLine();
        }
    }
}
