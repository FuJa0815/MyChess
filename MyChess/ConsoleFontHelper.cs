using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Text;

namespace MyChess
{
    public static class ConsoleFontHelper
    {
        public static void Init()
        {
            SetConsoleFont("NSimSun");
            Console.OutputEncoding = System.Text.Encoding.Unicode;
        }


        [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Unicode)]
        internal unsafe struct CONSOLE_FONT_INFO_EX
        {
            internal       uint  cbSize;
            internal       uint  nFont;
            internal       COORD dwFontSize;
            internal       int   FontFamily;
            internal       int   FontWeight;
            internal fixed char  FaceName[LF_FACESIZE];
        }

        [StructLayout(LayoutKind.Sequential)]
        internal struct COORD
        {
            internal short X;
            internal short Y;

            internal COORD(short x, short y)
            {
                X = x;
                Y = y;
            }
        }

        private const  int    STD_OUTPUT_HANDLE    = -11;
        private const  int    TMPF_TRUETYPE        = 4;
        private const  int    LF_FACESIZE          = 32;
        private static IntPtr INVALID_HANDLE_VALUE = new IntPtr(-1);

        [DllImport("kernel32.dll", SetLastError = true)]
        static extern bool SetCurrentConsoleFontEx(
            IntPtr                   consoleOutput,
            bool                     maximumWindow,
            ref CONSOLE_FONT_INFO_EX consoleCurrentFontEx);

        [DllImport("kernel32.dll", SetLastError = true)]
        static extern IntPtr GetStdHandle(int dwType);


        [DllImport("kernel32.dll", SetLastError = true)]
        static extern int SetConsoleFont(
            IntPtr hOut,
            uint   dwFontNum
        );

        public static unsafe void SetConsoleFont(string fontName)
        {
            unsafe
            {
                IntPtr hnd = GetStdHandle(STD_OUTPUT_HANDLE);
                if (hnd != INVALID_HANDLE_VALUE)
                {
                    CONSOLE_FONT_INFO_EX newInfo = new CONSOLE_FONT_INFO_EX();
                    newInfo.cbSize     = (uint)Marshal.SizeOf(newInfo);
                    newInfo.FontFamily = TMPF_TRUETYPE;
                    IntPtr ptr = new IntPtr(newInfo.FaceName);
                    Marshal.Copy(fontName.ToCharArray(), 0, ptr, fontName.Length);

                    // Get some settings from current font.
                    newInfo.dwFontSize = new COORD(50, 50);
                    newInfo.FontWeight = 50;
                    SetCurrentConsoleFontEx(hnd, false, ref newInfo);
                }
            }
        }
    }
}
