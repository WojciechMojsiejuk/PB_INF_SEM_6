global start

section .text

start:

    mov rdx, 2
    mov rsi, bufor
    mov rdi, 0 ; stdin
    mov rax, 0x2000003 ; read
    syscall

    mov rdx, 2
    mov rsi, bufor
    mov rdi, 1 ; stdout
    mov rax, 0x2000004 ; write
    syscall

    mov rdi, 0
    mov rax, 0x2000001 ; exit
    syscall

section .bss

    bufor  resb    1