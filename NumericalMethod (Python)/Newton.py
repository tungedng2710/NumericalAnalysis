import Horcner as H
import numpy as np
import matplotlib.pyplot as plt
import math

def diffup(x, y):
    n = len(y)
    delta = np.zeros((n, n))
    delta[:, 0] = y
    for i in range(1, n):
        for j in range(0, n - i):
            delta[j, i] = (delta[j+1, i-1] - delta[j, i-1])
            delta[j, i] = delta[j, i] / (x[j + i] - x[j])
    return delta

def difflow(y):
    n = len(y)
    delta = np.zeros((n, n))
    for i in range(0, n):
        delta[i, i] = y[i]
    for k in range(0, n):
        i = 0
        j = k + 1
        while (j != n):
            delta[i, j] = delta[i + 1, j] - delta[i, j - 1]
            i = i + 1
            j = j + 1
    
    return delta 

def diff(y):
    n = len(y)
    delta = np.zeros((n, n))
    delta[:, 0] = y
    
    for i in range(1, n):
        for j in range(0, n - i):
            delta[j, i] = delta[j+1, i-1] - delta[j, i-1]
    
    return delta

def Newton(x, y, x0, direction = "fw"):
    n = len(x)
    if (direction == "fw"):
        dd = diffup(x, y)
        p = np.array([y[0]])
        for i in range (1, n):
            xi = np.array([x[j] for j in range (i)])
            f = dd[0, i]
            p = H.poly_add(p, f * H.poly_mul(xi))
        val = H.eval_poly(p, x0)
        return p, val
    else:
        dd = diffup(x, y)
        p = np.array([y[n - 1]])
        for i in range (1, n):
            xi = np.array([x[j] for j in range (n - 1, n - 1 - i - 1, -1)])
            f = dd[n - 1 - i, i]
            p = H.poly_add(p, f * H.poly_mul(xi))
        print(dd)
        return p
    
    

def Newton2(x, y):
    n = len(x)
    delta = diff(y)
    gt = 1
    k = 0
    Pn = np.zeros(n)
    for i in range (2, n):
        gt = gt * i
    for i in range(n - 1, -1, -1):
        if (i==0):
            Pn[n-1] = Pn[n-1] + delta[0, 0]
        elif(i==1):
            Pn[n-2] = Pn[n-2] + delta[0, 1]
        else:
            xi = np.zeros(i)
            for j in range (0, i):
                xi[j] = j
            temp = H.mul(xi)
            for j in range (0, len(temp)):
                temp[j] = temp[j] * (delta[0, i]/gt)
                Pn[j + k] = Pn[j + k] + temp[j]
            gt = gt / i
            k = k + 1
        
    return Pn

def Newton3(x, y, x0):
    s = 0
    h = x[1] - x[0]
    t = (x[s] - x0) / h
    delta = diff(y)
    P = [y[0]]
    n = len(x)
    temp = [0]
    for i in range (1, n):
        temp = H.poly_mul(temp, [i])
        P = H.poly_add(P, (delta[0, i] / math.factorial(i)) * temp)
    val = H.eval_poly(P, t)
    return P, val

if __name__ == "__main__":
#    M = np.array([[1, 3.6],
#                  [2, 5.1],
#                  [3, 6.2],
#                  [4, 7.1],
#                  [5, 8.0],
#                  [6, 8.7],
#                  [7, 9.4],
#                  [8, 10.1],
#                  [9, 10.7],
#                  [10, 11.3],
#                  [12, 12.4],
#                  [14, 13.4],
#                  [16,14.3],
#                  [18,15.2],
#                  [20, 16.0],
#                  [25, 17.9],
#                  [30, 19.6],
#                  [35, 21.1],
#                  [40, 22.6],
#                  [45, 24.0],
#                  [50, 25.3],
#                  [60, 27.7],
#                  [70, 29.9],
#                  [80, 31.9],
#                  [90, 33.9],
#                  [100, 35.7],
#                  [150, 43.7],
#                  [200, 50.5],
#                  [250, 56.5],
#                  [300, 61.9],
#                  [400, 71.4],
#                  [500, 79.9],
#                  [1000, 112.9],
#                  [2000, 159.7]])
    M = np.loadtxt("Kip2Cau1.txt")
    x1 = M[12:21, 0]
    y1 = M[12:21, 1]
    x = []
    y = []
    for i in range (len(x1)):
        x.append(x1[i])
        y.append(y1[i])
    print(M[:, 0])
    #print(diffup(x, y))

    #print(Newton(y, x, 20))
    #print(diff(y1))
    #print(H.eval_poly(Newton2(x1, y1), 6.51))
    #print(H.eval_poly(Newton(x, y), 6.51))
    #print(H.eval_poly(P, 3.52))
    #H.eval_poly(Newton(x, y, "bw"), 6.5)
    plt.plot(x1, y1)
    
    
