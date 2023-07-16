# -*- coding: utf-8 -*-
"""
Created on Tue May 31 00:34:47 2022

@author: alper
"""

import socket
import sys

arg=sys.argv[1]
PORT = 5050
SERVER = "192.168.1.105"
ADDR = (SERVER,PORT)
s = socket.socket(socket.AF_INET, socket.SOCK_STREAM)
s.connect(ADDR) # here you must past the public external ipaddress of the server machine, not that local address
print("alper")
f = open("image.jpg", "rb")
image_data = f.read(2048)
i = 0
while image_data:
    i +=1
    print(i)
    s.send(image_data)
    image_data = f.read(2048)
a = b'0'
print(a)

f.close()
s.close()
# s.close()
print("Done sending")