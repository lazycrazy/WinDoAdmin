using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.InteropServices;

namespace WinDo.Utilities
{
    public enum OSName
    {
        UNKNOWN,
        Win95,
        Win98,
        WinME,
        WinNT3,
        WinNT4,
        Win2000,
        WinXP,
        Win2003,
        Win7,
    }

    public struct OSVERSIONINFO
    {
        public uint dwOSVersionInfoSize;
        public uint dwMajorVersion;
        public uint dwMinorVersion;
        public uint dwBuildNumber;
        public uint dwPlatformId;
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 128)]
        public string szCSDVersion;
        public Int16 wServicePackMajor;
        public Int16 wServicePackMinor;
        public Int16 wSuiteMask;
        public Byte wProductType;
        public Byte wReserved;
    }
   public static class OS
    {
        public const int VER_PLATFORM_WIN32_NT = 2;

        [DllImport("Kernel32.dll")]
        public static extern bool GetVersionEx(ref OSVERSIONINFO osvi);

        public static OSName GetVersion()
        {
            OSVERSIONINFO osvi = new OSVERSIONINFO();
            osvi.dwOSVersionInfoSize = (uint)Marshal.SizeOf(osvi);
            OSName ret;
            if (GetVersionEx(ref osvi))
            {
                if (osvi.dwPlatformId == VER_PLATFORM_WIN32_NT)
                {
                    #region Win32平台
                    switch (osvi.dwMajorVersion)
                    {
                        case 3:
                            ret = OSName.WinNT3;
                            break;
                        case 4:
                            ret = OSName.WinNT4;
                            break;
                        case 5:
                            switch (osvi.dwMinorVersion)
                            {
                                case 0:
                                    ret = OSName.Win2000;
                                    break;
                                case 1:
                                    ret = OSName.WinXP;
                                    break;
                                case 2:
                                    ret = OSName.Win2003;
                                    break;
                                default:
                                    ret = OSName.UNKNOWN;
                                    break;
                            }
                            break;
                        case 6:
                            ret = OSName.Win7;
                            break;
                        default:
                            ret = OSName.UNKNOWN;
                            break;
                    }
                    #endregion
                }
                else
                {
                    #region Win16平台
                    if (osvi.dwMajorVersion == 4)
                    {
                        //Win9X系列
                        switch (osvi.dwMinorVersion)
                        {
                            case 0:
                                ret = OSName.Win95;
                                break;
                            case 10:
                                ret = OSName.Win98;
                                break;
                            case 90:
                                ret = OSName.WinME;
                                break;
                            default:
                                ret = OSName.UNKNOWN;
                                break;
                        }
                    }
                    else
                    {
                        ret = OSName.UNKNOWN;
                    }
                    #endregion
                }
            }
            else
            {
                ret = OSName.UNKNOWN;
            }
            return ret;
        }
    }
}
