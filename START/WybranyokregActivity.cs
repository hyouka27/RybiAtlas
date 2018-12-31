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
    [Activity(Label = "@string/app_name", Theme = "@style/AppTheme")]
    public class WybranyokregActivity : AppCompatActivity
    {
        private Button btnmenu;
        private Button btnlowiska;
        private Button btnregulokreg;

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.activity_wybranyokreg);
            btnlowiska = FindViewById<Button>(Resource.Id.btnlowiska);
            btnlowiska.Click += delegate {
                var lowiska = new Intent(this, typeof(ListalowiskActivity));
                StartActivity(lowiska);
            };
            btnregulokreg = FindViewById<Button>(Resource.Id.btnregulokreg);
            btnregulokreg.Click += delegate {
                var rokreg = new Intent(this, typeof(RegulokregActivity));
                StartActivity(rokreg);
            };
            Podajnazwe(LinkBaza.lowsikobaza);

        }

        void Podajnazwe(string numerkart)
        {
            using (SqlConnection conn = new SqlConnection(LinkBaza.connString))
            {
                conn.Open();
                try
                {
                    string commandText = "select nazwalowiska from lowiska WHERE nazwalowiska LIKE @user";
                    SqlCommand command = new SqlCommand(commandText, conn);
                    command.Parameters.Add(new SqlParameter("user", numerkart));
                    command.ExecuteNonQuery();
                    SqlDataReader czytaj = command.ExecuteReader();
                    while (czytaj.Read())
                    {
                       string test = czytaj.GetString(0);
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
        private void btnlowiska_Click(object sender, System.EventArgs e)
        {
            SetContentView(Resource.Layout.activity_listalowisk);
        }
        private void btnregulokreg_Click(object sender, System.EventArgs e)
        {
            SetContentView(Resource.Layout.activity_regulokreg);
        }

    }
}
