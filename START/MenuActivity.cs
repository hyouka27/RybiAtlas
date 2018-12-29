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
        private Button btnwyloguj;


        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.activity_menu);

            btnkonto.Click += delegate {
                var konto = new Intent(this, typeof(KontoActivity));
                StartActivity(konto);
            };

            btnokregi.Click += delegate {
                var okregi = new Intent(this, typeof(OkregiActivity));
                StartActivity(okregi);
            };

            btnallokregi.Click += delegate {
                var allokregi = new Intent(this, typeof(AllokregiActivity));
                StartActivity(allokregi);
            };

            btnregul.Click += delegate {
                var regul = new Intent(this, typeof(RegulActivity));
                StartActivity(regul);
            };

            btnryby.Click += delegate {
                var ryby = new Intent(this, typeof(RybyActivity));
                StartActivity(ryby);
            };

            btnulub.Click += delegate {
                var ulub = new Intent(this, typeof(UlubActivity));
                StartActivity(ulub);
            };

            btnwyloguj.Click += delegate {
                var wyloguj = new Intent(this, typeof(WylogujActivity));
                StartActivity(wyloguj);
            };


        }

    }
}