using System;
using System.Data.SqlClient;

namespace ParcelNumberGenerator
{
    public class NumberPoolDatabase : INumberPoolGenerator
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
        public NumberPoolDatabase()
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
        public NumberPoolDatabase(Tuple<int, int> numberPoolRange, string connectionString, string tableName = "USED_NUMBERS", string columnName = "usedNumber")
        {
            _numberPoolRange = numberPoolRange;
            _connectionString = connectionString;
            _tableName = tableName;
            _columnName = columnName;
        }
        /// <summary>
        /// Generuje losowy numer paczki z puli i zapisuje go na baze
        /// </summary>
        /// <returns>numer paczki</returns>
        public int Generate()
        {
            Step firstCheck = new FirstCheck(_numberPoolRange, _columnName, _tableName, _connectionString);
            Step secondCheck = new SecondCheck(_numberPoolRange, _columnName, _tableName, _connectionString);
            Step thirdCheck = new ThirdCheck(_numberPoolRange, _columnName, _tableName, _connectionString);
            Step fourthCheck = new FourthCheck(_numberPoolRange, _columnName, _tableName, _connectionString);
            Step fifthCheck = new FifthCheck(_numberPoolRange, _columnName, _tableName, _connectionString);
            firstCheck.SetSuccessor(secondCheck);
            secondCheck.SetSuccessor(thirdCheck);
            thirdCheck.SetSuccessor(fourthCheck);
            fourthCheck.SetSuccessor(fifthCheck);
            return firstCheck.Execute();
        }
        private Mode _mode;
        public Mode Mode
        {
            get { return _mode; }
            set { _mode = value; }
        }
        public abstract class Step
        {
            public Step(Tuple<int, int> range, string ColumnName, string TableName, string ConnectionString)
            {
                _columnName = ColumnName;
                _tableName = TableName;
                _connectionString = ConnectionString;
                _elementInRange = ElementsInRange(range);
                _numberPoolCount = GetRangeCount(range);
            }
            private Step nextLink;
            protected virtual int _elementInRange { get; set; } = 0;
            protected virtual int _numberPoolCount { get; set; } = 0;
            protected virtual string _columnName { get; set; } = "defaultColumnName";
            protected virtual string _tableName { get; set; } = "defaultTableName";
            protected virtual string _connectionString { get; set; } = "defaultConnectionString";
            public void SetSuccessor(Step next)
            {
                nextLink = next;
            }

            public virtual int Execute()
            {
                if (nextLink != null)
                {
                    return nextLink.Execute();
                }
                return 0;
            }

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
                int leftIndex = 0, rightIndex = GetDataFromDB($@"Select Count(*) From {_tableName}");
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
            protected virtual int GetDataFromDB(string query) // prywatna, poniewaz dostep do bazy danych mozna zmienic tylko na poziomie konstruktora
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
            protected virtual int ElementsInRange(Tuple<int, int> range)
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
            protected virtual int GetRangeCount(Tuple<int, int> range) => range.Item2 - range.Item1 + 1;
        }
        public class FirstCheck : Step
        {
            public FirstCheck(Tuple<int, int> range, string ColumnName, string TableName, string ConnectionString) : base(range, ColumnName, TableName, ConnectionString) { }

            public override int Execute()
            {
                if (_elementInRange < 0 || _elementInRange > _numberPoolCount)
                {
                    throw new Exception("Something went wrong with database");
                }
                return base.Execute();
            }
        }
        public class SecondCheck : Step
        {
            public SecondCheck(Tuple<int, int> range, string ColumnName, string TableName, string ConnectionString) : base(range, ColumnName, TableName, ConnectionString) { }
            public override int Execute()
            {
                if (_elementInRange == 0)
                {
                    return StockNumberFromSortedRange(_numberPoolCount);
                }
                return base.Execute();
            }
        }
        public class ThirdCheck : Step
        {
            public ThirdCheck(Tuple<int, int> range, string ColumnName, string TableName, string ConnectionString) : base(range, ColumnName, TableName, ConnectionString) { }
            public override int Execute()
            {
                if (_elementInRange == _numberPoolCount)
                {
                    return -1;
                }
                return base.Execute();
            }
        }
        public class FourthCheck : Step
        {
            public FourthCheck(Tuple<int, int> range, string ColumnName, string TableName, string ConnectionString) : base(range, ColumnName, TableName, ConnectionString) { }
            public override int Execute() //te metodki tak de facto nie muszą nic zwracać, bo i tak zapisujemy na bazke
            {
                if (_elementInRange > 0 && _elementInRange < _numberPoolCount)
                {
                    int generatedNumber = -1;
                    do
                    {
                        generatedNumber = StockNumberFromSortedRange(_numberPoolCount);
                    } while (BinarySearch(new Tuple<int, int>(0, _numberPoolCount), generatedNumber) != -1);

                    if (generatedNumber == -1) //jesli Process nie zwrocil wyjatku to teraz go rzucamy, zeby nie zapisal nic na baze
                        throw new Exception("Pula pusta. Brak wolnych numerow. Nie mozna dalej losowac z tej puli.");
                    
                    //zapisuje nowy numer paczki na baze
                    GetDataFromDB($@"insert into {_tableName}({_columnName}) values({generatedNumber});");

                    return generatedNumber;
                }
                return base.Execute();
            }
        }
        public class FifthCheck : Step
        {
            public FifthCheck(Tuple<int, int> range, string ColumnName, string TableName, string ConnectionString) : base(range, ColumnName, TableName, ConnectionString) { }
            public override int Execute()
            {
                if (_elementInRange == _numberPoolCount)
                {
                    throw new Exception("Something went wrong with application");
                }
                return base.Execute();
            }
        }
    }
}
