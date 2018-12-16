using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;

using MySql.Data;
using MySql.Data.MySqlClient;
using System.Data.SqlClient;

namespace ConsoleApp1
{
    class Program
    {
        public static void Main()
        {


            
            

                string connString = @"workstation id=testowa.mssql.somee.com;packet size=4096;user id=hyouka27_SQLLogin_1;pwd=1234567*;data source=testowa.mssql.somee.com;persist security info=False;initial catalog=testowa
";
                using (SqlConnection conn = new SqlConnection(connString))
                {

                conn.Open();
                Console.WriteLine("Po³aczono");
                Console.ReadLine();
                    using (SqlCommand cmd = new SqlCommand("INSERT INTO Persons(PersonID, LastName) VALUES(1,'TOMEK')", conn))
                    {

                    
                    using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                        Console.WriteLine("Dodano do bazy");
                        Console.ReadLine();
                        //while (reader.Read())
                        //{
                        //    Console.WriteLine("ID: [{0}], Name: [{1}]", reader.GetValue(0), reader.GetValue(1));
                        //}
                    }
                    }
                }
            
            //Console.WriteLine("Podaj numer karty wedkarskiej: ");
            //int karta = Int32.Parse(Console.ReadLine());
            //Console.WriteLine("Podaj has³o: ");
            //int password= Int32.Parse(Console.ReadLine());
            //string connStr = "server=mysql32.mydevil.net;port=3306;database=m11808_baz;user=m11808_wed;password=Xamarin1@#";
            //MySqlConnection conn = new MySqlConnection(connStr);
            //try
            //{
            //    Console.WriteLine("Connecting to MySQL...");
            //    conn.Open();
            //    MySqlCommand cmd = new MySqlCommand("insert into User(numerkarty,haslo) values(@karta,@pass)", conn);
            //    cmd.Parameters.AddWithValue("@karta", karta);
            //    cmd.Parameters.AddWithValue("@pass", password);
            //    cmd.ExecuteNonQuery();
            //    // Perform database operations
            //}
            //catch (Exception ex)
            //{
            //    Console.WriteLine(ex.ToString());
            //}
            //conn.Close();
            //Console.WriteLine("Done.");
            //Console.ReadLine();
        }
    }
}
