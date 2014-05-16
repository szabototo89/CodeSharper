grammar Csv;

/*
 * Parser Rules
 */

compileUnit
	:	row+
	;

row		:	field (COMMA field)*
		;

field	:	STRING
		|	ID
		;

/*
 * Lexer Rules
 */

 ID		:	[a-zA-Z0-9_]+
		;

STRING	:	'"' .*? '"'
		;

COMMA	:	','
		;
WS
	:	' ' -> channel(HIDDEN)
	;