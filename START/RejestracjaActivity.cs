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
    [Activity(Label = "@string/rejestracja", Theme = "@style/AppTheme")]
    public class RejestracjaActivity : AppCompatActivity
    {
        /// <summary>
        /// Zmienne.
        /// </summary>
        private EditText nrkarty;
        private EditText pass;
        private EditText insimie;
        private EditText insnazwisko;
        private EditText instel;
        private EditText insmail;
        private TextView info;
        private Button btnrejinsert;

        protected override void OnCreate(Bundle savedInstanceState)
        {
            /// <summary>
            /// Zawiera opisy elementów przypisane do gui jak i metody.
            /// </summary>
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.activity_rejestracja);
            nrkarty = FindViewById<EditText>(Resource.Id.nrkarty);
            pass = FindViewById<EditText>(Resource.Id.Pass);
            insimie = FindViewById<EditText>(Resource.Id.insimie);
            insnazwisko = FindViewById<EditText>(Resource.Id.insnazwisko);
            instel = FindViewById<EditText>(Resource.Id.instel);
            insmail = FindViewById<EditText>(Resource.Id.insmail);
            btnrejinsert = FindViewById<Button>(Resource.Id.btnrejinsert);
            info = FindViewById<TextView>(Resource.Id.info);
            btnrejinsert.Click += Btnrejinsert_Click;
        }

        /// <summary>
        /// Sprawdza warunkowo czy podane dane są poprawne plus dokonuje konwersji z stringa na inta tam gdzie jest to potrzebne.
        /// </summary>
        private void Btnrejinsert_Click(object sender, System.EventArgs e)
        {
            string imie = insimie.Text;
            string nazwisko = insnazwisko.Text;
            string tel1 = instel.Text;
            int tel;
            if (int.TryParse(tel1, out tel))
            {
                tel = Int32.Parse(instel.Text);
            }
            string mail = insmail.Text;
            string numerkart1 = nrkarty.Text;
            int numerkart;
            if (int.TryParse(numerkart1, out numerkart))
            {
                numerkart = Int32.Parse(nrkarty.Text);
            }
            string pass2 = pass.Text;
            LinkBaza.numer = numerkart;
            InsertInfo2(numerkart,pass2,imie,nazwisko,tel,mail);
        }

        /// <summary>
        /// Metoda dodaje nowego użytkownika do bazy, oczywiście jeśli dane są poprawne i jest dostęp do sieci. 
        /// </summary>
        void InsertInfo2(int numerkart, string pass2,string imie,string nazwisko,int tel,string mail)
        {
            using (SqlConnection conn = new SqlConnection(LinkBaza.connString))
            {
                try
                {
                    conn.Open();
                    string commandText = "insert into userI(numerkarty,haslo,imie,nazwisko,telefon,email) values(@user,@pass,@imie,@nazwisko,@tel,@mail)";
                    SqlCommand command = new SqlCommand(commandText, conn);
                    command.Parameters.Add(new SqlParameter("user", numerkart));
                    command.Parameters.Add(new SqlParameter("pass", pass2));
                    command.Parameters.Add(new SqlParameter("imie", imie));
                    command.Parameters.Add(new SqlParameter("nazwisko", nazwisko));
                    command.Parameters.Add(new SqlParameter("tel", tel));
                    command.Parameters.Add(new SqlParameter("mail", mail));
                    command.ExecuteNonQuery();
                    conn.Close();
                    var menu = new Intent(this, typeof(MenuActivity));
                    StartActivity(menu);
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
    }
}