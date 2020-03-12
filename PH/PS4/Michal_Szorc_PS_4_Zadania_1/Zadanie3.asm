format ELF executable

entry main

segment readable executable

main:
  mov eax, 4            ; system interprets 4 as write
  mov ebx, 1            ; standard output (print to terminal)
  mov ecx, msg          ; pointer to the value being passed
  mov edx, [msgLen]     ; length of output in bytes
  int 80h               ; call the kernel

  mov eax, 3            ; system interprets 3 as read
  mov ebx, 0            ; read from standard input
  mov ecx, char         ; address to pass to
  mov edx, [charLen]    ; input length in bytes
  int 80h

  mov eax, 4
  mov ebx, 1
  mov ecx, char
  mov edx, [charLen]
  int 80h

  mov eax, 4
  mov ebx, 1
  mov ecx, endl
  mov edx, [endlLen]
  int 80h

  mov eax, 1            ; sys_exit
  xor ebx, ebx          ; exit code 0
  int 80h

segment readable writable
  msg db 'Enter your text: '
  msgLen dd $-msg
  char rb 0x1
  charLen dd $-char
  endl dd 0x0A
  endlLen dd $-endl
