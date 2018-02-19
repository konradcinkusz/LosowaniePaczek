using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ParcelNumberGenerator
{
    //te operacje klasy bazowe powinny sobie pobrać
    public class DBOperation
    {
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
        public DBOperation()
        {
            //Domyslnie serwer zainstalowany jest na lokalnej maszynie
            _connectionString =
                "Integrated Security=SSPI;Initial Catalog=ParcelNumberGenerator;Data Source=localhost\\SQLEXPRESS;";
            _tableName = "USED_NUMBERS";
            _columnName = "usedNumber";
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
        private int generateNumber { get; } = 33;
        //zapisuje nowy numer paczki na baze
        public void SaveNew()
        {
            GetDataFromDB($@"insert into {_tableName}({_columnName}) values({generateNumber});");
        }

        protected int GetNumberFromDatabaseTableByRowId(int rowId) =>
            GetDataFromDB($@"       
                    SELECT {_columnName} FROM
                        (SELECT ROW_NUMBER() OVER (ORDER BY {_columnName}) AS Row,
		                    {_columnName}
                        FROM {_tableName}) as {_columnName}
                    WHERE Row = {rowId}
                    ");
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
    }
}
