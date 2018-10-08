using System;
using System.Collections.Generic;
using System.Diagnostics;

namespace ProjectEuler
{
    public class UtilityFunctions
    {
        public static List<long> SquareFreeIntegers(long n, out List<long> nonSquareFree, List<long> primes = null)
        {
            nonSquareFree = NonSquareFreeIntegers(n);
            var allIntegersUpToN = new HashSet<long>();
            for (long i = 1; i <= n; i++)
                allIntegersUpToN.Add(i);
            allIntegersUpToN.ExceptWith(nonSquareFree);

            return new List<long>(allIntegersUpToN);
        }

        public static List<long> NonSquareFreeIntegers(long n, bool generatePrimes = false, List<long> squares = null)
        {
            var nonSquareFree = new HashSet<long>();
            if (squares == null)
            {
                squares = new List<long>();
                
                var primes = Primes((long)Math.Sqrt(n));
                foreach (var p in primes)
                    squares.Add(p * p);
            }
            
            foreach (var sq in squares)
            {
                for (long i = 1; ; i++)
                {
                    var nextNumber = i * sq;
                    if (nextNumber > n)
                        break;
                    nonSquareFree.Add(nextNumber);
                }
            }
            

            return new List<long>(nonSquareFree);
        }

        public static List<long[]> Partition(long n, long[] elementList = null, long[] coefficientList = null, long maxIndex = -100, int chunks = -100)
        {
            var dictionaryOfPartitions = new Dictionary<long,Dictionary<int,List<long[]>>>();
            if (elementList == null)
            {
                elementList = new long[n];
                for (long i = 0; i < n; i++)
                    elementList[i] = i + 1;
            }

            if (coefficientList==null)
            {
                coefficientList = new long[n];
                for (long i = 0; i < n; i++)
                    coefficientList[i] = i + 1;
            }

            var maxUsableIndex = 0;
            for (int i = 1; i < elementList.Length; i++)
            {
                if (elementList[i] > n)
                    break;
                maxUsableIndex = i;

            }

            if (maxIndex == -100 || maxIndex >= Math.Min(elementList.Length,maxUsableIndex))
                maxIndex = maxUsableIndex;

            if (n == 0)
                return new List<long[]> { new long[maxIndex + 1] };
            if (chunks == 0)
                return new List<long[]>();

            var partitions = new List<long[]>();

            if (maxIndex == 0)
            {
                if (n % elementList[0] == 0 && Array.IndexOf(coefficientList, n/elementList[0]) != -1)
                    return new List<long[]> { new long[] { n / elementList[0] } };
                else
                    return new List<long[]>();
            }
            if (!dictionaryOfPartitions.ContainsKey(n) || !dictionaryOfPartitions[n].ContainsKey(chunks))
            {
                var chunkDictionary = new Dictionary<int, List<long[]>>();
                chunkDictionary.Add(chunks, Partition(n, elementList, coefficientList, maxIndex - 1, chunks));
                dictionaryOfPartitions.Add(n, chunkDictionary);
            }
            foreach (var partition in dictionaryOfPartitions[n][chunks])
            {
                var thisPartition = new long[maxIndex + 1];
                for (long j = 0; j < maxIndex; j++)
                {
                    thisPartition[j] = partition[j];
                }
                partitions.Add(thisPartition);
            }
            
            for (long coeffIndex = 0; coeffIndex < coefficientList.Length; coeffIndex++)
            {
                var thisCoefficient = coefficientList[coeffIndex];
                long smallerN = n - elementList[maxIndex] * thisCoefficient;
                List<long[]> smallerList;
                if (smallerN < 0)
                    break;
                if (smallerN == 0)
                    smallerList = new List<long[]>();
                else
                {
                    if (!dictionaryOfPartitions.ContainsKey(smallerN) || !dictionaryOfPartitions[smallerN].ContainsKey(chunks - 1))
                    {
                        var chunkDictionary = new Dictionary<int, List<long[]>>();
                        chunkDictionary.Add(chunks - 1, Partition(smallerN, elementList, coefficientList, maxIndex - 1, chunks - 1));
                        dictionaryOfPartitions.Add(smallerN, chunkDictionary);
                    }
                    smallerList = dictionaryOfPartitions[smallerN][chunks - 1];
                }
                foreach (var partition in smallerList)
                {
                    var thisPartition = new long[maxIndex + 1];
                    thisPartition[maxIndex] = thisCoefficient;
                    for (long j = 0; j < partition.Length; j++)
                    {
                        thisPartition[j] = partition[j];
                    }
                    partitions.Add(thisPartition);
                }
            }

            return partitions;
        }

