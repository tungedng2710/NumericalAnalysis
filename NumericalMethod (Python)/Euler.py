import numpy as np

def f(x, y):
    return x+y

def EFw(xs, xd, ys, h):
    n = (int)((xd - xs)/h)
    x = np.zeros(n+1)
    y = np.zeros(n+1)
    x[0] = xs
    y[0] = ys
    for i in range (1, n+1):
        x[i] = x[i-1] + h
        y[i] = y[i-1] + h*f(x[i-1], y[i-1])
    return y

def EBw(xs, xd, ys, h):
    n = (int)((xd - xs)/h)
    x = np.zeros(n+1)
    y = np.zeros(n+1)
    x[0] = xs
    y[0] = ys
    for i in range (1, n+1):
        x[i] = x[i-1] + h
        y[i] = y[i-1] + h*f(x[i-1], y[i-1])
        y[i] = y[i-1] + h*f(x[i], y[i])
    return y

def Trapezium(xs, xd, ys, h):
    n = (int)((xd - xs)/h)
    x = np.zeros(n+1)
    y = np.zeros(n+1)
    x[0] = xs
    y[0] = ys
    for i in range (1, n+1):
        x[i] = x[i-1] + h
        y[i] = y[i-1] + h*f(x[i-1], y[i-1])
        y[i] = y[i-1] + (h/2)*(f(x[i-1], y[i-1]) + f(x[i], y[i]))
    return y

if __name__ == "__main__":
    xs = 0
    ys = 1
    xd = 1
    h = 0.01
    y = Trapezium(xs, xd, ys, h)
    print(y)