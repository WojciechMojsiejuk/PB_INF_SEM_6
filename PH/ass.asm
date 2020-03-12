format ELF64 executable 3

segment readable executable

entry main

include 'io.inc'

main:
  lea   rdi, [msng]
  call  print
  lea   rsi, [buf]
  mov   rdi, 64   ;buffer size
  call  read
  mov   rdi, rsi  ;move the string from input to rdi
  call  print
  xor   rdi, rdi  ;set rax to 0
  mov   rax, 60   ;sys_exit with code 0
  syscall

segment readable writable

msng    db 'wpisz wiadomosc: ', 0
buf     rb 64
