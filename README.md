# CSharp Collab Game - Ucieczka z serwerowni

- Praca odbywa się w zespołach dwu- albo trzyosobowych. 
- Każdy członek zespołu pracuje na:
    - swoim komputerze
    - swojej kopii repozytorium 
    - swoim branchu.
- Gracz wydaje polecenia przez konsolę.

## 1. Misja

Jest piątek, 16:59. W szkolnej serwerowni zgasło światło, drzwi się zablokowały, a administrator napisał: „u mnie działa”. Znajdźcie kartę i bezpiecznik, przywróćcie zasilanie i otwórzcie drzwi. Energia awaryjna wystarczy na 10 skutecznych czynności.

| Numer | Pomieszczenie | Cel |
|---|---|---|
| 1 | Recepcja | Znaleźć kartę |
| 2 | Magazyn | Zabrać bezpiecznik |
| 3 | Serwerownia | Zamontować bezpiecznik |
| 4 | Wyjście | Użyć karty przy działającym zasilaniu |

- Pokoje są połączone po kolei: 
    - w prawo numer rośnie o 1, 
    - w lewo maleje o 1. 
- Startujemy w pokoju 1. 
- Nie ma teleportacji.

## 2. Baza startowa (co jest) - zadania dla zespołu (co trzeba dopisać)

Baza startowa dla zespołu:

- uruchamialny projekt, 
- zmienne stanu, 
- odczytywanie komend, 
- główna pętla, 
- reset, 
- naliczanie energii,
- techniczny status.

**Zadania dla zespołu:** 

- wybór "osiołka" - lidera zespołu

- nazwy i opisy pomieszczeń, 

- pomoc, 

- mapa w pętli,

- ruch z kontrolą granic, 

- zbieranie przedmiotów, 

- naprawa,

- otwarcie drzwi. 

    

Każdy członek zespołu usuwa TODO ze swojej sekcji, dopisując działający kod. 

Samo skasowanie komentarza nie kończy zadania.

Starter kompiluje się i reaguje komunikatami TODO. Nie da się jeszcze wygrać. To zamierzone. Status działa od razu, aby każdy członek zespołu mógł sprawdzać własne funkcje bez czekania na innych.

## 3. Uruchomienie i wybór .NET

Wszyscy otwierają folder zawierający `CSharpCollabGame.csproj` w VS Code. Otwórz **Terminal → Nowy terminal**. Nie wpisuj komend gry do panelu Output ani Debug Console.

```text
dotnet --list-sdks
dotnet --list-runtimes
```

Projekt w paczce ma `net8.0`, ponieważ ćwiczenie używa składni dostępnej w .NET 8, 9 i 10. 

**Przed pierwszym wspólnym commitem cały zespół musi wybrać  tę samą wersję dostępną na wszystkich stanowiskach - NAJLEPIEJ by była to net10.0**

| Sprawdzony zestaw w pracowni | Linia w pliku `.csproj` |
|---|---|
| SDK i runtime 8 | `<TargetFramework>net8.0</TargetFramework>` |
| SDK i runtime 9 | `<TargetFramework>net9.0</TargetFramework>` |
| SDK i runtime 10 | `<TargetFramework>net10.0</TargetFramework>` |

Nie zakładaj, że samo SDK 10 oznacza dostępność runtime'u 8 i wszystkich pakietów potrzebnych do budowania net8.0 offline. 

"Osiołek" zmienia jedną linię, sprawdza build/run i publikuje uzgodniony plik. Pozostali go pobierają; nie tworzą różnych konfiguracji na swoich branchach. Wersja 8 w paczce jest bazą tego ćwiczenia, nie decyzją o wersji późniejszego kursu MAUI.

Uruchom:

```text
dotnet build
dotnet run
```

Powinien pojawić się tytuł, opis TODO i znak `>`. Wpisz:

```text
status
```

Oczekujesz: pokój 1, energia 10/10, wszystkie flagi False. Wpisz `koniec`, aby wrócić do terminala. Gdy aplikacja działa, `prawo` jest tekstem odczytywanym przez aplikację; po jej zamknięciu PowerShell/Git Bash potraktowałby je jako polecenie systemowe.

Nie uruchamiaj `dotnet new console` w tym folderze: projekt już istnieje. Nie dodawaj drugiego pliku z kopią kodu obok Program.cs - projekt mógłby próbować kompilować oba. Kopie do eksperymentów trzymaj poza folderem projektu albo w Git.

**Po zmianie kodu:** zapisz → zamknij grę komendą `koniec` lub Ctrl+C → `dotnet run` → wykonaj próbę. `start` resetuje rozgrywkę w uruchomionym programie, ale nie kompiluje zmienionych źródeł.

