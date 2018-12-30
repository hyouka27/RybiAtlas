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
using Android.Views;
using Android.Widget;

namespace START
{
    [Activity(Label = "@string/app_name", Theme = "@style/AppTheme", MainLauncher = true)]
    public class ListalowiskActivity : AppCompatActivity
    {
        private Button btnmenu;
        private List<string> lowiska;
        private ListView listaV;


        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.activity_listalowisk);
            listaV = FindViewById<ListView>(START.Resource.Id.listaV);
            lowiska = new List<string>();
            Lowsika(LinkBaza.numer);
            ArrayAdapter<string> adapter = new ArrayAdapter<string>(this, Android.Resource.Layout.SimpleListItem1, lowiska);
            listaV.Adapter = adapter;

            listaV.ItemClick += ListaVClick;
            //btnmenu.Click += delegate
            //{
            //    var menu = new Intent(this, typeof(MenuActivity));
            //    StartActivity(menu);
            //};


            void Lowsika(int numerkart)
            {
                using (SqlConnection conn = new SqlConnection(LinkBaza.connString))
                {
                    conn.Open();
                    try
                    {
                        string commandText = "SELECT nazwalowiska FROM lowiska";
                        SqlCommand command = new SqlCommand(commandText, conn);
                       // command.Parameters.Add(new SqlParameter("user", numerkart));
                        command.ExecuteNonQuery();
                        SqlDataReader czytaj = command.ExecuteReader();
                        foreach (var item in czytaj)
                            {
                                int i = 0;
                                lowiska.Add(czytaj.GetString(i));
                                i++;
                            }
                    }
                    catch
                    {
                    }
                    finally
                    {
                        conn.Close();
                    }
                }
            }
        }

        private void ListaVClick(object sender, AdapterView.ItemClickEventArgs e)
        {
            if (lowiska[e.Position] == "")
            {
                
            }
        }
    }
}