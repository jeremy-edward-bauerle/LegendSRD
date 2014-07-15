#!/usr/bin/python

import sys
from bs4 import BeautifulSoup

if len(sys.argv) != 2:
    sys.exit()

infile = open(sys.argv[1], 'r')
text = infile.read()

soup = BeautifulSoup(text)

spans = soup.find_all('span')

styles = []
for span in spans:
    styling = span.attrs
    if 'font-family' in styling['style']:
        if styling['style'] not in styles:
            styles.append(styling['style'])

outfile = open('used_fonts.txt', 'w')
for style in styles:
    outfile.write(style + '\n')


