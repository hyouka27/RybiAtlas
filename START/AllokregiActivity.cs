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
        /// <summary>
        /// Zawiera zmienne dla listy. 
        /// </summary>
        private List<string> okregall;
        private ListView listall;


        protected override void OnCreate(Bundle savedInstanceState)
        {
            /// <summary>
            /// Zawiera opisy elementów przypisane do gui jak i metody.
            /// </summary>
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.activity_allokregi);
            listall = FindViewById<ListView>(START.Resource.Id.listall);
            okregall = new List<string>();
            Okregall(LinkBaza.numer);
            ArrayAdapter<string> adapter = new ArrayAdapter<string>(this, Android.Resource.Layout.SimpleListItem1, okregall);
            listall.Adapter = adapter;
            listall.ItemClick += ListaVClick;
  
        }

        /// <summary>
        /// Działanie po kliknięciu w pozycję z listy, okreg to zmienna w której opsiano jaki ekran ma włączać, zaś poniżej przypisujemy to do LinkBaza.okregbaza = okregall1; statycznej zmiennej tej klasy tak by w podstronie kolejnej był punkt odniesienia, w sensie co ma ładować po wybraniu elementu z listy.
        /// </summary>
        private void ListaVClick(object sender, AdapterView.ItemClickEventArgs e)
        {
            var okreg = new Intent(this, typeof(WybranyokregActivity));
            StartActivity(okreg);
            string okregall1 = okregall[e.Position];
            LinkBaza.okregbaza = okregall1;
            Toast.MakeText(this, okregall1, ToastLength.Long).Show();
        }

        /// <summary>
        /// Metoda do pobrania z bazy przy pomocy zapytania nazwyokregów, potem wypisuje to do listy na bazie pętli foreach
        /// </summary>
        void Okregall(int numerkart)
        {
            using (SqlConnection conn = new SqlConnection(LinkBaza.connString))
            {
                conn.Open();
                try
                {
                    string commandText = "SELECT nazwaokregu FROM okregi";
                    SqlCommand command = new SqlCommand(commandText, conn);
                    command.ExecuteNonQuery();
                    SqlDataReader czytaj = command.ExecuteReader();
                    foreach (var item in czytaj)
                    {
                        int i = 0;
                        okregall.Add(czytaj.GetString(i));
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
}