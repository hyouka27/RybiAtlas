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
    [Activity(Label = "@string/app_name", Theme = "@style/AppTheme", MainLauncher = true)]
    public class RybyActivity : AppCompatActivity
    {
        private List<string> listaryblista;
        private ListView listaryb;
        public string Nazwaryby { get; set; }
        public string Opis { get; set; }
        public string Indeks { get; set; }
        public string Url_Obrazka { get; set; }
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.activity_ryby);
            listaryb = FindViewById<ListView>(START.Resource.Id.listaryb);
            listaryblista = new List<string>();
            Pokaryba();
            ArrayAdapter<string> adapter = new ArrayAdapter<string>(this, Android.Resource.Layout.SimpleListItem1, listaryblista);
            listaryb.Adapter = adapter;
            listaryb.ItemClick += ListaVClick;
            //NazwaRyby1 = FindViewById<TextView>(Resource.Id.NazwaRyby1);
            //Indeks1 = FindViewById<TextView>(Resource.Id.Indeks1);
            //UrlObrazka1 = FindViewById<TextView>(Resource.Id.Obrazek);
            //Opis1 = FindViewById<TextView>(Resource.Id.Opis1);
        }
   
       
        public void Pokaryba()
        {
            using (var webClient = new System.Net.WebClient())
            {
                var json = webClient.DownloadString("http://hyouka27.usermd.net/WENDKA/ryby.json");
                JObject rybki = JObject.Parse(json);
                //Opis1.Text = (string)rybki["ryby"][0]["Opis"];
                //Indeks1.Text = (string)rybki["ryby"][0]["Indeks"];
                //UrlObrazka1.Text = (string)rybki["ryby"][0]["Url_Obrazka"];
                var NazwaRyby = from p in rybki["ryby"] select (string)p["NazwaRyby"];
                foreach (var item in NazwaRyby)
                {
                    listaryblista.Add(item);
                }
            }
        }
        
        private void ListaVClick(object sender, AdapterView.ItemClickEventArgs e)
        {

            var rybylista = new Intent(this, typeof(WybranarybaActivity));
            StartActivity(rybylista);
            string okregall1 = listaryblista[e.Position];
            LinkBaza.Nazwa = okregall1;
            Toast.MakeText(this, okregall1, ToastLength.Long).Show();
            using (var webClient = new System.Net.WebClient())
            {
                var json = webClient.DownloadString("http://hyouka27.usermd.net/WENDKA/ryby.json");
                JObject rybki = JObject.Parse(json);
                //LinkBaza.Opis = (string)rybki["ryby"][0]["Opis"];
                //Indeks1.Text = (string)rybki["ryby"][0]["Indeks"];
                //UrlObrazka1.Text = (string)rybki["ryby"][0]["Url_Obrazka"];
                List<string> test = new List<string>();
                var Opis = from p in rybki["ryby"] where p.ToString()==okregall1 select (string)p["Opis"];
                foreach (var item in Opis)
                {
                    test.Add(item);
                }
                //string okregaOpis = listaryblista[e.Position];
                //LinkBaza.Opis = okregaOpis;
            }
        }
    }
}
