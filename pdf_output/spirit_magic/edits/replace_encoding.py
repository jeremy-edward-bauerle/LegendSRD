#!/usr/bin/python

import sys

replacements = {                \
'&Acirc;&frac14;' : '1/4',      \
'&Acirc;&frac12;' : '1/2',      \
'&Acirc;&frac34;' : '3/4',      \
'&acirc;&euro;&ldquo;' : '\'',  \
'&acirc;&euro;&rdquo;' : '\'',  \
'&acirc;&euro;&trade;' : '\'',  \
'&acirc;&euro;&tilde;' : '\'',  \
'&Acirc;&copy;' : 'copyright',  \
'&iuml;&not;' : 'fi',           \
'&acirc;&euro;&brvbar;' : ':',  \
'&iuml;&not;&sbquo;' : 'fl',    \
'&acirc;&euro;&cent;' : '-',    \
'&acirc;&euro;&oelig;' : '"',   \
'&acirc;&euro;' : '"',          \
'&oelig:' : '"'                 \
}

if len(sys.argv) != 2:
    print 'usage: ' + sys.argv[0] + '<file to decode>'
    sys.exit()

infile = open(sys.argv[1], 'r')
outfile = open('decoded.xml', 'w')

for line in infile:
    for bad, good in replacements.iteritems():
        line = line.replace(bad, good)
    outfile.write(line)

infile.close()
outfile.close()

