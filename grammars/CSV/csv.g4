grammar Csv;

start  : Rows=row*
       ;

row    : (Fields=field (Comma=COMMA Fields=field)*) '\r'? '\n' ;

field
    : TEXT
    | STRING
    |
    ;

COMMA : ',' ;

TEXT   : ~[,\n\r"]+
       ;

STRING : '"' ('""'|~'"')* '"'
       ;