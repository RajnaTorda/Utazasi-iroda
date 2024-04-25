using System;
using System.Collections.Generic;
using System.IO;
using System.Threading;

class Utas
{
    public string Nev { get; set; }
    public string Cim { get; set; }
    public string Telefonszam { get; set; }
}

class Utazas
{
    public string Uticel { get; set; }
    public double Ar { get; set; }
    public int MaxLetszam { get; set; }
    public List<Utas> Utasok { get; set; }

    public Utazas()
    {
        Utasok = new List<Utas>();
    }
}

class Program
{
    static List<Utas> utasok = new List<Utas>();
    static List<Utazas> utazasok = new List<Utazas>();

    static void Main(string[] args)
    {
        
        LoadData();

        
        AppDomain.CurrentDomain.ProcessExit += new EventHandler(OnProcessExit);

        MainMenu();
    }

    
    static void LoadData()
    {
        if (File.Exists("utasok.txt"))
        {
            string[] lines = File.ReadAllLines("utasok.txt");
            foreach (string line in lines)
            {
                string[] parts = line.Split(',');
                utasok.Add(new Utas { Nev = parts[0], Cim = parts[1], Telefonszam = parts[2] });
            }
        }

        if (File.Exists("utazasok.txt"))
        {
            string[] lines = File.ReadAllLines("utazasok.txt");
            foreach (string line in lines)
            {
                string[] parts = line.Split(',');
                Utazas utazas = new Utazas
                {
                    Uticel = parts[0],
                    Ar = double.Parse(parts[1]),
                    MaxLetszam = int.Parse(parts[2])
                };
                utazasok.Add(utazas);

                for (int i = 3; i < parts.Length; i += 3)
                {
                    utazas.Utasok.Add(new Utas { Nev = parts[i], Cim = parts[i + 1], Telefonszam = parts[i + 2] });
                }
            }
        }
    }

    
    static void SaveData()
    {
        using (StreamWriter sw = new StreamWriter("utasok.txt"))
        {
            foreach (Utas utas in utasok)
            {
                sw.WriteLine($"{utas.Nev},{utas.Cim},{utas.Telefonszam}");
            }
        }

        using (StreamWriter sw = new StreamWriter("utazasok.txt"))
        {
            foreach (Utazas utazas in utazasok)
            {
                sw.Write($"{utazas.Uticel},{utazas.Ar},{utazas.MaxLetszam}");
                foreach (Utas utas in utazas.Utasok)
                {
                    sw.Write($",{utas.Nev},{utas.Cim},{utas.Telefonszam}");
                }
                sw.WriteLine();
            }
        }
    }

    static void MainMenu()
    {
        bool exit = false;

        while (!exit)
        {
            Console.WriteLine("1. Utasok kezelése");
            Console.WriteLine("2. Utazások adatainak kezelése");
            Console.WriteLine("3. Utazásokra jelentkezés");
            Console.WriteLine("4. Kilépés");

            Console.Write("Válasszon menüpontot: ");
            int choice;
            while (!int.TryParse(Console.ReadLine(), out choice))
            {
                Console.WriteLine("Érvénytelen választás! Kérem, adjon meg egy számot.");
                Console.Write("Válasszon menüpontot: ");
            }

            switch (choice)
            {
                case 1:
                    Console.Clear();
                    UtasMenu();
                    break;
                case 2:
                    Console.Clear();
                    UtazasMenu();
                    break;
                case 3:
                    Console.Clear();
                    UtazasraJelentkezes();
                    break;
                case 4:
                    exit = true;
                    break;
                default:
                    Console.Clear();
                    Console.WriteLine("Érvénytelen választás!");
                    break;
            }
        }
    }

