# JmeterSharp

Jmeter Runner Wrapper for C#

Example:
```C#
Build Jmeter Runner configuration
var jmeterRunner = new JmeterRunner(jmeterBat, new ArgsBuilder());
jmeterRunner. ArgsBuilder
    .NonGui() //not Gui
    .CollectReportableData(logsFile) //Collect data
    .WithTestPlan(jmeterTestPlan); // use this test plan
```

Execute Test Plan
```
jmeterRunner.Start();
```

Create Reports
```
jmeterRunner.CreateSummaryReport(PluginType.AggregateReport,
    string.Format(@"C:\JmeterLogs\Summary{0}.csv", DateTime.Now.ToString("yyyyMMddHHmmssfff")), logsFile);

jmeterRunner.CreateGraph(PluginType.HitsPerSecond, 
    string.Format(@"C:\JmeterLogs\Graph{0}.png", DateTime.Now.ToString("yyyyMMddHHmmssfff")), logsFile);
```

