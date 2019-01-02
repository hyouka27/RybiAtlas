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
using Newtonsoft.Json.Linq;

namespace START
{
    [Activity(Label = "@string/app_name", Theme = "@style/AppTheme")]
    public class WybranarybaActivity : AppCompatActivity
    {
        private TextView NazwaRyby1;
        private TextView Indeks1;
        private Uri Obrazek;
        private TextView Opis1;
        private Button Dodaj;

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.activity_wybranaryba);
            NazwaRyby1 = FindViewById<TextView>(Resource.Id.NazwaRyby1);
            Indeks1 = FindViewById<TextView>(Resource.Id.Indeks1);
            //Obrazek = FindViewById<Uri>(Resource.Id.Obrazek);
            Opis1 = FindViewById<TextView>(Resource.Id.Opis1);
            Podajnazwe(LinkBaza.Nazwa);
            PodajOpis(LinkBaza.Opis);
            //Pokaobraz();

        }

        void Podajnazwe(string nazwar)
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
                    if (item == LinkBaza.Nazwa)
                    {
                        NazwaRyby1.Text = nazwar;
                    }
                }
            }
        }

        void PodajOpis(string nazwar)
        {
            Opis1.Text = nazwar;
        }

        //void Pokaobraz()
        //{
        //    using (var webClient = new System.Net.WebClient())
        //    {
        //        List<string> listaryblista2 = new List<string>();
        //        var json = webClient.DownloadString("http://hyouka27.usermd.net/WENDKA/ryby.json");
        //        JObject rybki = JObject.Parse(json);
        //        //Opis1.Text = (string)rybki["ryby"][0]["Opis"];
        //        //Indeks1.Text = (string)rybki["ryby"][0]["Indeks"];
        //        //UrlObrazka1.Text = (string)rybki["ryby"][0]["Url_Obrazka"];
        //        var Opis = from a in rybki["ryby"] /*where /*p.Contains(LinkBaza.Nazwa)*/ select (string)a["Opis"];

        //        foreach (var item in Opis)
        //        {
        //            listaryblista2.Add(item);
        //            if (LinkBaza.licznik == listaryblista2.Count())
        //            {
        //                Toast.MakeText(this, item, ToastLength.Long).Show();
        //                Opis1.Text = item;
        //            }
        //        }
        //    }
        //}
    }
}