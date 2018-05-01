using System;
using System.Collections.Generic;
using System.IO;

namespace Hackerrank
{
    class Program
    {
        static Dictionary<string, long> memo;
        static int hits = 0;
        static int boardCutting(int[] cost_y, int[] cost_x)
        {
            memo = new Dictionary<string, long>();
            var result = compute(cost_x, 0, cost_x.Length - 1, cost_y, 0, cost_y.Length - 1);

            return mod(result);
        }

        static long compute(int[] x, int xs, int xe, int[] y, int ys, int ye)
        {
            var emptyX = xs > xe;
            var emptyY = ys > ye;

            if (emptyX && emptyY)
            {
                return 0;
            }

            var sum = 0;
            int i;
            if (!emptyX && emptyY)
            {
                for (i = xs; i <= xe; i++)
                {
                    sum = mod(sum + x[i]);
                }
                return sum;
            }
            if (!emptyY && emptyX)
            {
                for (i = ys; i <= ye; i++)
                {
                    sum = mod(sum + y[i]);
                }
                return sum;
            }

            var maxxpos = maxPos(x, xs, xe); var maxfromx = x[maxxpos];
            var maxypos = maxPos(y, ys, ye); var maxfromy = y[maxypos];

            string kl1, kl2, kr1, kr2;
            long l1, l2, r1, r2;
            if (maxfromx > maxfromy)
            {
                kl1 = $"{xs}_{maxxpos - 1}__{ys}_{ye}";
                if (!memo.TryGetValue(kl1, out l1)) { l1 = compute(x, xs, maxxpos - 1, y, ys, ye); memo.Add(kl1, l1); }
                kr1 = $"{maxxpos + 1}_{xe}__{ys}_{ye}";
                if (!memo.TryGetValue(kr1, out r1)) { r1 = compute(x, maxxpos + 1, xe, y, ys, ye); memo.Add(kr1, r1); }
                return mod(maxfromx + l1 + r1);
            }
            if (maxfromx < maxfromy)
            {
                kl2 = $"{xs}_{xe}__{ys}_{maxypos - 1}";
                if (!memo.TryGetValue(kl2, out l2)) { l2 = compute(x, xs, xe, y, ys, maxypos - 1); memo.Add(kl2, l2); }
                kr2 = $"{xs}_{xe}__{maxypos + 1}_{ye}";
                if (!memo.TryGetValue(kr2, out r2)) { r2 = compute(x, xs, xe, y, maxypos + 1, ye); memo.Add(kr2, r2); }
                return mod(maxfromy + l2 + r2);
            }
            kl1 = $"{xs}_{maxxpos - 1}__{ys}_{ye}";
            if (!memo.TryGetValue(kl1, out l1)) { l1 = compute(x, xs, maxxpos - 1, y, ys, ye); memo.Add(kl1, l1); }
            kr1 = $"{maxxpos + 1}_{xe}__{ys}_{ye}";
            if (!memo.TryGetValue(kr1, out r1)) { r1 = compute(x, maxxpos + 1, xe, y, ys, ye); memo.Add(kr1, r1); }

            kl2 = $"{xs}_{xe}__{ys}_{maxypos - 1}";
            if (!memo.TryGetValue(kl2, out l2)) { l2 = compute(x, xs, xe, y, ys, maxypos - 1); memo.Add(kl2, l2); }
            kr2 = $"{xs}_{xe}__{maxypos + 1}_{ye}";
            if (!memo.TryGetValue(kr2, out r2)) { r2 = compute(x, xs, xe, y, maxypos + 1, ye); memo.Add(kr2, r2); }

            var fromx = mod(maxfromx + l1 + r1);
            var fromy = mod(maxfromy + l2 + r2);

            return mod(Math.Min(fromx, fromy));
        }

        static int maxPos(int[] arr, int start, int end)
        {
            var max = Int32.MinValue;
            var pos = -1;
            for (int i = start; i <= end; i++)
            {
                if (arr[i] > max)
                {
                    max = arr[i];
                    pos = i;
                }
            }
            return pos;
        }

        static int mod(long x)
        {
            return (int)(x % (1000000000 + 7));
        }

        static void Main(String[] args)
        {
            var idx = 0;
            var lines = File.ReadAllLines("input1.txt");
            int q = Convert.ToInt32(lines[idx++]);
            for (int a0 = 0; a0 < q; a0++)
            {
                string[] tokens_m = lines[idx++].Split(' ');
                int m = Convert.ToInt32(tokens_m[0]);
                int n = Convert.ToInt32(tokens_m[1]);
                string[] cost_y_temp = lines[idx++].Split(' ');
                int[] cost_y = Array.ConvertAll(cost_y_temp, Int32.Parse);
                string[] cost_x_temp = lines[idx++].Split(' ');
                int[] cost_x = Array.ConvertAll(cost_x_temp, Int32.Parse);
                int result = boardCutting(cost_y, cost_x);
                Console.WriteLine(result);

                Console.ReadLine();
            }
        }
    }
}
