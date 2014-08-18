## Editing the raw pdf output

The files in this folder are mostly scripts to help edit text files created from the original pdfs.

### Files created 'programmatically'

#### Note to self: this set of steps is ugly and error-prone.  Fix it!

1. run **no-br.py** on the html of the original pdf, creating no-br.html.
2. copy the contents of no-br.html and paste into [dirtymarkup](www.dirtymarkup.com)
3. **Click Clean** using *Output: Code fragment*, *Indent: None*, and check *Add empty lines for clarity*.
4. create and html file from the output of dirtymarkup.
5. run **font-attr.py** on the that file.
6. use the original pdf to fill in the generated **font-attr.txt** with the markup to be used to replace the inline styles.  *font-attr-full.txt* is a complete example.
7. copy the contents of the completed **font-attr.txt** over the similar section of the **make-legend.py** file.
8. run **make-legend.py** on an html file, generating **by-prog.md**.
9. clean up the imperfect markdown file.

