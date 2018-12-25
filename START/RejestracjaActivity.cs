using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Support.V7.App;
using Android.Views;
using Android.Widget;
using System;
using System.Data;

namespace START
{
    [Activity(Label = "@string/app_name", Theme = "@style/AppTheme")]
   public class RejestracjaActivity :AppCompatActivity
    {

        private EditText nrkarty;
        private EditText pass;
        private Button btnrejinsert;
        private Button button1;
       

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.activity_rejestracja);

            nrkarty = FindViewById<EditText>(Resource.Id.nrkarty);
            pass = FindViewById<EditText>(Resource.Id.Pass);
            btnrejinsert = FindViewById<Button>(Resource.Id.btnrejinsert);
            button1 = FindViewById<Button>(Resource.Id.button1);

            btnrejinsert.Click += Btnrejinsert_Click;
            button1.Click += Button1_Click;
          
        }

 
        private void Btnrejinsert_Click(object sender, System.EventArgs e)
        {
        //tutaj dodaj wysylanie danych do bazy
            SetContentView(Resource.Layout.activity_okregi);
        }

        private void Button1_Click(object sender, System.EventArgs e)
        {
            SetContentView(Resource.Layout.activity_okregi);
        }

      
    }
}