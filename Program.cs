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
    // TODO A1: zwroc nazwe dla 1, 2, 3, 4 oraz wartosc dla nieznanego numeru.ZROBIONE; DO TESTOW
    if(numer == 1)
    {
        return "Recepcja";
    }
     
    if(numer == 2)
    {
        return "Magazyn";
    }
    if(numer == 3)
    {
        return "Serwerownia";
    }
    if(numer == 4)
    {
        return "Wyjscie";
    }
    else
    {
        return "Dalej nie możewsz iść";
    }
    
}

void Pomoc()
{
    // TODO A2: wypisz wszystkie komendy oraz koszt czynnosci. ZROBIONE; DO TESTÓW
    Console.WriteLine("pomoc - wypisuje komendy wykorzystywane w grze,");
    Console.WriteLine("status - Dane rozgrywki,");
    Console.WriteLine("mapa - Cztery pokoje i jeden znacznik pozycji,");
    Console.WriteLine("opis - rozejrzenie się w aktualnym pokoju,");
    Console.WriteLine("lewo/prawo- Ruch o jeden pokój( w lewo cofasz się w prawo idziesz do przodu)");
    Console.WriteLine("karta - Próba zabrania karty,");
    Console.WriteLine("bezpiecznik- próba zabrania bezpiecznika ,");
    Console.WriteLine("napraw - próba naprawy zasilania ,");
    Console.WriteLine("wyjdz - próba otwarcia drzwi(czynność w grze, nie zamyka gry),");
    Console.WriteLine("start - nowa rozgrywka(reset do początku), ");
    Console.WriteLine("koniec-Zamknięcie gry.");
}

void Mapa()
{
    // TODO A3: petla for, numery 1..4, jeden znacznik aktualnego pokoju.ZROBIONE; DO TESTOW
    // Console.WriteLine("TODO A: mapa.");
    for(int numer =1; numer<=4; numer = numer + 1)
    {
        string wiersz = numer + ". " + NazwaPokoju(numer);
        if (numer == pokoj)
        {
            wiersz = wiersz + " <--- jesteś tutaj";

        }
        Console.WriteLine(wiersz);
    }
}

void Rozejrzyj()
{
    // Console.WriteLine("Jestes w: " + NazwaPokoju(pokoj));
    // TODO A4: opis zalezy od pokoju oraz obecnosci przedmiotow/zasilania. ZROBIONE.DO TESTOW
    if(pokoj == 1)
    {
        if (!karta)
        {
            Console.WriteLine("Na biurku leży karta");
        }
        else
        {
            Console.WriteLine("Biurko jest puste. Kartę masz już w kieszeni");
        }

    }
    else if (pokoj == 2)
    {
        if (!bezpiecznik && !zasilanie)
        {
            Console.WriteLine("Na półce leży bezpiecznik");
        }
        else
        {
            Console.WriteLine("Półka jest pusta");
        }

    }
    else if (pokoj == 3)
    {
        if (!zasilanie)
        {
            Console.WriteLine("Ciemno. Serwery milczą. Brakuje bezpiecznika w skrzynce.");
        }
        else
        {
            Console.WriteLine("Serwery szumia, świecą diody. Zasilanie działa");
        }
    }
    else if(pokoj == 4)
    {
        Console.WriteLine("Zablokowane drzwi z czytnikiem kart.");
        if (karta)
        {
            Console.WriteLine("Masz Kartę");

        }
        else
        {
            Console.WriteLine("Brakuje ci karty");

        }
        if (zasilanie)
        {
            Console.WriteLine("Czytnik Ma prąd");
        }
        else
        {
            Console.WriteLine("Czytnik nie ma prądu");
        }
    }
    // Console.WriteLine("TODO A: opis pokoju.");
}

// SEKCJA B: RUCH. Wlasciciel: osoba B.
void Idz(string kierunek)
{
    // TODO B1: odrzuc ruch po zakonczeniu gry (return).
    if(koniec == true){
        Console.WriteLine("Program sie skonczyl!!!");
        return;
    }
    // TODO B2: policz nastepnyPokoj, nie zmieniaj jeszcze wspolnego pokoj.
    int nastepnyPokoj = pokoj;
    if(kierunek == "prawo"){
        nastepnyPokoj++;
    } else if(kierunek == "lewo"){
        nastepnyPokoj--;
    }else{
        Console.WriteLine("Zly kierunek");
        return;
    }
    // TODO B3: odrzuc nieznany kierunek i ruch poza pokoje 1..4.
    if(nastepnyPokoj < 1 || nastepnyPokoj > 4){
        Console.WriteLine("Nie mozna przejsc do pokoju");
        return;
    }
    // TODO B4: zatwierdz pokoj, wywolaj Rozejrzyj i raz ZakonczTure.
    pokoj = nastepnyPokoj;
    Rozejrzyj();
    ZakonczTure();
    Console.WriteLine("TODO B: ruch " + kierunek);
}

// SEKCJA C: PRZEDMIOTY I WYGRANA. Wlasciciel: osoba C.
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
    }else if(co == "bezpiecznik")
    {
        if (pokoj != 2 || bezpiecznik || zasilanie)
        {
            Console.WriteLine("Tutaj nie ma bezpiecznika do zabrania.");
            return;
        }
        bezpiecznik = true;
        Console.WriteLine("Zabierasz bezpiecznik.");
    }else if(co == "napraw")
    {
        if (pokoj != 3 || !bezpiecznik || zasilanie )
        {
            Console.WriteLine("Tutaj nie możesz naprawić zasilania.");
            return;
        }
        zasilanie = true;
        bezpiecznik = false;
        Console.WriteLine("Naprawiasz zasilanie.");
    }else if(co == "wyjdz")
    {
        if (pokoj !=4 || !karta || !zasilanie)
        {
            Console.WriteLine("Nie możesz wyjść.");
            return;
        }
        wygrana = true;
        koniec = true;
        Console.WriteLine("Wygrywasz. Koniec gry.");
    } else
    {
        Console.WriteLine("Nieznana akcja.");
        return;
    }

    ZakonczTure();
}
