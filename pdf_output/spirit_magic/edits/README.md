## Editing the raw pdf output

The files in this folder are mostly scripts to help edit html and/or xml files that were created from the original pdfs.

### Files created by hand

**by-hand.md** is a hand-edited version of **pdftotext-layout.txt**.  It is run through a markdown interpreter, then html tags are added.

### Files created programmatically

1. run **font-attr.py** on the html/xml of the original pdf.
2. use the original pdf to fill in the generated **font-attr.txt** with the markup to be used to replace the inline styles.  *font-attr-full.txt* is a complete example.
3. copy the contents of the completed **font-attr.txt** over the similar section of the **make-legend.py** file.
4. run **make-legend.py** on an html file, generating **by-prog.md**.

