using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using System.Runtime.InteropServices;
using System.Security.Cryptography;
using Newtonsoft.Json;
using System.Threading;

namespace WLIDAR.BackEnd.UI
{
    /// <summary>
    /// 通用工具类。
    /// </summary>
    public class CommonToolkit
    {


        /// <summary>
        /// 结构体转字节数组。
        /// </summary>
        /// <param name="objStruct"></param>
        /// <param name="by"></param>
        /// <param name="nOffset"></param>
        public static void StructToBytes(object objStruct, ref byte[] by, ref int nOffset)
        {
            int nSize = Marshal.SizeOf(objStruct);
            IntPtr p = Marshal.AllocHGlobal(nSize);
            Marshal.StructureToPtr(objStruct, p, false);
            Marshal.Copy(p, by, nOffset, nSize);
            Marshal.FreeHGlobal(p);
            nOffset += nSize;
        }
        /// <summary>
        /// 合并字节数组。
        /// </summary>
        /// <param name="headerBytes"></param>
        /// <param name="bodyBytes"></param>
        /// <returns></returns>
        public static byte[] CombineSendBuffer(byte[] headerBytes, byte[] bodyBytes)
        {
            IntPtr ptr = IntPtr.Zero;
            try
            {
                int bufferSize = headerBytes.Length + bodyBytes.Length;
                ptr = Marshal.AllocHGlobal(bufferSize);

                // 拷贝包头到缓冲区首部
                Marshal.Copy(headerBytes, 0, ptr, headerBytes.Length);

                // 拷贝包体到缓冲区剩余部分
                Marshal.Copy(bodyBytes, 0, ptr + headerBytes.Length, bodyBytes.Length);

                byte[] bytes = new byte[bufferSize];
                Marshal.Copy(ptr, bytes, 0, bufferSize);

                return bytes;
            }
            finally
            {
                if (ptr != IntPtr.Zero)
                {
                    Marshal.FreeHGlobal(ptr);
                }
            }
        }
        /// <summary>
        /// 字节数组转结构体。
        /// </summary>
        /// <typeparam name="TStruct"></typeparam>
        /// <param name="by"></param>
        /// <param name="nOffset"></param>
        /// <returns></returns>
        public static TStruct BytesToStruct<TStruct>(byte[] by, ref int nOffset)
            where TStruct : struct
        {
            int nSize = Marshal.SizeOf(typeof(TStruct));
            IntPtr p = Marshal.AllocHGlobal(nSize);
            try
            {
                Marshal.Copy(by, nOffset, p, nSize);
                return (TStruct)Marshal.PtrToStructure(p, typeof(TStruct));
            }
            finally
            {
                nOffset += nSize;
                Marshal.FreeHGlobal(p);
            }
        }
    }

