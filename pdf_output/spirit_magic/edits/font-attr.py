#!/usr/bin/python

import sys
from bs4 import BeautifulSoup

# This script searches for font attributes.
# Font information will later be used to manipulate the html/xml files.

if len(sys.argv) != 2:
    sys.exit()

infile = open(sys.argv[1], 'r')
text = infile.read()
infile.close()

soup = BeautifulSoup(text)

spans = soup.find_all('span')

styles = []
for span in spans:
    styling = span.attrs
    if 'font-family' in styling['style']:
        if styling['style'] not in styles:
            styles.append(styling['style'])

outfile = open('font-attr.txt', 'w')
outfile.write('replacements = { \\\n')
for style in styles:
    outfile.write('\'' + style + '\' = \'\' \\\n')
outfile.write('}\n')
outfile.close()
# Use the original pdf to determine what <html> tags
# should be used for the present fonts.
# Enter that information into the font_attr.txt file.
#
# The contents of the font-attr.txt file should be copied
# and pasted into the make-legend.py file.
