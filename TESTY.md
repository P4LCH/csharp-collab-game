# Próby odbiorowe

Każdą próbę zaczynamy od `start`. Po sekwencji wpisujemy `status`. Komendy z tabeli wpisujemy osobno, każdą zatwierdzając Enterem. Oczekiwania odnoszą się do limitu energii 10 (`MAKS_ENERGIA = 10`).

| Nr  | Próba i komendy po `start`                                                | Oczekiwane                                                                                | Otrzymane / osoba                                                                               |
| --- | ------------------------------------------------------------------------- | ----------------------------------------------------------------------------------------- | ----------------------------------------------------------------------------------------------- |
| 1   | `mapa`, `pomoc`, `opis`, `status`                                         | 4 pokoje, 1 znacznik przy recepcji; energia 10                                            | Działa poprawnie, Cichecki (4 pokoje, znacznik przy 1. Recepcja, energia 10/10)                 |
| 2   | `lewo`, `gora`, pusty Enter                                               | Pokój 1, energia 10; odmowa i puste wejście bez kosztu                                    | Działa poprawnie, Cichecki (brak zmiany pokoju, energia 10/10)                                  |
| 3   | `prawo`                                                                   | Pokój 2, energia 9                                                                        | Pokój 2 (Magazyn), energia 9/10; wpis ruchu do pokoju 2                                         |
| 4   | `prawo`, `prawo`, `prawo`, `prawo`                                        | Pokój 4, energia 7; czwarty ruch odrzucony                                                | Pokój 4 (Wyjscie), energia 7/10; 3 ruchy zapisane, 4. odrzucony bez kosztu                      |
| 5   | `karta`, `karta`                                                          | `Karta: True`, energia 9; druga próba odrzucona                                           | Działa poprawnie, Cichecki (`Karta: True`, energia 9/10, 1 wpis zabrania karty)                 |
| 6   | `bezpiecznik`                                                             | `Bezpiecznik: False`, energia 10 (w pokoju 1 brak bezpiecznika)                           | Działa poprawnie, Cichecki (`Bezpiecznik: False`, energia 10/10)                                |
| 7   | `prawo`, `prawo`, `napraw`                                                | Pokój 3, energia 8, `Zasilanie: False` (brak bezpiecznika)                                | Pokój 3 (Serwerownia), energia 8/10, `Zasilanie: False`, `napraw` odrzucone                     |
| 8   | `karta`, `prawo`, `prawo`, `prawo`, `wyjdz`                               | Brak zasilania; pokój 4, energia 6, `Koniec: False`, `Wygrana: False`                     | Pokój 4, energia 6/10, `Koniec: False`, `Wygrana: False`, `wyjdz` odrzucone                     |
| 9   | `prawo`, `bezpiecznik`, `prawo`, `napraw`, `prawo`, `wyjdz`               | Brak karty; pokój 4, energia 5, `Koniec: False`, `Wygrana: False`                         | Pokój 4, energia 5/10, `Koniec: False`, `Wygrana: False`, `wyjdz` odrzucone                     |
| 10  | `karta`, `prawo`, `bezpiecznik`, `prawo`, `napraw`, `lewo`, `bezpiecznik` | Energia 4, `Zasilanie: True`, `Bezpiecznik: False`; bezpiecznik nie odradza się           | Pokój 2, energia 4/10, `Zasilanie: True`, `Bezpiecznik: False`, ponowne `bezpiecznik` odrzucone |
| 11  | `karta`, `prawo`, `bezpiecznik`, `prawo`, `napraw`, `prawo`, `wyjdz`      | `Wygrana: True`, `Koniec: True`, bez jednoczesnej przegranej (energia 3)                  | Pokój 4, energia 3/10, `Wygrana: True`, `Koniec: True` (komunikat wygranej)                     |
| 12  | Pięć razy para `prawo`, `lewo` (10 ruchów)                                | Pokój 1, energia 0, `Koniec: True`, `Wygrana: False`                                      | Pokój 1, energia 0/10, `Koniec: True`, `Wygrana: False` (komunikat przegranej)                  |
| 13  | Po próbie 12: `prawo`, `karta`, `status`                                  | Stan nie zmienia się; informacje (`status`, `mapa`, `opis`, `pomoc`, `dziennik`) działają | `prawo` i `karta` odrzucone po końcu gry; energia nadal 0/10, `status` i `dziennik` wyświetlone |
| 14  | Po końcu gry: `start`, `status`                                           | Pokój 1, energia 10, wszystkie flagi `False`, dziennik pusty                              | Działa poprawnie, Cichecki (pokój 1, energia 10/10, flagi `False`, dziennik pusty)              |
| 15  | `"   PRAWO   "` (spacje + wielkie litery)                                 | Pokój 2, energia 9; normalizacja `Trim().ToLowerInvariant()` działa                       | Pokój 2 (Magazyn), energia 9/10; wpis ruchu do pokoju 2                                         |
| 16  | `koniec`                                                                  | Aplikacja wraca do terminala                                                              | Działa poprawnie. Cichecki (komunikat pożegnania i powrót do powłoki)                           |

## Nasza droga zwycięska

Sekwencja od `start`:

1. `karta` (pokój 1 -> zabranie karty, energia 9)
2. `prawo` (pokój 1 -> pokój 2, energia 8)
3. `bezpiecznik` (pokój 2 -> zabranie bezpiecznika, energia 7)
4. `prawo` (pokój 2 -> pokój 3, energia 6)
5. `napraw` (pokój 3 -> naprawa zasilania, energia 5)
6. `prawo` (pokój 3 -> pokój 4, energia 4)
7. `wyjdz` (pokój 4 -> wygrana, `Koniec: True`, `Wygrana: True`, energia 3)

