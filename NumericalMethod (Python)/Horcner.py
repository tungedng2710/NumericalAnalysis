import numpy as np

def mul(point):
    n = len(point)
    multi = np.zeros(n + 1)
    temp = np.zeros(n + 1)
    for k in range(n):
        if (k == 0):
            multi[0] = temp[0] = 1
            multi[1] = temp[1] = -point[0]
        else:
            for i in range (1, k + 2): #i tu 1 den k+1
                multi[i] = temp[i] - point[k] * temp[i - 1]
            for i in range (1, k + 2):
                temp[i] = multi[i]
    return multi

def div(ts, c):
    n = len(ts)
    kq = np.zeros(n)
    for i in range (0, n):
        if (i == 0):
            kq[i] = ts[i]
        else:
            kq[i] = kq[i - 1] * c + ts[i]
    return kq

def poly_mul(vals, p=[]):
    n = len(vals)
    if len(p) > 0:
        m = len(p)
        result = np.zeros(m + n)
        result[:m] = p
        for i in range(0, n):
            temp = np.zeros(len(result[:i+m+1]))
            for j in range(1, len(temp)):
                temp[j] = -vals[i] * result[j-1]
            result[:i+m+1] = result[:i+m+1] + temp

    else:
        result = np.zeros(n+1)
        result[0] = 1
        result[1] = -vals[0]
        for i in range(1, n):
            temp = np.zeros(len(result[:i+2]))
            for j in range(1, len(temp)):
                temp[j] = -vals[i] * result[j-1]
            result[:i+2] = result[:i+2] + temp

    return result

# Cong da thuc
def poly_add(p1, p2):
    p1 = np.array(p1)
    p2 = np.array(p2)
    n = len(p1)
    m = len(p2)
    if n > m:
        for i in range(m):
            p1[n-i-1] = p1[n-i-1] + p2[m-i-1]
        # p1[n-m:] = p1[n-m:] + p2
        return p1
    elif m > n:
        for i in range(n):
            p2[m-i-1] = p2[m-i-1] + p1[n-i-1]
        # p2[m-n:] = p2[m-n:] + p1
        return p2
    else:
        return p1+p2


# Tinh gia tri da thuc tai x
def eval_poly(p, x):
    result = p[0]
    for i in range(1, len(p)):
        result = p[i] + x*result
    return result


x = [1, -2]
x1 = np.zeros(2)
x1[0] = 1
x1[1] = -2
p = mul(x)
print(p)
p1 = poly_mul(x1)
print(p1)
