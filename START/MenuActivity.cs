using System;
using System.Collections.Generic;
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
    public class MenuActivity :AppCompatActivity
    {
        private Button btnkonto;
        private Button btnokregi;
        private Button btnallokregi;
        private Button btnregul;
        private Button btnryby;
        private Button btnulub;

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.activity_menu);
            //przypisanie btnkonto ze zmiennej u gory linia19
            btnkonto = FindViewById<Button>(Resource.Id.btnkonto);
            //od 33 do 36 przelaczanie ekranu plus 39 do 42
            btnkonto.Click += delegate {
                var konto = new Intent(this, typeof(KontoActivity));
                StartActivity(konto);
            };
            btnokregi = FindViewById<Button>(Resource.Id.btnokregi);
            //od 33 do 36 przelaczanie ekranu plus 39 do 42
            btnokregi.Click += delegate {
                var okregi = new Intent(this, typeof(OkregiActivity));
                StartActivity(okregi);
            };
            btnallokregi = FindViewById<Button>(Resource.Id.btnallokregi);
            //od 33 do 36 przelaczanie ekranu plus 39 do 42
            btnallokregi.Click += delegate {
                var allokregi = new Intent(this, typeof(AllokregiActivity));
                StartActivity(allokregi);
            };
            btnregul = FindViewById<Button>(Resource.Id.btnregul);
            //od 33 do 36 przelaczanie ekranu plus 39 do 42
            btnregul.Click += delegate {
                var regul = new Intent(this, typeof(RegulActivity));
                StartActivity(regul);
            };
            btnryby = FindViewById<Button>(Resource.Id.btnryby);
            //od 33 do 36 przelaczanie ekranu plus 39 do 42
            btnryby.Click += delegate {
                var ryby = new Intent(this, typeof(RybyActivity));
                StartActivity(ryby);
            };
            btnulub = FindViewById<Button>(Resource.Id.btnulub);
            //od 33 do 36 przelaczanie ekranu plus 39 do 42
            btnulub.Click += delegate {
                var ulub = new Intent(this, typeof(UlubActivity));
                StartActivity(ulub);
            };
        }
        //metoda poza oncreate
        private void btnkonto_Click(object sender, System.EventArgs e)
        {
            SetContentView(Resource.Layout.activity_konto);
        }
        private void btnokregi_Click(object sender, System.EventArgs e)
        {
            SetContentView(Resource.Layout.activity_okregi);
        }
        private void btnallokregi_Click(object sender, System.EventArgs e)
        {
            SetContentView(Resource.Layout.activity_allokregi);
        }
        private void btnregul_Click(object sender, System.EventArgs e)
        {
            SetContentView(Resource.Layout.activity_regul);
        }
        private void btnryby_Click(object sender, System.EventArgs e)
        {
            SetContentView(Resource.Layout.activity_ryby);
        }
        private void btnkonto_ulub(object sender, System.EventArgs e)
        {
            SetContentView(Resource.Layout.activity_ulub);
        }
    }
}