#https://github.com/sanghvirajit/Medical-Image-Classification-using-CNN/blob/main/Medical_Image_Classification.ipynb

#Fixing issues faced during installation of libraries###
## cv2: https://stackoverflow.com/questions/63732353/error-could-not-build-wheels-for-opencv-python-which-use-pep-517-and-cannot-be
   #pip3 install opencv-python==3.4.13.47

# For tensorflow error: enable LongPathsEnabled in windows registry Editor

import os
import numpy as np
import pandas as pd
import random
import cv2
import matplotlib.pyplot as plt

import keras.backend as K
from keras.models import Model, Sequential
from keras.layers import Input, Dense, Flatten, Dropout, BatchNormalization
from keras.layers import Conv2D, SeparableConv2D, MaxPool2D, LeakyReLU, Activation
#from keras.optimizers import adam
from keras.preprocessing.image import ImageDataGenerator
from keras.callbacks import ModelCheckpoint, ReduceLROnPlateau, EarlyStopping
import tensorflow as tf

######


seed = 232
np.random.seed(seed)
tf.random.set_seed(seed)

#######
path = './chest_xray/'

fig, ax = plt.subplots(2, 3, figsize=(15, 10))
ax = ax.ravel()
plt.tight_layout()

for i, _set in enumerate(['train', 'val', 'test']):
    set_path = path + _set
    ax[i].imshow(plt.imread(set_path + '/NORMAL/' + os.listdir(set_path + '/NORMAL')[0]), cmap='gray')
    ax[i].set_title('Set: {}, Condition: Normal'.format(_set))

    ax[i + 3].imshow(plt.imread(set_path + '/PNEUMONIA/' + os.listdir(set_path + '/PNEUMONIA')[0]), cmap='gray')
    ax[i + 3].set_title('Set: {}, Condition: Pneumonia'.format(_set))

#######

def process_data(img_dims, batch_size):
    # Data generation objects
    #Note: This random transformation may be a source of changing the results thus true violations detection using our statiscal MT approach may not be detected.
    # If we make sure that we provide same training data and test data, we can better analyze the results. So, avoid these transformations

    #train_datagen = image_gen = ImageDataGenerator(
    #    rescale=1. / 255,
    #    shear_range=0.2,
    #    zoom_range=0.2,
    #    horizontal_flip=True,
    #)

    #test_val_datagen = ImageDataGenerator(
    #    rescale=1. / 255
    #)

    train_datagen = ImageDataGenerator()
    test_val_datagen = ImageDataGenerator()

    # This is fed to the network in the specified batch sizes and image dimensions
    train_gen = train_datagen.flow_from_directory(
        directory=path + 'train',
        target_size=(img_dims, img_dims),
        batch_size=batch_size,
        class_mode='binary') #,
        #shuffle=True

    test_gen = test_val_datagen.flow_from_directory(
       directory=path + 'test',
        target_size=(img_dims, img_dims),
        batch_size=batch_size,
        class_mode='binary')#,
        #shuffle=True)

    val_gen = test_val_datagen.flow_from_directory(
        directory=path + 'val',
        target_size=(img_dims, img_dims),
        batch_size=batch_size,
        class_mode='binary')#,
        #shuffle=True)




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

######
img_dims = 150
epochs = 3#10
batch_size = 32

train_gen, test_gen, test_data, test_labels, val_gen = process_data(img_dims, batch_size)

######

## CNN Architecture

inputs = Input(shape=(img_dims, img_dims, 3))

# First conv block
x = Conv2D(filters=16, kernel_size=(3, 3), activation='relu', padding='same')(inputs)
x = Conv2D(filters=16, kernel_size=(3, 3), activation='relu', padding='same')(x)
x = MaxPool2D(pool_size=(2, 2))(x)

# Second conv block
x = SeparableConv2D(filters=32, kernel_size=(3, 3), activation='relu', padding='same')(x)
x = SeparableConv2D(filters=32, kernel_size=(3, 3), activation='relu', padding='same')(x)
x = BatchNormalization()(x)
x = MaxPool2D(pool_size=(2, 2))(x)

# Third conv block
x = SeparableConv2D(filters=64, kernel_size=(3, 3), activation='relu', padding='same')(x)
x = SeparableConv2D(filters=64, kernel_size=(3, 3), activation='relu', padding='same')(x)
x = BatchNormalization()(x)
x = MaxPool2D(pool_size=(2, 2))(x)

