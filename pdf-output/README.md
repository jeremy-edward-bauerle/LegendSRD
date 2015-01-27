This directory contains text output from various pdf parsing tools.  
*Warning* - Some files are very long, and Github might give you trouble if you try to view them online.  Downloading the files and viewing them locally might be easier.

## How text is extracted

The examples below show the commands issued to create the text dumps of the pdfs. The examples use the Legend Spirit Magic free supplement found [here](http://www.mongoosepublishing.com/pdf/legspiritmagic.pdf) - legspiritmagic.pdf

### poppler utils

Create a text-only dump of the pdf:  
`pdftotext legspiritmagic.pdf pdftotext.txt`

Create a text-only dump that tries to preserve the layout of the pdf:  
`pdftotext -layout legspiritmagic.pdf pdftotext-layout.txt`

Quietly (-q) create a single (-s) html file from all pdf pages, ignoring images (-i):  
`pdftohtml -q -s -i legspiritmagic.pdf pdftohtml.html`  
This creates two files: `pdftohtml-html.html` and `pdftohtmls.html`. pdftohtmls.html contains a list of the pdf's table of contents, while pdftohtml-html.html contains the rest of the text.

Quietly (-q) create an xml file of the pdf, ignoring images (-i):  
`pdftohtml -xml -q -i legspiritmagic.pdf pdftohtml.xml`

### pdfminer

Create a text-only dump of the pdf:  
`pdf2txt.py -o pdf2txt.txt legspiritmagic.pdf`

Create an html file of the pdf:  
`pdf2txt.py -o pdf2txt.html legspiritmagic.pdf`

Create a very large xml file of the pdf:  
`pdf2txt.py -o pdf2txt.xml legspiritmagic.pdf`  
(I've often omitted this command because the file produced is so large.)

