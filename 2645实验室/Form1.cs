using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Imaging;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Forms;
using GraphicsHelper;

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
        }

        private void button3_Click(object sender, EventArgs e)
        {
            //获取测试程序的窗体句柄
            IntPtr mainWnd = FindWindow(null, "未来代码研究所");
            List<IntPtr> listWnd = new List<IntPtr>();
            //获取窗体上所有控件的句柄
            EnumChildWindows(mainWnd, new CallBack(delegate (IntPtr hwnd, int _lParam)
            {
                listWnd.Add(hwnd);
                StringBuilder className = new StringBuilder(126);
                StringBuilder title = new StringBuilder(200);
                GetWindowText(hwnd, title, 200);
                Rect clientRect;
                GetClientRect(hwnd, out clientRect);
                double controlWidth = clientRect.Width;
                double controlHeight = clientRect.Height;
                double x = 0, y = 0;
                IntPtr parerntHandle = GetParent(hwnd);
                if (parerntHandle != IntPtr.Zero)
                {
                    GetWindowRect(hwnd, out clientRect);
                    Rect rect;
                    GetWindowRect(parerntHandle, out rect);
                    x = clientRect.X - rect.X;
                    y = clientRect.Y - rect.Y;
                    Debug.WriteLine(x.ToString());
                    Debug.Print(y.ToString());
                }
                return true;
            }), 0);
            int looper = 0;
            foreach (IntPtr item in listWnd)
            {
                //MessageBox.Show(looper.ToString() + ":" + item.ToString());
                //if (item != hwnd_button)
                //{
                //    char[] UserChar = "luminji".ToCharArray();
                //    foreach (char ch in UserChar)
                //    {
                //        SendChar(item, ch, 100);
                //    }
                //}
                if (looper == 4)
                {
                    int x = 20; // X coordinate of the click 
                    int y = 250; // Y coordinate of the click 
                    IntPtr handle = item;
                    //StringBuilder className = new StringBuilder(100);
                    //while (className.ToString() != "Internet Explorer_Server") // The class control for the browser 
                    //{
                    //    handle = GetWindow(handle, 5); // Get a handle to the child window 
                    //    GetClassName(handle, className, className.Capacity);
                    //}

                    IntPtr lParam = (IntPtr)((y << 16) | x); // The coordinates 
                    IntPtr wParam = IntPtr.Zero; // Additional parameters for the click (e.g. Ctrl) 
                    const int downCode = 0x201; // Left click down code 
                    const int upCode = 0x202; // Left click up code 
                    SendMessage(handle, downCode, wParam, lParam); // Mouse button down 
                    SendMessage(handle, upCode, wParam, lParam); // Mouse button up
                }
                looper++;
            }
        }
        [DllImport("user32")]
        public static extern bool GetClientRect(IntPtr hwnd, out Rect lpRect);
        [DllImport("user32")]
        public static extern bool GetWindowRect(IntPtr hWnd, out Rect rect);
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

        private void button4_Click(object sender, EventArgs e)
        {
            Image baseImage = new Bitmap(Screen.PrimaryScreen.Bounds.Width, Screen.PrimaryScreen.Bounds.Height);
            Graphics g = Graphics.FromImage(baseImage);
            System.Drawing.Size sz = Screen.AllScreens[0].Bounds.Size;
            sz.Width = sz.Width;
            sz.Height = sz.Height;
            g.CopyFromScreen(new System.Drawing.Point(0, 0), new System.Drawing.Point(0, 0),sz);
            g.Dispose();

            Bitmap baseRes = new Bitmap(baseImage);

            Dictionary<int, int> verLines_raw = new Dictionary<int, int>();
            HashSet<int> horLines_raw = new HashSet<int>();

            baseRes = ImageAnalysis.binaryzation(baseRes, 127);
            int[] baseVertical = ImageAnalysis.getVerticalHistogram(baseRes);
            int[] baseHorizontal = ImageAnalysis.getHorizontalHistogram(baseRes);

            //获取铅垂线
            for (int i = 0; i < baseRes.Width; i++)
            {
                if (baseVertical[i] > baseRes.Height / 4)
                    verLines_raw.Add(i, baseVertical[i]);
            }

            int tableWidth = verLines_raw.Max(item => item.Key) - verLines_raw.Min(item => item.Key);
            //MessageBox.Show(verLines_raw.Max(item => item.Value).ToString() + verLines_raw.Min(item => item.Value).ToString());

            //获取水平线
            for (int j = 0; j < baseRes.Height; j++)
            {
                if (baseHorizontal[j] > tableWidth / 2)
                    horLines_raw.Add(j);
            }

            //合并连续区段
            Dictionary<int, int> verLines_dic = new Dictionary<int, int>();
            HashSet<int> horLines = new HashSet<int>();
            HashSet<int> verLines = new HashSet<int>();
            int temp = 0;
            foreach(int i in horLines_raw)
            {
                if (i - temp != 1)
                    horLines.Add(temp);
                temp = i;
            }
            horLines.Add(temp);
            horLines.Remove(0);
            KeyValuePair<int, int> kvtemp = new KeyValuePair<int, int>(0,0);
            foreach (KeyValuePair<int, int> kv in verLines_raw)
            {
                if (kv.Key - kvtemp.Key != 1)
                    verLines_dic.Add(kvtemp.Key, kvtemp.Value);
                kvtemp = kv;
            }
            verLines_dic.Add(kvtemp.Key, kvtemp.Value);
            verLines_dic.Remove(0);

            //铅垂线只保留六根
            verLines_dic = verLines_dic.OrderByDescending(item => item.Value).ToDictionary(item => item.Key, item => item.Value);
            int cnt = 0;
            foreach(KeyValuePair<int, int> kv in verLines_dic)
            {
                if (cnt >= 6)
                    break;
                verLines.Add(kv.Key);
                cnt++;
            }

            //寻找奇异顶点
            List<System.Drawing.Point> strangePoints = new List<System.Drawing.Point>();
            foreach(int j in horLines)
            {
                foreach(int i in verLines)
                {
                    int statistics = 0;
                    for(int k = 1; k <= 4; ++k)
                    {
                        //上面没有线 下面有线
                        if (j - k >= 0 && baseRes.GetPixel(i, j - k).R > 127 
                            && j + k < baseRes.Height && baseRes.GetPixel(i, j + k).R < 127)
                            ++statistics;
                    }
                    if (statistics > 2)
                        strangePoints.Add(new System.Drawing.Point(i, j));
                }
            }

            //裁剪
            for(int i = 0; i + 2 < strangePoints.Count;)
            {
                if (strangePoints[i + 1].Y == strangePoints[i + 2].Y 
                    && strangePoints[i + 1].Y - strangePoints[i].Y > 1
                    && verLines.Max() - strangePoints[i].X - 1 > 1)
                {
                    Rectangle rect = new Rectangle(strangePoints[i].X + 1, strangePoints[i].Y + 1,
                        verLines.Max() - strangePoints[i].X - 1,
                        strangePoints[i + 1].Y - strangePoints[i].Y - 1);
                    Bitmap bmpCrop = baseRes.Clone(rect, baseRes.PixelFormat);
                    bmpCrop.Save((i / 3).ToString() + ".png");
                    i += 3;
                    //SimilarityTest
                    Bitmap testImage = new Bitmap(Image.FromFile("test.jpg"));
                    //Bitmap bmp1 = bmpCrop, bmp2 = testImage;
                    ////bmp1 = ImageAnalysis.binaryzation(bmp1);
                    //bmp2 = ImageAnalysis.binaryzation(bmp2);
                    //bmp1 = ImageAnalysis.removeBlankVerticalLines(bmp1);
                    //bmp2 = ImageAnalysis.removeBlankVerticalLines(bmp2);
                    //bmp1 = ImageAnalysis.ResizeImage(bmp1, bmp2.Width, bmp1.Height);
                    //bmp1 = ImageAnalysis.binaryzation(bmp1, 200);
                    //bmp1.Save("simtest" + i.ToString() + "_1.png", ImageFormat.Png);
                    //bmp2.Save("simtest" + i.ToString() + "_2.png", ImageFormat.Png);
                    //bmp1 = ImageAnalysis.getVerticalHistogram(ImageAnalysis.getVerticalHistogram(bmp1), bmp1.Height);
                    //bmp2 = ImageAnalysis.getVerticalHistogram(ImageAnalysis.getVerticalHistogram(bmp2), bmp2.Height);


                    double sim = ImageAnalysis.getVerticalSimilarity(bmpCrop, testImage);
                    MessageBox.Show(sim.ToString());
                }
                else
                    i += 1;
            }

            //调试输出Hor,verMap
            Bitmap baseHorMap = ImageAnalysis.getHorizontalHistogram(baseHorizontal, baseRes.Width);
            baseHorMap.Save("baseHorMap.png", ImageFormat.Png);
            Bitmap baseVerMap = ImageAnalysis.getVerticalHistogram(baseVertical, baseRes.Height);
            baseVerMap.Save("baseVerMap.png", ImageFormat.Png);

            Bitmap baseVerMap_new = ImageAnalysis.getVerticalHistogram(
                ImageAnalysis.getVerticalHistogram(
                    ImageAnalysis.binaryzation(ImageAnalysis.ResizeImage(baseRes, 500, baseRes.Height), 255)
                    ), 
                baseRes.Height);
            Bitmap baseVerMap_new2 = ImageAnalysis.ResizeImage(baseRes, 500, baseRes.Height);


            baseVerMap_new.Save("baseVerMap_new.png", ImageFormat.Png);
            baseVerMap_new2.Save("baseVerMap_new2.png", ImageFormat.Png);
            //调试输出BaseMap
            string horLineStr = "", verLineStr = "", strangePointStr = "";
            
            Bitmap baseMap = new Bitmap(baseRes.Width, baseRes.Height);
            foreach(int j in horLines)
            {
                for(int i = 0; i < baseRes.Width; ++i)
                {
                    Color newColor = Color.FromArgb(255, 255, 255);
                    baseMap.SetPixel(i, j, newColor);
                }
                horLineStr += j.ToString() + ", ";
            }
            foreach (int i in verLines)
            {
                for (int j = 0; j < baseRes.Height; ++j)
                {
                    Color newColor = Color.FromArgb(255, 255, 255);
                    baseMap.SetPixel(i, j, newColor);
                }
                verLineStr += i.ToString() + ", ";
            }
            foreach (System.Drawing.Point pt in strangePoints)
            {
                Color newColor = Color.FromArgb(255, 0, 0);
                baseMap.SetPixel(pt.X, pt.Y, newColor);
                strangePointStr += "(" + pt.X.ToString() + ", " + pt.Y.ToString() + "), ";
            }

            MessageBox.Show(horLineStr, "HorLines");
            MessageBox.Show(verLineStr, "VerLines");
            MessageBox.Show(strangePointStr, "strangePoints");

            baseRes.Save("baseImage.png", ImageFormat.Png);
            baseMap.Save("baseMap.png", ImageFormat.Png);
        }
    }
}
