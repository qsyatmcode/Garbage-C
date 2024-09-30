grammar cprogram;


program : function ; // Root node
function : 'int' ID '(' ')' '{' statement '}' ;
statement : 'return' exp ';' ;
exp : op=('-'|'~'|'!'|'+') exp # Unary
    | exp op=('*'|'/'|'%') exp # MultDiv
    | exp op=('+'|'-') exp # AddSub
    | exp op=('<<'|'>>') exp # BWShift
    | exp op=('<'|'>'|'<='|'>=') exp # Relational
    | exp op=('=='|'!=') exp # Equality
    | exp op='&' exp # BWAnd
    | exp op='^' exp # BWXor
    | exp op='|' exp # BWOr
    | exp op=('||'|'&&') exp # Logical
    | '(' exp ')'          # Parens
    | INT                  # Literal
    ;
    
COMMENT_LINE: '//' ~('\r'|'\n')* -> channel(HIDDEN) ;
COMMENT_MULTILINE: '/*' .*? '*/' -> channel(HIDDEN) ;
WS: [ \t\r\n]+ -> skip ;

ID : [a-zA-Z]+;
INT: [0-9]+ ;

MUL: '*' ;
DIV: '/' ;
DIVR: '%' ;
ADD: '+' ;
SUB: '-' ;
BWC: '~' ;
LNT: '!' ;
BWRS: '>>' ;
BWLS: '<<' ;
BWA: '&' ;
BWO: '|' ;
BWX: '^' ;
RGR: '>' ;
RLS: '<' ;
RGREQ: '>=' ;
RLSEQ: '<=' ;
EQLS: '==' ;
NEQLS: '!=' ;
OR: '||' ;
AND: '&&' ;