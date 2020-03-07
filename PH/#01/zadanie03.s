global start

section .text

start:

    mov rdx, 1
    mov rsi, bufor
    mov rdi, 0 ; stdin
    mov rax, 0x2000003 ; read
    syscall

    mov rdx, 1
    mov rsi, bufor
    mov rdi, 1 ; stdout
    mov rax, 0x2000004 ; write
    syscall

    mov rdx, 1
    mov rsi, endl
    mov rdi, 1 ; stdout
    mov rax, 0x2000004 ; write
    syscall

    mov rdi, 0
    mov rax, 0x2000001 ; exit
    syscall

section .data

    endl    db      0xa

section .bss

    bufor  resb    1