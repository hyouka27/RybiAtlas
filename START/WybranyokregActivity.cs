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

namespace START
{
    [Activity(Label = "@string/wybranyokreg", Theme = "@style/AppTheme")]
    public class WybranyokregActivity : AppCompatActivity
    {
     
        private Button btnlowiska;
        private Button btnregulokreg;
        private TextView getnazwaokregu;
        private TextView getskladka;


        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.activity_wybranyokreg);
            btnlowiska = FindViewById<Button>(Resource.Id.btnlowiska);
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

        }
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

        private void btnlowiska_Click(object sender, System.EventArgs e)
        {

            SetContentView(Resource.Layout.activity_listalowisk);
        }
        private void btnregulokreg_Click(object sender, System.EventArgs e)
        {
            SetContentView(Resource.Layout.activity_regulokreg);
        }

    }
}
