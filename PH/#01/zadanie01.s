global start

section .text

start:
    mov rax, 12
    mov rbx, 17
    add rax, rbx

    mov rbx, 93
    add rax, rbx

    mov rbx, rax ;rbx - division

l1:
    xor rdx, rdx
    mov rax, rbx ;rbx - division
    mov rcx, 10
    div rcx

    mov rbx, rax ;rbx - division

    add rdx, '0'
    push rdx
    inc byte [rel len]

    mov rcx, rbx ;rbx - division
    dec rcx
    jnl l1

l2:
    pop rdx
    mov [rel remainder], rdx

    mov rdx, 1
    mov rsi, remainder
    mov rdi, 1 ; stdout
    mov rax, 0x2000004 ; write
    syscall

    dec byte [rel len]
    jnz l2

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
    len     db      0

section .bss

    remainder   resw    1