       public static List<long[]> PartitionIntoPerfectSquares(long n, long[] perfectSquares = null, long maxIndex = 0)
        {
            if (perfectSquares == null)
            {
                List<long> tempPerfectSquares = new List<long>();
                for (long i = 1; ; i++)
                {
                    long thisSquare = i * i;
                    if (thisSquare > n)
                        break;
                    tempPerfectSquares.Add(thisSquare);
                }
                perfectSquares = tempPerfectSquares.ToArray();
                maxIndex = perfectSquares.Length - 1;
            }
            return Partition(n, perfectSquares, null, maxIndex);
        }

        public static List<string> GeneratePermutations(List<string> elements, long length)
        {
            if (length == -1)
                length = elements.Count;
            var permutations = new List<string>();
            if (elements == null || length == 0 || length > elements.Count)
                return permutations;
            if (length == 1)
                return elements;
            foreach (var item in elements)
            {
                var subSet = new List<string>(elements);
                subSet.Remove(item);
                var permutationsOfSubset = GeneratePermutations(subSet, length - 1);
                for (int i = 0; i < permutationsOfSubset.Count; i++)
                    permutations.Add(item + permutationsOfSubset[i]);                   
            }
            return permutations;
        }

        public static List<string> GeneratePermutations(List<string> elements)
        {
            return GeneratePermutations(elements,elements.Count);
        }

        public static List<long> GeneratePermutations(List<long> elements, long length)
        {
            var stringElements = new List<string>();
            elements.ForEach(t => stringElements.Add(t.ToString()));
            var stringPermutations = GeneratePermutations(stringElements, length);
            var permutations = new List<long>();
            stringPermutations.ForEach(t => permutations.Add(long.Parse(t)));
            return permutations;
        }

        public static List<List<long>> GenerateSubsets(List<long> elements)
        {
            var subsets = new List<List<long>>();
            subsets.Add(new List<long>());
            foreach (var element in elements)
            {
                var subsetsCount = subsets.Count;
                for (int i = 0; i < subsetsCount; i++)
                {
                    var newSubset = new List<long>(subsets[i]);
                    newSubset.Add(element);
                    subsets.Add(newSubset);
                }
            }
            return subsets;
        }

        public static List<long> Complement(List<long> subset, List<long> reference)
        {
            var complement = new List<long>();
            foreach (var element in reference)
                if (!subset.Contains(element))
                    complement.Add(element);
            return complement;
        }

        public static bool IsPrime(long n)
        {
            if (n == 2 || n == 3 || n == 5 || n == 7)
                return true;
            var upperLimit = (long)Math.Sqrt(n);
            var sieve = Primes(upperLimit);
            foreach (var prime in sieve)
                if (n % prime == 0)
                    return false;
            return true;
        }

        public static List<long> Primes(long B0)
        {
            // Sieve of Eratosthenes 
            // find all prime numbers 
            // less than or equal B0 

            bool[] sieve = new bool[B0 + 1];
            long c = 3, i, inc;

            sieve[2] = true;

            for (i = 3; i <= B0; i++)
                if (i % 2 == 1)
                    sieve[i] = true;

            do
            {
                i = c * c;
                inc = c + c;

                while (i <= B0)
                {
                    sieve[i] = false;

                    i += inc;
                }

                c += 2;

                while (c <= B0 && !sieve[c])
                    c++;
            } while (c * c <= B0);

            var primes = new List<long>();

            for (i = 2; i <= B0; i++)
                if (sieve[i])
                    primes.Add(i);

            return primes;
        }

        public static long DigitSignature(long number, List<long> reference)
        {
            var digits = Digits(number);
            return DigitSignature(digits, reference);
        }

