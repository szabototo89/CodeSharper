parser grammar CodeQuery;

options { tokenVocab = CodeQueryLexer; }

command: expression (PIPELINE_OPERATOR command)?
       ;

expression: methodCall                                                                         #ExpressionMethodCall
          | selector                                                                           #ExpressionSelector
          | LEFT_BRACKET expression RIGHT_BRACKET                                              #ExpressionInner
          ;

methodCall: METHOD_CALL_SYMBOL MethodCallName=ID (MethodCallParameter=methodCallParameter)*
          ;

methodCallParameter: (ParameterName=ID ASSIGNMENT_OPERATOR)? ActualParameterValue=expression   #MethodCallParameterValueWithExpression
                   | (ParameterName=ID ASSIGNMENT_OPERATOR)? ActualParameterValue=constant     #MethodCallParameterValueWithConstant
                   | (ParameterName=ID ASSIGNMENT_OPERATOR)? ActualParameterValue=ID           #MethodCallParameterValueWithIdentifier
                   ;

selector: selectableElement (selectorAttribute)* (SELECTOR_OPERATOR? selectableElement)*;

selectableElement: DOT? ID pseudoSelector*;

pseudoSelector: COLON Name=ID (LEFT_BRACKET Value=constant RIGHT_BRACKET)?                     #PseudoSelectorWithConstant
              | COLON Name=ID (LEFT_BRACKET Value=ID RIGHT_BRACKET)?                           #PseudoSelectorWithIdentifier
              ;

selectorAttribute: LEFT_SQUARE_BRACKET
                        AttributeName=ID ASSIGNMENT_OPERATOR AttributeValue=constant
                   RIGHT_SQUARE_BRACKET
                 ;

constant: STRING                                                                               #ConstantString
        | NUMBER                                                                               #ConstantNumber
        | BOOLEAN                                                                              #ConstantBoolean
        ;