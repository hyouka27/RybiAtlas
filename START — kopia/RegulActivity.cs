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
    [Activity(Label = "@string/app_nameregul", Theme = "@style/AppTheme")]
    public class RegulActivity :AppCompatActivity
    {
        /// <summary>
        /// Zmienne
        /// </summary>
        private TextView regul;
        /// <summary>
        /// Regulamin aplikacji.
        /// </summary>
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.activity_regul);

            regul = FindViewById<TextView>(Resource.Id.regul);
            Regulaminapki(LinkBaza.numer);
        }

        /// <summary>
        /// Metoda która wyświetla nam z bazy regulamin karty wędkarskiej.
        /// </summary>
        void Regulaminapki(int idregul)
        {
            using (SqlConnection conn = new SqlConnection(LinkBaza.connString))
            {
                conn.Open();
                try
                {
                    string commandText = "select regulamin from regulamin WHERE idreg=1";
                    SqlCommand command = new SqlCommand(commandText, conn);
                    command.Parameters.Add(new SqlParameter("id", idregul));
                    command.ExecuteNonQuery();
                    SqlDataReader czytaj = command.ExecuteReader();
                    while (czytaj.Read())
                    {
                        regul.Text = czytaj.GetString(0);
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