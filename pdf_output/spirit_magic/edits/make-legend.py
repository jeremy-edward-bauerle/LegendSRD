#!/usr/bin/python

import sys, io
from bs4 import BeautifulSoup, NavigableString
import bleach

# Remove all unwanted tags
def strip_tags(html, valid_tags):
    soup = BeautifulSoup(html)
    for tag in soup.findAll(True):
        if tag.name not in valid_tags:
            tag.replaceWith(tag.renderContents())
    return soup

VALID_TAGS = ['span']

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

infile = open(sys.argv[1], 'r')
text = infile.read()
infile.close()

soup = BeautifulSoup(text)

# Strip all unwanted tags | script is above
# soup = strip_tags(text, VALID_TAGS)

# Remove all attributes from all tags
# for tag in soup.findAll(True):
#     tag.attrs = None

html = soup.prettify('utf-8')
#with io.open('output.html', 'w', encoding='utf8') as output:
#    output.write(html)
with open('output.html', 'w') as output:
    output.write(html)

