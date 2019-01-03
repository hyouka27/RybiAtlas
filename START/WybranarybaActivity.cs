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
using Newtonsoft.Json.Linq;

namespace START
{
    [Activity(Label = "@string/app_name", Theme = "@style/AppTheme")]
    public class WybranarybaActivity : AppCompatActivity
    {
        private TextView NazwaRyby1;
        private TextView Indeks1;
        private ImageView Obrazek;
        private TextView Opis1;
        private Button dodaj;

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.activity_wybranaryba);
            NazwaRyby1 = FindViewById<TextView>(Resource.Id.NazwaRyby1);
            Indeks1 = FindViewById<TextView>(Resource.Id.Indeks1);
            Obrazek = FindViewById<ImageView>(Resource.Id.Obrazek);
            Opis1 = FindViewById<TextView>(Resource.Id.Opis1);
            dodaj = FindViewById<Button>(Resource.Id.dodaj);
            Podajnazwe(LinkBaza.Nazwa);
            Podajopis(LinkBaza.Nazwa);
            Podajindeks(LinkBaza.Nazwa);
            Podajobraz(LinkBaza.Nazwa);
            dodaj.Click += Dodaj_Click;
        }

/// <summary>

/// </summary>
private void Dodaj_Click(object sender, System.EventArgs e)
{
            InsertInfo2(LinkBaza.Nazwa, LinkBaza.numer, LinkBaza.Obrazek, LinkBaza.Opis, LinkBaza.Indeks);
}


void Podajnazwe(string nazwa)
        {
            NazwaRyby1.Text = LinkBaza.Nazwa;
        }

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
                    while (czytaj.Read())
                    {
                        Opis1.Text = czytaj.GetString(0);
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

void Podajindeks(string nazwar)
        {
            using (SqlConnection conn = new SqlConnection(LinkBaza.connString))
            {
                conn.Open();
                try
                {
                    string commandText = "select idryby from rybki WHERE Nazwaryby LIKE @user";
                    SqlCommand command = new SqlCommand(commandText, conn);
                    command.Parameters.Add(new SqlParameter("user", nazwar));
                    command.ExecuteNonQuery();
                    SqlDataReader czytaj = command.ExecuteReader();
                    while (czytaj.Read())
                    {
                        LinkBaza.Indeks = czytaj.GetInt32(0);
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
                    while (czytaj.Read())
                    {
                        LinkBaza.Obrazek = czytaj.GetString(0);
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

 void InsertInfo2(string nazwaryby, int numerkart, string obrazek, string opis, int indeks)
        {
            using (SqlConnection conn = new SqlConnection(LinkBaza.connString))
            {
                try
                {
                    conn.Open();
                    string commandText = "insert into Ulubione (Nazwaryby,numerkart,obrazek,opis,indeks) values(@user,@pass,@imie,@nazwisko,@tel)";
                    SqlCommand command = new SqlCommand(commandText, conn);
                    command.Parameters.Add(new SqlParameter("user", nazwaryby));
                    command.Parameters.Add(new SqlParameter("pass", numerkart));
                    command.Parameters.Add(new SqlParameter("imie", obrazek));
                    command.Parameters.Add(new SqlParameter("nazwisko", opis));
                    command.Parameters.Add(new SqlParameter("tel", indeks));
                    command.ExecuteNonQuery();
                    conn.Close();
                    var menu = new Intent(this, typeof(MenuActivity));
                    StartActivity(menu);
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