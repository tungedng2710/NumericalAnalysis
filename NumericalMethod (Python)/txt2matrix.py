import numpy as np
file = open(r"Kip2Cau1.txt", "r")
str = file.read()
s = np.loadtxt("Kip2Cau1.txt")
print(s)

import matplotlib.pyplot as plt
def f(x):
    return (6*(x**2) - 2)/((1+x**2)**3)

x = np.linspace(0, 3, 100)
y = np.zeros(100)
for i in range (100):
    y[i] = f(x[i])
    
plt.plot(x, y)