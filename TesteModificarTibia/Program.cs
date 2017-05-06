using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TesteModificarTibia
{
    class Program
    {
        static void Main(string[] args)
        {
            ProcessMemoryReader processMemoryReader = new ProcessMemoryReader(System.Diagnostics.Process.GetProcessesByName("tibia")[0]);

            IntPtr baseAddress = processMemoryReader.GetProcessBaseAdress();

            UInt32 XOR = processMemoryReader.GetInt32(baseAddress + Addresses.adrXOR);
            Console.WriteLine("Health Points: " + (XOR ^ processMemoryReader.GetInt32(baseAddress + Addresses.adrMyHp)));
            Console.WriteLine("Mana Points: " + (XOR ^ processMemoryReader.GetInt32(baseAddress + Addresses.adrMyMana)));
            Console.WriteLine("Go?: " + processMemoryReader.GetBool(baseAddress + Addresses.adrGo));
        }
    }
}
