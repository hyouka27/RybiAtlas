using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;

namespace START
{
    /// <summary>
    /// Klasa statyczna która zajmuje się przechowywaniem zmiennych które są użytkowane w całej aplikacji,jest to np, sesja użytkownika, czy też zapis obecnego okręgu czy łowiska, tak by aktywność wiedziała co ma iść w zapytanie do bazy by coś zwrócić. 
    /// Mamy tu także connString czyli nasze dane do połączenia do bazy które wykorzystują wszystkie metody operujące na bazie.
    /// </summary>
    public static class LinkBaza
    {
    public static string Nazwa;
    public static string Opis;
    public static int licznik;
    public static int numer;
    public static string lowsikobaza;
    public static string okregbaza;
    public static string connString = @"workstation id=testowa.mssql.somee.com;packet size=4096;user id=hyouka27_SQLLogin_1;pwd=1234567*;data source=testowa.mssql.somee.com;persist security info=False;initial catalog=testowa";
    }
}
