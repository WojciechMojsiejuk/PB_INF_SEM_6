;nasm -felf64 zad1.s
;gcc -no-pie zad1.o -o zad1

global main
extern printf, scanf

section .bss
        s resw 1 

section .text
main:
        push    rbp

        mov     rdi, input
        mov     rsi, s
        xor     rax, rax
        call scanf
        

        xor     rax, rax
        mov     rsi, s
        mov     rdi, format
        call printf

        pop     rbp
        ret

format:
        db "%s", 10, 0
input:
        db '%s',0
        