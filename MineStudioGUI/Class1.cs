using System;
using System.Runtime.InteropServices;

namespace MineStudio.GUI
{
    public static class Class1
    {
        [DllImport("user32.dll")]
        public static extern bool SetCursorPos(int x, int y);
        [DllImport("user32.dll")]
        public static extern void mouse_event(MouseEventFlag flags, int dx, int dy, uint data, UIntPtr extraInfo);
        [Flags]
        public enum MouseEventFlag : uint
        {
            Move = 0x0001,
            LeftDown = 0x0002,
            LeftUp = 0x0004,
            RightDown = 0x0008,
            RightUp = 0x0010,
            MiddleDown = 0x0020,
            MiddleUp = 0x0040,
            XDown = 0x0080,
            XUp = 0x0100,
            Wheel = 0x0800,
            VirtualDesk = 0x4000,
            Absolute = 0x8000
        }

       
        public static void Click(int x, int y,int u=1)
        {
            SetCursorPos(x, y);
            if (u > 0) {
                mouse_event(MouseEventFlag.LeftDown, 10, 10, 0, UIntPtr.Zero);
                mouse_event(MouseEventFlag.LeftUp, 10, 10, 0, UIntPtr.Zero);
            } else {
                mouse_event(MouseEventFlag.RightDown, 10, 10, 0, UIntPtr.Zero);
                mouse_event(MouseEventFlag.RightUp, 10, 10, 0, UIntPtr.Zero);
            }
        }

     
    }
}