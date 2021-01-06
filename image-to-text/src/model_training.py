import tensorflow as tf
import cv2
import os
import pickle
from sklearn.model_selection import train_test_split
import numpy as np
import matplotlib.pyplot as plt
from tensorflow.keras.utils import to_categorical
from tensorflow.keras.models import Sequential
from tensorflow.keras.preprocessing.image import ImageDataGenerator
from tensorflow.keras.layers import Conv2D, MaxPooling2D, Dropout, Flatten, Dense
from tensorflow.keras.optimizers import Adam

############################
path_training_dataset = '../resources/training/'
images = []
classNo = []
testRatio = 0.1
batch_size_val = 32
epochs_val = 30
steps_per_epoch = 512
############################

dir_list = os.listdir(path_training_dataset)
print(dir_list)

dig = os.listdir(path_training_dataset + dir_list[0])
lwc = os.listdir(path_training_dataset + dir_list[1])
upc = os.listdir(path_training_dataset + dir_list[2])

print(dig)
print(upc)

noOfClasses = len(dig) + len(upc)
classes = dig + upc
print(len(classes))
class2index = {}

f = open("../class-mapping.txt", "r")
for x in f:
    print(x)
    key, val = x.split()
    class2index[chr(int(val))] = int(key)

print(classes)
print(class2index)
print('Total number of classes: ' + str(noOfClasses))

print('Loading...')
for x in dig:
    myPicsList = os.listdir(path_training_dataset + str(dir_list[0]) + '/' + x)
    for y in myPicsList:
        curImg = cv2.imread(path_training_dataset + str(dir_list[0]) + '/' + x + '/' + str(y))
        curImg = cv2.resize(curImg, (32, 32))
        images.append(curImg)
        classNo.append(class2index[x])
    print(x, end=' ')
print()

# for x in lwc:
#     myPicsList = os.listdir(path_resources + str(dir_list[1]) + '/' + x)
#     for y in myPicsList:
#         curImg = cv2.imread(path_resources + str(dir_list[1]) + '/' + x + '/' + str(y))
#         curImg = cv2.resize(curImg, (32, 32))
#         images.append(curImg)
#         classNo.append(class2index[x])
#     print(x, end=' ')
# print()

for x in upc:
    myPicsList = os.listdir(path_training_dataset + str(dir_list[2]) + '/' + x)
    for y in myPicsList:
        curImg = cv2.imread(path_training_dataset + str(dir_list[2]) + '/' + x + '/' + str(y))
        curImg = cv2.resize(curImg, (32, 32))
        images.append(curImg)
        classNo.append(class2index[x])
    print(x, end=' ')
print()

print(len(images))
print(len(classNo))

images = np.array(images)
classNo = np.array(classNo)

print(images.shape)
print(classNo.shape)

#######Spliting model#######
x_train, x_test, y_train, y_test = train_test_split(images, classNo, test_size=testRatio)
x_train, x_validation, y_train, y_validation = train_test_split(x_train, y_train, test_size=testRatio)

noOfSamples = []
for x in classes:
    if len(np.where(y_train == class2index[x])[0]) > 0:
        noOfSamples.append(len(np.where(y_train == class2index[x])[0]))
print(noOfSamples)

plt.figure(figsize=(10, 5))
plt.bar(classes, noOfSamples)
plt.title("Number of images per class")
plt.xlabel("Class ID")
plt.ylabel("Number of images")
plt.show()


def preProcessing(img):
    img = cv2.cvtColor(img, cv2.COLOR_BGR2GRAY)
    img = cv2.equalizeHist(img)
    img = img / 255
    return img


x_train = np.array(list(map(preProcessing, x_train)))
x_test = np.array(list(map(preProcessing, x_test)))
x_validation = np.array(list(map(preProcessing, x_validation)))

print(x_train.shape)
x_train = x_train.reshape(x_train.shape[0], x_train.shape[1], x_train.shape[2], 1)
x_test = x_test.reshape(x_test.shape[0], x_test.shape[1], x_test.shape[2], 1)
x_validation = x_validation.reshape(x_validation.shape[0], x_validation.shape[1], x_validation.shape[2], 1)
print(x_train.shape)

print("####################")

dataGen = ImageDataGenerator(width_shift_range=0.1,
                             height_shift_range=0.1,
                             zoom_range=0.2,
                             shear_range=0.1,
                             rotation_range=10)
dataGen.fit(x_train)

y_train = to_categorical(y_train, noOfClasses)
y_test = to_categorical(y_test, noOfClasses)
y_validation = to_categorical(y_validation, noOfClasses)


def convModel():
    numberOfFilters = 60
    sizeOfFilter1 = (5, 5)
    sizeOfFilter2 = (3, 3)
    sizeOfPool = (2, 2)
    numberOfNodes = 500
    model = Sequential()
    model.add(Conv2D(numberOfFilters, sizeOfFilter1, input_shape=(32, 32, 1), activation="relu"))
    model.add(Conv2D(numberOfFilters, sizeOfFilter1, activation="relu"))
    model.add(MaxPooling2D(pool_size=sizeOfPool))
    model.add(Conv2D(numberOfFilters // 2, sizeOfFilter2, activation="relu"))
    model.add(Conv2D(numberOfFilters // 2, sizeOfFilter2, activation="relu"))
    model.add(MaxPooling2D(pool_size=sizeOfPool))
    model.add(Dropout(0.5))

    model.add(Flatten())
    model.add(Dense(numberOfNodes, activation="relu"))
    model.add(Dropout(0.5))
    model.add(Dense(noOfClasses, activation="softmax"))
    model.compile(Adam(lr=0.001), loss='categorical_crossentropy', metrics=['accuracy'])
    return model


model = convModel()
print(model.summary())

history = model.fit_generator(dataGen.flow(
    x_train, y_train,
    batch_size=batch_size_val),
    steps_per_epoch=steps_per_epoch,
    epochs=epochs_val,
    validation_data=(x_validation, y_validation),
    shuffle=1
)

plt.figure(1)
plt.plot(history.history['loss'])
plt.plot(history.history['val_loss'])
plt.legend('training', 'validation')
plt.title('Loss')
plt.xlabel('epoch')

plt.figure(2)
plt.plot(history.history['accuracy'])
plt.plot(history.history['val_accuracy'])
plt.legend('training', 'validation')
plt.title('accuracy')
plt.xlabel('epoch')

plt.show()
score = model.evaluate(x_test, y_test, verbose=0)
print('Test score = ', score[0])
print('Test accuracy = ', score[1])

model.save('.\\saved_model\\my_model')

