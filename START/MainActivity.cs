using Android.App;
using Android.OS;
using Android.Support.V7.App;
using Android.Runtime;
using Android.Widget;
using System.Data;
using System;
using System.Data.SqlClient;

namespace START
{
    [Activity(Label = "@string/app_name", Theme = "@style/AppTheme", MainLauncher = true)]
    public class MainActivity : AppCompatActivity
    {
        private EditText etusername;
        private EditText etpass;
        private Button btninsert;
        private TextView tvTips;
        private Button btnrejestracja;

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.activity_main);
            etusername = FindViewById<EditText>(Resource.Id.etusername);
            etpass = FindViewById<EditText>(Resource.Id.etPass);
            btninsert = FindViewById<Button>(Resource.Id.btninsert);
            btnrejestracja = FindViewById<Button>(Resource.Id.btnrejestracja);
            tvTips = FindViewById<TextView>(Resource.Id.tvTips);
            btninsert.Click += Btninsert_Click;
            btnrejestracja.Click += Btnrejestracja_Click;
        }

        private void Btninsert_Click(object sender, System.EventArgs e)
        {
            string numerkart1 = etusername.Text;
            int numerkart;
            if (int.TryParse(numerkart1, out numerkart))
            {
                numerkart = Int32.Parse(etusername.Text);
            }
            string pass2 = etpass.Text;
            InsertInfo(numerkart, pass2);
        }

        void InsertInfo(int numerkart, string pass2)
        {
            using (SqlConnection conn = new SqlConnection(LinkBaza.connString))
            {
                conn.Open();
                try
                {
                    string commandText = "insert into userI(numerkarty,haslo) values(@user,@pass)";
                    SqlCommand command = new SqlCommand(commandText, conn);
                    command.Parameters.Add(new SqlParameter("user", numerkart));
                    command.Parameters.Add(new SqlParameter("pass", pass2));
                    command.ExecuteNonQuery();
                    tvTips.Text = "Zalogowano pomyślnie";
                    conn.Close();
                }
                catch
                {
                    tvTips.Text = "Nie możesz się zalogować, popraw dane.";
                    //using (SqlDataReader reader = cmd.ExecuteReader())
                    //{
                    //    //while (reader.Read())
                    //    //{
                    //    //    Console.WriteLine("ID: [{0}], Name: [{1}]", reader.GetValue(0), reader.GetValue(1));
                    //    //}
                    //}
                }
                finally
                {
                    conn.Close();
                }
            }
        }
        private void Btnrejestracja_Click(object sender, System.EventArgs e)
        {
        SetContentView(Resource.Layout.activity_rejestracja);

        }
    }
}

