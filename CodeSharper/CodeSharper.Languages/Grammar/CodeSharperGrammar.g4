grammar CodeSharperGrammar;

// TODO: Refactor the rule names!

start:
         Commands=commands;

// commands:   Command=command (Operator=operator? Command=command)+? ;

commands:   commandExpression;

commandExpression: 
                    commandExpression pipeLineOperator='|' commandExpression   #PipeLineCommandExpression      
                  | commandExpression commaOperator=',' commandExpression      #CommaCommandExpression      
                  | commandExpression andOperator='&&' commandExpression       #AndCommandExpression  
                  | commandExpression orOperator='||' commandExpression        #OrCommandExpression  
                  | commandExpression semicolonOperator=';' commandExpression  #SemicolonCommandExpression      
                  | command                                                    #SingleCommandExpression
                  ;
        
command:   CommandCall=commandCall
       |   Selector=selector
       |   '(' commands ')'
       |   '{' commands '}'
       ;

commandCall: CommandName=ID Parameters=parameters
           ; 


parameters:
              (Parameter=parameter)* (NamedParameter=namedParameter)*
          ;

parameter:
             ParameterValue=parameterValue;

namedParameter:
             (ParameterName=parameterName '=')? ParameterValue=parameterValue;

parameterName:
                  ParameterName=ID
              ;

parameterValue:            
                   STRING             #StringParameterValue
               |   NUMBER             #NumberParameterValue
               |   BOOLEAN            #BooleanParameterValue  
               |   Selector=selector  #SelectorParameterValue            
               ;

parameterValues:
                    ParameterValues=parameterValue (',' ParameterValues=parameterValue)*;

selector:
            SelectorLiterals=selectorLiteral (SelectorOperators=selectorOperator? SelectorLiterals=selectorLiteral)*;

selectorOperator:
                     '>' 
                 |   '~'
                 |   '+';

selectorLiteral:
                    SelectorId=ID SelectorAttributes=selectorAttributes* PseudoSelectors=pseudoSelector*
                |   SelectorAttributes=selectorAttributes+ PseudoSelectors=pseudoSelector*
                |   PseudoSelectors=pseudoSelector+
                ;

selectorAttributes:
                    '[' SelectorAttributeId=ID '=' ParameterValue=parameterValue ']'
                   ;

pseudoSelector:
                ':' PseudoSelectorId=ID ('(' ParameterValue= parameterValues ')')?  
               ;

BOOLEAN: 'true'|'false';

ID: LETTER (LETTER | SYMBOL | NUMBER)*;

fragment LETTER: [a-zA-Z];

fragment SYMBOL: [_\-];

STRING: '"'.+?'"'
      | '\''.+?'\'';

NUMBER: [0-9]+;

/*
PIPE_LINE_OPERATOR: '|';
COMMA_OPERATOR: ',';
AND_OPERATOR: '&&';
OR_OPERATOR: '||';
SEMICOLON_OPERATOR: ';';
*/

WS : [ \n\u000D] -> skip ;

