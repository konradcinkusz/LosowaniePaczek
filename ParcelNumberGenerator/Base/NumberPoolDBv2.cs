using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ParcelNumberGenerator.Base
{
    public class NumberPoolDBv2 : INumberPoolGenerator
    {
        /// <summary>
        /// Zakres puli - mozna przypisac tylko podczas tworzenia obiektu puli
        /// </summary>
        protected readonly Tuple<int, int> _numberPoolRange;
        /// <summary>
        /// ConnectionString do bazy
        /// </summary>
        protected readonly string _connectionString;
        /// <summary>
        /// Nazwa tabeli w bazie danych z ktorej pobieramy
        /// </summary>
        protected readonly string _tableName;
        /// <summary>
        /// Nazwa kolumny w bazie danych przechowujacej numery juz wylosowane.
        /// </summary>
        protected readonly string _columnName;
        /// <summary>
        /// Konstruktor domyslny - nadaje domyslne wartosci do pol tylko do odczytu
        /// </summary>
        public NumberPoolDBv2()
        {
            _numberPoolRange = new Tuple<int, int>(1, 100000);
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
        public NumberPoolDBv2(Tuple<int, int> numberPoolRange, string connectionString, string tableName = "USED_NUMBERS", string columnName = "usedNumber")
        {
            _numberPoolRange = numberPoolRange;
            _connectionString = connectionString;
            _tableName = tableName;
            _columnName = columnName;
        }
        
        private Mode _mode;
        public Mode Mode
        {
            get { return _mode; }
            set { _mode = value; }
        }

        /// <summary>
        /// Generuje losowy numer paczki z puli i zapisuje go na baze
        /// </summary>
        /// <returns>numer paczki</returns>
        public virtual int Generate()
        {
            int generateNumber = -1;
            int elementInRange = ElementsInRange(_numberPoolRange);
            int numberPoolCount = GetRangeCount(_numberPoolRange);

            if (elementInRange < 0 || elementInRange > numberPoolCount)
                throw new Exception("Something went wrong with database");
            else if (elementInRange == 0)
                generateNumber = StockNumberFromSortedRange(numberPoolCount);
            else if (elementInRange == numberPoolCount)
                throw new Exception("Empty pool. There are no more free elements in pool.");
            else if (elementInRange > 0 && elementInRange < numberPoolCount)
                do
                {
                    generateNumber = StockNumberFromSortedRange(numberPoolCount);
                } while (BinarySearch(new Tuple<int, int>(0, numberPoolCount), generateNumber) != -1);

            if (generateNumber == -1)
                throw new Exception("Something went wrong with application or empty pool");

            //zapisuje nowy numer paczki na baze
            GetDataFromDB($@"insert into {_tableName}({_columnName}) values({generateNumber});");

            return generateNumber;
        }

        protected int GetNumberFromDatabaseTableByRowId(int rowId) => 
            GetDataFromDB($@"       
                    SELECT {_columnName} FROM
                        (SELECT ROW_NUMBER() OVER (ORDER BY {_columnName}) AS Row,
		                    {_columnName}
                        FROM {_tableName}) as {_columnName}
                    WHERE Row = {rowId}
                    ");

        /// <summary>
        /// Wyszukiwanie binarne
        /// </summary>
        /// <param name="range">Zakres</param>
        /// <param name="searchElement">Element szukany</param>
        /// <returns>Indeks</returns>
        protected virtual int BinarySearch(Tuple<int, int> range, int searchElement)
        {
            int leftIndex = 0, rightIndex= GetDataFromDB($@"Select Count(*) From {_tableName}");
            while (leftIndex < rightIndex)
            {
                int center = (leftIndex + rightIndex) / 2;
                int centerNumber = GetNumberFromDatabaseTableByRowId(center);
                if (centerNumber < searchElement)
                    leftIndex = center + 1;
                else
                    rightIndex = center;
            }
            int leftNumber = GetNumberFromDatabaseTableByRowId(leftIndex);
            return leftNumber == searchElement ? leftIndex - 1 : -1;
        }
        /// <summary>
        /// Na podstawie zapytania zwraca jego wartosc
        /// oczekiwanym wynikiem zapytania jest liczba typu int
        /// metoda rzuci wyjatkiej w wypadku odczytu zlej wartosci
        /// </summary>
        /// <param name="query">zapytanie</param>
        /// <returns>wartosc pobrana lub -1 przy dodawaniu wpisu</returns>
        protected int GetDataFromDB(string query)
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
        protected int ElementsInRange(Tuple<int, int> range)
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
        protected int GetRangeCount(Tuple<int, int> range) => range.Item2 - range.Item1 + 1;
        /// <summary>
        /// Losowanie liczby z podanego zakresu
        /// </summary>
        /// <param name = "range" > zakres </ param >
        /// < returns > wylosowana liczba</returns>
        protected virtual int StockNumberFromSortedRange(int range)
        {
            int liczba = -1;
            Random rand = new Random();
            liczba = rand.Next(1, range);
            return liczba;
        }
    }
}
