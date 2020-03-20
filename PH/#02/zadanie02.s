global start

section .text

start:
    xor rax, rax
    xor rdx, rdx

l1:
    call multiply
    add rax, 8
    cmp rax, 32
    jl l1

    xor rax, rax
    add rdx, 8
    cmp rdx, 32
    jl l1

    mov rdx, result
    mov rcx, 63

print:
    movzx rax, byte [rdx + rcx]
    push rcx
    push rdx
    mov rbx, 16
    xor rdx, rdx
    div rbx
    push rdx

    mov rbx, values

    lea rsi, [rbx + rax]
    mov rdx, 1
    mov rdi, 1 ; stdout
    mov rax, 0x2000004 ; write
    syscall

    pop rdx
    mov rbx, values

    lea rsi, [rbx + rdx]
    mov rdx, 1
    mov rdi, 1 ; stdout
    mov rax, 0x2000004 ; write
    syscall

    mov rdx, 1
    mov rsi, space
    mov rdi, 1 ; stdout
    mov rax, 0x2000004 ; write
    syscall

    pop rdx
    pop rcx
    dec rcx
    jns print

exit:
    mov rdx, 1
    mov rsi, endl
    mov rdi, 1 ; stdout
    mov rax, 0x2000004 ; write
    syscall

    mov rdi, 0
    mov rax, 0x2000001 ; exit
    syscall

multiply:
    push rax
    push rcx
    push rdx
    mov rbx, 48 ;little endian
    sub rbx, rax
    sub rbx, rdx

    lea rcx, [rel num_1]
    add rcx, rax
    mov rax, [rcx]
    lea rcx, [rel num_2]
    add rcx, rdx
    mov rdx, [rcx]
    mul rdx
    lea rcx, [rel result]
    add rcx, rbx
    add [rcx], rax
    jnc resume1
    call carry

resume1:
    add rbx, 8
    lea rcx, [rel result]
    add rcx, rbx
    add [rcx], rdx
    jnc resume2
    call carry

resume2:
    pop rdx
    pop rcx
    pop rax
    ret

carry:
    mov rax, result
    add rax, rbx
    add rax, 8
    inc byte [rax]
    ret

section .data

    endl    db      0xa

    space   db      0x20

    num_1   dq      0x40ca02f59e7ba29a, 0x750838204344b8ef, 0xa30d77cb47944122, 0x96c675ac3462b045
    ; 40ca02f59e7ba29a750838204344b8efa30d77cb4794412296c675ac3462b045

    num_2   dq      0x2b87b6d3f309e022, 0x144dc8be158530a8, 0x9784293001687031, 0x29dba8f214f1f331
    ; 2b87b6d3f309e022144dc8be158530a8978429300168703129dba8f214f1f331

    values  db      '0123456789abcdef'

    result  times 64 db 0
    ; B04474C1342A742D93DA20DE32791B5D48A987E3DD8079AB91E6F67EF63B9C60250112DB310347E5B8B6C4485AB258F3ED2865F8B1E89CA99E2EB97092A3C35
