using CA240129;

//függvények, metódusok
static void Beolvasas(List<Ember> lista) 
{
	try
	{
        StreamReader sr = new(@"..\..\..\src\telefonkonyv.txt");
        while (!sr.EndOfStream) lista.Add(new Ember(sr.ReadLine()));
        Console.WriteLine("Sikeres fájl beolvasás!");
        sr.Close();
    }
	catch
	{
        Console.WriteLine("Hiba a fájl beolvasása során!");
    }
}
static void Menu(List<Ember> lista) 
{
    Console.Clear();
    Console.WriteLine($"|-----------------------------------|");
    Console.WriteLine($"{"|",-10}{"Telefonkönyv Menu",2}{"|",10}");
    Console.WriteLine($"{"|",-10}{"|",27}");
    Console.WriteLine($"{"|",-10}{"[1] Listázás",2}{"|",15}");
    Console.WriteLine($"{"|",-10}{"|",27}");
    Console.WriteLine($"{"|",-10}{"[2] Keresés",2}{"|",16}");
    Console.WriteLine($"{"|",-10}{"|",27}");
    Console.WriteLine($"{"|",-10}{"[3] Hozzáadás",2}{"|",14}");
    Console.WriteLine($"{"|",-10}{"|",27}");
    Console.WriteLine($"{"|",-10}{"[4] Törlés",2}{"|",17}");
    Console.WriteLine($"{"|",-10}{"|",27}");
    Console.WriteLine($"{"|",-10}{"[5] Módosítás",2}{"|",14}");
    Console.WriteLine($"{"|",-10}{"|",27}");
    Console.WriteLine($"|-----------------------------------|");

    switch (MenuValasztas(lista))
    {
        case 1:
            Listazas(lista);
            break;
        case 2:
            KeresesNevAlapon(lista); 
            break;
        case 3:
            HozzaAdas(lista);
            break;
        case 4:
            Torles(lista);
            break;
        case 5:
            Modositas(lista);
            break;
        default:
            HibasValaszSzam();
            Thread.Sleep(1000);
            Menu(lista);
            MenuValasztas(lista);
            break;
    }
}
static int MenuValasztas(List<Ember> lista) 
{
    try
    {
        Console.Write("\nAdd meg a kívánt opciót: ");
        return Convert.ToInt32(Console.ReadLine());
    }
    catch
    {
        HibasValaszSzam();
        Thread.Sleep(1000);
        Menu(lista);
        MenuValasztas(lista);
    }
    return 0;
}
static void HibasValaszSzam() 
{
    Console.Clear();
    Console.WriteLine($"|-------------------------------------------------------|");
    Console.WriteLine($"{"|",-5}{"Nem számot vagy nem megfelelő számot adtál meg!"}{"|",5}");
    Console.WriteLine($"|-------------------------------------------------------|");
}
static void HibasValaszGomb() 
{
    Console.Clear();
    Console.WriteLine($"|--------------------------------------|");
    Console.WriteLine($"{"|",-5}{"Nem a kért gombot nyomtad meg!"}{"|",5}");
    Console.WriteLine($"|--------------------------------------|");
}
static void Listazas(List<Ember> lista) 
{
    Console.Clear();
    Console.WriteLine($"|------------------------------------------------|");
    lista.ForEach(x => Console.WriteLine($"{" ",-10}{$"{x,10}{" ",10}"}"));
    Console.WriteLine($"|------------------------------------------------|");
    Console.Write("\nNyomj egy Backspace-t hogy vissza juss a menübe!");
    ConsoleKeyInfo pressed = Console.ReadKey();

    while (pressed.Key != ConsoleKey.Backspace)
    {
        HibasValaszGomb();
        Thread.Sleep(1000);
        Listazas(lista);
    }

    Menu(lista);

}
static void KeresesNevAlapon(List<Ember> lista) 
{
    Console.Clear();
    Console.Write("Kit keresünk: ");
    string keresett = Console.ReadLine().ToLower();

    var talalat = lista.Where(e => e.Neve.ToLower().Contains(keresett)).ToList();

    if (talalat.Count >= 1)
    {
        Console.WriteLine($"\n|------------------------------------------------------|");
        talalat.ForEach(e => Console.WriteLine($"{" ",-10}{e,30}{" ",10}"));
        Console.WriteLine($"|------------------------------------------------------|");
        Console.Write("\nNyomj egy Backspace-t hogy vissza juss a menübe vagy Enter-t hogy újra kereshess!");
        ConsoleKeyInfo pressed = Console.ReadKey();

        while ((pressed.Key != ConsoleKey.Backspace) || (pressed.Key != ConsoleKey.Enter))
        {
            if (pressed.Key == ConsoleKey.Enter) KeresesNevAlapon(lista);
            else if (pressed.Key == ConsoleKey.Backspace) Menu(lista);
            else
            {
                Console.Write("\nNyomj egy Backspace-t hogy vissza juss a menübe vagy Enter-t hogy újra kereshess!");
                pressed = Console.ReadKey();
            }
        }
    }
    else 
    {
        Console.WriteLine($"\n|---------------------------------------------------------|");
        Console.WriteLine($"{" ",-5}{$"Nincs {keresett} nevű ember a telefonkönyben!",10}{" ",5}");
        Console.WriteLine($"|---------------------------------------------------------|");
        Console.Write("\nNyomj egy Backspace-t hogy vissza juss a menübe vagy Enter-t hogy újra kereshess!");
        ConsoleKeyInfo pressed = Console.ReadKey();

        while ((pressed.Key != ConsoleKey.Backspace) || (pressed.Key != ConsoleKey.Enter))
        {
            if (pressed.Key == ConsoleKey.Enter) KeresesNevAlapon(lista);
            else if (pressed.Key == ConsoleKey.Backspace) Menu(lista);
            else
            {
                Console.Write("\nNyomj egy Backspace-t hogy vissza juss a menübe vagy Enter-t hogy újra kereshess!");
                pressed = Console.ReadKey();
            }
        }
    }
}
static void HozzaAdas(List<Ember> lista) 
{
    Console.Clear();
    Console.Write("Hozzá adni kívánt neve: ");
    string kivantNev = Console.ReadLine();
    Console.Write("Hozzá adni kívánt telefonszáma: ");
    string kivantTelSzam = Console.ReadLine();

    if ((string.IsNullOrEmpty(kivantNev.Trim())) || (string.IsNullOrEmpty(kivantTelSzam.Trim())))
    {
        Console.Clear();
        Console.WriteLine("|-------------------------------------------------------------------------------------|");
        Console.WriteLine("|  A kért nevet/telefonszámot nem tudtuk hozzá adni a telefonkönyvhöz, próbáld újra!  |");
        Console.WriteLine("|-------------------------------------------------------------------------------------|");
        Thread.Sleep(2500);
        HozzaAdas(lista);
    }
    else if(lista.Where(e => e.Telefonszama.Contains(kivantTelSzam)).Count() >= 1) 
    {
        Console.Clear();
        Console.WriteLine("|-------------------------------------------------------------------------------------|");
        Console.WriteLine("|  A kért telefonszámot nem tudtuk hozzá adni a telefonkönyvhöz, mert már létezik!    |");
        Console.WriteLine("|-------------------------------------------------------------------------------------|");
        Thread.Sleep(2500);
        HozzaAdas(lista);
    }
    else
    {
        try
        {
            lista.Add(new Ember(kivantNev, kivantTelSzam));
            StreamWriter sw = new(@"..\..\..\src\telefonkonyv.txt");
            lista.ForEach(x => sw.WriteLine($"{x.Neve};{x.Telefonszama}"));
            sw.Close();
            Console.WriteLine("\nAz új ember sikeresen hozzáadva a telefonkönyvhöz!");

            Console.Write("\nNyomj egy Backspace-t hogy vissza juss a menübe vagy Enter-t hogy újra hozzáadhass egy embert!");
            ConsoleKeyInfo pressed = Console.ReadKey();

            while ((pressed.Key != ConsoleKey.Backspace) || (pressed.Key != ConsoleKey.Enter))
            {
                if (pressed.Key == ConsoleKey.Enter) HozzaAdas(lista);
                else if (pressed.Key == ConsoleKey.Backspace) Menu(lista);
                else
                {
                    Console.Write("\nNyomj egy Backspace-t hogy vissza juss a menübe vagy Enter-t hogy újra hozzáadhass egy embert!");
                    pressed = Console.ReadKey();
                }
            }
        }
        catch
        {
            //Valami használja éppen a filet
            Console.WriteLine("Hiba a telefonkönyv kezelése közben, NEM lett elmentve a hozzáadni kívánt ember!");
            Thread.Sleep(2500);
            Menu(lista);
        }

    }

}
static void Torles(List<Ember> lista) 
{
    Console.Clear();
    Console.WriteLine($"|------------------------------------------------|");
    lista.ForEach(x => Console.WriteLine($"{" ",-10}{$"{x,10}{" ",10}"}"));
    Console.WriteLine($"|------------------------------------------------|");
    Console.Write("\nTörölni kívánt személy neve: ");
    string szemelyNeve = Console.ReadLine().ToLower();

    if (string.IsNullOrEmpty(szemelyNeve.Trim()))
    {
        Console.WriteLine("\"Senkit\" nem tudsz törölni!");
        Thread.Sleep(1500);
        Menu(lista);
    }
    else 
    {
        Console.Clear();

        int db = lista.Where(e => e.Neve.ToLower().Contains(szemelyNeve))
            .ToList()
            .Count();

        if (db > 1)
        {
            List<string> tobbember =
                lista
                .Where(e => e.Neve.ToLower().Contains(szemelyNeve))
                .Select((e, i) => $"{i + 1}. {e}")
                .ToList();

            Console.WriteLine($"|------------------------------------------------|");
            tobbember.ForEach(x => Console.WriteLine($"{" ",-10}{$"{x,10}{" ",10}"}"));
            Console.WriteLine($"|------------------------------------------------|");
            Console.Write("Törölni kívánt ember sorszáma, ha mégsem szeretnél törölni írd be hogy \"nem\": ");
            string valasz = Console.ReadLine().ToLower();
            int valaszSzam = 0;
            bool lehetE = int.TryParse(valasz, out valaszSzam);


            while (valasz.Trim() != "nem" && (valaszSzam > tobbember.Count() || valaszSzam <= 0))
            {
                Console.Write("Törölni kívánt ember sorszáma, ha mégsem szeretnél törölni írd be hogy \"nem\": ");
                valasz = Console.ReadLine().ToLower();
                lehetE = int.TryParse(valasz, out valaszSzam);
            }

            if (valasz.Trim() == "nem")
            {
                Menu(lista);
            }
            else
            {
                List<string> atmeneti = tobbember[valaszSzam - 1].Split('.').ToList();
                List<string> atmeneti2 = atmeneti[1].Split('|').ToList(); //atmeneti2[1] == telefonszámmal ami egyedi

                lista.Remove(lista.Where(e => e.Telefonszama == atmeneti2[1].Trim()).First());
                StreamWriter sw = new(@"..\..\..\src\telefonkonyv.txt");
                lista.ForEach(x => sw.WriteLine($"{x.Neve};{x.Telefonszama}"));
                sw.Close();
            }

        }
        else if (db == 1)
        {
            Console.WriteLine($"|------------------------------------------------|");
            lista
                .Where(e => e.Neve.ToLower().Contains(szemelyNeve))
                .ToList()
                .ForEach(x => Console.WriteLine($"{" ",-10}{$"{x,10}{" ",10}"}"));
            Console.WriteLine($"|------------------------------------------------|");

            Console.Write($"\nBiztosan {szemelyNeve}-t szeretnéd törölni (Igen/Nem): ");
            string valasz = Console.ReadLine().ToLower();
            if (valasz.Trim() == "nem")
            {
                Menu(lista);
            }
            else if (valasz.Trim() == "igen")
            {
                try
                {
                    lista.Remove(lista.Where(e => e.Neve.ToLower().Contains(szemelyNeve)).First());
                    StreamWriter sw = new(@"..\..\..\src\telefonkonyv.txt");
                    lista.ForEach(x => sw.WriteLine($"{x.Neve};{x.Telefonszama}"));
                    sw.Close();

                    Console.WriteLine($"{szemelyNeve} sikeresen törölve!");
                    Thread.Sleep(1500);
                    Menu(lista);
                }
                catch
                {
                    Console.WriteLine("Hiba a törlés során, NEM lett törölve a kívánt személy!");
                    Thread.Sleep(1500);
                    Menu(lista);
                }
            }
        }
        else 
        {
            Console.WriteLine($"Nincs {szemelyNeve} nevű ember!");
            Thread.Sleep(1500);
            Menu(lista);
        }
    }

}
static void Modositas(List<Ember> lista) 
{
    Console.Clear();
    Console.WriteLine("|---------------------------------------------------------------|");
    lista.Select((e,i) => $"{i+1}. {e}").ToList().ForEach(x => Console.WriteLine($"{" ",-10}{$"{x,10}"}{" ",10}"));
    Console.WriteLine("|---------------------------------------------------------------|");
    Console.Write("\nMódosítani kívánt személy neve: ");
    string szemelyNeve = Console.ReadLine().ToLower();

    if (string.IsNullOrEmpty(szemelyNeve.Trim()))
    {
        Console.WriteLine("\"Senkit\" nem tudsz módosítani!");
        Thread.Sleep(1500);
        Menu(lista);
    }
    else 
    {
        Console.Clear();

        int db = lista.Where(e => e.Neve.ToLower().Contains(szemelyNeve))
            .ToList()
            .Count();

        if (db > 1)
        {
            List<string> tobbember =
                lista
                .Where(e => e.Neve.ToLower().Contains(szemelyNeve))
                .Select((e, i) => $"{i + 1}. {e}")
                .ToList();

            Console.WriteLine($"|------------------------------------------------|");
            tobbember.ForEach(x => Console.WriteLine($"{" ",-10}{$"{x,10}{" ",10}"}"));
            Console.WriteLine($"|------------------------------------------------|");
            Console.Write("Módosítani kívánt ember sorszáma, ha mégsem szeretnél módosítani írd be hogy \"nem\": ");
            string valasz = Console.ReadLine().ToLower();
            int valaszSzam = 0;
            bool lehetE = int.TryParse(valasz, out valaszSzam);


            while (valasz.Trim() != "nem" && (valaszSzam > tobbember.Count() || valaszSzam <= 0))
            {
                Console.Write("Módosítani kívánt ember sorszáma, ha mégsem szeretnél módosítani írd be hogy \"nem\": ");
                valasz = Console.ReadLine().ToLower();
                lehetE = int.TryParse(valasz, out valaszSzam);
            }

            if (valasz.Trim() == "nem")
            {
                Menu(lista);
            }
            else
            {
                List<string> atmeneti = tobbember[valaszSzam - 1].Split('.').ToList();
                List<string> atmeneti2 = atmeneti[1].Split('|').ToList(); //atmeneti2[1] == telefonszámmal ami egyedi

                string jelenlegiTel = atmeneti2[1].Trim();
                Console.Clear();
                Console.WriteLine($"{szemelyNeve} jelenlegi telefonszáma: {jelenlegiTel}");
                Console.Write("\nMire szeretnéd módosítani: ");
                string ujTel = Console.ReadLine();

                lista.Where(e => e.Telefonszama.Contains(jelenlegiTel)).First().Telefonszama = ujTel;
                StreamWriter sw = new(@"..\..\..\src\telefonkonyv.txt");
                lista.ForEach(e => sw.WriteLine($"{e.Neve};{e.Telefonszama}"));
                sw.Close();

                Console.WriteLine($"{szemelyNeve} telefonszáma sikeresen módosítva!");
                Thread.Sleep(1500);
                Menu(lista);

            }

        }
        else if (db == 1)
        {
            Console.WriteLine($"|------------------------------------------------|");
            lista
                .Where(e => e.Neve.ToLower().Contains(szemelyNeve))
                .ToList()
                .ForEach(x => Console.WriteLine($"{" ",-10}{$"{x,10}{" ",10}"}"));
            Console.WriteLine($"|------------------------------------------------|");

            Console.Write($"\nBiztosan {szemelyNeve}-t szeretnéd módosítani (Igen/Nem): ");
            string valasz = Console.ReadLine().ToLower();
            if (valasz.Trim() == "nem")
            {
                Menu(lista);
            }
            else if (valasz.Trim() == "igen")
            {
                try
                {
                    string regiTel = lista.Where(e => e.Neve.ToLower().Contains(szemelyNeve)).Select(e => e.Telefonszama).First();
                    Console.Clear();
                    Console.WriteLine($"{szemelyNeve} jelenlegi telefonszáma: {regiTel}");
                    Console.Write("\nMire szeretnéd módosítani: ");
                    string ujTel = Console.ReadLine();

                    lista.Where(e => e.Telefonszama.Contains(regiTel)).First().Telefonszama = ujTel;
                    StreamWriter sw = new(@"..\..\..\src\telefonkonyv.txt");
                    lista.ForEach(e => sw.WriteLine($"{e.Neve};{e.Telefonszama}"));
                    sw.Close();

                    Console.WriteLine($"{szemelyNeve} telefonszáma sikeresen módosítva!");
                    Thread.Sleep(1500);
                    Menu(lista);
                }
                catch
                {
                    Console.WriteLine("Hiba a módosítás során, NEM lett módosítva a kívánt személy!");
                    Thread.Sleep(1500);
                    Menu(lista);
                }
            }
        }
        else
        {
            Console.WriteLine($"Nincs {szemelyNeve} nevű ember!");
            Thread.Sleep(1500);
            Menu(lista);
        }

    }

}

List<Ember> telefonkonyv = new();

//Beolvasás és listába rakás
Beolvasas(telefonkonyv);

//Menu megjelenítése
Menu(telefonkonyv);





