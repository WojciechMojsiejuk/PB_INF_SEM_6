format ELF executable

entry main

segment readable executable

main:
  mov eax, [a]        ; copy 'a' to eax register
  add [sum], eax      ; add 'a' to sum
  mov eax, [b]
  sub [sum], eax
  mov eax, [c]
  sub [sum], eax

  cmp [sum], 0        ; compare sum to 0
  jl absolute_value   ; if smaller go to absolute_value
  jmp divide          ; else - can go straight to divide

absolute_value:
  mov eax, [sum]      ; copy sum to eax register
  imul eax, -1        ; multiple by -1
  mov [sum], eax
  mov eax, 4          ; system interprets 4 as write
  mov ebx, 1          ; standard output (print to terminal)
  mov ecx, minus      ; pointer to the value being passed
  mov edx, [minusLen] ; length of output in bytes
  int 80h             ; call the kernel

divide:
  mov eax, [sum]      ; place sum in eax register
  mov ebx, 10         ; place 10 in ebx register
  xor edx, edx        ; clear edx register
  div ebx             ; divide eax by ebx and place result in eax, remainder in edx
  mov [sum], eax
  push edx            ; place remainder of last div on stack
  inc [counter]       ; increment counter
  cmp [sum], 0        ; compare sum to 0
  jne divide          ; jump if different than 0 - divide again

print:
  pop edx             ; get a number from stack
  mov [sum], edx
  add [sum], '0'      ; number to ASCII
  mov eax, 4          ; system interprets 4 as write
  mov ebx, 1          ; standard output (print to terminal)
  mov ecx, sum        ; pointer to the value being passed
  mov edx, [sumLen]   ; length of output in bytes
  int 80h             ; call the kernel
  dec [counter]       ; decrement counter
  jnle print          ; jump if not smaller than 0 or equal - go to print

  mov eax, 4          ; system interprets 4 as write
  mov ebx, 1          ; standard output (print to terminal)
  mov ecx, endl       ; pointer to the value being passed
  mov edx, [endlLen]  ; length of output in bytes
  int 80h             ; call the kernel

  mov eax, 1          ; sys_exit
  xor ebx, ebx        ; exit code 0
  int 80h

segment readable writable
  a dd 222
  b dd 220
  c dd -2
  sum dd 0
  sumLen dd $-sum
  counter dd 0
  minus db '-'
  minusLen dd $-minus
  endl dd 0x0A
  endlLen dd $-endl