## 4. Komendy i reguły gry

Każdą komendę wpisujesz osobno i naciskasz Enter. W konsoli wpisujesz **`prawo`**, a nie `Idz("prawo")`, i **`karta`**, a nie `Akcja("karta")`. Konsola aplikacji nie wykonuje wpisanego kodu C#; nasz program rozpoznaje umówione słowa.

| Komenda | Znaczenie |
|---|---|
| `pomoc` | Polecenia i zasady |
| `status` | Dane rozgrywki |
| `mapa` | Cztery pokoje i jeden znacznik pozycji |
| `opis` | Rozejrzenie się w aktualnym pokoju |
| `lewo`, `prawo` | Ruch o jeden pokój |
| `karta` | Próba zabrania karty |
| `bezpiecznik` | Próba zabrania bezpiecznika |
| `napraw` | Próba naprawy zasilania |
| `wyjdz` | Próba otwarcia drzwi - czynność w grze |
| `start` | Nowa rozgrywka |
| `koniec` | Zamknięcie programu |

- Tylko udany ruch lub udana akcja kosztuje 1 energię.
- Informacje, pusty Enter, nieznana komenda, ściana, duplikat przedmiotu i odrzucona akcja są bezpłatne.
- Odczyt usuwa skrajne spacje i ignoruje wielkość liter. `  PRAWO  ` znaczy to samo co `prawo`. Nie usuwamy spacji wewnątrz słowa.
- Bezpiecznik po montażu znika z kieszeni; nie pojawia się ponownie na półce.
- Przy energii 0 przegrywamy, chyba że właśnie skutecznie otwarto wyjście. Sukces ma pierwszeństwo.
- Po wygranej/przegranej ruch i akcje niczego nie zmieniają. Informacje, start i koniec nadal działają.
- Flaga `koniec` oznacza koniec rozgrywki. Komenda `koniec` zamyka aplikację. To dwie różne rzeczy.

## 5. Repozytorium zespołu - krok po kroku

1. "Osiołek" klonuje repozytorium. W GitHubie kopiuje jego adres z **Code → SSH**, zgodnie z działającą szkolną konfiguracją. `ADRES_REPOZYTORIUM` poniżej należy zastąpić faktycznym adresem:

```bash
git clone ADRES_REPOZYTORIUM csharp-collab-game
cd csharp-collab-game
git branch --show-current
```

2. "Osiołek" uzgadnia TargetFramework, uruchamia starter i zapisuje bazę:

```bash
git status
git add Program.cs CSharpCollabGame.csproj README.md TESTY.md ZESPOL.md .gitignore .gitattributes
git diff --staged
git commit -m "Dodaj wspolny starter gry CSharp"
git push origin main
```

7. Dopiero teraz pozostali klonują **to samo** repozytorium; jeżeli już sklonowali pustą bazę, wykonują `git switch main` i `git pull --ff-only`. Każdy uruchamia `dotnet run` i sprawdza status.
8. Wypełnijcie ZESPOL.md: osoby, sekcje, nazwy branchy, lider (czyli "osiołek") i osoba sprawdzająca. "Osiołek" commituje ten plik na main i wypycha; pozostali pobierają. Dopiero potem tworzycie branche.

Źródła mogą być w waszym uzgodnionym katalogu repozytoriów. Jeśli pendrive dramatycznie spowalnia build, sklonujcie repo do osobnego, lokalnego katalogu SSD i pracujcie w nim. Konfiguracja klucza SSH może nadal wskazywać pendrive. Nie edytujcie dwóch lokalnych kopii równolegle i nie współdzielcie jednego katalogu roboczego między osobami. Nie commitujcie katalogów `bin` i `obj`.

## 6. Wspólny kontrakt

Nazwy funkcji i zmiennych są umową. Wielkość liter ma znaczenie: `Idz` i `idz` to różne nazwy. Nie zmieniajcie ich bez uzgodnienia.

| Dane | Typ i wartość startowa |
|---|---|
| `MAKS_ENERGIA` | `const int`, 10 |
| `pokoj` | `int`, 1 |
| `energia` | `int`, 10 |
| `karta`, `bezpiecznik`, `zasilanie` | `bool`, false |
| `koniec`, `wygrana` | `bool`, false |

