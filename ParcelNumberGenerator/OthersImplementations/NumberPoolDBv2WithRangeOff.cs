using ParcelNumberGenerator.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ParcelNumberGenerator.OthersImplementations
{
    public sealed class NumberPoolDBv2WithRangeOff : NumberPoolDBv2
    {
        private readonly Tuple<int, int> _rangeOff;
        public NumberPoolDBv2WithRangeOff(Tuple<int, int> rangeOff) : base()
        {
            this._rangeOff = rangeOff;
        }
        /// <summary>
        /// Konstruktor przeciazony nadaje wartosci recznie
        /// </summary>
        /// <param name="rangeOff">Zakres ktory ma byc wyciety z puli</param>
        /// <param name="numberPoolRange">Zakres calej puli</param>
        /// <param name="connectionString">ConnectionString do bazy danych</param>
        /// <param name="tableName">Nazwa tabeli w bazie danych z ktorej pobierane sa numery juz wylosowane</param>
        /// <param name="columnName">Nazwa kolumny w bazie danych przechowujacej numery juz wylosowane</param>
        public NumberPoolDBv2WithRangeOff(
            Tuple<int, int> rangeOff, 
            Tuple<int, int> numberPoolRange, 
            string connectionString, 
            string tableName = "USED_NUMBERS", 
            string columnName = "usedNumber") 
            : 
            base(numberPoolRange, connectionString, tableName, columnName)
        {
            this._rangeOff = rangeOff;
        }
        protected override int BinarySearch(Tuple<int, int> range, int searchElement)
        {
            int leftIndex = 0, rightIndex = GetDataFromDB($@"Select Count(*) From {_tableName}");
            while (leftIndex < rightIndex)
            {
                if (leftIndex >= _rangeOff.Item1 && leftIndex <= _rangeOff.Item2) // jeżeli indeks wchodzi na zastrzeżony zakres to go pomijamy
                {
                    leftIndex++;
                    continue;
                }

                int center = (leftIndex + rightIndex) / 2;
                int centerNumber = GetNumberFromDatabaseTableByRowId(center);
                if (centerNumber < searchElement)
                    leftIndex = center + 1;
                else
                    rightIndex = center;
            }

            //int leftNumber = GetDataFromDB($@"       
            //        SELECT {_columnName} FROM
            //            (SELECT ROW_NUMBER() 
            //                OVER (ORDER BY {_columnName}) AS Row,
            //          {_columnName}
            //            FROM {_tableName}) as {_columnName}
            //        WHERE Row = {leftIndex}
            //        ");
            int leftNumber = GetNumberFromDatabaseTableByRowId(leftIndex);
            return leftNumber == searchElement ? leftIndex - 1 : -1;
        }
    }
}
