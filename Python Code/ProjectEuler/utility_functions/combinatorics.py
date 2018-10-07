import math, sympy
from .is_perfect_square import is_perfect_square



def choose(n, k):
    """
    A fast way to calculate binomial coefficients by Andrew Dalke (contrib).
    """
    if 0 <= k <= n:
        ntok = 1
        ktok = 1
        for t in range(1, min(k, n - k) + 1):
            ntok *= n
            ktok *= t
            n -= 1
        return ntok // ktok
    else:
        return 0


def decimal_to_roman(n):
    """
    Converts a decimal to Roman numerals.
    """
    if n > 10000:
        return "Number too large."
    M = n // 1000
    n -= 1000 * M
    C = n // 100
    n -= 100 * C
    X = n // 10
    I = n - 10 * X
    result = ""
    for m in range(M):
        result += "M"
    if C == 9:
        result += "CM"
    elif C >= 5:
        result += "D"
        for c in range(C - 5):
            result += "C"
    elif C == 4:
        result += "CD"
    else:
        for c in range(C):
            result += "C"
    if X == 9:
        result += "XC"
    elif X >= 5:
        result += "L"
        for x in range(X - 5):
            result += "X"
    elif X == 4:
        result += "XL"
    else:
        for x in range(X):
            result += "X"
    if I == 9:
        result += "IX"
    elif I >= 5:
        result += "V"
        for i in range(I - 5):
            result += "I"
    elif I == 4:
        result += "IV"
    else:
        for i in range(I):
            result += "I"
    return result


def digits(n):
    """Returns a list of digits of n"""
    s = str(n)
    digits = []
    for c in s:
        digits.append(int(c))
    return digits


def factorial(n):
    """Computes n!"""
    prod = 1
    k = 1
    while k <= n:
        prod *= k
        k += 1
    return prod


def reverse_string(A):
    B = ""
    for char in A:
        B = char + B
    return B


def is_palindrome(n):
    """Tests whether the positive integer n is a palindrome."""
    m = int(reverse_string(str(n)))
    return ((m - n) == 0)


def is_permutation(n, m):
    """
    Determines whether m and n (integer or string) are permutations
    of each other.
    """
    try:
        if int(n) == n:
            return digits(n).sort == digits(m).sort
    except ValueError:
        N = list(n)
        M = list(m)
        N.sort()
        M.sort()
        return N == M


def num_factors(n):
    """Determines the number of factor a positive integer n has,
    including itself"""
    count = 0
    for i in range(1, math.floor(math.sqrt(n)) + 1):
        if n % i == 0:
            count += 2
    if math.floor(math.sqrt(n)) ** 2 == n:
        count -= 1
    return count


def phi(n):
    """Calculates the value of phi(n), the number of positive integers
    less than n and relatively prime to n."""
    return sympy.totient(n)


def sieve(n) -> list:
    primeSieve = [False] * (n + 1)
    primeSieve[2] = True
    for i in range(n + 1):
        if (i % 2 == 1):
            primeSieve[i] = True

    c = 3
    while (c * c <= n):
        i = c * c
        inc = c + c
        while (i <= n):
            primeSieve[i] = False
            i += inc
        c += 2
        while (c <= n and (not primeSieve[c])):
            c += 1
    primes = list()
    for i in range(n + 1):
        if (primeSieve[i]):
            primes.append(i)
    return primes


def roman_to_decimal(s):
    """
    Converts Roman numerals to its decimal equivalent. Will not detect
    when a Roman numeral is invalid.
    """
    romans = {"M": 1000, "D": 500, "C": 100, "L": 50, "X": 10, "V": 5, "I": 1}
    l = len(s)
    num = 0
    for i in range(l - 1):
        if romans[s[i + 1]] > romans[s[i]]:
            num -= romans[s[i]]
        else:
            num += romans[s[i]]
    num += romans[s[l - 1]]
    return num


def sigma(n):
    """Computes the sum of the proper divisors of n."""
    divsum = -n
    step = n % 2 + 1
    root = math.floor(math.sqrt(n))
    for i in range(1, root + 1, step):
        if n % i == 0:
            divsum += i + n // i
    if root ** 2 == n:
        divsum -= root
    return divsum


def squarefree_part(n):
    """Calculates the squarefree part of n."""
    result = 1
    factors = sympy.factorint(n)
    for p in factors:
        if factors[p] % 2 == 1:
            result *= p
    return result


def radical(n):
    """Calculates the radical of n; the product of the distinct prime factors
    of n."""
    result = 1
    factors = sympy.factorint(n)
    for p in factors:
        result *= p
    return result


