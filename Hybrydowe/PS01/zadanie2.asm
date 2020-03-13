format ELF executable

entry main

segment readable executable
main:
  mov eax, [a]
  add [sum], eax
  mov eax, [b]
  sub [sum], eax
  mov eax, [c]
  sub [sum], eax
  cmp [sum], 0
  jl absolute
  jmp divide

  absolute:
    mov eax, [sum]
    imul eax, -1
    mov [sum], eax
    mov ecx, minus
    mov eax, 4
    mov ebx, 1
    mov edx, [minusleng]
    int 80h

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
    b dd 3
    c dd 6
    sum dd 0
    sumLen dd $-sum
    counter dd 0
    msg db 0Ah
    msglen dd $-msg
    minus db '-'
    minusleng dd $-minus
