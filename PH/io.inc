strlen:
  push  rdi       ;push to stack
  push  rcx       ;push to stack
  sub   rcx,  rcx ;set rcx to 0
  mov   rcx, -1   ;move -1 to rcx
  xor   al, al    ;set al to 0
  cld             ;clear the direction flags
  repne scasb     ;scan through the string and compare characters to what is in al/ repeat if not equal to al/ scasb retracts 1 for every character
  neg   rcx       ;negate rcx with string length
  sub   rcx, 1    ;rcx started with 1
  mov   rax, rcx  ;move value from rcx to rax
  pop   rcx       ;restore original register value
  pop   rdi
  ret

print:
  call  strlen    ;get string length
  mov   rdx, rax  ;move string length to rdx
  mov   rsi, rdi  ;move adress of string to rsi
  mov   rdi, 1    ;stdout
  mov   rax, 1    ;sys_write
  syscall
  ret

read:
  mov   rdx, rdi  ;buffer to read to
  mov   rdi, 0    ;stdin
  mov   rax, 0    
  syscall
  ret
