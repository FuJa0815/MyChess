using System;
using System.Runtime.InteropServices;

namespace MyChess.OutputClasses
{
    public static class ConsoleFontHelper
    {
        public const ushort FontSize = 30;

        public static void ClearCurrentConsoleLine()
        {
            var currentLineCursor = Console.CursorTop;
            Console.SetCursorPosition(0, Console.CursorTop);
            Console.Write(new string(' ', Console.WindowWidth));
            Console.SetCursorPosition(0, currentLineCursor);
        }


        public static void Init()
        {
            SetConsoleFont("NSimSun");
            Console.OutputEncoding = System.Text.Encoding.Unicode;
        }


        [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Unicode)]
        internal unsafe struct ConsoleFontInfoEx
        {
            internal uint cbSize;
            internal uint nFont;
            internal Coordinate dwFontSize;
            internal int FontFamily;
            internal int FontWeight;
            internal fixed char FaceName[32];
        }

        [StructLayout(LayoutKind.Sequential)]
        internal struct Coordinate
        {
            internal short X;
            internal short Y;

            internal Coordinate(short x, short y)
            {
                X = x;
                Y = y;
            }
        }

        private const           int    StdOutputHandle      = -11;
        private const           int    TmpfTruetype         = 4;
        private static readonly IntPtr InvalidHandleValue = new IntPtr(-1);

        [DllImport("kernel32.dll", SetLastError = true)]
        private static extern bool SetCurrentConsoleFontEx(
            IntPtr consoleOutput,
            bool maximumWindow,
            ref ConsoleFontInfoEx consoleCurrentFontEx);

        [DllImport("kernel32.dll", SetLastError = true)]
        private static extern IntPtr GetStdHandle(int dwType);


        [DllImport("kernel32.dll", SetLastError = true)]
        private static extern int SetConsoleFont(
            IntPtr hOut,
            uint dwFontNum
        );

        public static unsafe void SetConsoleFont(string fontName)
        {
            var hnd = GetStdHandle(StdOutputHandle);
            if (hnd == InvalidHandleValue) return;
            var newInfo = new ConsoleFontInfoEx();
            newInfo.cbSize     = (uint)Marshal.SizeOf(newInfo);
            newInfo.FontFamily = TmpfTruetype;
            var ptr = new IntPtr(newInfo.FaceName);
            Marshal.Copy(fontName.ToCharArray(), 0, ptr, fontName.Length);

            // Get some settings from current font.
            newInfo.dwFontSize = new Coordinate((short)FontSize, (short)FontSize);
            newInfo.FontWeight = FontSize;
            SetCurrentConsoleFontEx(hnd, false, ref newInfo);
        }
    }
}
