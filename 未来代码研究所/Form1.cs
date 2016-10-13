using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace 未来代码研究所
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            webBrowser1.DocumentText = "<!DOCTYPE html><html><head><script>function a1() { alert('我是原生的');} </script></head><body><button onclick='a1()'>button1</button><br /></body></html>";
            Debug.WriteLine(webBrowser1.DocumentText);

            //Debug.WriteLine(webBrowser1.DocumentText);

            bool flag = false;

            webBrowser1.DocumentCompleted += delegate (object sdr, WebBrowserDocumentCompletedEventArgs we)
            {
                if (!flag)
                {
                    Debug.WriteLine(webBrowser1.DocumentText);
                    string str = webBrowser1.DocumentText;
                    str = str.Replace("</body></html>", "");
                    for (int i = 0; i < 100; ++i)
                        str += "Lorem ipsum dolor sit amet, consectetur adipiscing elit. Maecenas ac augue at erat hendrerit dictum. Praesent porta, purus eget sagittis imperdiet, nulla mi ullamcorper metus, id hendrerit metus diam vitae est. Class aptent taciti sociosqu ad litora torquent per conubia nostra, per inceptos himenaeos.<br>";
                    str += "<form>你喜欢什么水果？<br>";
                    str += "<input name='fruit' type='radio' value='apple' />苹果<br/>";
                    str += "<input name='fruit' type='radio' value='orange' />橘子<br/>";
                    str += "<input name='fruit' type='radio' value='banana' />香蕉<br/>";
                    str += "<input name='fruit' type='radio' value='coconut' />椰子<br/>";
                    str += "</form></body></html>";
                    webBrowser1.DocumentText = str;
                    Debug.WriteLine(webBrowser1.DocumentText);
                }
                flag = true;
            };
        }

        private void WebBrowser1_DocumentCompleted(object sender, WebBrowserDocumentCompletedEventArgs e)
        {
            throw new NotImplementedException();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            webBrowser1.Document.InvokeScript("a1");
        }
    }
}
