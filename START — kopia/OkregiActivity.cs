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
    [Activity(Label = "@string/okregi", Theme = "@style/AppTheme")]
    public class OkregiActivity : AppCompatActivity
    {
        /// <summary>
        /// Zmienne
        /// </summary>
        private List<string> okrega;
        private ListView lista;
    
        protected override void OnCreate(Bundle savedInstanceState)
        {
            /// <summary>
            /// Zawiera opisy elementów przypisane do gui jak i metody.
            /// </summary>
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.activity_okregi);
            lista = FindViewById<ListView>(START.Resource.Id.lista);
            okrega = new List<string>();
            Okrega(LinkBaza.numer);
            ArrayAdapter<string> adapter = new ArrayAdapter<string>(this, Android.Resource.Layout.SimpleListItem1, okrega);
            lista.Adapter = adapter;
            lista.ItemClick += ListaVClick;
        }

        /// <summary>
        /// Akcja po kliknięciu na wybrany okręg plus zapisanie jego nazwy do zmiennej pomocniczej okregbaza z klasy LinkBaza.
        /// </summary>
        private void ListaVClick(object sender, AdapterView.ItemClickEventArgs e)
        {
            var okreg = new Intent(this, typeof(WybranyokregActivity));
            StartActivity(okreg);
            string okregall1 = okrega[e.Position];
            LinkBaza.okregbaza = okregall1;
            Toast.MakeText(this, okregall1, ToastLength.Long).Show();
        }

        /// <summary>
        /// Metoda sprawdza czy użytkownik zapłacił za dany okręg, jeśli nie okręg nie zostanie wyświetlony, pokazuje opłacone okręgi.
        /// </summary>
        void Okrega(int numerkart)
        {
            using (SqlConnection conn = new SqlConnection(LinkBaza.connString))
            {
                conn.Open();
                try
                {
                    string commandText = "SELECT nazwaokregu FROM okregi INNER JOIN oplacone ON okregi.idokregu=oplacone.idokregu WHERE oplacone LIKE 'tak' AND numerkarty LIKE @user";
                    SqlCommand command = new SqlCommand(commandText, conn);
                    command.Parameters.Add(new SqlParameter("user", numerkart));
                    command.ExecuteNonQuery();
                    SqlDataReader czytaj = command.ExecuteReader();
                    foreach (var item in czytaj)
                    {
                        int i = 0;
                        okrega.Add(czytaj.GetString(i));
                        i++;
                    }
                }
                catch
                {
                Toast.MakeText(this, "Brak wykupionych okręgów", ToastLength.Long).Show();
                }
                finally
                {
                    conn.Close();
                }
            }
        }

    }
}