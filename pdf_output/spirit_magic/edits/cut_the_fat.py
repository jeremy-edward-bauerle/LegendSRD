#!/usr/bin/python

import sys
from bs4 import BeautifulSoup

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
'font-family: WEJTGB+AGaramond-BoldItalic; font-size:10px' : '',    \
'font-family: MBSAWV+ImperatorSmallCaps; font-size:26px' : 'h1',    \
'font-family: HFWHEF+TimesNewRomanPS-Italic; font-size:9px' : 'i',  \
'font-family: RCUKOL+TimesNewRomanPS; font-size:9px' : 'p',         \
'font-family: BESSYR+TimesNewRomanPS-Bold; font-size:10px' : 'b',   \
'font-family: BESSYR+TimesNewRomanPS-Bold; font-size:11px' : 'th',  \
'font-family: RCUKOL+TimesNewRomanPS; font-size:16px' : 'h3'        \
}

if len(sys.argv) != 2:
    print 'usage: ' + sys.argv[0] + '<dirty html file>'
    sys.exit()

html_in = open(sys.argv[1], 'r')
text = html_in.read()
html_in.close()

soup = BeautifulSoup(text)


# Remove all the <div> tags
soup.body.div.unwrap()

output = open('clean.md', 'w')
for line in soup.prettify():
    output.write(line)
output.close()

