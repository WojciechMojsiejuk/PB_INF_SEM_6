format ELF executable

entry main

segment readable executable

main:
mov ecx, prompt
mov eax, 4
mov ebx, 1
mov edx, [promptleng]
int 80h

mov ecx, char
mov eax, 3
mov ebx, 0
mov edx, [charlen]
int 80h

mov ecx, char
mov eax, 4
mov ebx, 1
mov edx, [charlen]
int 80h

mov ecx, msg
mov eax, 4
mov ebx, 1
mov edx, [msglen]
int 80h

mov eax, 1
dec ebx
int 80h

segment readable writable
prompt db "Type something!\n"
promptleng dd $-prompt
char rb 0x100
charlen dd $-char
msg db 0Ah
msglen dd $-msg
