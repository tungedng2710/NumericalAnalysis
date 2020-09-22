import Horcner as H
import numpy as np
import matplotlib.pyplot as plt
import math

def diff(y):
    n = len(y)
    delta = np.zeros((n, n))
    delta[:, 0] = y
    
    for i in range(1, n):
        for j in range(0, n - i):
            delta[j, i] = delta[j+1, i-1] - delta[j, i-1]
    
    return delta


