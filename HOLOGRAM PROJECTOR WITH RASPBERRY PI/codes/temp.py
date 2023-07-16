# -*- coding: utf-8 -*-
"""
Created on Fri Jun  3 12:47:14 2022

@author: alper
"""

import os
 
fileitem = form['filename']
 
# check if the file has been uploaded
if fileitem.filename:
    # strip the leading path from the file name
    fn = os.path.basename(fileitem.filename)