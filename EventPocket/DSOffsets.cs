using System;
using System.Text.RegularExpressions;

namespace EventPocket
{
    static class DSOffsets
    {
        public static byte?[] EventFlagsAOB = getAOB("A1 ? ? ? ? 89 4B 30 3B C6");
        public static byte?[] EventFlagsAOBR = getAOB("48 8B 0D ? ? ? ? 99 33 C2 45 33 C0 2B C2 8D 50 F6");
        public static int EventFlagsOffset = 0x0;

        private static byte?[] getAOB(string text)
        {
            MatchCollection matches = Regex.Matches(text, @"\S+");
            byte?[] aob = new byte?[matches.Count];
            for (int i = 0; i < aob.Length; i++)
            {
                Match match = matches[i];
                if (match.Value == "?")
                    aob[i] = null;
                else
                    aob[i] = Byte.Parse(match.Value, System.Globalization.NumberStyles.AllowHexSpecifier);
            }
            return aob;
        }
    }
}
