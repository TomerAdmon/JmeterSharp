using System.Diagnostics;

namespace JmeterSharp
{
    public class ProcessManager
    {
        private readonly string m_Path;
        private readonly string m_Arguments;

        public ProcessManager(string path, string arguments)
        {
            m_Path = path;
            m_Arguments = arguments;
        }

        public string Start()
        {
            var process = new Process
            {
                StartInfo = { FileName = m_Path }
            };

            process.StartInfo.Arguments = m_Arguments;
            process.StartInfo.UseShellExecute = false;
            process.StartInfo.Verb = "runas";
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