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
            btnkonto = FindViewById<Button>(Resource.Id.btnkonto);
            btnkonto.Click += delegate {
                var konto = new Intent(this, typeof(KontoActivity));
                StartActivity(konto);
            };
        }

        private void btnkonto_Click(object sender, System.EventArgs e)
        {
            SetContentView(Resource.Layout.activity_konto);
        }
    }
}