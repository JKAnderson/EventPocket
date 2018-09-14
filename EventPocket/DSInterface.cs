using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.InteropServices;

namespace EventPocket
{
    class DSInterface
    {
        private const uint MEM_COMMIT = 0x1000;
        private const uint PAGE_EXECUTE_ANY = 0xF0;
        private const uint PAGE_GUARD = 0x100;

        [DllImport("kernel32.dll")]
        private static extern bool ReadProcessMemory(IntPtr hProcess, IntPtr lpBaseAddress, byte[] lpBuffer, uint nSize, uint lpNumberOfBytesRead);

        [DllImport("kernel32.dll", SetLastError = true)]
        private static extern uint VirtualQueryEx(IntPtr hProcess, IntPtr lpAddress, out MEMORY_BASIC_INFORMATION lpBuffer, uint dwLength);

        [StructLayout(LayoutKind.Sequential)]
        protected struct MEMORY_BASIC_INFORMATION
        {
            public IntPtr BaseAddress;
            public IntPtr AllocationBase;
            public uint AllocationProtect;
            public ulong RegionSize;
            public uint State;
            public uint Protect;
            public uint Type;
        }

        private Process process;
        private IntPtr handle;
        private bool remaster;

        public DSInterface(Process process, bool remaster)
        {
            this.process = process;
            handle = process.Handle;
            this.remaster = remaster;
        }

        public void Close()
        {
            process.Close();
        }

        private byte[] ReadProcessMemory(IntPtr address, uint size)
        {
            byte[] result = new byte[size];
            ReadProcessMemory(handle, address, result, size, 0);
            return result;
        }

        public byte[] ReadBytes(IntPtr address, int size)
        {
            return ReadProcessMemory(address, (uint)size);
        }

        public int ReadInt32(IntPtr address)
        {
            byte[] bytes = ReadProcessMemory(address, 4);
            return BitConverter.ToInt32(bytes, 0);
        }

        public long ReadInt64(IntPtr address)
        {
            byte[] bytes = ReadProcessMemory(address, 8);
            return BitConverter.ToInt64(bytes, 0);
        }

        public IntPtr ReadIntPtr(IntPtr address)
        {
            if (remaster)
                return (IntPtr)ReadInt64(address);
            else
                return (IntPtr)ReadInt32(address);
        }

        public bool ReadFlag32(IntPtr address, uint mask)
        {
            byte[] bytes = ReadProcessMemory(address, 4);
            uint flags = BitConverter.ToUInt32(bytes, 0);
            return (flags & mask) != 0;
        }

        public AOBScanner GetAOBScanner()
        {
            return new AOBScanner(process, handle, this);
        }

        public class AOBScanner
        {
            private DSInterface dsInterface;
            private List<MEMORY_BASIC_INFORMATION> memRegions;
            private Dictionary<IntPtr, byte[]> readMemory;

            public AOBScanner(Process process, IntPtr handle, DSInterface dsInterface)
            {
                this.dsInterface = dsInterface;
                memRegions = new List<MEMORY_BASIC_INFORMATION>();
                IntPtr memRegionAddr = process.MainModule.BaseAddress;
                IntPtr mainModuleEnd = process.MainModule.BaseAddress + process.MainModule.ModuleMemorySize;
                uint queryResult;

                do
                {
                    MEMORY_BASIC_INFORMATION memInfo = new MEMORY_BASIC_INFORMATION();
                    queryResult = VirtualQueryEx(handle, memRegionAddr, out memInfo, (uint)Marshal.SizeOf(memInfo));
                    if (queryResult != 0)
                    {
                        if ((memInfo.State & MEM_COMMIT) != 0 && (memInfo.Protect & PAGE_GUARD) == 0 && (memInfo.Protect & PAGE_EXECUTE_ANY) != 0)
                            memRegions.Add(memInfo);
                        memRegionAddr = (IntPtr)((ulong)memInfo.BaseAddress.ToInt64() + memInfo.RegionSize);
                    }
                } while (queryResult != 0 && memRegionAddr.ToInt64() < mainModuleEnd.ToInt64());

                readMemory = new Dictionary<IntPtr, byte[]>();
                foreach (MEMORY_BASIC_INFORMATION memRegion in memRegions)
                    readMemory[memRegion.BaseAddress] = dsInterface.ReadBytes(memRegion.BaseAddress, (int)memRegion.RegionSize);
            }

            public IntPtr Scan(byte?[] aob)
            {
                List<IntPtr> results = new List<IntPtr>();
                foreach (IntPtr baseAddress in readMemory.Keys)
                {
                    byte[] bytes = readMemory[baseAddress];

                    for (int i = 0; i < bytes.Length - aob.Length; i++)
                    {
                        bool found = true;
                        for (int j = 0; j < aob.Length; j++)
                        {
                            if (aob[j] != null && aob[j] != bytes[i + j])
                            {
                                found = false;
                                break;
                            }
                        }

                        if (found)
                        {
                            // Originally I was scanning everything every time to make sure there weren't multiple results,
                            // but it was slow enough to be obnoxious, so just break out once you find one.
                            results.Add(baseAddress + i);
                            break;
                        }
                    }

                    if (results.Count == 1)
                        break;
                }

                if (results.Count == 0)
                    throw new ArgumentException("AOB not found: " + aob.ToString());
                else if (results.Count > 1)
                    throw new ArgumentException("AOB found " + results.Count + " times: " + aob.ToString());
                return results[0];
            }

            public IntPtr Scan(byte?[] aob, int offset1, int offset2)
            {
                IntPtr result = Scan(aob);
                return result + dsInterface.ReadInt32(result + offset1) + offset2;
            }

            public IntPtr Scan(byte?[] aob, int offset)
            {
                return Scan(aob, offset, offset + 4);
            }
        }
    }
}
