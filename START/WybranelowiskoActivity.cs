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
    public class WybranelowiskoActivity : AppCompatActivity
    {
        /// <summary>
        /// Zmienne.
        /// </summary>
        private TextView nazwa;
        private TextView granica;
        private TextView zbiorniki;
        private TextView okreg;

        protected override void OnCreate(Bundle savedInstanceState)
        {
            /// <summary>
            /// Zawiera opisy elementów przypisane do gui jak i metody.
            /// </summary>
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.activity_wybranelowisko);
            nazwa = FindViewById<TextView>(Resource.Id.getnazwalowsika);
            granica = FindViewById<TextView>(Resource.Id.getgranice);
            zbiorniki = FindViewById<TextView>(Resource.Id.getzbiorniki);
            okreg = FindViewById<TextView>(Resource.Id.getokreg);
            Podajnazwe(LinkBaza.lowsikobaza);
            Podajgranice(LinkBaza.lowsikobaza);
            Podajzbiorniki(LinkBaza.lowsikobaza);
            Podajokreg(LinkBaza.lowsikobaza);
        }

        /// <summary>
        /// Metoda pobiera z bazy nazwę łowiska.
        /// </summary>
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
                        nazwa.Text = czytaj.GetString(0);
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

        /// <summary>
        /// Metoda pobiera z bazy granice łowiska.
        /// </summary>
        void Podajgranice(string numerkart)
        {
            using (SqlConnection conn = new SqlConnection(LinkBaza.connString))
            {
                conn.Open();
                try
                {
                    string commandText = "select  granicelowiska from lowiska WHERE nazwalowiska LIKE @user";
                    SqlCommand command = new SqlCommand(commandText, conn);
                    command.Parameters.Add(new SqlParameter("user", numerkart));
                    command.ExecuteNonQuery();
                    SqlDataReader czytaj = command.ExecuteReader();
                    while (czytaj.Read())
                    {
                        granica.Text = czytaj.GetString(0);
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

        /// <summary>
        /// Metoda pobiera z bazy zbiorniki łowiska.
        /// </summary>
        void Podajzbiorniki(string numerkart)
        {
            using (SqlConnection conn = new SqlConnection(LinkBaza.connString))
            {
                conn.Open();
                try
                {
                    string commandText = "select zbiorniki from lowiska WHERE nazwalowiska LIKE @user";
                    SqlCommand command = new SqlCommand(commandText, conn);
                    command.Parameters.Add(new SqlParameter("user", numerkart));
                    command.ExecuteNonQuery();
                    SqlDataReader czytaj = command.ExecuteReader();
                    while (czytaj.Read())
                    {
                        zbiorniki.Text = czytaj.GetString(0);

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

        /// <summary>
        /// Metoda pobiera z bazy nazwę okręgu w którym jest łowsiko.
        /// </summary>
        void Podajokreg(string numerkart)
        {
            using (SqlConnection conn = new SqlConnection(LinkBaza.connString))
            {
                conn.Open();
                try
                {
                    string commandText = "select nazwaokregu from lowiska WHERE nazwalowiska LIKE @user";
                    SqlCommand command = new SqlCommand(commandText, conn);
                    command.Parameters.Add(new SqlParameter("user", numerkart));
                    command.ExecuteNonQuery();
                    SqlDataReader czytaj = command.ExecuteReader();
                    while (czytaj.Read())
                    {
                        okreg.Text = czytaj.GetString(0);
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