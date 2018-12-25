using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;

namespace START
{
    public static class LinkBaza
    {
    public static string connString = @"workstation id=testowa.mssql.somee.com;packet size=4096;user id=hyouka27_SQLLogin_1;pwd=1234567*;data source=testowa.mssql.somee.com;persist security info=False;initial catalog=testowa";
    }
    public class Login
    {
        public int numerkart;
        public string pass2;
        public void Danelogin(int numerkart, string pass2)
        {
            using (SqlConnection conn = new SqlConnection(LinkBaza.connString))
            {
                conn.Open();

                try
                {
                    string Output = "";
                    string commandText = "select numerkarty as cnt from userI WHERE numerkarty=@user AND haslo LIKE @pass";
                    SqlCommand command = new SqlCommand(commandText, conn);
                    command.Parameters.Add(new SqlParameter("user", numerkart));
                    command.Parameters.Add(new SqlParameter("pass", pass2));
                    command.ExecuteNonQuery();
                    SqlDataReader czytaj = command.ExecuteReader();
                    while (czytaj.Read())
                    {
                        Output = Output + czytaj.GetValue(0);
                    }
                    int test;
                    test = Int32.Parse(Output);
                    if (test==numerkart)
                    {
                      this.numerkart=numerkart;

                    }
                    else
                    {
                        //tvTips.Text = "Błędny login lub hasło";

                    }
                }
                catch
                {
                   // tvTips.Text = "Nie możesz się zalogować, popraw dane.";
                    //using (SqlDataReader reader = cmd.ExecuteReader())
                    //{
                    //    //while (reader.Read())
                    //    //{
                    //    //    Console.WriteLine("ID: [{0}], Name: [{1}]", reader.GetValue(0), reader.GetValue(1));
                    //    //}
                    //}
                }
                finally
                {
                    conn.Close();
                }
            }
        }
    }
}