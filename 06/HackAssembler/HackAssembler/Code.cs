using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HackAssembler
{
    internal static class Code
    {
        public static string dest(string symbolicDest)
        {
            StringBuilder outputString = new StringBuilder("000");
           if (symbolicDest.Contains('A'))
            {
                outputString[0] = '1';
            }
           if (symbolicDest.Contains('D'))
            {
                outputString[1] = '1';
            }
           if (symbolicDest.Contains('M'))
            {
                outputString[2] = '1';
            }

            return outputString.ToString();
        }

        //Note: This doesn't *just* calculate the comp value. It also calculates the preceding a value.
        //I did this because otherwise I would have to perform this switch here and somewhere else.
        public static string comp(string symbolicComp)
        {
            string binaryComp;


            switch (symbolicComp)
            {
                case "0":
                    binaryComp = "0101010";
                    break;
                case "1":
                    binaryComp = "0111111";
                    break;
                case "-1":
                    binaryComp = "0111010";
                    break;
                case "D":
                    binaryComp = "0001100";
                    break;
                case "A":
                    binaryComp = "0110000";
                    break;
                case "M":
                    binaryComp = "1110000";
                    break;
                case "!D":
                    binaryComp = "001101";
                    break;
                case "!A":
                    binaryComp = "0110001";
                    break;
                case "!M":
                    binaryComp = "1110001";
                    break;
                case "-D":
                    binaryComp = "0001111";
                    break;
                case "-A":
                    binaryComp = "0110011";
                    break;
                case "-M":
                    binaryComp = "1110011";
                    break;
                case "D+1":
                    binaryComp = "0011111";
                    break;
                case "A+1":
                    binaryComp = "0110111";
                    break;
                case "M+1":
                    binaryComp = "1110111";
                    break;
                case "D-1":
                    binaryComp = "0001110";
                    break;
                case "A-1":
                    binaryComp = "0110010";
                    break;
                case "M-1":
                    binaryComp = "1110010";
                    break;
                case "D+A":
                    binaryComp = "0000010";
                    break;
                case "D+M":
                    binaryComp = "1000010";
                    break;
                case "D-A":
                    binaryComp = "0010011";
                    break;
                case "D-M":
                    binaryComp = "1010011";
                    break;
                case "A-D":
                    binaryComp = "0000111";
                    break;
                case "M-D":
                    binaryComp = "1000111";
                    break;
                case "D&A":
                    binaryComp = "0000000";
                    break;
                case "D&M":
                    binaryComp = "1000000";
                    break;
                case "D|A":
                    binaryComp = "0010101";
                    break;
                case "D|M":
                    binaryComp = "1010101";
                    break;
                default:
                    throw new ArgumentOutOfRangeException("You did not supply a correct comp instruction.\n" +
                    "Refer to the Hack specification for a list of the available comp instructions.");
            }
            return binaryComp;     
        }

        public static string jump(string symbolicJump)
        {
            StringBuilder outputString = new StringBuilder("000");

            return symbolicJump switch
            {
                "JGT" => "001",
                "JEQ" => "010",
                "JGE" => "011",
                "JLT" => "100",
                "JNE" => "101",
                "JLE" => "110",
                "JMP" => "111",
                _ => "000" //No jump instruction
            };
        }
    }
}
