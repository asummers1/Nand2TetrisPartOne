// This file is part of www.nand2tetris.org
// and the book "The Elements of Computing Systems"
// by Nisan and Schocken, MIT Press.
// File name: projects/04/Fill.asm

// Runs an infinite loop that listens to the keyboard input.
// When a key is pressed (any key), the program blackens the screen,
// i.e. writes "black" in every pixel;
// the screen should remain fully black as long as the key is pressed. 
// When no key is pressed, the program clears the screen, i.e. writes
// "white" in every pixel;
// the screen should remain fully clear as long as no key is pressed.

// Put your code here.
(BEGINNING)
@SCREEN
D=A


//We save the address of the initial position of the screen, which increases as the user either presses a button or not
@R1
M=D

//We also save the address of the end position of the screen, so we can check when the "cursor" gets to that point.
@24577
D=A

@R2
M=D

(LOOP)
@KBD
D=M



//Is the value zero (indicating no key was pressed)
@DRAWBLANK
D;JEQ
//If any key was pressed, the value will not be 0. Thus, we can draw black.

@DRAWBLACK
D;JNE
@LOOP
0;JMP


(DRAWBLANK)
//Are we at end? Remember that the end position is denoted by R2, and R1 denotes the current position.
@R2
D=M
//Get the difference between current and end position
@R1
D=M-D

//If we're at the end, we want our "cursor" to go back to the beginning
@BEGINNING
D;JEQ

//Get current position. Recall that the value of 'R1' is an index; therefore, we get that.
@R1
D=M


A=D
//A=D
M=0
//Increment value for subsequent position
@R1
M=M+1


@LOOP
0;JMP


(DRAWBLACK)

//Are we at end?
@R2
D=M

@R1
D=M-D


//If we're at the end, we want our "cursor" to go back to the beginning
@BEGINNING
D;JEQ

//Get current position. Recall that the value of 'R1' is the base index of the screen + extending value.

//Note that this style of pointer access is different than the one in the PointerDemo (see book, pg. 78), in that it kept a separate, 0-initialized index
//which was added to the base address to calculate a new address on each iteration of the loop, while my method initializes to a copy of the screen address before the loop and simply
//increments that value.
@R1
D=M

//Set screen details. We're setting the current address to the value of whatever the current screen position should be, and setting its value to -1, which corresponds to all 16 bits being 1, or "on"

A=D
M=-1
//Increment value for subsequent position
@R1
M=M+1


@LOOP
0;JMP