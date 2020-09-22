import numpy as np
from numpy import linalg as la
import matplotlib.pyplot as plt
import math
import RK4 as rk

def f(x, y):
    return (x+2*y)/(x**2 + 2*y**2)
    
def AB4(xs, xd, ys, h):
    n = (int)((xd - xs)/h)
    x = np.zeros(n + 1)
    y = np.zeros(n + 1)
    x1, y1 = rk.RK4(xs, xd, ys,h)
    x[0] = xs
    y[0] = ys
    y[1] = y1[1]
    y[2] = y1[2]
    y[3] = y1[3]
    for i in range(0, n):
        x[i + 1] = x[i] + h
    c1 = 55/24
    c2 = -59/24
    c3 = 37/24
    c4 = -9/24
    for i in range (4, n+1):
        y[i] = y[i-1] + h*(c1*f(x[i-1], y[i-1]) + c2*f(x[i-2], y[i-2]) + c3*f(x[i-3], y[i-3]) + c4*f(x[i-4], y[i-4]))
    
    
    return x, y
def loop4am4(xs, xd, ys, h):
    eps = 0.0001
    n = (int)((xd - xs)/h)
    x = np.zeros(n+1)
    y = np.zeros(n+1)
    y1 = np.zeros(n+1)
    xt, yt = rk.RK4(xs, xd, ys,h)
    sigma = np.zeros(n+1)
    y[0] = ys
    y[1] = yt[1]
    y[2] = yt[2]
    for i in range (3, n+1):
        sigma[i] = (h/24.0)*(19*f(xt[i-1], y[i-1])-5*f(xt[i-2], y[i-2])+f(xt[i-3], y[i-3]))
    for i in range (3, n+1):
        y[i] = y[i-1] + (9/24.0)*h*f(xt[i], y[i-1]) + sigma[i-1]
        y1[i] = y[i-1] + (9/24.0)*h*f(xt[i], y[i]) + sigma[i]
        while (abs(y[i] - y1[i]) >= eps):
            y[i] = y[i-1] + (9/24)*h*f(xt[i], y1[i]) + sigma[i]
            y1[i] = y[i-1] + (9/24)*h*f(xt[i], y[i]) + sigma[i]           
#           
    return y
    
def AM4(xs, xd, ys, h):
    n = (int)((xd - xs)/h)
    x = np.zeros(n+1)
    y = np.zeros(n+1)
    #xt, yt = rk.RK4(xs, xd, ys,h)
    xt, yt = AB4(xs, xd, ys,h)
    x[0] = xs
    y[0] = ys
    y[1] = yt[1]
    y[2] = yt[2]
    y[3] = yt[3]
    c1 = 9/24
    c2 = 19/24
    c3 = -5/24
    c4 = 1/24
    for i in range (3, n+1):
        y[i] = y[i-1] + h*(c1*f(xt[i], yt[i]) + c2*f(xt[i-1], y[i-1]) + c3*f(xt[i-2], y[i-2]) + c4*f(xt[i-3], y[i-3]))
    return xt, y

def Coef(k):
    Mat = np.zeros((k, k))
    c = np.zeros(k)
    for i in range (0, k):
        for j in range (0, k):
            t1 = (int)(j - 1)
            t2 = (int)(i - 1)
            Mat[i][j] = t1**t2
    c[0] = 1
    for i in range(1, k):
        c[i] = ((-1)**(i-1))/i
    b = np.matmul(c.T, la.inv(Mat))
    return b


if __name__ == "__main__":
    xs = 0
    xd = 3
    ys = 1
    h = 0.1
    #y = loop4am4(xs, xd, ys, h)
    x, y = rk.RK4(xs, xd, ys, h)
    print(x)
    print(y)
    plt.plot(x, y)
    