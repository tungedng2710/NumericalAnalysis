import Horcner as H
import numpy as np

def diff(y):
    n = len(y)
    delta = np.zeros((n, n))
    delta[:, 0] = y
    
    for i in range(1, n):
        for j in range(0, n - i):
            delta[j, i] = delta[j+1, i-1] - delta[j, i-1]
    
    return delta

def stirling(x, y, x1):
    n = len(x)
    h = x[1] - x[0]
    mid = 3
    # Moc noi suy (Co the thay doi cho phu hop)
    x0 = x[mid]
    t = (x1 - x0) / h
    temp1 = [1]
    temp2 = [1]

    delta = diff(y)
    a = 1
    b = 1
    f = 1
    p = [y[mid]]
    for i in range(1, n):
        if i % 2 != 0:
            if a == 1:
                temp1 = H.poly_mul([0], list(temp1))
            else:
                temp1 = H.poly_mul([(a-1), (a-1)], list(temp1))
            if a > mid:
                continue
            f = f * i
            temp1 = ((1/(2*f)) * (delta[mid-a, i] + delta[mid-a+1, i])) * temp1
            p = H.poly_add(p, temp1)
            a = a + 1
        else:
            f = f * i
            temp2 = H.poly_mul([(b-1), (b-1)], list(temp2))
            temp2 = ((1/f) * delta[mid-b, i]) * temp2
            p = H.poly_add(p, temp2)
            b = b + 1

    return p, H.eval_poly(p, t)

def stirling2(x, y, x0):
    n = len(x)
    delta = diff(y)
    idx = 3
    f = 1
    a = 0
    b = 0
    temp1 = [1]
    temp2 = [1]
    h = x[1] - x[0]
    t = (x0 - x[idx]) / h
    p = np.array([y[idx]])
    # print(delta*1e+5)
    for i in range(1, n):
        f = f * i
        if i % 2 != 0:
            if idx - 1 >= 0:
                # print(np.array([delta[idx, i], delta[idx-1, i]]) * 1e+5)
                if i == 1:
                    temp1 = H.poly_mul([0], temp1)
                else:
                    temp1 = H.poly_mul([-a, a], temp1)
                p = H.poly_add(p, temp1 * ((delta[idx, i] + delta[idx-1, i])/(2*f)))
                a += 1
                idx -= 1
            else:
                break
        else:
            # print(delta[idx, i]*1e+5)
            temp2 = H.poly_mul([-b, b], temp2)
            p = H.poly_add(p, temp2 * (delta[idx, i] / f))
            b += 1
    val = H.eval_poly(p, t)
    return p, val



if __name__ == "__main__":
    x = [75, 76, 77, 78, 79, 80, 81, 82, 83, 84]
    y = [2.76806, 2.83267, 2.90256, 2.97857, 3.06173, 3.15339, 3.25530, 3.36987, 3.50042, 3.65186]
    x0 = 78.5
    #delta = diff(y)
    #print("\nBảng sai phân")
    #print(delta)
    #print("\nHệ số của đa thức nội suy")
    #print(coeff)
    #print("\nGiá trị tại ", x0, " là")
    #print(value)
    print(diff(y))
    print("Stirling")
    print(stirling(x, y, x0))
    print(stirling2(x, y, x0))