def partition(elementlist, bins):
    """
    Lists the ways in which the elements of elementlist can be divided into
    bins groups.
    """
    if bins > len(elementlist):
        return set()
    elif bins == 1:
        return [[sorted(elementlist)]]
    else:
        listofpartitions = []
        firstelement = elementlist[0]
        for subpartition in partition(elementlist[1:], bins - 1):
            subpartition.append([firstelement])
            if sorted(subpartition) not in listofpartitions:
                listofpartitions.append(sorted(subpartition))
        for subpartition in partition(elementlist[1:], bins):
            for subsubpartition in subpartition:
                tempsubpartition = list(subpartition)
                tempsubsubpartition = list(subsubpartition)
                tempsubpartition.remove(subsubpartition)
                tempsubsubpartition.append(firstelement)
                tempsubpartition.append(sorted(tempsubsubpartition))
                if sorted(tempsubpartition) not in listofpartitions:
                    listofpartitions.append(sorted(tempsubpartition))
    return (listofpartitions)


def permutations(elementset, subsetsize):
    """
    Lists the different permutations of size subsetsize that can be obtained
    from the elements in elementset. Repetition not allowed.
    """
    if subsetsize == 1:
        permutationlist = []
        for j in elementset:
            permutationlist.append([j])
    elif len(elementset) < subsetsize:
        permutationlist = []
    else:
        firstelement = elementset[0]
        permutationlist = permutations(elementset[1:], subsetsize)
        temppermutations = permutations(elementset[1:], subsetsize - 1)
        for i in temppermutations:
            for j in range(subsetsize - 1):
                temp = list(i)
                temp.insert(j, firstelement)
                permutationlist.append(temp)
            i.append(firstelement)
            permutationlist.append(i)
    return permutationlist


def combinations(elementset, subsetsize):
    """
    Lists the different combinations of size subsetsize that can be obtained
    from the elements in elementset. No repetition.
    """
    if subsetsize == 0:
        combinationlist = [[]]
    elif len(elementset) < subsetsize:
        combinationlist = []
    else:
        lastelement = elementset[len(elementset) - 1]
        combinationlist = combinations(elementset[:len(elementset) - 1], subsetsize)
        tempcombinations = combinations(elementset[:len(elementset) - 1], subsetsize - 1)
        for i in tempcombinations:
            i.append(lastelement)
            combinationlist.append(i)
    return combinationlist


def permutations_with_repetition(elementset, subsetsize):
    """
    Lists the different permutations of size subsetsize that can be obtained
    from the elements in elementset. Repetition allowed.
    """
    permutationlist = []
    n = len(elementset)
    r = subsetsize
    for i in range(n ** r):
        newpermutation = []
        N = i
        for j in range(r):
            index = N % n
            N = N // n
            newpermutation.append(elementset[index])
        permutationlist.append(newpermutation)
    return permutationlist


def combinations_with_repetition(elementset, subsetsize):
    """
    Lists the different combinations of size subsetsize that can be obtained
    from the elements in elementset. Repetition allowed.
    """
    combinationslist = []
    n = len(elementset)
    for j in combinations(range(n + subsetsize - 1), n - 1):
        newcombination = []
        j.insert(0, -1)
        j.insert(n, n + subsetsize - 1)
        for i in range(n):
            for k in range(j[i + 1] - j[i] - 1):
                newcombination.append(elementset[i])
        combinationslist.append(newcombination)
    return combinationslist


def runlength(cipher, encode=True):
    """
    Computes the runlength encoding/decoding of a bit string.
    """
    tempcipher = str(cipher)
    if cipher == "":
        return ""
    elif encode:
        index = tempcipher.find(str(1 - int(tempcipher[0])))
        if index == -1:
            index = len(tempcipher)
        return str(index) + runlength(tempcipher[index:], encode)
    else:
        bit = 0
        bitstring = ""
        for c in tempcipher:
            bitstring += str(bit) * int(c)
            bit = 1 - bit
        return bitstring


def mod_power(a, exp, n):
    if (n == 1):
        return 0
    mod_power_result = 1
    a = a % n
    while (exp > 0):
        if (exp % 2 == 1):
            mod_power_result = (mod_power_result * a % n)
        exp >>= 1
        a = a * a % n
    return modPower


def gcd(a, b, return_inverses = False):
    x = [0,0];
    y = [0,0];
    
    x[0] = 1; y[0] = 0;
    x[1] = 0; y[1] = 1;
    while (b != 0):
        temp = b;
        b = a % b;
        m = (a - b) / temp;
        a = temp;
        tempX = x[0] - m * x[1];
        tempY = y[0] - m * y[1];
        x[0] = x[1]; x[1] = tempX;
        y[0] = y[1]; y[1] = tempY;
    inverseA = x[0];
    inverseB = y[0];
    if return_inverses: 
        return a, inverseA, inverseB
    return a