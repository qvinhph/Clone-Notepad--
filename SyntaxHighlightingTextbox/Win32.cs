using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.InteropServices;

namespace SyntaxHighlightingTextbox
{
    /// <summary>
    /// 
    /// </summary>
    public class Win32
    {
        private Win32() { }

        public const int WM_USER = 0x400;
        public const int WM_PAINT = 0xF;
        public const int WM_KEYDOWN = 0x100;
        public const int WM_KEYUP = 0x101;
        public const int WM_CHAR = 0x102;
        public const int WM_SETREDRAW = 0x0b;

        public const int EM_GETSCROLLPOS = (WM_USER + 221);
        public const int EM_SETSCROLLPOS = (WM_USER + 222);

        public const int VK_CONTROL = 0x11;
        public const int VK_UP = 0x26;
        public const int VK_DOWN = 0x28;
        public const int VK_NUMLOCK = 0x90;

        public const short KS_ON = 0x01;
        public const short KS_KEYDOWN = 0x80;

        public struct POINT
        {
            public long x;
            public long y;

            public POINT(int a, int b)
            {
                x = a;
                y = b;
            }
        }

        /// <summary>
        /// The SendMessage function calls the window procedure for the specified window.
        /// </summary>
        /// <param name="hwnd">A handle to the window whose window procedure will receive the message.</param>
        /// <param name="Msg">The message to be sent.</param>
        /// <param name="wParam">Additional message-specific information.</param>
        /// <param name="point">The value return from the procedure.</param>
        /// <returns></returns>
        [DllImport("user32")]
        public static extern int SendMessage(IntPtr hwnd, int Msg, int wParam, ref POINT point);
        

        /// <summary>
        /// The SendMessage function calls the window procedure for the specified window.
        /// </summary>
        /// <param name="hwnd">A handle to the window whose window procedure will receive the message.</param>
        /// <param name="Msg">The message to be sent.</param>
        /// <param name="wParam">Additional message-specific information.</param>
        /// <param name="point">This parameter is depended on the messages.</param>
        /// <returns></returns>
        [DllImport("user32")]
        public static extern int SendMessage(IntPtr hwnd, int Msg, int wParam, int lParam);

        [DllImport("user32")]
        public static extern short GetKeyState(int nVirtKey);

        /// <summary>
        /// Disables or enables drawing in the specified window.
        /// </summary>
        /// <param name="hwnd">The handle to the window in which drawing will be disabled. If this parameter is NULL, drawing in the locked window is enabled.</param>
        /// <returns></returns>
    }
}
