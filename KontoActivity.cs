﻿using System;
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

namespace RybiAtlas
{
    [Activity(Label = "@string/konto", Theme = "@style/AppTheme")]
#pragma warning disable CS1591 // Brak komentarza XML dla widocznego publicznie typu lub składowej
    public class KontoActivity : AppCompatActivity
#pragma warning restore CS1591 // Brak komentarza XML dla widocznego publicznie typu lub składowej
    {
        /// <summary>
        /// Zmienne
        /// </summary>
        private TextView imie;
        private TextView nazwisko;
        private TextView tel;
        private TextView mail;
#pragma warning disable CS1591 // Brak komentarza XML dla widocznego publicznie typu lub składowej
        protected override void OnCreate(Bundle savedInstanceState)
#pragma warning restore CS1591 // Brak komentarza XML dla widocznego publicznie typu lub składowej
        {
            /// <summary>
            /// Zawiera opisy elementów przypisane do gui jak i metody.
            /// </summary>
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.activity_konto);
            imie = FindViewById<TextView>(Resource.Id.getimie);
            nazwisko = FindViewById<TextView>(Resource.Id.getnazwisko);
            tel = FindViewById<TextView>(Resource.Id.gettel);
            mail = FindViewById<TextView>(Resource.Id.getmail);
            Podajimie(LinkBaza.numer);
            Podajnazwisko(LinkBaza.numer);
            Podajtel(LinkBaza.numer);
            Podajmail(LinkBaza.numer); 
        }

        /// <summary>
        /// Metoda która wyświetla nam z bazy imie użytkownika na bazie jego numerukarty jako punktu odniesienia.
        /// </summary>
        void Podajimie(int numerkart)
        {
            using (SqlConnection conn = new SqlConnection(LinkBaza.connString))
            {
                conn.Open();
                try
                {
                    string commandText = "select imie from userI WHERE numerkarty=@user";
                    SqlCommand command = new SqlCommand(commandText, conn);
                    command.Parameters.Add(new SqlParameter("user", numerkart));
                    command.ExecuteNonQuery();
                    SqlDataReader czytaj = command.ExecuteReader();
                    while (czytaj.Read())
                    {
                        imie.Text=czytaj.GetString(0); 
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
        /// Metoda wyświetla nam z bazy nazwisko użytkownika. 
        /// </summary>
        void Podajnazwisko(int numerkart)
        {
            using (SqlConnection conn = new SqlConnection(LinkBaza.connString))
            {
                conn.Open();
                try
                {
                    string commandText = "select nazwisko from userI WHERE numerkarty=@user";
                    SqlCommand command = new SqlCommand(commandText, conn);
                    command.Parameters.Add(new SqlParameter("user", numerkart));
                    command.ExecuteNonQuery();
                    SqlDataReader czytaj = command.ExecuteReader();
                    while (czytaj.Read())
                    {
                        nazwisko.Text = czytaj.GetString(0);
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
        /// Metoda wyświetla nam z bazy numer telefonu użytkownika.
        /// </summary>
        void Podajtel(int numerkart)
        {
            using (SqlConnection conn = new SqlConnection(LinkBaza.connString))
            {
                conn.Open();
                try
                {
                    string commandText = "select telefon from userI WHERE numerkarty=@user";
                    SqlCommand command = new SqlCommand(commandText, conn);
                    command.Parameters.Add(new SqlParameter("user", numerkart));
                    command.ExecuteNonQuery();
                    SqlDataReader czytaj = command.ExecuteReader();
                    while (czytaj.Read())
                    {
                        tel.Text = czytaj.GetInt32(0).ToString();
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
        /// Metoda wyświelta nam z bazy mail użytkownika.
        /// </summary>
        void Podajmail(int numerkart)
        {
            using (SqlConnection conn = new SqlConnection(LinkBaza.connString))
            {
                conn.Open();
                try
                {
                    string commandText = "select email from userI WHERE numerkarty=@user";
                    SqlCommand command = new SqlCommand(commandText, conn);
                    command.Parameters.Add(new SqlParameter("user", numerkart));
                    command.ExecuteNonQuery();
                    SqlDataReader czytaj = command.ExecuteReader();
                    while (czytaj.Read())
                    {
                        mail.Text = czytaj.GetString(0);
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