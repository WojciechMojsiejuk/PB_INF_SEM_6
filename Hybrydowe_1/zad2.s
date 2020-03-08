;nasm -felf64 zad1.s
;gcc -no-pie zad1.o -o zad1

global main
extern printf


section .text
main:
        mov     rsi, 345
        sub     rsi, 32
        sub     rsi, 111

        mov     rdi, format
        xor     rax, rax

        call    printf
        ret

format:
        db "%d", 10, 0