# cpu-8bits

- registers = 4
- special registers = 2
  - cmp result
  - stack pointer
- code instructions limit = 64 opcodes
- memory limit = 256 bytes

inst. | opcode | example | description
----- | ------ | ------- | -----------
nop | 00000000 | nop | não faz nada
add | 0001rrss | add $0 $1 | soma rr e ss e coloca em rr
mul | 0010rrss | mul $0 $1 | multiplica rr e ss e coloca em rr
and | 0011rrss | and $0 $1 | aplica and rr e ss e coloca em rr
or  | 0100rrss | or  $0 $1 | aplica or rr e ss e coloca em rr
cmp | 0101rrss | cmp $0 $1 | compara rr e ss e coloca em um registrador especial
mov | 0110rrss | mov $0 $1 | move ss para rr
mov | 011100cc | mov 4 | move constante para registrador 3 (de 0 a 3)
str | 011101rr | str $0 | move valor do enedreço rr na memória para registrador 3
ldr | 011110rr | ldr $0 | move valor do registrador 3 para endereço rr na memória
ret | 01111110 | ret | retorna para endereço localizado no topo da pilha
jg  | 10aaaaaa | jg loop | pula para loop se no ulitmo cmp comparou um registrador rr maior que ss
call| 11aaaaaa | call func | chama função colocando ponto de retorno na pilha
