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
        private List<string> okrega;
        private ListView lista;

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.activity_okregi);
            lista = FindViewById<ListView>(START.Resource.Id.lista);
            okrega = new List<string>();
            Okrega(LinkBaza.numer);
            ArrayAdapter<string> adapter = new ArrayAdapter<string>(this, Android.Resource.Layout.SimpleListItem1, okrega);
            lista.Adapter = adapter;
            lista.ItemClick += ListaVClick;


        }
        private void ListaVClick(object sender, AdapterView.ItemClickEventArgs e)
        {
            var okreg = new Intent(this, typeof(WybranyokregActivity));
            StartActivity(okreg);
            string okregall1 = okrega[e.Position];
            LinkBaza.okregbaza = okregall1;
            Toast.MakeText(this, okregall1, ToastLength.Long).Show();
        }



        void Okrega(int numerkart)
        {
            using (SqlConnection conn = new SqlConnection(LinkBaza.connString))
            {
                conn.Open();
                try
                {
                    string commandText = "SELECT nazwaokregu FROM okregi INNER JOIN oplacone ON okregi.idokregu=oplacone.idokregu WHERE oplacone LIKE 'tak'";
                    SqlCommand command = new SqlCommand(commandText, conn);
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
                }
                finally
                {
                    conn.Close();
                }
            }
        }

    }
}