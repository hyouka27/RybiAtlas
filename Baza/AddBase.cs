using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using Android.App;
using Android.OS;
using Android.Support.V7.App;
using Android.Runtime;
using Android.Widget;
using MySql.Data;
using MySql.Data.MySqlClient;

namespace Baza
{
    class AddBase
    {
        public static void Main()
        {
            
            string connStr = "server=mysql32.mydevil.net;port=3306;database=m11808_baz;user=m11808_wed;password=Xamarin1@#";
            MySqlConnection conn = new MySqlConnection(connStr);
            try
            {
                string sql = "insert into User(numerkarty,haslo) values(@user,@pass)";
                conn.Open();
                MySqlCommand cmd = new MySqlCommand(sql, conn);
                cmd.ExecuteNonQuery();
                // Perform database operations
            }
            catch (Exception)
            {

                //tvTips.Text = "Zalogowano pomyślnie";
            }
            conn.Close();
        }
        
    }
}
