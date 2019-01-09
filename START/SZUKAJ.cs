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
    [Activity(Label = "@string/wybranelowisko", Theme = "@style/AppTheme")]
    public class SZUKAJ : AppCompatActivity
    {
        /// <summary>
        /// Zmienne.
        /// </summary>
        private EditText szuka;
        private TextView Szukaj2;
        private Button bszukaj;

        protected override void OnCreate(Bundle savedInstanceState)
        {
            /// <summary>
            /// Zawiera opisy elementów przypisane do gui jak i metody.
            /// </summary>
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.szukaj);
            szuka = FindViewById<EditText>(Resource.Id.szuka);
            Szukaj2 = FindViewById<EditText>(Resource.Id.Szukaj2);
            bszukaj = FindViewById<Button>(Resource.Id.bszukaj);
            bszukaj.Click += delegate {
                using (SqlConnection conn = new SqlConnection(LinkBaza.connString))
                {
                    conn.Open();
                    try
                    {
                        string commandText = "SELECT Nazwaryby FROM rybki WHERE Nazwaryby LIKE @test";
                        SqlCommand command = new SqlCommand(commandText, conn);
                        command.Parameters.Add(new SqlParameter("test", szuka));
                        command.ExecuteNonQuery();
                        SqlDataReader czytaj = command.ExecuteReader();
                        while (czytaj.Read())
                        {
                            Szukaj2.Text = czytaj.GetString(0);
                        }
                        var ulub23 = new Intent(this, typeof(WybranarybaActivity));
                        StartActivity(ulub23);
                    }
                    catch
                    {
                        string info = "Nie ma takiej ryby";
                        Toast.MakeText(this, info, ToastLength.Long).Show();
                    }
                    finally
                    {
                        conn.Close();
                    }
                }
            };
        }
    }
}