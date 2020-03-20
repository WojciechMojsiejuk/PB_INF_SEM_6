global start

section .text

start:
    mov rdx, 3
    mov rsi, bufor
    mov rdi, 0 ; stdin
    mov rax, 0x2000003 ; read
    syscall
    
    mov rcx, rax
    mov rdx, bufor
    xor rax, rax

read:
    movzx rbx, byte [rdx]

    cmp rbx, 0xa
    je loaded

    sub rbx, '0'

    cmp rbx, 0xa
    jnl input_err

    cmp rbx, 0
    jl input_err

    push rdx
    mov rdx, 10
    mul rdx
    pop rdx

    add rax, rbx
    inc rdx
    dec rcx
    jnz read

loaded:
    mov rbx, rax


calculate:
    cmp rbx, 0
    je l1

    mov rax, [rel result]
    mul rbx
    jo overflow_err
    mov [rel result], rax
    dec rbx
    jmp calculate

l1:
    xor rdx, rdx
    mov rax, [rel result]
    mov rbx, 10
    div rbx

    add rdx, '0'
    push rdx
    inc byte [rel length]

    mov [rel result], rax
    dec rax
    jnl l1

l2:
    pop rdx
    mov [rel result], rdx

    mov rdx, 1
    mov rsi, result
    mov rdi, 1 ; stdout
    mov rax, 0x2000004 ; write
    syscall

    dec byte [rel length]
    jnz l2

exit:
    mov rdx, 1
    mov rsi, endl
    mov rdi, 1 ; stdout
    mov rax, 0x2000004 ; write
    syscall

    mov rdi, 0
    mov rax, 0x2000001 ; exit
    syscall

input_err:
    mov rdx, i_err.len
    mov rsi, i_err
    mov rdi, 1 ; stdout
    mov rax, 0x2000004 ; write
    syscall

    jmp exit

overflow_err:
    mov rdx, o_err.len
    mov rsi, o_err
    mov rdi, 1 ; stdout
    mov rax, 0x2000004 ; write
    syscall

    jmp exit


section .data

    endl    db      0xa

    length  db      0

    result  dq      1

    i_err   db      "Podano błędną wartość"
    .len    equ     $ - i_err

    o_err   db      "Wynik poza zakresem"
    .len    equ     $ - o_err

section .bss

    bufor   resb    3