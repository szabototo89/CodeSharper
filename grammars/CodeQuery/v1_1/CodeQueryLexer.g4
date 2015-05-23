lexer grammar CodeQueryLexer;

STRING: '"' .*? '"';

LEFT_BRACKET: '(';
RIGHT_BRACKET: ')';

LEFT_SQUARE_BRACKET: '[';
RIGHT_SQUARE_BRACKET: ']';

NUMBER: [0-9]+('.'[0-9]+)?;

BOOLEAN: 'false' | 'true';

ID: (CHARACTER | ID_SYMBOL) (CHARACTER | NUMBER | ID_SYMBOL)*;

SELECTOR_OPERATOR: '>' | '+';

DOT: '.';
COLON: ':';
COMMA: ',';

METHOD_CALL_SYMBOL: '@';

ASSIGNMENT_OPERATOR: '=';

PIPELINE_OPERATOR: '|' | '&&' | ';' | '||' ;

fragment ID_SYMBOL: '-'|'_'|'$';

fragment CHARACTER: [a-zA-Z];

WHITESPACE: [ \r\n\t]+ -> channel(HIDDEN);