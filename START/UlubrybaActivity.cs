﻿using System;
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
    public class UlubrybaActivity : AppCompatActivity
    {
        private Button btnmenu;


        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.activity_ulubryba);


            btnmenu.Click += delegate
            {
                var menu = new Intent(this, typeof(MenuActivity));
                StartActivity(menu);
            };
        }
    }
}
