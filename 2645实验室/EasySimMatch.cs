using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Matching
{
    class EasySimMatch
    {
        public static int getLcm(int a, int b)
        {
            int p = a;
            int q = b;
            int gcd = 0;
            while (true)
            {
                if (p % q == 0)
                {
                    gcd = q;
                    break;
                }
                else
                {
                    int r = p % q;
                    p = q;
                    q = r;
                }
            }
            return a * b / gcd;
        }
        static public int[][] reSample(int[] a, int[] b)
        {
            List<int> s1 = a.ToList<int>();
            List<int> s2 = b.ToList<int>();

            s1.RemoveAll(item => item == 0);
            s2.RemoveAll(item => item == 0);

            if (s1.Count > s2.Count)
                s1 = reSample(s1, s2.Count);
            else
                s2 = reSample(s2, s1.Count);

            return new int[][] { s1.ToArray(), s2.ToArray() };
        }

        static public List<int> reSample(List<int> s1, int precision)
        {
            List<int> ret = new List<int>();
            int lcm = getLcm(s1.Count, precision);
            int n = (lcm - s1.Count) / s1.Count + 1;//插入n-1个点把这一小段分为n份
            //线性插入最后一个值
            if (s1.Count > 1)
                s1.Add(2 * s1[s1.Count - 1] - s1[s1.Count - 2]);
            else
                s1.Add(s1.Last());
            //开始线性插值
            for(int i = s1.Count - 1; i > 0; --i)
            {
                int dy = (s1[i] - s1[i - 1]) / n;
                for(int j = 1; j < n; ++j)
                {
                    s1.Insert(i, s1[i - 1] + j * dy);
                }
            }
            //去掉多插的最后一个值
            s1.RemoveAt(s1.Count - 1);
            for(int i = 0; i + precision <= s1.Count; i += precision)
            {
                int sum = 0;
                for(int j = 0; j < precision; ++j)
                {
                    sum += s1[i + j];
                }
                int avr = sum / precision;
                ret.Add(avr);
            }
            return ret;
        }
    }
}
