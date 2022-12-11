using System.IO;
using System.Linq.Expressions;

namespace HackAssembler
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Parser firstParse = new Parser(args[0] + ".asm");
            Parser secondParse = new Parser(args[0] + ".asm");
            int jumpLocationAddress = 0;
            int aInstructionStandardAddress = 16;
            

            //Initialization
            SymbolTable table = new SymbolTable();

            table.addEntry("SP", 0);
            table.addEntry("LCL", 1);
            table.addEntry("ARG", 2);
            table.addEntry("THIS", 3);
            table.addEntry("THAT", 4);
            table.addEntry("SCREEN", SymbolTable.BaseScreenAddress);
            table.addEntry("KBD", SymbolTable.KeyboardAddress);
            
            //First pass. We add all the lines involving () into the table.
            //The address for a line is the current line number + 1
            while (firstParse.hasMoreLines())
            {
                //Recall that advance() skips past comments and blank lines
                firstParse.advance();

                if (firstParse.instructionType() == Parser.InstructionTypes.L_INSTRUCTION)
                {
                    table.addEntry(firstParse.symbol(), jumpLocationAddress);
                }
                else 
                {
                    jumpLocationAddress++;
                }
            }

            //Second pass. We add all of the lines involving @ into the table, which start at address 16 (recall that by contrast (), or L-instructions, start at address 0)
            //We also put the symbolic instructions into their equivalent binary representation into the .asm file.
            using (StreamWriter strm = new StreamWriter(args[0] + ".hack"))
            {
                while (secondParse.hasMoreLines())
                {
                    secondParse.advance();

                    if (secondParse.instructionType() == Parser.InstructionTypes.A_INSTRUCTION)
                    {
                        //We already have the symbol -- put its address in the file
                        if (table.contains(secondParse.symbol().Trim('@')))
                        {
                            int parseAddress = table.getAddress(secondParse.symbol().Trim('@'));

                            //Convert value to 16-bit binary. Adapted from https://stackoverflow.com/a/40913333/11813067
                            strm.WriteLine(Convert.ToString(parseAddress, 2).PadLeft(16, '0'));
                        }
                        else //Our value is not in the table. We could be dealing with an integer, or an A-instruction.
                        {
                            //We have an integer
                            //The user can enter in a plain integer or one preceded by R (for 0..15)

                            if ((Int32.TryParse(secondParse.symbol().Trim('@'), out int value) ||
                                (secondParse.symbol()[1] == 'R' && int.TryParse(secondParse.symbol().ToString().Substring(2), out value) && value >= 0 && value <= 15)))
                                {
                                strm.WriteLine(Convert.ToString(value, 2).PadLeft(16, '0'));
                            }
                            else //We have an A-instruction
                            {
                               table.addEntry(secondParse.symbol().Trim('@'), aInstructionStandardAddress);

                               //Convert address to 16-bit binary. Adapted from https://stackoverflow.com/a/40913333/11813067
                               strm.WriteLine(Convert.ToString(aInstructionStandardAddress++, 2).PadLeft(16, '0'));

                            }
                        }
                        
                        
                    }
                    else if (secondParse.instructionType() == Parser.InstructionTypes.C_INSTRUCTION)
                    {
                        string dest = secondParse.dest();
                        string comp = secondParse.comp();
                        string jump = secondParse.jump();
                        strm.WriteLine("111" + Code.comp(comp) + Code.dest(dest) + Code.jump(jump));
                    }

                }
            }
        }
    }
}