using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Support.V7.App;
using Android.Views;
using Android.Widget;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace START
{
    [Activity(Label = "@string/app_name", Theme = "@style/AppTheme")]
    public class RybyActivity : AppCompatActivity
    {
        private TextView NazwaRyby1;
        //private TextView Indeks1;
        //private TextView UrlObrazka1;
        private TextView Opis1;
        public string Nazwaryby { get; set; }
        public string Opis { get; set; }
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.activity_ryby);
            NazwaRyby1 = FindViewById<TextView>(Resource.Id.NazwaRyby1);
            //Indeks1 = FindViewById<TextView>(Resource.Id.Indeks1);
            //UrlObrazka1 = FindViewById<TextView>(Resource.Id.UrlObrazka1);
            Opis1 = FindViewById<TextView>(Resource.Id.Opis1);
            using (var webClient = new System.Net.WebClient())
            {
                var json = webClient.DownloadString("http://hyouka27.usermd.net/WENDKA/ryby.json");
            }
            
        } 
    }
}
