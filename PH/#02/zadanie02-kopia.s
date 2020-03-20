global start

section .text

start:
    mov rax, qword [rel num_1]
    mov rdx, qword [rel num_2]
    mul rdx
    mov rbx, 8
    add [rel result], rax
    jnc resume1
    call overflow

resume1:
    mov rbx, 16
    add [rel result + 8], rdx
    jnc resume2
    call overflow

resume2:
    mov rax, qword [rel num_1]
    mov rdx, qword [rel num_2 + 8]
    mul rdx
    mov rbx, 16
    add [rel result + 8], rax
    jnc resume3
    call overflow

resume3:
    mov rbx, 24
    add [rel result + 16], rdx
    jnc resume4
    call overflow

resume4:
    mov rax, qword [rel num_1]
    mov rdx, qword [rel num_2 + 16]
    mul rdx
    mov rbx, 24
    add [rel result + 16], rax
    jnc resume5
    call overflow

resume5:
    mov rbx, 32
    add [rel result + 24], rdx
    jnc resume6
    call overflow

resume6:
    mov rax, qword [rel num_1]
    mov rdx, qword [rel num_2 + 24]
    mul rdx
    mov rbx, 32
    add [rel result + 24], rax
    jnc resume7
    call overflow

resume7:
    mov rbx, 40
    add [rel result + 32], rdx
    jnc resume8
    call overflow

resume8:
    mov rax, qword [rel num_1 + 8]
    mov rdx, qword [rel num_2]
    mul rdx
    mov rbx, 16
    add [rel result + 8], rax
    jnc resume9
    call overflow

resume9:
    mov rbx, 24
    add [rel result + 16], rdx
    jnc resume10
    call overflow

resume10:
    mov rax, qword [rel num_1 + 8]
    mov rdx, qword [rel num_2 + 8]
    mul rdx
    mov rbx, 24
    add [rel result + 16], rax
    jnc resume11
    call overflow
    
resume11:
    mov rbx, 32
    add [rel result + 24], rdx
    jnc resume12
    call overflow

resume12:
    mov rax, qword [rel num_1 + 8]
    mov rdx, qword [rel num_2 + 16]
    mul rdx
    mov rbx, 32
    add [rel result + 24], rax
    jnc resume13
    call overflow

resume13:
    mov rbx, 40
    add [rel result + 32], rdx
    jnc resume14
    call overflow

resume14:
    mov rax, qword [rel num_1 + 8]
    mov rdx, qword [rel num_2 + 24]
    mul rdx
    mov rbx, 40
    add [rel result + 32], rax
    jnc resume15
    call overflow

resume15:
    mov rbx, 48
    add [rel result + 40], rdx
    jnc resume16
    call overflow

resume16:
    mov rax, qword [rel num_1 + 16]
    mov rdx, qword [rel num_2]
    mul rdx
    mov rbx, 24
    add [rel result + 16], rax
    jnc resume17
    call overflow

resume17:
    mov rbx, 32
    add [rel result + 24], rdx
    jnc resume18
    call overflow

resume18:
    mov rax, qword [rel num_1 + 16]
    mov rdx, qword [rel num_2 + 8]
    mul rdx
    mov rbx, 32
    add [rel result + 24], rax
    jnc resume19
    call overflow

resume19:
    mov rbx, 40
    add [rel result + 32], rdx
    jnc resume20
    call overflow

resume20:
    mov rax, qword [rel num_1 + 16]
    mov rdx, qword [rel num_2 + 16]
    mul rdx
    mov rbx, 40
    add [rel result + 32], rax
    jnc resume21
    call overflow

resume21:
    mov rbx, 48
    add [rel result + 40], rdx
    jnc resume22
    call overflow

resume22:
    mov rax, qword [rel num_1 + 16]
    mov rdx, qword [rel num_2 + 24]
    mul rdx
    mov rbx, 48
    add [rel result + 40], rax
    jnc resume23
    call overflow

resume23:
    mov rbx, 56
    add [rel result + 48], rdx
    jnc resume24
    call overflow

resume24:
    mov rax, qword [rel num_1 + 24]
    mov rdx, qword [rel num_2]
    mul rdx
    mov rbx, 32
    add [rel result + 24], rax
    jnc resume25
    call overflow

resume25:
    mov rbx, 40
    add [rel result + 32], rdx
    jnc resume26
    call overflow

resume26:
    mov rax, qword [rel num_1 + 24]
    mov rdx, qword [rel num_2 + 8]
    mul rdx
    mov rbx, 40
    add [rel result + 32], rax
    jnc resume27
    call overflow

resume27:
    mov rbx, 48
    add [rel result + 40], rdx
    jnc resume28
    call overflow

resume28:
    mov rax, qword [rel num_1 + 24]
    mov rdx, qword [rel num_2 + 16]
    mul rdx
    mov rbx, 48
    add [rel result + 40], rax
    jnc resume29
    call overflow

resume29:
    mov rbx, 56
    add [rel result + 48], rdx
    jnc resume30
    call overflow

resume30:
    mov rax, qword [rel num_1 + 24]
    mov rdx, qword [rel num_2 + 24]
    mul rdx
    mov rbx, 56
    add [rel result + 48], rax
    jnc resume31
    call overflow

resume31:
    add [rel result + 56], rdx

    mov rdx, result
    mov rcx, 63

print:
    movzx rax, byte [rdx + rcx]
    push rcx
    push rdx
    xor rdx, rdx
    mov rbx, 16
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

overflow:
    mov rax, result
    add rax, rbx
    inc byte [rax]
    ret

section .data

    endl    db      0xa

    space   db      0x20

    num_1   dq      0x96c675ac3462b045, 0xa30d77cb47944122, 0x750838204344b8ef, 0x40ca02f59e7ba29a
    ; 40ca02f59e7ba29a750838204344b8efa30d77cb4794412296c675ac3462b045

    num_2   dq      0x29dba8f214f1f331, 0x9784293001687031, 0x144dc8be158530a8, 0x2b87b6d3f309e022
    ; 2b87b6d3f309e022144dc8be158530a8978429300168703129dba8f214f1f331

    values  db      '0123456789abcdef'

    result  times 64 db 0
