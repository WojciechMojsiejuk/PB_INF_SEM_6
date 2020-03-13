format ELF executable

entry main

segment readable executable

main:
  mov eax, [a]
  mov ebx, [b]
  add eax, ebx
  mov ebx, [c]
  add eax, ebx
  add [sum], eax

divide:
  xor edx, edx
  mov eax, [sum]
  mov ebx, 10
  div ebx
  mov [sum], eax
  push edx
  inc [counter]
  cmp [sum], 0
  jne divide

print:
  pop edx
  mov [sum], edx
  add [sum], '0'
  mov ecx, sum
  mov eax, 4
  mov ebx, 1
  mov edx, [sumLen]
  int 80h
  dec [counter]
  jnz print
  mov ecx, msg
  mov eax, 4
  mov ebx, 1
  mov edx, [msglen]
  int 80h

  mov eax, 1
  dec ebx
  int 80h

segment readable writable
  a dd 2
  b dd 2203
  c dd -2201
  sum dd 0
  sumLen dd $-sum
  counter dd 0
  msg db 0Ah
  msglen dd $-msg
