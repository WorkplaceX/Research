﻿namespace Build
{
    using System;
    using System.Collections.Generic;
    using System.Diagnostics;
    using System.IO;
    using System.Reflection;
    using System.Runtime.InteropServices;
    using System.Linq;

    public static class Util
    {
        public static void OpenBrowser(string url)
        {
            if (RuntimeInformation.IsOSPlatform(OSPlatform.Windows))
            {
                Process.Start(new ProcessStartInfo("cmd", $"/c start {url}")); // Works ok on windows
            }
        }

        public static void OpenCode(string folderName)
        {
            if (RuntimeInformation.IsOSPlatform(OSPlatform.Windows))
            {
                ProcessStartInfo info = new ProcessStartInfo(ConnectionManager.CodeFileName, folderName);
                info.CreateNoWindow = true;
                Process.Start(info);
            }
        }

        public static MethodInfo[] MethodList(Type type)
        {
            List<MethodInfo> result = new List<MethodInfo>();
            foreach (var methodInfo in type.GetTypeInfo().GetMethods())
            {
                if (methodInfo.DeclaringType == type)
                {
                    result.Add(methodInfo);
                }
            }
            return result.ToArray();
        }

        public static void MethodExecute(Type type)
        {
            int number = 0;
            foreach (var methodInfo in Util.MethodList(typeof(Script)))
            {
                number += 1;
                string text = number + "=" + methodInfo.Name;
                DescriptionAttribute description = methodInfo.GetCustomAttribute(typeof(DescriptionAttribute)) as DescriptionAttribute;
                if (description != null)
                {
                    text += " " + "(" + description.Text + ")";
                }
                Util.Log(text);
            }
            Console.Write(">");
            string numberText = Console.ReadLine();
            int numberInt = int.Parse(numberText);
            try
            {
                Util.MethodList(typeof(Script))[numberInt - 1].Invoke(null, new object[] { });
            }
            catch (Exception exception)
            {
                string message = exception.Message;
                if (exception.InnerException != null)
                {
                    message = exception.InnerException.Message;
                }
                Util.Log(message);
            }
        }

        public static void Start(string workingDirectory, string fileName, string arguments, bool isThrowException = true, bool isWait = true, KeyValuePair<string, string>? environment = null)
        {
            ProcessStartInfo info = new ProcessStartInfo();
            if (environment != null)
            {
                info.Environment.Add(environment.Value.Key, environment.Value.Value);
            }
            info.WorkingDirectory = workingDirectory;
            info.FileName = fileName;
            info.Arguments = arguments;
            var process = Process.Start(info);
            if (isWait == true)
            {
                process.WaitForExit();
                if (isThrowException && process.ExitCode != 0)
                {
                    throw new Exception("Script failed!");
                }
            }
        }

        private static void Process_Exited(object sender, EventArgs e)
        {
            throw new NotImplementedException();
        }

        public static void NpmInstall(string workingDirectory, bool isThrowException = true)
        {
            string fileName = ConnectionManager.NpmFileName;
            Start(workingDirectory, fileName, "install", isThrowException);
        }

        public static void NpmRun(string workingDirectory, string script)
        {
            string fileName = ConnectionManager.NpmFileName;
            Start(workingDirectory, fileName, "run " + script);
        }

        public static void Node(string workingDirectory, string fileName, bool isWait = true)
        {
            string nodeFileName = ConnectionManager.NodeFileName;
            Start(workingDirectory, nodeFileName, fileName, false, isWait, new KeyValuePair<string, string>("PORT", "1337"));
        }

        public static void DotNetRestore(string workingDirectory)
        {
            string fileName = ConnectionManager.DotNetFileName;
            Start(workingDirectory, fileName, "restore");
        }

        public static void DotNetBuild(string workingDirectory)
        {
            string fileName = ConnectionManager.DotNetFileName;
            Start(workingDirectory, fileName, "build");
        }

        public static void DotNetRun(string workingDirectory, bool isWait = true)
        {
            string fileName = ConnectionManager.DotNetFileName;
            Start(workingDirectory, fileName, "run", false, isWait);
        }

        public static void Log(string text)
        {
            Console.WriteLine(text);
        }

        public static string FileRead(string fileName)
        {
            return File.ReadAllText(fileName);
        }

        public static string[] FileNameList(string folderName)
        {
            var result = Directory.GetFiles(folderName, "*.*", SearchOption.AllDirectories).OrderBy(item => item).ToArray();
            return result;
        }
    }
}