| Funkcja | Kontrakt |
|---|---|
| `void Start()` | Gotowy reset wszystkich danych |
| `void ZakonczTure()` | Gotowy koszt 1 i sprawdzenie końca |
| `void Status()` | Gotowy podgląd danych dla testów |
| `string NazwaPokoju(int numer)` | Zwraca nazwę; nie zmienia stanu |
| `void Pomoc()` | Wypisuje komendy i zasady |
| `void Mapa()` | Pętla for, pokoje 1–4, pozycja |
| `void Rozejrzyj()` | Wypisuje opis zależny od stanu |
| `void Idz(string kierunek)` | Przyjmuje `"lewo"` lub `"prawo"`; waliduje ruch |
| `void Akcja(string co)` | Przyjmuje `"karta"`, `"bezpiecznik"`, `"napraw"`, `"wyjdz"` |

To funkcje lokalne w jednym pliku z instrukcjami najwyższego poziomu. Mogą odczytywać i zmieniać wspólne zmienne zadeklarowane wyżej. Nie dodawajcie im `static`, bo wtedy nie będą mogły przechwycić tych zmiennych. Nie opakowujcie startera dodatkową klasą ani Main. Nie deklarujcie ponownie `int energia` wewnątrz resetu. Stan i architekturę obiektową rozdzielimy w późniejszym kursie; to świadomie małe ćwiczenie proceduralne.

**Koszt nalicza wyłącznie ZakonczTure.** Nie odejmujcie energii dodatkowo w innych funkcjach. Wywołujcie ją dokładnie raz po udanej czynności i nigdy po odmowie.

## 7. Podział pracy

| Liczba osób | Osoba A | Osoba B | Osoba C |
|---|---|---|---|
| 3 | Sekcja A: nazwy, pomoc, mapa, opisy | Sekcja B: ruch; zapis prób i prowadzenie integracji | Sekcja C: akcje, przedmioty, wygrana |
| 2 | Sekcje A i B | Sekcja C oraz zapis prób |

Każdy implementuje kod i czyta fragment drugiej osoby. A ma więcej komunikatów, C więcej warunków. B po skończeniu ruchu przygotowuje przypadki brzegowe i przegląda C; nie modyfikuje równocześnie tej samej sekcji na swojej gałęzi. Przy wspólnej pomocy pisze autor gałęzi, partner proponuje i wyjaśnia.

Wszyscy edytujecie Program.cs, ale tylko swoją oznaczoną sekcję. Nie przeformatowujcie całego pliku ani nie zmieniajcie kolejności funkcji. Sekcja 0 jest gotowa. W ZESPOL.md przypiszcie również jednego autora TESTY.md, żeby nie produkować przypadkowych konfliktów dokumentacji.

## 8. Karta A - mapa i komunikaty

**A1. Nazwa pokoju.** Uzupełnij NazwaPokoju tak, aby 1–4 zwracały nazwy z tabeli. Dla innego numeru zwróć `"Nieznane pomieszczenie"`. Użyj if/else albo switch. Podpowiedź pierwszej gałęzi:

```csharp
if (numer == 1)
{
    return "Recepcja";
}
```

Nie wystarczy Console.WriteLine: wywołujący potrzebuje zwróconego napisu. Każda ścieżka funkcji zwracającej string ma zwracać string.

**A2. Pomoc.** Zastąp TODO czytelną listą wszystkich komend. Wyjaśnij różnicę między `wyjdz` i `koniec`. Informacje są bezpłatne.

**A3. Mapa.** Pętla for od 1 do 4, włącznie. Każdy wiersz: numer i NazwaPokoju(numer). Przy aktualnym pokoju dopisz `" <-- jestes tutaj"`. Licznik pętli musi być nową zmienną `numer`, nie wspólnym `pokoj`.

```csharp
for (int numer = 1; numer <= 4; numer = numer + 1)
{
    // Zbuduj jeden wiersz i wyświetl go.
}
```

**A4. Opis.** Recepcja: karta jest na biurku tylko przed zabraniem. Magazyn: bezpiecznik jest na półce tylko, gdy nie ma go w kieszeni i nie jest zamontowany. Serwerownia: opis zależy od zasilania. Wyjście: przypomnij kartę i prąd. Użyj znanych ifów, nie potrzebujesz tablic.

**Próba samodzielna na twoim branchu:** `mapa`, `opis`, `pomoc`, `status`. Mapa ma cztery wiersze i jeden znacznik przy pokoju 1. Energia pozostaje 10. Opisy innych pokoi sprawdzicie po integracji ruchu; teraz można na chwilę dodać przed Start() wypisanie NazwaPokoju(3) i NazwaPokoju(99), sprawdzić i usunąć diagnostykę przed commitem. Nie jest to komenda wpisywana w konsoli gry.

## 9. Karta B - ruch

