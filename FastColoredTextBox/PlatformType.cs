using System;
using System.Runtime.InteropServices;
namespace FastColoredTextBoxNS
{
    public static class PlatformType
    {
        private const ushort PROCESSOR_ARCHITECTURE_AMD64 = 9;
        private const ushort PROCESSOR_ARCHITECTURE_IA64 = 6;
        private const ushort PROCESSOR_ARCHITECTURE_INTEL = 0;
        private const ushort PROCESSOR_ARCHITECTURE_UNKNOWN = 0xffff;

        public static Platform GetOperationSystemPlatform()
        {
            SYSTEM_INFO lpSystemInfo = new SYSTEM_INFO();
            if ((Environment.OSVersion.Version.Major > 5) || ((Environment.OSVersion.Version.Major == 5) && (Environment.OSVersion.Version.Minor >= 1)))
            {
                GetNativeSystemInfo(ref lpSystemInfo);
            }
            else
            {
                GetSystemInfo(ref lpSystemInfo);
            }
            switch (lpSystemInfo.wProcessorArchitecture)
            {
                case 0:
                    return Platform.X86;

                case 6:
                case 9:
                    return Platform.X64;
            }
            return Platform.Unknown;
        }

        [DllImport("kernel32.dll")]
        private static extern void GetNativeSystemInfo(ref SYSTEM_INFO lpSystemInfo);
        [DllImport("kernel32.dll")]
        private static extern void GetSystemInfo(ref SYSTEM_INFO lpSystemInfo);

        [StructLayout(LayoutKind.Sequential)]
        private struct SYSTEM_INFO
        {
            public ushort wProcessorArchitecture;
            public ushort wReserved;
            public uint dwPageSize;
            public IntPtr lpMinimumApplicationAddress;
            public IntPtr lpMaximumApplicationAddress;
            public UIntPtr dwActiveProcessorMask;
            public uint dwNumberOfProcessors;
            public uint dwProcessorType;
            public uint dwAllocationGranularity;
            public ushort wProcessorLevel;
            public ushort wProcessorRevision;
        }
    }
}