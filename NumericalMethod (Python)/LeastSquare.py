import numpy as np 
import matplotlib.pyplot as plt
import math
from numpy import linalg as la

def LS(X, y):
    n = X.shape[0] #số hàng
    d = X.shape[1] #số cột
    S = 0
    one = np.ones((n, 1)) 
    X = np.append(one, X, axis = 1)
    print(X)
    xtx = np.matmul(X.T, X)
    xty = np.matmul(X.T, y) 
    B_ls = np.matmul(la.inv(xtx), xty)
    print("Hồi quy tuyến tính")
    print("=========================================")
    print("XTX")
    print(xtx)
    print("Vecto hệ số:" )
    print(B_ls)
    y_ls = np.matmul(X, B_ls)
    e_ls = y - y_ls
    print("\nước lượng bình phương cực tiểu của y :")
    print(y_ls)
    print("\nMa trận phần dư")
    print(e_ls)
    print("\nTổng bình phương các phần dư là")
    print(np.matmul(e_ls.T, e_ls))
    
if __name__ == "__main__":
    x = np.array([7, 8, 9, 10, 11, 12, 13])
    y = np.array([[7.4], [8.4], [9.1], [9.4], [9.5], [9.5], [9.4]])
    x2 = np.zeros(len(x))
    for i in range(len(x)):
        x2[i] = math.pow(x[i], 2)
    Z = np.zeros((len(x), 2))
    Z[:, 0] = x
    Z[:, 1] = x2
    #print(Z)
    LS(Z, y)