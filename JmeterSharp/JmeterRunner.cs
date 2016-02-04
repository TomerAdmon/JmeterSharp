using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;

namespace JmeterSharp
{
    public enum PluginTypeGraph
    {
        ThreadsStateOverTime,
        BytesThroughputOverTime,
        HitsPerSecond,
        LatenciesOverTime,
        ResponseCodesPerSecond,
        ResponseTimesDistribution,
        ResponseTimesOverTime,
        ResponseTimesPercentiles,
        ThroughputVsThreads,
        TimesVsThreads,
        TransactionsPerSecond,
        PageDataExtractorOverTime,
        MergeResults
    }
    public enum PluginTypeCsv
    {
        AggregateReport,
        SynthesisReport
    }

    public class JmeterRunner
    {
        private readonly string _path;
        public readonly ArgsBuilder ArgsBuilder;

        public JmeterRunner(string path, ArgsBuilder argsBuilder)
        {
            _path = path;
            ArgsBuilder = argsBuilder;
        }

        public static bool IsJavaInstalled()
        {
            var environmentVariable = Environment.GetEnvironmentVariable("PATH");
            if (environmentVariable != null)
            {
                var enumerable = environmentVariable.Split(';').Where(x => x.Contains("Java")).ToArray();
            }
            return true;
        }

        public string CreateSummaryReport(PluginTypeCsv pluginTypeCsv, string fileName, string logFile)
        {
            return StartCmdPlugin(fileName, "--generate-csv", logFile, pluginTypeCsv.ToString());
        }

        public string CreateGraph(PluginTypeGraph pluginTypeGraph, string fileName, string logFile)
        {
            return StartCmdPlugin(fileName, "--generate-png", logFile, pluginTypeGraph.ToString());
        }

        private string StartCmdPlugin(string fileName, string flagName, string file, string pluginType)
        {
            string cmdRunner = _path.Replace(@"bin\jmeter.bat", @"\lib\ext\JMeterPluginsCMD.bat");
            var args = string.Format(
                "{0} \"{1}\" --input-jtl {2} --plugin-type {3}", flagName, fileName, file, pluginType);
            var process = new ProcessManager(cmdRunner, args);
            return process.Start();
        }

        public string Start()
        {
            var process = new ProcessManager(_path, ArgsBuilder.Build());
            return process.Start();
        }
    }
}
