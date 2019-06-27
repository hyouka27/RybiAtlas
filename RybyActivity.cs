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



namespace RybiAtlas
{
    [Activity(Label = "@string/ryby", Theme = "@style/AppTheme")]
    public class RybyActivity : AppCompatActivity
    {
        /// <summary>
        /// Zmienne
        /// </summary>
        private List<string> listaryblista;
        private ListView listaryb;
        public string Nazwaryby { get; set; }
        public string Opis { get; set; }
        public string Indeks { get; set; }
        public string Url_Obrazka { get; set; }
  
    
    protected override void OnCreate(Bundle savedInstanceState)
        {
            /// <summary>
            /// Akcja po kliknięciu w przycisk, dodaje rybe do listy. 
            /// </summary>
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.activity_ryby);
            listaryb = FindViewById<ListView>(RybiAtlas.Resource.Id.listaryb);
            listaryblista = new List<string>();
            Pokaryba();
            ArrayAdapter<string> adapter = new ArrayAdapter<string>(this, Android.Resource.Layout.SimpleListItem1, listaryblista);
            listaryb.Adapter = adapter;
            listaryb.ItemClick += ListaVClick;

        }

        /// <summary>
        /// Wyciąga z jsona nazwę ryby i dodaje do listy.
        /// </summary>
        public void Pokaryba()
        {
           
        }

        /// <summary>
        /// Akcja po kliknięciu w przycisk, włącza widok WybranarybaActivity.
        /// </summary>
        private void ListaVClick(object sender, AdapterView.ItemClickEventArgs e)
        {

            var rybylista = new Intent(this, typeof(WybranarybaActivity));
            StartActivity(rybylista);
            string okregall1 = listaryblista[e.Position];
            LinkBaza.Nazwa = okregall1;
            Toast.MakeText(this, okregall1, ToastLength.Long).Show();
        }
    }
}
