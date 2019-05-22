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
            Szukaj2 = FindViewById<TextView>(Resource.Id.Szukaj2);
            bszukaj = FindViewById<Button>(Resource.Id.bszukaj);
            bszukaj.Click += Bszukaj_Click; 
        }

        /// <summary>
        /// Przycisk do analizy wyszukiwanej ryby
        /// </summary>
        private void Bszukaj_Click(object sender, EventArgs e)
        {
            string szuka2 = szuka.Text;
            LinkBaza.Nazwa = szuka2;
            Podajrybe(szuka2);
        }

        /// <summary>
        /// Metoda wyszukująca rybe w bazie i włączająca ekran z widokiem ryby jak znajdzie a jak nie to ładująca komunikat
        /// </summary>
        void Podajrybe(string nazwa)
        {
            using (SqlConnection conn = new SqlConnection(LinkBaza.connString))
            {
                conn.Open();
                string Output = "";
                try
                {
                    string commandText ="select count(*) as cnt FROM rybki WHERE Nazwaryby LIKE @test";
                    SqlCommand command = new SqlCommand(commandText, conn);
                    command.Parameters.Add(new SqlParameter("test", nazwa));
                    command.ExecuteNonQuery();
                    SqlDataReader czytaj = command.ExecuteReader();
                    while (czytaj.Read())
                    {
                        Output = Output + czytaj.GetValue(0);
                    }
                    int test;
                    test = Int32.Parse(Output);
                    if (test == 1)
                    {
                        var ulub23 = new Intent(this, typeof(WybranarybaActivity));
                        StartActivity(ulub23);
                    }
                    else
                    {
                        string info = "Nie ma takiej ryby";
                        Toast.MakeText(this, info, ToastLength.Long).Show();

                    }
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
        }
    }
}