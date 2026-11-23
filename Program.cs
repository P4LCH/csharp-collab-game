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

// SEKCJA 0: GOTOWY SILNIK NAUCZYCIELA. Nie zmieniaj w zadaniu podstawowym.
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

    // Rozdzielacz dziala od poczatku, zeby zespoly mogly testowac niezaleznie.
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
        default: Console.WriteLine("Nieznane polecenie. Wpisz pomoc."); break;
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
    energia = energia - 1;
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

// Status jest gotowy: sluzy do testow, zanim autor mapy skonczy swoja sekcje.
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

// SEKCJA A: INFORMACJE I MAPA. Wlasciciel: osoba A.
string NazwaPokoju(int numer)
{
    switch (numer)
    {
        case 1: return "Recepcja";
        case 2: return "Magazyn";
        case 3: return "Serwerownia";
        case 4: return "Wyjscie";
        default: return "Nieznany pokoj";
    }
}

void Pomoc()
{
    Console.WriteLine("DOSTEPNE KOMENDY:");
    Console.WriteLine("start - rozpocznij gre");
    Console.WriteLine("pomoc - wyswietl pomoc");
    Console.WriteLine("status - sprawdz stan gry");
    Console.WriteLine("mapa - pokaz mape");
    Console.WriteLine("opis - rozejrzyj sie");
    Console.WriteLine("lewo - przejdz do poprzedniego pokoju");
    Console.WriteLine("prawo - przejdz do nastepnego pokoju");
    Console.WriteLine("karta - zabierz karte");
    Console.WriteLine("bezpiecznik - zabierz bezpiecznik");
    Console.WriteLine("napraw - napraw zasilanie");
    Console.WriteLine("wyjdz - sprobuj opuscic budynek");
    Console.WriteLine("dziennik - historia czynnosci (dodamy pozniej)");
    Console.WriteLine("start - zresetuj gre");
    Console.WriteLine("koniec - zamknij program");
    Console.WriteLine("Udany ruch lub akcja kosztuje 1 energie.");
}

void Mapa()
{
    Console.WriteLine("======= MAPA =======");

    for (int i = 1; i <= 4; i++)
    {
        if (i == pokoj)
        {
            Console.WriteLine("[" + i + "] " + NazwaPokoju(i) + " <-- TY");
        }
        else
        {
            Console.WriteLine("[" + i + "] " + NazwaPokoju(i));
        }
    }

    Console.WriteLine("====================");
}


void Rozejrzyj()
{
    Console.WriteLine("Jestes w: " + NazwaPokoju(pokoj));

    switch (pokoj)
    {
        case 1:
            Console.WriteLine("Znajdujesz sie przy drzwiach wyjsciowych serwerowni.");

            if (!karta)
                Console.WriteLine("Drzwi wymagaja karty dostepu.");

            if (!zasilanie)
                Console.WriteLine("Drzwi nie maja zasilania.");

            Console.WriteLine("Mozesz uzyc polecenia: wyjdz.");
            break;

        case 2:
            Console.WriteLine("To pokoj administratora.");

            if (!karta)
                Console.WriteLine("Na biurku znajduje sie karta dostepu.");
            else
                Console.WriteLine("Karta dostepu jest juz zabrana.");

            Console.WriteLine("Mozesz uzyc polecenia: karta.");
            break;

        case 3:
            Console.WriteLine("To magazyn techniczny.");

            if (!bezpiecznik)
                Console.WriteLine("Na polce znajduje sie bezpiecznik.");
            else
                Console.WriteLine("Bezpiecznik zostal juz zabrany.");

            Console.WriteLine("Mozesz uzyc polecenia: bezpiecznik.");
            break;

        case 4:
            Console.WriteLine("To pomieszczenie zasilania.");

            if (!zasilanie)
            {
                if (bezpiecznik)
                    Console.WriteLine("Mozesz naprawic zasilanie.");
                else
                    Console.WriteLine("Brakuje bezpiecznika do naprawy.");
            }
            else
            {
                Console.WriteLine("Zasilanie dziala poprawnie.");
            }

            Console.WriteLine("Mozesz uzyc polecenia: napraw.");
            break;

        default:
            Console.WriteLine("Nieznany pokoj.");
            break;
    }
}