    static void UtasMenu()
    {
        bool exit = false;

        while (!exit)
        {
            Console.WriteLine("1. Új utas felvétele");
            Console.WriteLine("2. Utas adatainak szerkesztése");
            Console.WriteLine("3. Vissza a főmenübe");

            Console.Write("Válasszon menüpontot: ");
            int choice;
            while (!int.TryParse(Console.ReadLine(), out choice))
            {
                Console.WriteLine("Érvénytelen választás! Kérem, adjon meg egy számot.");
                Console.Write("Válasszon menüpontot: ");
            }

            switch (choice)
            {
                case 1:
                    Console.Write("Új utas neve: ");
                    string nev = Console.ReadLine();
                    Console.Write("Új utas címe: ");
                    string cim = Console.ReadLine();
                    Console.Write("Új utas telefonszáma: ");
                    string telefonszam = Console.ReadLine();
                    Utas ujUtas = new Utas { Nev = nev, Cim = cim, Telefonszam = telefonszam };
                    utasok.Add(ujUtas);
                    Console.WriteLine("Új utas hozzáadva!");
                    Thread.Sleep(2000);
                    Console.Clear();
                    break;
                case 2:
                    Console.Write("Módosítandó utas neve: ");
                    string modositandoNev = Console.ReadLine();
                    Utas modositandoUtas = utasok.Find(utas => utas.Nev == modositandoNev);
                    if (modositandoUtas != null)
                    {
                        Console.Write("Új név: ");
                        modositandoUtas.Nev = Console.ReadLine();
                        Console.Write("Új cím: ");
                        modositandoUtas.Cim = Console.ReadLine();
                        Console.Write("Új telefonszám: ");
                        modositandoUtas.Telefonszam = Console.ReadLine();
                        Console.WriteLine("Az utas adatai módosítva!");
                        Thread.Sleep(2000);
                        Console.Clear();
                    }
                    else
                    {
                        Console.WriteLine("Nincs ilyen nevű utas!");
                    }
                    Thread.Sleep(4000);
                    Console.Clear();
                    break;
                case 3:
                    exit = true;
                    Console.Clear();
                    break;
                default:
                    Console.WriteLine("Érvénytelen választás!");
                    Thread.Sleep(4000);
                    Console.Clear();
                    break;
            }
        }
    }

    static void UtazasMenu()
    {
        bool exit = false;

        while (!exit)
        {
            Console.WriteLine("1. Új út felvétele");
            Console.WriteLine("2. Vissza a főmenübe");

            Console.Write("Válasszon menüpontot: ");
            int choice;
            while (!int.TryParse(Console.ReadLine(), out choice))
            {
                Console.WriteLine("Érvénytelen választás! Kérem, adjon meg egy számot.");
                Console.Write("Válasszon menüpontot: ");
            }

            switch (choice)
            {
                case 1:
                    Console.Write("Úticél: ");
                    string uticel = Console.ReadLine();
                    Console.Write("Ár: ");
                    double ar;
                    while (!double.TryParse(Console.ReadLine(), out ar))
                    {
                        Console.WriteLine("Érvénytelen ár! Kérem, adjon meg egy számot.");
                        Console.Write("Ár: ");
                    }
                    Console.Write("Maximális létszám: ");
                    int maxLetszam;
                    while (!int.TryParse(Console.ReadLine(), out maxLetszam))
                    {
                        Console.WriteLine("Érvénytelen létszám! Kérem, adjon meg egy számot.");
                        Console.Write("Maximális létszám: ");
                    }
                    Utazas ujUtazas = new Utazas { Uticel = uticel, Ar = ar, MaxLetszam = maxLetszam };
                    utazasok.Add(ujUtazas);
                    Console.WriteLine("Új út felvéve!");
                    Thread.Sleep(2000);
                    Console.Clear();
                    break;
                case 2:
                    exit = true;
                    Console.Clear();
                    break;
                default:
                    Console.WriteLine("Érvénytelen választás!");
                    Thread.Sleep(4000);
                    Console.Clear();
                    break;
            }
        }
    }

    static void UtazasraJelentkezes()
    {
        Console.WriteLine("Utazásra jelentkezés menü");
        Console.Write("Utas neve: ");
        string nev = Console.ReadLine();
        Utas utas = utasok.Find(u => u.Nev == nev);
        if (utas == null)
        {
            Console.WriteLine("Nincs ilyen nevű utas!");
            Thread.Sleep(5000);
            Console.Clear();
            return;
        }

        Console.Write("Uticél: ");
        string uticel = Console.ReadLine();
        Utazas utazas = utazasok.Find(u => u.Uticel == uticel);
        if (utazas == null)
        {
            Console.WriteLine("Nincs ilyen úticél!");
            Thread.Sleep(4000);
            Console.Clear();
            return;
        }

        if (utazas.Utasok.Count >= utazas.MaxLetszam)
        {
            Console.WriteLine("Az út megtelt, nem lehet további jelentkezőket felvenni.");
            return;
        }

        Console.Write("Előleg összege (max. {0}): ", utazas.Ar);
        double eloleg;
        while (!double.TryParse(Console.ReadLine(), out eloleg) || eloleg > utazas.Ar)
        {
            Console.WriteLine("Érvénytelen előleg! Kérem, adjon meg egy számot, ami nem haladja meg az út árát.");
            Console.Write("Előleg összege: ");
        }

        utasok.Remove(utas);
        utazas.Utasok.Add(utas);
        Console.WriteLine($"Sikeres jelentkezés az {uticel} útra, {nev} néven!");
        Console.WriteLine($"Befizetett előleg: {eloleg}.");
        Thread.Sleep(2000);
        Console.Clear();
    }

    
    static void OnProcessExit(object sender, EventArgs e)
    {
        SaveData();
    }
}
