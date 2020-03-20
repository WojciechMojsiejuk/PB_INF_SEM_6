global start

section .text

start:
    mov rdx, 20
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
    jc input_err
    inc rdx
    dec rcx
    jnz read

loaded:
    mov rbx, rax

l1:
    mov rax, rbx
    mov rcx, 2
    xor rdx, rdx
    div rcx
    cmp rdx, 0
    je resume
    inc byte [rel result]

resume:
    mov rbx, rax
    cmp rax, 0
    jne l1

print:
    movzx rax, byte [rel result]
    mov rbx, 10
    xor rdx, rdx
    div rbx
    push rdx
    add rax, '0'
    mov [rel bufor], rax

    mov rdx, 1
    mov rsi, bufor
    mov rdi, 1 ; stdout
    mov rax, 0x2000004 ; write
    syscall

    pop rdx
    add rdx, '0'
    mov [rel bufor], rdx

    mov rdx, 1
    mov rsi, bufor
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

input_err:
    mov rdx, i_err.len
    mov rsi, i_err
    mov rdi, 1 ; stdout
    mov rax, 0x2000004 ; write
    syscall

    jmp exit

section .data

    endl    db      0xa

    result  db      0

    i_err   db      "Podano błędną wartość"
    .len    equ     $ - i_err

    o_err   db      "Wynik poza zakresem"
    .len    equ     $ - o_err

section .bss

    bufor   resb    20