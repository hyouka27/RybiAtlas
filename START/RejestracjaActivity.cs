using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Support.V7.App;
using Android.Views;
using Android.Widget;
using System;
using System.Data;
using System.Data.SqlClient;

namespace START
{
    [Activity(Label = "@string/app_name", Theme = "@style/AppTheme")]
    public class RejestracjaActivity : AppCompatActivity
    {
        private EditText nrkarty;
        private EditText pass;
        private TextView info;
        private Button btnrejinsert;

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.activity_rejestracja);
            nrkarty = FindViewById<EditText>(Resource.Id.nrkarty);
            pass = FindViewById<EditText>(Resource.Id.Pass);
            btnrejinsert = FindViewById<Button>(Resource.Id.btnrejinsert);
            info = FindViewById<TextView>(Resource.Id.info);
            btnrejinsert.Click += Btnrejinsert_Click;

        }


        private void Btnrejinsert_Click(object sender, System.EventArgs e)
        {
            string numerkart1 = nrkarty.Text;
            int numerkart;
            if (int.TryParse(numerkart1, out numerkart))
            {
                numerkart = Int32.Parse(nrkarty.Text);
            }
            string pass2 = pass.Text;
            InsertInfo2(numerkart, pass2);
        }
         void InsertInfo2(int numerkart, string pass2)
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
                    conn.Close();
                    SetContentView(Resource.Layout.activity_konto);
                }
                catch
                {
                    info.Text = "Błędny numer karty wędkarskiej";
                }
                finally
                {
                    conn.Close();
                }
            }
        }


    }
}