import numpy as np
import matplotlib.pyplot as plt
import math

def f(x):
    return 1/(1+x*x)

def NewtonCotes(a, b, n):
    
    ######## He so cua Newton Cotes #########
    n1 = [1, 1]                             #
    n2 = [1, 4, 1]                          #
    n3 = [1, 3, 3, 1]                       #
    n4 = [7,32, 12,32, 7]                   #
    n5 = [19, 75, 50, 50, 75, 19]           #
    n6 = [41, 216, 27, 272, 27, 216, 41]    #
    #########################################
    x = np.linspace(a, b, n + 1)
    y = np.zeros(n + 1)
    for i in range (n + 1):
        y[i] = f(x[i])
    
    Hy = np.zeros(n + 1)
    
    if (n == 1):
        for i in range (n + 1):
            Hy[i] = (n1[i] * y[i])/2
    elif (n == 2):
        for i in range (n + 1):
            Hy[i] = (n2[i] * y[i])/6
    elif (n == 3):
        for i in range (n + 1):
            Hy[i] = (n3[i] * y[i])/8
    elif (n == 4):
        for i in range (n + 1):
            Hy[i] = (n4[i] * y[i])/90
    elif (n == 5):
        for i in range (n + 1):
            Hy[i] = (n5[i] * y[i])/288
    elif (n == 6):
        for i in range (n + 1):
            Hy[i] = (n6[i] * y[i])/840
    S = np.sum(Hy)    
    return S



def Simpson(a, b, n):
    h = 1/n
    x = np.linspace(a, b, n+1)
    y = np.zeros(n + 1)
    for i in range (n + 1):
        y[i] = f(x[i])
    I1 = y[0] + y[n]
    I2 = 0
    I3 = 0
    for i in range (1, n):
        if (i % 2 == 0):
            I2 = I2 + y[i]
        else:
            I3 = I3 + y[i]
            
    return (h/3)*(I1 + 4*I3 + 2*I2)



def Trapezium(a, b, n):
    h = (b - a)/n
    x = np.linspace(a, b, n+1)
    y = np.zeros(n + 1)
    for i in range (n + 1):
        y[i] = f(x[i])
    I1 = 0
    for i in range(1, n):
        I1 = I1 + y[i]
    return (h/2)*((y[0] + y[n]) + 2*I1)


#=========================================================================#
if __name__ =="__main__":
    I = Simpson(0, 3, 10)
    I1 = NewtonCotes(0, 3, 3)
    I2 = Trapezium(0, 3, 6000)
    print(I2)
        
    