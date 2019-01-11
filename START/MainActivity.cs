using Android.App;
using Android.OS;
using Android.Support.V7.App;
using Android.Runtime;
using Android.Widget;
using System.Data;
using System;
using System.Data.SqlClient;
using Android.Content;

namespace START
{
    [Activity(Label = "@string/logowanie", Theme = "@style/AppTheme", MainLauncher = true)]
    public class MainActivity : AppCompatActivity
    {
        /// <summary>
        /// Zmienne
        /// </summary>
        /// 
        
        private EditText etusername;
        private EditText etpass;
        private Button btninsert;
        private TextView tvTips;
        private Button btnrejestracja;
        protected override void OnCreate(Bundle savedInstanceState)
        {
            /// <summary>
            /// Zawiera opisy elementów przypisane do gui jak i metody.
            /// </summary>
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.activity_main);
            etusername = FindViewById<EditText>(Resource.Id.etusername);
            etpass = FindViewById<EditText>(Resource.Id.etPass);
            btninsert = FindViewById<Button>(Resource.Id.btninsert);
            btnrejestracja = FindViewById<Button>(Resource.Id.btnrejestracja);
            tvTips = FindViewById<TextView>(Resource.Id.tvTips);
            btninsert.Click += Btninsert_Click;
            btnrejestracja.Click += delegate {
            var rejestracja = new Intent(this, typeof(RejestracjaActivity));
            StartActivity(rejestracja);
            };   
        }

        /// <summary>
        /// Zawiera działanie przycisku logowania, konwersję typu i sprawdzanie czy nie wpisano czasem liter zamiast cyfr do numerukarty.
        /// </summary>
        private void Btninsert_Click(object sender, System.EventArgs e)
        {
            string numerkart1 = etusername.Text;
            int numerkart;
            if (int.TryParse(numerkart1, out numerkart))
            {
                numerkart = Int32.Parse(etusername.Text);
            }
            string pass2 = etpass.Text;
            LinkBaza.numer = numerkart;
            InsertInfo(numerkart, pass2);
        }

        /// <summary>
        /// Metoda sprawdza czy użytkownik jest zalogowany, na bazie sumy zapytania, jeśli znajdzie login i hasło to daje wynik 1 i wtedy loguje do aplikacji.
        /// W przeciwnym wypadku daje informację że użytkownik podał błędne dane. 
        /// </summary>
        void InsertInfo(int numerkart, string pass2)
        {
            using (SqlConnection conn = new SqlConnection(LinkBaza.connString))
            {
                try
                {
                    conn.Open();
                    string Output = "";
                    string commandText = "select count(*) as cnt from userI WHERE numerkarty=@user AND haslo LIKE @pass";
                    SqlCommand command = new SqlCommand(commandText, conn);
                    command.Parameters.Add(new SqlParameter("user", numerkart));
                    command.Parameters.Add(new SqlParameter("pass", pass2));
                    command.ExecuteNonQuery();
                    SqlDataReader czytaj = command.ExecuteReader();
                    while (czytaj.Read())
                    {
                        Output = Output + czytaj.GetValue(0);
                    }
                    int test;
                    test = Int32.Parse(Output);
                    if (test == 1)
                    {
                            var menu = new Intent(this, typeof(MenuActivity));
                            StartActivity(menu);
                    }
                    else
                    {
                        tvTips.Text = "Nie możesz się zalogować, popraw dane.";

                    }
                }
                catch
                {
                    string info = "Brak dostępu do sieci.";
                    Toast.MakeText(this, info, ToastLength.Long).Show();
                }
                finally
                {
                    conn.Close();
                }
            }
        }
        /// <summary>
        /// Przycisk do włączania aktywności rejestracji.
        /// </summary>
        private void Btnrejestracja_Click(object sender, System.EventArgs e)
        {
        SetContentView(Resource.Layout.activity_rejestracja);
        }

    }
}

