grammar CodeSharperGrammar;

start:
         Commands=commands;

commands:   Command=command (Operator=operator? Command=command)+?
        ;

operator: '|'
        | ','
        | '&'
        | ';'
        | '>'
        ;

command:
           CommandId=ID Parameters=parameters?
       |   Selector=selector
       |   '(' commands ')'
       |   '{' commands '}'
       ;

parameters:
              Parameter=parameter (',' Parameter=parameter)*;

parameter:
             (ParameterName=parameter_name '=')? ParameterValue=parameter_value;

parameter_name:
                  ParameterId=ID
              ;

parameter_value:
                   STRING 
               |   NUMBER
               |   BOOLEAN
               |   Selector=selector;

parameter_values:
                    ParameterValues=parameter_value (',' ParameterValues=parameter_value)*;

selector:
            SelectorLiterals=selector_literal (SelectorOperators=selector_operator? SelectorLiterals=selector_literal)*;

selector_operator:
                     '>' 
                 |   '~'
                 |   '+';

selector_literal:
                    SelectorId=ID SelectorAttributes=selector_attributes* PseudoSelectors=pseudo_selector*
                |   SelectorAttributes=selector_attributes+ PseudoSelectors=pseudo_selector*
                |   PseudoSelectors=pseudo_selector+
                ;

selector_attributes:
                    '[' SelectorAttributeId=ID '=' ParameterValue=parameter_value ']'
                   ;

pseudo_selector:
                ':' PseudoSelectorId=ID ('(' ParameterValue=parameter_values ')')?  
               ;
BOOLEAN:
           'true'|'false';

ID: LETTER (LETTER | SYMBOL)*;

STRING: '"'.+?'"';

NUMBER: [0-9]+(.[0-9]+)?;

WS : [ \n\u000D] -> skip ;

fragment LETTER: [a-zA-Z];

fragment SYMBOL: [_\-];
