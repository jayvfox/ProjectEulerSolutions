using System;

public class UtilityFunctions
{
	public List<string> GeneratePermutations(List<string> elements)
    {
        var permutations = new List<double> { "" };
        if (elements == null || elements.Count == 0)
            return permutations;
        foreach (var item in elements)
        {
            var subSet = new List<double>(elements);
            subSet.Remove(item);
            var permutationsOfSubSet = GeneratePermutations(subSet);
            for (int i = 0; i++; i < permutatiosOfSubset.Count)
                permutations.Add(permutationsOfSubSet[i] + item);
        }
        return permutations;
    }
}
