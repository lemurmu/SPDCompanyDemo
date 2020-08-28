using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RedisDemo
{
    class Program
    {
        static void Main(string[] args)
        {
            //启动Redis
            string redisPath = "Redis3\\redis-server.exe";
            Process myprocess = new Process();

            ProcessStartInfo startInfo = new ProcessStartInfo(redisPath, null);
            startInfo.CreateNoWindow = true;
            startInfo.RedirectStandardOutput = true;
            startInfo.UseShellExecute = false;

            myprocess.StartInfo = startInfo;

            bool flag = false;
            System.Threading.Mutex hMutex = new System.Threading.Mutex(true, "redis-server", out flag);//进程间的同步
            bool b = hMutex.WaitOne(0, false);
            if (flag)
            {
                myprocess.Start();
            }

            RedisHelper helper = new RedisHelper();
            byte[] data = new byte[3] { 0x23, 0x34, 0x56 };

            helper.Set("aaf", "nishizhu", 10);
            helper.Set("abc", data, 10);
            var value = helper.Get<string>("aaf");
            byte[] bytes = helper.Get<byte[]>("abc");
            Console.WriteLine(value);
            Console.WriteLine(BitConverter.ToString(bytes));
            Console.ReadKey();

        }
    }
}
