using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ParcelNumberGenerator
{
    /// <summary>
    /// Pula numerow uzywajaca bazy danych
    /// </summary>
    [Obsolete("Error: This class is deprecated. Use NumberPoolDBv2 instead of this.", true)]
    public class NumberPoolDB : INumberPoolGenerator
    {
        /// <summary>
        /// Zakres puli - mozna przypisac tylko podczas tworzenia obiektu puli
        /// </summary>
        private readonly Tuple<int, int> _numberPoolRange;
        /// <summary>
        /// ConnectionString do bazy
        /// </summary>
        private readonly string _connectionString;
        /// <summary>
        /// Nazwa tabeli w bazie danych z ktorej pobieramy
        /// </summary>
        private readonly string _tableName;
        /// <summary>
        /// Nazwa kolumny w bazie danych przechowujacej numery juz wylosowane.
        /// </summary>
        private readonly string _columnName;
        /// <summary>
        /// Konstruktor domyslny - nadaje domyslne wartosci do pol tylko do odczytu
        /// </summary>
        public NumberPoolDB()
        {
            _numberPoolRange = new Tuple<int, int>(1, 10000000);
            //Domyslnie serwer zainstalowany jest na lokalnej maszynie
            _connectionString =
                "Integrated Security=SSPI;Initial Catalog=ParcelNumberGenerator;Data Source=localhost\\SQLEXPRESS;";
            _tableName = "USED_NUMBERS";
            _columnName = "usedNumber";
        }
        /// <summary>
        /// Konstruktor przeciazony nadaje wartosci recznie
        /// </summary>
        /// <param name="numberPoolRange">Zakres calej puli</param>
        /// <param name="connectionString">ConnectionString do bazy danych</param>
        /// <param name="tableName">Nazwa tabeli w bazie danych z ktorej pobierane sa numery juz wylosowane</param>
        /// <param name="columnName">Nazwa kolumny w bazie danych przechowujacej numery juz wylosowane</param>
        public NumberPoolDB(Tuple<int, int> numberPoolRange, string connectionString, string tableName = "USED_NUMBERS", string columnName = "usedNumber")
        {
            _numberPoolRange = numberPoolRange;
            _connectionString = connectionString;
            _tableName = tableName;
            _columnName = columnName;
        }
        /// <summary>
        /// Wyszukiwanie binarne
        /// </summary>
        /// <param name="range">Zakres</param>
        /// <param name="searchElement">Element szukany</param>
        /// <returns>Indeks</returns>
        private int BinarySearch(Tuple<int, int> range, int searchElement)
        {
            int leftIndex = GetDataFromDB($@"Select Count(*) From
                        (
                        SELECT ROW_NUMBER() OVER(ORDER BY {_columnName} ASC) As Row, {_columnName}
                        from {_tableName}
                        where {_columnName} <= {range.Item2}
                        ) as subquery;
                        ") - 1; //od numeru wiersza odejmujemy 1 zeby otrzymac index

            int rightIndex = GetDataFromDB($@"Select Count(*) From
                        (
                        SELECT ROW_NUMBER() OVER(ORDER BY {_columnName} ASC) As Row, {_columnName}
                        from {_tableName}
                        where {_columnName} <= {range.Item2}
                        ) as subquery;
                        ") - 1;

            while (leftIndex < rightIndex)
            {
                int center = (leftIndex + rightIndex) / 2;

                int centerNumber = GetDataFromDB($@"       
                    SELECT {_columnName} FROM
                        (SELECT ROW_NUMBER() 
                            OVER (ORDER BY {_columnName}) AS Row,
		                    {_columnName}
                        FROM {_tableName}) as {_columnName}
                    WHERE Row = {center}
                    ");

                if (centerNumber < searchElement)
                    leftIndex = center + 1;
                else
                    rightIndex = center;
            }

            int leftNumber = GetDataFromDB($@"       
                    SELECT {_columnName} FROM
                        (SELECT ROW_NUMBER() 
                            OVER (ORDER BY {_columnName}) AS Row,
		                    {_columnName}
                        FROM {_tableName}) as {_columnName}
                    WHERE Row = {leftIndex}
                    ");
            
            return leftNumber == searchElement ? leftIndex - 1 : -1;
        }
        /// <summary>
        /// Generuje losowy numer paczki z puli i zapisuje go na baze
        /// </summary>
        /// <returns>numer paczki</returns>
        public int Generate()
        {
            int generatedNumber = -1;//poczatkowa wartosc numeru wylosowanego

            generatedNumber = ProcessTemp2(_numberPoolRange);
            if (generatedNumber == -1) //jesli Process nie zwrocil wyjatku to teraz go rzucamy, zeby nie zapisal nic na baze
                throw new Exception("Pula pusta. Brak wolnych numerow. Nie mozna dalej losowac z tej puli.");
            //zapisuje nowy numer paczki na baze
            GetDataFromDB($@"insert into {_tableName}({_columnName}) values({generatedNumber});");

            return generatedNumber;
        }
        private Mode _mode;
        public Mode Mode
        {
            get { return _mode; }
            set { _mode = value; }
        }
        /// <summary>
        /// sprawdza czy w zakresie nie ma przydzielonych numerow i losuje numer z takiego zakresu
        /// jezeli w zakresie sa przydzielone numery, to rekurencyjnie szuka podzakresow w ktorych numerow przydzielonych nie ma i losuje az do wyczerpania puli
        /// </summary>
        /// <param name="range">zakres puli w danej rekurencji</param>
        /// <returns>wylosowany numer</returns>
        protected virtual int Process(Tuple<int, int> range)
        {
            int generatedNumber = -1; // poczatkowa wartosc numeru wylosowanego dla danej rekurencji
            //int elementInRange = ElementsInRange(range); // sprawdzenie ile elementow przetworzonych znajduje sie w zakresie puli danej rekurencji
            int elementInRange = ElementsInRangeTemp(range);
            int numberPoolCount = (range.Item2 - range.Item1) + 1;//liczebnosc puli

            if (elementInRange < 0 || elementInRange > numberPoolCount)//jesli liczebnosc elementow przetworzonych w zakresie jest mniejsza od zera lub wieksza od zakresu puli to rzucamy wyjatek
            {
                throw new Exception("Something went wrong with database");
            }
            else if (elementInRange == 0) // jezeli w zakresie nie wystepuja elementy przetworzone to losujemy liczbe z zakresu
            {
                generatedNumber = StockNumberFromSortedRange(range);
            }
            else if (elementInRange == numberPoolCount) // jezeli ilosc elementow przetworzonych w zakresie jest rowna liczebnosci zakresu to zwracamy -1, co oznacza ze ze zbioru nie da sie losowac
            {
                //z tego zbioru juz nie mozna losowac
                generatedNumber = -1;
            }
            else if (elementInRange > 0 && elementInRange < numberPoolCount)//jesli liczebnosc elementow przetworzonych w zakresie jest wieksza od zera i mniejsza od zakresu puli to rekurencyjnie przeszukujemy podzakresy
            {
                List<Tuple<int, int>> newRanges = DivideRange(range, elementInRange + 1); // dodajemy 1 poniewaz jak mamy jeden numer w zakresie, to musimy dzielic na dwa podzakresy
                if (newRanges.Count == 0)//jezeli z powyzszego nie otrzymalismy podzakresow, bo nie bylo dzielenia, to przypisujemy ostatni podzakres jako domyslny
                    newRanges.Add(range);
                List<Tuple<int, int>> processedRanges = new List<Tuple<int, int>>();// lista przeprocesowanych podzakresow
                do
                {
                    int stockRangeIndex = StockIndexOfRanges(newRanges.Count - 1); // losowanie indeksu podzakresu do rekurencyjnego wyszukiwania
                    //jeżeli w przetworzonych podzakresach już istnieje taki element, to losuj ponownie
                    if (processedRanges.Exists(x => x.Item1 == newRanges[stockRangeIndex].Item1 && x.Item2 == newRanges[stockRangeIndex].Item2)) // jezeli przetwarzalismy juz ten podzakres to kontynuujemy i losujemy inny
                    {
                        continue;
                    }
                    processedRanges.Add(newRanges[stockRangeIndex]); // jezeli nie przetwarzalismy wylosowanego podzakrsu, to zapamietujemy go w liscie podzakresow przetworzonych
                    generatedNumber = Process(newRanges[stockRangeIndex]); // wywolujemy rekurencyjnie kolejne przetwarzanie

                } while (generatedNumber == -1 && processedRanges.Count < newRanges.Count); // kontynuuujemy do momentu gdy wylosowany numerek bedzie rozny od -1 i dopoki nie przeprocesujemy wszystkich
            }
            else
            {
                throw new Exception("Something went wrong with application"); // na wypadek nieprawidlowego dzialania aplikacji
            }
            return generatedNumber;
        }
        
        /// <summary>
        /// Pobiera liczebność zakresu zamkniętego
        /// </summary>
        /// <param name="range">Zakres</param>
        /// <returns></returns>
        protected virtual int GetRangeCount(Tuple<int, int> range) => range.Item2 - range.Item1 + 1;
        protected virtual int ProcessTemp2(Tuple<int, int> range)
        {
            //ElementCheck firstCheck = new FirstCheck();
            //ElementCheck secondCheck = new SecondCheck();
            //ElementCheck thirdCheck = new ThirdCheck();
            //ElementCheck fourthCheck = new FourthCheck(_columnName,_tableName,_connectionString);
            //ElementCheck fifthCheck = new FifthCheck();
            //firstCheck.SetSuccessor(secondCheck);
            //secondCheck.SetSuccessor(thirdCheck);
            //thirdCheck.SetSuccessor(fourthCheck);
            //fourthCheck.SetSuccessor(fifthCheck);
            return -1;// firstCheck.Execute(ElementsInRangeTemp(range), GetRangeCount(range));
        }
        /// <summary>
        /// sprawdza czy w zakresie nie ma przydzielonych numerow i losuje numer z takiego zakresu
        /// jezeli w zakresie sa przydzielone numery, to rekurencyjnie szuka podzakresow w ktorych numerow przydzielonych nie ma i losuje az do wyczerpania puli
        /// </summary>
        /// <param name="range">zakres puli w danej rekurencji</param>
        /// <returns>wylosowany numer</returns>
        protected virtual int ProcessTemp(Tuple<int, int> range)
        {
            int generatedNumber = -1; // poczatkowa wartosc numeru wylosowanego dla danej rekurencji
            int elementInRange = ElementsInRangeTemp(range);
            int numberPoolCount = GetRangeCount(range);//liczebnosc puli

            if (elementInRange < 0 || elementInRange > numberPoolCount)//jesli liczebnosc elementow przetworzonych w zakresie jest mniejsza od zera lub wieksza od zakresu puli to rzucamy wyjatek
            {
                throw new Exception("Something went wrong with database");
            }
            else if (elementInRange == 0) // jezeli w zakresie nie wystepuja elementy przetworzone to losujemy liczbe z zakresu
            {
                generatedNumber = StockNumberFromSortedRange(range);
            }
            else if (elementInRange == numberPoolCount) // jezeli ilosc elementow przetworzonych w zakresie jest rowna liczebnosci zakresu to zwracamy -1, co oznacza ze ze zbioru nie da sie losowac
            {
                //z tego zbioru juz nie mozna losowac
                generatedNumber = -1;
            }
            else if (elementInRange > 0 && elementInRange < numberPoolCount)//jesli liczebnosc elementow przetworzonych w zakresie jest wieksza od zera i mniejsza od zakresu puli to rekurencyjnie przeszukujemy podzakresy
            {
                generatedNumber = -1;
                do
                {
                    generatedNumber = StockNumberFromSortedRange(range);
                } while (BinarySearch(range, generatedNumber) != -1);
            }
            else
            {
                throw new Exception("Something went wrong with application"); // na wypadek nieprawidlowego dzialania aplikacji
            }
            return generatedNumber;
        }
        /// <summary>
        /// Na podstawie zapytania zwraca jego wartosc
        /// oczekiwanym wynikiem zapytania jest liczba typu int
        /// metoda rzuci wyjatkiej w wypadku odczytu zlej wartosci
        /// </summary>
        /// <param name="query">zapytanie</param>
        /// <returns>wartosc pobrana lub -1 przy dodawaniu wpisu</returns>
        private int GetDataFromDB(string query) // prywatna, poniewaz dostep do bazy danych mozna zmienic tylko na poziomie konstruktora
        {
            SqlConnection connection = new SqlConnection(_connectionString);
            SqlCommand command;
            SqlDataReader dataReader;
            int number = -1;
            try
            {
                connection.Open();
                command = new SqlCommand(query, connection);
                dataReader = command.ExecuteReader();
                while (dataReader.Read())
                {
                    number = (int)dataReader.GetInt32(0);//jezeli bedzie inna wartosci to rzuci wyjatek
                }
                dataReader.Close();
                command.Dispose();
                connection.Close();
            }
            catch (Exception ex)
            {
                throw ex; //przerzucamy, ale domyslnie zalogowac
            }
            return number;
        }
        protected virtual int ElementsInRangeTemp(Tuple<int, int> range)
        {
            int pierwszyElement = GetDataFromDB($@"Select Count(*) From
                        (
                        SELECT ROW_NUMBER() OVER(ORDER BY {_columnName} ASC) As Row, {_columnName}
                        from {_tableName}
                        where {_columnName} <= {range.Item1}
                        ) as subquery;
                        ");
            int drugiElement = GetDataFromDB($@"Select Count(*) From
                        (
                        SELECT ROW_NUMBER() OVER(ORDER BY {_columnName} ASC) As Row, {_columnName}
                        from {_tableName}
                        where {_columnName} <= {range.Item2}
                        ) as subquery;
                        ");
            return drugiElement - pierwszyElement + 1; //dodajemy 1 bo zbiór jest zamknięty
        }
        /// <summary>
        /// Sprawdza na podstawie skrajnych elementow z bazy i ilosci wszystkich elementow z bazy ile elementow przydzielono w zakresie
        /// </summary>
        /// <param name="range">zakres</param>
        protected virtual int ElementsInRange(Tuple<int, int> range)
        {
            int elementsInRange = 0;// poczatkowa wartosc ilosci elementow
            // pobierz element najbliższy do pierwszego elementu, większy lub równy jemu
            // *1 jeżeli takiego nie ma, to w zakresie jest 0 elementów 
            // jezeli jest wiekszy to zbior otwarty, jezeli mniejszy to zbior zamkniety
            bool isFirstOpen;
            int first = GetNearestElement(range.Item1, out isFirstOpen); // pierwszy najblizszy element
            // numer wiersza w bazie danych z pierwszym najblizszym elementem
            int rowNumberOfFirst = GetDataFromDB($@"Select Count(*) From
                        (
                        SELECT ROW_NUMBER() OVER(ORDER BY {_columnName} ASC) As Row, {_columnName}
                        from {_tableName}
                        where {_columnName} <= {first}
                        ) as subquery;
                        ");
            int indexOfFirst = rowNumberOfFirst - 1; //od numeru wiersza odejmujemy 1 zeby otrzymac index

            // pobierz element najbliższy do ostatniego elementu, mniejszy lub równy jemu
            //jeżeli takiego nie ma, to w zakresie jest 0 elementów <-- ale tutaj nie wejdzie, bo *1 działa
            // jezeli jest mniejszy to zbior otwarty, jezeli wiekszy to zbior zamkniety
            bool isSecondOpen;
            int last = GetNearestElement(range.Item2, out isSecondOpen); //tutaj podobnie jak poprzednio
            int rowNumberOfLast = GetDataFromDB($@"Select Count(*) From
                        (
                        SELECT ROW_NUMBER() OVER(ORDER BY {_columnName} ASC) As Row, {_columnName}
                        from {_tableName}
                        where {_columnName} <= {last}
                        ) as subquery;
                        ");
            int indexOfLast = rowNumberOfLast - 1;

            //powyższe tworzy zbiór co najmniej dwuelementowy

            var tempRange = indexOfLast - indexOfFirst + 1;
            //zakres utworzyl nam ilosc elementow do dodawania
            //robimy zbior otwarty
            bool oneElementRange = first == last;
            if (oneElementRange)
                tempRange = tempRange - 1;
            else
                tempRange = tempRange - 2;

            // sprawdzenie jak te elementy maja sie do glownej tablicy
            // jesli mamy zbior zamkniety to dodajemy do liczebnosci skrajne elementy
            if (first >= range.Item1 && first <= range.Item2)
            {
                elementsInRange++;
            }
            if (last >= range.Item1 && last <= range.Item2 && !oneElementRange)
            {
                elementsInRange++;
            }
            elementsInRange += tempRange;

            return elementsInRange;
        }
        protected virtual int GetNearestElement(int nearestElement, out bool isOpen)
        {
            int liczba = -1;
            liczba = GetDataFromDB($"select distinct {_columnName} from {_tableName} where {_columnName} = {nearestElement}"); // uwaga na starsze wersja - interpolacja // daje distinct jesli jakims cudem na bazie wartosc sie powtorzy
            if (liczba != -1)
            {
                isOpen = false;
            }
            else
            {
                isOpen = true;
                int minValue = GetDataFromDB($"select min({_columnName}) from {_tableName}");
                int maxValue = GetDataFromDB($"select max({_columnName}) from {_tableName}");
                Tuple<int, int> usedNumbersRange = new Tuple<int, int>(minValue, maxValue);

                liczba = IndexOf(usedNumbersRange, nearestElement);
            }

            return liczba;
        }
        protected virtual int IndexOf(Tuple<int, int> usedNumbersRange, int nearestElement)
        {
            int liczba = -1;
            //jeżeli indeks pierwszy jest większy od indeksu elementu
            if (usedNumbersRange.Item1 > nearestElement) // czy nie jest pod dolnym zakresem
            {
                liczba = usedNumbersRange.Item1; //zakres otwarty
            }
            else if (usedNumbersRange.Item2 < nearestElement) //czy nie jest nad gornym zakresem
            {
                liczba = usedNumbersRange.Item2; //zakres otwarty
            }
            else if (usedNumbersRange.Item1 < nearestElement && usedNumbersRange.Item2 > nearestElement)
            {
                int usedNumbersCount = GetDataFromDB($"select Count(*) from {_tableName}");
                int subRangeCount = usedNumbersCount / 2;

                //ostatnia liczba z pierwszej
                //pobierz element z takiego miejsca:
                //subRangeCount - 1
                int lastNumberFromFirstSubrange = GetDataFromDB($@"       
                    SELECT {_columnName} FROM
                        (SELECT ROW_NUMBER() 
                            OVER (ORDER BY {_columnName}) AS Row,
		                    {_columnName}
                        FROM {_tableName}) as {_columnName}
                    WHERE Row = {subRangeCount - 1}
                    ");

                //pierwsza liczba z drugiej
                //pobierz element z takiego miejsca:
                // subrangeCount
                int firstNumberFromSecondSubrange = GetDataFromDB($@"       
                    SELECT {_columnName} FROM
                        (SELECT ROW_NUMBER() 
                            OVER (ORDER BY {_columnName}) AS Row,
		                    {_columnName}
                        FROM {_tableName}) as {_columnName}
                    WHERE Row = {subRangeCount}
                    ");

                Tuple<int, int> firstSubrange = new Tuple<int, int>(0, subRangeCount);
                Tuple<int, int> secondSubrange = new Tuple<int, int>(subRangeCount, subRangeCount);

                if (lastNumberFromFirstSubrange > nearestElement)
                {
                    //to wybieraj na nowo z drugiego czlonu numerow przydzielonych
                    liczba = IndexOf(firstSubrange, nearestElement);
                }
                else
                {
                    //wybieraj na nowo z pierwszego członu
                    liczba = IndexOf(secondSubrange, nearestElement);
                }
            }
            return liczba;
        }
        /// <summary>
        /// Losowanie liczby z podanego zakresu
        /// </summary>
        /// <param name="range">zakres</param>
        /// <returns>wylosowana liczba</returns>
        protected virtual int StockNumberFromSortedRange(Tuple<int, int> range)
        {
            int number = -1;
            Random rand = new Random();
            number = rand.Next(range.Item1, range.Item2 + 1);
            return number;
        }
        /// <summary>
        /// Losowanie indeksu podzakresu z jego dlugosci
        /// </summary>
        /// <param name="rangesCount">dlugosc podzakresu</param>
        /// <returns>wylosowany indeks</returns>
        protected virtual int StockIndexOfRanges(int rangesCount)
        {
            int liczba = -1;
            Random rand = new Random();
            liczba = rand.Next(0, rangesCount + 1);
            return liczba;
        }
        protected virtual List<Tuple<int, int>> DivideRange(Tuple<int, int> range, int numberOfDivisions)
        {
            if (numberOfDivisions < 2)
                return new List<Tuple<int, int>>();//tego zbioru juz nie mozna dzielic

            List<Tuple<int, int>> rangesList = new List<Tuple<int, int>>();
            int rangeCenter = (range.Item2 + range.Item1) / 2;// to jest nieprawidłowe liczenie środka zbioru
            var firstRange = new Tuple<int, int>(range.Item1, rangeCenter);
            rangesList.Add(firstRange);
            var secondRange = new Tuple<int, int>(rangeCenter + 1, range.Item2);
            rangesList.Add(secondRange);

            int newCount = numberOfDivisions - 2;

            int rangesDepth = newCount / 2;
            int firstRangeDepth = rangesDepth;
            if (newCount % 2 != 0)
                firstRangeDepth++;
            if (firstRangeDepth + 1 >= 2)
                rangesList.Remove(firstRange);
            rangesList.AddRange(DivideRange(firstRange, firstRangeDepth + 1)); //na trzy
            if (rangesDepth + 1 >= 2)
                rangesList.Remove(secondRange);
            rangesList.AddRange(DivideRange(secondRange, rangesDepth + 1)); //na cztery

            return rangesList;
        }
    }
}
