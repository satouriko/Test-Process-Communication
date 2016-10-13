using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace _2645实验室
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
        private void button1_Click(object sender, EventArgs e)
        {
            //获取测试程序的窗体句柄
            IntPtr mainWnd = FindWindow(null, "未来代码研究所");
            List<IntPtr> listWnd = new List<IntPtr>();
            //获取窗体上OK按钮的句柄 
            IntPtr hwnd_button = FindWindowEx(mainWnd, new IntPtr(0), null, "button1");
            SendMessage(hwnd_button, WM_CLICK, mainWnd, "0");
            //获取窗体上所有控件的句柄
            EnumChildWindows(mainWnd, new CallBack(delegate (IntPtr hwnd, int lParam)
            {
                listWnd.Add(hwnd);
                return true;
            }), 0);
            //foreach (IntPtr item in listWnd)
            //{
              
            //    if (item != hwnd_button)
            //    {
            //        char[] UserChar = "luminji".ToCharArray();
            //        foreach (char ch in UserChar)
            //        {
            //            SendChar(item, ch, 100);
            //        }
            //    }
            //}

        }

        public void SendChar(IntPtr hand, char ch, int SleepTime)
        {
            PostMessage(hand, WM_CHAR, ch, 0);
            System.Threading.Thread.Sleep(SleepTime);
        }

        public static int WM_CHAR = 0x102;
        public static int WM_CLICK = 0x00F5;

        [DllImport("User32.dll", EntryPoint = "SendMessage")]
        public static extern int SendMessage(IntPtr hWnd, int Msg, IntPtr wParam, string lParam);

        [DllImport("user32.dll")]
        public static extern IntPtr FindWindowEx(IntPtr hwndParent, IntPtr hwndChildAfter,
            string lpszClass, string lpszWindow);

        [DllImport("user32.dll", SetLastError = true)]
        public static extern IntPtr FindWindow(string lpClassName, string lpWindowName);

        [DllImport("user32.dll")]
        public static extern int AnyPopup();

        [DllImport("user32.dll", CharSet = CharSet.Auto, SetLastError = true)]
        public static extern int GetWindowText(IntPtr hWnd, StringBuilder lpString, int nMaxCount);

        [DllImport("user32.dll")]
        public static extern int EnumThreadWindows(IntPtr dwThreadId, CallBack lpfn, int lParam);

        [DllImport("user32.dll")]
        public static extern int EnumChildWindows(IntPtr hWndParent, CallBack lpfn, int lParam);

        [DllImport("user32.dll", CharSet = CharSet.Ansi)]
        public static extern IntPtr PostMessage(IntPtr hwnd, int wMsg, int wParam, int lParam);

        [DllImport("user32.dll", CharSet = CharSet.Ansi)]
        public static extern IntPtr SendMessage(IntPtr hwnd, int wMsg, IntPtr wParam, IntPtr lParam);

        [DllImport("user32.dll", CharSet = CharSet.Unicode)]
        public static extern IntPtr SendMessageA(IntPtr hwnd, int wMsg, int wParam, int lParam);

        [DllImport("user32.dll", CharSet = CharSet.Auto)]
        static extern int GetClassName(IntPtr hWnd, StringBuilder lpClassName, int nMaxCount);

        [DllImport("user32.dll", SetLastError = true, CharSet = CharSet.Auto)]
        public static extern int GetWindowTextLength(IntPtr hWnd);

        [DllImport("user32.dll", CharSet = CharSet.Auto, SetLastError = false)]
        public static extern IntPtr GetParent(IntPtr hWnd);

        public delegate bool CallBack(IntPtr hwnd, int lParam);

        [DllImport("user32.dll", SetLastError = true)]
        static extern IntPtr GetWindow(IntPtr hWnd, uint uCmd);
    }
}
