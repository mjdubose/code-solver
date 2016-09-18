# code-solver
Simple substitution solver (in process)

http://www.atksolutions.com/games/cryptoquote.html - is what I used to get new ciphers.

This program was built to solve simple substition ciphers.  

It has a word list that contains a list of word possibilities that is sorted by a repeating word pattern.
The cipher text is divided into "codewords" by splitting on spaces.  Punctuation is ignored.  A pattern for each code word is generated and then
compared to the list of words sorted by pattern.  If there is only one word in the list of potential words then it is considered a solution for that
particular code word and those letters are propigated throughout the cipher text and each respective code word candidates are reduced accordingly.

This process is repeated until a solution is found, or the program cannot continue any further at which point the results are returned and displayed.


