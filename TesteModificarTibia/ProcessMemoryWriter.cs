using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;

namespace TesteModificarTibia
{
    class ProcessMemoryWriter
    {
        [DllImport("kernel32.dll")]
        public static extern IntPtr OpenProcess(int dwDesiredAccess, bool bInheritHandle, int dwProcessId);

        [DllImport("kernel32.dll", SetLastError = true)]
        static extern bool WriteProcessMemory(int hProcess, int lpBaseAddress, byte[] lpBuffer, int dwSize, ref int lpNumberOfBytesWritten);

        const int PROCESS_VM_WRITE = 0x0020;
        const int PROCESS_VM_OPERATION = 0x0008;

        public Process process { get; private set; }
        public IntPtr processHandle { get; private set; }

        ProcessMemoryWriter(Process process)
        {
            this.process = process;
            processHandle = OpenProcess(0x1F0FFF, false, process.Id);
        }

        public void SetInt32(IntPtr address, int value)
        {
            int bytesWritten = 0;
            byte[] buffer = BitConverter.GetBytes(value);

            WriteProcessMemory((int)processHandle, (int)address, buffer, buffer.Length, ref bytesWritten);
        }

        public void SetBool(IntPtr address, bool value)
        {
            int bytesWritten = 0;
            byte[] buffer = BitConverter.GetBytes(value);

            WriteProcessMemory((int)processHandle, (int)address, buffer, buffer.Length, ref bytesWritten);
        }
    }
}
