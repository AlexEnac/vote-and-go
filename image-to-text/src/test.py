import cv2
import tensorflow as tf
import os
import numpy as np
import matplotlib.pyplot as plt

test_file = '../resources/testing/1.jpg'
temp_files_path = '../resources/testing/temp/'
if not os.path.exists(temp_files_path):
    os.mkdir(temp_files_path)

class_idx2val = {}

f = open("../class-mapping.txt", "r")
for x in f:
    print(x)
    key, val = x.split()
    class_idx2val[int(key)] = chr(int(val))
print(class_idx2val)

model = tf.keras.models.load_model(".\\saved_model\\my_model")

input_image = cv2.imread(test_file)

input_image = cv2.cvtColor(input_image, cv2.COLOR_RGB2GRAY)
input_image = cv2.resize(input_image, (1500, 600))

ret, thresh = cv2.threshold(input_image, 127, 255, 0)
contours, hierarchy = cv2.findContours(thresh, cv2.RETR_TREE, cv2.CHAIN_APPROX_SIMPLE)

contours = sorted(contours, key=cv2.contourArea)

BLACK_THRESHOLD = 200
THIN_THRESHOLD = 9

contours = sorted(contours, key=lambda ctr: cv2.boundingRect(ctr)[0] + cv2.boundingRect(ctr)[1] * input_image.shape[1])
idx = 0
for cnt in contours:
    idx += 1
    x, y, w, h = cv2.boundingRect(cnt)
    roi = input_image[y:y + h, x:x + w]
    if h < THIN_THRESHOLD or w < THIN_THRESHOLD:
        # contours.remove(cnt)
        continue
    if idx != len(contours):
        cv2.imwrite(temp_files_path + str(idx) + '.png', roi)
        cv2.rectangle(input_image, (x, y), (x + w, y + h), (200, 0, 0), 2)

print(contours)

cv2.imshow('img', input_image)
cv2.waitKey(0)


def remove_temp():
    for file in os.listdir(temp_files_path):
        os.remove(file)
    os.remove(temp_files_path)


pics_to_predict = os.listdir(temp_files_path)
print(pics_to_predict)


def preProcessing(img):
    img = cv2.cvtColor(img, cv2.COLOR_BGR2GRAY)
    img = cv2.equalizeHist(img)
    img = img / 255
    return img


for pic in pics_to_predict:
    imag = cv2.imread(temp_files_path + pic)
    # cv2.imshow('a', img)
    # cv2.waitKey(0)
    # break
    imag = np.array(imag)
    imag = cv2.resize(imag, (32, 32))
    imag = preProcessing(imag)
    imag = imag.reshape(1, 32, 32, 1)
    class_idx = int(model.predict_classes(imag))
    print(pic + ' ' + class_idx2val[class_idx])
