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
using Android.Support.V7.Widget;
using Android.Views;
using Android.Widget;

namespace START
{
    [Activity(Label = "@string/dodane", Theme = "@style/AppTheme")]
    public class UlubActivity : AppCompatActivity
    {
        private List<string> ulub;
        private ListView listaV;

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.activity_ulub);
            listaV = FindViewById<ListView>(START.Resource.Id.listaV);
            ulub = new List<string>();
            Uowsika(LinkBaza.numer);
            ArrayAdapter<string> adapter = new ArrayAdapter<string>(this, Android.Resource.Layout.SimpleListItem1, ulub);
            listaV.Adapter = adapter;
            listaV.ItemClick += ListaVClick;
        }

        void Uowsika(int numerkart)
        {
            using (SqlConnection conn = new SqlConnection(LinkBaza.connString))
            {
                conn.Open();
                try
                {
                    string commandText = "SELECT Nazwaryby FROM Ulubione WHERE numerkart LIKE @test";
                    SqlCommand command = new SqlCommand(commandText, conn);
                    command.Parameters.Add(new SqlParameter("test", numerkart));
                    command.ExecuteNonQuery();
                    SqlDataReader czytaj = command.ExecuteReader();
                    foreach (var item in czytaj)
                    {
                        int i = 0;
                        ulub.Add(czytaj.GetString(i));
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
        private void ListaVClick(object sender, AdapterView.ItemClickEventArgs e)
        {
            var ulub2 = new Intent(this, typeof(Wybranarybaulubione));
            StartActivity(ulub2);
            string ulubiona = ulub[e.Position];
            LinkBaza.Nazwa2 = ulubiona;
            Toast.MakeText(this, ulubiona, ToastLength.Long).Show();
        }
    }
 

}