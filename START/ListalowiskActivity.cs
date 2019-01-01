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
    public class ListalowiskActivity : AppCompatActivity
    {
        /// <summary>
        /// Zmienne. 
        /// </summary>
        private List<string> lowiska;
        private ListView listaV;

        protected override void OnCreate(Bundle savedInstanceState)
        {
            /// <summary>
            /// Zawiera opisy elementów przypisane do gui jak i metody.
            /// </summary>
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.activity_listalowisk);
            listaV = FindViewById<ListView>(START.Resource.Id.listaV);
            lowiska = new List<string>();
            Lowsika(LinkBaza.okregbaza);
            ArrayAdapter<string> adapter = new ArrayAdapter<string>(this, Android.Resource.Layout.SimpleListItem1, lowiska);
            listaV.Adapter = adapter;
            listaV.ItemClick += ListaVClick;

            /// <summary>
            /// Odczytuje z bazy liste łowisk na bazie okręgu odczytanego z dostępnej w całej aplikacji zmiennej okregbaza z klasy LinkBaza
            /// </summary>
            void Lowsika(string numerkart)
            {
                using (SqlConnection conn = new SqlConnection(LinkBaza.connString))
                {
                    conn.Open();
                    try
                    {
                        string commandText = "SELECT nazwalowiska FROM okregi INNER JOIN lowiska ON okregi.idokregu=lowiska.idokregu WHERE okregi.nazwaokregu LIKE @test";
                        SqlCommand command = new SqlCommand(commandText, conn);
                        command.Parameters.Add(new SqlParameter("test", numerkart));
                        command.ExecuteNonQuery();
                        SqlDataReader czytaj = command.ExecuteReader();
                        foreach (var item in czytaj)
                            {
                                int i= 0;
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
        /// <summary>
        /// Pamięta łowisko i zapisuje jego nazwę do zmiennej lowsikobaza z klasy LinkBaza by użyć tego w dalszej części aplikacji
        /// </summary>
        private void ListaVClick(object sender, AdapterView.ItemClickEventArgs e)
        {
                var lowsiko = new Intent(this, typeof(WybranelowiskoActivity));
                StartActivity(lowsiko);
                string lowisko1 = lowiska[e.Position];
                LinkBaza.lowsikobaza = lowisko1;
                Toast.MakeText(this,lowisko1, ToastLength.Long).Show();
        }
    }
}