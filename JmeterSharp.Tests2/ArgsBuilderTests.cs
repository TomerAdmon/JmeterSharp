using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace JmeterSharp.Tests
{
    [TestClass]
    public class ArgsBuilderTests
    {
        [TestMethod]
        public void ArgumentParsing()
        {
            var argsBuilder = new ArgsBuilder();
            var build = argsBuilder.NonGui().LogTo(@"c:\tom\a a").SetPropertiesFile(@"c:\a.txt").WithTestPlan(@"a.txt").Build();
            Assert.AreEqual(build, "-n -l \"c:\\tom\\a a\" -p \"c:\\a.txt\" -t \"a.txt\"");
        }

        [TestMethod, Ignore]
        public void TestPlanExecution()
        {
            const string path = @"%jmeter%\bin\jmeter.bat";

            var argsBuilder = new ArgsBuilder()
                .NonGui()
                .LogTo(@"LoopTestPlan.jtl")
                .WithTestPlan(@"LoopTestPlan.jmx")
                .WithFailedAssersionReport();
            
            var jmeterRunner = new JmeterRunner(path,argsBuilder);
            jmeterRunner.Start();
            var logPath = @"LoopTestPlan.jtl";
            jmeterRunner.CreateSummaryReport(PluginTypeCsv.AggregateReport, @"LoopTestPlan.csv", logPath);
            var errorsRate = jmeterRunner.GetErrorsRate(logPath);
            Assert.AreEqual(errorsRate, 83.33d);
            var totalDuration = jmeterRunner.GetTotalDuration(logPath);
            Assert.AreNotEqual(totalDuration,0);
        }
    }
}