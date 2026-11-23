# PRZEKAZANIE.md — Collab 03

## 1. Gałąź, commit i tag

- Gałąź główna: `[master / main]`
- Gałąź robocza / ostatni commit: `[np. feature/c03-integracja — <skrót_hasha_commita>]`
- Tag rdzenia (po odbiorze regresji przez integratora): `collab-03-core-v1` `[lub kolejna wersja, jeśli tag już istniał]`

## 2. Uruchomienie projektu

- Katalog roboczy: katalog projektu zawierający plik `.csproj` (bez drugiego `Main` i bez projektów próbnych w katalogu gry).
- Kompilacja i uruchomienie:
  ```bash
  dotnet build
  dotnet run
  ```
