# JmeterSharp

Jmeter Runner Wrapper for C#

Example:
```C#
Build Jmeter Runner configuration
var jmeterRunner = new JmeterRunner(jmeterBat, new ArgsBuilder());
jmeterRunner. ArgsBuilder
    .NonGui() //not Gui
    .CollectReportableData(logsFile)
    .WithTestPlan(jmeterTestPlan);
```

Execute Test Plan
```
jmeterRunner.Start();
```

Create Reports
```
jmeterRunner.CreateSummaryReport(PluginTypeCsv.AggregateReport,
    string.Format(@"C:\JmeterLogs\Summary{0}.csv", DateTime.Now.ToString("yyyyMMddHHmmssfff")), logsFile);

jmeterRunner.CreateGraph(PluginTypeGraph.HitsPerSecond, 
    string.Format(@"C:\JmeterLogs\Graph{0}.png", DateTime.Now.ToString("yyyyMMddHHmmssfff")), logsFile);
```