// SEKCJA B: RUCH. Wlasciciel: osoba B.

void Idz(string kierunek)
{
    // B1: Nie pozwalamy poruszać się po zakończeniu gry.
    if (koniec)
    {
        Console.WriteLine("Gra jest zakonczona. Wpisz start, aby zagrac ponownie.");
        return;
    }

    // B2: Obliczamy następny pokój.
    int nastepnyPokoj = pokoj;

    if (kierunek == "lewo")
    {
        nastepnyPokoj--;
    }
    else if (kierunek == "prawo")
    {
        nastepnyPokoj++;
    }
    else
    {
        Console.WriteLine("Nieznany kierunek. Wpisz lewo albo prawo.");
        return;
    }

    // B3: Sprawdzamy granice mapy.
    if (nastepnyPokoj < 1 || nastepnyPokoj > 4)
    {
        Console.WriteLine("Nie mozesz tam przejsc. To koniec mapy.");
        return;
    }

    // B4: Zatwierdzamy ruch.
    pokoj = nastepnyPokoj;

    Console.WriteLine("Przechodzisz do pokoju " + pokoj + ".");

    Rozejrzyj();

    ZakonczTure();
}


// SEKCJA C: PRZEDMIOTY I WYGRANA. Wlasciciel: osoba C.

void Akcja(string co)
{
    // C1: Blokada akcji po zakończeniu gry.
    if (koniec)
    {
        Console.WriteLine("Gra jest zakonczona. Wpisz start, aby zagrac ponownie.");
        return;
    }

    // C2: Pobranie karty.
    if (co == "karta")
    {
        if (pokoj != 2)
        {
            Console.WriteLine("Karta znajduje sie w pokoju 2.");
            return;
        }

        if (karta)
        {
            Console.WriteLine("Masz juz karte dostepu.");
            return;
        }

        karta = true;
        Console.WriteLine("Zdobyles karte dostepu!");
    }

    // C2: Pobranie bezpiecznika.
    else if (co == "bezpiecznik")
    {
        if (pokoj != 3)
        {
            Console.WriteLine("Bezpiecznik znajduje sie w pokoju 3.");
            return;
        }

        if (bezpiecznik)
        {
            Console.WriteLine("Masz juz bezpiecznik.");
            return;
        }

        bezpiecznik = true;
        Console.WriteLine("Zdobyles bezpiecznik!");
    }

    // C2: Naprawa zasilania.
    else if (co == "napraw")
    {
        if (pokoj != 4)
        {
            Console.WriteLine("Zasilanie mozna naprawic w pokoju 4.");
            return;
        }

        if (!bezpiecznik)
        {
            Console.WriteLine("Nie masz bezpiecznika!");
            return;
        }

        if (zasilanie)
        {
            Console.WriteLine("Zasilanie jest juz naprawione.");
            return;
        }

        zasilanie = true;
        Console.WriteLine("Zasilanie zostalo naprawione!");
    }

    // C2: Otwarcie drzwi i wygrana.
    else if (co == "wyjdz")
    {
        if (pokoj != 1)
        {
            Console.WriteLine("Drzwi wyjsciowe znajduja sie w pokoju 1.");
            return;
        }

        if (!karta)
        {
            Console.WriteLine("Potrzebujesz karty dostepu!");
            return;
        }

        if (!zasilanie)
        {
            Console.WriteLine("Najpierw musisz naprawic zasilanie!");
            return;
        }

        wygrana = true;
        Console.WriteLine("Otwierasz drzwi serwerowni!");
    }

    else
    {
        Console.WriteLine("Nieznana akcja.");
        return;
    }

    // C4: Energia jest odejmowana tylko po udanej akcji.
    ZakonczTure();
}
