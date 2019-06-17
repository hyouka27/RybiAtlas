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

namespace RybiAtlas
{
    /// <summary>
    /// Klasa statyczna która zajmuje się przechowywaniem zmiennych które są użytkowane w całej aplikacji,jest to np, sesja użytkownika, czy też zapis obecnego okręgu czy łowiska, tak by aktywność wiedziała co ma iść w zapytanie do bazy by coś zwrócić. 
    /// Mamy tu także connString czyli nasze dane do połączenia do bazy które wykorzystują wszystkie metody operujące na bazie.
    /// </summary>
    public static class LinkBaza
    {
    public static string Nazwa;
    public static string Nazwa2;
    public static string Opis;
    public static string Obrazek;
    public static int licznik;
    public static int numer;
    public static int Indeks;
    public static string lowsikobaza;
    public static string okregbaza;
    public static string connString = @"workstation id=77.55.213.238;packet size=4096;user id=kinga;pwd=test1!@W;data source=77.55.213.238;persist security info=False;initial catalog=WENDKA";
    }
}
