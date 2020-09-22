import Horcner as H
import numpy as np

def diff(x, y):
    n = len(y)
    delta = np.zeros((n, n))
    delta[:, 0] = y
    
    for i in range(1, n):
        for j in range(0, n - i):
            delta[j, i] = (delta[j+1, i-1] - delta[j, i-1])
    return delta

def  Bessel(x, y):
    n = len(x)
    P = np.zeros(n)
    for i in range(1, n):
        L = np.zeros(i)
        for j in range(0, i):
            if (i % 2 != 0):
                L[j] = (1 - i)/2 + j
            else:
                L[j] = (2 - i) / 2 + j
        #for j in range(n - 1, n - i - 2, -1):
            