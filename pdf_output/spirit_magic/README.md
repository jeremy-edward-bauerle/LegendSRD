## Extracting text from pdf files

legspiritmagic.pdf - Legend Spirit Magic free supplement is [here](http://www.mongoosepublishing.com/pdf/legspiritmagic.pdf)

### poppler utils

Create a text-only dump of the pdf:  
`pdftotext legspiritmagic.pdf pdftotxt.txt`

Create a text-only dump that tries to preserve the layout of the pdf:  
`pdftotext -layout legspiritmagic.pdf pdftotxt-layout.txt`

Quietly (-q) create a single (-s) html file from all pdf pages, ignoring images (-i):  
`pdftohtml -q -s -i legspiritmagic.pdf pdftohtml.html`  
This creates two files: `pdftohtml-html.html` and `pdftohtmls.html`. I don't need pdftohtmls.html and I want to rename pdftohtml-html.html, so...    
`mv pdftohtml-html.html pdftohtml.html && rm pdftohtmls.html`

Quietly (-q) create an xml file of the pdf, ignoring images (-i):  
`pdftohtml -xml -q -i legspiritmagic.pdf pdftohtml.xml`

### pdfminer

Create a text-only dump of the pdf:  
`pdf2txt.py -o pdf2txt.txt legspiritmagic.pdf`

Create an html file of the pdf:  
`pdf2txt.py -o pdf2txt.html legspiritmagic.pdf`

Create a very large xml file of the pdf:  
`pdf2txt.py -o pdf2txt.xml legspiritmagic.pdf`

### HTML Tidy

Make html and xml files pretty *(almost)*

`tidy -i -asxml -utf8 pdf2txt.html > pdf2txt-tidy.html`  
`tidy -i -xml -utf8 pdf2txt-tidy.html > pdf2txt-tidy.xml`  
`tidy -i -asxml -utf8 pdftohtml.html > pdftohtml-tidy.html`