        public static long DigitSignature(List<long> digits, List<long> reference)
        {
            long count = 0;
            if (digits == null)
                return 0;
            foreach (var digit in digits)
            {
                var digitPosition = reference.IndexOf(digit);
                if (digitPosition == -1)
                    return 0;
                count += (long)Math.Pow(2, digitPosition);
            }
            return count;
        }

        public static List<long> Digits(long number)
        {
            var digits = new List<long>();
            for (int i = 0; number > 0; i++)
            {
                var thisDigit = number % 10;
                number = number / 10;
                if (digits.Contains(thisDigit))
                    return null;
                digits.Add(thisDigit);
            }
            return digits;
        }

        public static long Totient(long n)
        {
            {
                var primes = Primes(n);
                if (primes[primes.Count - 1] == n)
                    return n - 1;

                long numPrimes = primes.Count;

                long totient = n;
                long currentNum = n, temp, p, prevP = 0;
                for (int i = 0; i < numPrimes; i++)
                {
                    p = primes[i];
                    if (p > currentNum) break;
                    temp = currentNum / p;
                    if (temp * p == currentNum)
                    {
                        currentNum = temp;
                        i--;
                        if (prevP != p)
                        {
                            prevP = p;
                            totient -= (totient / p);
                        }
                    }
                }
                return totient;
            }
        }

        /// <summary>
        /// Computes a to the power b modulo n.
        /// </summary>
        /// <param name="a">Base</param>
        /// <param name="exp">Power</param>
        /// <param name="n">Modulus</param>
        /// <returns></returns>
        public static long ModPower(long a, long exp, long n)
        {
            if (n == 1) return 0;
            long modPower = 1;

            a = a % n;

            while (exp > 0)
            {
                if (exp % 2 == 1)
                    modPower = (modPower * a % n);
                exp >>= 1;
                a = a * a % n;
            }
            return modPower;
         }

        public static long Gcd(long a, long b, out long inverseA, out long inverseB)
        {
            var x = new long[2];
            var y = new long[2];

            x[0] = 1; y[0] = 0;
            x[1] = 0; y[1] = 1;
            while (b != 0)
            {
                var temp = b;
                b = a % b;
                var m = (a - b) / temp;
                a = temp;
                var tempX = x[0] - m * x[1];
                var tempY = y[0] - m * y[1];
                x[0] = x[1]; x[1] = tempX;
                y[0] = y[1]; y[1] = tempY;
            }
            inverseA = x[0];
            inverseB = y[0];
            return a;
        }

        public static long Gcd(long a, long b)
        {
            while (b != 0)
            {
                var temp = b;
                b = a % b;
                a = temp;
            }
            return a;
        }

        public static long Lcm(long a, long b)
        {
            return (a / Gcd(a, b)) * b;
        }

        public static long[] CarmichaelArray(long n)
        {
            var stopwatch = new Stopwatch();
            var tracker = 0;
            stopwatch.Start();
            var charmichaelArray = new long[n + 1];
            charmichaelArray[1] = 1;

            var numbers = new List<long> { 1 };
            var primes = Primes(n);

            foreach (var p in primes)
            {
                
                long factor = p;
                numbers.Sort();
                var numbersLength = numbers.Count;
                if ((numbersLength - tracker) / (double)n > 0.1)
                {
                    Console.WriteLine($"{100.0*numbersLength / n}% completed in {stopwatch.ElapsedMilliseconds / 1000.0} seconds.");
                    tracker = numbersLength;
                }
                if (numbersLength == n)
                    break;
                while (factor <= n)
                {
                    if (p == 2 && factor > 4)
                    {
                        charmichaelArray[factor] = factor / 2 - factor / 4;
                    }
                    else
                        charmichaelArray[factor] = factor - factor / p;
                    numbers.Add(factor);

                    for (int i = 1; i < numbersLength; i++)
                    {
                        var numberToUse = numbers[i];
                        var index = numberToUse * factor;
                        if (index <= n)
                        {
                            charmichaelArray[index] = Lcm(charmichaelArray[numberToUse], charmichaelArray[factor]);
                            numbers.Add(index);
                        }
                        else break;
                    }
                    factor *= p;
                }
            }
            return charmichaelArray;
        }