**B1. Koniec rozgrywki.** Jeśli `koniec` jest true, wypisz komunikat i wykonaj `return`. Reszta funkcji ma się nie wykonać.

**B2. Planowany pokój.** Utwórz lokalną zmienną `int nastepnyPokoj = pokoj;`. Dla kierunku prawo zwiększ ją o 1, dla lewo zmniejsz. Inny kierunek oznacza komunikat i return.

**B3. Granice.** Odrzuć kandydat mniejszy od 1 lub większy od 4. Sprawdź go, zanim zmienisz `pokoj`. Ruch w ścianę jest bezpłatny.

**B4. Zatwierdzenie.** Przypisz kandydat do `pokoj`, wywołaj Rozejrzyj i dokładnie raz ZakonczTure.

**Próba na własnym branchu:** wpisuj kolejno:

```text
start
lewo
status
prawo
status
```

Po lewo: pokój 1, energia 10. Po prawo: pokój 2, energia 9. Opis może jeszcze mówić TODO — status działa niezależnie. Komenda `gora` sprawdza odmowę w rozdzielaczu, nie wewnętrzną walidację Idz. Wewnętrzny nieznany kierunek można sprawdzić tymczasowym wywołaniem `Idz("gora");` w kodzie tuż po Start(); przed commitem usuń je.

**B5. Przygotuj odbiór:** zapisz w TESTY.md próby obu ścian i kosztów. Gdy podstawowy ruch działa, sprawdź cudzą sekcję C. Opcjonalny pasek energii piszesz dopiero po ukończeniu podstawy zespołu.

## 10. Karta C - akcje i wygrana

**C1. Blokada:** po zakończeniu rozgrywki komunikat i return.

**C2. Wybierz gałąź według `co`:** if / else if / else wystarczy. Switch jest dopuszczalny, jeśli go znasz.

| Akcja | Warunki sukcesu | Zmiana |
|---|---|---|
| karta | Pokój 1, brak karty | karta = true |
| bezpiecznik | Pokój 2, brak bezpiecznika w kieszeni, brak zasilania | bezpiecznik = true |
| napraw | Pokój 3, bezpiecznik w kieszeni, brak zasilania | bezpiecznik = false; zasilanie = true |
| wyjdz | Pokój 4, karta i zasilanie | wygrana = true; koniec = true |

**C3. Najpierw warunki, potem zmiany.** Jedna gałąź jako wzór:

```csharp
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
// Dalej dopisz else if dla pozostałych akcji.
```

`||` oznacza LUB: odmawiamy, gdy jesteśmy w złym pokoju LUB karta już została zabrana. Możesz zamiast złożonego warunku napisać dwa osobne ify z return. Ważne jest zachowanie, nie najkrótszy zapis.

**C4. Wspólny koszt.** Po całym łańcuchu gałęzi wywołaj raz ZakonczTure. Nieznana akcja również ma kończyć się przez return. Wszystkie odmowy muszą wyjść przed naliczeniem kosztu. W gałęzi wyjdz ustaw wygraną przed rozliczeniem, aby sukces ostatnią energią był dozwolony.

**C5. Próba na twoim branchu:** `start`, `karta`, `status`, `karta`, `status`, `napraw`, `status`. Karta kosztuje raz: energia 9, karta True. Kolejne odmowy nie zmieniają danych. Do pełnego testu potrzebny jest już ruch z gałęzi B: po jego włączeniu do main pobierz main do swojej gałęzi według punktu 11. Nie pisz drugiego ruchu u siebie i nie czekaj bezczynnie na mapę.

## 11. Branche, przegląd i integracja

Przykład dla osoby B, w Git Bash, po zamknięciu gry:

```bash
git switch main
git pull --ff-only
git switch -c feature/ruch
```

Osoba A używa `feature/mapa`, C `feature/akcje`. W parze: `feature/mapa-ruch` i `feature/akcje`.

Po małej działającej zmianie:

```bash
git status
git diff
dotnet build
dotnet run
```

W grze przeprowadź próbę, zakończ przez `koniec`, a następnie:

```bash
git add Program.cs
git diff --staged
git commit -m "Dodaj ruch z kontrola granic"
git push -u origin feature/ruch
```

Dodaj TESTY.md tylko jeśli zgodnie z umową to ty zapisujesz próby. `git diff --staged` ma pokazać własną sekcję i celowe zmiany, nie cały przeformatowany plik.

