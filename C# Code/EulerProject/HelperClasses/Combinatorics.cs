using System.Collections.Generic;
using System.Linq;

namespace ProjectEuler.HelperClasses
{
    public static class Combinatorics
    {
        /// <summary>
        /// Generates a list of partitions of n as the sum of integers.
        /// </summary>
        /// <param name="n"></param>
        /// <returns></returns>
        public static Dictionary<long, List<List<long>>> Partition(long n)
        {
            var partitionsDictionary = new Dictionary<long, List<List<long>>>();
            partitionsDictionary.Add(0, new List<List<long>> { new List<long> { } });
            partitionsDictionary.Add(1, new List<List<long>> { new List<long> { 1 } });

            for (long k = 2; k <= n; k++)
            {
                partitionsDictionary.Add(k, new List<List<long>> { new List<long> { k } });
                for (long firstBit = k - 1; firstBit > 0; firstBit--)
                {
                    var eligiblePartitions = partitionsDictionary[k - firstBit].Where(p => p.Max() <= firstBit);

                    foreach (var par in eligiblePartitions)
                    {
                        var thisPartition = new List<long>(par);
                        thisPartition.Add(firstBit);
                        partitionsDictionary[k].Add(thisPartition);
                    }
                }
            }
            return partitionsDictionary;
        }

        /// <summary>
        /// Computes n choose k = n!/(k!(n-k)!).
        /// </summary>
        /// <param name="n"></param>
        /// <param name="k"></param>
        /// <returns></returns>
        public static long Choose(long n, long k)
        {
            if (k < 0 || k > n)
                return 0;
            if (2 * k > n)
                return Choose(n, n - k);
            long result = 1;
            for (long i = 0; i < k; i++)
                result = (result * (n - i)) / (i + 1);
            return result;
        }

        /// <summary>
        /// Generate a list of permutations of a string of a specified length. 
        /// </summary>
        /// <param name="elements">The letters to permute.</param>
        /// <param name="length">Permutation length. </param>
        /// <returns></returns>
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

        /// <summary>
        /// Generate a list of permutations of numbers of a specified length. 
        /// </summary>
        /// <param name="elements">The letters to permute.</param>
        /// <param name="length">Permutation length. </param>
        /// <returns></returns>
        public static List<List<int>> GeneratePermutations(List<int> elements, long length)
        {
            if (length == -1)
                length = elements.Count;
            var permutations = new List<List<int>>();
            if (elements == null || length == 0 || length > elements.Count)
                return permutations;
            if (length == 1)
            {
                foreach (var element in elements)
                    permutations.Add(new List<int> { element });
                return permutations;
            }
            foreach (var item in elements)
            {
                var subSet = new List<int>(elements);
                subSet.Remove(item);
                var permutationsOfSubset = GeneratePermutations(subSet, length - 1);
                for (int i = 0; i < permutationsOfSubset.Count; i++)
                {
                    permutationsOfSubset[i].Add(item);
                    permutations.Add(permutationsOfSubset[i]);
                }
            }
            return permutations;
        }

        /// <summary>
        /// Generates a set of permutations of all the elements of a list of strings. 
        /// </summary>
        /// <param name="elements"></param>
        /// <returns></returns>
        public static List<string> GeneratePermutations(List<string> elements)
        {
            return GeneratePermutations(elements, elements.Count);
        }

        /// <summary>
        /// Generate a digit permutation of digits of a specified length. 
        /// </summary>
        /// <param name="elements">The letters to permute.</param>
        /// <param name="length">Permutation length. </param>
        /// <returns></returns>
        public static List<long> GeneratePermutations(List<long> elements, long length)
        {
            var stringElements = new List<string>();
            elements.ForEach(t => stringElements.Add(t.ToString()));
            var stringPermutations = GeneratePermutations(stringElements, length);
            var permutations = new List<long>();
            stringPermutations.ForEach(t => permutations.Add(long.Parse(t)));
            return permutations;
        }

        /// <summary>
        /// Generates a list of all subsets of a set of elements. 
        /// </summary>
        /// <param name="elements"></param>
        /// <returns></returns>
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

        /// <summary>
        /// Computes the complement of a set with respect to a reference set.
        /// </summary>
        /// <param name="subset"></param>
        /// <param name="reference"></param>
        /// <returns></returns>
        public static List<long> Complement(List<long> subset, List<long> reference)
        {
            var complement = new List<long>();
            foreach (var element in reference)
                if (!subset.Contains(element))
                    complement.Add(element);
            return complement;
        }
    }
}
