#!/usr/bin/python

import sys, io
from bs4 import BeautifulSoup, NavigableString

# Paste over this dictionary with updated font information
replacements = { \
'font-family: MBSAWV+ImperatorSmallCaps; font-size:74px' : '',      \
'font-family: MBSAWV+ImperatorSmallCaps; font-size:34px' : '',      \
'font-family: MBSAWV+AjiHand; font-size:10px' : '',                 \
'font-family: XCFOUZ+AGaramond-Regular; font-size:10px' : 'p',      \
'font-family: XCFOUZ+AGaramond-Regular; font-size:7px' : '',        \
'font-family: CFNDMP+AGaramond-Italic; font-size:9px' : '',         \
'font-family: XCFOUZ+AGaramond-Regular; font-size:8px' : '',        \
'font-family: MBSAWV+ImperatorSmallCaps; font-size:35px' : 'h1',    \
'font-family: MCLLWV+Hultog; font-size:20px' : 'h2',                \
'font-family: CFNDMP+AGaramond-Italic; font-size:10px' : 'i',       \
'font-family: XCFOUZ+AGaramond-Regular; font-size:20px' : '',       \
'font-family: MCLLWV+Hultog; font-size:17px' : 'h3',                \
'font-family: WEJTGB+HeroQuestCoreRunes; font-size:7px' : '-',      \
'font-family: WEJTGB+AGaramond-BoldItalic; font-size:13px' : 'h4',  \
'font-family: WEJTGB+AGaramond-Bold; font-size:10px' : 'th',        \
'font-family: MBSAWV+AjiHand; font-size:13px' : 'h4',               \
'font-family: XCFOUZ+AGaramond-Regular; font-size:11px' : 'td',     \
'font-family: RCUKOL+TimesNewRomanPS; font-size:10px' : 'td',       \
'font-family: HFWHEF+TimesNewRomanPS-Italic; font-size:10px' : 'i', \
'font-family: CFNDMP+AGaramond-Italic; font-size:11px' : 'i',       \
'font-family: MBSAWV+ImperatorSmallCaps; font-size:26px' : 'h1',    \
'font-family: HFWHEF+TimesNewRomanPS-Italic; font-size:9px' : 'i',  \
'font-family: RCUKOL+TimesNewRomanPS; font-size:9px' : 'p',         \
'font-family: BESSYR+TimesNewRomanPS-Bold; font-size:10px' : 'b',   \
'font-family: BESSYR+TimesNewRomanPS-Bold; font-size:11px' : 'th',  \
'font-family: RCUKOL+TimesNewRomanPS; font-size:16px' : 'h3'        \
}

if len(sys.argv) != 2:
    print 'usage: ' + sys.argv[0] + '<verbose html file>'
    sys.exit()

# Read the html file
infile = open(sys.argv[1], 'r')
html = infile.read()

# Create an output markdown file
outfile = io.open('output.md', 'w', encoding='utf8')

# Make soup
soup = BeautifulSoup(html)

# Remove all br tags
for br in soup.find_all('br'):
    print 'found one'
    br.extract()

# Information that I care about is only present in span tags
# Find all the span tags
for span in soup.find_all('span'):
    style = span['style']
    
    # Check to see if the span contains the styles we care about
    if style in replacements:
        text = ""
        # Are there multiple unicode strings?
        if len(span.contents) > 1:
            # Concatenate them
            for s in span.contents:
                text += s
        # Is the span empty?
        elif len(span.contents) == 0:
            continue
        else:
            text = span.string
        # -----------------------------------------------------------
        # Use the replacement dictionary information to make markdown
        # from the 'text' variable.
        #

        # <p> tags need no markdown
        if replacements[style] == 'p':
            pass
        # <h1> tags get #
        elif replacements[style] == 'h1':
            text = '# ' + text
        # <h2> tags get ##
        elif replacements[style] == 'h2':
            text = '## ' + text
        # <h3> tags get ###
        elif replacements[style] == 'h3':
            text = '### ' + text
        # <h4> tags get ####
        elif replacements[style] == 'h4':
            text = '#### ' + text
        # italics get * *
        elif replacements[style] == 'i':
            text = '*' + text + '*'
        # bold gets ** **
        elif replacements[style] == 'b':
            text = '**' + text + '**'
        # Tables are wacky - ignore them for now
        elif replacements[style] == 'th' or replacements[style] == 'td':
            pass
        # Throw away text from other styles.
        else:
            continue
        # End of Markdown replacement
        # -----------------------------------------------------------
        outfile.write(text + '\n\n')

infile.close()
outfile.close()

