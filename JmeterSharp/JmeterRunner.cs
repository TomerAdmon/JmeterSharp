using System;
using System.IO;
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
        private readonly string m_Path;
        public readonly ArgsBuilder ArgsBuilder;

        public JmeterRunner(string path, ArgsBuilder argsBuilder)
        {
            m_Path = path;
            ArgsBuilder = argsBuilder;
        }

        public string CreateSummaryReport(PluginTypeCsv pluginTypeCsv, string fileName, string logFile)
        {
            var results = StartCmdPlugin(fileName, "--generate-csv", logFile, pluginTypeCsv.ToString());
            ValidateCommandResult(results);
            return results;
        }

        private static void ValidateCommandResult(string results)
        {
            if (results.Contains("ERROR"))
                throw new Exception(string.Format("Error occured during summary report creation: {0}", results));
        }

        public double GetErrorsRate(string logsPath)
        {
            const string tempSummaryReportCsv = "TempSummaryReport.csv";

            CreateSummaryReport(PluginTypeCsv.AggregateReport, tempSummaryReportCsv, logsPath);
            
            var lastLine = File.ReadLines(tempSummaryReportCsv).Last();
            var precent = lastLine.Split(',').First(x => x.Contains('%')).Split('%').First(); //TOTAL,18,441,379,616,167,1046,83.33%,.0,.0,203.04
            File.Delete(tempSummaryReportCsv);
            return Convert.ToDouble(precent);
        }

        public long GetTotalDuration(string logsPath)
        {
            var lines = File.ReadLines(logsPath).Skip(1);
            return lines.Sum(line => Convert.ToInt64(line.Split(',')[1]));
        }

        public string CreateGraph(PluginTypeGraph pluginTypeGraph, string fileName, string logFile)
        {
            var results = StartCmdPlugin(fileName, "--generate-png", logFile, pluginTypeGraph.ToString());
            ValidateCommandResult(results);
            return results;
        }

        private string StartCmdPlugin(string fileName, string flagName, string file, string pluginType)
        {
            string cmdRunner = m_Path.Replace(@"bin\jmeter.bat", @"\lib\ext\JMeterPluginsCMD.bat");
            var args = string.Format(
                "{0} \"{1}\" --input-jtl {2} --plugin-type {3}", flagName, fileName, file, pluginType);
            var process = new ProcessManager(cmdRunner, args);
            return process.Start();
        }

        public string Start()
        {
            var process = new ProcessManager(m_Path, ArgsBuilder.Build());
            return process.Start();
        }
    }
}
