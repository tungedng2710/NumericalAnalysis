import Horcner as H
import numpy as np
import Lagrange as L
import Newton as N
import math

def diff(x, y):
    n = len(y)
    delta = np.zeros((n, n))
    delta[:, 0] = y
    for i in range(1, n):
        for j in range(0, n - i):
            delta[j, i] = (delta[j+1, i-1] - delta[j, i-1])
    return delta


def inv_N(x, y, y_hat, epsilon=0.5 * 1e-5, n_iter=100):
    n = len(x)
    delta = diff(x, y)
    f = 1
    t0 = (y_hat - y[0])
    p = np.array([t0])
    t0 = t0 / delta[0, 1]
    f = 1
    for i in range(2, n):
        #print(t0)
        f = f * i
        t = np.array([j for j in range(0, i)])
        p = H.poly_add(p, -1 * (delta[0, i]/f)*H.poly_mul(t))
        t0 = H.eval_poly(p, t0)/delta[0, 1]
    p = p / delta[0, 1]
    # t0 = t0 / delta[0, 1]
    for i in range(n_iter):
        print(t0)
        if np.abs(t0 - H.eval_poly(p, t0)) > epsilon:
            t0 = H.eval_poly(p, t0)
        else:
            break
if __name__ == "__main__":
    M = np.array([[1, 3.6],
                  [2, 5.1],
                  [3, 6.2],
                  [4, 7.1],
                  [5, 8.0],
                  [6, 8.7],
                  [7, 9.4],
                  [8, 10.1],
                  [9, 10.7],
                  [10, 11.3],
                  [12, 12.4],
                  [14, 13.4],
                  [16,14.3],
                  [18,15.2],
                  [20, 16.0],
                  [25, 17.9],
                  [30, 19.6],
                  [35, 21.1],
                  [40, 22.6],
                  [45, 24.0],
                  [50, 25.3],
                  [60, 27.7],
                  [70, 29.9],
                  [80, 31.9],
                  [90, 33.9],
                  [100, 35.7],
                  [150, 43.7],
                  [200, 50.5],
                  [250, 56.5],
                  [300, 61.9],
                  [400, 71.4],
                  [500, 79.9],
                  [1000, 112.9],
                  [2000, 159.7]])
    x1 = M[:, 0]
    y1 = M[:, 1]
    x = []
    y = []
    for i in range (len(x1)):
        x.append(x1[i])
        y.append(y1[i])
    
    P = L.Lagrange(y, x)
    print(H.eval_poly(P, 20))


    