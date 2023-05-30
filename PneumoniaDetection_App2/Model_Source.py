#https://github.com/Karan-Malik/ConvNet

#Fixing issues faced during installation of libraries###
## cv2: https://stackoverflow.com/questions/63732353/error-could-not-build-wheels-for-opencv-python-which-use-pep-517-and-cannot-be
   #pip3 install opencv-python==3.4.13.47

#https://stackoverflow.com/questions/62812563/keras-prediction-returns-only-one-class-in-binary-problem

# For tensorflow error: enable LongPathsEnabled in windows registry Editor

import os
import numpy as np
import pandas as pd
import random
import cv2
import matplotlib.pyplot as plt

import keras.backend as K
import keras
from keras.models import Model, Sequential
from keras.layers import Input, Dense, Flatten, Dropout, BatchNormalization
from keras.layers import Conv2D, SeparableConv2D, MaxPool2D, LeakyReLU, Activation
from keras.layers import Conv2D,MaxPooling2D,Flatten,Dropout,Dense,BatchNormalization,SpatialDropout2D
#from keras.optimizers import adam
from keras.preprocessing.image import ImageDataGenerator
from keras.callbacks import ModelCheckpoint, ReduceLROnPlateau, EarlyStopping
import tensorflow as tf
from sklearn.metrics import accuracy_score, confusion_matrix

#Writing in Excel
import xlwt
from xlwt import Workbook

from datetime import datetime

import os
import random as rn
######

# Image Directory
path = './chest_xray/'

###### Hyperparameters####
img_dims = 128 #150
epochs = 10
batch_size = 16

