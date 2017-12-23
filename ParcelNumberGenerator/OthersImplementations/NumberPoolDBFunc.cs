using ParcelNumberGenerator.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ParcelNumberGenerator.OthersImplementations
{
    public sealed class NumberPoolDBFunc : NumberPoolDBv2
    {
        public override int Generate()
        {
            int generateNumber = -1;
            int elementInRange = ElementsInRange(_numberPoolRange);
            int numberPoolCount = GetRangeCount(_numberPoolRange);
            int checkingNumber = -1;
            if (firstCheck(elementInRange, numberPoolCount, out checkingNumber)) ;
            else if (secondCheck(elementInRange, numberPoolCount, out checkingNumber)) ;
            else if (thirdCheck(elementInRange, numberPoolCount, out checkingNumber)) ;
            else if (fourthCheck(elementInRange, numberPoolCount, out checkingNumber)) ;
            else
                throw new Exception("Something went wrong with application");
            generateNumber = checkingNumber;
            return generateNumber;
        }
        private bool firstCheck(int elementInRange, int numberPoolCount, out int checkingNumber)
        {
            if (elementInRange < 0 || elementInRange > numberPoolCount)
                throw new Exception("Something went wrong with database");
            checkingNumber = -1;
            return false;
        }
        private bool secondCheck(int elementInRange, int numberPoolCount, out int checkingNumber)
        {
            bool isSecond = elementInRange == 0;
            checkingNumber = isSecond ? StockNumberFromSortedRange(numberPoolCount) : -1;
            return isSecond;
        }
        private bool thirdCheck(int elementInRange, int numberPoolCount, out int checkingNumber)
        {
            checkingNumber = -1;
            return elementInRange == numberPoolCount;
        }
        private bool fourthCheck(int elementInRange, int numberPoolCount, out int checkingNumber)
        {
            bool isFourth = elementInRange > 0 && elementInRange < numberPoolCount;
            int generatedNumber = -1;
            if (isFourth)
            {
                do
                {
                    generatedNumber = StockNumberFromSortedRange(numberPoolCount);
                } while (BinarySearch(new Tuple<int, int>(0, numberPoolCount), generatedNumber) != -1);

                if (generatedNumber == -1) //jesli Process nie zwrocil wyjatku to teraz go rzucamy, zeby nie zapisal nic na baze
                    throw new Exception("Pula pusta. Brak wolnych numerow. Nie mozna dalej losowac z tej puli.");

                //zapisuje nowy numer paczki na baze
                GetDataFromDB($@"insert into {_tableName}({_columnName}) values({generatedNumber});");

            }
            checkingNumber = generatedNumber;
            return isFourth;
        }

    }
}
