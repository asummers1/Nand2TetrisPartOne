using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HackAssembler
{
    internal class SymbolTable
    {
        Dictionary<string, int> symbolTable;
        public const int BaseScreenAddress = 16384;
        public const int KeyboardAddress = 24576;
        public SymbolTable()
        {
            symbolTable = new Dictionary<string, int>();
        }
        public void addEntry(string symbol, int address)
        {
            if (!symbolTable.TryAdd(symbol.Trim('(', ')'), address))
            {
                throw new ArgumentOutOfRangeException();
            }
        }
        public bool contains(string symbol) => symbolTable.ContainsKey(symbol);

        public int getAddress(string symbol)
        {
            if (symbolTable.TryGetValue(symbol, out int value))
            {
                return value;
            }
            else
            {
                throw new ArgumentOutOfRangeException();
            }
        }
        
    }
}
