# -*- coding: utf-8 -*-
"""
Created on Tue May 31 00:34:03 2022

@author: alper
"""
"""
import socket
import os

s = socket.socket(socket.AF_INET, socket.SOCK_STREAM)
s.bind(("localhost", 1002)) #if the clients/server are on different network you shall bind to ('', port)
print("listening...")
s.listen()
c, addr = s.accept()
print('{} connected.'.format(addr))

f = open("image.jpg", "wb")
l = os.path.getsize("image.jpg")
m = f.read(l)
c.send_all(m)
f.close()
print("Done sending...")"""

import socket
import os
from runImage import runImages
from pynput.keyboard import Key, Controller
from threading import Thread

def taken(thread):
    print("listening...")
    server.listen()
    kb = Controller()

    c, addr = server.accept()
    print('{} connected.'.format(addr))
    kb.press(Key.esc)
    kb.release(Key.esc)
    # remove file
    os.remove("./images/image.jpg")
    thread.join()
    f = open("./images/image.jpg", "wb")
    image_chunck = c.recv(2048)
    while image_chunck:
        f.write(image_chunck)
        image_chunck = c.recv(2048)
    f.close()
    thread = Thread(target = runImages, args = ())
    thread.start()
    print("Done receiving...")
    taken(thread)
    
def temp():
    print("temp")
if __name__ == "__main__":
    PORT = 5050
    SERVER = socket.gethostbyname(socket.gethostname())
    ADDR = (SERVER,PORT)
    
    print(SERVER)
    server = socket.socket(socket.AF_INET, socket.SOCK_STREAM)
    server.bind(ADDR) #if the clients/server are on different network you shall bind to ('', port)
    thread = Thread(target = temp, args = ())
    thread.start()
    taken(thread)