using System;
using System.Collections.Generic;
using System.Linq;
using System.IO;
using System.Text;
using System.Threading.Tasks;

namespace HackAssembler
{
    internal class Parser
    {
        StreamReader inputFile;

        //We'll use a StringBuilder instead of a string, since
        //we'll frequently be overwriting its value
         StringBuilder currentInstruction = new StringBuilder();
        
        public enum InstructionTypes { 
            A_INSTRUCTION, 
            C_INSTRUCTION,
            L_INSTRUCTION
        };
          public Parser(string fileName)
        {
            inputFile = new StreamReader(fileName);
            
        }
         ~Parser()
        {
            inputFile.Close();
        }

        public bool hasMoreLines() => !inputFile.EndOfStream;

        //Note: only call advance() if hasMoreLines() returns true
        public void advance()
        {
            //Clear the instruction, so we can set a new one
            currentInstruction.Clear();

            //The '?' means that the variable's value can be null.
            string? buffer;
         
             do
                {
                //Read the line, and save it to a buffer. This won't save any commas, in the case
                //they're at the end.
                    buffer = inputFile.ReadLine().Split('/')[0];

                }
                while (String.IsNullOrEmpty(buffer));
            buffer.Trim();
            currentInstruction = currentInstruction.Append(buffer);
            
        }

        public InstructionTypes instructionType()
        {
            if (currentInstruction.ToString().Contains('@'))
                {
                return InstructionTypes.A_INSTRUCTION;
            }
            else if (currentInstruction.ToString().Contains('('))
            {
                return InstructionTypes.L_INSTRUCTION;
            }
            else
            {
                return InstructionTypes.C_INSTRUCTION;
            }
        }

        public string symbol()
        {
            return instructionType() switch
            {
                InstructionTypes.A_INSTRUCTION => currentInstruction.ToString().Trim(),
                InstructionTypes.L_INSTRUCTION => currentInstruction.ToString(),
                _ => throw new ArgumentOutOfRangeException("You should only supply an A or L instruction.")
            };
        }

        public string dest()
        {
            
            if (instructionType() == InstructionTypes.C_INSTRUCTION && currentInstruction.ToString().Contains('='))
            {
               
                return currentInstruction.ToString().Split('=')[0].Trim();
            }

            return "";
        }

        public string comp()
        {
            if (instructionType() == InstructionTypes.C_INSTRUCTION)
            {
                int equalsIndex = currentInstruction.ToString().IndexOf('=');

                string comparisonUntrimmed;

                if (currentInstruction.ToString().Contains(';'))
                {
                    comparisonUntrimmed = currentInstruction.ToString().Split(';')[0];

                }
                else
                {
                    comparisonUntrimmed = currentInstruction.ToString().Split('=')[1];
                    //comparisonUntrimmed = currentInstruction.ToString().Substring(equalsIndex + 1, currentInstruction.Length - 1);
                }
                return comparisonUntrimmed.Trim();
            }

            throw new ArgumentOutOfRangeException();
        }

        public string jump()
        {
            if (instructionType() == InstructionTypes.C_INSTRUCTION)
            {
                if (currentInstruction.ToString().Contains(';'))
                {
                    string comparisonUntrimmed = currentInstruction.ToString().Split(';')[1];

                    //string comparisonUntrimmed = currentInstruction.ToString().Substring(semicolonIndex + 1, currentInstruction.Length - 1);
                    return comparisonUntrimmed.Trim();
                }
            }
            return "";
        }
    }
}
