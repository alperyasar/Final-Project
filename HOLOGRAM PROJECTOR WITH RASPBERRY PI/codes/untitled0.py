# -*- coding: utf-8 -*-
"""
Created on Tue May 31 18:01:34 2022

@author: alper
"""

# -*- coding: utf-8 -*-
"""
Created on Tue Apr 26 20:02:48 2022

@author: alper


çalışan kod Düzenlemesini yap
"""
import numpy as np
import cv2
from screeninfo import get_monitors
import os
from threading import Thread, Lock



def runImages():
    screen = get_monitors()[0]
    print(screen)
    width, height = screen.width, screen.height
    path = "./images/"
    dir_list = os.listdir(path)
    newW, newH = int(width/2), int(height/2)
#    image = np.zeros((height, width, 3), dtype=np.float64)
#    image[:, :] = 0  # black screen    
    path = "./images/" + dir_list[0]
    img = cv2.imread(path)
    w = int(width / 8)
    h = int(height/5)
    new_img = cv2.resize(img, (w, h))
    image = np.zeros((height, width, 3), dtype=np.uint8)
    img0 = img1 = img2 = img3 = new_img
    img1 = cv2.rotate(img1, cv2.ROTATE_90_CLOCKWISE)
    img2 = cv2.rotate(img2, cv2.ROTATE_180)
    img3 = cv2.rotate(img3, cv2.ROTATE_90_COUNTERCLOCKWISE)

    w = int(img0.shape[1])
    w1=int(w/2)
    h = int(img0.shape[0])
    print(newW-w1+w - (newW-w1))
    image[(newH-150-h):(newH-150),newW-w1:newW-w1+w] = img0
    
    image[newH-w1:newH-w1+w,(newW+150):(newW+150+h)] = img1
    
    image[(newH+150):(newH+150+h),newW-w1:newW-w1+w] = img2
    
    image[newH-w1:newH-w1+w,(newW-150-h):(newW-150)] = img3
    """    p = 0.25
    w = int(img.shape[1])
    h = int(img.shape[0])
    new_img = cv2.resize(img, (w, h))
    image[(newH-10-h):(newH-10), w:w+w] = img"""
    
    window_name = 'projector'
    cv2.namedWindow(window_name, cv2.WND_PROP_FULLSCREEN)
    cv2.moveWindow(window_name, screen.x - 1, screen.y - 1)
    cv2.setWindowProperty(window_name, cv2.WND_PROP_FULLSCREEN,
    cv2.WINDOW_FULLSCREEN)
    cv2.imshow(window_name, image)
    global mutex
    print("alper")
    cv2.destroyAllWindows()
    