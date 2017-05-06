using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;

namespace TesteModificarTibia
{
    class ProcessMemoryReader
    {
        [DllImport("kernel32.dll")]
        public static extern IntPtr OpenProcess(int dwDesiredAccess, bool bInheritHandle, int dwProcessId);

        [DllImport("kernel32.dll")]
        public static extern bool ReadProcessMemory(int hProcess,
          int lpBaseAddress, byte[] lpBuffer, int dwSize, ref int lpNumberOfBytesRead);

        const int PROCESS_WM_READ = 0x0010;

        public Process process { get; private set; }
        public IntPtr processHandle { get; private set; }

        public ProcessMemoryReader(Process process)
        {
            this.process = process;
            processHandle = OpenProcess(PROCESS_WM_READ, false, process.Id);
        }

        public IntPtr GetProcessBaseAdress()
        {
            return process.MainModule.BaseAddress;
        }

        public UInt32 GetInt32(IntPtr adress)
        {
            byte[] buffer = new byte[4];
            int bytesRead = 0;

            ReadProcessMemory((int)processHandle, (int)adress, buffer, 4, ref bytesRead);


            return BitConverter.ToUInt32(buffer, 0);
        }

        public bool GetBool(IntPtr address)
        {
            byte[] buffer = new byte[1];
            int bytesRead = 0;

            ReadProcessMemory((int)processHandle, (int)address, buffer, 1, ref bytesRead);


            return BitConverter.ToBoolean(buffer, 0);
        }
    }
}
