using System;
using System.Diagnostics;

namespace JmeterSharp
{
    public class ProcessManager
    {
        private readonly string _path;
        private readonly string _arguments;

        public ProcessManager(string path, string arguments)
        {
            _path = path;
            _arguments = arguments;
        }

        public string Start()
        {
            var process = new Process
            {
                StartInfo = { FileName = _path }
            };

            process.StartInfo.Arguments = _arguments;
            process.StartInfo.UseShellExecute = false;
            process.StartInfo.Verb = "runas";
       //     process.StartInfo.WorkingDirectory = @"c:\users\tomer.a";
            process.StartInfo.RedirectStandardOutput = true;
            var stdout = string.Empty;
            try
            {
                process.Start();
                var exit = process.WaitForExit(10000);
                while (!process.StandardOutput.EndOfStream)
                {
                    var line = process.StandardOutput.ReadLine();
                    stdout += line;
                }
            }
            catch
            {
                process.Dispose();
                process = null;
                throw;
            }
            return stdout;
        }
    }
}