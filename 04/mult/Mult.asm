// This file is part of www.nand2tetris.org
// and the book "The Elements of Computing Systems"
// by Nisan and Schocken, MIT Press.
// File name: projects/04/Mult.asm

// Multiplies R0 and R1 and stores the result in R2.
// (R0, R1, R2 refer to RAM[0], RAM[1], and RAM[2], respectively.)
//
// This program only needs to handle arguments that satisfy
// R0 >= 0, R1 >= 0, and R0*R1 < 32768.

// Put your code here.

//Initial setup

//If either of our values are 0 at start
@R1
D=M
//Set R0 to 0, and return
@SETZERO
D;JEQ

@R0
D=M
@STOP
D;JEQ

//Finished with edge cases
@R0
D=M

@base
M=D

@R1
M=M-1



//We decrement our count by 1, so for example if r0=5 and r1=2, r1 would be 1, and adding 5 to our r0 would cause it r1 to be 0 during the decrement in the loop. We have a specific condition for checking zero values.
(LOOP)
//Are we done?
@R1
D=M
@STOP
D;JEQ


@base
D=M

@R0
M=M+D


@R1
M=M-1




@LOOP
0;JMP

//If R1 is zero, or R0 is 1, we want to copy R1 to R0.
//We handle the case if R1 is 1 in our main loop. We cover this one here because, while such calculations involve decrementing R0 and R1 before comparing,
//decrementing R1 is not an edge case and is a key part of our algorithm.
(SETZERO)
@R1
D=M

@R0
M=D

//@STOP
//0;JMP
(STOP)
//R2 is where we store our result
@R0
D=M

@R2
M=D

(END)
@END
0;JMP

//Suppose values were r1=5, r2=2
//In the beginning, we'd set R0 as our current data value, and set up base as that value.
//In our loop, we'd get base and set it as our data value.
//Then, we'd add that data value to R0, and that would be our new value of R0.
//Then, we'd decrement our R1 value by one.
//If R1 was 0, we would have our correct value

//So
//r0=5, r1=2
//base is 5 before loop
//In loop, set base's value as our current data value (so it would be 5)
//Then, R0 would be R0 plus our base (so it would be 10)
//Then, we'd decrement our counter by 1
//If counter was 0, we'd progress to our end loop
//