using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Support.V7.App;
using Android.Views;
using Android.Widget;
using Plugin.Permissions;
using Plugin.Permissions.Abstractions;

namespace START
{
    [Activity(Label = "@string/menu", Theme = "@style/AppTheme")]
    public class MenuActivity :AppCompatActivity
    {
        /// <summary>
        /// Zmienne
        /// </summary>
        private Button btnkonto;
        private Button btnokregi;
        private Button btnallokregi;
        private Button btnregul;
        private Button btnryby;
        private Button btnulub;
        private Button btszuka;
        private Button btaparat;

        protected override void OnCreate(Bundle savedInstanceState)
        {
            /// <summary>
            /// Zawiera opisy elementów przypisane do gui jak i metody.
            /// </summary>
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.activity_menu);
            btnkonto = FindViewById<Button>(Resource.Id.btnkonto);
            btnkonto.Click += delegate {
                var konto = new Intent(this, typeof(KontoActivity));
                StartActivity(konto);
            };
            btnokregi = FindViewById<Button>(Resource.Id.btnokregi);
            btnokregi.Click += delegate {
                var okregi = new Intent(this, typeof(OkregiActivity));
                StartActivity(okregi);
            };
            btnallokregi = FindViewById<Button>(Resource.Id.btnallokregi);
            btnallokregi.Click += delegate {
                var allokregi = new Intent(this, typeof(AllokregiActivity));
                StartActivity(allokregi);
            };
            btnregul = FindViewById<Button>(Resource.Id.btnregul);
            btnregul.Click += delegate {
                var regul = new Intent(this, typeof(RegulActivity));
                StartActivity(regul);
            };
            btnryby = FindViewById<Button>(Resource.Id.btnryby);
            btnryby.Click += delegate {
                var ryby = new Intent(this, typeof(RybyActivity));
                StartActivity(ryby);
            };
            btnulub = FindViewById<Button>(Resource.Id.btnulub);
            btnulub.Click += delegate {
                var ulub = new Intent(this, typeof(UlubActivity));
                StartActivity(ulub);
            };
            btszuka = FindViewById<Button>(Resource.Id.btszuka);
            btszuka.Click += delegate {
                var szuka = new Intent(this, typeof(SZUKAJ));
                StartActivity(szuka);
            };
            btaparat = FindViewById<Button>(Resource.Id.btaparat);
            btaparat.Click += delegate
            {
                Intent intent = new Intent("android.media.action.IMAGE_CAPTURE");
                StartActivity(intent);
            };
            GetPermissions();
        }

        /// <summary>
        /// Metody odpowiedzialne za działanie przycisków w menu, przekierowują do wybranych aktywnośći.
        /// </summary>
        private void btnkonto_Click(object sender, System.EventArgs e)
        {
            SetContentView(Resource.Layout.activity_konto);
        }
        private void btnokregi_Click(object sender, System.EventArgs e)
        {
            SetContentView(Resource.Layout.activity_okregi);
        }
        private void btnallokregi_Click(object sender, System.EventArgs e)
        {
            SetContentView(Resource.Layout.activity_allokregi);
        }
        private void btnregul_Click(object sender, System.EventArgs e)
        {
            SetContentView(Resource.Layout.activity_regul);
        }
        private void btnryby_Click(object sender, System.EventArgs e)
        {
            SetContentView(Resource.Layout.activity_ryby);
        }
        private void btnkonto_ulub(object sender, System.EventArgs e)
        {
            SetContentView(Resource.Layout.activity_ulub);
        }
        private void btnszuka(object sender, System.EventArgs e)
        {
            SetContentView(Resource.Layout.szukaj);
        }
        /// <summary>
        /// Blokuje cofanie na strzałkach systemowych, tak by trzymało sesję. 
        /// </summary>
        public override void OnBackPressed()
        {
            return;
        }
        public override void OnRequestPermissionsResult(int requestCode, string[] permissions, [GeneratedEnum] Android.Content.PM.Permission[] grantResults)
        {
            PermissionsImplementation.Current.OnRequestPermissionsResult(requestCode, permissions, grantResults);
            base.OnRequestPermissionsResult(requestCode, permissions, grantResults);
        }
        private async void GetPermissions()
        {
            var status = await CrossPermissions.Current.CheckPermissionStatusAsync(Permission.Camera);
            if (status == PermissionStatus.Granted)
            {
                btaparat.Click += delegate
                {
                    Intent intent = new Intent("android.media.action.IMAGE_CAPTURE");
                    StartActivity(intent);
                };
            }
            else
            {
                string info = "Włącz uprawnienia do aparatu w ustawieniach systemowych";
                Toast.MakeText(this, info, ToastLength.Long).Show();
            }

        }
    }
}