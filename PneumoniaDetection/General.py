import numpy as np


import keras
import tensorflow as tf
print("Tensorlfow version = ", tf.__version__)
print("Keras version = ", keras.__version__)

exit(0)
predictedProbabilitiesArr = [0]*2
predictedProbabilitiesArr[0] = 0
predictedProbabilitiesArr[1] = 1

print(str(np.amax([predictedProbabilitiesArr])))


def loadData(a,b):
    print("loadDataa function")

    number = 1
    while number < 11:
        print(number)
        number += 1

    numbers = [1,2,3,4,5,6,7,8,9,10]
    for number in numbers:
        print(number)