        public static long MultiplicativeOrder(long baseNumber, long n)
        {
            if (Gcd(baseNumber, n) != 1)
                return 0;
            long currentMod = 1;
            for (int i = 1; true; i++)
            {
                currentMod = (currentMod * baseNumber % n);
                if (currentMod == 1)
                    return i;
            }
        }

        public static long ModularInverse(long number, long n)
        {
            long result, dummy;
            if (Gcd(number, n, out result, out dummy) != 1)
                return 0;
                           
            return (result % n + n) % n;
        }

        public static bool IsPerfectSquare(long n)
        {
            // Quickfail
            if (n < 0 || ((n & 2) != 0) || ((n & 7) == 5) || ((n & 11) == 8))
                return false;
            if (n == 0)
                return true;

            // Check mod 255 = 3 * 5 * 17, for fun
            long y = n;
            y = (y & 0xffffffffL) + (y >> 32);
            y = (y & 0xffffL) + (y >> 16);
            y = (y & 0xffL) + ((y >> 8) & 0xffL) + (y >> 16);
            if (bad255[(int)y])
                return false;

            // Divide out powers of 4 using binary search
            if ((n & 0xffffffffL) == 0)
                n >>= 32;
            if ((n & 0xffffL) == 0)
                n >>= 16;
            if ((n & 0xffL) == 0)
                n >>= 8;
            if ((n & 0xfL) == 0)
                n >>= 4;
            if ((n & 0x3L) == 0)
                n >>= 2;

            if ((n & 0x7L) != 1)
                return false;

            // Compute sqrt using something like Hensel's lemma
            long r, t, z;
            r = start[(int)((n >> 3) & 0x3ffL)];
            do
            {
                z = n - r * r;
                if (z == 0)
                    return true;
                if (z < 0)
                    return false;
                t = z & (-z);
                r += (z & t) >> 1;
                if (r > (t >> 1))
                    r = t - r;
            } while (t <= (1L << 33));
            return false;
        }

        private static readonly bool[] bad255 =
            {
                false,false,true ,true ,false,true ,true ,true ,true ,false,true ,true ,true ,
                true ,true ,false,false,true ,true ,false,true ,false,true ,true ,true ,false,
                true ,true ,true ,true ,false,true ,true ,true ,false,true ,false,true ,true ,
                true ,true ,true ,true ,true ,true ,true ,true ,true ,true ,false,true ,false,
                true ,true ,true ,false,true ,true ,true ,true ,false,true ,true ,true ,false,
                true ,false,true ,true ,false,false,true ,true ,true ,true ,true ,false,true ,
                true ,true ,true ,false,true ,true ,false,false,true ,true ,true ,true ,true ,
                true ,true ,true ,false,true ,true ,true ,true ,true ,false,true ,true ,true ,
                true ,true ,false,true ,true ,true ,true ,false,true ,true ,true ,false,true ,
                true ,true ,true ,false,false,true ,true ,true ,true ,true ,true ,true ,true ,
                true ,true ,true ,true ,true ,false,false,true ,true ,true ,true ,true ,true ,
                true ,false,false,true ,true ,true ,true ,true ,false,true ,true ,false,true ,
                true ,true ,true ,true ,true ,true ,true ,true ,true ,true ,false,true ,true ,
                false,true ,false,true ,true ,false,true ,true ,true ,true ,true ,true ,true ,
                true ,true ,true ,true ,false,true ,true ,false,true ,true ,true ,true ,true ,
                false,false,true ,true ,true ,true ,true ,true ,true ,false,false,true ,true ,
                true ,true ,true ,true ,true ,true ,true ,true ,true ,true ,true ,false,false,
                true ,true ,true ,true ,false,true ,true ,true ,false,true ,true ,true ,true ,
                false,true ,true ,true ,true ,true ,false,true ,true ,true ,true ,true ,false,
                true ,true ,true ,true ,true ,true ,true ,true ,false,false,true ,true ,false,
                true ,true ,true ,true ,false,true ,true ,true ,true ,true ,false,false,true ,
                true ,false,true ,false,true ,true ,true ,false,true ,true ,true ,true ,false,
                true ,true ,true ,false,true ,false,true ,true ,true ,true ,true ,true ,true ,
                true ,true ,true ,true ,true ,false,true ,false,true ,true ,true ,false,true ,
                true ,true ,true ,false,true ,true ,true ,false,true ,false,true ,true ,false,
                false,true ,true ,true ,true ,true ,false,true ,true ,true ,true ,false,true ,
                true ,false,false,true ,true ,true ,true ,true ,true ,true ,true ,false,true ,
                true ,true ,true ,true ,false,true ,true ,true ,true ,true ,false,true ,true ,
                true ,true ,false,true ,true ,true ,false,true ,true ,true ,true ,false,false,
                true ,true ,true ,true ,true ,true ,true ,true ,true ,true ,true ,true ,true ,
                false,false,true ,true ,true ,true ,true ,true ,true ,false,false,true ,true ,
                true ,true ,true ,false,true ,true ,false,true ,true ,true ,true ,true ,true ,
                true ,true ,true ,true ,true ,false,true ,true ,false,true ,false,true ,true ,
                false,true ,true ,true ,true ,true ,true ,true ,true ,true ,true ,true ,false,
                true ,true ,false,true ,true ,true ,true ,true ,false,false,true ,true ,true ,
                true ,true ,true ,true ,false,false,true ,true ,true ,true ,true ,true ,true ,
                true ,true ,true ,true ,true ,true ,false,false,true ,true ,true ,true ,false,
                true ,true ,true ,false,true ,true ,true ,true ,false,true ,true ,true ,true ,
                true ,false,true ,true ,true ,true ,true ,false,true ,true ,true ,true ,true ,
                true ,true ,true ,false,false
            };


