using ParcelNumberGenerator.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ParcelNumberGenerator.OthersImplementations
{
    public sealed class NumberPoolDBv2WithUBS : NumberPoolDBv2
    {
        private const int LOG_N = 2;
        private readonly List<int> delta;
        public NumberPoolDBv2WithUBS() : base()
        {
            delta = makeDelta();
        }
        public NumberPoolDBv2WithUBS(Tuple<int, int> numberPoolRange, string connectionString, string tableName = "USED_NUMBERS", string columnName = "usedNumber") : base(numberPoolRange, connectionString, tableName, columnName)
        {
            delta = makeDelta();
        }
        private int makeDeltaElement(int i, int N) => (int)Math.Floor((N + Math.Pow(LOG_N, i - 1)) / Math.Pow(LOG_N, i));
        private int endDeltaRange(int N) => (int)Math.Floor(Math.Log(N, LOG_N) + LOG_N);
        public List<int> makeDelta()
        {
            int length = GetDataFromDB($@"Select Count(*) From {_tableName}");
            List<int> delta = new List<int>();
            for (int i = 1; i <= endDeltaRange(length); i++)
                delta.Add(makeDeltaElement(i, length));
            return delta;
        }
        /// <summary>
        /// Uniform binary search
        /// </summary>
        /// <param name="range"></param>
        /// <param name="searchElement"></param>
        /// <returns></returns>
        protected override int BinarySearch(Tuple<int, int> range, int searchElement) =>
            Mode == Mode.Iterative ? UBSInterative(searchElement) : UBS(delta[0] - 1, 0, searchElement);
        private int UBSInterative(int key)
        {
            int searchElement = -1;
            int i = delta[0] - 1;
            int j = 0;
            while (true)
            {
                int a = GetNumberFromDatabaseTableByRowId(i);
                if (key == a)
                {
                    searchElement = a;
                    break;
                }
                else if (delta[j] == 0)
                {
                    searchElement = -1;
                    break;
                }
                else
                    if (key < a)
                    i -= delta[++j];
                else
                    i += delta[++j];
            }
            return searchElement;
        }

        private int UBS(int i, int j, int key)
        {
            int searchElement = -1;
            int a = GetNumberFromDatabaseTableByRowId(i);
            if (key < a && delta[j] != 0)
                searchElement = UBS(i - delta[++j], j, key);
            else if (key > a && delta[j] != 0)
                searchElement = UBS(i + delta[++j], j, key);
            else if (key == a)
                searchElement = key;
            return searchElement;
        }

    }
}
