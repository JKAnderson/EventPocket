using System;
using System.Collections.Generic;
using System.Diagnostics;

namespace EventPocket
{
    class DSProcess
    {
        public static bool GetProcess(out DSProcess dsProcess)
        {
            foreach (Process process in Process.GetProcesses())
            {
                try
                {
                    if (process.MainWindowTitle == "DARK SOULS")
                    {
                        dsProcess = new DSProcess(process, false);
                        return true;
                    }
                    else if (process.MainWindowTitle == "DARK SOULS™: REMASTERED")
                    {
                        dsProcess = new DSProcess(process, true);
                        return true;
                    }
                }
                catch { }
            }

            dsProcess = null;
            return false;
        }

        public bool Remastered;

        private Process process;
        private DSInterface dsInterface;
        private IntPtr eventFlagPtr;

        private DSProcess(Process process, bool remastered)
        {
            this.process = process;
            Remastered = remastered;
            dsInterface = new DSInterface(process, remastered);

            int size = process.MainModule.ModuleMemorySize;

            DSInterface.AOBScanner scanner = dsInterface.GetAOBScanner();
            if (remastered)
            {
                eventFlagPtr = scanner.Scan(DSOffsets.EventFlagsAOBR, 3);
            }
            else
            {
                eventFlagPtr = scanner.Scan(DSOffsets.EventFlagsAOB);
                eventFlagPtr = dsInterface.ReadIntPtr(eventFlagPtr + 1);
            }
        }

        public void Close()
        {
            dsInterface.Close();
        }

        public bool Alive()
        {
            return !process.HasExited;
        }

        private static Dictionary<string, int> eventFlagGroups = new Dictionary<string, int>()
        {
            {"0", 0x00000},
            {"1", 0x00500},
            {"5", 0x05F00},
            {"6", 0x0B900},
            {"7", 0x11300},
        };

        private static Dictionary<string, int> eventFlagAreas = new Dictionary<string, int>()
        {
            {"000", 00},
            {"100", 01},
            {"101", 02},
            {"102", 03},
            {"110", 04},
            {"120", 05},
            {"121", 06},
            {"130", 07},
            {"131", 08},
            {"132", 09},
            {"140", 10},
            {"141", 11},
            {"150", 12},
            {"151", 13},
            {"160", 14},
            {"170", 15},
            {"180", 16},
            {"181", 17},
        };

        private IntPtr getEventFlagAddress(IntPtr eventFlagAddr, int ID, out uint mask)
        {
            string idString = ID.ToString("D8");
            if (idString.Length == 8)
            {
                string group = idString.Substring(0, 1);
                string area = idString.Substring(1, 3);
                int section = Int32.Parse(idString.Substring(4, 1));
                int number = Int32.Parse(idString.Substring(5, 3));

                if (eventFlagGroups.ContainsKey(group) && eventFlagAreas.ContainsKey(area))
                {
                    int offset = eventFlagGroups[group];
                    offset += eventFlagAreas[area] * 0x500;
                    offset += section * 128;
                    offset += (number - (number % 32)) / 8;

                    mask = 0x80000000 >> (number % 32);
                    return eventFlagAddr + offset;
                }
            }
            throw new ArgumentException("Unknown event flag ID: " + ID);
        }

        public bool ReadEventFlag(int ID)
        {
            IntPtr eventFlagAddr = dsInterface.ReadIntPtr(eventFlagPtr) + DSOffsets.EventFlagsOffset;
            eventFlagAddr = dsInterface.ReadIntPtr(eventFlagAddr);
            IntPtr address = getEventFlagAddress(eventFlagAddr, ID, out uint mask);
            return dsInterface.ReadFlag32(address, mask);
        }

        public int ReadEventValue(int ID, int width)
        {
            int value = 0;
            for (int i = 0; i < width; i++)
            {
                bool flag = ReadEventFlag(ID + i);
                if (flag)
                    value += 1 << (width - i);
            }
            return value;
        }
    }
}
