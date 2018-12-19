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

        public string connString { get; private set; }

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
            //InsertInfo(numerkart, pass2);
            Start();
           

        }

        public void Start()
        {
            string connString = @"workstation id=testowa.mssql.somee.com;packet size=4096;user id=hyouka27_SQLLogin_1;pwd=1234567*;data source=testowa.mssql.somee.com;persist security info=False;initial catalog=testowa";
           
            using (SqlConnection conn = new SqlConnection(connString))
            {
                conn.Open();
                using (SqlCommand cmd = new SqlCommand("INSERT INTO Persons(PersonID, LastName) VALUES(88,'TOMEK')", conn))
                {
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        //while (reader.Read())
                        //{
                        //    Console.WriteLine("ID: [{0}], Name: [{1}]", reader.GetValue(0), reader.GetValue(1));
                        //}
                    }
                }
                conn.Close();
            }
           
        }
        //void InsertInfo(int userPar, int passPar)
        //{
        //    string connStr = "server=mysql32.mydevil.net;port=3306;database=m11808_baz;user=m11808_wed;password=Xamarin1@#";
        //    MySqlConnection con = new MySqlConnection(connStr);

           
        //    try
        //    {
        //        if (con.State == ConnectionState.Closed) {
        //            con.Open();
        //            tvTips.Text = "Połączono pomyślnie";
        //            MySqlCommand cmd = new MySqlCommand("insert into User(numerkarty,haslo) values(@user,@pass)", con);
        //            cmd.Parameters.AddWithValue("@user", userPar);
        //            cmd.Parameters.AddWithValue("@pass", passPar);
        //            cmd.ExecuteNonQuery();
        //            tvTips.Text = "Zalogowano pomyślnie";
        //        }
        //    }
        //    catch (MySqlException ex)
        //    {
        //        throw ex;
        //    }
        //    finally
        //    {
        //        con.Close();
        //    }
            
                
            
        }

}