        private static readonly int[] start =
            {
                1,3,1769,5,1937,1741,7,1451,479,157,9,91,945,659,1817,11,
                1983,707,1321,1211,1071,13,1479,405,415,1501,1609,741,15,339,1703,203,
                129,1411,873,1669,17,1715,1145,1835,351,1251,887,1573,975,19,1127,395,
                1855,1981,425,453,1105,653,327,21,287,93,713,1691,1935,301,551,587,
                257,1277,23,763,1903,1075,1799,1877,223,1437,1783,859,1201,621,25,779,
                1727,573,471,1979,815,1293,825,363,159,1315,183,27,241,941,601,971,
                385,131,919,901,273,435,647,1493,95,29,1417,805,719,1261,1177,1163,
                1599,835,1367,315,1361,1933,1977,747,31,1373,1079,1637,1679,1581,1753,1355,
                513,1539,1815,1531,1647,205,505,1109,33,1379,521,1627,1457,1901,1767,1547,
                1471,1853,1833,1349,559,1523,967,1131,97,35,1975,795,497,1875,1191,1739,
                641,1149,1385,133,529,845,1657,725,161,1309,375,37,463,1555,615,1931,
                1343,445,937,1083,1617,883,185,1515,225,1443,1225,869,1423,1235,39,1973,
                769,259,489,1797,1391,1485,1287,341,289,99,1271,1701,1713,915,537,1781,
                1215,963,41,581,303,243,1337,1899,353,1245,329,1563,753,595,1113,1589,
                897,1667,407,635,785,1971,135,43,417,1507,1929,731,207,275,1689,1397,
                1087,1725,855,1851,1873,397,1607,1813,481,163,567,101,1167,45,1831,1205,
                1025,1021,1303,1029,1135,1331,1017,427,545,1181,1033,933,1969,365,1255,1013,
                959,317,1751,187,47,1037,455,1429,609,1571,1463,1765,1009,685,679,821,
                1153,387,1897,1403,1041,691,1927,811,673,227,137,1499,49,1005,103,629,
                831,1091,1449,1477,1967,1677,697,1045,737,1117,1737,667,911,1325,473,437,
                1281,1795,1001,261,879,51,775,1195,801,1635,759,165,1871,1645,1049,245,
                703,1597,553,955,209,1779,1849,661,865,291,841,997,1265,1965,1625,53,
                1409,893,105,1925,1297,589,377,1579,929,1053,1655,1829,305,1811,1895,139,
                575,189,343,709,1711,1139,1095,277,993,1699,55,1435,655,1491,1319,331,
                1537,515,791,507,623,1229,1529,1963,1057,355,1545,603,1615,1171,743,523,
                447,1219,1239,1723,465,499,57,107,1121,989,951,229,1521,851,167,715,
                1665,1923,1687,1157,1553,1869,1415,1749,1185,1763,649,1061,561,531,409,907,
                319,1469,1961,59,1455,141,1209,491,1249,419,1847,1893,399,211,985,1099,
                1793,765,1513,1275,367,1587,263,1365,1313,925,247,1371,1359,109,1561,1291,
                191,61,1065,1605,721,781,1735,875,1377,1827,1353,539,1777,429,1959,1483,
                1921,643,617,389,1809,947,889,981,1441,483,1143,293,817,749,1383,1675,
                63,1347,169,827,1199,1421,583,1259,1505,861,457,1125,143,1069,807,1867,
                2047,2045,279,2043,111,307,2041,597,1569,1891,2039,1957,1103,1389,231,2037,
                65,1341,727,837,977,2035,569,1643,1633,547,439,1307,2033,1709,345,1845,
                1919,637,1175,379,2031,333,903,213,1697,797,1161,475,1073,2029,921,1653,
                193,67,1623,1595,943,1395,1721,2027,1761,1955,1335,357,113,1747,1497,1461,
                1791,771,2025,1285,145,973,249,171,1825,611,265,1189,847,1427,2023,1269,
                321,1475,1577,69,1233,755,1223,1685,1889,733,1865,2021,1807,1107,1447,1077,
                1663,1917,1129,1147,1775,1613,1401,555,1953,2019,631,1243,1329,787,871,885,
                449,1213,681,1733,687,115,71,1301,2017,675,969,411,369,467,295,693,
                1535,509,233,517,401,1843,1543,939,2015,669,1527,421,591,147,281,501,
                577,195,215,699,1489,525,1081,917,1951,2013,73,1253,1551,173,857,309,
                1407,899,663,1915,1519,1203,391,1323,1887,739,1673,2011,1585,493,1433,117,
                705,1603,1111,965,431,1165,1863,533,1823,605,823,1179,625,813,2009,75,
                1279,1789,1559,251,657,563,761,1707,1759,1949,777,347,335,1133,1511,267,
                833,1085,2007,1467,1745,1805,711,149,1695,803,1719,485,1295,1453,935,459,
                1151,381,1641,1413,1263,77,1913,2005,1631,541,119,1317,1841,1773,359,651,
                961,323,1193,197,175,1651,441,235,1567,1885,1481,1947,881,2003,217,843,
                1023,1027,745,1019,913,717,1031,1621,1503,867,1015,1115,79,1683,793,1035,
                1089,1731,297,1861,2001,1011,1593,619,1439,477,585,283,1039,1363,1369,1227,
                895,1661,151,645,1007,1357,121,1237,1375,1821,1911,549,1999,1043,1945,1419,
                1217,957,599,571,81,371,1351,1003,1311,931,311,1381,1137,723,1575,1611,
                767,253,1047,1787,1169,1997,1273,853,1247,413,1289,1883,177,403,999,1803,
                1345,451,1495,1093,1839,269,199,1387,1183,1757,1207,1051,783,83,423,1995,
                639,1155,1943,123,751,1459,1671,469,1119,995,393,219,1743,237,153,1909,
                1473,1859,1705,1339,337,909,953,1771,1055,349,1993,613,1393,557,729,1717,
                511,1533,1257,1541,1425,819,519,85,991,1693,503,1445,433,877,1305,1525,
                1601,829,809,325,1583,1549,1991,1941,927,1059,1097,1819,527,1197,1881,1333,
                383,125,361,891,495,179,633,299,863,285,1399,987,1487,1517,1639,1141,
                1729,579,87,1989,593,1907,839,1557,799,1629,201,155,1649,1837,1063,949,
                255,1283,535,773,1681,461,1785,683,735,1123,1801,677,689,1939,487,757,
                1857,1987,983,443,1327,1267,313,1173,671,221,695,1509,271,1619,89,565,
                127,1405,1431,1659,239,1101,1159,1067,607,1565,905,1755,1231,1299,665,373,
                1985,701,1879,1221,849,627,1465,789,543,1187,1591,923,1905,979,1241,181
            };
    }
}