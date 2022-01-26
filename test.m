10 ASSIGN fileIn .\input.txt READ
15 ASSIGN fileOut .\output.txt WRITE

20 READ fileIn line 
30 COMPARE 40 $line NE EOF
END

40 WRITE fileOut $line
50 GOTO 20