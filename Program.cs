
using System;

// WSPOLNY KONTRAKT. Wszystkie dane inicjalizujemy przed Start().
const int MAKS_ENERGIA = 10;
int pokoj = 1;
int energia = MAKS_ENERGIA;
bool karta = false;
bool bezpiecznik = false;
bool zasilanie = false;
bool koniec = false;
bool wygrana = false;

// SEKCJA 0: GOTOWY SILNIK NAUCZYCIELA.
Start();

while (true)
{
    Console.Write("> ");
    string? wejscie = Console.ReadLine();

    if (wejscie == null)
    {
        Console.WriteLine("Koniec wejscia. Zamykam program.");
        break;
    }

    string polecenie = wejscie.Trim().ToLowerInvariant();

    if (polecenie == "koniec")
    {
        Console.WriteLine("Do zobaczenia!");
        break;
    }

    if (polecenie == "")
    {
        Console.WriteLine("Wpisz polecenie albo pomoc.");
        continue;
    }

    switch (polecenie)
    {
        case "start": Start(); break;
        case "pomoc": Pomoc(); break;
        case "status": Status(); break;
        case "mapa": Mapa(); break;
        case "opis": Rozejrzyj(); break;
        case "lewo": Idz("lewo"); break;
        case "prawo": Idz("prawo"); break;
        case "karta": Akcja("karta"); break;
        case "bezpiecznik": Akcja("bezpiecznik"); break;
        case "napraw": Akcja("napraw"); break;
        case "wyjdz": Akcja("wyjdz"); break;

        default:
            Console.WriteLine("Nieznane polecenie. Wpisz pomoc.");
            break;
    }
}

void Start()
{
    pokoj = 1;
    energia = MAKS_ENERGIA;
    karta = false;
    bezpiecznik = false;
    zasilanie = false;
    koniec = false;
    wygrana = false;

    Console.WriteLine("UCIECZKA Z SERWEROWNI: PIATEK, 16:59.");
    Console.WriteLine("Administrator napisal: u mnie dziala. Drzwi twierdza inaczej.");
    Console.WriteLine("Masz " + MAKS_ENERGIA + " energii. Wpisz pomoc. Koniec zamyka program.");

    Rozejrzyj();
}

void ZakonczTure()
{
    if (energia > 0)
    {
        energia--;
    }

    Console.WriteLine("Pozostala energia: " + energia);

    if (wygrana)
    {
        koniec = true;
        Console.WriteLine("WYGRANA! Drzwi otwarte. Weekend uratowany.");
    }
    else if (energia <= 0)
    {
        koniec = true;
        Console.WriteLine("PRZEGRANA. Energia sie skonczyla. Wpisz start albo koniec.");
    }
}

void Status()
{
    Console.WriteLine("Pokoj: " + pokoj);
    Console.WriteLine("Energia: " + energia + "/" + MAKS_ENERGIA);
    Console.WriteLine("Karta: " + karta);
    Console.WriteLine("Bezpiecznik: " + bezpiecznik);
    Console.WriteLine("Zasilanie: " + zasilanie);
    Console.WriteLine("Koniec: " + koniec);
    Console.WriteLine("Wygrana: " + wygrana);
}

// SEKCJA A: INFORMACJE I MAPA.

string NazwaPokoju(int numer)
{
    if (numer == 1)
    {
        return "Recepcja";
    }
    else if (numer == 2)
    {
        return "Magazyn";
    }
    else if (numer == 3)
    {
        return "Serwerownia";
    }
    else if (numer == 4)
    {
        return "Wyjscie";
    }
    else
    {
        return "Nieznany pokoj";
    }
}

void Pomoc()
{
    Console.WriteLine("pomoc - wypisuje komendy wykorzystywane w grze");
    Console.WriteLine("status - dane rozgrywki");
    Console.WriteLine("mapa - cztery pokoje i znacznik pozycji");
    Console.WriteLine("opis - rozejrzenie sie w aktualnym pokoju");
    Console.WriteLine("lewo/prawo - ruch o jeden pokoj");
    Console.WriteLine("karta - proba zabrania karty");
    Console.WriteLine("bezpiecznik - proba zabrania bezpiecznika");
    Console.WriteLine("napraw - proba naprawy zasilania");
    Console.WriteLine("wyjdz - proba otwarcia drzwi");
    Console.WriteLine("start - nowa rozgrywka");
    Console.WriteLine("koniec - zamkniecie gry");
}