**Review:** kolega czyta zmianę i odpowiada: jakie są wejścia, kiedy funkcja odmawia, gdzie nalicza koszt, jaki przypadek graniczny sprawdzono? Autor w razie potrzeby poprawia, commituje i ponownie pushuje. Możecie użyć PR na GitHubie do rozmowy, ale w tej instrukcji merge wykonuje jeden integrator w terminalu. Nie łączcie tej samej gałęzi równocześnie przyciskiem na GitHubie i lokalnie.

**Integrator** zaczyna od czystego katalogu (`git status`), własne zmiany ma zapisane w commicie. Scala ukończone gałęzie po jednej. Dobry porządek: ruch → akcje → mapa; można zacząć od mapy, gdy jest już gotowa. Przykład:

```bash
git switch main
git pull --ff-only
git fetch origin
git merge --no-edit origin/feature/ruch
dotnet build
dotnet run
```

Po sprawdzeniu i zamknięciu gry:

```bash
git push origin main
```

Teraz osoba C może pobrać gotowy ruch do swojego brancha:

```bash
git switch feature/akcje
git status
git fetch origin
git merge --no-edit origin/main
```

Przed merge commituj własną rozpoczętą pracę. Po merge sprawdź grę i kontynuuj akcje. Nie zmieniaj bez potrzeby sekcji B. Po ukończeniu wypchnij swój branch. Integrator analogicznie scala `origin/feature/akcje`, a potem `origin/feature/mapa`, **po każdym merge osobno budując i uruchamiając grę**. W parze używa rzeczywistej nazwy `feature/mapa-ruch`.

Tylko wyznaczony integrator wypycha zmiany na main. Gdy push zostanie odrzucony, sprawdź historię; nie używaj force-push. Gdy `pull --ff-only` odmawia, lokalny i zdalny main wymagają wyjaśnienia, nie resetowania w ciemno.

**Jeżeli wystąpi konflikt:**

1. Sprawdź `git status` i odszukaj znaczniki `<<<<<<<`, `=======`, `>>>>>>>`.
2. Przeczytaj obie wersje razem z autorem. Zachowaj potrzebne zmiany obu stron i usuń znaczniki.
3. Zapisz plik. Wykonaj `dotnet build` i właściwą próbę gry.
4. Wykonaj `git add Program.cs`, a potem `git commit -m "Polacz zmiany po rozwiazaniu konfliktu"`.
5. Dopiero po zakończeniu tego merge podejmij następny. Jeśli trzeba wrócić do stanu przed rozpoczętym scalaniem, poproś nauczyciela o pomoc z `git merge --abort`; nie rób `reset --hard`.

Konflikt nie jest warunkiem zaliczenia. Poprawne połączenie niezależnych zmian też jest współpracą.

## 12. Odbiór

Wypełnijcie TESTY.md konkretnymi komendami i wynikami. Każdą niezależną próbę zaczynajcie od `start`. Status jest odczytem, nie oszustwem; ręczne zmienianie energii w kodzie nie jest legalnym ruchem w podstawowej próbie.

Po integracji wszyscy przechodzą na main i wykonują `git pull --ff-only`. Każdy uruchamia tę samą wersję. Zamieńcie się stanowiskami z drugim zespołem, zagrajcie bez czytania kodu i zgłoście jeden odtwarzalny problem lub czytelny element interfejsu.

**Oddajecie:** źródła na main, wkład każdej osoby w historii, uzupełnione ZESPOL.md i TESTY.md, działającą wygraną/przegraną/reset. Każdy wyjaśnia jedną funkcję kolegi i samodzielnie wykonuje drobną zmianę wskazaną przez nauczyciela. Liczba commitów nie jest konkursem.

## 13. Dodatki - najwyżej jeden po MVP

- Pasek energii: nowa funkcja `string PasekEnergii()` z pętlą for tworzy np. `###.......` dla energii 3/10. Zwraca tekst, nie zmienia energii. Jedna uzgodniona osoba dodaje jej wywołanie w Status.
- Licznik ruchów: reset do zera, wzrost tylko po poprawnym ruchu. Wymaga uzgodnienia wspólnej zmiennej i zmian w Start/Status.
- Własny klimat: statek, loch, szkoła po godzinach. Zachowajcie kontrakt mechaniki, zmieńcie opisy.

Nie dokładamy grafiki, losowych przeciwników, sieci, bazy, list ekwipunku ani klas postaci, zanim działa podstawowa gra.

### Ostrzeżenie w nieuzupełnionym starterze

Jeśli zobaczysz CS8321 przy `ZakonczTure`, to informacja, że funkcja nie jest jeszcze używana. Jej wywołanie dopiszecie w częściach B i C. To ostrzeżenie nie blokuje uruchomienia.