class ImageClassifier:



    def loadData(self,img_dims, batch_size):
        # Data generation objects
        train_datagen = image_gen = ImageDataGenerator(
            rescale=1./255,
            shear_range=0.2,
            zoom_range=0.2,
            horizontal_flip=True,
        )

        test_val_datagen = ImageDataGenerator(
            rescale=1./255
        )

        # This is fed to the network in the specified batch sizes and image dimensions
        train_gen = train_datagen.flow_from_directory(
            directory=path + 'train',
            target_size=(img_dims, img_dims),
            batch_size=batch_size,
            class_mode='binary')  # ,
        # shuffle=True

        test_gen = test_val_datagen.flow_from_directory(
            directory=path + 'test',
            target_size=(img_dims, img_dims),
            batch_size=batch_size,
            class_mode='binary')  # ,
        # shuffle=True)

        val_gen = test_val_datagen.flow_from_directory(
            directory=path + 'val',
            target_size=(img_dims, img_dims),
            batch_size=batch_size,
            class_mode='binary')  # ,
        # shuffle=True)

        test_data = []
        test_labels = []

        for cond in ['/NORMAL/', '/PNEUMONIA/']:
            for img in (os.listdir(path + 'test' + cond)):
                img = plt.imread(path + 'test' + cond + img)
                img = cv2.resize(img, (img_dims, img_dims))
                img = np.dstack([img, img, img])
                img = img.astype('float32') / 255
                if cond == '/NORMAL/':
                    label = 0
                elif cond == '/PNEUMONIA/':
                    label = 1
                test_data.append(img)
                test_labels.append(label)

        test_data = np.array(test_data)
        test_labels = np.array(test_labels)

        return train_gen, test_gen, test_data, test_labels, val_gen


    def buildCNNModelAndMakePrediction(self,fixRandomSeed = False,fileName=None,classLabel=''):

        if (fixRandomSeed):
            # Important Note: To show that although the software tester tried to fix the random seed but still we don't get deterministic results; thus, traditional MT technique can not be used
            print("Fixed Random Seed = True")
            self.setSession()

        # Load Data
        train_gen, test_gen, test_data, test_labels, val_gen = self.loadData(img_dims, batch_size)

        ## CNN Architecture
        model = Sequential()

        # First conv block
        model.add(Conv2D(32, (3, 3), padding='same', activation='relu', input_shape=(img_dims, img_dims, 3)))
        model.add(MaxPooling2D(2, 2))
        model.add(SpatialDropout2D(0.1))

        # Second conv block
        model.add(Conv2D(32, (3, 3), padding='same', activation='relu'))
        model.add(MaxPooling2D(2, 2))
        model.add(BatchNormalization())
        model.add(SpatialDropout2D(0.2))

        # Third conv block
        model.add(Conv2D(32, (3, 3), padding='same', activation='relu'))
        model.add(MaxPooling2D(2, 2))
        model.add(BatchNormalization())
        model.add(SpatialDropout2D(0.2))

        # Fourth conv block
        model.add(Conv2D(32, (3, 3), padding='same', activation='relu'))
        model.add(MaxPooling2D(2, 2))
        model.add(BatchNormalization())
        model.add(SpatialDropout2D(0.3))

        # Fifth conv block
        model.add(Conv2D(32, (3, 3), padding='same', activation='relu'))
        model.add(MaxPooling2D(2, 2))
        model.add(BatchNormalization())
        model.add(SpatialDropout2D(0.3))

        # Six conv block
        model.add(Conv2D(32, (3, 3), padding='same', activation='relu'))
        model.add(MaxPooling2D(2, 2))
        model.add(BatchNormalization())
        model.add(SpatialDropout2D(0.3))

        # Seventh conv block
        model.add(Conv2D(32, (3, 3), padding='same', activation='relu'))
        model.add(MaxPooling2D(2, 2))
        model.add(BatchNormalization())
        model.add(SpatialDropout2D(0.5))

        # FC Layer
        model.add(Flatten())
        model.add(Dropout(0.5))
        model.add(Dense(units=img_dims, activation='relu'))
        model.add(Dropout(0.5))
        model.add(Dense(units=img_dims, activation='relu'))
        model.add(Dropout(0.5))

        #Output Layer
        model.add(Dense(units=1, activation='sigmoid'))

        #adam = tf.keras.optimizers.Adam(learning_rate=0.001)
        model.compile(optimizer='adam', loss='binary_crossentropy', metrics=['accuracy'])

        # Callbacks
        checkpoint = ModelCheckpoint(filepath='best_weights.hdf5', save_best_only=True, save_weights_only=True)
        lr_reduce = ReduceLROnPlateau(monitor='val_loss', factor=0.3, patience=2, verbose=2, mode='max')
        early_stop = EarlyStopping(monitor='val_loss', min_delta=0.1, patience=1, mode='min')


        ##############

        model.fit(
            train_gen, steps_per_epoch=train_gen.samples // batch_size,
            epochs=epochs, validation_data=val_gen,
            validation_steps=val_gen.samples // batch_size, callbacks=[checkpoint, lr_reduce])


        preds = model.predict(test_data)

        #print("Preds = ", preds)

        return test_data, test_labels, preds

    def drawTrainValidationLoss(self):
        test_labels, hist, preds = self.buildCNNModelAndMakePrediction()
        ###########
        fig, ax = plt.subplots(1, 2, figsize=(10, 5))
        ax = ax.ravel()

        for i, met in enumerate(['accuracy', 'loss']):
            ax[i].plot(hist.history[met])
            ax[i].plot(hist.history['val_' + met])

            ax[i].set_title('Model {}'.format(met))

            ax[i].set_xlabel('epochs')
            ax[i].set_ylabel(met)

            ax[i].legend(['train', 'val'])

        ##########

    def checkCNNModelPerformance(self,test_labels, preds,fileName):

        acc = accuracy_score(test_labels, np.round(preds)) * 100
        cm = confusion_matrix(test_labels, np.round(preds))
        tn, fp, fn, tp = cm.ravel()

        print('CONFUSION MATRIX ------------------')
        print(cm)

        print('\nTEST METRICS ----------------------')
        precision = tp / (tp + fp) * 100
        recall = tp / (tp + fn) * 100
        print('Accuracy: {}%'.format(acc))
        print('Precision: {}%'.format(precision))
        print('Recall: {}%'.format(recall))
        print('F1-score: {}'.format(2 * precision * recall / (precision + recall)))

        #print('\nTRAIN METRIC ----------------------')
        #print('Train acc: {}'.format(np.round((hist.history['accuracy'][-1]) * 100, 2)))

    def setSession(self):
        # Sets session for deterministic results
        # https://keras.io/getting-started/faq/#how-can-i-obtain-reproducible-results-using-keras-during-development

        # The below is necessary in Python 3.2.3 onwards to
        # have reproducible behavior for certain hash-based operations.
        # See these references for further details:
        # https://docs.python.org/3.4/using/cmdline.html#envvar-PYTHONHASHSEED
        # https://github.com/keras-team/keras/issues/2280#issuecomment-306959926
        #Faqeer: os.environ['PYTHONHASHSEED'] = '0'

        # The below is necessary for starting Numpy generated random numbers
        # in a well-defined initial state.
        seed = 232
        np.random.seed(seed)
        tf.random.set_seed(seed)
        # The below is necessary for starting core Python generated random numbers
        # in a well-defined state.
        #Faqeer: rn.seed(seed)
        # Force TensorFlow to use single thread.
        # Multiple threads are a potential source of
        # non-reproducible results.
        # For further details, see: https://stackoverflow.com/questions/42022950/which-seeds-have-to-be-set-where-to-realize-100-reproducibility-of-training-res
        #Faqeer: session_conf = tf.compat.v1.ConfigProto(intra_op_parallelism_threads=1, inter_op_parallelism_threads=1)

        # The below tf.set_random_seed() will make random number generation
        # in the TensorFlow backend have a well-defined initial state.
        # For further details, see: https://www.tensorflow.org/api_docs/python/tf/set_random_seed
        # tf.global_variables_initializer()

        #Faqeer: tf.compat.v1.set_random_seed(seed)
        #Faqeer: sess = tf.compat.v1.Session(graph=tf.compat.v1.get_default_graph(), config=session_conf)

        # Fixed by Faqeer ur Rehman on 24 Nov 2019
        # K.set_session(sess)
        #Faqeer: tf.compat.v1.keras.backend.set_session(sess)

    def saveResults(self,test_data, test_labels,preds,iterationNo,wb):

        # =====create sheet and add headers====
        sheetToRecordInstanceLevelOutput = wb.add_sheet('IterationNo' + str(iterationNo))
        sheetToRecordInstanceLevelOutput.write(0, 0, 'Expected_OR_ActualOutput')
        sheetToRecordInstanceLevelOutput.write(0, 1, 'PredictedOutput')
        sheetToRecordInstanceLevelOutput.write(0, 2, 'PredictedProbabilities')
        sheetToRecordInstanceLevelOutput.write(0, 3, 'MaxProbability')

        startRowToBeInserted = 1 # Add new data starting from row 1
        predictedProbabilitiesArr = [0]*2

        for x in range(test_data.shape[0]):
            expectedClassLabel = test_labels[x]
            predictedClassLabel = 0
            predictedClassProbabilities=1
            predictedClass0Probability=0
            predictedClass1Probability=0
            if preds[x] > 0.5:
                predictedClassLabel = 1
            else:
                predictedClassLabel = 0

            predictedClass1Probability = preds[x] # As it only contains the probabilities for first class (i.e., class 0)
            predictedClass0Probability = 1 - predictedClass1Probability

            predictedProbabilitiesArr[0] = predictedClass0Probability
            predictedProbabilitiesArr[1] = predictedClass1Probability

            predictedClassProbabilities = str(predictedClass0Probability) + "," + str(predictedClass1Probability)

            #print("Expected Class Label = ", expectedClassLabel)
            #print("Predicted Class Label = ", predictedClassLabel)
            #print("Predicted Class probabilities = ", predictedClassProbabilities)

            sheetToRecordInstanceLevelOutput.write(startRowToBeInserted, 0, str(expectedClassLabel))
            sheetToRecordInstanceLevelOutput.write(startRowToBeInserted, 1, str(predictedClassLabel))
            sheetToRecordInstanceLevelOutput.write(startRowToBeInserted, 2, str(predictedClassProbabilities))
            sheetToRecordInstanceLevelOutput.write(startRowToBeInserted, 3, str(np.amax([predictedProbabilitiesArr])))
            startRowToBeInserted = startRowToBeInserted + 1




    def executeCNNModelUnderTest(self,wb,fileNameToSaveTheResults):

        for iterationNo in range(1, 6):  # 31: Number of times we want to build Model and storing the results
            print("Iteration No = ", iterationNo)
            # Build CNN Model
            imageClassifier = ImageClassifier()
            # Important Note: To show that although the software tester tried to fix the random seed but still we don't get deterministic results; thus, traditional MT technique can not be used
            test_data, test_labels, preds = imageClassifier.buildCNNModelAndMakePrediction(fixRandomSeed=True)
            # Make Predictions
            imageClassifier.checkCNNModelPerformance(test_labels, preds, fileNameToSaveTheResults)

            # Save the results in Excel
            imageClassifier.saveResults(test_data, test_labels, preds,iterationNo,wb)

        if fileNameToSaveTheResults != None:
            wb.save(fileNameToSaveTheResults)  # .xls
        else:
            wb.save("Default.xls")  # .xls

if __name__ == '__main__':

    startTime = datetime.now() #Time when the process starts

    imageClassifier = ImageClassifier()

    # MR: Consistent prediction
    # Create Workbook
    wbSource = Workbook()
    imageClassifier.executeCNNModelUnderTest(wb=wbSource,fileNameToSaveTheResults="Source.xls")



    #wbFollowup = Workbook()
    #imageClassifier.executeCNNModelUnderTest(wb=wbFollowup,fileNameToSaveTheResults="testFollowup.xls")
    #imageClassifier.executeCNNModelUnderTest(wb=wbFollowup, fileNameToSaveTheResults="test.xls")

    endTime = datetime.now() #Time when the process ends

    print("Start time =", startTime)
    print("End time =", endTime)













