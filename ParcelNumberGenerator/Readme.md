# Generator numerow paczek

Generowanie numerów paczek z puli z uwzględnieniem numerów wyłączonych.

# Instrukcja użytkownia

Na swoim serwerze należy wygenerować tabele za pomocą skryptu i przekazać ConnectionString w aplikacji

## Struktura projektu

Aplikacja konsolowa składa się z folderów klas bazowych i rozszerzeń z proponowanymi implementacjami. 

### NumberPoolDatabase

Implementacja z użyciem wzorca Chain of Responsibility

### NumberPoolDB

Implementacja przestarzała.

### NumberPoolDBv2

Główna implementacja, najbardziej zoptymalizowana pod względem kodu C#.

### NumberPoolDBFunc

Implementacja z wyodrębnieniem funkcji dla poszczególnych warunków algorytmu.

### NumberPoolDBv2WithRangeOff

Rozszerzenie NumberPoolDBv2 o możliwość dodania zakresu wyłączonego z losowania.
