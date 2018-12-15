using Android.App;
using Android.OS;
using Android.Support.V7.App;
using Android.Runtime;
using Android.Widget;
using System.Data;
using MySql.Data;
using MySql.Data.MySqlClient;
using System;

namespace START
{
    [Activity(Label = "@string/app_name", Theme = "@style/AppTheme", MainLauncher = true)]
    public class MainActivity : AppCompatActivity
    {


        private EditText etusername;
        private EditText etpass;
        private Button btninsert;
        private TextView tvTips;
        

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.activity_main);

            etusername = FindViewById<EditText>(Resource.Id.etusername);
            etpass = FindViewById<EditText>(Resource.Id.etPass);
            btninsert = FindViewById<Button>(Resource.Id.btninsert);
            tvTips = FindViewById<TextView>(Resource.Id.tvTips);
            
            btninsert.Click += Btninsert_Click;
        }

        private void Btninsert_Click(object sender, System.EventArgs e)
        {
            int numerkart = Int32.Parse(etusername.Text);
            int pass2 = Int32.Parse(etpass.Text);
            InsertInfo(numerkart, pass2);
           

        }

        void InsertInfo(int userPar, int passPar)
        {
            string connStr = "";
            MySqlConnection conn = new MySqlConnection(connStr);
            try
            {
                conn.Open();
                tvTips.Text = "Połączono pomyślnie";
                MySqlCommand cmd = new MySqlCommand("insert into User(numerkarty,haslo) values(@user,@pass)", conn);
                cmd.Parameters.AddWithValue("@user", userPar);
                cmd.Parameters.AddWithValue("@pass", passPar);
                cmd.ExecuteNonQuery();
                tvTips.Text = "Zalogowano pomyślnie";
                
            }
            catch (Exception ex)
            {
                tvTips.Text = ex.ToString();
            }
            finally
            {
                conn.Close();
            }
            
                
            
        }

    }
}
