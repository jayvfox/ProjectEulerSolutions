import math
from utility_functions import combinatorics

_limit = int(1e12)


def solution():
    answer = 0
    
    m_limit = int(1e4)
    squares = set()

    for m in range(2, m_limit):
        if m%2==1:
            step = 1;
        else: 
            step = 2
        for r in range(1,m,step):
            if combinatorics.gcd(m,r) != 1:
                    continue
            if m*m*m*r + r*r > _limit:
                break
            for k in range(1,_limit):
                n = k*r*(k*m*m*m+r)
                if n >= _limit:
                    break
                if combinatorics.is_perfect_square(n):
                    if n not in squares:
                        print(k,m,r,n)
                        squares.add(n)
                        answer+=n
    return answer


