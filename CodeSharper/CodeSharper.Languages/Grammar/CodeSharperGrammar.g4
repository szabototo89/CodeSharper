grammar CodeSharperGrammar;

start:
         Commands=commands;

commands:
        Command=command (Operator=operator Command=command+)?;

operator: '|'
        | ','
        | '>'
        ;

command:
           CommandId=ID Parameters=parameters?
       |   Selector=selector;

parameters:
              Parameter=parameter (',' Parameter=parameter)*;

parameter:
             (ParameterName=parameterName '=')? ParameterValue=parameterValue;

parameterName:
                  ParameterId=ID
              ;

parameterValue:
                   STRING 
               |   NUMBER
               |   BOOLEAN
               |   Selector=selector;

parameterValues:
                    ParameterValues=parameterValue (',' ParameterValues=parameterValue)*;

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
                    '[' SelectorAttributeId=ID '=' ParameterValue=parameterValue ']'
                   ;

pseudo_selector:
                ':' PseudoSelectorId=ID ('(' ParameterValue=parameterValues ')')?  
               ;
BOOLEAN:
           'true'|'false';

ID: LETTER (LETTER | SYMBOL)*;

STRING: '"'.+?'"';

NUMBER: [0-9]+(.[0-9]+)?;

fragment LETTER: [a-zA-Z];

fragment SYMBOL: [_\-];

WS : [ \n\u000D] -> skip ;