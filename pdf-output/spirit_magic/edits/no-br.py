#!/usr/bin/python

import sys, io
from bs4 import BeautifulSoup

if len(sys.argv) != 2:
    print 'usage: ' + sys.argv[0] + '<verbose html file>'
    sys.exit()

# Make soup
soup = BeautifulSoup(open(sys.argv[1], 'r'))

# Remove all br tags
for br in soup.find_all('br'):
    br.extract()

# Create an output markdown file
with io.open('no-br.html', 'w', encoding='utf-8') as o:
    o.write(soup.prettify())

