global start

section .text

start:

    mov rdx, 16
    mov rsi, bufor
    mov rdi, 0 ; stdin
    mov rax, 0x2000003 ; read
    syscall

    mov [rel length], rax
    dec byte [rel length]

    mov rbx, result
    mov rcx, rax
    mov rdx, bufor

read:
    mov al, [rdx]

    cmp al, 0xa
    je print

    sub al, '0'

    cmp al, 0xa
    jnl error

    cmp al, 0
    jl error

    add al, '0'

    mov [rbx], al
    
    add rbx, 1
    add rdx, 1
    dec rcx
    jnz read

print:
    mov rdx, [rel length]
    mov rsi, result
    mov rdi, 1 ; stdout
    mov rax, 0x2000004 ; write
    syscall

exit:
    mov rdx, 1
    mov rsi, endl
    mov rdi, 1 ; stdout
    mov rax, 0x2000004 ; write
    syscall

    mov rdi, 0
    mov rax, 0x2000001 ; exit
    syscall

error:
    mov rdx, err.len
    mov rsi, err
    mov rdi, 1 ; stdout
    mov rax, 0x2000004 ; write
    syscall

    jmp exit

section .data

    endl    db      0xa

    err     db      "Podano błędną wartość"
    .len    equ     $ - err

section .bss

    bufor   resb    16
    result  resb    16
    length  resb    1