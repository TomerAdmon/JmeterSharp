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
            var a = new ArgsBuilder();
            var build = a.NonGui().LogTo(@"c:\tom\a a").SetPropertiesFile(@"c:\a.txt").WithTestPlan(@"a.txt").Build();
            Assert.AreEqual(build, "-n -l \"c:\\tom\\a a\" -p \"c:\\a.txt\" -t \"a.txt\"");
        }
    }
}