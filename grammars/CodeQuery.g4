grammar CodeQuery;

expression: constant
          | selectorMember (selectorOperator selectorMember)*
          | LEFT_BRACKET expression RIGHT_BRACKET
          ;

selectorOperator: '>'
                | '+'
                |
                ;

selectorMember:  ID (pseudoSelectorMember)*
              |  pseudoSelectorMember+
              ;

pseudoSelectorMember : ':' ID (LEFT_BRACKET expression RIGHT_BRACKET)?
                     ;

constant: STRING
        | NUMBER
        | ID;

LEFT_BRACKET: '(';
RIGHT_BRACKET: ')';

STRING: '"'.*?'"';
NUMBER: [0-9]+;

ID: ([a-zA-Z]|'-')+;

COMMAND_OPERATOR: ';' | '|';

WHITESPACE: (' '|'\r'|'\n')+ -> skip;