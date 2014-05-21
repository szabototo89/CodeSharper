grammar Csv;

/*
 * Parser Rules
 */

compileUnit
	:	record+
	;

record	:	field (delimiter field)*
		;

delimiter	:	COMMA
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
	:	[ \t] -> channel(HIDDEN)
	;