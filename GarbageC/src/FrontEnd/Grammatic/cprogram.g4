grammar cprogram;

WS: [ \t\r\n]+ -> skip ;

ID : [a-zA-Z]+;
INT: [0-9]+ ;

MUL: '*' ;
DIV: '/' ;
ADD: '+' ;
SUB: '-' ;
BWC: '~' ;
LNT: '!' ;

program : function ; // Root node
function : 'int' ID '(' ')' '{' statement '}' ;
statement : 'return' exp ';' ;
exp : exp op=('*'|'/') exp # MultDiv
    | exp op=('+'|'-') exp # AddSub
    | '(' exp ')'          # Parens
    | op=('-'|'~'|'!') exp # Unary
    | INT                  # Literal
    ;