void Mapa()
{
    for (int numer = 1; numer <= 4; numer++)
    {
        string wiersz = numer + ". " + NazwaPokoju(numer);

        if (numer == pokoj)
        {
            wiersz = wiersz + " <--- jestes tutaj";
        }

        Console.WriteLine(wiersz);
    }
}

void Rozejrzyj()
{
    Console.WriteLine("Jestes w: " + NazwaPokoju(pokoj));

    if (pokoj == 1)
    {
        if (!karta)
        {
            Console.WriteLine("Na biurku lezy karta.");
        }
        else
        {
            Console.WriteLine("Biurko jest puste. Karte masz juz w kieszeni.");
        }
    }
    else if (pokoj == 2)
    {
        if (!bezpiecznik && !zasilanie)
        {
            Console.WriteLine("Na polce lezy bezpiecznik.");
        }
        else
        {
            Console.WriteLine("Polka jest pusta.");
        }
    }
    else if (pokoj == 3)
    {
        if (!zasilanie)
        {
            Console.WriteLine("Ciemno. Serwery milcza. Brakuje bezpiecznika w skrzynce.");
        }
        else
        {
            Console.WriteLine("Serwery szumia, swieca diody. Zasilanie dziala.");
        }
    }
    else if (pokoj == 4)
    {
        Console.WriteLine("Zablokowane drzwi z czytnikiem kart.");

        if (karta)
        {
            Console.WriteLine("Masz karte.");
        }
        else
        {
            Console.WriteLine("Brakuje ci karty.");
        }

        if (zasilanie)
        {
            Console.WriteLine("Czytnik ma prad.");
        }
        else
        {
            Console.WriteLine("Czytnik nie ma pradu.");
        }
    }
}

// SEKCJA B: RUCH.

void Idz(string kierunek)
{
    if (koniec)
    {
        Console.WriteLine("Gra jest zakonczona!");
        return;
    }

    int nastepnyPokoj = pokoj;

    if (kierunek == "prawo")
    {
        nastepnyPokoj++;
    }
    else if (kierunek == "lewo")
    {
        nastepnyPokoj--;
    }
    else
    {
        Console.WriteLine("Zly kierunek.");
        return;
    }

    if (nastepnyPokoj < 1 || nastepnyPokoj > 4)
    {
        Console.WriteLine("Nie mozna przejsc do tego pokoju.");
        return;
    }

    pokoj = nastepnyPokoj;

    Rozejrzyj();
    ZakonczTure();
}

// SEKCJA C: PRZEDMIOTY I WYGRANA.

void Akcja(string co)
{
    if (koniec)
    {
        Console.WriteLine("Gra zakonczona.");
        return;
    }
    if (co == "karta")
    {
        if (pokoj != 1 || karta)
        {
            Console.WriteLine("Tutaj nie ma karty do zabrania.");
            return;
        }

        karta = true;
        Console.WriteLine("Zabierasz karte.");
    }
    else if (co == "bezpiecznik")
    {
        if (pokoj != 2 || bezpiecznik || zasilanie)
        {
            Console.WriteLine("Tutaj nie ma bezpiecznika do zabrania.");
            return;
        }

        bezpiecznik = true;
        Console.WriteLine("Zabierasz bezpiecznik.");
    }
    else if (co == "napraw")
    {
        if (pokoj != 3 || !bezpiecznik || zasilanie)
        {
            Console.WriteLine("Tutaj nie mozesz naprawic zasilania.");
            return;
        }

        zasilanie = true;
        bezpiecznik = false;
        Console.WriteLine("Naprawiasz zasilanie.");
    }
    else if (co == "wyjdz")
    {
        if (pokoj != 4 || !karta || !zasilanie)
        {
            Console.WriteLine("Nie mozesz wyjsc.");
            return;
        }

        wygrana = true;
    }
    else
    {
        Console.WriteLine("Nieznana akcja.");
        return;
    }

    ZakonczTure();
}
