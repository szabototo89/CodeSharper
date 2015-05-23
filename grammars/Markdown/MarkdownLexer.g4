lexer grammar MarkdownLexer;

LIST_MARK: '#'+ -> more, pushMode(HEADER_MODE)
         ;

WHITESPACE: ' ' -> channel(HIDDEN)
          ;

mode HEADER_MODE;

NEW_LINE: '\r'? '\n' -> popMode
        ;

HEADER_TEXT: .* -> more;

