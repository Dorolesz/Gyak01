using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gyak01
{
    internal class Program
    {
        static Random r = new Random();
        static int sorokSzama = 5;
        static int oszlopokSzama = 10;
        static bool[,] matrix = new bool[sorokSzama, oszlopokSzama];

        static Dictionary<string, string> etlap = new Dictionary<string, string>();
        static Dictionary<string, int> raktarKeszlet = new Dictionary<string, int>();


        static void Idojaras()
        {
            List<int> maxHomerseklet = new List<int>();
            List<int> minHomerseklet = new List<int>();
            for (int i = 0; i < 7; i++)
            {
                maxHomerseklet.Add(r.Next(25, 40));
                minHomerseklet.Add(r.Next(25, 40));
            }
            for (int i = 0; i < 7; i++)
            {
                Console.WriteLine($"{i + 1}. napon a minimum: {minHomerseklet[i]} " +
                    $"a maximum: {maxHomerseklet[i]}");
            }

            double atlag = (maxHomerseklet.Sum() + minHomerseklet.Sum()) / (1.0 * maxHomerseklet.Count + minHomerseklet.Count);
            Console.WriteLine($"A heti átlaghőmérséklet: {Math.Round(atlag, 2)}");

            //legmelegebb nap:
            int maxi = maxHomerseklet.Max();
            int index = maxHomerseklet.IndexOf(maxi);
            Console.WriteLine($"A legmelegebb ({maxi} Celsius) {index + 1}. napon volt");
        }

        static void Szovegszerkeszto()
        {
            using (StreamReader r = new StreamReader("szavak.txt"))
            {
                List<string> szavak = new List<string>();
                while (!r.EndOfStream)
                {
                    szavak.Add(r.ReadLine());
                }
                foreach (var item in szavak)
                {
                    Console.WriteLine(item);
                }
            }

            using (StreamWriter sw = new StreamWriter("szavak.txt", true))
            {
                Console.WriteLine("Kérek egy szót, amit a fájlba írjunk: ");
                string szo = Console.ReadLine();
                sw.WriteLine(szo);
            }
        }

        static void Nevjegyzek()
        {
            Dictionary<string, string> nevjegyzek = new Dictionary<string, string>
            {
                { "Joska", "0690631631" },
                { "Piroska", "0690631632" },
                { "Aladár", "0690631633" },
                { "Julcsa", "0690631634" }
            };

            foreach (var item in nevjegyzek)
            {
                Console.WriteLine($"{item.Key} - {item.Value}");
            }

            Console.WriteLine($"Aladár telefonszáma: {nevjegyzek["Aladár"]}");

            foreach (var item in nevjegyzek)
            {
                if (item.Value.Equals("0690631633"))
                {
                    Console.WriteLine($"{item.Key} száma 0690631633");
                }
            }
        }

        static void Mozi()
        {
            while (true)
            {
                Console.Clear();
                MatrixKiiratas();
                Console.WriteLine("Válassz egy műveletet:");
                Console.WriteLine("1. Hely foglalása");
                Console.WriteLine("2. Hely visszamondása");
                Console.WriteLine("3. Kilépés");
                string valasz = Console.ReadLine();

                switch (valasz)
                {
                    case "1":
                        HelyFoglalas();
                        break;
                    case "2":
                        HelyVisszamondas();
                        break;
                    case "3":
                        return;
                    default:
                        Console.WriteLine("Érvénytelen választás.");
                        break;
                }
            }
        }

        static void MatrixKiiratas()
        {
            Console.Write("   ");
            for (int i = 1; i <= oszlopokSzama; i++)
            {
                Console.Write($"{i,3} ");
            }
            Console.WriteLine();

            for (int i = 0; i < sorokSzama; i++)
            {
                Console.Write($"{i + 1,2} ");
                for (int j = 0; j < oszlopokSzama; j++)
                {
                    char karakter = matrix[i, j] ? 'X' : 'O';
                    Console.Write($"{karakter,3} ");
                }
                Console.WriteLine();
            }
        }

        static void HelyFoglalas()
        {
            Console.Write("Add meg a foglalni kívánt hely sorát és oszlopát (pl. 2 5): ");
            string[] bemenet = Console.ReadLine().Split(' ');

            if (bemenet.Length != 2 ||
                !int.TryParse(bemenet[0], out int sor) ||
                !int.TryParse(bemenet[1], out int oszlop) ||
                sor < 1 || sor > sorokSzama ||
                oszlop < 1 || oszlop > oszlopokSzama)
            {
                Console.WriteLine("Érvénytelen bemenet.");
                return;
            }

            sor--;
            oszlop--;

            if (matrix[sor, oszlop])
            {
                Console.WriteLine("Ez a hely már foglalt.");
            }
            else
            {
                matrix[sor, oszlop] = true;
                Console.WriteLine("Hely sikeresen lefoglalva.");
            }

            Console.WriteLine("Nyomj meg egy billentyűt a folytatáshoz.");
            Console.ReadKey();
        }

        static void HelyVisszamondas()
        {
            Console.Write("Add meg a visszamondani kívánt hely sorát és oszlopát (pl. 2 5): ");
            string[] bemenet = Console.ReadLine().Split(' ');

            if (bemenet.Length != 2 ||
                !int.TryParse(bemenet[0], out int sor) ||
                !int.TryParse(bemenet[1], out int oszlop) ||
                sor < 1 || sor > sorokSzama ||
                oszlop < 1 || oszlop > oszlopokSzama)
            {
                Console.WriteLine("Érvénytelen bemenet.");
                return;
            }

            sor--;
            oszlop--;

            if (!matrix[sor, oszlop])
            {
                Console.WriteLine("Ez a hely még nincs lefoglalva.");
            }
            else
            {
                matrix[sor, oszlop] = false;
                Console.WriteLine("Hely sikeresen visszamondva.");
            }

            Console.WriteLine("Nyomj meg egy billentyűt a folytatáshoz.");
            Console.ReadKey();
        }


        static void Etlap()
        {
            while (true)
            {
                Console.Clear();
                Console.WriteLine("Éttermünk heti menüje:");
                Console.WriteLine("1. Menü megtekintése");
                Console.WriteLine("2. Étel hozzáadása");
                Console.WriteLine("3. Étel módosítása");
                Console.WriteLine("4. Étel törlése");
                Console.WriteLine("5. Kilépés");
                Console.Write("Választás: ");
                string valasz = Console.ReadLine();

                switch (valasz)
                {
                    case "1":
                        MenuMegtekintese();
                        break;
                    case "2":
                        EtelHozzaadasa();
                        break;
                    case "3":
                        EtelModositasa();
                        break;
                    case "4":
                        EtelTorlese();
                        break;
                    case "5":
                        return;
                    default:
                        Console.WriteLine("Érvénytelen választás. Kérlek próbáld újra.");
                        break;
                }
            }
        }

        static void MenuMegtekintese()
        {
            Console.Clear();
            Console.WriteLine("Heti Menü:");
            if (etlap.Count == 0)
            {
                Console.WriteLine("A menü üres.");
            }
            else
            {
                foreach (var etel in etlap)
                {
                    Console.WriteLine($"{etel.Key}: {etel.Value}");
                }
            }
            Console.WriteLine("Nyomj meg egy billentyűt a folytatáshoz.");
            Console.ReadKey();
        }

        static void EtelHozzaadasa()
        {
            Console.Clear();
            Console.WriteLine("Add meg az étel nevét: ");
            string nev = Console.ReadLine();
            Console.Write("Add meg az étel leírását: ");
            string leiras = Console.ReadLine();

            if (etlap.ContainsKey(nev))
            {
                Console.WriteLine("Ez az étel már szerepel a menüben.");
            }
            else
            {
                etlap.Add(nev, leiras);
                Console.WriteLine("Étel sikeresen hozzáadva.");
            }
            Console.WriteLine("Nyomj meg egy billentyűt a folytatáshoz.");
            Console.ReadKey();
        }

        static void EtelModositasa()
        {
            Console.Clear();
            Console.Write("Add meg az étel nevét, amit módosítani szeretnél: ");
            string nev = Console.ReadLine();

            if (etlap.ContainsKey(nev))
            {
                Console.Write("Add meg az új leírást: ");
                string ujLeiras = Console.ReadLine();
                etlap[nev] = ujLeiras;
                Console.WriteLine("Étel sikeresen módosítva.");
            }
            else
            {
                Console.WriteLine("Ez az étel nem található a menüben.");
            }
            Console.WriteLine("Nyomj meg egy billentyűt a folytatáshoz.");
            Console.ReadKey();
        }

        static void EtelTorlese()
        {
            Console.Clear();
            Console.Write("Add meg az étel nevét, amit törölni szeretnél: ");
            string nev = Console.ReadLine();

            if (etlap.ContainsKey(nev))
            {
                etlap.Remove(nev);
                Console.WriteLine("Étel sikeresen törölve.");
            }
            else
            {
                Console.WriteLine("Ez az étel nem található a menüben.");
            }
            Console.WriteLine("Nyomj meg egy billentyűt a folytatáshoz.");
            Console.ReadKey();
        }

        static void Kereso()
        {

        }

        static void Gyumolcs()
        {
            while (true)
            {
                Console.Clear();
                Console.WriteLine("Gyümölcs raktárkészlet kezelő");
                Console.WriteLine("1. Gyümölcs hozzáadása");
                Console.WriteLine("2. Készlet csökkentése");
                Console.WriteLine("3. Készlet megjelenítése");
                Console.WriteLine("4. Kilépés");
                Console.Write("Válassz egy műveletet: ");
                string valasz = Console.ReadLine();

                switch (valasz)
                {
                    case "1":
                        HozzaadGyumolcs();
                        break;
                    case "2":
                        CsokkentKeszlet();
                        break;
                    case "3":
                        MegjelenitKeszlet();
                        break;
                    case "4":
                        return;
                    default:
                        Console.WriteLine("Érvénytelen választás. Próbáld újra.");
                        break;
                }
            }
        }

        static void HozzaadGyumolcs()
        {
            Console.Write("Add meg a gyümölcs nevét: ");
            string nev = Console.ReadLine()?.Trim();
            if (string.IsNullOrEmpty(nev))
            {
                Console.WriteLine("A gyümölcs neve nem lehet üres.");
                return;
            }

            Console.Write("Add meg a mennyiséget: ");
            if (!int.TryParse(Console.ReadLine(), out int mennyiseg) || mennyiseg <= 0)
            {
                Console.WriteLine("Érvénytelen mennyiség.");
                return;
            }

            if (raktarKeszlet.ContainsKey(nev))
            {
                raktarKeszlet[nev] += mennyiseg;
            }
            else
            {
                raktarKeszlet[nev] = mennyiseg;
            }

            Console.WriteLine($"A {nev} mennyisége {mennyiseg} darabbal frissítve.");
            Console.WriteLine("Nyomj meg egy billentyűt a folytatáshoz.");
            Console.ReadKey();
        }

        static void CsokkentKeszlet()
        {
            Console.Write("Add meg a gyümölcs nevét: ");
            string nev = Console.ReadLine()?.Trim();
            if (string.IsNullOrEmpty(nev))
            {
                Console.WriteLine("A gyümölcs neve nem lehet üres.");
                return;
            }

            if (!raktarKeszlet.ContainsKey(nev))
            {
                Console.WriteLine($"A {nev} gyümölcs nem található a raktárkészletben.");
                return;
            }

            Console.Write("Add meg a csökkenteni kívánt mennyiséget: ");
            if (!int.TryParse(Console.ReadLine(), out int mennyiseg) || mennyiseg <= 0)
            {
                Console.WriteLine("Érvénytelen mennyiség.");
                return;
            }

            if (raktarKeszlet[nev] < mennyiseg)
            {
                Console.WriteLine("A csökkenteni kívánt mennyiség meghaladja a raktárkészletben lévő mennyiséget.");
                return;
            }

            raktarKeszlet[nev] -= mennyiseg;

            if (raktarKeszlet[nev] == 0)
            {
                raktarKeszlet.Remove(nev);
                Console.WriteLine($"A {nev} gyümölcs teljesen elfogyott és eltávolításra került a raktárkészletből.");
            }
            else
            {
                Console.WriteLine($"A {nev} mennyisége {mennyiseg} darabbal csökkentve.");
            }

            Console.WriteLine("Nyomj meg egy billentyűt a folytatáshoz.");
            Console.ReadKey();
        }

        static void MegjelenitKeszlet()
        {
            Console.WriteLine("Raktárkészlet:");
            if (raktarKeszlet.Count == 0)
            {
                Console.WriteLine("A raktárkészlet üres.");
            }
            else
            {
                foreach (var item in raktarKeszlet)
                {
                    Console.WriteLine($"{item.Key}: {item.Value} db");
                }
            }
            Console.WriteLine("Nyomj meg egy billentyűt a folytatáshoz.");
        }

        static void Main(string[] args)
        {
            //Idojaras();
            //Szovegszerkeszto();
            //Nevjegyzek();
            //Mozi();
            //Etlap();
            //Kereso();
            Gyumolcs();

            Console.ReadKey();
        }
    }
}
