using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using Android.App;
using Android.Content;
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
    public class WybranarybaActivity : AppCompatActivity
    {
        private TextView NazwaRyby1;
        private TextView Indeks1;
        private ImageView Obrazek;
        private TextView Opis1;
        private Button dodaj;
        private Button opisryby;

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.activity_wybranaryba);
            NazwaRyby1 = FindViewById<TextView>(Resource.Id.NazwaRyby1);
            Indeks1 = FindViewById<TextView>(Resource.Id.Indeks1);
            Obrazek = FindViewById<ImageView>(Resource.Id.Obrazek);
            Opis1 = FindViewById<TextView>(Resource.Id.Opis1);
            dodaj = FindViewById<Button>(Resource.Id.dodaj);
            opisryby = FindViewById<Button>(Resource.Id.opisryby);
            Podajnazwe(LinkBaza.Nazwa);
            Podajobraz(LinkBaza.Indeks);
            Podajopis(LinkBaza.Indeks);
            dodaj.Click += Dodaj_Click;
            opisryby.Click += Opisryby_Click;
        }

private void Opisryby_Click(object sender, EventArgs e)
        {
            var rybyopis = new Intent(this, typeof(OpisrybyActivityy));
            StartActivity(rybyopis);
        }

private void Dodaj_Click(object sender, System.EventArgs e)
{
            InsertInfo2(LinkBaza.Nazwa, LinkBaza.numer, LinkBaza.Obrazek, LinkBaza.Opis, LinkBaza.Indeks);
}


void Podajnazwe(string nazwa)
        {
            NazwaRyby1.Text = LinkBaza.Nazwa;
            using (SqlConnection conn = new SqlConnection(LinkBaza.connString))
            {
                conn.Open();
                try
                {
                    string commandText = "select idryby from rybki WHERE Nazwaryby LIKE @user";
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
                    string commandText = "select Opis from rybki WHERE idryby LIKE @user";
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


void Podajobraz(int nazwar)
        {
            using (SqlConnection conn = new SqlConnection(LinkBaza.connString))
            {
                conn.Open();
                try
                {
                    string commandText = "select URLObrazka from rybki WHERE idryby LIKE @user";
                    SqlCommand command = new SqlCommand(commandText, conn);
                    command.Parameters.Add(new SqlParameter("user", nazwar));
                    command.ExecuteNonQuery();
                    SqlDataReader czytaj = command.ExecuteReader();
                    foreach (var item in czytaj)
                    {
                        LinkBaza.Obrazek = czytaj.GetString(0);
                       // Opis1.Text = czytaj.GetString(0);
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
                conn.Open();
                try
                {
                    string Output = "";
                    string commandText2 = "select count(*) as cnt from Ulubione WHERE numerkart=@user AND idryby LIKE @pass AND Nazwaryby LIKE @nazwa";
                    SqlCommand command2 = new SqlCommand(commandText2, conn);
                    command2.Parameters.Add(new SqlParameter("user", numerkart));
                    command2.Parameters.Add(new SqlParameter("pass", indeks));
                    command2.Parameters.Add(new SqlParameter("nazwa", nazwaryby));
                    command2.ExecuteNonQuery();
                    SqlDataReader czytaj = command2.ExecuteReader();
                    while (czytaj.Read())
                    {
                        Output = Output + czytaj.GetValue(0);
                    }
                    int test;
                    test = Int32.Parse(Output);
                    if (test == 1)
                    {
                        string info3 = "Dodałeś już tą rybę do ulubionych.";
                        Toast.MakeText(this, info3, ToastLength.Long).Show();
                        conn.Close();
                    }
                    else
                    {
                        string commandText = "insert into Ulubione (idryby,Nazwaryby,numerkart,obrazek,opis) values(@tel,@user,@pass,@imie,@nazwisko)";
                        SqlCommand command = new SqlCommand(commandText, conn);
                        command.Parameters.Add(new SqlParameter("user", nazwaryby));
                        command.Parameters.Add(new SqlParameter("pass", numerkart));
                        command.Parameters.Add(new SqlParameter("imie", obrazek));
                        command.Parameters.Add(new SqlParameter("nazwisko", opis));
                        command.Parameters.Add(new SqlParameter("tel", indeks));
                        command.ExecuteNonQuery();
                        string info = "Dodano do ulubionych.";
                        Toast.MakeText(this, info, ToastLength.Long).Show();
                        conn.Close();
                    }

                }
                catch
                {
                    string commandText = "insert into Ulubione (idryby,Nazwaryby,numerkart,obrazek,opis) values(@tel,@user,@pass,@imie,@nazwisko)";
                    SqlCommand command = new SqlCommand(commandText, conn);
                    command.Parameters.Add(new SqlParameter("user", nazwaryby));
                    command.Parameters.Add(new SqlParameter("pass", numerkart));
                    command.Parameters.Add(new SqlParameter("imie", obrazek));
                    command.Parameters.Add(new SqlParameter("nazwisko", opis));
                    command.Parameters.Add(new SqlParameter("tel", indeks));
                    command.ExecuteNonQuery();
                    string info2 = "Dodano do ulubionych.";
                    Toast.MakeText(this, info2, ToastLength.Long).Show();
                }
                finally
                {
                    conn.Close();
                }

            }
        }
    }
}