## Znaleziony błąd

- Wersja/commit: `collab-03-core-v1` (stan przed migracją do `DziennikWyprawy`)
- Kroki od startu: `start` -> sprawdzenie obsługi odkryć i historii trzech ostatnich udanych czynności
- Oczekiwane: odkrycia pokoi i 3 ostatnie udane wpisy przechowywane w obiekcie `DziennikWyprawy` (w osobnym pliku `DziennikWyprawy.cs`), bez duplikowania stanu odkryć ani licznika w `Program.cs`
- Rzeczywiste: w bazowym `Program.cs` brakowało klasy `DziennikWyprawy`, integracji `CzyOdwiedzono`/`Odwiedz`/`LiczbaOdwiedzonych`, zapisu wpisów udanych akcji przed `ZakonczTure()` oraz komendy `dziennik`
- Poprawka i ponowna próba: utworzono `DziennikWyprawy.cs`, podłączono jedną instancję przed `Start()`, dodano `Reset()` w `Start()`, zapis wpisów w `Idz` i `Akcja` przed `ZakonczTure()` oraz komendę `dziennik`; ponowne uruchomienie prób potwierdziło zgodność z regułami

---

## Próby Collab 03 (T3.1–T3.12)

| ID    | Stan początkowy / działania                                                                                                              | Oczekiwany wynik                                                                                                | Wynik rzeczywisty / osoba |
| ----- | ---------------------------------------------------------------------------------------------------------------------------------------- | --------------------------------------------------------------------------------------------------------------- | ------------------------- |
| T3.1  | `start`, `dziennik`, `odkrycia`, `status`                                                                                                | Dziennik pusty; odkrycia `1/4`; energia `10`                                                                    | …                         |
| T3.2  | `start`, `karta`, `karta`, `lewo`, `mapa`, `dziennik`                                                                                    | 1 wpis (zabranie karty); odmowy i `mapa` nic nie dopisują; energia `9`                                          | …                         |
| T3.3  | `start`, `karta`, `prawo`, `bezpiecznik`, `prawo`, `dziennik`                                                                            | 3 wpisy: ruch do 2, zabranie bezpiecznika, ruch do 3 (karta wypadła); energia `6`                               | …                         |
| T3.4  | Po T3.3: `start`, `dziennik`, `odkrycia`, potem `prawo`, `dziennik`                                                                      | Po `start`: dziennik pusty i tylko pokój 1; po `prawo` tylko nowy wpis ruchu do 2                               | …                         |
| T3.5  | `start`, 5x (`prawo`, `lewo`), potem `karta`, `prawo`, `dziennik`, `status`                                                              | Energia `0`, przegrana; ostatnie 3 wpisy: lewo/prawo/lewo; odmowy po końcu nie zmieniają dziennika              | …                         |
| T3.6  | `start`, `karta`, `prawo`, `bezpiecznik`, `prawo`, `napraw`, `prawo`, `wyjdz`, `dziennik`                                                | Wygrana, energia `3`; ostatnie 3 wpisy: naprawa, ruch do 4, udane wyjście                                       | …                         |
| T3.7  | Osobny projekt prób: `a = new DziennikWyprawy()`, `b = new DziennikWyprawy()`, `a.Odwiedz(2)`, `a.DodajWpis("A")`, potem `a.Reset()`     | `a`: przed resetem 2 odkrycia i 1 wpis, po resecie 1 odkrycie i 0 wpisów; `b` nadal 1 odkrycie i 0 wpisów       | …                         |
| T3.8  | Osobny obiekt: `DodajWpis(null)`, `DodajWpis("")`, `DodajWpis("   ")`, `DodajWpis("  A  ")`, potem znów `null`, `""`, `"   "`            | Pierwsze trzy nic nie zapisują; potem 1 wpis `"A"`; ponowne puste wywołania nic nie zmieniają                   | …                         |
| T3.9  | Osobny obiekt: `Odwiedz(0)`, `Odwiedz(5)`, `CzyOdwiedzono(0)`, `CzyOdwiedzono(5)`, `PobierzWpis(-1)`, `PobierzWpis(0)`, `PobierzWpis(3)` | Odkrycia `1`; `CzyOdwiedzono` zwraca `false`; `PobierzWpis` zwraca `""`; bez wyjątków                           | …                         |
| T3.10 | Osobny obiekt: `A`, `B`, `C`, `D`, `E` -> odczyt -> `Reset()` -> `X` -> odczyt, potem znów `null`/`""`/`"   "`                           | Najpierw `C, D, E`; po `Reset()` i `X` tylko `X` (`LiczbaWpisow == 1`), brak śladów A–E; puste po `X` bez zmian | …                         |
| T3.11 | `start`, `"  DZIENNIK  "`, `dziennik`                                                                                                    | Dwa identyczne komunikaty pustego dziennika; energia `10`                                                       | …                         |
| T3.12 | Regresja T1–T16 + próba wyjścia przy energii `1` (pokój 4, energia 1, karta i zasilanie aktywne -> `wyjdz`)                              | Regresja zgodna z wynikiem 10; przy `wyjdz` z energii 1: wygrana, energia `0`, wpis o wyjściu                   | …                         |
