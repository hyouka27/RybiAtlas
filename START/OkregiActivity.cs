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
    public class OkregiActivity :AppCompatActivity
    {
        private Button btnmenu;
        private ListView lista1;

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.activity_okregi);

            
            btnmenu = FindViewById<Button>(Resource.Id.btnmenu);

            btnmenu.Click += Btnmenu_Click;

        }

        private void Btnmenu_Click(object sender, System.EventArgs e)
        {

            SetContentView(Resource.Layout.activity_main);
        }
    }
}