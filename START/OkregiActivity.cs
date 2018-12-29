using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Support.V7.App;
using Android.Support.V7.Widget;
using Android.Views;
using Android.Widget;

namespace START
{
    [Activity(Label = "@string/app_name", Theme = "@style/AppTheme")]
    public class OkregiActivity : AppCompatActivity
    {
        private Button btnmenu;
        private RecyclerView lista1;

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.activity_okregi);


            btnmenu.Click += delegate {
                var menu = new Intent(this, typeof(MenuActivity));
                StartActivity(menu);
            };
        }

        private void Btnmenu_Click(object sender, System.EventArgs e)
        {

            SetContentView(Resource.Layout.activity_menu);
        }

        void Wyswietlokregi(int numerkart, string pass2)
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
                    if (test == numerkart)
                    {


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