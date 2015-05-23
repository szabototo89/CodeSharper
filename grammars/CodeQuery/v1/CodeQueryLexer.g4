lexer grammar CodeQueryLexer;

tokens { LEFT_BRACKET, RIGHT_BRACKET }


STRING: '"' .+? '"';

WHITESPACE: (' '|'\r'|'\n')+ -> channel(HIDDEN);