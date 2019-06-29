using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Android.App;
using Android.Content;
using Android.Graphics;
using Android.Graphics.Drawables;
using Android.Icu.Util;
using Android.OS;
using Android.Runtime;
using Android.Support.V7.App;
using Android.Views;
using Android.Widget;
using Java.IO;
using Java.Net;



namespace RybiAtlas
{
 
    [Activity(Label = "@string/wybranaryba", Theme = "@style/AppTheme")]
    public class Wybranarybaulubione : AppCompatActivity
    {
        /// <summary>
        /// Zmienne
        /// </summary>
        private TextView NazwaRyby1;
        private TextView Indeks1;
        private ImageView Obrazek;
        private TextView Opis1;
        private Button usun;
        private Button opisryby;
        /// <summary>
        /// Wyciąga obrazek z url.
        /// </summary>
        private Bitmap GetImageBitmapFromUrl(string url)
        {
            Bitmap imageBitmap = null;

            using (var webClient = new WebClient())
            {
                var imageBytes = webClient.DownloadData(url);
                if (imageBytes != null && imageBytes.Length > 0)
                {
                    imageBitmap = BitmapFactory.DecodeByteArray(imageBytes, 0, imageBytes.Length);
                }
            }

            return imageBitmap;
        }

        protected override void OnCreate(Bundle savedInstanceState)
        {
            /// <summary>
            /// Zawiera opisy elementów przypisane do gui jak i metody.
            /// </summary>
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.activity_rybaulub);
            NazwaRyby1 = FindViewById<TextView>(Resource.Id.NazwaRyby1);
            Indeks1 = FindViewById<TextView>(Resource.Id.Indeks1);
            Opis1 = FindViewById<TextView>(Resource.Id.Opis1);
            usun= FindViewById<Button>(Resource.Id.usun);
            opisryby = FindViewById<Button>(Resource.Id.opisryby);
            Podajobraz(LinkBaza.Nazwa2);
            string linkobrazek = LinkBaza.Obrazek;
            Obrazek = FindViewById<ImageView>(Resource.Id.Obrazek);
            var imageBitmap = GetImageBitmapFromUrl(linkobrazek);
            Obrazek.SetImageBitmap(imageBitmap);
            Podajnazwe(LinkBaza.Nazwa2);
            Podajopis(LinkBaza.Nazwa2);
            usun.Click += Usun_Click;
            opisryby.Click += Opisryby_Click;

        }

        /// <summary>
        /// Akcja po kliknięciu w przycisk, wyświetla aktywność OpisrybyActivityy.
        /// </summary>
        private void Opisryby_Click(object sender, EventArgs e)
        {
            var rybyopis = new Intent(this, typeof(OpisrybyActivityyu));
            StartActivity(rybyopis);
        }

        /// <summary>
        /// Akcja po kliknięciu w przycisk, usuwa rybe z listy i przeładowuje do menu głównego. 
        /// </summary>
        private void Usun_Click(object sender, System.EventArgs e) { 

            InsertInfo2(LinkBaza.numer, LinkBaza.Nazwa2);
            var menu = new Intent(this, typeof(MenuActivity));
            StartActivity(menu);
        }

        /// <summary>
        /// Czyta nazwę ryby.
        /// </summary>
        void Podajnazwe(string nazwa)
        {
            NazwaRyby1.Text = LinkBaza.Nazwa2;
        }

        /// <summary>
        /// Czyta opis ryby z bazy.
        /// </summary>
        void Podajopis(string nazwar)
        {
            using (SqlConnection conn = new SqlConnection(LinkBaza.connString))
            {
                conn.Open();
                try
                {
                    string commandText = "select Opis from rybki WHERE Nazwaryby LIKE @user";
                    SqlCommand command = new SqlCommand(commandText, conn);
                    command.Parameters.Add(new SqlParameter("user", nazwar));
                    command.ExecuteNonQuery();
                    SqlDataReader czytaj = command.ExecuteReader();
                    foreach (var item in czytaj)
                    {
                        LinkBaza.Opis = czytaj.GetString(0);
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
        /// Czyta adres url obrazka z ryby. 
        /// </summary>
        void Podajobraz(string nazwar)
        {
            using (SqlConnection conn = new SqlConnection(LinkBaza.connString))
            {
                conn.Open();
                try
                {
                    string commandText = "select URLObrazka from rybki WHERE Nazwaryby LIKE @user";
                    SqlCommand command = new SqlCommand(commandText, conn);
                    command.Parameters.Add(new SqlParameter("user", nazwar));
                    command.ExecuteNonQuery();
                    SqlDataReader czytaj = command.ExecuteReader();
                    foreach (var item in czytaj)
                    {
                       LinkBaza.Obrazek = czytaj.GetString(0);
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
        /// Usuwa pozycję z listy na bazie zapytania sql, indeks robi tutaj za warunek niezbędny by usunąc tylko to co dany użytkownik chce usunąć.
        /// </summary>

        void InsertInfo2(int numerkart, string indeks)
        {
            using (SqlConnection conn = new SqlConnection(LinkBaza.connString))
            {
                conn.Open();
                try
                {
                        string commandText = "DELETE FROM Ulubione WHERE numerkart=@pass AND Nazwaryby=@tel";
                        SqlCommand command = new SqlCommand(commandText, conn);
                        command.Parameters.Add(new SqlParameter("pass", numerkart));
                        command.Parameters.Add(new SqlParameter("tel", indeks));
                        command.ExecuteNonQuery();
                        conn.Close();
                        string info = "Usunięto z ulubionych.";
                        Toast.MakeText(this, info, ToastLength.Long).Show();
                    }
                
                catch
                {
                    string info = "Usunięto z ulubionych.";
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