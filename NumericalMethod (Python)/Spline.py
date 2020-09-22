import numpy as np
import matplotlib.pyplot as plt
from numpy import linalg as la
import math

def QuadSpline(x, y):
    h = []
    d = []
    n = len(x)
    for i in range (n - 1):
        h.append(x[i + 1] - x[i])
        d.append(y[i + 1] - y[i])
    Mat = np.zeros((2*n - 2, 2*n - 1))
    for i in range (2 * n - 2):
        if (i % 2 == 0):
            t = i // 2
            Mat[i, 2 * n - 2] = d[t]
            Mat[i, i] = math.pow(h[t], 2)
            Mat[i, i + 1] = h[t]
    for i in range (2*n - 3):
        if (i % 2 == 1):
            t = i // 2
            Mat[i, i - 1] = 2*h[t]
            Mat[i, i] = 1
            Mat[i, i + 2] = -1
    #Mat[2*n-3, 2*n-4] = 2*h[n - 2]     
    #Mat[2*n-3, 2*n-3] = 1
    Mat[2*n-3, 1] = 1  #dao ham tai diem bien    
    Mat[2*n-3, 2*n-2] = 1
    A = Mat[:, 0 : (2*n - 2)]
    b = Mat[:, (2*n - 2):(2*n - 1)]
    
    X = np.matmul(la.inv(A), b) 
    print(Mat)   
    c = []
    for i in range (1, n):
        c.append(y[i-1])
    print("c = ", c)
    return X

def CubicSpline(x, y):
    n = len(x) - 1 
    h = []
    d = []
    for i in range (n):
        h.append(x[i + 1] - x[i])
        d.append(y[i + 1] - y[i])
    print(h)
    Mat = np.zeros((3*n, 3*n + 1))
    for i in range (3*n - 2):
        t = i // 3
        if (i % 3 == 0):
            Mat[i, 3*n] = d[t]
            Mat[i, i] = math.pow(h[t], 3)
            Mat[i, i + 1] = math.pow(h[t], 2)
            Mat[i, i + 2] = h[t]
        elif (i % 3 == 1):
            Mat[i, i - 1] = 3 * math.pow(h[t], 2)
            Mat[i, i] = 2 * math.pow(h[t], 1)
            Mat[i, i + 1] = 1
            Mat[i, i + 4] = -1
        else:
            Mat[i, i - 2] = 3*h[t]
            Mat[i, i - 1] = 1
            Mat[i, i + 2] = -1
    #Dieu kien bien
    Mat[3*n - 1, 3*n - 3] = 3 * h[n - 1] #dao ham cap 2 tai diem cuoi
    Mat[3*n - 1, 3*n - 2 ] = 1
    Mat[3*n - 1, 3*n] = -1
    Mat[3*n - 2, 3*n - 3] = 3*h[n - 1]*h[n - 1]
    Mat[3*n - 2, 3*n - 2] = 2*h[n - 1]
    Mat[3*n - 2, 3*n - 1] = 1
    Mat[3*n - 2, 3*n] = 0 
    di = []
    for i in range (1, n):
        di.append(y[i-1])
    A = Mat[:, 0:3*n]
    b = Mat[:, (3*n):(3*n + 1)]
    X = np.matmul(la.inv(A), b)

    return Mat

if __name__ == "__main__":
    x = [0, (math.pi)/4, (math.pi)/3, (math.pi)/2]
    y = []
    for i in range (len(x)):
        y.append(math.sin(x[i]))
    #x = [0, 1, 2, 3, 6]
    #y = []
    #for i in range (len(x)):
    #    y.append(math.pow(x[i], 2))
    i = math.factorial(8)
    print(i)
    print(CubicSpline(x, y))
    #plt.plot(x, y)
    
