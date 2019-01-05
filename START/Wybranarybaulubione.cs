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
using Android.Icu.Util;
using Android.OS;
using Android.Runtime;
using Android.Support.V7.App;
using Android.Views;
using Android.Widget;
using Newtonsoft.Json.Linq;

namespace START
{
    [Activity(Label = "@string/wybranaryba", Theme = "@style/AppTheme")]
    public class Wybranarybaulubione : AppCompatActivity
    {
        private TextView NazwaRyby1;
        private TextView Indeks1;
        private ImageView Obrazek;
        private TextView Opis1;
        private Button usun;

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.activity_rybaulub);
            NazwaRyby1 = FindViewById<TextView>(Resource.Id.NazwaRyby1);
            Indeks1 = FindViewById<TextView>(Resource.Id.Indeks1);
            Obrazek = FindViewById<ImageView>(Resource.Id.Obrazek);
            Opis1 = FindViewById<TextView>(Resource.Id.Opis1);
            usun= FindViewById<Button>(Resource.Id.usun);
            Podajnazwe(LinkBaza.Nazwa);
            Podajobraz(LinkBaza.Indeks);
            Podajopis(LinkBaza.Indeks);
            usun.Click += Usun_Click;
            string linkobrazek = LinkBaza.Obrazek;
        }

        private async Task<Bitmap> GetImageBitmapFromUrlAsync(string linkobrazek)
        {
            Bitmap imageBitmap = null;

            using (var httpClient = new HttpClient())
            {
                var imageBytes = await httpClient.GetByteArrayAsync(linkobrazek);
                if (imageBytes != null && imageBytes.Length > 0)
                {
                    imageBitmap = BitmapFactory.DecodeByteArray(imageBytes, 0, imageBytes.Length);
                }
            }
            
            return imageBitmap;
        }

        private void Usun_Click(object sender, System.EventArgs e)
{
            InsertInfo2(LinkBaza.numer, LinkBaza.Indeks);
}


void Podajnazwe(string nazwa)
        {
            NazwaRyby1.Text = LinkBaza.Nazwa;
            using (SqlConnection conn = new SqlConnection(LinkBaza.connString))
            {
                conn.Open();
                try
                {
                    string commandText = "select idryby from Ulubione WHERE Nazwaryby LIKE @user";
                    SqlCommand command = new SqlCommand(commandText, conn);
                    command.Parameters.Add(new SqlParameter("user", nazwa));
                    command.ExecuteNonQuery();
                    SqlDataReader czytaj = command.ExecuteReader();
                    foreach (var item in czytaj)
                    {
                        int i = 0;
                        LinkBaza.Indeks = czytaj.GetInt32(i);
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

void Podajopis(int nazwar)
        {
            using (SqlConnection conn = new SqlConnection(LinkBaza.connString))
            {
                conn.Open();
                try
                {
                    string commandText = "select Opis from Ulubione WHERE idryby LIKE @user";
                    SqlCommand command = new SqlCommand(commandText, conn);
                    command.Parameters.Add(new SqlParameter("user", nazwar));
                    command.ExecuteNonQuery();
                    SqlDataReader czytaj = command.ExecuteReader();
                    foreach (var item in czytaj)
                    {
                        Opis1.Text = czytaj.GetString(0);
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


void Podajobraz(int nazwar)
        {
            using (SqlConnection conn = new SqlConnection(LinkBaza.connString))
            {
                conn.Open();
                try
                {
                    string commandText = "select URLObrazka from Ulubione WHERE idryby LIKE @user";
                    SqlCommand command = new SqlCommand(commandText, conn);
                    command.Parameters.Add(new SqlParameter("user", nazwar));
                    command.ExecuteNonQuery();
                    SqlDataReader czytaj = command.ExecuteReader();
                    foreach (var item in czytaj)
                    {
                       LinkBaza.Obrazek = czytaj.GetString(0);

                        //Obrazek.SetImageURI(TEST);
                        //Obrazek ImageView = czytaj.GetString(0);

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


 void InsertInfo2(int numerkart, int indeks)
        {
            using (SqlConnection conn = new SqlConnection(LinkBaza.connString))
            {
                try
                {
                    conn.Open();
                        string commandText = "DELETE FROM Ulubione WHERE numerkart=@pass AND idryby=@tel";
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
                    string info = "Brak dostępu do sieci.";
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