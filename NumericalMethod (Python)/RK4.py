import numpy as np
import matplotlib.pyplot as plt

r = 0.5
n = 10
K = 0.3
a = 1.2
def f(x, y):
    return x + y


def RK4(xs, xd, ys, h):
    n = (int)((xd - xs)/h)
    x = np.zeros(n + 1)
    y = np.zeros(n + 1)
    x[0] = xs
    y[0] = ys
    for i in range(1, n + 1):
        k1 = h * f(x[i-1], y[i-1])
        k2 = h * f(x[i-1] + 0.5 * h, y[i-1] + 0.5 * k1)
        k3 = h * f(x[i-1] + 0.5 * h, y[i-1] + 0.5 * k2)
        k4 = h * f(x[i-1] + h, y[i-1] + k3)
        y[i] = y[i - 1] + (1.0 / 6.0)*(k1 + 2 * k2 + 2 * k3 + k4)
        x[i] = x[i - 1] + h
    return x, y



def F(x, y):
    F = np.zeros(2)
    F[0] = y[1]
    F[1] = x*(y[0]*y[0])*(1+np.sin(x*y[0])) + (x*x*x)*y[1]
    #F[1] = -x*F[1] - F[0]
    return F

def rk4coef(F, x, y, h):
    k0 = h*F(x, y)
    k1 = h*F(x + h/2.0, y + k0/2.0)
    k2 = h*F(x + h/2.0, y + k1/2.0) 
    k3 = h*F(x + h, y + k2)
    return k0, k1, k2, k3

def rk4(F, x, y, xStop, h):
    X = np.linspace(x, xStop, num = np.ceil((np.abs(xStop - x)/h) + 1))
    Y = []
    Y.append(y)
    for i in range (1, len(X)):
        k1, k2, k3, k4 = rk4coef(F, X[i-1], Y[i-1], h)
        Y.append(Y[i-1] + (1/6)*(k1 + 2*k2 + 2*k3 + k4))
    return X, Y


if __name__ == "__main__":
    xs = 0
    xd = 1
    
    ys = np.array([1, 1])

    h = 0.1
    #x, y = RK4(xs, xd, ys, h)
    x, y= rk4(F, xs, ys, xd, h)
    print(y)
    plt.plot(y)
    #plt.plot(x, y, "ro")