    /// <summary>
    /// 文件工具。
    /// </summary>
    public static class FileWrap
    {
        /// <summary>
        /// 复制文件。
        /// </summary>
        /// <param name="strSrcDir"></param>
        /// <param name="strDstDir"></param>
        public static void Copy(string strSrcDir, string strDstDir)
        {
            if (!Directory.Exists(strDstDir))
                Directory.CreateDirectory(strDstDir);

            DirectoryInfo di = new DirectoryInfo(strSrcDir);
            FileInfo[] fis = di.GetFiles();
            foreach (FileInfo fi in fis)
                File.Copy(fi.FullName, strDstDir + @"\" + fi.Name, true);

            DirectoryInfo[] disChild = di.GetDirectories();
            foreach (DirectoryInfo diChild in disChild)
                Copy(diChild.FullName, strDstDir + @"\" + diChild.Name);
        }
        /// <summary>
        /// 删除文件夹及文件。
        /// </summary>
        /// <param name="dir"></param>
        public static void DeleteDirAndFiles(string dir)
        {
            try
            {
                DirectoryInfo fileInfo = new DirectoryInfo(dir);
                fileInfo.Attributes = FileAttributes.Normal & FileAttributes.Directory;
                //去除文件的只读属性
                File.SetAttributes(dir, FileAttributes.Normal);

                //判断文件夹是否还存在
                if (Directory.Exists(dir))
                {
                    foreach (string f in Directory.GetFileSystemEntries(dir))
                    {
                        if (File.Exists(f))
                        {
                            //如果有子文件删除文件
                            File.Delete(f);
                        }
                        else
                        {
                            //循环递归删除子文件夹
                            DeleteDirAndFiles(f);
                        }
                    }
                    //删除空文件夹
                    Directory.Delete(dir, true);
                }
            }
            catch (Exception ex)
            {

            }
        }
        /// <summary>
        /// 获取不重复的名字。
        /// </summary>
        /// <param name="name"></param>
        /// <param name="allNames"></param>
        /// <returns></returns>
        public static string GetAvailableName(string name, List<string> allNames)
        {
            int i = 0, j = 1;
            string str = name;
            while (true)
            {
                for (i = 0; i < allNames.Count; i++)
                {
                    if (string.Compare(str, allNames[i], true) == 0)
                        break;
                }
                if (i == allNames.Count)
                    return str;
                str = string.Format("{0}_{1}", name, j++);
            }
        }
    }
    /// <summary>
    /// JSON工具。
    /// </summary>
    public static class JsonWrap
    {
        /// <summary>
        /// serialize JSON to a string.
        /// </summary>
        /// <param name="obj"></param>
        internal static string SerializeObject(object obj)
        {
            string strJson = JsonConvert.SerializeObject(obj, Formatting.Indented);
            return strJson;
        }

        internal static T DeserializeObject<T>(string strJson)
        {
            T deserializedT = JsonConvert.DeserializeObject<T>(strJson);
            return deserializedT;
        }

        /// <summary>
        /// write string to a file.
        /// </summary>
        /// <param name="obj"></param>
        /// <param name="filePath"></param>
        internal static void WriteToJsonFile(object obj, string filePath)
        {
            File.WriteAllText(filePath, SerializeObject(obj));
        }
    }

    /// <summary>
    /// 消息工具。
    /// </summary>
    public static class MessageWrap
    {
        public const int WM_USER = 0x0400;
        public const int WM_RAY_ADDED = WM_USER + 100;
        public const int WM_USER_CHANGED = WM_USER + 101;
        public const int WM_SELECTED_RAY_CHANGED = WM_USER + 102;
        public const int WM_SELECTED_BIN_CHANGED = WM_USER + 103;
        public const int WM_DISTANCE = WM_USER + 104;
        public const int WM_MDICHILD_CLOSED = WM_USER + 105;
        public const int WM_MAPBACKCOLOR_CHANGED = WM_USER + 106;
        public const int WM_UPDATE_MOUSEFOLLOWINFO = WM_USER + 107;
        public const int WM_WAVEFORM_CHANGED = WM_USER + 108;

        [DllImport("user32.dll")]
        public static extern IntPtr SendMessage(IntPtr hwnd, int msg, int wparam, int lparam);
        [DllImport("user32.dll")]
        public static extern IntPtr SendMessage(IntPtr hwnd, int msg, IntPtr wparam, int lparam);

        public static void SendMessage(Control ctrl, int msg, int wparam, int lparam)
        {
            if (ctrl.InvokeRequired)
            {
                //Console.WriteLine("SendMessage.");
                ThreadStart method = delegate
                {
                    SendMessage(ctrl.Handle, msg, wparam, lparam);
                };
                ctrl.BeginInvoke(method);
            }
            else
            {
                SendMessage(ctrl.Handle, msg, wparam, lparam);
            }
        }
    }


    public static class UserWrap
    {
        private const int SaltByteSize = 24;
        private const int HashByteSize = 24;
        private const int HasingIterationsCount = 10101;
        //---------------------------------------------------------------------------------------------------
        /// <summary>
        /// 生成用户信息.
        /// </summary>
        /// <param name="password"></param>
        /// <returns></returns>
        public static UserInfo GenerateUserInfo(string name, string password, string role)
        {
            UserInfo userInfo = new UserInfo();
            userInfo.Name = name;
            byte[] salt = GenerateSalt();
            byte[] hash = ComputeHash(password, salt);
            userInfo.PasswordSalt = Convert.ToBase64String(salt);
            userInfo.PasswordHash = Convert.ToBase64String(hash);
            userInfo.Role = role;
            return userInfo;
        }
        //---------------------------------------------------------------------------------------------------
        /// <summary>
        /// 生成盐度.
        /// </summary>
        /// <param name="saltByteSize"></param>
        /// <returns></returns>
        public static byte[] GenerateSalt(int saltByteSize = SaltByteSize)
        {
            using (RNGCryptoServiceProvider saltGenerator = new RNGCryptoServiceProvider())
            {
                byte[] salt = new byte[saltByteSize];
                saltGenerator.GetBytes(salt);
                return salt;
            }
        }
        //---------------------------------------------------------------------------------------------------
        /// <summary>
        /// 验证密码是否正确.
        /// </summary>
        /// <param name="password"></param>
        /// <param name="passwordSalt"></param>
        /// <param name="passwordHash"></param>
        /// <returns></returns>
        public static bool VerifyPassword(string password, byte[] passwordSalt, byte[] passwordHash)
        {
            byte[] computedHash = ComputeHash(password, passwordSalt);
            return AreHashesEqual(computedHash, passwordHash);
        }
        //---------------------------------------------------------------------------------------------------
        /// <summary>
        /// 判断哈希值是否相等.
        /// </summary>
        /// <param name="firstHash"></param>
        /// <param name="secondHash"></param>
        /// <returns></returns>
        public static bool AreHashesEqual(byte[] firstHash, byte[] secondHash)
        {
            int minHashLength = firstHash.Length <= secondHash.Length ? firstHash.Length : secondHash.Length;
            var xor = firstHash.Length ^ secondHash.Length;
            for (int i = 0; i < minHashLength; i++)
                xor |= firstHash[i] ^ secondHash[i];
            return 0 == xor;
        }
        //---------------------------------------------------------------------------------------------------
        /// <summary>
        /// 计算哈希值.
        /// </summary>
        /// <param name="password"></param>
        /// <param name="salt"></param>
        /// <param name="iterations"></param>
        /// <param name="hashByteSize"></param>
        /// <returns></returns>
        public static byte[] ComputeHash(string password, byte[] salt, int iterations = HasingIterationsCount, int hashByteSize = HashByteSize)
        {
            using (Rfc2898DeriveBytes hashGenerator = new Rfc2898DeriveBytes(password, salt))
            {
                hashGenerator.IterationCount = iterations;
                return hashGenerator.GetBytes(hashByteSize);
            }
        }
    }

    public class UserInfo
    {
        public string Name { get; internal set; }
        public string PasswordSalt { get; internal set; }
        public string PasswordHash { get; internal set; }
        public string Role { get; internal set; }
    }
}
