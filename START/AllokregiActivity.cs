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
    public class AllokregiActivity :AppCompatActivity
    {

        
        private RecyclerView listaall1;
      

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.activity_allokregi);
            listaall1 = FindViewById<RecyclerView>(Resource.Id.listaall1);
            Oplaconeo(LinkBaza.numer);
  
        }
        void Oplaconeo(int numerkart)
        {
            using (SqlConnection conn = new SqlConnection(LinkBaza.connString))
            {
                conn.Open();
                try
                {
                    string commandText = "select nazwisko from userI WHERE numerkarty=@user";
                    SqlCommand command = new SqlCommand(commandText, conn);
                    command.Parameters.Add(new SqlParameter("user", numerkart));
                    command.ExecuteNonQuery();
                    SqlDataReader czytaj = command.ExecuteReader();
                    while (czytaj.Read())
                    {
                        //listaall1.RecyclerEvent = czytaj.GetString(0);
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
}