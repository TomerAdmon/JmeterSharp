using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace JmeterSharp.Tests
{
    [TestClass]
    public class JmeterRunnerTests
    {
        [TestMethod]
        public void Test1()
        {
            const string logsFile = @"C:\JmeterLogs\logs.jtl";
            const string jmeterBat = @"C:\apache-jmeter-2.12\bin\jmeter.bat";
            const string jmeterTestPlan = @"C:\apache-jmeter-2.12\printable_docs\demos\ForEachTest2.jmx";
            
            /* Build Jmeter Runner configuration
             */
            var jmeterRunner = new JmeterRunner(jmeterBat, new ArgsBuilder());
            jmeterRunner. ArgsBuilder
                .NonGui() //not Gui
                .CollectReportableData(logsFile) //Collect data
                .WithTestPlan(jmeterTestPlan); // use this test plan
            
            /* Execute Test Plan
             */
            jmeterRunner.Start();

            /* Create Report
             */
            jmeterRunner.CreateSummaryReport(PluginType.AggregateReport,
                string.Format(@"C:\JmeterLogs\Summary{0}.csv", DateTime.Now.ToString("yyyyMMddHHmmssfff")), logsFile);

            Console.WriteLine(
            jmeterRunner.CreateGraph(PluginType.HitsPerSecond, 
                string.Format(@"C:\JmeterLogs\Graph{0}.png", DateTime.Now.ToString("yyyyMMddHHmmssfff")), logsFile));
        }
    }
}