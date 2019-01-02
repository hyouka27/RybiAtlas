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
using Android.Support.V7.Widget;
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
        private TextView Indeks1;
        private TextView UrlObrazka1;
        private TextView Opis1;
        private RecyclerView listaryb;
        public string Nazwaryby { get; set; }
        public string Opis { get; set; }
        public string Indeks { get; set; }
        public string Url_Obrazka { get; set; }
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.activity_ryby);
            NazwaRyby1 = FindViewById<TextView>(Resource.Id.NazwaRyby1);
            Indeks1 = FindViewById<TextView>(Resource.Id.Indeks1);
            UrlObrazka1 = FindViewById<TextView>(Resource.Id.Obrazek);
            Opis1 = FindViewById<TextView>(Resource.Id.Opis1);
            listaryb = FindViewById<RecyclerView>(Resource.Id.listaryb);

            using (var webClient = new System.Net.WebClient())
            {
                var json = webClient.DownloadString("http://hyouka27.usermd.net/WENDKA/ryby.json");
                JObject rybki = JObject.Parse(json);
                NazwaRyby1.Text = (string)rybki["ryby"][0]["NazwaRyby"];
                Opis1.Text = (string)rybki["ryby"][0]["Opis"];
                Indeks1.Text = (string)rybki["ryby"][0]["Indeks"];
                UrlObrazka1.Text = (string)rybki["ryby"][0]["Url_Obrazka"];
            }
            
        } 
    }
}