# Fourth conv block
x = SeparableConv2D(filters=128, kernel_size=(3, 3), activation='relu', padding='same')(x)
x = SeparableConv2D(filters=128, kernel_size=(3, 3), activation='relu', padding='same')(x)
x = BatchNormalization()(x)
x = MaxPool2D(pool_size=(2, 2))(x)
x = Dropout(rate=0.2)(x)

# Fifth conv block
x = SeparableConv2D(filters=256, kernel_size=(3, 3), activation='relu', padding='same')(x)
x = SeparableConv2D(filters=256, kernel_size=(3, 3), activation='relu', padding='same')(x)
x = BatchNormalization()(x)
x = MaxPool2D(pool_size=(2, 2))(x)
x = Dropout(rate=0.2)(x)

# FC layer
x = Flatten()(x)
x = Dense(units=512, activation='relu')(x)
x = Dropout(rate=0.7)(x)
x = Dense(units=128, activation='relu')(x)
x = Dropout(rate=0.5)(x)
x = Dense(units=64, activation='relu')(x)
x = Dropout(rate=0.3)(x)

# Output layer
output = Dense(units=1, activation='sigmoid')(x)

# Creating model and compiling
model = Model(inputs=inputs, outputs=output)
model.compile(optimizer='adam', loss='binary_crossentropy', metrics=['accuracy'])

# Callbacks
checkpoint = ModelCheckpoint(filepath='best_weights.hdf5', save_best_only=True, save_weights_only=True)
lr_reduce = ReduceLROnPlateau(monitor='val_loss', factor=0.3, patience=2, verbose=2, mode='max')
early_stop = EarlyStopping(monitor='val_loss', min_delta=0.1, patience=1, mode='min')

##############

hist = model.fit(
           train_gen, steps_per_epoch=train_gen.samples // batch_size,
           epochs=epochs, validation_data=test_gen,
           validation_steps=test_gen.samples // batch_size, callbacks=[checkpoint, lr_reduce])

#hist = model.fit(
#           train_gen, steps_per_epoch=train_gen.samples // batch_size,
#           epochs=epochs, callbacks=[checkpoint, lr_reduce])

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

from sklearn.metrics import accuracy_score, confusion_matrix

preds = model.predict(test_data)

acc = accuracy_score(test_labels, np.round(preds))*100
cm = confusion_matrix(test_labels, np.round(preds))
tn, fp, fn, tp = cm.ravel()

print('CONFUSION MATRIX ------------------')
print(cm)

print('\nTEST METRICS ----------------------')
precision = tp/(tp+fp)*100
recall = tp/(tp+fn)*100
print('Accuracy: {}%'.format(acc))
print('Precision: {}%'.format(precision))
print('Recall: {}%'.format(recall))
print('F1-score: {}'.format(2*precision*recall/(precision+recall)))

print('\nTRAIN METRIC ----------------------')
print('Train acc: {}'.format(np.round((hist.history['accuracy'][-1])*100, 2)))

############

fig, ax = plt.subplots(4, 3, figsize=(20, 30))
ax = ax.ravel()
plt.tight_layout()

for i in range(12):
    ax[i].imshow(test_data[i], cmap='gray')

    if test_labels[i] == 0:
        ax[i].set_title(
            '{} % probablity of being NORMAL case'.format((1 - preds[i]) * 100) + '\n' + 'Actual Case: NORMAL')
    else:
        ax[i].set_title(
            '{} % probablity of being PNEUMONIA case'.format(preds[i] * 100) + '\n' + 'Actual Case: PNEUMONIA')

############

fig, ax = plt.subplots(4, 3, figsize=(20, 30))
ax = ax.ravel()
plt.tight_layout()

for i in range(12):
    for j in range(234, 246):
        ax[i].imshow(test_data[j])

        if test_labels[j] == 0:
            ax[i].set_title(
                '{} % probablity of being NORMAL case'.format((1 - preds[j]) * 100) + '\n' + 'Actual Case: NORMAL')
        else:
            ax[i].set_title(
                '{} % probablity of being PNEUMONIA case'.format(preds[j] * 100) + '\n' + 'Actual Case: PNEUMONIA')

############




