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
    [Activity(Label = "@string/wybranyokreg", Theme = "@style/AppTheme")]
    public class WybranyokregActivity : AppCompatActivity
    {
        /// <summary>
        /// Zmienne.
        /// </summary>
        private Button btnlowiska;
        private Button btnregulokreg;
        private Button dodajoplacone;
        private TextView getnazwaokregu;
        private TextView getskladka;

        protected override void OnCreate(Bundle savedInstanceState)
        {
            /// <summary>
            /// Akcja po kliknięciu w przycisk, dodaje rybe do listy. 
            /// </summary>
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.activity_wybranyokreg);
            btnlowiska = FindViewById<Button>(Resource.Id.btnlowiska);
            dodajoplacone = FindViewById<Button>(Resource.Id.dodajoplacone);
            btnlowiska.Click += delegate {
                var lowiska = new Intent(this, typeof(ListalowiskActivity));
                StartActivity(lowiska);
            };
            btnregulokreg = FindViewById<Button>(Resource.Id.btnregulokreg);
            btnregulokreg.Click += delegate {
                var rokreg = new Intent(this, typeof(RegulokregActivity));
                StartActivity(rokreg);
            };
            getnazwaokregu = FindViewById<TextView>(Resource.Id.getnazwaokregu);
            getskladka = FindViewById<TextView>(Resource.Id.getskladka);
            Podajnazweokregu(LinkBaza.okregbaza);
            Podajskladke(LinkBaza.okregbaza);
            dodajoplacone.Click += Dodajoplacone_Click;
        }

        /// <summary>
        /// Odczytuje nazwę okręgu z bazy.
        /// </summary>
        void Podajnazweokregu(string numerkart)
        {
            using (SqlConnection conn = new SqlConnection(LinkBaza.connString))
            {
                conn.Open();
                try
                {
                    string commandText = "SELECT nazwaokregu FROM okregi WHERE nazwaokregu LIKE @user";
                    SqlCommand command = new SqlCommand(commandText, conn);
                    command.Parameters.Add(new SqlParameter("user", numerkart));
                    command.ExecuteNonQuery();
                    SqlDataReader czytaj = command.ExecuteReader();
                    while (czytaj.Read())
                    {
                        getnazwaokregu.Text = czytaj.GetString(0);
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
        /// Odczytuje kwotę składki z bazy.
        /// </summary>
        void Podajskladke(string numerkart)
        {
            using (SqlConnection conn = new SqlConnection(LinkBaza.connString))
            {
                conn.Open();
                try
                {
                    string commandText = "SELECT skladka FROM okregi WHERE nazwaokregu LIKE @user";
                    SqlCommand command = new SqlCommand(commandText, conn);
                    command.Parameters.Add(new SqlParameter("user", numerkart));
                    command.ExecuteNonQuery();
                    SqlDataReader czytaj = command.ExecuteReader();
                    while (czytaj.Read())
                    {
                        getskladka.Text = czytaj.GetInt32(0).ToString();
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
        /// Akcja po kliknięciu w przycisk, łąduje widok listalowisk.
        /// </summary>
        private void btnlowiska_Click(object sender, System.EventArgs e)
        {

            SetContentView(Resource.Layout.activity_listalowisk);
        }

        /// <summary>
        /// Akcja po kliknięciu w przycisk, Dodaje opłacone okregi do listy.
        /// </summary>
        private void Dodajoplacone_Click(object sender, System.EventArgs e)
        {

            Podajid(LinkBaza.okregbaza);
            InsertInfo2(LinkBaza.licznik, LinkBaza.numer);
        }
        /// <summary>
        /// Akcja po kliknięciu w przycisk, włącza regulamin okręgu.
        /// </summary>
        private void btnregulokreg_Click(object sender, System.EventArgs e)
        {
            SetContentView(Resource.Layout.activity_regulokreg);
        }
        /// <summary>
        /// Podaje id okregu - używane w innych zapytaniach. 
        /// </summary>
        void Podajid(string numerkart)
        {
            using (SqlConnection conn = new SqlConnection(LinkBaza.connString))
            {
                conn.Open();
                try
                {
                    string commandText = "SELECT idokregu FROM okregi WHERE nazwaokregu LIKE @user";
                    SqlCommand command = new SqlCommand(commandText, conn);
                    command.Parameters.Add(new SqlParameter("user", numerkart));
                    command.ExecuteNonQuery();
                    SqlDataReader czytaj = command.ExecuteReader();
                    while (czytaj.Read())
                    {
                        LinkBaza.licznik = czytaj.GetInt32(0);
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
        /// Sprawdza czy nie ma dubla, czyli dwa razy opłąconego okręgu, i pozwala dodawać okręg tylko wtedy gdy nie jest opłacony.
        /// </summary>
        void InsertInfo2(int mail,int numer)
        {
            using (SqlConnection conn = new SqlConnection(LinkBaza.connString))
            {
                try
                {
                    conn.Open();
                    string Output = "";
                    string commandText = "select count(idokregu) as cnt from oplacone WHERE numerkarty=@user AND idokregu LIKE @pass AND oplacone LIKE 'tak'";
                    SqlCommand command = new SqlCommand(commandText, conn);
                    command.Parameters.Add(new SqlParameter("user", numer));
                    command.Parameters.Add(new SqlParameter("pass", mail));
                    command.ExecuteNonQuery();
                    SqlDataReader czytaj = command.ExecuteReader();
                    while (czytaj.Read())
                    {
                        Output = Output + czytaj.GetValue(0);
                    }
                    int test;
                    test = Int32.Parse(Output);
                    if (test>=1)
                    {
                        string info = "Wybrany okręg jest już opłacony.";
                        Toast.MakeText(this, info, ToastLength.Long).Show();
                        var menu = new Intent(this, typeof(MenuActivity));
                        StartActivity(menu);
                    }
                    else
                    {
                        conn.Open();
                        string commandText2 = "insert into oplacone(numerkarty,idokregu,oplacone) values(@user,@mail,'tak')";
                        SqlCommand command2 = new SqlCommand(commandText2, conn);
                        command2.Parameters.Add(new SqlParameter("user", numer));
                        command2.Parameters.Add(new SqlParameter("mail", mail));
                        command2.ExecuteNonQuery();
                        conn.Close();
                        var menu = new Intent(this, typeof(MenuActivity));
                        StartActivity(menu);
                    }
                }
                catch
                {
                    conn.Close();
                    conn.Open();
                    string commandText2 = "insert into oplacone(numerkarty,idokregu,oplacone) values(@user,@mail,'tak')";
                        SqlCommand command2 = new SqlCommand(commandText2, conn);
                        command2.Parameters.Add(new SqlParameter("user", numer));
                        command2.Parameters.Add(new SqlParameter("mail", mail));
                        command2.ExecuteNonQuery();
                        conn.Close();
                        var menu = new Intent(this, typeof(MenuActivity));
                        StartActivity(menu);
                }
                finally
                {
                    conn.Close();
                }
            }
        }
    }
}